namespace Tc.Psg.CloudFtpBridge.UI.Controls
{
    partial class WorkflowGridControl
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
            this._addWorkflowButton = new System.Windows.Forms.Button();
            this._grid = new System.Windows.Forms.DataGridView();
            this._workflowNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._workflowDirectionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._workflowServerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._workflowRemotePathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._workflowLocalPathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this.SuspendLayout();
            // 
            // _addWorkflowButton
            // 
            this._addWorkflowButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._addWorkflowButton.Location = new System.Drawing.Point(3, 456);
            this._addWorkflowButton.Name = "_addWorkflowButton";
            this._addWorkflowButton.Size = new System.Drawing.Size(130, 23);
            this._addWorkflowButton.TabIndex = 4;
            this._addWorkflowButton.Text = "Add Workflow";
            this._addWorkflowButton.UseVisualStyleBackColor = true;
            this._addWorkflowButton.Click += new System.EventHandler(this._addWorkflowButton_Click);
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
            this._workflowNameColumn,
            this._workflowDirectionColumn,
            this._workflowServerColumn,
            this._workflowRemotePathColumn,
            this._workflowLocalPathColumn});
            this._grid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this._grid.Location = new System.Drawing.Point(3, 3);
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(959, 447);
            this._grid.TabIndex = 3;
            this._grid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this._grid_CellMouseDoubleClick);
            // 
            // _workflowNameColumn
            // 
            this._workflowNameColumn.Frozen = true;
            this._workflowNameColumn.HeaderText = "Name";
            this._workflowNameColumn.MinimumWidth = 200;
            this._workflowNameColumn.Name = "_workflowNameColumn";
            this._workflowNameColumn.ReadOnly = true;
            this._workflowNameColumn.Width = 200;
            // 
            // _workflowDirectionColumn
            // 
            this._workflowDirectionColumn.HeaderText = "Direction";
            this._workflowDirectionColumn.MinimumWidth = 100;
            this._workflowDirectionColumn.Name = "_workflowDirectionColumn";
            this._workflowDirectionColumn.ReadOnly = true;
            // 
            // _workflowServerColumn
            // 
            this._workflowServerColumn.HeaderText = "Server";
            this._workflowServerColumn.MinimumWidth = 150;
            this._workflowServerColumn.Name = "_workflowServerColumn";
            this._workflowServerColumn.ReadOnly = true;
            this._workflowServerColumn.Width = 150;
            // 
            // _workflowRemotePathColumn
            // 
            this._workflowRemotePathColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._workflowRemotePathColumn.HeaderText = "Remote Path";
            this._workflowRemotePathColumn.MinimumWidth = 250;
            this._workflowRemotePathColumn.Name = "_workflowRemotePathColumn";
            this._workflowRemotePathColumn.ReadOnly = true;
            // 
            // _workflowLocalPathColumn
            // 
            this._workflowLocalPathColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._workflowLocalPathColumn.HeaderText = "Local Path";
            this._workflowLocalPathColumn.MinimumWidth = 250;
            this._workflowLocalPathColumn.Name = "_workflowLocalPathColumn";
            this._workflowLocalPathColumn.ReadOnly = true;
            // 
            // WorkflowGridControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._addWorkflowButton);
            this.Controls.Add(this._grid);
            this.Name = "WorkflowGridControl";
            this.Size = new System.Drawing.Size(965, 482);
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _addWorkflowButton;
        private System.Windows.Forms.DataGridView _grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn _workflowNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _workflowDirectionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _workflowServerColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _workflowRemotePathColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _workflowLocalPathColumn;
    }
}
