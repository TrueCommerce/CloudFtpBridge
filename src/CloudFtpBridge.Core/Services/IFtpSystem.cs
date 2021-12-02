using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using CloudFtpBridge.Core.Models;

namespace CloudFtpBridge.Core.Services
{
    public interface IFtpSystem
    {
        Task Send(string localPath);
        Task Receive(string localPath);
    }
}
