namespace Tc.Psg.CloudFtpBridge.Mail
{
    public interface IMailOptionsRepository
    {
        MailOptions Get();
        void Set(MailOptions options);
    }
}
