using System;
using System.Security.Authentication;
using System.Threading.Tasks;

using FluentFTP;

namespace Tc.Psg.CloudFtpBridge
{
    public class ServerConnectionTester : IServerConnectionTester
    {
        public Task TestConnection(Server server)
        {
            Server.Validate(server);

            var ftpClient = new FtpClient(server.Host, server.Port, server.Username, server.Password);

            if (server.FtpsEnabled && Enum.TryParse(server.EncryptionMode, out FtpEncryptionMode ftpEncryptionMode))
            {
                ftpClient.EncryptionMode = ftpEncryptionMode;
                ftpClient.SslProtocols = SslProtocols.Default;
                ftpClient.ValidateCertificate += (control, e) => e.Accept = true;
            }

            return ftpClient.ConnectAsync();
        }
    }
}
