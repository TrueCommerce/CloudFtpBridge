using System;
using System.Collections.Generic;

using LiteDB;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Tc.Psg.CloudFtpBridge
{
    public class WorkflowRepository : IWorkflowRepository
    {
        private ILogger _log;

        public WorkflowRepository(ILogger<WorkflowRepository> logger, IOptions<CloudFtpBridgeOptions> optionsAccessor, IServerRepository serverRepository)
        {
            _log = logger;

            Options = optionsAccessor.Value;
            ServerRepository = serverRepository;
        }

        public CloudFtpBridgeOptions Options { get; private set; }
        public IServerRepository ServerRepository { get; private set; }

        public void Delete(Guid id)
        {
            using (LiteDatabase db = new LiteDatabase(Options.GetFullDatabaseFileName()))
            {
                LiteCollection<Workflow> workflowCollection = db.GetCollection<Workflow>();

                workflowCollection.Delete(x => x.Id.Equals(id));
            }

            _log.LogInformation("Deleted workflow: {WorkflowId}", id);
        }

        public Workflow Get(Guid id)
        {
            Workflow workflow;

            using (LiteDatabase db = new LiteDatabase(Options.GetFullDatabaseFileName()))
            {
                LiteCollection<Workflow> workflowCollection = db.GetCollection<Workflow>();

                workflow = workflowCollection.FindOne(x => x.Id.Equals(id)) ?? Workflow.Empty;
            }

            if (workflow != Workflow.Empty)
            {
                workflow.Server = ServerRepository.Get(workflow.ServerId);
            }

            return workflow;
        }

        public IEnumerable<Workflow> GetAll()
        {
            List<Workflow> workflows = new List<Workflow>();

            using (LiteDatabase db = new LiteDatabase(Options.GetFullDatabaseFileName()))
            {
                LiteCollection<Workflow> workflowCollection = db.GetCollection<Workflow>();

                workflows.AddRange(workflowCollection.FindAll());
            }

            foreach (Workflow workflow in workflows)
            {
                workflow.Server = ServerRepository.Get(workflow.ServerId);
            }

            return workflows;
        }

        public void Save(Workflow workflow)
        {
            using (LiteDatabase db = new LiteDatabase(Options.GetFullDatabaseFileName()))
            {
                LiteCollection<Workflow> workflowCollection = db.GetCollection<Workflow>();
                workflowCollection.EnsureIndex(x => x.Id);

                workflowCollection.Upsert(workflow);
            }

            _log.LogInformation("Saved workflow: {WorkflowName}", workflow.Name);
        }
    }
}
