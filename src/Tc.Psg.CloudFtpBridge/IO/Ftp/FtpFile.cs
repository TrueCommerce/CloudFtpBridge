using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using FluentFTP;
using Tc.Psg.CloudFtpBridge.Utils;

namespace Tc.Psg.CloudFtpBridge.IO.Ftp
{
    public class FtpFile : IFile
    {
        private List<string> _tempFileNames;

        public FtpFile(FtpClient ftpClient, IFolder folder, string fullName)
        {
            _tempFileNames = new List<string>();

            BaseFtpClient = ftpClient;
            Folder = folder;
            FullName = fullName;
        }

        public FtpClient BaseFtpClient { get; private set; }
        public IFolder Folder { get; private set; }
        public string FullName { get; private set; }
        public string Name => PathUtil.GetFileName(FullName);

        public void Dispose()
        {
            try
            {
                foreach (string tempFileName in _tempFileNames)
                {
                    File.Delete(tempFileName);
                }
            }

            catch { }
        }

        public async Task<bool> Exists()
        {
            return await BaseFtpClient.FileExistsAsync(FullName);
        }

        public async Task<Stream> GetReadStream()
        {
            string tempFileName = Path.GetTempFileName();

            await BaseFtpClient.DownloadFileAsync(tempFileName, FullName, true, FtpVerify.Retry | FtpVerify.Throw);

            _tempFileNames.Add(tempFileName);

            return new FileStream(tempFileName, FileMode.Open, FileAccess.Read);
        }

        public async Task<Stream> GetWriteStream()
        {
            string tempFileName = Path.GetTempFileName();

            ProxyFileStream stream = new ProxyFileStream(tempFileName, FileMode.Create, FileAccess.Write);

            stream.Closed += async () =>
            {
                await BaseFtpClient.UploadFileAsync(tempFileName, FullName, FtpExists.Overwrite, true, FtpVerify.Retry | FtpVerify.Throw);
            };

            _tempFileNames.Add(tempFileName);

            return stream;
        }

        public async Task<IFile> MoveTo(IFolder destinationFolder, string newName = null)
        {
            string destinationFullName = PathUtil.CombineFragments(destinationFolder.FullName, Name);

            await BaseFtpClient.MoveFileAsync(FullName, destinationFullName, FtpExists.Overwrite);

            return new FtpFile(BaseFtpClient, destinationFolder, destinationFullName);
        }
    }
}
