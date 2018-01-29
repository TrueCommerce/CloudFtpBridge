namespace Tc.Psg.CloudFtpBridge.UI.Controls
{
    partial class ServerGridControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._addServerButton = new System.Windows.Forms.Button();
            this._grid = new System.Windows.Forms.DataGridView();
            this._serverNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._serverHostColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._serverPortColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._serverPathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._serverUsernameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this.SuspendLayout();
            // 
            // _addServerButton
            // 
            this._addServerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._addServerButton.Location = new System.Drawing.Point(3, 456);
            this._addServerButton.Name = "_addServerButton";
            this._addServerButton.Size = new System.Drawing.Size(130, 23);
            this._addServerButton.TabIndex = 3;
            this._addServerButton.Text = "Add Server";
            this._addServerButton.UseVisualStyleBackColor = true;
            this._addServerButton.Click += new System.EventHandler(this._addServerButton_Click);
            // 
            // _grid
            // 
            this._grid.AllowUserToAddRows = false;
            this._grid.AllowUserToDeleteRows = false;
            this._grid.AllowUserToOrderColumns = true;
            this._grid.AllowUserToResizeRows = false;
            this._grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._grid.BackgroundColor = System.Drawing.SystemColors.Control;
            this._grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._serverNameColumn,
            this._serverHostColumn,
            this._serverPortColumn,
            this._serverPathColumn,
            this._serverUsernameColumn});
            this._grid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this._grid.Location = new System.Drawing.Point(3, 3);
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(958, 447);
            this._grid.TabIndex = 2;
            this._grid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this._grid_CellMouseDoubleClick);
            // 
            // _serverNameColumn
            // 
            this._serverNameColumn.Frozen = true;
            this._serverNameColumn.HeaderText = "Name";
            this._serverNameColumn.MinimumWidth = 200;
            this._serverNameColumn.Name = "_serverNameColumn";
            this._serverNameColumn.ReadOnly = true;
            this._serverNameColumn.Width = 200;
            // 
            // _serverHostColumn
            // 
            this._serverHostColumn.HeaderText = "Host";
            this._serverHostColumn.MinimumWidth = 200;
            this._serverHostColumn.Name = "_serverHostColumn";
            this._serverHostColumn.ReadOnly = true;
            this._serverHostColumn.Width = 200;
            // 
            // _serverPortColumn
            // 
            this._serverPortColumn.HeaderText = "Port";
            this._serverPortColumn.Name = "_serverPortColumn";
            this._serverPortColumn.ReadOnly = true;
            this._serverPortColumn.Width = 50;
            // 
            // _serverPathColumn
            // 
            this._serverPathColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._serverPathColumn.HeaderText = "Path";
            this._serverPathColumn.MinimumWidth = 250;
            this._serverPathColumn.Name = "_serverPathColumn";
            this._serverPathColumn.ReadOnly = true;
            // 
            // _serverUsernameColumn
            // 
            this._serverUsernameColumn.HeaderText = "Username";
            this._serverUsernameColumn.MinimumWidth = 150;
            this._serverUsernameColumn.Name = "_serverUsernameColumn";
            this._serverUsernameColumn.ReadOnly = true;
            this._serverUsernameColumn.Width = 150;
            // 
            // ServerGridControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._addServerButton);
            this.Controls.Add(this._grid);
            this.Name = "ServerGridControl";
            this.Size = new System.Drawing.Size(965, 482);
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _addServerButton;
        private System.Windows.Forms.DataGridView _grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn _serverNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _serverHostColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _serverPortColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _serverPathColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _serverUsernameColumn;
    }
}
