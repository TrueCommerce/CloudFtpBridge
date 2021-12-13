using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using CloudFtpBridge.Core.Models;

namespace CloudFtpBridge.Core.Services
{
    public interface IAuditLog
    {
        Task AddEntry(Workflow workflow, FileRef file, FileStage stage, string message = null, string reference ="");
        Task Cleanup(DateTimeOffset deleteBeforeDate);
        Task<IReadOnlyCollection<AuditLogEntry>> GetEntries(int pageNumber, int pageSize = 100, Expression<Func<AuditLogEntry, bool>> filter = null);
    }
}
