using Tc.Psg.CloudFtpBridge.Service;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCloudFtpBridgeService(this IServiceCollection services)
        {
            return services.AddTransient<PollingService>();
        }
    }
}
