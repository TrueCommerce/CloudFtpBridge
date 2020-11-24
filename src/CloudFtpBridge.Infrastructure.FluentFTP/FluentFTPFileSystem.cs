using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using FluentFTP;
using Microsoft.Extensions.Configuration;

using CloudFtpBridge.Core.Models;
using CloudFtpBridge.Core.Services;
using CloudFtpBridge.Core.Utils;
using Microsoft.Extensions.Logging;

namespace CloudFtpBridge.Infrastructure.FluentFTP
{
    public class FluentFTPFileSystem : IFileSystem
    {
        private readonly IFtpClient _ftpClient;
        private readonly FluentFTPFileSystemOptions _options = new FluentFTPFileSystemOptions();

        public FluentFTPFileSystem(
            IConfiguration configuration,
            ILogger<FluentFTPFileSystem> logger)
        {
            configuration.GetSection("CloudFtpBridge:Infrastructure:FluentFTP").Bind(_options);

            var ftpClient = new FtpClient(_options.Host, _options.Port, _options.Username, _options.Password);

            ftpClient.OnLogEvent = (level, message) =>
            {
                logger.LogTrace(message);
            };

            _ftpClient = ftpClient;
        }

        public async Task Delete(string fileName)
        {
            await _EnsureConnection();

            await _ftpClient.DeleteFileAsync($"/{PathHelper.Combine(_options.Path, fileName)}");
        }

        public async Task<bool> HasFiles()
        {
            await _EnsureConnection();

            var names = await _ftpClient.GetNameListingAsync($"/{PathHelper.Combine(_options.Path)}");

            return names.Length > 0;
        }

        public async Task<IReadOnlyCollection<FileRef>> List()
        {
            await _EnsureConnection();

            var items = await _ftpClient.GetListingAsync($"/{PathHelper.Combine(_options.Path)}");

            return items
                .Where(i => i.Type == FtpFileSystemObjectType.File)
                .Select(i => new FileRef(i.Name))
                .ToArray();
        }

        public async Task<Stream> Read(string fileName)
        {
            await _EnsureConnection();

            var stream = new MemoryStream();

            await _ftpClient.DownloadAsync(stream, $"/{PathHelper.Combine(_options.Path, fileName)}");

            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        public async Task Write(string fileName, Stream fromStream)
        {
            await _EnsureConnection();

            if (fromStream.CanSeek)
            {
                fromStream.Seek(0, SeekOrigin.Begin);
            }

            await _ftpClient.UploadAsync(fromStream, $"/{PathHelper.Combine(_options.Path, fileName)}", FtpRemoteExists.Overwrite, true);
        }

        private async Task _EnsureConnection()
        {
            if (_ftpClient.IsConnected)
            {
                return;
            }

            if (_options.AutoConnect)
            {
                await _ftpClient.AutoConnectAsync();

                return;
            }

            _ftpClient.DataConnectionType = _options.DataConnectionType;
            _ftpClient.EncryptionMode = _options.EncryptionMode;
            _ftpClient.SslProtocols = _options.SslProtocol;
            _ftpClient.RetryAttempts = _options.RetryAttempts;
            _ftpClient.SocketPollInterval = _options.SocketPollInterval;
            _ftpClient.ConnectTimeout = _options.Timeout;
            _ftpClient.DataConnectionConnectTimeout = _options.Timeout;
            _ftpClient.DataConnectionReadTimeout = _options.Timeout;
            _ftpClient.ReadTimeout = _options.Timeout;

            await _ftpClient.ConnectAsync();
        }
    }
}
