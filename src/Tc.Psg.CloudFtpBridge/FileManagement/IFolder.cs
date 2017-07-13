using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tc.Psg.CloudFtpBridge.FileManagement
{
    /// <summary>
    /// A logical abstraction that represents an underlying directory (local or remote).
    /// </summary>
    public interface IFolder
    {
        /// <summary>
        /// The "local" name of the folder (without the path).
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Creates a new empty file.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IFile> CreateFile(string name);

        /// <summary>
        /// Gets the full implementation-specific path to the underlying folder.
        /// </summary>
        /// <returns></returns>
        Task<string> GetFullPath();

        /// <summary>
        /// Lists all <see cref="IFile"/>s in this folder.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<IFile>> ListFiles();

        /// <summary>
        /// Lists all child <see cref="IFolder"/>s in the folder.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<IFolder>> ListFolders();
    }
}
