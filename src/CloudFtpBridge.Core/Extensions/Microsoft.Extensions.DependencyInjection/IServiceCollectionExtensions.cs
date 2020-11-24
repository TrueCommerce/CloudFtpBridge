using System;

using CloudFtpBridge.Core.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCloudFtpBridge(this IServiceCollection services, Action<CloudFtpBridgeAppBuilder> configureApp)
        {
            services
                .AddSingleton<FileSystemActivator>()
                .AddSingleton<WorkflowRunner>();

            var builder = new CloudFtpBridgeAppBuilder(services);

            configureApp(builder);

            return services;
        }
    }
}
