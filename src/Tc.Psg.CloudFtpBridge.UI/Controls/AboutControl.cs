using System.Windows.Forms;
using System.Diagnostics;

namespace Tc.Psg.CloudFtpBridge.UI.Controls
{
    public partial class AboutControl : UserControl
    {
        public AboutControl()
        {
            InitializeComponent();
        }

        private void _licenseLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/TrueCommerce/CloudFtpBridge/blob/master/LICENSE");
        }
    }
}
