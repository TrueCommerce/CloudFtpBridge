using CloudFtpBridge.Infrastructure.LocalFileSystem;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CloudFtpBridgeAppBuilderExtensions
    {
        public static CloudFtpBridgeAppBuilder UseLocalFileSystem(this CloudFtpBridgeAppBuilder builder)
        {
            builder.AddTransient<LocalFileSystem>();

            return builder;
        }
    }
}
