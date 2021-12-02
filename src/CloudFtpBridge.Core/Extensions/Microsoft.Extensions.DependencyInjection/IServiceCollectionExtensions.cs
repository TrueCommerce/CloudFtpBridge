using System;

using CloudFtpBridge.Core;
using CloudFtpBridge.Core.Services;

using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCloudFtpBridge(this IServiceCollection services, IConfiguration configuration, Action<CloudFtpBridgeAppBuilder> configureApp)
        {
            services
                .Configure<CoreOptions>(configuration.GetSection("CloudFtpBridge"))
                .AddSingleton<FileSystemActivator>()
                .AddSingleton<FTPSystemActivator>()
                .AddSingleton<WorkflowRunner>();

            var builder = new CloudFtpBridgeAppBuilder(services);

            configureApp(builder);

            return services;
        }
    }
}
