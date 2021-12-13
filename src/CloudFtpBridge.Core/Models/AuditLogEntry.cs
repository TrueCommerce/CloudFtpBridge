using System;

namespace CloudFtpBridge.Core.Models
{
    public class AuditLogEntry
    {
        public DateTimeOffset DateCreated { get; set; }
        public string WorkflowName { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public FileStage FileStage { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
    }
}
