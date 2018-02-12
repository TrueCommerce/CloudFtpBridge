using System;
using System.IO;
using System.Threading.Tasks;

namespace Tc.Psg.CloudFtpBridge.IO
{
    public interface IFile : IDisposable
    {
        IFolder Folder { get; }
        string FullName { get; }
        string Name { get; }

        Task<bool> Exists();
        Task<Stream> GetReadStream();
        Task<Stream> GetWriteStream();
        Task<IFile> MoveTo(IFolder destinationFolder, string newFileName = null);
    }
}
