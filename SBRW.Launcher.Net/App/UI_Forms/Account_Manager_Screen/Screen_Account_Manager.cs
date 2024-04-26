using SBRW.Launcher.App.UI_Forms.Main_Screen;
using SBRW.Launcher.Core.Required.System.Windows_;
using System.ComponentModel;
using System.Drawing;
using System.ServiceProcess;
using System.Windows.Forms;

namespace SBRW.Launcher.App.UI_Forms.Account_Manager_Screen
{
    public partial class Screen_Account_Manager : Form
    {
#pragma warning disable CS8618
        /// <summary>
        /// 
        /// </summary>
        public static Screen_Account_Manager? Screen_Instance { get; set; }
#pragma warning restore CS8618
        /// <summary>
        /// 
        /// </summary>
        public static BindingList<Json_List_Account> Accounts_Cache = new BindingList<Json_List_Account>();
        /// <summary>
        /// 
        /// </summary>
        public static int Auto_ID = 0;
        /// <summary>
        /// 
        /// </summary>
        public bool Use_New_Max_Blob_Size => ((Product_Version.GetWindowsBuildNumber() > 17134) && Product_Version.ConvertWindowsNumberToName().Contains("Windows 1"));
        /// <summary>
        /// 
        /// </summary>
        public static ServiceControllerStatus? Store_Info_In_Vault { get; set; }
        /// <summary>
        /// 
        /// </summary>
        Color selectionBackColor;
        /// <summary>
        /// 
        /// </summary>
        Color selectionForeColor;

        public Screen_Account_Manager()
        {
            InitializeComponent();
            Set_Visuals();

            this.Closing += (x, y) =>
            {
                if (Screen_Main.Screen_Instance != default)
                {
                    Screen_Main.Clear_Hide_Screen_Form_Panel();
                }

                Screen_Instance = default;
            };
            Screen_Instance = this;
        }
    }
}
