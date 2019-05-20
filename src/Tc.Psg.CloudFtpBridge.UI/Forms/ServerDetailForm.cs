using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tc.Psg.CloudFtpBridge.UI.Forms
{
    public partial class ServerDetailForm : Form
    {
        private readonly IServerConnectionTester _serverConnectionTester;

        public ServerDetailForm(IServerRepository serverRepository, IServerConnectionTester serverConnectionTester)
        {
            ServerRepository = serverRepository;
            _serverConnectionTester = serverConnectionTester;

            InitializeComponent();
        }

        public Guid ServerId { get; set; }
        public IServerRepository ServerRepository { get; private set; }

        public void LoadServer()
        {
            if (ServerId == Guid.Empty)
            {
                return;
            }

            Server server = ServerRepository.Get(ServerId);

            if (server != Server.Empty)
            {
                _hostTextBox.Text = server.Host;
                _serverNameTextBox.Text = server.Name;
                _passwordTextBox.Text = server.Password;
                _pathTextBox.Text = server.Path;
                _portTextBox.Text = server.Port.ToString();
                _usernameTextBox.Text = server.Username;
                _useFtpsCheckbox.Checked = server.FtpsEnabled;
                _encryptionModeComboBox.SelectedItem = server.EncryptionMode;
            }

            _deleteLink.Visible = ServerRepository.CanDelete(ServerId);
            _encryptionModeGroupBox.Enabled = _useFtpsCheckbox.Checked;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            LoadServer();
        }

        public bool SaveServer()
        {
            Server server = new Server
            {
                Id = (ServerId == Guid.Empty) ? Guid.NewGuid() : ServerId,
                Host = _hostTextBox.Text,
                Name = _serverNameTextBox.Text,
                Password = _passwordTextBox.Text,
                Path = _pathTextBox.Text,
                Port = int.Parse(_portTextBox.Text),
                Username = _usernameTextBox.Text,
                EncryptionMode =  _encryptionModeComboBox.SelectedItem?.ToString(),
                FtpsEnabled = _useFtpsCheckbox.Checked
            };

            try
            {
                Server.Validate(server);
            }

            catch (InvalidOperationException ex)
            {
                MessageBox.Show(this, ex.Message, "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }

            ServerRepository.Save(server);

            return true;
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void _deleteLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this server?", "Confirm Server Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                ServerRepository.Delete(ServerId);
                Close();
            }
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            if (SaveServer())
            {
                Close();
            }
        }

        private void _testConnectionBtn_Click(object sender, EventArgs e)
        {
            TestConnectionToServer();
        }

        public void TestConnectionToServer()
        {
            Server server = new Server
            {
                Id = (ServerId == Guid.Empty) ? Guid.NewGuid() : ServerId,
                Host = _hostTextBox.Text,
                Name = _serverNameTextBox.Text,
                Password = _passwordTextBox.Text,
                Path = _pathTextBox.Text,
                Port = int.Parse(_portTextBox.Text),
                Username = _usernameTextBox.Text,
                EncryptionMode = _encryptionModeComboBox.SelectedItem?.ToString(),
                FtpsEnabled = _useFtpsCheckbox.Checked
            };

            _SetControlText(_testConnectionBtn, "Testing...");
            _SetFormState(false);

            Task.Run(async () =>
            {
                try
                {
                    await _serverConnectionTester.TestConnection(server);

                    _ShowMessageBox("Connection test was successful!", "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                catch (InvalidOperationException ex)
                {
                    _ShowMessageBox(ex.Message, "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                catch (Exception otherEx)
                {
                    _ShowMessageBox(otherEx.Message, "Connection Test Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                finally
                {
                    _SetControlText(_testConnectionBtn, "Test Connection");
                    _SetFormState(true);
                }
            });
            
        }

        private void _useFtpsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            _encryptionModeGroupBox.Enabled = _useFtpsCheckbox.Checked;
        }

        private void _SetControlText(Control control, string text)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(new Action<Control, string>(_SetControlText), control, text);

                return;
            }

            control.Text = text;
        }

        private void _SetFormState(bool enabled)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<bool>(_SetFormState), enabled);

                return;
            }

            Enabled = enabled;
        }

        private void _ShowMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string, string, MessageBoxButtons, MessageBoxIcon>(_ShowMessageBox), text, caption, buttons, icon);

                return;
            }

            MessageBox.Show(this, text, caption, buttons, icon);
        }
    }
}
