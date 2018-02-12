using System;
using System.Collections.Generic;

namespace Tc.Psg.CloudFtpBridge
{
    public interface IServerRepository
    {
        bool CanDelete(Guid id);
        void Delete(Guid id);
        Server Get(Guid id);
        IEnumerable<Server> GetAll();
        void Save(Server server);
    }
}
