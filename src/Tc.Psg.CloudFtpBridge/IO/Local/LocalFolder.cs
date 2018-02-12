using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Tc.Psg.CloudFtpBridge.IO.Local
{
    public class LocalFolder : IFolder
    {
        public LocalFolder(string fullName)
        {
            BaseDirectoryInfo = new DirectoryInfo(fullName);
        }

        public DirectoryInfo BaseDirectoryInfo { get; private set; }
        public string FullName { get { return BaseDirectoryInfo.FullName; } }

        public async Task<IFile> CreateFile(string name)
        {
            return new LocalFile(this, Path.Combine(FullName, name));
        }

        public async Task<IFolder> CreateOrGetFolder(string name)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(FullName, name));

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            return new LocalFolder(directoryInfo.FullName);
        }

        public async Task<IEnumerable<IFile>> GetFiles()
        {
            FileInfo[] fileInfos = BaseDirectoryInfo.GetFiles();
            List<IFile> files = new List<IFile>();

            foreach (FileInfo fileInfo in fileInfos)
            {
                files.Add(new LocalFile(this, fileInfo.FullName));
            }

            return files;
        }

        public async Task<IEnumerable<IFolder>> GetFolders()
        {
            DirectoryInfo[] directoryInfos = BaseDirectoryInfo.GetDirectories();
            List<IFolder> folders = new List<IFolder>();

            foreach (DirectoryInfo directoryInfo in directoryInfos)
            {
                folders.Add(new LocalFolder(directoryInfo.FullName));
            }

            return folders;
        }
    }
}
