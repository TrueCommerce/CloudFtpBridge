using CloudFtpBridge.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudFtpBridge.Infrastructure.FTP
{
    public class FTPFileSystem : IFtpSystem
    {
        private readonly FTPFileSystemOptions _options = new FTPFileSystemOptions();

        public FTPFileSystem(
            IConfiguration configuration,
            ILogger<FTPFileSystem> logger)
        {
            configuration.GetSection("CloudFtpBridge:Infrastructure:FTP").Bind(_options);
        }

        public async Task Send(string localPath)
        {
           await Task.Run(() => SendFiles(localPath));
        }

        public async Task Receive(string localPath)
        {
            await Task.Run(() => ReceiveFiles(localPath));
        }

        public void SendFiles(string localPath)
        {
            Send ftpSend = new Send();
            ftpSend.Pass = _options.Password;
            ftpSend.User = _options.Username;
            ftpSend.FtpUrl = _options.Host;
            ftpSend.LocalDestDir = localPath;
            ftpSend.Dir = _options.Path;
            ftpSend.UseFtps = _options.UseFtps;
            ftpSend.Port = _options.Port;

            if (_options.AutoConnect || (!string.IsNullOrEmpty(_options.DataConnectionType) && _options.DataConnectionType.Equals("Passive")))
                ftpSend.UsePassive = true;
            else
                ftpSend.UsePassive = false;

            ftpSend.SendFiles();
        }

        public void ReceiveFiles(string localPath)
        {
            Receive ftpRcv = new Receive();
            ftpRcv.Pass = _options.Password;
            ftpRcv.User = _options.Username;
            ftpRcv.FtpUrl = _options.Host;
            ftpRcv.LocalDestDir = localPath;
            ftpRcv.Dir = _options.Path;
            ftpRcv.UseFtps = _options.UseFtps;
            ftpRcv.Port = _options.Port;

            if (_options.AutoConnect || (!string.IsNullOrEmpty(_options.DataConnectionType) && _options.DataConnectionType.Equals("Passive")))
                ftpRcv.SetPassive = true;
            else
                ftpRcv.SetPassive = false;

            ftpRcv.Download();
        }
        
    }
}
