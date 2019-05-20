using FluentFTP;
using System;
using System.Security.Authentication;
using System.Windows.Forms;

namespace Tc.Psg.CloudFtpBridge.UI.Forms
{
    public partial class ServerDetailForm : Form
    {
        public ServerDetailForm(IServerRepository serverRepository)
        {
            ServerRepository = serverRepository;

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
            string encryptMode = "";

            if (implicitChk.Checked)
                encryptMode = "Implicit";
            else if (explicitChk.Checked)
                encryptMode = "Explicit";
            else
                encryptMode = "None";

            Server server = new Server
            {
                Id = (ServerId == Guid.Empty) ? Guid.NewGuid() : ServerId,
                Host = _hostTextBox.Text,
                Name = _serverNameTextBox.Text,
                Password = _passwordTextBox.Text,
                Path = _pathTextBox.Text,
                Port = int.Parse(_portTextBox.Text),
                Username = _usernameTextBox.Text,
                EncryptionMode =  encryptMode,
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
            string encryptMode = "None";

            if (implicitChk.Checked)
                encryptMode = "Implicit";
            else if (explicitChk.Checked)
                encryptMode = "Explicit";

            Server server = new Server
            {
                Id = (ServerId == Guid.Empty) ? Guid.NewGuid() : ServerId,
                Host = _hostTextBox.Text,
                Name = _serverNameTextBox.Text,
                Password = _passwordTextBox.Text,
                Path = _pathTextBox.Text,
                Port = int.Parse(_portTextBox.Text),
                Username = _usernameTextBox.Text,
                EncryptionMode = encryptMode,
                FtpsEnabled = _useFtpsCheckbox.Checked
            };

            try
            {
                Server.Validate(server);
                FtpClient ftpClient = new FtpClient(server.Host, server.Port, server.Username, server.Password);

                if (server.FtpsEnabled)
                {
                    if (!string.IsNullOrEmpty(server.EncryptionMode))
                    {
                        if (server.EncryptionMode == "Explicit")
                        {
                            ftpClient.EncryptionMode = FtpEncryptionMode.Explicit;
                        }
                        else if (server.EncryptionMode == "Implicit")
                        {
                            ftpClient.EncryptionMode = FtpEncryptionMode.Implicit;
                        }
                        else
                            ftpClient.EncryptionMode = FtpEncryptionMode.None;
                    }

                    ftpClient.ValidateCertificate += new FtpSslValidation(OnValidateCertificate);
                    ftpClient.SslProtocols = SslProtocols.Default;
                }

                ftpClient.Connect();
                MessageBox.Show(this, "Connection test was successful!", "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(this, ex.Message, "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception otherEx)
            {
                MessageBox.Show(this, otherEx.Message, "Connection Test Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OnValidateCertificate(FtpClient control, FtpSslValidationEventArgs e)
        {
            e.Accept = true;
        }

        private void _useFtpsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            _encryptionModeGroupBox.Enabled = _useFtpsCheckbox.Checked;
        }
    }
}
