namespace Tc.Psg.CloudFtpBridge.UI.Controls
{
    partial class AboutControl
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
            this._licenseLink = new System.Windows.Forms.LinkLabel();
            this._copyrightLabel = new System.Windows.Forms.Label();
            this._appVersionLabel = new System.Windows.Forms.Label();
            this._appNameLabel = new System.Windows.Forms.Label();
            this._logoPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _licenseLink
            // 
            this._licenseLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._licenseLink.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._licenseLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(124)))), ((int)(((byte)(186)))));
            this._licenseLink.Location = new System.Drawing.Point(6, 319);
            this._licenseLink.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this._licenseLink.Name = "_licenseLink";
            this._licenseLink.Size = new System.Drawing.Size(952, 30);
            this._licenseLink.TabIndex = 9;
            this._licenseLink.TabStop = true;
            this._licenseLink.Text = "Use of this software is governed under the MIT License";
            this._licenseLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._licenseLink.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(124)))), ((int)(((byte)(186)))));
            this._licenseLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._licenseLink_LinkClicked);
            // 
            // _copyrightLabel
            // 
            this._copyrightLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._copyrightLabel.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._copyrightLabel.Location = new System.Drawing.Point(6, 289);
            this._copyrightLabel.Name = "_copyrightLabel";
            this._copyrightLabel.Size = new System.Drawing.Size(952, 30);
            this._copyrightLabel.TabIndex = 10;
            this._copyrightLabel.Text = "Copyright (c) 2018, TrueCommerce, Inc.";
            this._copyrightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _appVersionLabel
            // 
            this._appVersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._appVersionLabel.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._appVersionLabel.Location = new System.Drawing.Point(6, 259);
            this._appVersionLabel.Name = "_appVersionLabel";
            this._appVersionLabel.Size = new System.Drawing.Size(952, 30);
            this._appVersionLabel.TabIndex = 8;
            this._appVersionLabel.Text = "[FILE_VERSION]";
            this._appVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _appNameLabel
            // 
            this._appNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._appNameLabel.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._appNameLabel.Location = new System.Drawing.Point(9, 229);
            this._appNameLabel.Name = "_appNameLabel";
            this._appNameLabel.Size = new System.Drawing.Size(949, 30);
            this._appNameLabel.TabIndex = 7;
            this._appNameLabel.Text = "Cloud FTP Bridge";
            this._appNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _logoPictureBox
            // 
            this._logoPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._logoPictureBox.Image = global::Tc.Psg.CloudFtpBridge.UI.Properties.Resources.TrueCommerceLogo;
            this._logoPictureBox.Location = new System.Drawing.Point(9, 133);
            this._logoPictureBox.Name = "_logoPictureBox";
            this._logoPictureBox.Size = new System.Drawing.Size(949, 93);
            this._logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._logoPictureBox.TabIndex = 6;
            this._logoPictureBox.TabStop = false;
            // 
            // AboutControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._licenseLink);
            this.Controls.Add(this._copyrightLabel);
            this.Controls.Add(this._appVersionLabel);
            this.Controls.Add(this._appNameLabel);
            this.Controls.Add(this._logoPictureBox);
            this.Name = "AboutControl";
            this.Size = new System.Drawing.Size(965, 482);
            ((System.ComponentModel.ISupportInitialize)(this._logoPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel _licenseLink;
        private System.Windows.Forms.Label _copyrightLabel;
        private System.Windows.Forms.Label _appVersionLabel;
        private System.Windows.Forms.Label _appNameLabel;
        private System.Windows.Forms.PictureBox _logoPictureBox;
    }
}
