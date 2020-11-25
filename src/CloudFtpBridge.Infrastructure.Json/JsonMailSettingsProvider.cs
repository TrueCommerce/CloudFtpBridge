using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using CloudFtpBridge.Core.Models;
using CloudFtpBridge.Core.Services;
using CloudFtpBridge.Core.Utils;

namespace CloudFtpBridge.Infrastructure.Json
{
    public class JsonMailSettingsProvider : IMailSettingsProvider
    {
        private readonly string _fileName = Path.Combine(PathHelper.GetDefaultStoragePath(), "MailSettings.json");

        public JsonMailSettingsProvider()
        {
            if (!File.Exists(_fileName))
            {
                File.WriteAllText(_fileName, JsonSerializer.Serialize(new MailSettings()));
            }
        }

        public Task<MailSettings> Get()
        {
            var json = File.ReadAllText(_fileName);

            return Task.FromResult(JsonSerializer.Deserialize<MailSettings>(json));
        }

        public Task Save(MailSettings mailSettings)
        {
            File.WriteAllText(_fileName, JsonSerializer.Serialize(mailSettings));

            return Task.CompletedTask;
        }
    }
}
