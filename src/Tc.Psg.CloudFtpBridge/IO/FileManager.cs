using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            _log.LogDebug("Begin workflow execution for: {WorkflowName}", workflow.Name);

            Server server = workflow.Server;
            FtpClient ftpClient = new FtpClient(server.Host, server.Port, server.Username, server.Password);

            if (_log.IsEnabled(LogLevel.Trace))
            {
                _log.LogTrace("Using {FtpUsername} / {FtpPassword} to connect to {FtpHost}:{FtpPort}.", server.Username, server.Password, server.Host, server.Port);
            }

            else
            {
                _log.LogDebug("Using {FtpUsername} to connect to {FtpHost}:{FtpPort}. Enable trace-level logging to see password.", server.Username, server.Host, server.Port);
            }

            IFolder localFolder = new LocalFolder(workflow.LocalPath);
            IFolder remoteFolder = new FtpFolder(ftpClient, PathUtil.CombineFragments(server.Path, workflow.RemotePath));

            if (workflow.Direction == WorkflowDirection.Inbound)
            {
                await _ProcessWorkflowFiles(workflow, remoteFolder, localFolder);
            }

            else if (workflow.Direction == WorkflowDirection.Outbound)
            {
                await _ProcessWorkflowFiles(workflow, localFolder, remoteFolder);
            }

            _log.LogDebug("Completed workflow execution for: {WorkflowName}", workflow.Name);
        }

        private string _GetTimestampedName(string name)
        {
            return $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_{name}";
        }

        private string _GetUnstampedName(string stampedName)
        {
            return stampedName.Substring(18);
        }

        private async Task _ProcessWorkflowFiles(Workflow workflow, IFolder sourceFolder, IFolder destinationFolder)
        {
            _log.LogDebug("Source Folder: {SourceFolderName}", sourceFolder.FullName);
            _log.LogDebug("Destination Folder: {DestinationFolderName}", destinationFolder.FullName);

            IFolder archiveFolder = await sourceFolder.CreateOrGetFolder(_ArchiveFolderName);
            IFolder failedFolder = await sourceFolder.CreateOrGetFolder(_FailedFolderName);
            IFolder processingFolder = await sourceFolder.CreateOrGetFolder(_ProcessingFolderName);
            IEnumerable<IFile> sourceFiles = await sourceFolder.GetFiles();
            IList<IFile> stagedFiles = new List<IFile>();

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
                stagedFiles.Add(await sourceFile.MoveTo(processingFolder, _GetTimestampedName(sourceFile.Name)));
            }

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
                    }

                    await stagedFile.MoveTo(archiveFolder);
                }

                catch (Exception ex)
                {
                    await stagedFile.MoveTo(failedFolder);

                    _log.LogError(ex, "Failed to process {SourceFileName}!", stagedFile.FullName);
                }
            }

            stagedFiles.Clear();
            stagedFiles = null;
        }
    }
}
