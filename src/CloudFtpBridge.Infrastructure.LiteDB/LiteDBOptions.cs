using System.IO;

using CloudFtpBridge.Core.Utils;

namespace CloudFtpBridge.Infrastructure.LiteDB
{
    public class LiteDBOptions
    {
        public string WorkflowDbConnectionString { get; set; } = Path.Combine(PathHelper.GetDefaultStoragePath(), "Workflows.db");
        public string AuditLogDbConnectionString { get; set; } = Path.Combine(PathHelper.GetDefaultStoragePath(), "AuditLog.db");
    }
}
