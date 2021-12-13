using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using LiteDB;
using Microsoft.Extensions.Options;

using CloudFtpBridge.Core.Models;
using CloudFtpBridge.Core.Services;

namespace CloudFtpBridge.Infrastructure.LiteDB
{
    public class LiteDBAuditLog : IAuditLog, IDisposable
    {
        private readonly ILiteDatabase _db;
        private readonly ILiteCollection<AuditLogEntry> _auditLogEntryCollection;

        public LiteDBAuditLog(IOptionsMonitor<LiteDBOptions> liteDBOptions)
        {
            _db = new LiteDatabase(liteDBOptions.CurrentValue.AuditLogDbConnectionString);
            _auditLogEntryCollection = _db.GetCollection<AuditLogEntry>();
        }

        public Task AddEntry(Workflow workflow, FileRef file, FileStage stage, string message = null, string refData = null)
        {
            _auditLogEntryCollection.Insert(new AuditLogEntry
            {
                DateCreated = DateTimeOffset.UtcNow,
                WorkflowName = workflow.Name,
                FileName = file.Name,
                FileStage = stage,
                Message = message ?? string.Empty,
                Reference = refData ?? string.Empty
            });

            return Task.CompletedTask;
        }

        public Task Cleanup(DateTimeOffset deleteBeforeDate)
        {
            _auditLogEntryCollection.DeleteMany(e => e.DateCreated < deleteBeforeDate);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _db.Checkpoint();
        }

        public Task<IReadOnlyCollection<AuditLogEntry>> GetEntries(int pageNumber, int pageSize = 100, Expression<Func<AuditLogEntry, bool>> filter = null)
        {
            filter = filter ?? (e => true);

            var entries = _auditLogEntryCollection
                .Query()
                .OrderByDescending(e => e.DateCreated)
                .Where(filter)
                .Skip(pageSize * (pageNumber - 1))
                .Limit(pageSize)
                .ToArray()
                .GroupBy(e => e.FileName)
                .Select(g => g
                    .OrderByDescending(e => e.FileStage))
                .SelectMany(g => g)
                .ToArray();

            return Task.FromResult(entries as IReadOnlyCollection<AuditLogEntry>);
        }
    }
}
