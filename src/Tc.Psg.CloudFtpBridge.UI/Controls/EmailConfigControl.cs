using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            MailSender = mailSender;

            InitializeComponent();

            _RefreshMailOptions();
        }

        public IMailOptionsRepository MailOptionsRepository { get; private set; }
        public IMailSender MailSender { get; private set; }

        private void _RefreshMailOptions()
        {
            MailOptions options = MailOptionsRepository.Get();

            _fromAddressTextBox.Text = options.FromAddress;
            _smtpHostTextBox.Text = options.SmtpHost;
            _smtpPasswordTextBox.Text = options.SmtpPassword;
            _smtpPortTextBox.Text = options.SmtpPort.ToString();
            _smtpUsernameTextBox.Text = options.SmtpUsername;
            _toAddressesTextBox.Text = options.ToAddresses.Aggregate(string.Empty, (joined, address) => string.Concat(joined, address, ";"));
        }

        private void _testButton_Click(object sender, EventArgs e)
        {
            try
            {
                Task.Run(async () =>
                {
                    await MailSender.Send("Cloud FTP Bridge Email Test", "This is a test email from the Cloud FTP Bridge utility.");
                }).Wait();

                MessageBox.Show("Test message sent!", "Test Email Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

            catch (AggregateException ex)
            {
                StringBuilder errorMessage = new StringBuilder();

                foreach (Exception innerEx in ex.InnerExceptions)
                {
                    errorMessage.AppendLine(innerEx.Message);
                }

                MessageBox.Show(errorMessage.ToString(), "Test Message Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Test Message Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            int smtpPort;
            int.TryParse(_smtpPortTextBox.Text, out smtpPort);

            if (smtpPort == 0)
            {
                smtpPort = 25;
            }

            MailOptions options = new MailOptions
            {
                FromAddress = _fromAddressTextBox.Text,
                SmtpHost = _smtpHostTextBox.Text,
                SmtpPassword = _smtpPasswordTextBox.Text,
                SmtpPort = smtpPort,
                SmtpUsername = _smtpUsernameTextBox.Text,
                ToAddresses = new List<string>(_toAddressesTextBox.Text.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()))
            };

            MailOptionsRepository.Set(options);

            MessageBox.Show("Email settings saved!", "Email Settings", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
