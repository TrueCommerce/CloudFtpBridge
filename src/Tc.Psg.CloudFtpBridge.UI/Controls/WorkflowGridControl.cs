using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Tc.Psg.CloudFtpBridge.UI.DependencyInjection;
using Tc.Psg.CloudFtpBridge.UI.Forms;

namespace Tc.Psg.CloudFtpBridge.UI.Controls
{
    public partial class WorkflowGridControl : UserControl
    {
        public WorkflowGridControl(IFormFactory formFactory, IServerRepository serverRepository, IWorkflowRepository workflowRepository)
        {
            FormFactory = formFactory;
            ServerRepository = serverRepository;
            WorkflowRepository = workflowRepository;

            InitializeComponent();
            RefreshWorkflows();
        }

        public IFormFactory FormFactory { get; private set; }
        public IServerRepository ServerRepository { get; private set; }
        public IWorkflowRepository WorkflowRepository { get; private set; }

        public void RefreshWorkflows()
        {
            IEnumerable<Workflow> workflows = WorkflowRepository.GetAll();

            _grid.Rows.Clear();

            foreach (Workflow workflow in workflows)
            {
                int index = _grid.Rows.Add(workflow.Name, workflow.Direction.ToString(), workflow.Server.Name, workflow.RemotePath, workflow.LocalPath);

                _grid.Rows[index].Tag = workflow.Id;
            }
        }

        private void _addWorkflowButton_Click(object sender, EventArgs e)
        {
            WorkflowDetailForm workflowDetailForm = FormFactory.CreateForm<WorkflowDetailForm>(ParentForm);

            workflowDetailForm.ShowDialog();

            RefreshWorkflows();
        }

        private void _grid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Guid workflowId = (Guid)_grid.Rows[e.RowIndex].Tag;

                WorkflowDetailForm workflowDetailForm = FormFactory.CreateForm<WorkflowDetailForm>(ParentForm);

                workflowDetailForm.WorkflowId = workflowId;
                workflowDetailForm.ShowDialog();

                RefreshWorkflows();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceInformation("Exception in _grid_CellMouseDoubleClick (WorkflowGridControl): " + ex.Message);
            }
        }
    }
}
