using System;
using System.IO;
using System.Reflection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Topshelf;

using Tc.Psg.CloudFtpBridge.Logging;

namespace Tc.Psg.CloudFtpBridge.Service
{
    class Program
    {
        private const string _ServiceDescription = "Executes workflows to check for and transfer files between a local directory and an FTP folder.";
        private const string _ServiceDisplayName = "Cloud FTP Bridge Polling Service";
        private const string _ServiceName = "TcCloudFtpBridge";

        static void Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Constants.AppSettingsFileName))
                .Build();

            IServiceCollection services = new ServiceCollection()
                .AddLogging()
                .EnableTraceLogging()
                .AddCloudFtpBridgeCore(configuration.GetSection(Constants.CloudFtpBridgeConfigSectionName))
                .AddCloudFtpBridgeService();

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ILoggerFactory loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            loggerFactory
                .AddPsgLogging(configuration.GetSection(Constants.LoggingConfigSectionName))
                .AddProvider(serviceProvider.GetService<IEmailLoggerProvider>());

            TopshelfExitCode topshelfExitCode = HostFactory.Run(topshelf =>
            {
                topshelf.EnableServiceRecovery(x =>
                {
                    x.RestartService(1).RestartService(2).RestartService(4);
                });

                topshelf.Service<PollingService>(service =>
                {
                    service.ConstructUsing(() => serviceProvider.GetService<PollingService>());
                    service.WhenStarted(x => x.Start());
                    service.WhenStopped(x => x.Stop());
                });

                topshelf.RunAsLocalService();
                topshelf.SetDescription(_ServiceDescription);
                topshelf.SetDisplayName(_ServiceDisplayName);
                topshelf.SetServiceName(_ServiceName);
                topshelf.StartAutomatically();
            });

            int exitCode = (int)Convert.ChangeType(topshelfExitCode, topshelfExitCode.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
