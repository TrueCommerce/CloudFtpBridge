using CloudFtpBridge.Infrastructure.FluentFTP;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CloudFtpBridgeAppBuilderExtensions
    {
        public static CloudFtpBridgeAppBuilder UseFluentFTPFileSystem(this CloudFtpBridgeAppBuilder builder)
        {
            builder.AddTransient<FluentFTPFileSystem>();

            return builder;
        }
    }
}
