using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentFTP;

namespace Tc.Psg.CloudFtpBridge.FileManagement.Ftp
{
    public class FtpFolder : IFolder
    {
        private FtpClient _ftpClient;
        private FtpListItem _ftpListItem;
        private string _rootDirectoryPath;

        public FtpFolder(FtpClient ftpClient)
        {
            _ftpClient = ftpClient;
        }

        public FtpFolder(FtpClient ftpClient, string rootDirectoryPath)
            : this(ftpClient)
        {
            _rootDirectoryPath = rootDirectoryPath;
        }

        public FtpFolder(FtpClient ftpClient, FtpListItem ftpListItem)
            : this(ftpClient)
        {
            _ftpListItem = ftpListItem;
        }

        public string Name
        {
            get
            {
                return _ftpListItem.Name;
            }
        }

        public async Task<IFile> CreateFile(string name)
        {
            string fullPath = await GetFullPath();

            if (!fullPath.EndsWith("/"))
            {
                fullPath += "/";
            }

            fullPath += name;

            return new FtpFile(_ftpClient, fullPath);
        }

        public async Task<string> GetFullPath()
        {
            return _ftpListItem?.FullName ?? _rootDirectoryPath ?? await _ftpClient.GetWorkingDirectoryAsync();
        }

        public async Task<IEnumerable<IFile>> ListFiles()
        {
            FtpListItem[] ftpListItems = await _GetFtpListItems(FtpFileSystemObjectType.File);

            List<FtpFile> files = new List<FtpFile>();

            foreach (FtpListItem ftpListItem in ftpListItems)
            {
                files.Add(new FtpFile(_ftpClient, ftpListItem));
            }

            return files;
        }

        public async Task<IEnumerable<IFolder>> ListFolders()
        {
            FtpListItem[] ftpListItems = await _GetFtpListItems(FtpFileSystemObjectType.Directory);

            List<FtpFolder> folders = new List<FtpFolder>();

            foreach (FtpListItem ftpListItem in ftpListItems)
            {
                folders.Add(new FtpFolder(_ftpClient, ftpListItem));
            }

            return folders;
        }

        private async Task<FtpListItem[]> _GetFtpListItems(FtpFileSystemObjectType type)
        {
            string fullPath = await GetFullPath();
            FtpListItem[] ftpListItems = await _ftpClient.GetListingAsync(fullPath);

            return ftpListItems.Where(x => x.Type == type).ToArray();
        }
    }
}
