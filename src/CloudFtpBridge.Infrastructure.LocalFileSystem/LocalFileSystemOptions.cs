using System;
using System.Collections.Generic;

namespace CloudFtpBridge.Infrastructure.LocalFileSystem
{
    public class LocalFileSystemOptions
    {
        private const string _ConfigPrefix = "CloudFtpBridge:Infrastructure:LocalFileSystem:";

        public LocalFileSystemOptions()
        { }

        public LocalFileSystemOptions(IDictionary<string, string> configuration)
        {
            configuration.ToObject(_ConfigPrefix, this);
        }
        public string Path { get; set; } = string.Empty;
        public string Pattern { get; set; } = "*.*";
    }
}
