using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Tc.Psg.CloudFtpBridge.IO.Ftp
{
    public class ProxyFileStream : FileStream
    {
        private readonly Func<Stream, Task> _flushAction;

        public ProxyFileStream(string path, FileMode mode, Func<Stream, Task> flushAction)
            : base(path, mode, FileAccess.ReadWrite)
        {
            _flushAction = flushAction ?? throw new ArgumentNullException(nameof(flushAction));
        }

        public override async Task FlushAsync(CancellationToken cancellationToken)
        {
            await base.FlushAsync(cancellationToken);

            Seek(0, SeekOrigin.Begin);

            await _flushAction.Invoke(this);
        }
    }
}
