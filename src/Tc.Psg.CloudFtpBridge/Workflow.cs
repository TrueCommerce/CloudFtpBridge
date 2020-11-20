using System;

using FluentValidation;
using FluentValidation.Results;
using LiteDB;

namespace Tc.Psg.CloudFtpBridge
{
    public class Workflow
    {
        private Guid _serverId;

        public WorkflowDirection Direction { get; set; }
        public string FileFilter { get; set; }
        public Guid Id { get; set; }
        public string LocalPath { get; set; }
        public string Name { get; set; }
        public string RemotePath { get; set; }
        public bool AutoRetryFailed { get; set; }

        [BsonIgnore]
        public Server Server { get; set; }

        public Guid ServerId
        {
            get
            {
                return Server?.Id ?? _serverId;
            }

            set
            {
                _serverId = value;
            }
        }

        public static Workflow Empty
        {
            get
            {
                return new Workflow();
            }
        }

        public static void Validate(Workflow workflow)
        {
            ValidationResult result = new WorkflowValidator().Validate(workflow);

            if (!result.IsValid)
            {
                throw new InvalidOperationException(result.Errors[0].ErrorMessage);
            }
        }
    }

    public class WorkflowValidator : AbstractValidator<Workflow>
    {
        public WorkflowValidator()
        {
            RuleFor(x => x.LocalPath).NotEmpty().WithMessage("Please specify a local path.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please specify a workflow name.");
            RuleFor(x => x.Server).NotNull().WithMessage("Please select a server.");
        }
    }
}
