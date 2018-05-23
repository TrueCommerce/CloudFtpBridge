using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Tc.Psg.CloudFtpBridge.IO.Ftp
{
    public class ProxyFileStream : FileStream
    {
        private readonly Func<Task> _flushAction;

        public ProxyFileStream(string path, FileMode mode, FileAccess access, Func<Task> flushAction)
            : base(path, mode, access)
        {
            _flushAction = flushAction ?? throw new ArgumentNullException(nameof(flushAction));
        }

        public override async Task FlushAsync(CancellationToken cancellationToken)
        {
            await base.FlushAsync(cancellationToken);
            await _flushAction.Invoke();
        }
    }
}
