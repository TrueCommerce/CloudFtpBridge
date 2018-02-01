namespace Tc.Psg.CloudFtpBridge.UI.Controls
{
    partial class EmailConfigControl
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
            this._alertNoteLabel = new System.Windows.Forms.Label();
            this._smtpGroupBox = new System.Windows.Forms.GroupBox();
            this._testButton = new System.Windows.Forms.Button();
            this._saveButton = new System.Windows.Forms.Button();
            this._toAddressesTextBox = new System.Windows.Forms.TextBox();
            this._fromAddressTextBox = new System.Windows.Forms.TextBox();
            this._smtpPortTextBox = new System.Windows.Forms.TextBox();
            this._smtpHostTextBox = new System.Windows.Forms.TextBox();
            this._toAddressesLabel = new System.Windows.Forms.Label();
            this._fromAddressLabel = new System.Windows.Forms.Label();
            this._smtpPortLabel = new System.Windows.Forms.Label();
            this._smtpHostLabel = new System.Windows.Forms.Label();
            this._smtpUsernameLabel = new System.Windows.Forms.Label();
            this._smtpPasswordLabel = new System.Windows.Forms.Label();
            this._smtpUsernameTextBox = new System.Windows.Forms.TextBox();
            this._smtpPasswordTextBox = new System.Windows.Forms.TextBox();
            this._smtpGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _alertNoteLabel
            // 
            this._alertNoteLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._alertNoteLabel.Location = new System.Drawing.Point(3, 0);
            this._alertNoteLabel.Name = "_alertNoteLabel";
            this._alertNoteLabel.Size = new System.Drawing.Size(959, 35);
            this._alertNoteLabel.TabIndex = 0;
            this._alertNoteLabel.Text = "Email alerts are sent anytime an exception is thrown by the file manager during f" +
    "ile transfers.";
            this._alertNoteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _smtpGroupBox
            // 
            this._smtpGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._smtpGroupBox.Controls.Add(this._smtpPasswordTextBox);
            this._smtpGroupBox.Controls.Add(this._smtpUsernameTextBox);
            this._smtpGroupBox.Controls.Add(this._smtpPasswordLabel);
            this._smtpGroupBox.Controls.Add(this._smtpUsernameLabel);
            this._smtpGroupBox.Controls.Add(this._testButton);
            this._smtpGroupBox.Controls.Add(this._saveButton);
            this._smtpGroupBox.Controls.Add(this._toAddressesTextBox);
            this._smtpGroupBox.Controls.Add(this._fromAddressTextBox);
            this._smtpGroupBox.Controls.Add(this._smtpPortTextBox);
            this._smtpGroupBox.Controls.Add(this._smtpHostTextBox);
            this._smtpGroupBox.Controls.Add(this._toAddressesLabel);
            this._smtpGroupBox.Controls.Add(this._fromAddressLabel);
            this._smtpGroupBox.Controls.Add(this._smtpPortLabel);
            this._smtpGroupBox.Controls.Add(this._smtpHostLabel);
            this._smtpGroupBox.Location = new System.Drawing.Point(3, 38);
            this._smtpGroupBox.Name = "_smtpGroupBox";
            this._smtpGroupBox.Size = new System.Drawing.Size(959, 235);
            this._smtpGroupBox.TabIndex = 1;
            this._smtpGroupBox.TabStop = false;
            this._smtpGroupBox.Text = "SMTP Settings";
            // 
            // _testButton
            // 
            this._testButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._testButton.Location = new System.Drawing.Point(752, 205);
            this._testButton.Name = "_testButton";
            this._testButton.Size = new System.Drawing.Size(75, 23);
            this._testButton.TabIndex = 7;
            this._testButton.Text = "Test";
            this._testButton.UseVisualStyleBackColor = true;
            this._testButton.Click += new System.EventHandler(this._testButton_Click);
            // 
            // _saveButton
            // 
            this._saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._saveButton.Location = new System.Drawing.Point(833, 205);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(120, 23);
            this._saveButton.TabIndex = 6;
            this._saveButton.Text = "Save";
            this._saveButton.UseVisualStyleBackColor = true;
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _toAddressesTextBox
            // 
            this._toAddressesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._toAddressesTextBox.Location = new System.Drawing.Point(9, 179);
            this._toAddressesTextBox.Name = "_toAddressesTextBox";
            this._toAddressesTextBox.Size = new System.Drawing.Size(944, 20);
            this._toAddressesTextBox.TabIndex = 5;
            // 
            // _fromAddressTextBox
            // 
            this._fromAddressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._fromAddressTextBox.Location = new System.Drawing.Point(9, 133);
            this._fromAddressTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this._fromAddressTextBox.Name = "_fromAddressTextBox";
            this._fromAddressTextBox.Size = new System.Drawing.Size(944, 20);
            this._fromAddressTextBox.TabIndex = 4;
            // 
            // _smtpPortTextBox
            // 
            this._smtpPortTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._smtpPortTextBox.Location = new System.Drawing.Point(888, 41);
            this._smtpPortTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this._smtpPortTextBox.Name = "_smtpPortTextBox";
            this._smtpPortTextBox.Size = new System.Drawing.Size(65, 20);
            this._smtpPortTextBox.TabIndex = 1;
            // 
            // _smtpHostTextBox
            // 
            this._smtpHostTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._smtpHostTextBox.Location = new System.Drawing.Point(9, 41);
            this._smtpHostTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this._smtpHostTextBox.Name = "_smtpHostTextBox";
            this._smtpHostTextBox.Size = new System.Drawing.Size(873, 20);
            this._smtpHostTextBox.TabIndex = 0;
            // 
            // _toAddressesLabel
            // 
            this._toAddressesLabel.AutoSize = true;
            this._toAddressesLabel.Location = new System.Drawing.Point(6, 163);
            this._toAddressesLabel.Name = "_toAddressesLabel";
            this._toAddressesLabel.Size = new System.Drawing.Size(72, 13);
            this._toAddressesLabel.TabIndex = 3;
            this._toAddressesLabel.Text = "To Addresses";
            // 
            // _fromAddressLabel
            // 
            this._fromAddressLabel.AutoSize = true;
            this._fromAddressLabel.Location = new System.Drawing.Point(6, 117);
            this._fromAddressLabel.Name = "_fromAddressLabel";
            this._fromAddressLabel.Size = new System.Drawing.Size(71, 13);
            this._fromAddressLabel.TabIndex = 2;
            this._fromAddressLabel.Text = "From Address";
            // 
            // _smtpPortLabel
            // 
            this._smtpPortLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._smtpPortLabel.AutoSize = true;
            this._smtpPortLabel.Location = new System.Drawing.Point(885, 25);
            this._smtpPortLabel.Name = "_smtpPortLabel";
            this._smtpPortLabel.Size = new System.Drawing.Size(26, 13);
            this._smtpPortLabel.TabIndex = 1;
            this._smtpPortLabel.Text = "Port";
            // 
            // _smtpHostLabel
            // 
            this._smtpHostLabel.AutoSize = true;
            this._smtpHostLabel.Location = new System.Drawing.Point(6, 25);
            this._smtpHostLabel.Name = "_smtpHostLabel";
            this._smtpHostLabel.Size = new System.Drawing.Size(62, 13);
            this._smtpHostLabel.TabIndex = 0;
            this._smtpHostLabel.Text = "SMTP Host";
            // 
            // _smtpUsernameLabel
            // 
            this._smtpUsernameLabel.AutoSize = true;
            this._smtpUsernameLabel.Location = new System.Drawing.Point(6, 71);
            this._smtpUsernameLabel.Name = "_smtpUsernameLabel";
            this._smtpUsernameLabel.Size = new System.Drawing.Size(88, 13);
            this._smtpUsernameLabel.TabIndex = 2;
            this._smtpUsernameLabel.Text = "SMTP Username";
            // 
            // _smtpPasswordLabel
            // 
            this._smtpPasswordLabel.AutoSize = true;
            this._smtpPasswordLabel.Location = new System.Drawing.Point(472, 71);
            this._smtpPasswordLabel.Name = "_smtpPasswordLabel";
            this._smtpPasswordLabel.Size = new System.Drawing.Size(86, 13);
            this._smtpPasswordLabel.TabIndex = 10;
            this._smtpPasswordLabel.Text = "SMTP Password";
            // 
            // _smtpUsernameTextBox
            // 
            this._smtpUsernameTextBox.Location = new System.Drawing.Point(9, 87);
            this._smtpUsernameTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this._smtpUsernameTextBox.Name = "_smtpUsernameTextBox";
            this._smtpUsernameTextBox.Size = new System.Drawing.Size(460, 20);
            this._smtpUsernameTextBox.TabIndex = 2;
            // 
            // _smtpPasswordTextBox
            // 
            this._smtpPasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._smtpPasswordTextBox.Location = new System.Drawing.Point(475, 87);
            this._smtpPasswordTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this._smtpPasswordTextBox.Name = "_smtpPasswordTextBox";
            this._smtpPasswordTextBox.Size = new System.Drawing.Size(478, 20);
            this._smtpPasswordTextBox.TabIndex = 3;
            // 
            // EmailConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._smtpGroupBox);
            this.Controls.Add(this._alertNoteLabel);
            this.Name = "EmailConfigControl";
            this.Size = new System.Drawing.Size(965, 482);
            this._smtpGroupBox.ResumeLayout(false);
            this._smtpGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label _alertNoteLabel;
        private System.Windows.Forms.GroupBox _smtpGroupBox;
        private System.Windows.Forms.TextBox _smtpHostTextBox;
        private System.Windows.Forms.Label _toAddressesLabel;
        private System.Windows.Forms.Label _fromAddressLabel;
        private System.Windows.Forms.Label _smtpPortLabel;
        private System.Windows.Forms.Label _smtpHostLabel;
        private System.Windows.Forms.TextBox _smtpPortTextBox;
        private System.Windows.Forms.Button _testButton;
        private System.Windows.Forms.Button _saveButton;
        private System.Windows.Forms.TextBox _toAddressesTextBox;
        private System.Windows.Forms.TextBox _fromAddressTextBox;
        private System.Windows.Forms.TextBox _smtpPasswordTextBox;
        private System.Windows.Forms.TextBox _smtpUsernameTextBox;
        private System.Windows.Forms.Label _smtpPasswordLabel;
        private System.Windows.Forms.Label _smtpUsernameLabel;
    }
}
