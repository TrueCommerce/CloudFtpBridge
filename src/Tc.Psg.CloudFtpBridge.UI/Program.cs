using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Tc.Psg.CloudFtpBridge.UI.Forms;

namespace Tc.Psg.CloudFtpBridge.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Constants.AppSettingsFileName))
                .Build();

            IServiceCollection services = new ServiceCollection()
                .AddLogging()
                .AddCloudFtpBridgeCore(configuration.GetSection(Constants.CloudFtpBridgeConfigSectionName))
                .AddCloudFtpBridgeUI();

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ILoggerFactory loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            loggerFactory.AddPsgLogging(configuration.GetSection(Constants.LoggingConfigSectionName));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(serviceProvider.GetService<MainForm>());
        }
    }
}
