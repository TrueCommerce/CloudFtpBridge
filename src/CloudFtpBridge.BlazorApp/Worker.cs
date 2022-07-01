using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using CloudFtpBridge.Core;
using CloudFtpBridge.Core.Models;
using CloudFtpBridge.Core.Services;

namespace CloudFtpBridge.BlazorApp
{
    public class Worker : BackgroundService
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly WorkflowRunner _workflowRunner;
        private readonly IOptionsMonitor<CoreOptions> _coreOptions;
        private readonly ILogger _logger;

        public Worker(
            IWorkflowRepository workflowRepository,
            WorkflowRunner workflowRunner,
            IOptionsMonitor<CoreOptions> coreOptions,
            ILogger<Worker> logger)
        {
            _workflowRepository = workflowRepository;
            _workflowRunner = workflowRunner;
            _coreOptions = coreOptions;
            _logger = logger;
        }

        /// <summary>
        /// The worker should:
        /// 1. Load a list of workflows that need to be run.
        /// 2. For each workflow, grab a new instance from the workflow repo just in case updates were made while other workflows were running.
        /// 3. After each workflow is run, the source file system is asked if it has more files to process (could have been added since the initial file listing was taken).
        ///    a. If more files ARE available, a flag is set so the delay at the end of the run is skipped and we immediately start back at step 1.
        /// 4 If the skipDelay flag hasn't been set, we wait 2 minutes before going back to step 1.
        /// </summary>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker Service Started");

            IReadOnlyCollection<Workflow> workflows = Array.Empty<Workflow>();

            while (!stoppingToken.IsCancellationRequested)
            {
                bool skipDelay = false;

                try
                {
                    workflows = await _workflowRepository.List();
                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to load workflows.");
                }

                foreach (var workflowRef in workflows)
                {
                    var workflow = await _workflowRepository.Get(workflowRef.Id);

                    if (workflow == null)
                    {
                        continue;
                    }

                    if (!workflow.Enabled)
                    {
                        _logger.LogInformation("Workflow {WorkflowName} is disabled and will be skipped.", workflow.Name);

                        continue;
                    }

                    try
                    {
                        _logger.LogDebug("Begin running workflow {WorkflowName}.", workflow.Name);

                        var hasMoreFiles = await _workflowRunner.Run(workflow);

                        skipDelay = skipDelay || hasMoreFiles;

                        _logger.LogDebug("Done running workflow {WorkflowName}.", workflow.Name);
                    }

                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to run workflow {WorkflowName}.", workflow.Name);
                    }
                }

                if (skipDelay)
                {
                    _logger.LogDebug("Skipping delay - there are already more files to process!");
                }

                else
                {
                    var workerDelay = _coreOptions.CurrentValue.WorkerDelay;

                    if (workerDelay.TotalSeconds < 30)
                    {
                        _logger.LogWarning("WorkerDelay was set to {OriginalWorkerDelay}, which is less than the minimum delay of 30 seconds. The configured value will be set to the minimum of 30 seconds.", workerDelay);

                        workerDelay = TimeSpan.FromSeconds(30);
                    }

                    _logger.LogDebug("Waiting {WorkerDelay} until the next run.", _coreOptions.CurrentValue.WorkerDelay);

                    await Task.Delay(_coreOptions.CurrentValue.WorkerDelay);
                }
            }

            _logger.LogInformation("Worker Service Stopped");
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker Service Starting...");

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker Service Stopping...");

            return base.StopAsync(cancellationToken);
        }
    }
}
