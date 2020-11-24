using System;
using System.IO;
using System.Linq;

namespace CloudFtpBridge.Core.Utils
{
    public static class PathHelper
    {
        private const string _DefaultStoragePathFragment = @"TrueCommerce\PSGEngineering\CloudFtpBridge\v3";

        /// <summary>
        /// Combines the provided path parts.
        /// The returned path will NOT have leading or trailing slashes and will always use the forwrd slash (/).
        /// </summary>
        public static string Combine(params string[] parts)
        {
            return string.Join("/", parts
                .Select(p => p.Replace('\\', '/').Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries))
                .SelectMany(p => p));
        }

        /// <summary>
        /// Gets the default location for storing data for cloud FTP Bridge.
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultStoragePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), _DefaultStoragePathFragment);
        }
    }
}
