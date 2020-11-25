using System.Threading.Tasks;

namespace CloudFtpBridge.Core.Services
{
    public class NoopMailSender : IMailSender
    {
        public Task Send(string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}
