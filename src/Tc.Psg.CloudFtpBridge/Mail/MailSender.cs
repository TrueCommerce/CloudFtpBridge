using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Tc.Psg.CloudFtpBridge.Mail
{
    public class MailSender : IMailSender
    {
        public MailSender(IMailOptionsRepository mailOptionsRepository)
        {
            MailOptionsRepository = mailOptionsRepository;
        }

        public IMailOptionsRepository MailOptionsRepository { get; private set; }

        public async Task Send(string subject, string body, IEnumerable<string> toAddresses = null, string fromAddress = null)
        {
            MailOptions options = MailOptionsRepository.Get();

            if (options.Equals(MailOptions.Empty))
            {
                return;
            }

            toAddresses = toAddresses ?? options.ToAddresses;
            fromAddress = fromAddress ?? options.FromAddress;

            SmtpClient smtpClient = new SmtpClient(options.SmtpHost);
            smtpClient.Credentials = new NetworkCredential(options.SmtpUsername, options.SmtpPassword);
            smtpClient.UseDefaultCredentials = false;

            MailMessage message = new MailMessage();
            message.Body = body;
            message.From = new MailAddress(options.FromAddress);
            message.IsBodyHtml = false;
            message.Subject = subject;
            
            foreach (string toAddress in toAddresses)
            {
                message.To.Add(toAddress);
            }

            await Task.Run(() =>
            {
                smtpClient.Send(message);
            });
        }
    }
}
