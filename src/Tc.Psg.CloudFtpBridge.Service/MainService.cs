using System;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

using Serilog;

using Tc.Psg.CloudFtpBridge.Configuration;
using Tc.Psg.CloudFtpBridge.FileManagement;

namespace Tc.Psg.CloudFtpBridge.Service
{
    public partial class MainService : ServiceBase
    {
        private CancellationTokenSource _cancellationTokenSource;
        private CloudFtpBridgeConfig _config;
        private ILogger _log;

        public MainService()
        {
            InitializeComponent();

            _cancellationTokenSource = new CancellationTokenSource();
            _config = CloudFtpBridgeConfig.Load();
            _log = Log.ForContext<MainService>();
        }

        protected override void OnStart(string[] args)
        {
            _log.Debug("OnStart() BEGIN");

            CancellationToken token = _cancellationTokenSource.Token;

            Task.Factory.StartNew(async () =>
            {
                await _RunPollingLoop(token);
            }, token);

            _log.Debug("OnStart() END");
        }

        protected override void OnStop()
        {
            _log.Debug("OnStop() BEGIN");

            _cancellationTokenSource.Cancel();

            _log.Debug("OnStop() END");
        }

        private async Task _RunPollingLoop(CancellationToken cancellationToken)
        {
            _log.Debug("_RunPollingLoop() BEGIN");

            while (!cancellationToken.IsCancellationRequested)
            {
                _config = CloudFtpBridgeConfig.Load();

                foreach (CompanyConfig companyConfig in _config.Companies.Where(x => x.Enabled))
                {
                    _log.Debug("Processing files for {CompanyName} / {EdiId}.", companyConfig.Name, companyConfig.EdiId);

                    FileManager fileManager = new FileManager(companyConfig);

                    try
                    {
                        await fileManager.ProcessInboundFiles();
                    }

                    catch (Exception ex)
                    {
                        _log.Error(ex, "Unable to process inbound files for this iteration.");
                    }

                    try
                    {
                        await fileManager.ProcessOutboundFiles();
                    }

                    catch (Exception ex)
                    {
                        _log.Error(ex, "Unable to process outbound files for this iteration.");
                    }
                }

                _log.Debug("Waiting {PollingInterval} seconds until next iteration.", _config.PollingInterval);

                Thread.Sleep(TimeSpan.FromSeconds(_config.PollingInterval));
            }

            _log.Debug("_RunPollingLoop() END");
        }
    }
}
