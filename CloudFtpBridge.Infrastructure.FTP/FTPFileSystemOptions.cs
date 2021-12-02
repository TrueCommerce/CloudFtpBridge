using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFtpBridge.Infrastructure.FTP
{
    public class FTPFileSystemOptions
    {
        private const string _ConfigPrefix = "CloudFtpBridge:Infrastructure:FTP:";

        public FTPFileSystemOptions()
        { }

        public FTPFileSystemOptions(IDictionary<string, string> configuration)
        {
            configuration.ToObject(_ConfigPrefix, this);
        }

        public bool AutoConnect { get; set; } = true;
        public string DataConnectionType { get; set; }
        public string Host { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public int Port { get; set; } = 21;
        public string Username { get; set; } = string.Empty;
        public string LocalPath { get; set; }

        public Dictionary<string, string> ToDictionary(Dictionary<string, string> dictionary = null)
        {
            return this.ToStringDictionary(_ConfigPrefix, dictionary);
        }
    }
}
