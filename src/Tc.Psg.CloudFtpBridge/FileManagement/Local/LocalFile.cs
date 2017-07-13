using System.IO;
using System.Threading.Tasks;

namespace Tc.Psg.CloudFtpBridge.FileManagement.Local
{
    public class LocalFile : IFile
    {
        private FileInfo _fileInfo;

        public LocalFile(string fullPath)
        {
            _fileInfo = new FileInfo(fullPath);
        }

        public LocalFile(FileInfo fileInfo)
        {
            _fileInfo = fileInfo;
        }

        public string Name
        {
            get
            {
                return _fileInfo.Name;
            }
        }

        public async Task Delete()
        {
            File.Delete(_fileInfo.FullName);
        }

        public void Dispose()
        {
            // nothing to dispose of here
        }

        public async Task<bool> Exists()
        {
            return _fileInfo.Exists;
        }

        public async Task<string> GetFullPath()
        {
            return _fileInfo.FullName;
        }

        public async Task<Stream> GetReadStream()
        {
            FileStream stream = new FileStream(_fileInfo.FullName, FileMode.Open, FileAccess.Read);

            return stream;
        }

        public async Task Write(Stream stream)
        {
            using (FileStream fileStream = new FileStream(_fileInfo.FullName, FileMode.Open, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream);
            }
        }
    }
}
