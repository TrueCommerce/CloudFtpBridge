using CloudFtpBridge.Core.Services;
using CloudFtpBridge.Infrastructure.Json;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CloudFtpBridgeAppBuilderExtensions
    {
        public static CloudFtpBridgeAppBuilder UseJsonMailSettingsProvider(this CloudFtpBridgeAppBuilder builder)
        {
            builder.AddSingleton<IMailSettingsProvider, JsonMailSettingsProvider>();

            return builder;
        }
    }
}
