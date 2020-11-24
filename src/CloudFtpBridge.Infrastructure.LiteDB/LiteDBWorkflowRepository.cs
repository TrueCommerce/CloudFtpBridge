using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LiteDB;
using Microsoft.Extensions.Options;

using CloudFtpBridge.Core.Models;
using CloudFtpBridge.Core.Services;

namespace CloudFtpBridge.Infrastructure.LiteDB
{
    public class LiteDBWorkflowRepository : IWorkflowRepository
    {
        private readonly ILiteCollection<Workflow> _workflowCollection;

        public LiteDBWorkflowRepository(IOptionsMonitor<LiteDBOptions> liteDBOptions)
        {
            var db = new LiteDatabase(liteDBOptions.CurrentValue.WorkflowDbConnectionString);

            _workflowCollection = db.GetCollection<Workflow>();
        }

        public Task Delete(string id)
        {
            _workflowCollection.Delete(id);

            return Task.CompletedTask;
        }

        public Task<Workflow> Get(string id)
        {
            return Task.FromResult(_workflowCollection.FindOne(w => w.Id == id));
        }

        public Task<IReadOnlyCollection<Workflow>> List()
        {
            var workflows = _workflowCollection.FindAll().ToArray();

            return Task.FromResult(workflows as IReadOnlyCollection<Workflow>);
        }

        public Task Save(Workflow workflow)
        {
            _workflowCollection.Upsert(workflow.Id, workflow);

            return Task.CompletedTask;
        }
    }
}
