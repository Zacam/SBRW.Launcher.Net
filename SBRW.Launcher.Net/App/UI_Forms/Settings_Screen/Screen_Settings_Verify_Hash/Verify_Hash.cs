using SBRW.Launcher.Core.Reference.Json_.Newtonsoft_;
using SBRW.Launcher.RunTime.LauncherCore.Lists.JSON;
using System.Collections.Generic;
using System.ComponentModel;

namespace SBRW.Launcher.App.UI_Forms.Settings_Screen
{
    partial class Screen_Settings
    {
        public static BackgroundWorker? Verify_Hash { get; set; }
        /* VerifyHash */
        public int Files_Total { get; set; }
        public int Files_Scanned_Total { get; set; }
        public int Files_Invaild_Total { get; set; }
        public int Files_Downloaded_Total { get; set; }
        public int Files_Download_Error_Total { get; set; }
        public int Files_Deletion_Error_Total { get; set; }
        public string Verify_CDN_URL { get; set; } = string.Empty;
        public bool File_Downloading { get; set; }
        public Raw_Download_Progress Verify_Hash_Status { get; set; } = Raw_Download_Progress.Idle;
        /// <summary>
        /// 
        /// </summary>
        public static bool Skip_Scripts_Folder { get; set; }
        public static List<Json_List_Scanned_Game_Files> Generated_Scanned_List { get; set; } = new List<Json_List_Scanned_Game_Files>();
        public static List<Json_List_Invalid_Game_Files> Generated_Scanned_Invalid_List { get; set; } = new List<Json_List_Invalid_Game_Files>();
        public string[] File_Checksum { get; set; } = { };
    }
    /// <summary>
    /// 
    /// </summary>
    public enum Raw_Download_Progress
    {
        Idle = -1,
        Scanning = 0,
        Removing = 1,
        Checksums_File = 2,
        Checksums_File_Found = 2,
        Checksums_File_Error = 2,
        Checksums_Not_Available,
        Verifying = 3,
        Invaild = 4,
        Downloading = 5,
        Stopped = 6,
        Complete = 7,
        Passed = 8,
        Error = 9
    }
}
