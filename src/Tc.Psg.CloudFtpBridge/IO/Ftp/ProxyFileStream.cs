using System;
using System.IO;

namespace Tc.Psg.CloudFtpBridge.IO.Ftp
{
    public class ProxyFileStream : FileStream
    {
        public ProxyFileStream(string path, FileMode mode, FileAccess access)
            : base(path, mode, access)
        { }

        public event Action Closed;

        public override void Close()
        {
            base.Close();

            Closed?.Invoke();
        }
    }
}
