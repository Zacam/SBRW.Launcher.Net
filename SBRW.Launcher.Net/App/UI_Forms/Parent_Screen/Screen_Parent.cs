using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.Support;
using SBRW.Launcher.Core.Theme;
using System;
using System.Drawing;
using System.Windows.Forms;
using SBRW.Launcher.RunTime.LauncherCore.Visuals;

namespace SBRW.Launcher.App.UI_Forms.Parent_Screen
{
    public partial class Screen_Parent : Form
    {
        #region Screen Variables
        private Point Mouse_Down_Point { get; set; } = Point.Empty;
        public static Screen_Parent? Screen_Instance { get; set; }
        private static bool Clock_Tick_Theme_Update { get; set; }
        public static bool Launcher_Restart { get; set; }
        /// <summary>
        /// First time Run Is Active
        /// </summary>
        /// <remarks>-1 - Closing<br/>0 - Not Active<br/>1 - Active</remarks>
        public static int Launcher_Setup { get; set; } = 0;
        #endregion

        public Screen_Parent()
        {
            InitializeComponent();
            Set_Visuals();
        }
    }
}
