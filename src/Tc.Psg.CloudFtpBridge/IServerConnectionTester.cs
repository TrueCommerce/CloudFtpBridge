using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tc.Psg.CloudFtpBridge
{
    public interface IServerConnectionTester
    {
        Task TestConnection(Server server);
    }
}
