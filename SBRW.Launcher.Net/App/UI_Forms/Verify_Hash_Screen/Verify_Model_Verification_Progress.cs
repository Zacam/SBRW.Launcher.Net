namespace SBRW.Launcher.App.UI_Forms.Verify_Hash_Screen
{
    /// <summary>
    /// Reports the state of the verification process.
    /// </summary>
    public class Verify_Model_Verification_Progress
    {
        /// <summary>
        /// A human-readable status message.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// The current file being processed (scanned or downloaded).
        /// </summary>
        public string? CurrentFile { get; set; }

        /// <summary>
        /// Overall progress percentage (0-100) for the current major step.
        /// </summary>
        public int Percentage { get; set; }

        /// <summary>
        /// For downloads, the bytes received for the current file.
        /// </summary>
        public long BytesDownloaded { get; set; }

        /// <summary>
        /// For downloads, the total bytes of the current file.
        /// </summary>
        public long TotalBytesToDownload { get; set; }
    }
}