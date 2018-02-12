using Tc.Psg.CloudFtpBridge.UI.Controls;
using Tc.Psg.CloudFtpBridge.UI.DependencyInjection;
using Tc.Psg.CloudFtpBridge.UI.Forms;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCloudFtpBridgeUI(this IServiceCollection services)
        {
            return services
                .AddTransient<IControlFactory, ControlFactory>()
                .AddTransient<IFormFactory, FormFactory>()
                .AddTransient<AboutControl>()
                .AddTransient<EmailConfigControl>()
                .AddTransient<ServerGridControl>()
                .AddTransient<WorkflowGridControl>()
                .AddTransient<MainForm>()
                .AddTransient<ServerDetailForm>()
                .AddTransient<WorkflowDetailForm>();
        }
    }
}
