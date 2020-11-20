using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Tc.Psg.CloudFtpBridge.UI.Forms
{
    public partial class WorkflowDetailForm : Form
    {
        public WorkflowDetailForm(IServerRepository serverRepository, IWorkflowRepository workflowRepository)
        {
            ServerRepository = serverRepository;
            WorkflowRepository = workflowRepository;

            InitializeComponent();

            _directionComboBox.Items.AddRange(new string[] { "Inbound", "InboundOptimized", "Outbound", "OutboundOptimized", "InboundOptimizedNoArchive", "OutboundOptimizedNoArchive" });
            _serverComboBox.DisplayMember = "Name";

            LoadServers();
        }

        public Guid WorkflowId { get; set; }
        public IServerRepository ServerRepository { get; private set; }
        public IWorkflowRepository WorkflowRepository { get; private set; }

        public void LoadServers()
        {
            IEnumerable<Server> servers = ServerRepository.GetAll();

            _serverComboBox.Items.Clear();

            foreach (Server server in servers)
            {
                _serverComboBox.Items.Add(server);
            }
        }

        public void LoadWorkflow()
        {
            if (WorkflowId == Guid.Empty)
            {
                return;
            }

            Workflow workflow = WorkflowRepository.Get(WorkflowId);

            if (workflow != Workflow.Empty)
            {
                _directionComboBox.SelectedItem = workflow.Direction.ToString();
                _fileNameFilterTextBox.Text = workflow.FileFilter;
                _localPathTextBox.Text = workflow.LocalPath;
                _nameTextBox.Text = workflow.Name;
                _remotePathTextBox.Text = workflow.RemotePath;
                _serverComboBox.SelectedItem = _serverComboBox.Items.Cast<Server>().First(x => x.Id.Equals(workflow.Server.Id));
                _autoRetryFailedCheckbox.Checked = workflow.AutoRetryFailed;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            LoadWorkflow();
        }

        public bool SaveWorkflow()
        {
            Workflow workflow = new Workflow
            {
                Direction = (WorkflowDirection)Enum.Parse(typeof(WorkflowDirection),_directionComboBox.SelectedItem?.ToString()),
                FileFilter = _fileNameFilterTextBox.Text,
                Id = (WorkflowId == Guid.Empty) ? Guid.NewGuid() : WorkflowId,
                LocalPath = _localPathTextBox.Text,
                Name = _nameTextBox.Text,
                RemotePath = _remotePathTextBox.Text,
                Server = (Server)_serverComboBox.SelectedItem,
                AutoRetryFailed = _autoRetryFailedCheckbox.Checked
            };

            try
            {
                Workflow.Validate(workflow);
            }

            catch (InvalidOperationException ex)
            {
                MessageBox.Show(this, ex.Message, "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }

            WorkflowRepository.Save(workflow);

            return true;
        }

        private void _browseLocalButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Select Local Path";
            dialog.RootFolder = Environment.SpecialFolder.MyComputer;
            dialog.ShowNewFolderButton = true;

            dialog.ShowDialog();

            if (!string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                _localPathTextBox.Text = dialog.SelectedPath;
            }
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void _deleteLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this workflow?", "Confirm Workflow Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                WorkflowRepository.Delete(WorkflowId);
                Close();
            }
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            if (SaveWorkflow())
            {
                Close();
            }
        }
    }
}
