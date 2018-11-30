﻿using System;

using Microsoft.Extensions.Configuration;

using Tc.Psg.CloudFtpBridge;
using Tc.Psg.CloudFtpBridge.IO;
using Tc.Psg.CloudFtpBridge.Logging;
using Tc.Psg.CloudFtpBridge.Mail;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCloudFtpBridgeCore(this IServiceCollection services)
        {
            return services
                .Configure<CloudFtpBridgeOptions>((options) => { })
                ._AddCloudFtpBridgeCoreCore();
        }

        public static IServiceCollection AddCloudFtpBridgeCore(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<CloudFtpBridgeOptions>(configuration)
                ._AddCloudFtpBridgeCoreCore();
        }

        public static IServiceCollection AddCloudFtpBridgeCore(this IServiceCollection services, Action<CloudFtpBridgeOptions> configure)
        {
            return services
                .Configure(configure)
                ._AddCloudFtpBridgeCoreCore();
        }

        private static IServiceCollection _AddCloudFtpBridgeCoreCore(this IServiceCollection services)
        {
            return services
                .AddOptions()
                .AddTransient<IMailOptionsRepository, MailOptionsRepository>()
                .AddTransient<IWorkflowRepository, WorkflowRepository>()
                .AddTransient<IServerRepository, ServerRepository>()
                .AddTransient<IFileManager, FileManager>()
                .AddTransient<IMailSender, MailSender>()
                .AddSingleton<IEmailLogger, EmailLogger>()
                .AddSingleton<IEmailLoggerProvider, EmailLoggerProvider>();
        }
    }
}
