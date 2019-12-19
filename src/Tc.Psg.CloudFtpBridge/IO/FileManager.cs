﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using FluentFTP;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using Tc.Psg.CloudFtpBridge.IO.Ftp;
using Tc.Psg.CloudFtpBridge.IO.Local;
using Tc.Psg.CloudFtpBridge.Utils;

namespace Tc.Psg.CloudFtpBridge.IO
{
    public class FileManager : IFileManager
    {
        private const string _ArchiveFolderName = "CloudFtpBridge_Archive";
        private const string _FailedFolderName = "CloudFtpBridge_Failed";
        private const string _ProcessingFolderName = "CloudFtpBridge_Processing";

        private ILogger _log;

        public FileManager()
        {
            _log = NullLogger.Instance;
        }

        public FileManager(ILogger<FileManager> logger)
            : this()
        {
            _log = logger;
        }

        public async Task ExecuteWorkflow(Workflow workflow)
        {
            _log.LogDebug("Begin Executing Workflow", workflow.Name);

            _log.LogDebug("Workflow Name: {WorkflowName}", workflow.Name);

            //Skip staging and download directly
            if (workflow.Direction == WorkflowDirection.InboundOptimized)
            {
                await ProcessInboundOptimizedWorkflowFiles(workflow);
            }
            else if (workflow.Direction == WorkflowDirection.InboundOptimizedNoArchive)
            {
                await ProcessInboundOptimizedWorkflowFiles(workflow, false);
            }
            else if (workflow.Direction == WorkflowDirection.OutboundOptimized)
            {
                await OutboundOptimized(workflow);
            }
            else if (workflow.Direction == WorkflowDirection.OutboundOptimizedNoArchive)
            {
                await OutboundOptimized(workflow,false);
            }
            else
            {
                await StageWorkflowFiles(workflow);
                await ProcessStagedWorkflowFiles(workflow);
            }

            _log.LogDebug("Finished Executing Workflow", workflow.Name);
        }

        //Copy of ProcessStagedWorkflowFiles/ removed stageworkflowfiles
        public async Task OutboundOptimized(Workflow workflow, bool archive =true)
        {
            _log.LogDebug("Begin Processing Workflow Files (OutboundOptimized)");

            _log.LogDebug("Workflow Name: {WorkflowName}", workflow.Name);
            IFolder sourceFolder = await _GetWorkflowFolder(workflow, FolderType.Source);
            IFolder destinationFolder = await _GetWorkflowFolder(workflow, FolderType.Destination);
            IFolder archiveFolder = await _GetWorkflowFolder(workflow, FolderType.Archive);
            IFolder failedFolder = await _GetWorkflowFolder(workflow, FolderType.Failed);

            IEnumerable<IFile> sourceFiles = await sourceFolder.GetFiles();

            foreach (IFile sourceFile in sourceFiles)
            {
                _log.LogDebug("Processing {SourceFileName}", sourceFile.FullName);

                try
                {
                    IFile destinationFile = await destinationFolder.CreateFile(sourceFile.Name);

                    using (Stream sourceStream = await sourceFile.GetReadStream())
                    using (Stream destinationStream = await destinationFile.GetWriteStream())
                    {
                        await sourceStream.CopyToAsync(destinationStream);
                        await destinationStream.FlushAsync();
                    }

                    if (archive)
                        await sourceFile.MoveTo(archiveFolder);
                    else
                        await sourceFile.Delete();
                }

                catch (Exception ex)
                {
                    await sourceFile.MoveTo(failedFolder);

                    _log.LogError(ex, "Failed to process {SourceFileName}!", sourceFile.FullName);
                }
            }

            _log.LogDebug("Finished Workflow Files (OutboundOptimized)");
        }

