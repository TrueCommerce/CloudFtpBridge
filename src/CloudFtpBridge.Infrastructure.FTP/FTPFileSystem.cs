using CloudFtpBridge.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudFtpBridge.Core.Models;
using CloudFtpBridge.Core.Utils;

namespace CloudFtpBridge.Infrastructure.FTP
{
    public class FTPFileSystem : IFileSystem
    {
        private readonly FTPFileSystemOptions _options = new FTPFileSystemOptions();
        private readonly FtpWebRequestClient _ftpClient;

        public FTPFileSystem(
            IConfiguration configuration,
            ILogger<FTPFileSystem> logger)
        {
            configuration.GetSection("CloudFtpBridge:Infrastructure:FTP").Bind(_options);
            bool passive = _options.AutoConnect || _options.DataConnectionType.Equals("Passive");

            _ftpClient = new FtpWebRequestClient(_options.Host, _options.Path, _options.Port, _options.Username,
                _options.Password, logger, passive, _options.UseFtps);
        }

        public async Task Delete(string fileName)
        {
            await Task.Run(() =>_ftpClient.Delete(fileName));
        }

        public ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }

        public async Task<bool> HasFiles()
        {
            var names = await Task.Run(()=> _ftpClient.GetListOfFiles());
            return names.Count > 0;
        }

        public async Task<IReadOnlyCollection<FileRef>> List()
        {
            var items = await Task.Run(() => _ftpClient.GetListOfFiles());
            return items
                .Select(i => new FileRef(i))
                .ToArray();
        }

        public async Task<Stream> Read(string fileName)
        {
            var memStream = new MemoryStream();

            var stream = await Task.Run(() => _ftpClient.GetFile(fileName));

            if (stream != null)
            {
                await stream.CopyToAsync(memStream);
                memStream.Seek(0, SeekOrigin.Begin);
            }

            return memStream;
        }

        public async Task Rename(string oldFileName, string newFileName, bool overwriteExisting)
        {
            if (!overwriteExisting && await Task.Run(()=>_ftpClient.FileExists(newFileName)))
            {
                throw new InvalidOperationException($"Unable to rename {oldFileName}. The file {newFileName} already exists.");
            }

            await Task.Run(()=>_ftpClient.Rename(oldFileName, newFileName));
        }

        public async Task Write(string fileName, Stream fromStream)
        {
            if (fromStream.CanSeek)
            {
                fromStream.Seek(0, SeekOrigin.Begin);
            }

            await Task.Run(()=>_ftpClient.SendToFtp(fileName, fromStream));
        }
    }
}
