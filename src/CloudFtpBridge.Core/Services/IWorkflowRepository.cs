using System.Collections.Generic;
using System.Threading.Tasks;

using CloudFtpBridge.Core.Models;

namespace CloudFtpBridge.Core.Services
{
    public interface IWorkflowRepository
    {
        Task Delete(string id);
        Task<Workflow> Get(string id);
        Task<IReadOnlyCollection<Workflow>> List();
        Task Save(Workflow workflow);
    }
}
