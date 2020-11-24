using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading;

using FluentFTP;

namespace CloudFtpBridge.Infrastructure.FluentFTP
{
    public class FluentFTPFileSystemOptions
    {
        private const string _ConfigPrefix = "CloudFtpBridge:Infrastructure:FluentFTP:";

        public FluentFTPFileSystemOptions()
        { }

        public FluentFTPFileSystemOptions(IDictionary<string, string> configuration)
        {
            configuration.ToObject(_ConfigPrefix, this);
        }

        public bool AutoConnect { get; set; } = true;
        public FtpDataConnectionType DataConnectionType { get; set; }
        public FtpEncryptionMode EncryptionMode { get; set; }
        public string Host { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public int Port { get; set; } = 21;
        public int RetryAttempts { get; set; }
        public int SocketPollInterval { get; set; }
        public SslProtocols SslProtocol { get; set; }
        public int Timeout { get; set; } = System.Threading.Timeout.Infinite;
        public string Username { get; set; } = string.Empty;

        public Dictionary<string, string> ToDictionary(Dictionary<string, string> dictionary = null)
        {
            return this.ToStringDictionary(_ConfigPrefix, dictionary);
        }
    }
}
