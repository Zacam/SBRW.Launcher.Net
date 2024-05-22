using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.ModNet;
using SBRW.Launcher.Core.Downloader;
using SBRW.Launcher.Core.Downloader.LZMA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SBRW.Launcher.App.UI_Forms.Main_Screen
{
    public partial class Screen_Main : Form
    {
        public static Screen_Main? Screen_Instance { get; set; }
        public static int UI_MODE { get; set; } = 0;
        private static bool LoginEnabled { get; set; }
        private static bool ServerEnabled { get; set; }
        private static bool Builtinserver { get; set; }
        private static bool SkipServerTrigger { get; set; }
        private static bool Playenabled { get; set; }
        private static bool IsDownloading { get; set; } = true;
        private static bool DisableLogout { get; set; }

        public static string GetTempName { get; set; } = Path.GetTempFileName();

        private static int LastSelectedServerId { get; set; }
        public static int NfswPid { get; set; }
        public static Thread? Nfswstarted { get; set; }
        private static bool StillCheckingLastServer { get; set; }

        
        public static Download_LZMA_Data? LZMA_Downloader { get; set; }
        public static Download_Client? Pack_SBRW_Downloader { get; set; }
        public static Download_Extract? Pack_SBRW_Unpacker { get; set; }
        private static Download_Information_ModNet? ModNet_Download_Status { get; set; }
        public static bool Pack_SBRW_Downloader_Unpack_Lock { get; set; }


        private static string JsonGSI { get; set; } = string.Empty;
        private static MemoryStream? ServerRawBanner { get; set; }
        private string LoginWelcomeTime { get; set; } = string.Empty;
        private string LoginToken { get; set; } = string.Empty;
        private string UserId { get; set; } = string.Empty;
        private static int ServerSecondsToShutDown { get; set; }
        
        private static System.Timers.Timer? Live_Action_Timer { get; set; }

        public static string ModNetFileNameInUse { get; set; } = string.Empty;
        public static Queue<Uri> ModFilesDownloadUrls { get; set; } = new Queue<Uri>();
        public static bool IsDownloadingModNetFiles { get; set; }
        public static int CurrentModFileCount { get; set; }
        public static int TotalModFileCount { get; set; }
        public static string Custom_SBRW_Pack { get { return Path.Combine(Locations.LauncherFolder, "GameFiles.sbrwpack"); } }

        public static int Game_Affinity { get; set; } = 4;
        public static int Game_Affinity_Start { get; set; } = 0;
        public static int Game_Affinity_End { get; set; } = 3;

        public Screen_Main()
        {
            InitializeComponent();
            Set_Visuals();
            Screen_Instance = this;
        }
    }
}
