namespace Tc.Psg.CloudFtpBridge.ConfigManager.Forms
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._gridView = new System.Windows.Forms.DataGridView();
            this.Enabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CompanyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._newCompany = new System.Windows.Forms.Button();
            this._done = new System.Windows.Forms.Button();
            this._serviceController = new System.ServiceProcess.ServiceController();
            this._deleteSelected = new System.Windows.Forms.Button();
            this._startStop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // _gridView
            // 
            this._gridView.AllowUserToAddRows = false;
            this._gridView.AllowUserToDeleteRows = false;
            this._gridView.AllowUserToResizeRows = false;
            this._gridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._gridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._gridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Enabled,
            this.CompanyName,
            this.DateCreated,
            this.DateModified});
            this._gridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this._gridView.Location = new System.Drawing.Point(0, 0);
            this._gridView.MultiSelect = false;
            this._gridView.Name = "_gridView";
            this._gridView.ReadOnly = true;
            this._gridView.RowHeadersVisible = false;
            this._gridView.RowTemplate.Height = 24;
            this._gridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._gridView.Size = new System.Drawing.Size(727, 445);
            this._gridView.TabIndex = 0;
            this._gridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._OnGridViewCellDoubleClick);
            // 
            // Enabled
            // 
            this.Enabled.Frozen = true;
            this.Enabled.HeaderText = "Enabled";
            this.Enabled.Name = "Enabled";
            this.Enabled.ReadOnly = true;
            this.Enabled.Width = 75;
            // 
            // CompanyName
            // 
            this.CompanyName.Frozen = true;
            this.CompanyName.HeaderText = "Name";
            this.CompanyName.Name = "CompanyName";
            this.CompanyName.ReadOnly = true;
            this.CompanyName.Width = 300;
            // 
            // DateCreated
            // 
            this.DateCreated.HeaderText = "Date Created";
            this.DateCreated.Name = "DateCreated";
            this.DateCreated.ReadOnly = true;
            this.DateCreated.Width = 175;
            // 
            // DateModified
            // 
            this.DateModified.HeaderText = "Date Modified";
            this.DateModified.Name = "DateModified";
            this.DateModified.ReadOnly = true;
            this.DateModified.Width = 175;
            // 
            // _newCompany
            // 
            this._newCompany.Location = new System.Drawing.Point(12, 451);
            this._newCompany.Name = "_newCompany";
            this._newCompany.Size = new System.Drawing.Size(140, 30);
            this._newCompany.TabIndex = 1;
            this._newCompany.Text = "New Company...";
            this._newCompany.UseVisualStyleBackColor = true;
            this._newCompany.Click += new System.EventHandler(this._OnNewCompanyClick);
            // 
            // _done
            // 
            this._done.Location = new System.Drawing.Point(575, 451);
            this._done.Name = "_done";
            this._done.Size = new System.Drawing.Size(140, 30);
            this._done.TabIndex = 2;
            this._done.Text = "Done";
            this._done.UseVisualStyleBackColor = true;
            this._done.Click += new System.EventHandler(this._OnDoneClick);
            // 
            // _serviceController
            // 
            this._serviceController.ServiceName = "TcCloudFtpBridge";
            // 
            // _deleteSelected
            // 
            this._deleteSelected.Location = new System.Drawing.Point(158, 451);
            this._deleteSelected.Name = "_deleteSelected";
            this._deleteSelected.Size = new System.Drawing.Size(140, 30);
            this._deleteSelected.TabIndex = 4;
            this._deleteSelected.Text = "Delete Selected";
            this._deleteSelected.UseVisualStyleBackColor = true;
            this._deleteSelected.Click += new System.EventHandler(this._OnDeleteSelectedClick);
            // 
            // _startStop
            // 
            this._startStop.Location = new System.Drawing.Point(304, 451);
            this._startStop.Name = "_startStop";
            this._startStop.Size = new System.Drawing.Size(140, 30);
            this._startStop.TabIndex = 5;
            this._startStop.Text = "Start Service";
            this._startStop.UseVisualStyleBackColor = true;
            this._startStop.Click += new System.EventHandler(this._OnStartStopClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 493);
            this.Controls.Add(this._startStop);
            this.Controls.Add(this._deleteSelected);
            this.Controls.Add(this._done);
            this.Controls.Add(this._newCompany);
            this.Controls.Add(this._gridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cloud FTP Bridge Configuration";
            this.Load += new System.EventHandler(this._OnMainFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this._gridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView _gridView;
        private System.Windows.Forms.Button _newCompany;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Enabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompanyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateCreated;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateModified;
        private System.Windows.Forms.Button _done;
        private System.ServiceProcess.ServiceController _serviceController;
        private System.Windows.Forms.Button _deleteSelected;
        private System.Windows.Forms.Button _startStop;
    }
}