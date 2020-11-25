using System.Threading.Tasks;

namespace CloudFtpBridge.Core.Services
{
    public interface IMailSender
    {
        Task Send(string subject, string message);
    }
}