        public async Task ProcessInboundOptimizedWorkflowFiles(Workflow workflow, bool archive = true)
        {
            _log.LogDebug("Begin Processing (InboundOptimized)");

            _log.LogDebug("Workflow Name: {WorkflowName}", workflow.Name);

            IFolder sourceFolder = await _GetWorkflowFolder(workflow, FolderType.Source);
            IFolder destinationFolder = await _GetWorkflowFolder(workflow, FolderType.Destination);
            IFolder archiveFolder = await _GetWorkflowFolder(workflow, FolderType.Archive);

            IEnumerable<IFile> sourceFolderFiles = await sourceFolder.GetFiles();
            #region Filter with Regex
            if (!string.IsNullOrWhiteSpace(workflow.FileFilter))
            {
                int preFilterSourceFileCount = sourceFolderFiles.Count();

                Regex regex = new Regex(workflow.FileFilter, RegexOptions.Compiled);
                sourceFolderFiles = sourceFolderFiles.Where(x => regex.IsMatch(x.Name));

                int postFilterSourceFileCount = sourceFolderFiles.Count();

                _log.LogDebug("Found {PreFilterSourceFileCount} files in source folder. Filtered to {PostFilterSourceFileCount} files using regex pattern: {FilterPattern}", preFilterSourceFileCount, postFilterSourceFileCount, workflow.FileFilter);
            }

            else
            {
                _log.LogDebug("Found {FileCount} files in source folder.", sourceFolderFiles.Count());
            }
            #endregion
            foreach (IFile sourceFile in sourceFolderFiles)
            {
                _log.LogDebug("Processing {SourceFileName} (InboundOptimized)", sourceFile.FullName);

                try
                {
                    IFile destinationFile = await destinationFolder.CreateFile(sourceFile.Name);

                    using (Stream sourceStream = await sourceFile.GetReadStream())
                    using (Stream destinationStream = await destinationFile.GetWriteStream())
                    {
                        await sourceStream.CopyToAsync(destinationStream);
                        await destinationStream.FlushAsync();
                    }

                    if (archive)
                        await sourceFile.MoveTo(archiveFolder);
                    else
                        await sourceFile.Delete();
                }

                catch (Exception ex)
                {
                    _log.LogError(ex, "Failed to process {SourceFileName} (InboundOptimized)!", sourceFile.FullName);
                }
            }

            _log.LogDebug("Finished Processing Workflow Files (InboundOptimized)");
        }

        public async Task ProcessStagedWorkflowFiles(Workflow workflow)
        {
            _log.LogDebug("Begin Processing Staged Workflow Files");

            _log.LogDebug("Workflow Name: {WorkflowName}", workflow.Name);

            IFolder stagingFolder = await _GetWorkflowFolder(workflow, FolderType.Staging);
            IFolder destinationFolder = await _GetWorkflowFolder(workflow, FolderType.Destination);
            IFolder archiveFolder = await _GetWorkflowFolder(workflow, FolderType.Archive);
            IFolder failedFolder = await _GetWorkflowFolder(workflow, FolderType.Failed);

            IEnumerable<IFile> stagedFiles = await stagingFolder.GetFiles();

            foreach (IFile stagedFile in stagedFiles)
            {
                _log.LogDebug("Processing {SourceFileName}", stagedFile.FullName);

                try
                {
                    IFile destinationFile = await destinationFolder.CreateFile(_GetUnstampedName(stagedFile.Name));

                    using (Stream sourceStream = await stagedFile.GetReadStream())
                    using (Stream destinationStream = await destinationFile.GetWriteStream())
                    {
                        await sourceStream.CopyToAsync(destinationStream);
                        await destinationStream.FlushAsync();
                    }

                    await stagedFile.MoveTo(archiveFolder);
                }

                catch (Exception ex)
                {
                    await stagedFile.MoveTo(failedFolder);

                    _log.LogError(ex, "Failed to process {SourceFileName}!", stagedFile.FullName);
                }
            }

            _log.LogDebug("Finished Processing Staged Workflow Files");
        }

        public async Task StageWorkflowFiles(Workflow workflow)
        {
            _log.LogDebug("Begin Staging Workflow Files");

            _log.LogDebug("Workflow Name: {WorkflowName}", workflow.Name);

            IFolder sourceFolder = await _GetWorkflowFolder(workflow, FolderType.Source);
            IFolder processingFolder = await _GetWorkflowFolder(workflow, FolderType.Staging);
            IEnumerable<IFile> sourceFiles = await sourceFolder.GetFiles();

            if (!string.IsNullOrWhiteSpace(workflow.FileFilter))
            {
                int preFilterSourceFileCount = sourceFiles.Count();

                Regex regex = new Regex(workflow.FileFilter, RegexOptions.Compiled);
                sourceFiles = sourceFiles.Where(x => regex.IsMatch(x.Name));

                int postFilterSourceFileCount = sourceFiles.Count();

                _log.LogDebug("Found {PreFilterSourceFileCount} files in source folder. Filtered to {PostFilterSourceFileCount} files using regex pattern: {FilterPattern}", preFilterSourceFileCount, postFilterSourceFileCount, workflow.FileFilter);
            }

            else
            {
                _log.LogDebug("Found {FileCount} files in source folder.", sourceFiles.Count());
            }

            foreach (IFile sourceFile in sourceFiles)
            {
                _log.LogDebug("Staging {SourceFileName}", sourceFile.FullName);

                try
                {
                    await sourceFile.MoveTo(processingFolder, _GetTimestampedName(sourceFile.Name));
                }

                catch (Exception ex)
                {
                    _log.LogError(ex, "Failed to stage {SourceFileName}!", sourceFile.FullName);
                }
            }

            _log.LogDebug("Finished Staging Workflow Files");
        }

