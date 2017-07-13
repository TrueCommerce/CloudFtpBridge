using System.Collections.Generic;
using System.IO;
using System.Reflection;

using Newtonsoft.Json;

namespace Tc.Psg.CloudFtpBridge.Configuration
{
    public class CloudFtpBridgeConfig
    {
        public CloudFtpBridgeConfig()
        {
            Companies = new List<CompanyConfig>();
            PollingInterval = 30;
        }

        public List<CompanyConfig> Companies { get; set; }
        public bool DebugEnabled { get; set; }
        public int PollingInterval { get; set; } // seconds

        public static string GetDefaultConfigFileName()
        {
            FileInfo assemblyFileInfo = new FileInfo(Assembly.GetEntryAssembly().Location);

            return Path.Combine(assemblyFileInfo.DirectoryName, "CloudFtpBridgeConfig.json");
        }

        public static CloudFtpBridgeConfig Load()
        {
            return LoadFrom(GetDefaultConfigFileName());
        }

        public static CloudFtpBridgeConfig LoadFrom(string path)
        {
            CloudFtpBridgeConfig config = new CloudFtpBridgeConfig();

            if (!File.Exists(path))
            {
                config.SaveTo(path);
            }

            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                config = JsonConvert.DeserializeObject<CloudFtpBridgeConfig>(json);
            }

            return config;
        }

        public void Save()
        {
            SaveTo(GetDefaultConfigFileName());
        }

        public void SaveTo(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                string json = JsonConvert.SerializeObject(this, Formatting.Indented);

                writer.Write(json);
            }
        }
    }
}
