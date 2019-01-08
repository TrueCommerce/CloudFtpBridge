using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Tc.Psg.CloudFtpBridge.UI.Forms;
using Tc.Psg.CloudFtpBridge.UI.DependencyInjection;

namespace Tc.Psg.CloudFtpBridge.UI.Controls
{
    public partial class ServerGridControl : UserControl
    {
        public ServerGridControl(IFormFactory formFactory, IServerRepository serverRepository)
        {
            FormFactory = formFactory;
            ServerRepository = serverRepository;

            InitializeComponent();
            RefreshServers();
        }

        public IFormFactory FormFactory { get; private set; }
        public IServerRepository ServerRepository { get; private set; }

        public void RefreshServers()
        {
            IEnumerable<Server> servers = ServerRepository.GetAll();

            _grid.Rows.Clear();

            foreach (Server server in servers)
            {
                int index = _grid.Rows.Add(server.Name, server.Host, server.Port.ToString(), server.Path, server.Username);

                _grid.Rows[index].Tag = server.Id;
            }
        }

        private void _addServerButton_Click(object sender, EventArgs e)
        {
            ServerDetailForm serverDetailForm = FormFactory.CreateForm<ServerDetailForm>(ParentForm);

            serverDetailForm.ShowDialog();

            RefreshServers();
        }

        private void _grid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Guid serverId = (Guid)_grid.Rows[e.RowIndex].Tag;

                ServerDetailForm serverDetailForm = FormFactory.CreateForm<ServerDetailForm>(ParentForm);

                serverDetailForm.ServerId = serverId;
                serverDetailForm.ShowDialog();

                RefreshServers();
            } 
            catch (Exception ex)
            { System.Diagnostics.Trace.TraceInformation("_grid_CellMouseDoubleClick -  " + ex.Message); }
        }
    }
}
