using SBRW.Launcher.App.UI_Forms.Main_Screen;
using SBRW.Launcher.Core.Discord.RPC_;
using SBRW.Launcher.Core.Extra.File_;
using SBRW.Launcher.Core.Extra.File_.Save_;
using System;
using System.Windows.Forms;

namespace SBRW.Launcher.App.UI_Forms.Settings_Screen
{
    public partial class Screen_Settings : Form
    {
        #region Global Functions  
#pragma warning disable CS8618
        public static Screen_Settings? Screen_Instance { get; set; }
#pragma warning restore CS8618
        public static bool RestartRequired { get; set; }
        public static bool Insider_Settings_Lock { get; set; }
        private string NewLauncherPath { get; set; }
        private string NewGameFilesPath { get; set; }
        public string New_Choosen_CDN { get; set; }
        #region Security Center
        ///<summary>Windows 10: Caches Old Game Path in the event of the user does Firewall First</summary>
        private static string CacheOldGameLocation { get; set; } = Save_Settings.Live_Data.Game_Path_Old;
        ///<summary>Disable Button: Firewall Rules API</summary>
        private static bool DisableButtonFRAPI { get; set; }
        ///<summary>Disable Button: Firewall Rules Check</summary>
        private static bool DisableButtonFRC { get; set; } = true;
        ///<summary>Disable Button: Firewall Rules Add All</summary>
        private static bool DisableButtonFRAA { get; set; } = true;
        ///<summary>Disable Button: Firewall Rules Add Launcher</summary>
        private static bool DisableButtonFRAL { get; set; } = true;
        ///<summary>Disable Button: Firewall Rules Add Game</summary>
        private static bool DisableButtonFRAG { get; set; } = true;
        ///<summary>Disable Button: Firewall Rules Remove All</summary>
        private static bool DisableButtonFRRA { get; set; } = true;
        ///<summary>Disable Button: Firewall Rules Remove Launcher</summary>
        private static bool DisableButtonFRRL { get; set; } = true;
        ///<summary>Disable Button: Firewall Rules Remove Game</summary>
        private static bool DisableButtonFRRG { get; set; } = true;
        ///<summary>Disable Button: Defender Exclusion API</summary>
        private static bool DisableButtonDRAPI { get; set; }
        ///<summary>Disable Button: Defender Exclusion Check</summary>
        private static bool DisableButtonDRC { get; set; } = true;
        ///<summary>Disable Button: Defender Exclusion Add All</summary>
        private static bool DisableButtonDRAA { get; set; } = true;
        ///<summary>Disable Button: Defender Exclusion Add Launcher</summary>
        private static bool DisableButtonDRAL { get; set; } = true;
        ///<summary>Disable Button: Defender Exclusion Add Game</summary>
        private static bool DisableButtonDRAG { get; set; } = true;
        ///<summary>Disable Button: Defender Exclusion Remove All</summary>
        private static bool DisableButtonDRRA { get; set; } = true;
        ///<summary>Disable Button: Defender Exclusion Remove Launcher</summary>
        private static bool DisableButtonDRRL { get; set; } = true;
        ///<summary>Disable Button: Defender Exclusion Remove Game</summary>
        private static bool DisableButtonDRRG { get; set; } = true;
        ///<summary>Disable Button: Permission Check</summary>
        private static bool DisableButtonPRC { get; set; }
        ///<summary>Disable Button: Permission Set</summary>
        private static bool DisableButtonPRAA { get; set; } = true;
        #endregion
        #endregion
#pragma warning disable CS8618
        public Screen_Settings()
#pragma warning restore CS8618
        {
            InitializeComponent();
            Set_Visuals();
            this.Closing += (x, y) =>
            {
                Presence_Launcher.Status(4);
                /* Security Center */
                DisableButtonFRAPI = DisableButtonDRAPI = DisableButtonDRAPI = DisableButtonPRC = false;

                /* This is for Mono Support */
                if (ToolTip_Hover.Active)
                {
                    ToolTip_Hover.RemoveAll();
                    ToolTip_Hover.Dispose();
                }

                if (Screen_Main.Screen_Instance != default)
                {
                    Screen_Main.Clear_Hide_Screen_Form_Panel();
                }

                Screen_Instance = default;
            };
            Screen_Instance = this;

            Presence_Launcher.Status(22);
        }
    }
}