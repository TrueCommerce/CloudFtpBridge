using System.Threading.Tasks;

using CloudFtpBridge.Core.Models;

namespace CloudFtpBridge.Core.Services
{
    public interface IMailSettingsProvider
    {
        Task<MailSettings> Get();
        Task Save(MailSettings mailSettings);
    }
}
