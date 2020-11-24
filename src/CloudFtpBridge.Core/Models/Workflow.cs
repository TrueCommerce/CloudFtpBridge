using System;
using System.Collections.Generic;

namespace CloudFtpBridge.Core.Models
{
    public class Workflow
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public Dictionary<string, string> SourceFileSystemConfig { get; set; } = new Dictionary<string, string>();
        public string SourceFileSystemType { get; set; } = string.Empty;
        public string SourceFileFilter { get; set; } = string.Empty;
        public Dictionary<string, string> DestinationFileSystemConfig { get; set; } = new Dictionary<string, string>();
        public string DestinationFileSystemType { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;

    }
}
