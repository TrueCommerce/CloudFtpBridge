using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CloudFtpBridge.Core.Services
{
    public class FileSystemActivator
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public FileSystemActivator(IServiceProvider serviceProvider, ILogger<FileSystemActivator> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public IFileSystem Activate(string fileSystemTypeName, IDictionary<string, string> configuration)
        {
            _logger.LogDebug("Activating File System: {FileSystemType}", fileSystemTypeName);

            var fileSystemType = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => a.FullName.Contains("CloudFtpBridge"))
                .SelectMany(a => a.GetTypes())
                .Where(t => !t.IsInterface && typeof(IFileSystem).IsAssignableFrom(t))
                .FirstOrDefault(t => t.FullName.Equals(fileSystemTypeName));

            if (fileSystemType == null)
            {
                _logger.LogError("No file system implementation could be found matching the name {FileSystemType}.", fileSystemTypeName);

                throw new TypeLoadException($"Failed to activate file system of type '{fileSystemTypeName}'.");
            }

            var diConfig = _serviceProvider.GetRequiredService<IConfiguration>();

            diConfig = new ConfigurationBuilder()
                .AddConfiguration(diConfig)
                .AddInMemoryCollection(configuration)
                .Build();

            return (IFileSystem)ActivatorUtilities.CreateInstance(_serviceProvider, fileSystemType, diConfig);
        }
    }
}
