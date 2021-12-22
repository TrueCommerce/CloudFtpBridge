using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using CloudFtpBridge.Core.Models;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CloudFtpBridge.Core.Services
{
    public class WorkflowRunner
    {
        private readonly FileSystemActivator _fileSystemActivator;
        private readonly IAuditLog _auditLog;
        private readonly IOptionsMonitor<CoreOptions> _coreOptions;
        private readonly IMailSender _mailSender;
        private readonly ILogger _logger;

        public WorkflowRunner(
            FileSystemActivator fileSystemActivator,
            IAuditLog auditLog,
            IOptionsMonitor<CoreOptions> coreOptions,
            IMailSender mailSender,
            ILogger<WorkflowRunner> logger)
        {
            _fileSystemActivator = fileSystemActivator;
            _auditLog = auditLog;
            _coreOptions = coreOptions;
            _mailSender = mailSender;
            _logger = logger;
        }

        /// <summary>
        /// Runs the provided workflow by activating the configured file systems and transferring files from source to destination.
        /// Returns "true" if there were files added to the source file system after the initial file listing was taken.
        /// Returns "false" if the source file system is empty or contains files that failed to transfer properly.
        /// This means if this method returns "true", it can be immediatelly called again to continue processing files from the source file system.
        /// </summary>
        public async Task<bool> Run(Workflow workflow)
        {
            var sourceFileSystem = _fileSystemActivator.Activate(workflow.SourceFileSystemType, workflow.SourceFileSystemConfig);
            var destinationFileSystem = _fileSystemActivator.Activate(workflow.DestinationFileSystemType, workflow.DestinationFileSystemConfig);

            var sourceFiles = await sourceFileSystem.List();

            var hasErrors = false;

            _logger.LogDebug("Found {SourceFileCount} files.", sourceFiles.Count);

            if (!string.IsNullOrWhiteSpace(workflow.SourceFileFilter))
            {
                _logger.LogDebug("Filtering files with: {SourceFileFilter}", workflow.SourceFileFilter);

                var regex = new Regex(workflow.SourceFileFilter);

                sourceFiles = sourceFiles.Where(fr => regex.IsMatch(fr.Name)).ToArray();

                _logger.LogDebug("Found {SourceFileCount} files after filtering.", sourceFiles.Count);
            }

            foreach (var sourceFile in sourceFiles)
            {
                _logger.LogDebug("Processing {FileName}", sourceFile.Name);

                await _auditLog.AddEntry(workflow, sourceFile, FileStage.TransferStarted);

                if (workflow.EnforceUniqueFileNames)
                {
                    await _auditLog.AddEntry(workflow, sourceFile, FileStage.EnforceUniqueNameStarted);

                    try
                    {
                        var newFileName = $"{Path.GetFileNameWithoutExtension(sourceFile.Name)}_{Guid.NewGuid()}{Path.GetExtension(sourceFile.Name)}";

                        await sourceFileSystem.Rename(sourceFile.Name, newFileName, overwriteExisting: false);

                        sourceFile.Name = newFileName;

                        await _auditLog.AddEntry(workflow, sourceFile, FileStage.EnforceUniqueNameCompleted);
                    }

                    catch (Exception ex)
                    {
                        hasErrors = true;

                        await _auditLog.AddEntry(workflow, sourceFile, FileStage.EnforceUniqueNameFailed, ex.Message);

                        continue;
                    }
                }

                await _auditLog.AddEntry(workflow, sourceFile, FileStage.ReadStarted);

                Stream sourceStream = null;
                string refData = string.Empty;

                try
                {
                    
                    sourceStream = await sourceFileSystem.Read(sourceFile.Name);
                    refData = GetReference(sourceFile.Name, sourceStream, workflow.SourceRefXPathRegex);

                    await _auditLog.AddEntry(workflow, sourceFile, FileStage.ReadCompleted, null, refData);
                }

                catch (Exception ex)
                {
                    hasErrors = true;

                    _logger.LogError(ex, "Failed to read {FileName}", sourceFile.Name);

                    await _auditLog.AddEntry(workflow, sourceFile, FileStage.ReadFailed, ex.Message);

                    sourceStream?.Dispose();
                    sourceStream = null;

                    await _mailSender.Send("Cloud FTP Bridge: File Read Failure", $"<h3>Workflow</h3><p>{workflow.Name}</p><h3>File Name</h3><p>{sourceFile.Name}</p><h3>Error Message</h3><p>{ex.Message}</p>");

                    continue;
                }

                await _auditLog.AddEntry(workflow, sourceFile, FileStage.WriteStarted,null,refData);

                try
                {
                    await destinationFileSystem.Write(sourceFile.Name, sourceStream);

                    await _auditLog.AddEntry(workflow, sourceFile, FileStage.WriteCompleted, null, refData);
                }

                catch (Exception ex)
                {
                    hasErrors = true;

                    _logger.LogError(ex, "Failed to write {FileName}", sourceFile.Name);

                    await _auditLog.AddEntry(workflow, sourceFile, FileStage.WriteFailed, ex.Message,refData);

                    sourceStream?.Dispose();
                    sourceStream = null;

                    await _mailSender.Send("Cloud FTP Bridge: File Write Failure", $"<h3>Workflow</h3><p>{workflow.Name}</p><h3>File Name</h3><p>{sourceFile.Name}</p><h3>Error Message</h3><p>{ex.Message}</p>");

                    continue;
                }

                // we do this explicitly to ensure we aren't holding a handle when trying to delete the source if the underlying stream is directly tied to the source file
                sourceStream?.Dispose();
                sourceStream = null;

                await _auditLog.AddEntry(workflow, sourceFile, FileStage.DeleteSourceStarted, null, refData);

                try
                {
                    await sourceFileSystem.Delete(sourceFile.Name);

                    await _auditLog.AddEntry(workflow, sourceFile, FileStage.DeleteSourceCompleted, null, refData);
                }

                catch (Exception ex)
                {
                    hasErrors = true;

                    _logger.LogError(ex, "Failed to delete {FileName} at source. This may cause duplicates if the file is processed again on the next run.", sourceFile.Name);

                    await _auditLog.AddEntry(workflow, sourceFile, FileStage.DeleteSourceFailed, ex.Message, refData);
                    await _mailSender.Send("Cloud FTP Bridge: File Delete Failure", $"<p style=\"color:red;\"><strong>WARNING:</strong>&nbsp;This error may result in the referenced file being processed more than once. Please audit your transactions as soon as possible.</p><h3>Workflow</h3><p>{workflow.Name}</p><h3>File Name</h3><p>{sourceFile.Name}</p><h3>Error Message</h3><p>{ex.Message}</p>");
                }

                await _auditLog.AddEntry(workflow, sourceFile, FileStage.TransferCompleted, null, refData);
            }

            await _auditLog.Cleanup(DateTimeOffset.UtcNow.Subtract(_coreOptions.CurrentValue.AuditLogRetentionLimit));

            var hasFiles = await sourceFileSystem.HasFiles();

            sourceFiles = null;
            sourceFileSystem = null;
            destinationFileSystem = null;

            if (_coreOptions.CurrentValue.ForceGarbageCollection)
            {
                var currentProcess = Process.GetCurrentProcess();
                var currentMemory = currentProcess.PrivateMemorySize64;

                _logger.LogDebug("Starting garbage collection to ensure dereferenced streams are cleaned up. [Private Memory: {PrivateMemorySize}]", currentMemory);

                GC.Collect();

                currentProcess = Process.GetCurrentProcess();
                currentMemory = currentProcess.PrivateMemorySize64;

                _logger.LogDebug("Garbage collection completed. [Private Memory: {Private Memory Size}]", currentMemory);
            }

            return !hasErrors && hasFiles;
        }

        private string GetReference(string fileName, Stream inData, string regexOrXpath)
        {
            string ret = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(fileName) || inData == null || string.IsNullOrEmpty(regexOrXpath))
                    return string.Empty;

                StreamReader reader = new StreamReader(inData);
                string data = reader.ReadToEnd();
                inData.Seek(0, SeekOrigin.Begin);

                string ext = Path.GetExtension(fileName);

                if (ext?.ToUpper() == ".XML")
                {
                    if (!string.IsNullOrEmpty(data))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(data);

                        if (regexOrXpath.Contains("|"))//if multiple xpaths
                        {
                            foreach (var xpath in regexOrXpath.Split('|'))
                            {
                                ret = doc.SelectSingleNode(xpath)?.InnerText ?? string.Empty;
                                if (!string.IsNullOrEmpty(ret))
                                    break;
                            }
                        }
                        else //if single xpath
                        {
                            ret = doc.SelectSingleNode(regexOrXpath)?.InnerText ?? string.Empty;
                        }
                    }
                }
                else
                {
                    if (Regex.Matches(data, regexOrXpath).Count > 0)
                        ret = Regex.Matches(data,regexOrXpath)[0].Value ?? string.Empty;
                }
            }
            catch (Exception)
            {
                
            }
            return ret;
        }
    }
}
