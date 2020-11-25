namespace CloudFtpBridge.Core.Models
{
    public class FileRef
    {
        public FileRef(string name)
        {
            Name = name;
        }

        public string Name { get; set; } = string.Empty;
    }
}
