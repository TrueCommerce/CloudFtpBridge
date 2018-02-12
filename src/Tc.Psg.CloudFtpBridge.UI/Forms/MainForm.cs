using Microsoft.Extensions.Logging;
using System.Windows.Forms;

using Tc.Psg.CloudFtpBridge.UI.Controls;
using Tc.Psg.CloudFtpBridge.UI.DependencyInjection;

namespace Tc.Psg.CloudFtpBridge.UI.Forms
{
    public partial class MainForm : Form
    {
        public MainForm(IControlFactory controlFactory, ILogger<MainForm> logger)
        {
            logger.LogInformation("Launched management UI.");

            InitializeComponent();

            _aboutTabPage.Controls.Add(controlFactory.CreateDockedControl<AboutControl>());
            _serversTabPage.Controls.Add(controlFactory.CreateDockedControl<ServerGridControl>());
            _workflowsTabPage.Controls.Add(controlFactory.CreateDockedControl<WorkflowGridControl>());
            _alertsTabPage.Controls.Add(controlFactory.CreateDockedControl<EmailConfigControl>());
        }
    }
}
