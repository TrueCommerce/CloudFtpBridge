using Microsoft.Extensions.Logging;

namespace Tc.Psg.CloudFtpBridge.Logging
{
    public class EmailLoggerProvider : IEmailLoggerProvider
    {
        private readonly IEmailLogger _logger;

        public EmailLoggerProvider(IEmailLogger logger)
        {
            _logger = logger;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _logger;
        }

        public void Dispose()
        {
            // do nothing
        }
    }
}
