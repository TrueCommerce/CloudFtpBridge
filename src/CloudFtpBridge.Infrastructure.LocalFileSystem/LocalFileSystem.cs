using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using CloudFtpBridge.Core.Models;
using CloudFtpBridge.Core.Services;
using CloudFtpBridge.Core.Utils;

using Microsoft.Extensions.Configuration;

namespace CloudFtpBridge.Infrastructure.LocalFileSystem
{
    public class LocalFileSystem : IFileSystem
    {
        private readonly LocalFileSystemOptions _options = new LocalFileSystemOptions();

        public LocalFileSystem(IConfiguration configuration)
        {
            configuration.GetSection("CloudFtpBridge:Infrastructure:LocalFileSystem").Bind(_options);
        }

        public Task Delete(string fileName)
        {
            File.Delete(PathHelper.Combine(_options.Path, fileName));

            return Task.CompletedTask;
        }

        public Task<bool> HasFiles()
        {
            var directoryInfo = new DirectoryInfo(PathHelper.Combine(_options.Path));

            return Task.FromResult(directoryInfo.EnumerateFiles(_options.Pattern).Any());
        }

        public Task<IReadOnlyCollection<FileRef>> List()
        {
            var directoryInfo = new DirectoryInfo(PathHelper.Combine(_options.Path));

            return Task.FromResult(directoryInfo.EnumerateFiles(_options.Pattern)
                .Select(fi => new FileRef(fi.Name))
                .ToArray() as IReadOnlyCollection<FileRef>);
        }

        public Task<Stream> Read(string fileName)
        {
            return Task.FromResult(File.OpenRead(PathHelper.Combine(_options.Path, fileName)) as Stream);
        }

        public async Task Write(string fileName, Stream fromStream)
        {
            if (fromStream.CanSeek)
            {
                fromStream.Seek(0, SeekOrigin.Begin);
            }

            using (var stream = new FileStream(PathHelper.Combine(_options.Path, fileName), FileMode.OpenOrCreate, FileAccess.Write))
            {
                await fromStream.CopyToAsync(stream);
            }
        }
    }
}
