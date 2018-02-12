using System;
using System.IO;

namespace Tc.Psg.CloudFtpBridge
{
    public class CloudFtpBridgeOptions
    {
        private const string _DefaultDatabaseFileName = "CloudFtpBridge.db";
        private const string _DefaultStoragePathFragment = @"TrueCommerce\PSGEngineering\CloudFtpBridge";

        public CloudFtpBridgeOptions()
        {
            DatabaseFileName = _DefaultDatabaseFileName;
            StoragePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), _DefaultStoragePathFragment);
        }

        public string DatabaseFileName { get; set; }
        public int PollingInterval { get; set; }
        public string StoragePath { get; set; }

        public string GetFullDatabaseFileName()
        {
            TryEnsurePathsExist();

            return Path.Combine(StoragePath, DatabaseFileName);
        }

        public void TryEnsurePathsExist()
        {
            try
            {
                if (!Directory.Exists(StoragePath))
                {
                    Directory.CreateDirectory(StoragePath);
                }
            }

            catch { }
        }
    }
}
