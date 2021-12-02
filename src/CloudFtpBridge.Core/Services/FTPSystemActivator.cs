using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CloudFtpBridge.Core.Services
{
    public class FTPSystemActivator
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public FTPSystemActivator(IServiceProvider serviceProvider, ILogger<FTPSystemActivator> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public IFtpSystem Activate(string fileSystemTypeName, IDictionary<string, string> configuration)
        {
            Type fileSystemType;
            _logger.LogDebug("Activating File System: {IFtpSystem}", fileSystemTypeName);

            var temp = AppDomain.CurrentDomain
                .GetAssemblies();
            var temp2 = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => a.FullName.Contains("CloudFtpBridge"));
            var temp3 = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => a.FullName.Contains("CloudFtpBridge"))
                .SelectMany(a => a.GetTypes());
            var temp4 = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => a.FullName.Contains("CloudFtpBridge"))
                .SelectMany(a => a.GetTypes())
                .Where(t => !t.IsInterface && typeof(IFtpSystem).IsAssignableFrom(t));

            fileSystemType = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => a.FullName.Contains("CloudFtpBridge"))
                .SelectMany(a => a.GetTypes())
                .Where(t => !t.IsInterface && typeof(IFtpSystem).IsAssignableFrom(t))
                .First();


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

            return (IFtpSystem)ActivatorUtilities.CreateInstance(_serviceProvider, fileSystemType, diConfig);
        }
    }
}
