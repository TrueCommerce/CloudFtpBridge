using System.ComponentModel;

namespace CloudFtpBridge.Core.Models
{
    public enum FileStage
    {
        [Description("Transfer Started")]
        TransferStarted = 0,

        [Description("Read - Started")]
        ReadStarted = 1,

        [Description("Read - Failed")]
        ReadFailed = 2,

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
