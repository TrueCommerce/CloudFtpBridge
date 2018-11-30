using System;
using System.Text;

using Microsoft.Extensions.Logging;

using Tc.Psg.CloudFtpBridge.Mail;

namespace Tc.Psg.CloudFtpBridge.Logging
{
    public class EmailLogger : IEmailLogger
    {
        private readonly IMailSender _mailSender;

        public EmailLogger(IMailSender mailSender)
        {
            _mailSender = mailSender;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return (logLevel == LogLevel.Error);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (exception == null || !exception.IsMarkedForEmailNotification())
            {
                return;
            }

            StringBuilder messageBuilder = new StringBuilder();

            messageBuilder.Append("An exception was thrown by Cloud FTP Bridge. The associated log entry is included below:");
            messageBuilder.Append("\r\n<br/>\r\n<br/>");

            messageBuilder.Append(formatter(state, exception));

            messageBuilder.Append("\r\n<br/>\r\n<br/>");

            messageBuilder.Append("Exception Details:");
            messageBuilder.Append("\r\n<br/>");

            messageBuilder.Append(exception.ToString());

            _mailSender.Send("Cloud FTP Bridge: Exception Notification", messageBuilder.ToString());
        }
    }
}
