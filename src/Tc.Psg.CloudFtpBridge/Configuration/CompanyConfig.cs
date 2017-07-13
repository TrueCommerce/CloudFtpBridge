using System;

namespace Tc.Psg.CloudFtpBridge.Configuration
{
    public class CompanyConfig
    {
        public CompanyConfig()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            CloudFtpHost = "integrationftp.truecommerce.com";
            CloudFtpPort = 21;
        }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public string Name { get; set; }
        public string EdiId { get; set; }
        public string LocalInboundPath { get; set; }
        public string LocalOutboundPath { get; set; }
        public string CloudFtpHost { get; set; }
        public string CloudFtpUser { get; set; }
        public string CloudFtpPassword { get; set; }
        public int CloudFtpPort { get; set; }
        public bool Enabled { get; set; }
    }
}
