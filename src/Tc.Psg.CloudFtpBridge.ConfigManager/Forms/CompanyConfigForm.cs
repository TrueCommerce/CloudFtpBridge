using System;
using System.Windows.Forms;

using Tc.Psg.CloudFtpBridge.Configuration;

namespace Tc.Psg.CloudFtpBridge.ConfigManager.Forms
{
    public partial class CompanyConfigForm : Form
    {
        public CompanyConfigForm()
        {
            InitializeComponent();

            CompanyConfig = new CompanyConfig();
        }

        public CompanyConfigForm(CompanyConfig companyConfig)
            : this()
        {
            CompanyConfig = companyConfig;
        }

        public CompanyConfig CompanyConfig { get; private set; }

        private void _OnBrowseInboundPathClick(object sender, EventArgs e)
        {
            _ShowFolderDialogForTextBox(_inboundPath);
        }

        private void _OnBrowseOutboundPathClick(object sender, EventArgs e)
        {
            _ShowFolderDialogForTextBox(_outboundPath);
        }

        private void _OnCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }

        private void _OnCompanyConfigFormLoad(object sender, EventArgs e)
        {
            _companyName.Text = CompanyConfig.Name;
            _ediId.Text = CompanyConfig.EdiId;
            _inboundPath.Text = CompanyConfig.LocalInboundPath;
            _outboundPath.Text = CompanyConfig.LocalOutboundPath;
            _ftpUsername.Text = CompanyConfig.CloudFtpUser;
            _ftpPassword.Text = CompanyConfig.CloudFtpPassword;
            _enabled.Checked = CompanyConfig.Enabled;
        }

        private void _OnSaveClick(object sender, EventArgs e)
        {
            CompanyConfig.Name = _companyName.Text;
            CompanyConfig.EdiId = _ediId.Text;
            CompanyConfig.LocalInboundPath = _inboundPath.Text;
            CompanyConfig.LocalOutboundPath = _outboundPath.Text;
            CompanyConfig.CloudFtpUser = _ftpUsername.Text;
            CompanyConfig.CloudFtpPassword = _ftpPassword.Text;
            CompanyConfig.Enabled = _enabled.Checked;

            CompanyConfig.DateModified = DateTime.Now;

            DialogResult = DialogResult.OK;

            Close();
        }

        private void _ShowFolderDialogForTextBox(TextBox textBox)
        {
            _folderBrowserDialog.SelectedPath = textBox.Text;

            _folderBrowserDialog.ShowDialog(this);

            if (!string.IsNullOrEmpty(_folderBrowserDialog.SelectedPath))
            {
                textBox.Text = _folderBrowserDialog.SelectedPath;
            }
        }
    }
}
