using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tc.Psg.CloudFtpBridge.IO
{
    public interface IFolder
    {
        string FullName { get; }

        Task<IFile> CreateFile(string name);
        Task<IFolder> CreateOrGetFolder(string name);
        Task<IEnumerable<IFile>> GetFiles();
        Task<IEnumerable<IFolder>> GetFolders();
    }
}
