using System.Collections.Generic;

namespace SBRW.Launcher.App.UI_Forms.Verify_Hash_Screen
{
    /// <summary>
    /// A summary of the completed verification process.
    /// </summary>
    public class Verify_Model_Verification_Result
    {
        public int TotalFilesScanned { get; set; }
        public int TotalFilesMissingOrInvalid { get; set; }
        public int FilesSuccessfullyDownloaded { get; set; }
        public int FilesFailedToDownload { get; set; }
        public bool WasCancelled { get; set; }
        public List<string> FailedDownloadFiles { get; } = new List<string>();
    }
}