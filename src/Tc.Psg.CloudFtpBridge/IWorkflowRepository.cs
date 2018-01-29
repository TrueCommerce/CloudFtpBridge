using System;
using System.Collections.Generic;

namespace Tc.Psg.CloudFtpBridge
{
    public interface IWorkflowRepository
    {
        void Delete(Guid id);
        Workflow Get(Guid id);
        IEnumerable<Workflow> GetAll();
        void Save(Workflow workflow);
    }
}
