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
        EnforceUniqueNameStarted = 51,

        [Description("Enforce Unique File Name - Failed")]
        EnforceUniqueNameFailed = 52,

        [Description("Enforce Unique File Name - Completed")]
        EnforceUniqueNameCompleted = 53,

        // 54
        // 55
        // ...

        [Description("Read - Started")]
        ReadStarted = 1,

        // 101
        // 102
        // ...

        [Description("Read - Failed")]
        ReadFailed = 2,

        // 201
        // 202
        // ...

        [Description("Read - Completed")]
        ReadCompleted = 3,

        [Description("Write - Started")]
        WriteStarted = 4,

        [Description("Write - Failed")]
        WriteFailed = 5,

        [Description("Write - Completed")]
        WriteCompleted = 6,

        [Description("Delete Source - Started")]
        DeleteSourceStarted = 7,

        [Description("Delete Source - Failed")]
        DeleteSourceFailed = 8,

        [Description("Delete Source - Completed")]
        DeleteSourceCompleted = 9,

        [Description("Transfer Completed")]
        TransferCompleted = 10
    }
}
