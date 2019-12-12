using System.IO;
using System.Threading.Tasks;

namespace Tc.Psg.CloudFtpBridge.IO.Local
{
    public class LocalFile : IFile
    {
        public LocalFile(IFolder folder, string fullName)
        {
            BaseFileInfo = new FileInfo(fullName);
            Folder = folder;
        }

        public FileInfo BaseFileInfo { get; private set; }
        public IFolder Folder { get; private set; }
        public string FullName => BaseFileInfo.FullName;
        public string Name => BaseFileInfo.Name;

        public void Dispose()
        {
            // do nothing
        }

        public async Task<bool> Exists()
        {
            return BaseFileInfo.Exists;
        }

        public async Task<Stream> GetReadStream()
        {
            return new FileStream(BaseFileInfo.FullName, FileMode.Open, FileAccess.Read);
        }

        public async Task<Stream> GetWriteStream()
        {
            return new FileStream(BaseFileInfo.FullName, FileMode.Create, FileAccess.Write);
        }

        public async Task<IFile> MoveTo(IFolder destinationFolder, string newName)
        {
            string destinationFileName = Path.Combine(destinationFolder.FullName, newName ?? Name);

            File.Move(FullName, destinationFileName);

            return new LocalFile(destinationFolder, destinationFileName);
        }

        public async Task Delete()
        {
            File.Delete(FullName);
        }
    }
}
