using System;
using System.Collections.Generic;
using System.Linq;

using LiteDB;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Tc.Psg.CloudFtpBridge
{
    public class ServerRepository : IServerRepository
    {
        private ILogger _log;

        public ServerRepository(ILogger<ServerRepository> logger, IOptions<CloudFtpBridgeOptions> optionsAccessor)
        {
            _log = logger;

            Options = optionsAccessor.Value;
        }

        public CloudFtpBridgeOptions Options { get; private set; }

        public bool CanDelete(Guid id)
        {
            bool canDelete;

            using (LiteDatabase db = new LiteDatabase(Options.GetFullDatabaseFileName()))
            {
                LiteCollection<Workflow> serverCollection = db.GetCollection<Workflow>();

                canDelete = (serverCollection.FindOne(x => x.ServerId.Equals(id)) == null);
            }

            return canDelete;
        }

        public void Delete(Guid id)
        {
            using (LiteDatabase db = new LiteDatabase(Options.GetFullDatabaseFileName()))
            {
                LiteCollection<Server> serverCollection = db.GetCollection<Server>();

                serverCollection.Delete(x => x.Id.Equals(id));
            }

            _log.LogInformation("Deleted server: {ServerId}", id);
        }

        public Server Get(Guid id)
        {
            Server server;

            using (LiteDatabase db = new LiteDatabase(Options.GetFullDatabaseFileName()))
            {
                LiteCollection<Server> serverCollection = db.GetCollection<Server>();

                server = serverCollection.FindOne(x => x.Id.Equals(id)) ?? Server.Empty;
            }

            return server;
        }

        public IEnumerable<Server> GetAll()
        {
            IEnumerable<Server> servers;

            using (LiteDatabase db = new LiteDatabase(Options.GetFullDatabaseFileName()))
            {
                LiteCollection<Server> serverCollection = db.GetCollection<Server>();

                servers = serverCollection.FindAll();
            }

            return servers.ToArray();
        }

        public void Save(Server server)
        {
            using (LiteDatabase db = new LiteDatabase(Options.GetFullDatabaseFileName()))
            {
                LiteCollection<Server> serverCollection = db.GetCollection<Server>();
                serverCollection.EnsureIndex(x => x.Id);

                serverCollection.Upsert(server);
            }

            _log.LogInformation("Saved server: {ServerName}", server.Name);
        }
    }
}
