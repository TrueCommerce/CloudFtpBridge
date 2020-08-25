using System;

using FluentValidation;
using FluentValidation.Results;

namespace Tc.Psg.CloudFtpBridge
{
    public class Server
    {
        public string Host { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Path { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public bool FtpsEnabled { get; set; } = false;
        public string EncryptionMode { get; set; } = "None";
        public string DataConnectionType { get; set; } = "Default";
        public static Server Empty
        {
            get
            {
                return new Server();
            }
        }

        public static void Validate(Server server)
        {
            ValidationResult result = new ServerValidator().Validate(server);

            if (!result.IsValid)
            {
                throw new InvalidOperationException(result.Errors[0].ErrorMessage);
            }
        }
    }

    public class ServerValidator : AbstractValidator<Server>
    {
        public ServerValidator()
        {
            RuleFor(x => x.Host).NotEmpty().WithMessage("Please specify a hostname for the server.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please specify a server name.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Please provide your password.");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Please provide your username.");
        }
    }
}
