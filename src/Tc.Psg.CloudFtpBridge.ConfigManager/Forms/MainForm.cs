using System;
using System.Windows.Forms;

using Tc.Psg.CloudFtpBridge.Configuration;

namespace Tc.Psg.CloudFtpBridge.ConfigManager.Forms
{
    public partial class MainForm : Form
    {
        private CloudFtpBridgeConfig _config;

        public MainForm(CloudFtpBridgeConfig config)
        {
            InitializeComponent();

            _config = config;
        }

        private void _OnDoneClick(object sender, EventArgs e)
        {
            Close();
        }

        private void _OnGridViewCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CompanyConfig companyConfig = (CompanyConfig)_gridView.Rows[e.RowIndex].Tag;
            CompanyConfigForm form = new CompanyConfigForm(companyConfig);

            form.ShowDialog(this);

            if (form.DialogResult == DialogResult.OK)
            {
                _config.Save();
            }
        }

        private void _OnMainFormLoad(object sender, EventArgs e)
        {
            _RefreshGrid();
        }

        private void _OnNewCompanyClick(object sender, EventArgs e)
        {
            CompanyConfigForm form = new CompanyConfigForm();

            form.ShowDialog(this);

            if (form.DialogResult == DialogResult.OK)
            {
                _config.Companies.Add(form.CompanyConfig);
                _config.Save();
            }

            _RefreshGrid();
        }

        private void _RefreshGrid()
        {
            _gridView.Rows.Clear();

            foreach (CompanyConfig companyConfig in _config.Companies)
            {
                _gridView.Rows.Add(
                    companyConfig.Enabled,
                    $"{companyConfig.Name} ({companyConfig.EdiId})",
                    companyConfig.DateCreated.ToString(),
                    companyConfig.DateModified.ToString()
                    );

                _gridView.Rows[_gridView.Rows.Count - 1].Tag = companyConfig;
            }
        }
    }
}
