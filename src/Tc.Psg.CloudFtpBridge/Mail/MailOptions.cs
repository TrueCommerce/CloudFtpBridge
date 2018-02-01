using System;
using System.Collections.Generic;

namespace Tc.Psg.CloudFtpBridge.Mail
{
    public class MailOptions
    {
        public MailOptions()
        {
            Id = Guid.Empty;
            ToAddresses = new List<string>();
        }

        public Guid Id { get; set; }
        public string FromAddress { get; set; }
        public string SmtpHost { get; set; }
        public string SmtpPassword { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public List<string> ToAddresses { get; set; }

        public static MailOptions Empty
        {
            get
            {
                return new MailOptions();
            }
        }
    }
}
