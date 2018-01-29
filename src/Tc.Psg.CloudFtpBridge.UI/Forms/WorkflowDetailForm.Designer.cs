namespace Tc.Psg.CloudFtpBridge.UI.Forms
{
    partial class WorkflowDetailForm
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
            this._nameLabel = new System.Windows.Forms.Label();
            this._nameTextBox = new System.Windows.Forms.TextBox();
            this._directionLabel = new System.Windows.Forms.Label();
            this._directionComboBox = new System.Windows.Forms.ComboBox();
            this._serverLabel = new System.Windows.Forms.Label();
            this._serverComboBox = new System.Windows.Forms.ComboBox();
            this._remotePathLabel = new System.Windows.Forms.Label();
            this._remotePathTextBox = new System.Windows.Forms.TextBox();
            this._localPathLabel = new System.Windows.Forms.Label();
            this._localPathTextBox = new System.Windows.Forms.TextBox();
            this._fileNameFilterLabel = new System.Windows.Forms.Label();
            this._fileNameFilterTextBox = new System.Windows.Forms.TextBox();
            this._saveButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._browseLocalButton = new System.Windows.Forms.Button();
            this._deleteLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // _nameLabel
            // 
            this._nameLabel.AutoSize = true;
            this._nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._nameLabel.Location = new System.Drawing.Point(12, 9);
            this._nameLabel.Name = "_nameLabel";
            this._nameLabel.Size = new System.Drawing.Size(96, 13);
            this._nameLabel.TabIndex = 0;
            this._nameLabel.Text = "Workflow Name";
            // 
            // _nameTextBox
            // 
            this._nameTextBox.Location = new System.Drawing.Point(15, 25);
            this._nameTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this._nameTextBox.Name = "_nameTextBox";
            this._nameTextBox.Size = new System.Drawing.Size(457, 20);
            this._nameTextBox.TabIndex = 1;
            // 
            // _directionLabel
            // 
            this._directionLabel.AutoSize = true;
            this._directionLabel.Location = new System.Drawing.Point(12, 55);
            this._directionLabel.Name = "_directionLabel";
            this._directionLabel.Size = new System.Drawing.Size(49, 13);
            this._directionLabel.TabIndex = 2;
            this._directionLabel.Text = "Direction";
            // 
            // _directionComboBox
            // 
            this._directionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._directionComboBox.FormattingEnabled = true;
            this._directionComboBox.Location = new System.Drawing.Point(15, 71);
            this._directionComboBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this._directionComboBox.Name = "_directionComboBox";
            this._directionComboBox.Size = new System.Drawing.Size(165, 21);
            this._directionComboBox.TabIndex = 3;
            // 
            // _serverLabel
            // 
            this._serverLabel.AutoSize = true;
            this._serverLabel.Location = new System.Drawing.Point(183, 55);
            this._serverLabel.Name = "_serverLabel";
            this._serverLabel.Size = new System.Drawing.Size(38, 13);
            this._serverLabel.TabIndex = 4;
            this._serverLabel.Text = "Server";
            // 
            // _serverComboBox
            // 
            this._serverComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._serverComboBox.FormattingEnabled = true;
            this._serverComboBox.Location = new System.Drawing.Point(186, 71);
            this._serverComboBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this._serverComboBox.Name = "_serverComboBox";
            this._serverComboBox.Size = new System.Drawing.Size(286, 21);
            this._serverComboBox.TabIndex = 5;
            // 
            // _remotePathLabel
            // 
            this._remotePathLabel.AutoSize = true;
            this._remotePathLabel.Location = new System.Drawing.Point(12, 102);
            this._remotePathLabel.Name = "_remotePathLabel";
            this._remotePathLabel.Size = new System.Drawing.Size(69, 13);
            this._remotePathLabel.TabIndex = 6;
            this._remotePathLabel.Text = "Remote Path";
            // 
            // _remotePathTextBox
            // 
            this._remotePathTextBox.Location = new System.Drawing.Point(15, 118);
            this._remotePathTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this._remotePathTextBox.Name = "_remotePathTextBox";
            this._remotePathTextBox.Size = new System.Drawing.Size(457, 20);
            this._remotePathTextBox.TabIndex = 7;
            // 
            // _localPathLabel
            // 
            this._localPathLabel.AutoSize = true;
            this._localPathLabel.Location = new System.Drawing.Point(12, 148);
            this._localPathLabel.Name = "_localPathLabel";
            this._localPathLabel.Size = new System.Drawing.Size(58, 13);
            this._localPathLabel.TabIndex = 8;
            this._localPathLabel.Text = "Local Path";
            // 
            // _localPathTextBox
            // 
            this._localPathTextBox.Location = new System.Drawing.Point(15, 164);
            this._localPathTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this._localPathTextBox.Name = "_localPathTextBox";
            this._localPathTextBox.Size = new System.Drawing.Size(426, 20);
            this._localPathTextBox.TabIndex = 9;
            // 
            // _fileNameFilterLabel
            // 
            this._fileNameFilterLabel.AutoSize = true;
            this._fileNameFilterLabel.Location = new System.Drawing.Point(12, 194);
            this._fileNameFilterLabel.Name = "_fileNameFilterLabel";
            this._fileNameFilterLabel.Size = new System.Drawing.Size(179, 13);
            this._fileNameFilterLabel.TabIndex = 10;
            this._fileNameFilterLabel.Text = "File Name Filter (Regular Expression)";
            // 
            // _fileNameFilterTextBox
            // 
            this._fileNameFilterTextBox.Location = new System.Drawing.Point(15, 210);
            this._fileNameFilterTextBox.Name = "_fileNameFilterTextBox";
            this._fileNameFilterTextBox.Size = new System.Drawing.Size(457, 20);
            this._fileNameFilterTextBox.TabIndex = 11;
            // 
            // _saveButton
            // 
            this._saveButton.Location = new System.Drawing.Point(271, 236);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(120, 23);
            this._saveButton.TabIndex = 12;
            this._saveButton.Text = "Save";
            this._saveButton.UseVisualStyleBackColor = true;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.Location = new System.Drawing.Point(397, 236);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 13;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _browseLocalButton
            // 
            this._browseLocalButton.Location = new System.Drawing.Point(447, 164);
            this._browseLocalButton.Name = "_browseLocalButton";
            this._browseLocalButton.Size = new System.Drawing.Size(25, 20);
            this._browseLocalButton.TabIndex = 16;
            this._browseLocalButton.Text = "...";
            this._browseLocalButton.UseVisualStyleBackColor = true;
            this._browseLocalButton.Click += new System.EventHandler(this._browseLocalButton_Click);
            // 
            // _deleteLink
            // 
            this._deleteLink.AutoSize = true;
            this._deleteLink.LinkColor = System.Drawing.Color.Red;
            this._deleteLink.Location = new System.Drawing.Point(12, 241);
            this._deleteLink.Name = "_deleteLink";
            this._deleteLink.Size = new System.Drawing.Size(86, 13);
            this._deleteLink.TabIndex = 17;
            this._deleteLink.TabStop = true;
            this._deleteLink.Text = "Delete Workflow";
            this._deleteLink.VisitedLinkColor = System.Drawing.Color.Red;
            this._deleteLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._deleteLink_LinkClicked);
            // 
            // WorkflowDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 271);
            this.Controls.Add(this._deleteLink);
            this.Controls.Add(this._browseLocalButton);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._saveButton);
            this.Controls.Add(this._fileNameFilterTextBox);
            this.Controls.Add(this._fileNameFilterLabel);
            this.Controls.Add(this._localPathTextBox);
            this.Controls.Add(this._localPathLabel);
            this.Controls.Add(this._remotePathTextBox);
            this.Controls.Add(this._remotePathLabel);
            this.Controls.Add(this._serverComboBox);
            this.Controls.Add(this._serverLabel);
            this.Controls.Add(this._directionComboBox);
            this.Controls.Add(this._directionLabel);
            this.Controls.Add(this._nameTextBox);
            this.Controls.Add(this._nameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WorkflowDetailForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Workflow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _nameLabel;
        private System.Windows.Forms.TextBox _nameTextBox;
        private System.Windows.Forms.Label _directionLabel;
        private System.Windows.Forms.ComboBox _directionComboBox;
        private System.Windows.Forms.Label _serverLabel;
        private System.Windows.Forms.ComboBox _serverComboBox;
        private System.Windows.Forms.Label _remotePathLabel;
        private System.Windows.Forms.TextBox _remotePathTextBox;
        private System.Windows.Forms.Label _localPathLabel;
        private System.Windows.Forms.TextBox _localPathTextBox;
        private System.Windows.Forms.Label _fileNameFilterLabel;
        private System.Windows.Forms.TextBox _fileNameFilterTextBox;
        private System.Windows.Forms.Button _saveButton;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.Button _browseLocalButton;
        private System.Windows.Forms.LinkLabel _deleteLink;
    }
}