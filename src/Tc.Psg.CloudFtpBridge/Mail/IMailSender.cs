using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tc.Psg.CloudFtpBridge.Mail
{
    public interface IMailSender
    {
        Task Send(string subject, string body, IEnumerable<string> toAddresses = null, string fromAddress = null);
    }
}
