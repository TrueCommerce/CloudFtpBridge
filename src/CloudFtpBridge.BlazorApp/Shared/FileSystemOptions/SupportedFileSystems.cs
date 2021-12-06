namespace CloudFtpBridge.BlazorApp.Shared.FileSystemOptions
{
    public static class SupportedFileSystems
    {
        public const string FluentFTP = "CloudFtpBridge.Infrastructure.FluentFTP.FluentFTPFileSystem";
        public const string FTP = "CloudFtpBridge.Infrastructure.FTP.FTPFileSystem";
        public const string Local = "CloudFtpBridge.Infrastructure.LocalFileSystem.LocalFileSystem";
    }
}
