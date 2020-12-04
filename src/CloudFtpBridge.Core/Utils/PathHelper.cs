using System;
using System.IO;
using System.Linq;

namespace CloudFtpBridge.Core.Utils
{
    public static class PathHelper
    {
        private const string _DefaultStoragePathFragment = @"TrueCommerce\PSGEngineering\CloudFtpBridge\v3";
        private const string _LegacyStoragePathFragment = @"TrueCommerce\PSGEngineering\CloudFtpBridge";

        /// <summary>
        /// Combines the provided path parts.
        /// The returned path will NOT have leading or trailing slashes and will always use the forwrd slash (/) UNLESS it is a UNC path, in which case backslashes and two leading backslashes are used.
        /// </summary>
        public static string Combine(params string[] parts)
        {
            parts = parts.Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();

            if (parts.Length > 0 && parts[0].StartsWith("\\\\"))
            {
                return string.Concat("\\\\", string.Join("\\", parts
                .Select(p => p.Replace('/', '\\').Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries))
                .SelectMany(p => p)));
            }

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

        /// <summary>
        /// Gets the location used by Cloud FTP Bridge 2.x and earlier.
        /// Used primarily for data migration.
        /// </summary>
        /// <returns></returns>
        public static string GetLegacyStoragePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), _LegacyStoragePathFragment);
        }
    }
}
