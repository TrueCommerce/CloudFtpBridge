using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Tc.Psg.CloudFtpBridge.FileManagement.Local
{
    public class LocalFolder : IFolder
    {
        private DirectoryInfo _directoryInfo;

        public LocalFolder(string fullPath)
        {
            _directoryInfo = new DirectoryInfo(fullPath);
        }

        public LocalFolder(DirectoryInfo directoryInfo)
        {
            _directoryInfo = directoryInfo;
        }

        public string Name
        {
            get
            {
                return _directoryInfo.Name;
            }
        }

        public async Task<IFile> CreateFile(string name)
        {
            string fullDirectoryPath = await GetFullPath();
            string fullFilePath = Path.Combine(fullDirectoryPath, name);

            // this just creates an empty file
            File.Create(fullFilePath).Dispose();

            IEnumerable<IFile> files = await ListFiles();

            return files.FirstOrDefault(x => x.Name == name);
        }

        public async Task<string> GetFullPath()
        {
            return _directoryInfo.FullName;
        }

        public async Task<IEnumerable<IFile>> ListFiles()
        {
            FileInfo[] fileInfos = _directoryInfo.GetFiles();

            List<IFile> files = new List<IFile>();

            foreach (FileInfo fileInfo in fileInfos)
            {
                files.Add(new LocalFile(fileInfo));
            }

            return files;
        }

        public async Task<IEnumerable<IFolder>> ListFolders()
        {
            DirectoryInfo[] directories = _directoryInfo.GetDirectories();

            List<LocalFolder> folders = new List<LocalFolder>();

            foreach (DirectoryInfo directory in directories)
            {
                folders.Add(new LocalFolder(directory));
            }

            return folders;
        }
    }
}
