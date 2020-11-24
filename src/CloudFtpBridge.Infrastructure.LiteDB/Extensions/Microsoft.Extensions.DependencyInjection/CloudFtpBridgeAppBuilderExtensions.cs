using System;
using System.IO;

using CloudFtpBridge.Core.Services;
using CloudFtpBridge.Core.Utils;
using CloudFtpBridge.Infrastructure.LiteDB;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CloudFtpBridgeAppBuilderExtensions
    {
        public static CloudFtpBridgeAppBuilder UseLiteDBStorage(this CloudFtpBridgeAppBuilder builder, Action<LiteDBOptions> configure = null)
        {
            Directory.CreateDirectory(PathHelper.GetDefaultStoragePath());

            configure = configure ?? new Action<LiteDBOptions>(o => { });

            builder.Configure(configure);

            builder
                .AddSingleton<IAuditLog, LiteDBAuditLog>()
                .AddSingleton<IWorkflowRepository, LiteDBWorkflowRepository>();

            return builder;
        }
    }
}
