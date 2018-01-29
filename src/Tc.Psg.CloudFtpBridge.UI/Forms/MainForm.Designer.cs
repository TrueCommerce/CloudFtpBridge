namespace Tc.Psg.CloudFtpBridge.UI.Forms
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
            this._tabControl = new System.Windows.Forms.TabControl();
            this._workflowsTabPage = new System.Windows.Forms.TabPage();
            this._serversTabPage = new System.Windows.Forms.TabPage();
            this._aboutTabPage = new System.Windows.Forms.TabPage();
            this._tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // _tabControl
            // 
            this._tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this._tabControl.Controls.Add(this._workflowsTabPage);
            this._tabControl.Controls.Add(this._serversTabPage);
            this._tabControl.Controls.Add(this._aboutTabPage);
            this._tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabControl.Location = new System.Drawing.Point(0, 0);
            this._tabControl.Name = "_tabControl";
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(973, 511);
            this._tabControl.TabIndex = 0;
            // 
            // _workflowsTabPage
            // 
            this._workflowsTabPage.Location = new System.Drawing.Point(4, 25);
            this._workflowsTabPage.Name = "_workflowsTabPage";
            this._workflowsTabPage.Size = new System.Drawing.Size(965, 482);
            this._workflowsTabPage.TabIndex = 0;
            this._workflowsTabPage.Text = "Workflows";
            this._workflowsTabPage.UseVisualStyleBackColor = true;
            // 
            // _serversTabPage
            // 
            this._serversTabPage.Location = new System.Drawing.Point(4, 25);
            this._serversTabPage.Name = "_serversTabPage";
            this._serversTabPage.Size = new System.Drawing.Size(965, 482);
            this._serversTabPage.TabIndex = 1;
            this._serversTabPage.Text = "Servers";
            this._serversTabPage.UseVisualStyleBackColor = true;
            // 
            // _aboutTabPage
            // 
            this._aboutTabPage.Location = new System.Drawing.Point(4, 25);
            this._aboutTabPage.Name = "_aboutTabPage";
            this._aboutTabPage.Size = new System.Drawing.Size(965, 482);
            this._aboutTabPage.TabIndex = 2;
            this._aboutTabPage.Text = "About";
            this._aboutTabPage.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 511);
            this.Controls.Add(this._tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cloud FTP Bridge";
            this._tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.TabPage _workflowsTabPage;
        private System.Windows.Forms.TabPage _serversTabPage;
        private System.Windows.Forms.TabPage _aboutTabPage;
    }
}