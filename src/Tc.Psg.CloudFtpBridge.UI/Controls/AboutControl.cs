using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace Tc.Psg.CloudFtpBridge.UI.Controls
{
    public partial class AboutControl : UserControl
    {
        public AboutControl()
        {
            InitializeComponent();

            _appVersionLabel.Text = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
        }

        private void _licenseLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/TrueCommerce/CloudFtpBridge/blob/master/LICENSE");
        }
    }
}
