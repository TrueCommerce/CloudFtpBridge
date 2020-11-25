using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using LiteDB;

using CloudFtpBridge.Core.Models;
using CloudFtpBridge.Core.Services;
using CloudFtpBridge.Core.Utils;

namespace CloudFtpBridge.Infrastructure.LiteDB
{
    public class LiteDBLegacyDataProvider : ILegacyDataProvider
    {
        private const string _FluentFTPConfigPrefix = "CloudFtpBridge:Infrastructure:FluentFTP";
        private const string _LocalFileSystemPrefix = "CloudFtpBridge:Infrastructure:LocalFileSystem";

        private readonly ILiteDatabase _legacyDatabase = null;

        public LiteDBLegacyDataProvider()
        {
            var legacyDbFileName = Path.Combine(PathHelper.GetLegacyStoragePath(), "CloudFtpBridge.db");

            if (File.Exists(legacyDbFileName))
            {
                try
                {
                    File.Copy(legacyDbFileName, string.Concat(legacyDbFileName, $".backup"));
                }

                catch { }

                try
                {

                    _legacyDatabase = new LiteDatabase($"filename={legacyDbFileName};upgrade=true;");
                }

                catch { }
            }
        }

        public Task<MailSettings> GetLegacyMailSettings()
        {
            if (_legacyDatabase is null || !_legacyDatabase.CollectionExists("MailOptions"))
            {
                return Task.FromResult(new MailSettings());
            }

            var legacyMailOptions = _legacyDatabase.GetCollection("MailOptions").FindOne(o => true);

            if (legacyMailOptions is null)
            {
                return Task.FromResult(new MailSettings());
            }

            var mailSettings = new MailSettings
            {
                Enabled = false,
                FromAddress = legacyMailOptions.RawValue["FromAddress"]?.AsString ?? string.Empty,
                Host = legacyMailOptions.RawValue["SmtpHost"]?.AsString ?? string.Empty,
                Password = legacyMailOptions.RawValue["SmtpPassword"]?.AsString ?? string.Empty,
                Port = legacyMailOptions.RawValue["SmtpPort"]?.AsInt32 ?? 21,
                ToAddresses = legacyMailOptions.RawValue["ToAddresses"].AsArray.Select(d => d.AsString).ToList(),
                Username = legacyMailOptions.RawValue["SmtpUsername"]?.AsString ?? string.Empty
            };

            return Task.FromResult(mailSettings);
        }

        public Task<IReadOnlyCollection<Workflow>> GetLegacyWorkflows()
        {
            if (_legacyDatabase is null)
            {
                return Task.FromResult<IReadOnlyCollection<Workflow>>(Array.Empty<Workflow>());
            }

            List<Workflow> workflows = new List<Workflow>();

            var serverCollection = _legacyDatabase.GetCollection("Server");
            var workflowCollection = _legacyDatabase.GetCollection("Workflow");

            foreach (var legacyWorkflow in workflowCollection.FindAll())
            {
                var legacyServer = serverCollection.FindById(legacyWorkflow.RawValue["ServerId"].AsGuid);

                var fileSystemConfiguration = new Dictionary<string, string>
                {
                    { $"{_FluentFTPConfigPrefix}:AutoConnect", "False" },
                    { $"{_FluentFTPConfigPrefix}:DataConnectionType", legacyServer.RawValue["DataConnectionType"]?.AsString },
                    { $"{_FluentFTPConfigPrefix}:EncryptionMode", legacyServer.RawValue["EncryptionMode"]?.AsString },
                    { $"{_FluentFTPConfigPrefix}:Host", legacyServer.RawValue["Host"]?.AsString },
                    { $"{_FluentFTPConfigPrefix}:Password", legacyServer.RawValue["Password"]?.AsString },
                    { $"{_FluentFTPConfigPrefix}:Path", PathHelper.Combine(legacyServer.RawValue["Path"]?.AsString, legacyWorkflow.RawValue["RemotePath"]?.AsString) },
                    { $"{_FluentFTPConfigPrefix}:Port", legacyServer.RawValue["Port"]?.AsInt32.ToString() },
                    { $"{_FluentFTPConfigPrefix}:Username", legacyServer.RawValue["Username"]?.AsString },
                    { $"{_LocalFileSystemPrefix}:Path", legacyWorkflow.RawValue["LocalPath"]?.AsString }
                };

                workflows.Add(new Workflow
                {
                    Id = legacyWorkflow.RawValue["_id"].AsGuid.ToString(),
                    Name = legacyWorkflow.RawValue["Name"].AsString,
                    SourceFileSystemType = legacyWorkflow.RawValue["Direction"].AsString.Contains("Inbound") ? "CloudFtpBridge.Infrastructure.FluentFTP.FluentFTPFileSystem" : "CloudFtpBridge.Infrastructure.LocalFileSystem.LocalFileSystem",
                    SourceFileSystemConfig = fileSystemConfiguration,
                    SourceFileFilter = legacyWorkflow.RawValue["FileFilter"]?.AsString,
                    DestinationFileSystemType = legacyWorkflow.RawValue["Direction"].AsString.Contains("Outbound") ? "CloudFtpBridge.Infrastructure.FluentFTP.FluentFTPFileSystem" : "CloudFtpBridge.Infrastructure.LocalFileSystem.LocalFileSystem",
                    DestinationFileSystemConfig = fileSystemConfiguration,
                    Enabled = false
                });
            }

            return Task.FromResult<IReadOnlyCollection<Workflow>>(workflows);
        }
    }
}
