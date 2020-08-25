using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Polly;

using Tc.Psg.CloudFtpBridge.IO;
using Tc.Psg.CloudFtpBridge.Logging;

namespace Tc.Psg.CloudFtpBridge.Service
{
    public class PollingService
    {
        private readonly CancellationTokenSource _tokenSource;

        private ILogger _log;
        private Task _pollTask;

        public PollingService(IOptions<CloudFtpBridgeOptions> optionsAccessor, IFileManager fileManager, IWorkflowRepository workflowRepository)
        {
            _tokenSource = new CancellationTokenSource();

            _log = NullLogger.Instance;

            FileManager = fileManager;
            Options = optionsAccessor.Value;
            WorkflowRepository = workflowRepository;
        }

        public PollingService(IOptions<CloudFtpBridgeOptions> optionsAccessor, IFileManager fileManager, IWorkflowRepository workflowRepository, ILogger<PollingService> logger)
            : this(optionsAccessor, fileManager, workflowRepository)
        {
            _log = logger;
        }

        public IFileManager FileManager { get; private set; }
        public CloudFtpBridgeOptions Options { get; private set; }
        public IWorkflowRepository WorkflowRepository { get; private set; }

        public async Task Poll(CancellationToken token)
        {
            try
            {
                try
                {
                    await ProcessStagedFiles();
                }

                catch (Exception ex)
                {
                    ex.MarkForEmailNotification();

                    _log.LogError(ex, "Failed to successfully process all staged files. Normal polling will continue, but some staged files may remain unprocessed until the next time all events complete successfully.");
                }

                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        await ProcessWorkflows();
                    }

                    catch (Exception ex)
                    {
                        ex.MarkForEmailNotification();

                        _log.LogError(ex, "Failed to successfully process all workflows. The service will try again in {PollingInterval} seconds.", Options.PollingInterval);
                    }

                    try
                    {
                        await Task.Delay(TimeSpan.FromSeconds(Options.PollingInterval), token);
                    }

                    catch
                    {
                        // swallow exception due to task cancellation
                    }
                }
            }

            catch (Exception ex)
            {
                ex.MarkForEmailNotification();

                _log.LogError(ex, "The polling service has crashed. It will need to be restarted manually.");
            }

            finally
            {
                _log.LogDebug("The polling service has been stopped.");
            }
        }

        public async Task ProcessStagedFiles()
        {
            _log.LogDebug("Begin Checking Staging Folders for Existing Files");

            IEnumerable<Workflow> workflows = WorkflowRepository.GetAll();

            foreach (Workflow workflow in workflows)
            {
                if (workflow.Direction == WorkflowDirection.InboundOptimizedNoArchive ||
                    workflow.Direction == WorkflowDirection.InboundOptimized)
                {
                    //Do nothing
                    _log.LogDebug("Skipping staging for Optimized Inbound. {WorkflowName}", workflow.Name);
                }
                else
                {
                    await Policy
                        .Handle<Exception>()
                        .WaitAndRetryAsync(5, x => _CalculateRetryInterval(x),
                            (ex, interval) =>
                            {
                                _log.LogError(ex,
                                    "Failed to process staged files for: {WorkflowName}. The polling service will try again in {RetryInterval} seconds.",
                                    workflow.Name, interval.TotalSeconds);
                            })
                        .ExecuteAsync(async () => { await FileManager.ProcessStagedWorkflowFiles(workflow); });
                }
            }

            _log.LogDebug("Finished Checking Staging Folders for Existing Files");
        }

        public async Task ProcessWorkflows()
        {
            IEnumerable<Workflow> workflows = WorkflowRepository.GetAll();

            foreach (Workflow workflow in workflows)
            {
                await Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync(5, x => _CalculateRetryInterval(x), (ex, interval) =>
                    {
                        _log.LogError(ex, "Failed to execute workflow: {WorkflowName}. The polling service will try again in {RetryInterval} seconds.", workflow.Name, interval.TotalSeconds);
                    })
                    .ExecuteAsync(async () =>
                    {
                        await FileManager.ExecuteWorkflow(workflow);
                    });
            }
        }

        public void Start()
        {
            _log.LogDebug("Starting polling service.");

            _pollTask = Poll(_tokenSource.Token);

            _log.LogInformation("Polling service started successfully.");
        }

        public void Stop()
        {
            _log.LogDebug("Stopping polling service.");

            _tokenSource.Cancel();
        }

        private TimeSpan _CalculateRetryInterval(int retryAttempt)
        {
            return TimeSpan.FromSeconds(((int)Math.Pow(3, retryAttempt) + Options.PollingInterval));
        }
    }
}
