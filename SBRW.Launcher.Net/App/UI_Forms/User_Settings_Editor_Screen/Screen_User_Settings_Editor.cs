using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.Core.Discord.RPC_;
using System.IO;
using System.Windows.Forms;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.Lists;
using SBRW.Launcher.RunTime.LauncherCore.Support;
using SBRW.Launcher.Core.Extra.XML_;
using SBRW.Launcher.App.UI_Forms.Settings_Screen;

namespace SBRW.Launcher.App.UI_Forms.User_Settings_Editor_Screen
{
    public partial class Screen_User_Settings_Editor : Form
    {
        public static bool FileReadOnly { get; set; }
        public static bool ResolutionsListLoaded { get; set; }
        public static bool PresetLoaded { get; set; }
        private int LastSelectedLanguage { get; set; }

        public Screen_User_Settings_Editor()
        {
            Presence_Launcher.Status(23);
            Log.Checking("UXE: Success, a UserSettings.xml file was found!");
            if (new FileInfo(Locations.UserSettingsXML).IsReadOnly == true)
            {
                FileReadOnly = true;
                Log.Warning("UXE: UserSettings.xml is Read-Only!");
            }
            else
            {
                Log.Completed("UXE: UserSettings.xml can be modified!");
            }

            XML_File.Read(1);
            ResolutionsListUpdater.Get();
            InitializeComponent();
            Icon = FormsIcon.Retrive_Icon();
            SetVisuals();
            this.Closing += (x, y) =>
            {
                Presence_Launcher.Status(22);
                /* This is for Mono Support */
                if (Hover.Active)
                {
                    Hover.RemoveAll();
                    Hover.Dispose();
                }

                if (Screen_Settings.Screen_Instance != default)
                {
                    Screen_Settings.Clear_Hide_Screen_Form_Panel();
                }
            };
        }
    }
}