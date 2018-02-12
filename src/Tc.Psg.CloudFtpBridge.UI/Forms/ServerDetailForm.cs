using System;
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
            }

            _deleteLink.Visible = ServerRepository.CanDelete(ServerId);
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
                Username = _usernameTextBox.Text
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
    }
}
