using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Threading.Tasks;
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

        private void _OnDeleteSelectedClick(object sender, EventArgs e)
        {
            List<CompanyConfig> companyConfigs = new List<CompanyConfig>();

            foreach (DataGridViewRow row in _gridView.SelectedRows)
            {
                companyConfigs.Add((CompanyConfig)row.Tag);
            }

            foreach (CompanyConfig companyConfig in companyConfigs)
            {
                _config.Companies.Remove(companyConfig);
            }

            _SaveConfig();
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
                _SaveConfig();
            }
        }

        private void _OnMainFormLoad(object sender, EventArgs e)
        {
            _RefreshGrid();
            _RefreshStartStopButton();
        }

        private void _OnNewCompanyClick(object sender, EventArgs e)
        {
            CompanyConfigForm form = new CompanyConfigForm();

            form.ShowDialog(this);

            if (form.DialogResult == DialogResult.OK)
            {
                _config.Companies.Add(form.CompanyConfig);
                _SaveConfig();
            }
        }

        private void _OnStartStopClick(object sender, EventArgs e)
        {
            ServiceControllerStatus desiredStatus = ServiceControllerStatus.Stopped;

            if (_serviceController.Status == ServiceControllerStatus.Running)
            {
                desiredStatus = ServiceControllerStatus.Stopped;
            }

            else if (_serviceController.Status == ServiceControllerStatus.Stopped)
            {
                desiredStatus = ServiceControllerStatus.Running;
            }

            Task.Factory.StartNew(() =>
            {
                _SetFormControlsEnabled(false);
                _SetStartStopButtonText("Please Wait...");
                
                if (desiredStatus == ServiceControllerStatus.Running)
                {
                    _serviceController.Start();
                }

                else if (desiredStatus == ServiceControllerStatus.Stopped)
                {
                    _serviceController.Stop();
                }

                _serviceController.WaitForStatus(desiredStatus);
                _RefreshStartStopButton();
                _SetFormControlsEnabled(true);
            });
        }

        private void _RefreshGrid()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(_RefreshGrid));
                return;
            }

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

        private void _RefreshStartStopButton()
        {
            if (_serviceController.Status == ServiceControllerStatus.Running)
            {
                _SetStartStopButtonText("Stop Service");
            }

            else if (_serviceController.Status == ServiceControllerStatus.Stopped)
            {
                _SetStartStopButtonText("Start Service");
            }
        }

        private void _SaveConfig()
        {
            _SetFormControlsEnabled(false);

            Task.Factory.StartNew(() =>
            {
                bool restartService = false;

                if (_serviceController.Status == ServiceControllerStatus.Running)
                {
                    restartService = true;

                    _serviceController.Stop();
                    _serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
                }

                _config.Save();

                if (restartService)
                {
                    _serviceController.Start();
                    _serviceController.WaitForStatus(ServiceControllerStatus.Running);
                }

                _RefreshGrid();
                _SetFormControlsEnabled(true);
            });
        }

        private void _SetFormControlsEnabled(bool enabled)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<bool>(_SetFormControlsEnabled), enabled);
                return;
            }

            foreach (Control control in Controls)
            {
                control.Enabled = enabled;
            }
        }

        private void _SetStartStopButtonText(string text)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string>(_SetStartStopButtonText), text);
                return;
            }

            _startStop.Text = text;
        }
    }
}
