using CloudFtpBridge.Core.Services;
using CloudFtpBridge.Infrastructure.Smtp;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CloudFtpAppBuilderExtensions
    {
        public static CloudFtpBridgeAppBuilder UseSmtpMailSender(this CloudFtpBridgeAppBuilder builder)
        {
            builder.AddTransient<IMailSender, SmtpMailSender>();

            return builder;
        }
    }
}
