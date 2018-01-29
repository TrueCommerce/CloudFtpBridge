using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentFTP;

using Tc.Psg.CloudFtpBridge.Utils;

namespace Tc.Psg.CloudFtpBridge.IO.Ftp
{
    public class FtpFolder : IFolder
    {
        public FtpFolder(FtpClient ftpClient, string fullName)
        {
            BaseFtpClient = ftpClient;
            FullName = fullName;
        }

        public FtpClient BaseFtpClient { get; private set; }
        public string FullName { get; private set; }

        public async Task<IFile> CreateFile(string name)
        {
            return new FtpFile(BaseFtpClient, this, PathUtil.CombineFragments(FullName, name));
        }

        public async Task<IFolder> CreateOrGetFolder(string name)
        {
            string fullPath = PathUtil.CombineFragments(FullName, name);

            bool exists = await BaseFtpClient.DirectoryExistsAsync(fullPath);

            if (!exists)
            {
                await BaseFtpClient.CreateDirectoryAsync(fullPath);
            }

            return new FtpFolder(BaseFtpClient, fullPath);
        }

        public async Task<IEnumerable<IFile>> GetFiles()
        {
            FtpListItem[] ftpListItems = await BaseFtpClient.GetListingAsync(FullName);
            ftpListItems = ftpListItems.Where(x => x.Type == FtpFileSystemObjectType.File).ToArray();

            List<IFile> files = new List<IFile>();

            foreach (FtpListItem ftpListItem in ftpListItems)
            {
                files.Add(new FtpFile(BaseFtpClient, this, ftpListItem.FullName));
            }

            return files;
        }

        public async Task<IEnumerable<IFolder>> GetFolders()
        {
            FtpListItem[] ftpListItems = await BaseFtpClient.GetListingAsync(FullName);
            ftpListItems = ftpListItems.Where(x => x.Type == FtpFileSystemObjectType.Directory).ToArray();

            List<IFolder> folders = new List<IFolder>();

            foreach (FtpListItem ftpListItem in ftpListItems)
            {
                folders.Add(new FtpFolder(BaseFtpClient, ftpListItem.FullName));
            }

            return folders;
        }
    }
}
