namespace SBRW.Launcher.App.UI_Forms.Verify_Hash_Screen
{
    /// <summary>
    /// Configuration for the FileVerifier.
    /// </summary>
    public class Verify_Config
    {
        /// <summary>
        /// The absolute path to the game directory.
        /// </summary>
        public string GamePath { get; set; } = string.Empty;

        /// <summary>
        /// The base URL for the CDN (e.g., "https://my-cdn.com/gamefiles").
        /// </summary>
        public string CdnBaseUrl { get; set; } = string.Empty;

        /// <summary>
        /// The name of the checksum manifest file (e.g., "checksums.dat").
        /// </summary>
        public string ChecksumFileName { get; set; } = "checksums.dat";

        /// <summary>
        /// A list of sub-paths within the CdnBaseUrl to try.
        /// </summary>
        public string CdnUnpackedPath { get; set; } = "/unpacked";

        /// <summary>
        /// Whether to delete all files in the 'scripts' folder (except 'LangPicker.ini').
        /// The UI is responsible for asking the user before setting this.
        /// </summary>
        public bool CleanScriptsFolder { get; set; }
    }
}