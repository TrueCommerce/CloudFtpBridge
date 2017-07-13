namespace Tc.Psg.CloudFtpBridge.ConfigManager.Forms
{
    partial class CompanyConfigForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._browseOutboundPath = new System.Windows.Forms.Button();
            this._browseInboundPath = new System.Windows.Forms.Button();
            this._outboundPath = new System.Windows.Forms.TextBox();
            this._inboundPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this._ftpPassword = new System.Windows.Forms.TextBox();
            this._ftpUsername = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._save = new System.Windows.Forms.Button();
            this._cancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this._companyName = new System.Windows.Forms.TextBox();
            this._folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label7 = new System.Windows.Forms.Label();
            this._ediId = new System.Windows.Forms.TextBox();
            this._enabled = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._browseOutboundPath);
            this.groupBox1.Controls.Add(this._browseInboundPath);
            this.groupBox1.Controls.Add(this._outboundPath);
            this.groupBox1.Controls.Add(this._inboundPath);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(575, 89);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Local Folder Paths";
            // 
            // _browseOutboundPath
            // 
            this._browseOutboundPath.Location = new System.Drawing.Point(526, 49);
            this._browseOutboundPath.Name = "_browseOutboundPath";
            this._browseOutboundPath.Size = new System.Drawing.Size(35, 23);
            this._browseOutboundPath.TabIndex = 5;
            this._browseOutboundPath.Text = "...";
            this._browseOutboundPath.UseVisualStyleBackColor = true;
            this._browseOutboundPath.Click += new System.EventHandler(this._OnBrowseOutboundPathClick);
            // 
            // _browseInboundPath
            // 
            this._browseInboundPath.Location = new System.Drawing.Point(526, 21);
            this._browseInboundPath.Name = "_browseInboundPath";
            this._browseInboundPath.Size = new System.Drawing.Size(35, 23);
            this._browseInboundPath.TabIndex = 4;
            this._browseInboundPath.Text = "...";
            this._browseInboundPath.UseVisualStyleBackColor = true;
            this._browseInboundPath.Click += new System.EventHandler(this._OnBrowseInboundPathClick);
            // 
            // _outboundPath
            // 
            this._outboundPath.Location = new System.Drawing.Point(120, 49);
            this._outboundPath.Name = "_outboundPath";
            this._outboundPath.Size = new System.Drawing.Size(400, 22);
            this._outboundPath.TabIndex = 3;
            // 
            // _inboundPath
            // 
            this._inboundPath.Location = new System.Drawing.Point(120, 21);
            this._inboundPath.Name = "_inboundPath";
            this._inboundPath.Size = new System.Drawing.Size(400, 22);
            this._inboundPath.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Inbound Files:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Outbound Files:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this._ftpPassword);
            this.groupBox2.Controls.Add(this._ftpUsername);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 163);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(575, 89);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cloud FTP Credentials";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(356, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(213, 50);
            this.label6.TabIndex = 4;
            this.label6.Text = "Please contact TrueCommerce to obtain your FTP credentials.";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _ftpPassword
            // 
            this._ftpPassword.Location = new System.Drawing.Point(120, 49);
            this._ftpPassword.Name = "_ftpPassword";
            this._ftpPassword.Size = new System.Drawing.Size(230, 22);
            this._ftpPassword.TabIndex = 3;
            // 
            // _ftpUsername
            // 
            this._ftpUsername.Location = new System.Drawing.Point(120, 21);
            this._ftpUsername.Name = "_ftpUsername";
            this._ftpUsername.Size = new System.Drawing.Size(230, 22);
            this._ftpUsername.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Username:";
            // 
            // _save
            // 
            this._save.Location = new System.Drawing.Point(406, 258);
            this._save.Name = "_save";
            this._save.Size = new System.Drawing.Size(100, 34);
            this._save.TabIndex = 2;
            this._save.Text = "Save";
            this._save.UseVisualStyleBackColor = true;
            this._save.Click += new System.EventHandler(this._OnSaveClick);
            // 
            // _cancel
            // 
            this._cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancel.Location = new System.Drawing.Point(512, 258);
            this._cancel.Name = "_cancel";
            this._cancel.Size = new System.Drawing.Size(75, 34);
            this._cancel.TabIndex = 3;
            this._cancel.Text = "Cancel";
            this._cancel.UseVisualStyleBackColor = true;
            this._cancel.Click += new System.EventHandler(this._OnCancelClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Company Name:";
            // 
            // _companyName
            // 
            this._companyName.Location = new System.Drawing.Point(143, 12);
            this._companyName.Name = "_companyName";
            this._companyName.Size = new System.Drawing.Size(389, 22);
            this._companyName.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(79, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "EDI ID:";
            // 
            // _ediId
            // 
            this._ediId.Location = new System.Drawing.Point(143, 40);
            this._ediId.Name = "_ediId";
            this._ediId.Size = new System.Drawing.Size(389, 22);
            this._ediId.TabIndex = 7;
            // 
            // _enabled
            // 
            this._enabled.AutoSize = true;
            this._enabled.Location = new System.Drawing.Point(12, 266);
            this._enabled.Name = "_enabled";
            this._enabled.Size = new System.Drawing.Size(82, 21);
            this._enabled.TabIndex = 8;
            this._enabled.Text = "Enabled";
            this._enabled.UseVisualStyleBackColor = true;
            // 
            // CompanyConfigForm
            // 
            this.AcceptButton = this._save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancel;
            this.ClientSize = new System.Drawing.Size(602, 303);
            this.Controls.Add(this._enabled);
            this.Controls.Add(this._ediId);
            this.Controls.Add(this.label7);
            this.Controls.Add(this._companyName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._cancel);
            this.Controls.Add(this._save);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CompanyConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Company Configuration";
            this.Load += new System.EventHandler(this._OnCompanyConfigFormLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button _browseOutboundPath;
        private System.Windows.Forms.Button _browseInboundPath;
        private System.Windows.Forms.TextBox _outboundPath;
        private System.Windows.Forms.TextBox _inboundPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox _ftpPassword;
        private System.Windows.Forms.TextBox _ftpUsername;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button _save;
        private System.Windows.Forms.Button _cancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox _companyName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.FolderBrowserDialog _folderBrowserDialog;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox _ediId;
        private System.Windows.Forms.CheckBox _enabled;
    }
}