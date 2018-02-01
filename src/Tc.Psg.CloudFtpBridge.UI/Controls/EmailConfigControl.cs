using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Tc.Psg.CloudFtpBridge.Mail;

namespace Tc.Psg.CloudFtpBridge.UI.Controls
{
    public partial class EmailConfigControl : UserControl
    {
        public EmailConfigControl(IMailOptionsRepository mailOptionsRepository, IMailSender mailSender)
        {
            MailOptionsRepository = mailOptionsRepository;

            InitializeComponent();
        }

        public IMailOptionsRepository MailOptionsRepository { get; private set; }
        public IMailSender MailSender { get; private set; }

        private void _testButton_Click(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                await MailSender.Send("Cloud FTP Bridge Email Test", "This is a test email from the Cloud FTP Bridge utility.");
            }).Wait();

            MessageBox.Show("Test message sent!", "Test Email Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            int smtpPort = 25;
            int.TryParse(_smtpPortTextBox.Text, out smtpPort);

            MailOptions options = new MailOptions
            {
                FromAddress = _fromAddressTextBox.Text,
                SmtpHost = _smtpHostTextBox.Text,
                SmtpPassword = _smtpPasswordTextBox.Text,
                SmtpPort = smtpPort,
                SmtpUsername = _smtpUsernameTextBox.Text,
                ToAddresses = new List<string>(_toAddressesTextBox.Text.Split(',', ';').Select(x => x.Trim()))
            };

            MailOptionsRepository.Set(options);
        }
    }
}
