using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

using Tc.Psg.CloudFtpBridge.IO;

namespace Tc.Psg.CloudFtpBridge.Service
{
    public class PollingService
    {
        private bool _cancellationPending;
        private ILogger _log;

        public PollingService(IOptions<CloudFtpBridgeOptions> optionsAccessor, IFileManager fileManager, IWorkflowRepository workflowRepository)
        {
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

        public void Start()
        {
            _log.LogDebug("Starting polling service.");

            Task.Run(async () =>
            {
                await _RunPollingLoop();
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
            while (!_cancellationPending)
            {
                IEnumerable<Workflow> workflows = WorkflowRepository.GetAll();

                foreach (Workflow workflow in workflows)
                {
                    try
                    {
                        await FileManager.ExecuteWorkflow(workflow);
                    }

                    catch (Exception ex)
                    {
                        _log.LogError(ex, "Failed to execute workflow: {WorkflowName}. The polling service will retry again during the next polling iteration.", workflow.Name);
                    }
                }

                Thread.Sleep(TimeSpan.FromSeconds(Options.PollingInterval));
            }

            _log.LogInformation("Polling service stopped successfully.");
        }
    }
}
