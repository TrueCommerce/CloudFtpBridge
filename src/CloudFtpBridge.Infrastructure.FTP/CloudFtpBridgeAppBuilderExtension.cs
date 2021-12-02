using CloudFtpBridge.Infrastructure.FTP;

namespace Microsoft.Extensions.DependencyInjection
{
        public static class CloudFtpBridgeAppBuilderExtensions
        {
            public static CloudFtpBridgeAppBuilder UseFTPFileSystem(this CloudFtpBridgeAppBuilder builder)
            {
                builder.AddTransient<FTPFileSystem>();
                return builder;
            }
        }
 }
