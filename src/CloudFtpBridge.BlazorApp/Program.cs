using System.IO;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Extensions.Logging;

using CloudFtpBridge.Core.Utils;

namespace CloudFtpBridge.BlazorApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService(options =>
                {
                    options.ServiceName = "TcCloudFtpBridge";
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();

                    var loggerConfig = new LoggerConfiguration()
                        .MinimumLevel.Verbose()
                        .Enrich.FromLogContext()
                        .Enrich.WithEnvironmentUserName()
                        .Enrich.WithMachineName()
                        .WriteTo.File(
                            path: Path.Combine(PathHelper.GetDefaultStoragePath(), @"Logs\CloudFtpBridge.txt"),
                            fileSizeLimitBytes: (250 * 1024 * 1024), // 250MB
                            rollingInterval: RollingInterval.Day,
                            rollOnFileSizeLimit: true);

                    logging.AddProvider(new SerilogLoggerProvider(loggerConfig.CreateLogger(), true));
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddCloudFtpBridge(context.Configuration, app =>
                    {
                        app.UseJsonMailSettingsProvider();
                        app.UseLiteDBLegacyStorage();
                        app.UseLiteDBStorage();
                        app.UseSmtpMailSender();

                        app.UseFluentFTPFileSystem();
                        app.UseLocalFileSystem();
                        app.UseFTPFileSystem();
                    });

                    services.AddHostedService<Worker>();
                });
    }
}