        private string _GetTimestampedName(string name)
        {
            return $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_{name}";
        }

        private string _GetUnstampedName(string stampedName)
        {
            return stampedName.Substring(18);
        }

        private async Task<IFolder> _GetWorkflowFolder(Workflow workflow, FolderType type, bool suppressLogging = false)
        {
            IFolder folder;
            string typeName = Enum.GetName(typeof(FolderType), type);

            if (type == FolderType.Archive)
            {
                IFolder sourceFolder = await _GetWorkflowFolder(workflow, FolderType.Source, true);

                folder = await sourceFolder.CreateOrGetFolder($"{_ArchiveFolderName}_{workflow.Id}");
            }

            else if (type == FolderType.Failed)
            {
                IFolder sourceFolder = await _GetWorkflowFolder(workflow, FolderType.Source, true);

                folder = await sourceFolder.CreateOrGetFolder($"{_FailedFolderName}_{workflow.Id}");
            }

            else if (type == FolderType.Staging)
            {
                IFolder sourceFolder = await _GetWorkflowFolder(workflow, FolderType.Source, true);

                folder = await sourceFolder.CreateOrGetFolder($"{_ProcessingFolderName}_{workflow.Id}");
            }

            else if ((type == FolderType.Source && workflow.Direction == WorkflowDirection.Inbound) || (type == FolderType.Source && workflow.Direction == WorkflowDirection.InboundOptimized) || (type == FolderType.Source && workflow.Direction == WorkflowDirection.InboundOptimizedNoArchive) || (type == FolderType.Destination && workflow.Direction == WorkflowDirection.Outbound) || (type == FolderType.Destination && workflow.Direction == WorkflowDirection.OutboundOptimized) || (type == FolderType.Destination && workflow.Direction == WorkflowDirection.OutboundOptimizedNoArchive))
            {
                Server server = workflow.Server;
                FtpDataConnectionType tempType;
                FtpClient ftpClient = new FtpClient(server.Host, server.Port, server.Username, server.Password);
                ftpClient.ConnectTimeout = 900000;
                ftpClient.DataConnectionConnectTimeout = 900000;
                ftpClient.DataConnectionReadTimeout = 900000;

                if (_log.IsEnabled(LogLevel.Trace))
                {
                    _log.LogTrace("Using {FtpUsername} / {FtpPassword} to connect to {FtpHost}:{FtpPort}.", server.Username, server.Password, server.Host, server.Port);
                }

                else
                {
                    _log.LogDebug("Using {FtpUsername} to connect to {FtpHost}:{FtpPort}. Enable trace-level logging to see password.", server.Username, server.Host, server.Port);
                }

                if (server.DataConnectionType == "Default")
                {
                    server.DataConnectionType = "AutoPassive";
                }
                //Set DataConnectionType FTP and FTPS
                Enum.TryParse(server.DataConnectionType, out tempType);
                ftpClient.DataConnectionType = tempType; 

                if (server.FtpsEnabled && Enum.TryParse(server.EncryptionMode, out FtpEncryptionMode ftpEncryptionMode))
                {
                    ftpClient.EncryptionMode = ftpEncryptionMode;
                    ftpClient.ValidateCertificate += new FtpSslValidation(OnValidateCertificate);
                    ftpClient.SslProtocols = SslProtocols.Default;
                    
                    _log.LogDebug("FTPS is enabled. Encryption mode is set to {EncryptionMode}. DataConnectionType is {DataConnectionType}", server.EncryptionMode, server.DataConnectionType);
                }

                folder = new FtpFolder(ftpClient, PathUtil.CombineFragments(server.Path, workflow.RemotePath));
            }

            else
            {
                folder = new LocalFolder(workflow.LocalPath);
            }

            if (!suppressLogging)
            {
                _log.LogDebug(string.Concat(typeName, " Folder: {", typeName, "folderName}"), folder.FullName);
            }

            return folder;
        }

        private void OnValidateCertificate(FtpClient control, FtpSslValidationEventArgs e)
        {
            e.Accept = true;
        }
    }
}
