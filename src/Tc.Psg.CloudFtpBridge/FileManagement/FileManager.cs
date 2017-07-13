using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using FluentFTP;
using Serilog;

using Tc.Psg.CloudFtpBridge.Configuration;
using Tc.Psg.CloudFtpBridge.FileManagement.Ftp;
using Tc.Psg.CloudFtpBridge.FileManagement.Local;

namespace Tc.Psg.CloudFtpBridge.FileManagement
{
    public class FileManager
    {
        private CompanyConfig _companyConfig;
        private FtpClient _ftpClient;
        private ILogger _log;

        public FileManager(CompanyConfig companyConfig)
        {
            _companyConfig = companyConfig;
            _ftpClient = new FtpClient(companyConfig.CloudFtpHost, companyConfig.CloudFtpPort, companyConfig.CloudFtpUser, companyConfig.CloudFtpPassword);
            _log = Log.ForContext<FileManager>();
        }

        public async Task<IFolder> GetLocalFolder(FileDirection direction)
        {
            _log.Debug("GetLocalFolder() BEGIN");
            _log.Debug("File Direction: {FileDirection}", direction);

            IFolder folder = null;

            if (direction == FileDirection.Inbound)
            {
                folder = new LocalFolder(_companyConfig.LocalInboundPath);
            }

            else if (direction == FileDirection.Outbound)
            {
                folder = new LocalFolder(_companyConfig.LocalOutboundPath);
            }

            _log.Debug("GetLocalFolder() END");

            return folder;
        }

        public async Task<IFolder> GetRemoteFolder(FileDirection direction)
        {
            _log.Debug("GetRemoteFolder() BEGIN");
            _log.Debug("File Direction: {FileDirection}", direction);

            IFolder remoteFolder = null;
            IFolder rootFtpFolder = new FtpFolder(_ftpClient);
            string importExport = (direction == FileDirection.Inbound) ? "Export" : "Import";

            try
            {
                remoteFolder = await ResolveRelativeFolderPath(rootFtpFolder,
                    $"/{_companyConfig.EdiId}/IntegrationFTP/{importExport}/{_companyConfig.Name}/Transaction");
            }

            catch (Exception ex)
            {
                DirectoryNotFoundException outerException = new DirectoryNotFoundException("The remote folder could not be resolved. The remote folder structure may be incorrect.", ex);

                _log.Error(ex, ex.Message);

                throw outerException;
            }

            _log.Debug("GetRemoteFolder() END");

            return remoteFolder;
        }

        public async Task ProcessInboundFiles()
        {
            _log.Debug("ProcessInboundFiles() BEGIN");

            IFolder localFolder = await GetLocalFolder(FileDirection.Inbound);
            IFolder remoteFolder = await GetRemoteFolder(FileDirection.Inbound);

            string localFolderPath = await localFolder.GetFullPath();
            string remoteFolderPath = await remoteFolder.GetFullPath();

            _log.Debug("Local Inbound Path: {LocalInboundPath}", localFolderPath);
            _log.Debug("Remote Inbound Path: {RemoteInboundPath}", remoteFolderPath);

            await TransferAllFiles(remoteFolder, localFolder, true);

            _log.Debug("ProcessInboundFiles() END");
        }

        public async Task ProcessOutboundFiles()
        {
            _log.Debug("ProcessOutboundFiles() BEGIN");

            IFolder localFolder = await GetLocalFolder(FileDirection.Outbound);
            IFolder remoteFolder = await GetRemoteFolder(FileDirection.Outbound);

            string localFolderPath = await localFolder.GetFullPath();
            string remoteFolderPath = await remoteFolder.GetFullPath();

            _log.Debug("Local Outbound Path: {LocalOutboundPath}", localFolderPath);
            _log.Debug("Remote Outbound Path: {RemoteOutboundPath}", remoteFolderPath);

            await TransferAllFiles(localFolder, remoteFolder, true);

            _log.Debug("ProcessOutboundFiles() END");
        }

        public async Task<IFolder> ResolveRelativeFolderPath(IFolder rootFolder, string path)
        {
            path = path ?? "/";
            path = path.Replace(@"\", "/");

            string[] segments = path.Split('/');
            IFolder parent = rootFolder;

            foreach (string segment in segments.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                IFolder child = (await parent.ListFolders()).FirstOrDefault(x => x.Name.Equals(segment, StringComparison.OrdinalIgnoreCase));
                parent = child ?? throw new DirectoryNotFoundException("Unable to resolve the relative folder path.");
            }

            return parent;
        }

        public async Task TransferAllFiles(IFolder sourceFolder, IFolder destinationFolder, bool deleteSourceFiles)
        {
            _log.Debug("TransferAllFiles() BEGIN");

            IEnumerable<IFile> sourceFiles = await sourceFolder.ListFiles();

            foreach (IFile sourceFile in sourceFiles)
            {
                await TransferFile(sourceFile, destinationFolder, deleteSourceFiles);
            }

            _log.Debug("TransferAllFiles() END");
        }

        public async Task TransferFile(IFile sourceFile, IFolder destinationFolder, bool deleteSourceFile)
        {
            _log.Debug("TransferFile() BEGIN");

            string sourceFilePath = await sourceFile.GetFullPath();

            _log.Debug("Source File Path: {SourceFilePath}", sourceFilePath);

            using (Stream sourceStream = await sourceFile.GetReadStream())
            using (IFile destinationFile = await destinationFolder.CreateFile(sourceFile.Name))
            {
                string destinationFilePath = await destinationFile.GetFullPath();

                _log.Debug("Destination File Path: {DestinationFilePath}", destinationFilePath);

                await destinationFile.Write(sourceStream);
            }

            if (deleteSourceFile)
            {
                await sourceFile.Delete();
                _log.Debug("Source file deleted.");
            }

            _log.Debug("TransferFile() END");
        }
    }
}
