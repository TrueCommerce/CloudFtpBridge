using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Polly;

using Tc.Psg.CloudFtpBridge.IO;
using Tc.Psg.CloudFtpBridge.Mail;

namespace Tc.Psg.CloudFtpBridge.Service
{
    public class PollingService
    {
        private bool _cancellationPending;
        private ILogger _log;

        public PollingService(IOptions<CloudFtpBridgeOptions> optionsAccessor, IFileManager fileManager, IMailSender mailSender, IWorkflowRepository workflowRepository)
        {
            _log = NullLogger.Instance;

            FileManager = fileManager;
            MailSender = mailSender;
            Options = optionsAccessor.Value;
            WorkflowRepository = workflowRepository;
        }

        public PollingService(IOptions<CloudFtpBridgeOptions> optionsAccessor, IFileManager fileManager, IMailSender mailSender, IWorkflowRepository workflowRepository, ILogger<PollingService> logger)
            : this(optionsAccessor, fileManager, mailSender, workflowRepository)
        {
            _log = logger;
        }

        public IFileManager FileManager { get; private set; }
        public IMailSender MailSender { get; private set; }
        public CloudFtpBridgeOptions Options { get; private set; }
        public IWorkflowRepository WorkflowRepository { get; private set; }

        public void Start()
        {
            _log.LogDebug("Starting polling service.");

            Task.Run(async () =>
            {
                await Policy
                    .Handle<Exception>()
                    .WaitAndRetryForeverAsync(retryAttempt => TimeSpan.FromSeconds(10))
                    .ExecuteAsync(async () =>
                    {
                        try
                        {
                            await _RunPollingLoop();
                        }

                        catch (Exception ex)
                        {
                            _log.LogError(ex, "An exception was thrown that caused the polling service to crash. It will be restarted in 10 seconds.");

                            try
                            {
                                await MailSender.Send("Polling Service Crash", "An exception was thrown that caused the polling service to crash. It will be restarted in 10 seconds.");
                            }

                            catch { }

                            throw;
                        }
                    });
            });

            _log.LogInformation("Polling service started successfully.");
        }

        public void Stop()
        {
            _log.LogDebug("Stopping polling service.");

            _cancellationPending = true;
        }

        private async Task _RunPollingLoop()
        {
            _log.LogDebug("Begin Checking Staging Folders for Existing Files");

            IEnumerable<Workflow> workflows = WorkflowRepository.GetAll();

            foreach (Workflow workflow in workflows)
            {
                await Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                    .ExecuteAsync(async () =>
                    {
                        try
                        {
                            await FileManager.ProcessStagedWorkflowFiles(workflow);
                        }

                        catch (Exception ex)
                        {
                            _log.LogError(ex, "Failed to process staged files for: {WorkflowName}. The polling service will try again shortly.", workflow.Name);

                            try
                            {
                                await MailSender.Send("Failed Cloud FTP Bridge Workflow", $"Failed to process staged files for: {workflow.Name}. The polling service will try again shortly.");
                            }

                            catch { }

                            throw;
                        }
                    });
            }

            _log.LogDebug("Finished Checking Staging Folders for Existing Files");

            while (!_cancellationPending)
            {
                workflows = WorkflowRepository.GetAll();

                foreach (Workflow workflow in workflows)
                {
                    await Policy
                        .Handle<Exception>()
                        .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                        .ExecuteAsync(async () =>
                        {
                            try
                            {
                                await FileManager.ExecuteWorkflow(workflow);
                            }

                            catch (Exception ex)
                            {
                                _log.LogError(ex, "Failed to execute workflow: {WorkflowName}. The polling service will try again shortly.", workflow.Name);

                                try
                                {
                                    await MailSender.Send("Failed Cloud FTP Bridge Workflow", $"Failed to execute workflow: {workflow.Name}. The polling service will try again shortly.");
                                }

                                catch { }

                                throw;
                            }
                        });
                }

                Thread.Sleep(TimeSpan.FromSeconds(Options.PollingInterval));
            }

            _log.LogInformation("Polling service stopped successfully.");
        }
    }
}
