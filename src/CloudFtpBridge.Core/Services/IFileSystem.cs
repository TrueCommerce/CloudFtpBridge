using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using CloudFtpBridge.Core.Models;

namespace CloudFtpBridge.Core.Services
{
    public interface IFileSystem
    {
        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        Task Delete(string fileName);

        /// <summary>
        /// Returns true if there are files available to process.
        /// Implementations should apply any applicable filtering mechanisms her as well.
        /// </summary>
        Task<bool> HasFiles();

        /// <summary>
        /// Gets a list of files in the specified directory that are available for processing.
        /// </summary>
        Task<IReadOnlyCollection<FileRef>> List();

        /// <summary>
        /// Gets a read stream with the contents of th specified file.
        /// The underlying stream implementation should be treated as transient as it may be a memory stream or a temporary file.
        /// </summary>
        Task<Stream> Read(string fileName);

        /// <summary>
        /// Writes the bytes from the provided stream to the specified file.
        /// If the specified file already exists, it will be overwritten.
        /// </summary>
        Task Write(string fileName, Stream fromStream);
    }
}
