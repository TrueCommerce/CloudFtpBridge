﻿using System.Collections.Generic;

namespace CloudFtpBridge.Core.Models
{
    public class MailSettings
    {
        public List<string> ToAddresses { get; set; } = new List<string>();
        public string FromAddress { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 25;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Enabled { get; set; }
    }
}
