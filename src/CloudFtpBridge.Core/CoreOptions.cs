using System;

namespace CloudFtpBridge.Core
{
    public class CoreOptions
    {
        /// <summary>
        /// The maximum length of time an audit log entry can live in the database before being cleaned up.
        /// </summary>
        public TimeSpan AuditLogRetentionLimit { get; set; } = TimeSpan.FromDays(90);

        /// <summary>
        /// The amount of time the worker service will wait before running workflows again if no files were present in the latest listing.
        /// </summary>
        public TimeSpan WorkerDelay { get; set; } = TimeSpan.FromMinutes(2);

        /// <summary>
        /// Forces a garbage collection in all generations after each workflow run to ensure dereferenced streams are immediately cleaned up.
        /// </summary>
        public bool ForceGarbageCollection { get; set; } = false;
    }
}
