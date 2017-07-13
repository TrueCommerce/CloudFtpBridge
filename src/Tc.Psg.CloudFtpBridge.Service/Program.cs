using System.IO;
using System.Reflection;
using System.ServiceProcess;

using Serilog;
using Serilog.Events;

using Tc.Psg.CloudFtpBridge.Configuration;

namespace Tc.Psg.CloudFtpBridge.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            CloudFtpBridgeConfig config = CloudFtpBridgeConfig.Load();
            FileInfo assemblyFileInfo = new FileInfo(Assembly.GetEntryAssembly().Location);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(config.DebugEnabled ? LogEventLevel.Debug : LogEventLevel.Warning)
                .WriteTo.RollingFile(Path.Combine(assemblyFileInfo.DirectoryName, @"Log\CloudFtpBridgeLog_{Date}.log"),
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {SourceContext} {Message}{NewLine}{Exception}")
                .CreateLogger();

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MainService()
            };

            ServiceBase.Run(ServicesToRun);
        }
    }
}
