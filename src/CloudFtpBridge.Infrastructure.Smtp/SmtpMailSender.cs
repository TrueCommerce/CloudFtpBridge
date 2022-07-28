using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

using CloudFtpBridge.Core.Services;

namespace CloudFtpBridge.Infrastructure.Smtp
{
    public class SmtpMailSender : IMailSender
    {
        private readonly IMailSettingsProvider _mailSettingsProvider;

        public SmtpMailSender(IMailSettingsProvider mailSettingsProvider)
        {
            _mailSettingsProvider = mailSettingsProvider;
        }

        public async Task Send(string subject, string message)
        {
            var settings = await _mailSettingsProvider.Get();

            if (!settings.Enabled)
            {
                return;
            }

            SmtpClient smtpClient = new SmtpClient(settings.Host);

            try
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(settings.Username, settings.Password);
                smtpClient.Port = settings.Port;

                MailMessage mailMessage = new MailMessage();
                mailMessage.Body = message;
                mailMessage.From = new MailAddress(settings.FromAddress);
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = subject;

                foreach (string toAddress in settings.ToAddresses)
                {
                    mailMessage.To.Add(toAddress);
                }

                await Task.Run(() =>
                {
                    smtpClient.Send(mailMessage);
                });
            }

            finally
            {
                try
                {
                    smtpClient.Dispose();
                }

                catch { }
            }
            
        }
    }
}
