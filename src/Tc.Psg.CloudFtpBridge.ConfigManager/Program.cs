using System;
using System.Windows.Forms;

using Tc.Psg.CloudFtpBridge.Configuration;
using Tc.Psg.CloudFtpBridge.ConfigManager.Forms;

namespace Tc.Psg.CloudFtpBridge.ConfigManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CloudFtpBridgeConfig config = CloudFtpBridgeConfig.Load();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(config));
        }
    }
}
