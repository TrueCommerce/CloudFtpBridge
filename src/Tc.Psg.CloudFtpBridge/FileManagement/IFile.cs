using System;
using System.IO;
using System.Threading.Tasks;

namespace Tc.Psg.CloudFtpBridge.FileManagement
{
    /// <summary>
    /// A logical abstraction that represents an underlying file (local or remote).
    /// </summary>
    public interface IFile : IDisposable
    {
        /// <summary>
        /// The "local" name of the file (without the path).
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <returns></returns>
        Task Delete();

        /// <summary>
        /// Checks to see if the underlying file still exists.
        /// </summary>
        /// <returns></returns>
        Task<bool> Exists();

        /// <summary>
        /// Gets the full implementation-specific path to the underlying file.
        /// </summary>
        /// <returns></returns>
        Task<string> GetFullPath();

        /// <summary>
        /// Gets a readable stream of the file contents.
        /// </summary>
        /// <returns></returns>
        Task<Stream> GetReadStream();

        /// <summary>
        /// Writes the contents of the provided stream to the underlying file, overwriting any existing data.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        Task Write(Stream stream);
    }
}
