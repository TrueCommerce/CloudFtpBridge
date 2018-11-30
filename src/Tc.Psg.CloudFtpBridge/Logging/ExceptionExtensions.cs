using System;

namespace Tc.Psg.CloudFtpBridge.Logging
{
    public static class ExceptionExtensions
    {
        private const string _EmailNotificationFlagKey = "MarkedForEmailNotification";

        public static bool IsMarkedForEmailNotification(this Exception ex)
        {
            return ex.Data.Contains(_EmailNotificationFlagKey) && (bool)ex.Data[_EmailNotificationFlagKey];
        }

        public static void MarkForEmailNotification(this Exception ex)
        {
            ex.Data[_EmailNotificationFlagKey] = true;
        }
    }
}
