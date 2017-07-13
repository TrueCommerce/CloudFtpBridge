using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using FluentFTP;
using Serilog;

namespace Tc.Psg.CloudFtpBridge.FileManagement.Ftp
{
    public class FtpFile : IFile
    {
        private FtpClient _ftpClient;
        private FtpListItem _ftpListItem;
        private ILogger _log;
        private List<string> _tempFileNames;
        private string _fullPath;

        private FtpFile(FtpClient ftpClient)
        {
            _ftpClient = ftpClient;
            _log = Log.ForContext<FtpFile>();
            _tempFileNames = new List<string>();
        }

        public FtpFile(FtpClient ftpClient, FtpListItem ftpListItem)
            : this(ftpClient)
        {
            _ftpListItem = ftpListItem;

            if (_ftpListItem.Type != FtpFileSystemObjectType.File)
            {
                throw new InvalidOperationException("Invalid FtpListItem typ (must be a file).");
            }
        }

        public FtpFile(FtpClient ftpClient, string fullPath)
            : this(ftpClient)
        {
            _fullPath = fullPath;
        }

        public string Name
        {
            get
            {
                string[] pathParts = _fullPath?.Split('/');

                return _ftpListItem.Name ?? pathParts?[pathParts.Length - 1];
            }
        }

        public async Task Delete()
        {
            string fullPath = await GetFullPath();

            await _ftpClient.DeleteFileAsync(fullPath);

            _RemoveTempFiles();
        }

        public void Dispose()
        {
            _RemoveTempFiles();
        }

        public async Task<bool> Exists()
        {
            string fullPath = await GetFullPath();

            return await _ftpClient.FileExistsAsync(fullPath);
        }

        public async Task<string> GetFullPath()
        {
            return _ftpListItem?.FullName ?? _fullPath;
        }

        public async Task<Stream> GetReadStream()
        {
            string tempFileName = await _DownloadRemoteFileToTempFile();

            FileStream stream = new FileStream(tempFileName, FileMode.Open, FileAccess.Read);

            return stream;
        }

        public async Task Write(Stream stream)
        {
            string fullPath = await GetFullPath();

            await _ftpClient.UploadAsync(stream, fullPath, FtpExists.Overwrite, false);
        }

        private async Task<string> _DownloadRemoteFileToTempFile()
        {
            string fullPath = await GetFullPath();

            FtpVerify ftpVerify = FtpVerify.Delete | FtpVerify.Retry | FtpVerify.Throw;
            string tempFileName = Path.GetTempFileName();

            await _ftpClient.DownloadFileAsync(tempFileName, fullPath, true, ftpVerify);

            _tempFileNames.Add(tempFileName);

            return tempFileName;
        }

        private void _RemoveTempFiles()
        {
            try
            {
                foreach (string tempFileName in _tempFileNames)
                {
                    if (File.Exists(tempFileName))
                    {
                        File.Delete(tempFileName);
                    }
                }
            }

            catch (Exception ex)
            {
                // log it, but don't rethrow - this isn't THAT important
                _log.Warning(ex, "Unable to remove all temp files created by this FtpFile instance.");
            }
        }
    }
}
