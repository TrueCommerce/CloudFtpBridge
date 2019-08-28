﻿namespace Tc.Psg.CloudFtpBridge.UI.Forms
{
    partial class ServerDetailForm
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
            this._serverNameLabel = new System.Windows.Forms.Label();
            this._serverNameTextBox = new System.Windows.Forms.TextBox();
            this._hostLabel = new System.Windows.Forms.Label();
            this._hostTextBox = new System.Windows.Forms.TextBox();
            this._portTextBox = new System.Windows.Forms.TextBox();
            this._portLabel = new System.Windows.Forms.Label();
            this._pathLabel = new System.Windows.Forms.Label();
            this._pathTextBox = new System.Windows.Forms.TextBox();
            this._usernameLabel = new System.Windows.Forms.Label();
            this._passwordLabel = new System.Windows.Forms.Label();
            this._usernameTextBox = new System.Windows.Forms.TextBox();
            this._passwordTextBox = new System.Windows.Forms.TextBox();
            this._saveButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._deleteLink = new System.Windows.Forms.LinkLabel();
            this._testConnectionBtn = new System.Windows.Forms.Button();
            this._ftpsOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this._useFtpsCheckbox = new System.Windows.Forms.CheckBox();
            this._encryptionModeGroupBox = new System.Windows.Forms.GroupBox();
            this._encryptionModeComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._dataConnectionTypeCombo = new System.Windows.Forms.ComboBox();
            this._ftpsOptionsGroupBox.SuspendLayout();
            this._encryptionModeGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _serverNameLabel
            // 
            this._serverNameLabel.AutoSize = true;
            this._serverNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._serverNameLabel.Location = new System.Drawing.Point(12, 9);
            this._serverNameLabel.Name = "_serverNameLabel";
            this._serverNameLabel.Size = new System.Drawing.Size(80, 13);
            this._serverNameLabel.TabIndex = 0;
            this._serverNameLabel.Text = "Server Name";
            // 
            // _serverNameTextBox
            // 
            this._serverNameTextBox.Location = new System.Drawing.Point(15, 25);
            this._serverNameTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this._serverNameTextBox.Name = "_serverNameTextBox";
            this._serverNameTextBox.Size = new System.Drawing.Size(457, 20);
            this._serverNameTextBox.TabIndex = 1;
            // 
            // _hostLabel
            // 
            this._hostLabel.AutoSize = true;
            this._hostLabel.Location = new System.Drawing.Point(12, 55);
            this._hostLabel.Name = "_hostLabel";
            this._hostLabel.Size = new System.Drawing.Size(29, 13);
            this._hostLabel.TabIndex = 2;
            this._hostLabel.Text = "Host";
            // 
            // _hostTextBox
            // 
            this._hostTextBox.Location = new System.Drawing.Point(15, 71);
            this._hostTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this._hostTextBox.Name = "_hostTextBox";
            this._hostTextBox.Size = new System.Drawing.Size(416, 20);
            this._hostTextBox.TabIndex = 2;
            // 
            // _portTextBox
            // 
            this._portTextBox.Location = new System.Drawing.Point(437, 71);
            this._portTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this._portTextBox.Name = "_portTextBox";
            this._portTextBox.Size = new System.Drawing.Size(35, 20);
            this._portTextBox.TabIndex = 3;
            this._portTextBox.Text = "21";
            // 
            // _portLabel
            // 
            this._portLabel.AutoSize = true;
            this._portLabel.Location = new System.Drawing.Point(434, 55);
            this._portLabel.Name = "_portLabel";
            this._portLabel.Size = new System.Drawing.Size(26, 13);
            this._portLabel.TabIndex = 6;
            this._portLabel.Text = "Port";
            // 
            // _pathLabel
            // 
            this._pathLabel.AutoSize = true;
            this._pathLabel.Location = new System.Drawing.Point(12, 101);
            this._pathLabel.Name = "_pathLabel";
            this._pathLabel.Size = new System.Drawing.Size(29, 13);
            this._pathLabel.TabIndex = 7;
            this._pathLabel.Text = "Path";
            // 
            // _pathTextBox
            // 
            this._pathTextBox.Location = new System.Drawing.Point(15, 117);
            this._pathTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this._pathTextBox.Name = "_pathTextBox";
            this._pathTextBox.Size = new System.Drawing.Size(457, 20);
            this._pathTextBox.TabIndex = 4;
            // 
            // _usernameLabel
            // 
            this._usernameLabel.AutoSize = true;
            this._usernameLabel.Location = new System.Drawing.Point(12, 147);
            this._usernameLabel.Name = "_usernameLabel";
            this._usernameLabel.Size = new System.Drawing.Size(55, 13);
            this._usernameLabel.TabIndex = 9;
            this._usernameLabel.Text = "Username";
            // 
            // _passwordLabel
            // 
            this._passwordLabel.AutoSize = true;
            this._passwordLabel.Location = new System.Drawing.Point(218, 147);
            this._passwordLabel.Name = "_passwordLabel";
            this._passwordLabel.Size = new System.Drawing.Size(53, 13);
            this._passwordLabel.TabIndex = 10;
            this._passwordLabel.Text = "Password";
            // 
            // _usernameTextBox
            // 
            this._usernameTextBox.Location = new System.Drawing.Point(15, 165);
            this._usernameTextBox.Name = "_usernameTextBox";
            this._usernameTextBox.Size = new System.Drawing.Size(200, 20);
            this._usernameTextBox.TabIndex = 5;
            // 
            // _passwordTextBox
            // 
            this._passwordTextBox.Location = new System.Drawing.Point(221, 165);
            this._passwordTextBox.Name = "_passwordTextBox";
            this._passwordTextBox.Size = new System.Drawing.Size(251, 20);
            this._passwordTextBox.TabIndex = 6;
            this._passwordTextBox.UseSystemPasswordChar = true;
            // 
            // _saveButton
            // 
            this._saveButton.Location = new System.Drawing.Point(273, 272);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(120, 23);
            this._saveButton.TabIndex = 7;
            this._saveButton.Text = "Save";
            this._saveButton.UseVisualStyleBackColor = true;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.Location = new System.Drawing.Point(399, 272);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 8;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _deleteLink
            // 
            this._deleteLink.AutoSize = true;
            this._deleteLink.LinkColor = System.Drawing.Color.Red;
            this._deleteLink.Location = new System.Drawing.Point(12, 277);
            this._deleteLink.Name = "_deleteLink";
            this._deleteLink.Size = new System.Drawing.Size(72, 13);
            this._deleteLink.TabIndex = 11;
            this._deleteLink.TabStop = true;
            this._deleteLink.Text = "Delete Server";
            this._deleteLink.Visible = false;
            this._deleteLink.VisitedLinkColor = System.Drawing.Color.Red;
            this._deleteLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._deleteLink_LinkClicked);
            // 
            // _testConnectionBtn
            // 
            this._testConnectionBtn.Location = new System.Drawing.Point(165, 272);
            this._testConnectionBtn.Name = "_testConnectionBtn";
            this._testConnectionBtn.Size = new System.Drawing.Size(102, 23);
            this._testConnectionBtn.TabIndex = 12;
            this._testConnectionBtn.Text = "Test Connection";
            this._testConnectionBtn.UseVisualStyleBackColor = true;
            this._testConnectionBtn.Click += new System.EventHandler(this._testConnectionBtn_Click);
            // 
            // _ftpsOptionsGroupBox
            // 
            this._ftpsOptionsGroupBox.Controls.Add(this._useFtpsCheckbox);
            this._ftpsOptionsGroupBox.Controls.Add(this._encryptionModeGroupBox);
            this._ftpsOptionsGroupBox.Location = new System.Drawing.Point(191, 192);
            this._ftpsOptionsGroupBox.Name = "_ftpsOptionsGroupBox";
            this._ftpsOptionsGroupBox.Size = new System.Drawing.Size(283, 74);
            this._ftpsOptionsGroupBox.TabIndex = 13;
            this._ftpsOptionsGroupBox.TabStop = false;
            this._ftpsOptionsGroupBox.Text = "FTPS Options";
            // 
            // _useFtpsCheckbox
            // 
            this._useFtpsCheckbox.AutoSize = true;
            this._useFtpsCheckbox.Location = new System.Drawing.Point(6, 31);
            this._useFtpsCheckbox.Name = "_useFtpsCheckbox";
            this._useFtpsCheckbox.Size = new System.Drawing.Size(75, 17);
            this._useFtpsCheckbox.TabIndex = 3;
            this._useFtpsCheckbox.Text = "Use FTPS";
            this._useFtpsCheckbox.UseVisualStyleBackColor = true;
            this._useFtpsCheckbox.CheckedChanged += new System.EventHandler(this._useFtpsCheckbox_CheckedChanged);
            // 
            // _encryptionModeGroupBox
            // 
            this._encryptionModeGroupBox.Controls.Add(this._encryptionModeComboBox);
            this._encryptionModeGroupBox.Location = new System.Drawing.Point(87, 17);
            this._encryptionModeGroupBox.Name = "_encryptionModeGroupBox";
            this._encryptionModeGroupBox.Size = new System.Drawing.Size(183, 51);
            this._encryptionModeGroupBox.TabIndex = 2;
            this._encryptionModeGroupBox.TabStop = false;
            this._encryptionModeGroupBox.Text = "Encryption Mode";
            // 
            // _encryptionModeComboBox
            // 
            this._encryptionModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._encryptionModeComboBox.FormattingEnabled = true;
            this._encryptionModeComboBox.Items.AddRange(new object[] {
            "Explicit",
            "Implicit",
            "None"});
            this._encryptionModeComboBox.Location = new System.Drawing.Point(6, 19);
            this._encryptionModeComboBox.Name = "_encryptionModeComboBox";
            this._encryptionModeComboBox.Size = new System.Drawing.Size(171, 21);
            this._encryptionModeComboBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._dataConnectionTypeCombo);
            this.groupBox1.Location = new System.Drawing.Point(12, 192);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(176, 74);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Connection Type";
            // 
            // _dataConnectionTypeCombo
            // 
            this._dataConnectionTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._dataConnectionTypeCombo.FormattingEnabled = true;
            this._dataConnectionTypeCombo.Items.AddRange(new object[] {
            "Default",
            "AutoPassive",
            "AutoActive",
            "EPRT",
            "EPSV",
            "PASV",
            "PASVEX",
            "PORT"});
            this._dataConnectionTypeCombo.Location = new System.Drawing.Point(6, 19);
            this._dataConnectionTypeCombo.Name = "_dataConnectionTypeCombo";
            this._dataConnectionTypeCombo.Size = new System.Drawing.Size(165, 21);
            this._dataConnectionTypeCombo.TabIndex = 0;
            // 
            // ServerDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 301);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._ftpsOptionsGroupBox);
            this.Controls.Add(this._testConnectionBtn);
            this.Controls.Add(this._deleteLink);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._saveButton);
            this.Controls.Add(this._passwordTextBox);
            this.Controls.Add(this._usernameTextBox);
            this.Controls.Add(this._passwordLabel);
            this.Controls.Add(this._usernameLabel);
            this.Controls.Add(this._pathTextBox);
            this.Controls.Add(this._pathLabel);
            this.Controls.Add(this._portLabel);
            this.Controls.Add(this._portTextBox);
            this.Controls.Add(this._hostTextBox);
            this.Controls.Add(this._hostLabel);
            this.Controls.Add(this._serverNameTextBox);
            this.Controls.Add(this._serverNameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerDetailForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Server";
            this._ftpsOptionsGroupBox.ResumeLayout(false);
            this._ftpsOptionsGroupBox.PerformLayout();
            this._encryptionModeGroupBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _serverNameLabel;
        private System.Windows.Forms.TextBox _serverNameTextBox;
        private System.Windows.Forms.Label _hostLabel;
        private System.Windows.Forms.TextBox _hostTextBox;
        private System.Windows.Forms.TextBox _portTextBox;
        private System.Windows.Forms.Label _portLabel;
        private System.Windows.Forms.Label _pathLabel;
        private System.Windows.Forms.TextBox _pathTextBox;
        private System.Windows.Forms.Label _usernameLabel;
        private System.Windows.Forms.Label _passwordLabel;
        private System.Windows.Forms.TextBox _usernameTextBox;
        private System.Windows.Forms.TextBox _passwordTextBox;
        private System.Windows.Forms.Button _saveButton;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.LinkLabel _deleteLink;
        private System.Windows.Forms.Button _testConnectionBtn;
        private System.Windows.Forms.GroupBox _ftpsOptionsGroupBox;
        private System.Windows.Forms.GroupBox _encryptionModeGroupBox;
        private System.Windows.Forms.CheckBox _useFtpsCheckbox;
        private System.Windows.Forms.ComboBox _encryptionModeComboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox _dataConnectionTypeCombo;
    }
}