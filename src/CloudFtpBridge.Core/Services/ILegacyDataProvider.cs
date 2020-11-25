using System.Collections.Generic;
using System.Threading.Tasks;

using CloudFtpBridge.Core.Models;

namespace CloudFtpBridge.Core.Services
{
    public interface ILegacyDataProvider
    {
        Task<MailSettings> GetLegacyMailSettings();
        Task<IReadOnlyCollection<Workflow>> GetLegacyWorkflows();
    }
}
