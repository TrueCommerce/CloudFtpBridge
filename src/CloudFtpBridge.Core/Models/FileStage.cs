using System.ComponentModel;

namespace CloudFtpBridge.Core.Models
{
    /// <summary>
    /// Represents a specific stage or checkpoint of a file transfer.
    /// Used for auditing purposes.
    /// </summary>
    public enum FileStage
    {
        [Description("Transfer Started")]
        TransferStarted = 0,

        [Description("Enforce Unique File Name - Started")]
        EnforceUniqueNameStarted = 100,

        [Description("Enforce Unique File Name - Failed")]
        EnforceUniqueNameFailed = 200,

        [Description("Enforce Unique File Name - Completed")]
        EnforceUniqueNameCompleted = 300,

        [Description("Read - Started")]
        ReadStarted = 400,

        [Description("Read - Failed")]
        ReadFailed = 500,

        [Description("Read - Completed")]
        ReadCompleted = 600,

        [Description("Write - Started")]
        WriteStarted = 700,

        [Description("Write - Failed")]
        WriteFailed = 800,

        [Description("Write - Completed")]
        WriteCompleted = 900,

        [Description("Delete Source - Started")]
        DeleteSourceStarted = 1000,

        [Description("Delete Source - Failed")]
        DeleteSourceFailed = 1100,

        [Description("Delete Source - Completed")]
        DeleteSourceCompleted = 1200,

        [Description("Transfer Completed")]
        TransferCompleted = 1300
    }
}
