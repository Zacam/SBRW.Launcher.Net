namespace SBRW.Launcher.App.UI_Forms.Settings_Screen
{
    partial class Screen_Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ToolTip_Hover = new System.Windows.Forms.ToolTip(this.components);
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.Panel_Form_Screens = new System.Windows.Forms.Panel();
            this.Button_Console_Submit = new System.Windows.Forms.Button();
            this.Input_Console = new System.Windows.Forms.TextBox();
            this.TabControl_Shared_Hub = new SBRW.Launcher.Core.Theme.Control_TabControl();
            this.TabPage_Setup = new System.Windows.Forms.TabPage();
            this.LinkLabel_Game_Path_Setup = new System.Windows.Forms.LinkLabel();
            this.Label_Game_Current_Path_Setup = new System.Windows.Forms.Label();
            this.Button_Change_Game_Path_Setup = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.Label_CDN_Current_Setup = new System.Windows.Forms.Label();
            this.Button_CDN_List_Setup = new System.Windows.Forms.Button();
            this.LinkLabel_CDN_Current_Setup = new System.Windows.Forms.LinkLabel();
            this.Label_Version = new System.Windows.Forms.Label();
            this.Button_Save_Setup = new System.Windows.Forms.Button();
            this.Label_Introduction = new System.Windows.Forms.Label();
            this.Button_Change_Tabs = new System.Windows.Forms.Button();
            this.TabPage_Settings = new System.Windows.Forms.TabPage();
            this.Button_Save = new System.Windows.Forms.Button();
            this.Button_Exit = new System.Windows.Forms.Button();
            this.TabControl_Settings = new SBRW.Launcher.Core.Theme.Control_TabControl();
            this.TabPage_Launcher = new System.Windows.Forms.TabPage();
            this.TabControl_Launcher = new SBRW.Launcher.Core.Theme.Control_TabControl();
            this.TabPage_Launcher_Downloader = new System.Windows.Forms.TabPage();
            this.Label_GameFiles_Downloader_Raw = new System.Windows.Forms.Label();
            this.Label_GameFiles_Downloader_Pack = new System.Windows.Forms.Label();
            this.Label_GameFiles_Downloader_LZMA = new System.Windows.Forms.Label();
            this.Panel_GameFiles_Downloader = new System.Windows.Forms.Panel();
            this.Radio_Button_GameFiles_Downloader_LZMA = new System.Windows.Forms.RadioButton();
            this.Radio_Button_GameFiles_Downloader_SBRW_Pack = new System.Windows.Forms.RadioButton();
            this.Radio_Button_GameFiles_Downloader_Raw = new System.Windows.Forms.RadioButton();
            this.Label_CDN_Current_Details = new System.Windows.Forms.Label();
            this.Label_WebClient_Timeout_Details = new System.Windows.Forms.Label();
            this.Label_GameFiles_Downloader_Details = new System.Windows.Forms.Label();
            this.Label_Alt_WebCalls_Details = new System.Windows.Forms.Label();
            this.Label_CDN_Current = new System.Windows.Forms.Label();
            this.Button_CDN_List = new System.Windows.Forms.Button();
            this.LinkLabel_CDN_Current = new System.Windows.Forms.LinkLabel();
            this.Label_GameFiles_Downloader = new System.Windows.Forms.Label();
            this.CheckBox_LZMA_Downloader = new System.Windows.Forms.CheckBox();
            this.CheckBox_Alt_WebCalls = new System.Windows.Forms.CheckBox();
            this.Label_WebClient_Timeout = new System.Windows.Forms.Label();
            this.NumericUpDown_WebClient_Timeout = new System.Windows.Forms.NumericUpDown();
            this.TabPage_Launcher_Proxy = new System.Windows.Forms.TabPage();
            this.Button_Launcher_logs = new System.Windows.Forms.Button();
            this.Label_Proxy_Logging_None = new System.Windows.Forms.Label();
            this.Label_Proxy_Logging_Responses = new System.Windows.Forms.Label();
            this.Label_Proxy_Logging_Requests = new System.Windows.Forms.Label();
            this.Label_Proxy_Logging_Errors = new System.Windows.Forms.Label();
            this.Label_Proxy_Logging_All = new System.Windows.Forms.Label();
            this.Panel_Proxy_Logging = new System.Windows.Forms.Panel();
            this.Radio_Button_Proxy_Logging_None = new System.Windows.Forms.RadioButton();
            this.Radio_Button_Proxy_Logging_Responses = new System.Windows.Forms.RadioButton();
            this.Radio_Button_Proxy_Logging_All = new System.Windows.Forms.RadioButton();
            this.Radio_Button_Proxy_Logging_Errors = new System.Windows.Forms.RadioButton();
            this.Radio_Button_Proxy_Logging_Requests = new System.Windows.Forms.RadioButton();
            this.Label_Proxy_Logging_Details = new System.Windows.Forms.Label();
            this.Label_Proxy_Logging = new System.Windows.Forms.Label();
            this.Label_Proxy_Port = new System.Windows.Forms.Label();
            this.NumericUpDown_Proxy_Port = new System.Windows.Forms.NumericUpDown();
            this.CheckBox_Proxy = new System.Windows.Forms.CheckBox();
            this.CheckBox_Proxy_Domain = new System.Windows.Forms.CheckBox();
            this.CheckBox_Host_to_IP = new System.Windows.Forms.CheckBox();
            this.TabPage_Launcher_Miscellaneous = new System.Windows.Forms.TabPage();
            this.Label_Launcher_Builds_Branch_Developer = new System.Windows.Forms.Label();
            this.Label_Launcher_Builds_Branch_Beta = new System.Windows.Forms.Label();
            this.Label_Launcher_Builds_Branch_Stable = new System.Windows.Forms.Label();
            this.Panel_Launcher_Builds_Branch = new System.Windows.Forms.Panel();
            this.Radio_Button_Launcher_Builds_Branch_Stable = new System.Windows.Forms.RadioButton();
            this.Radio_Button_Launcher_Builds_Branch_Beta = new System.Windows.Forms.RadioButton();
            this.Radio_Button_Launcher_Builds_Branch_Developer = new System.Windows.Forms.RadioButton();
            this.Label_Launcher_Builds_Branch_Details = new System.Windows.Forms.Label();
            this.Label_Launcher_Builds_Branch = new System.Windows.Forms.Label();
            this.CheckBox_RPC = new System.Windows.Forms.CheckBox();
            this.LinkLabel_Launcher_Path = new System.Windows.Forms.LinkLabel();
            this.CheckBox_JSON_Update_Cache = new System.Windows.Forms.CheckBox();
            this.Label_Launcher_Path = new System.Windows.Forms.Label();
            this.CheckBox_Theme_Support = new System.Windows.Forms.CheckBox();
            this.CheckBox_Opt_Insider = new System.Windows.Forms.CheckBox();
            this.TabPage_Game = new System.Windows.Forms.TabPage();
            this.TabControl_Game = new SBRW.Launcher.Core.Theme.Control_TabControl();
            this.TabPage_Game_General = new System.Windows.Forms.TabPage();
            this.Button_Clear_NFSWO_Logs = new System.Windows.Forms.Button();
            this.Button_Clear_Server_Mods = new System.Windows.Forms.Button();
            this.Button_Clear_Crash_Logs = new System.Windows.Forms.Button();
            this.Label_Display_Timer = new System.Windows.Forms.Label();
            this.Panel_Display_Timer = new System.Windows.Forms.Panel();
            this.Radio_Button_Static_Timer = new System.Windows.Forms.RadioButton();
            this.Radio_Button_Dynamic_Timer = new System.Windows.Forms.RadioButton();
            this.Radio_Button_No_Timer = new System.Windows.Forms.RadioButton();
            this.CheckBox_Word_Filter_Check = new System.Windows.Forms.CheckBox();
            this.LinkLabel_Game_Path = new System.Windows.Forms.LinkLabel();
            this.Label_Game_Current_Path = new System.Windows.Forms.Label();
            this.Button_Game_User_Settings = new System.Windows.Forms.Button();
            this.Label_Game_Settings = new System.Windows.Forms.Label();
            this.ComboBox_Language_List = new System.Windows.Forms.ComboBox();
            this.Button_Game_Verify_Files = new System.Windows.Forms.Button();
            this.Button_Change_Game_Path = new System.Windows.Forms.Button();
            this.Label_Game_Files = new System.Windows.Forms.Label();
            this.TabPage_Game_Verify_Hash = new System.Windows.Forms.TabPage();
            this.VerifyHashWelcome = new System.Windows.Forms.Label();
            this.DownloadProgressText_Alt = new System.Windows.Forms.Label();
            this.DownloadProgressText = new System.Windows.Forms.Label();
            this.VerifyHashText = new System.Windows.Forms.Label();
            this.ScanProgressText = new System.Windows.Forms.Label();
            this.ScanProgressBar = new System.Windows.Forms.ProgressBar();
            this.DownloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.StartScanner = new System.Windows.Forms.Button();
            this.StopScanner = new System.Windows.Forms.Button();
            this.TabPage_Game_Security_Center = new System.Windows.Forms.TabPage();
            this.TabControl_Security_Center = new SBRW.Launcher.Core.Theme.Control_TabControl();
            this.TabPage_Security_Center_Firewall = new System.Windows.Forms.TabPage();
            this.GroupBox_Firewall = new System.Windows.Forms.GroupBox();
            this.TextBox_Console_Firewall = new System.Windows.Forms.TextBox();
            this.ButtonFirewallRulesAPI = new System.Windows.Forms.Button();
            this.ButtonFirewallRulesCheck = new System.Windows.Forms.Button();
            this.ButtonFirewallRulesAddAll = new System.Windows.Forms.Button();
            this.ButtonFirewallRulesAddGame = new System.Windows.Forms.Button();
            this.ButtonFirewallRulesAddLauncher = new System.Windows.Forms.Button();
            this.ButtonFirewallRulesRemoveGame = new System.Windows.Forms.Button();
            this.ButtonFirewallRulesRemoveLauncher = new System.Windows.Forms.Button();
            this.ButtonFirewallRulesRemoveAll = new System.Windows.Forms.Button();
            this.TextWindowsFirewall = new System.Windows.Forms.Label();
            this.TabPage_Security_Center_Defender = new System.Windows.Forms.TabPage();
            this.GroupBox_Defender = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ButtonDefenderExclusionAPI = new System.Windows.Forms.Button();
            this.ButtonDefenderExclusionAddGame = new System.Windows.Forms.Button();
            this.ButtonDefenderExclusionAddLauncher = new System.Windows.Forms.Button();
            this.ButtonDefenderExclusionAddAll = new System.Windows.Forms.Button();
            this.ButtonDefenderExclusionCheck = new System.Windows.Forms.Button();
            this.ButtonDefenderExclusionRemoveGame = new System.Windows.Forms.Button();
            this.ButtonDefenderExclusionRemoveLauncher = new System.Windows.Forms.Button();
            this.ButtonDefenderExclusionRemoveAll = new System.Windows.Forms.Button();
            this.TextWindowsDefender = new System.Windows.Forms.Label();
            this.TabPage_Security_Center_Permissons = new System.Windows.Forms.TabPage();
            this.GroupBox_Permissons = new System.Windows.Forms.GroupBox();
            this.TextBox_Live_Log = new System.Windows.Forms.TextBox();
            this.ButtonFolderPermissonCheck = new System.Windows.Forms.Button();
            this.ButtonFolderPermissonSet = new System.Windows.Forms.Button();
            this.TextFolderPermissions = new System.Windows.Forms.Label();
            this.TabPage_API = new System.Windows.Forms.TabPage();
            this.Label_API_Status_Five = new System.Windows.Forms.Label();
            this.Label_API_Status_Four = new System.Windows.Forms.Label();
            this.Label_API_Status_Three = new System.Windows.Forms.Label();
            this.Label_API_Status_Two = new System.Windows.Forms.Label();
            this.Label_API_Status_One = new System.Windows.Forms.Label();
            this.Label_API_Status = new System.Windows.Forms.Label();
            this.TabPage_About = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PatchText1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Picture_Logo = new System.Windows.Forms.PictureBox();
            this.Label_Version_Build_About = new System.Windows.Forms.Label();
            this.Label_Theme_Author = new System.Windows.Forms.Label();
            this.Label_Theme_Name = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Button_Settings = new System.Windows.Forms.PictureBox();
            this.Button_Close = new System.Windows.Forms.PictureBox();
            this.Button_Security_Center = new System.Windows.Forms.Button();
            this.Clock = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.TabControl_Shared_Hub.SuspendLayout();
            this.TabPage_Setup.SuspendLayout();
            this.TabPage_Settings.SuspendLayout();
            this.TabControl_Settings.SuspendLayout();
            this.TabPage_Launcher.SuspendLayout();
            this.TabControl_Launcher.SuspendLayout();
            this.TabPage_Launcher_Downloader.SuspendLayout();
            this.Panel_GameFiles_Downloader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_WebClient_Timeout)).BeginInit();
            this.TabPage_Launcher_Proxy.SuspendLayout();
            this.Panel_Proxy_Logging.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Proxy_Port)).BeginInit();
            this.TabPage_Launcher_Miscellaneous.SuspendLayout();
            this.Panel_Launcher_Builds_Branch.SuspendLayout();
            this.TabPage_Game.SuspendLayout();
            this.TabControl_Game.SuspendLayout();
            this.TabPage_Game_General.SuspendLayout();
            this.Panel_Display_Timer.SuspendLayout();
            this.TabPage_Game_Verify_Hash.SuspendLayout();
            this.TabPage_Game_Security_Center.SuspendLayout();
            this.TabControl_Security_Center.SuspendLayout();
            this.TabPage_Security_Center_Firewall.SuspendLayout();
            this.GroupBox_Firewall.SuspendLayout();
            this.TabPage_Security_Center_Defender.SuspendLayout();
            this.GroupBox_Defender.SuspendLayout();
            this.TabPage_Security_Center_Permissons.SuspendLayout();
            this.GroupBox_Permissons.SuspendLayout();
            this.TabPage_API.SuspendLayout();
            this.TabPage_About.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Button_Settings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Button_Close)).BeginInit();
            this.SuspendLayout();
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // Panel_Form_Screens
            // 
            this.Panel_Form_Screens.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Form_Screens.ForeColor = System.Drawing.Color.Transparent;
            this.Panel_Form_Screens.Location = new System.Drawing.Point(871, 61);
            this.Panel_Form_Screens.Name = "Panel_Form_Screens";
            this.Panel_Form_Screens.Size = new System.Drawing.Size(891, 529);
            this.Panel_Form_Screens.TabIndex = 79;
            this.Panel_Form_Screens.Visible = false;
            // 
            // Button_Console_Submit
            // 
            this.Button_Console_Submit.BackColor = System.Drawing.SystemColors.Control;
            this.Button_Console_Submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Console_Submit.ForeColor = System.Drawing.Color.Black;
            this.Button_Console_Submit.Location = new System.Drawing.Point(213, 18);
            this.Button_Console_Submit.Name = "Button_Console_Submit";
            this.Button_Console_Submit.Size = new System.Drawing.Size(78, 23);
            this.Button_Console_Submit.TabIndex = 148;
            this.Button_Console_Submit.Text = "Enter";
            this.Button_Console_Submit.UseVisualStyleBackColor = false;
            this.Button_Console_Submit.Visible = false;
            // 
            // Input_Console
            // 
            this.Input_Console.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.Input_Console.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Input_Console.Location = new System.Drawing.Point(296, 18);
            this.Input_Console.Name = "Input_Console";
            this.Input_Console.Size = new System.Drawing.Size(405, 20);
            this.Input_Console.TabIndex = 149;
            this.Input_Console.Visible = false;
            // 
            // TabControl_Shared_Hub
            // 
            this.TabControl_Shared_Hub.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.TabControl_Shared_Hub.Controls.Add(this.TabPage_Setup);
            this.TabControl_Shared_Hub.Controls.Add(this.TabPage_Settings);
            this.TabControl_Shared_Hub.DoubleBufferTabPages = true;
            this.TabControl_Shared_Hub.Location = new System.Drawing.Point(26, 48);
            this.TabControl_Shared_Hub.Multiline = true;
            this.TabControl_Shared_Hub.Name = "TabControl_Shared_Hub";
            this.TabControl_Shared_Hub.SelectedIndex = 0;
            this.TabControl_Shared_Hub.Size = new System.Drawing.Size(839, 455);
            this.TabControl_Shared_Hub.TabIndex = 156;
            // 
            // TabPage_Setup
            // 
            this.TabPage_Setup.Controls.Add(this.LinkLabel_Game_Path_Setup);
            this.TabPage_Setup.Controls.Add(this.Label_Game_Current_Path_Setup);
            this.TabPage_Setup.Controls.Add(this.Button_Change_Game_Path_Setup);
            this.TabPage_Setup.Controls.Add(this.label19);
            this.TabPage_Setup.Controls.Add(this.Label_CDN_Current_Setup);
            this.TabPage_Setup.Controls.Add(this.Button_CDN_List_Setup);
            this.TabPage_Setup.Controls.Add(this.LinkLabel_CDN_Current_Setup);
            this.TabPage_Setup.Controls.Add(this.Label_Version);
            this.TabPage_Setup.Controls.Add(this.Button_Save_Setup);
            this.TabPage_Setup.Controls.Add(this.Label_Introduction);
            this.TabPage_Setup.Controls.Add(this.Button_Change_Tabs);
            this.TabPage_Setup.Location = new System.Drawing.Point(0, 24);
            this.TabPage_Setup.Name = "TabPage_Setup";
            this.TabPage_Setup.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Setup.Size = new System.Drawing.Size(839, 431);
            this.TabPage_Setup.TabIndex = 1;
            this.TabPage_Setup.Text = "Welcome Setup";
            this.TabPage_Setup.UseVisualStyleBackColor = true;
            // 
            // LinkLabel_Game_Path_Setup
            // 
            this.LinkLabel_Game_Path_Setup.BackColor = System.Drawing.Color.Transparent;
            this.LinkLabel_Game_Path_Setup.Location = new System.Drawing.Point(14, 196);
            this.LinkLabel_Game_Path_Setup.Name = "LinkLabel_Game_Path_Setup";
            this.LinkLabel_Game_Path_Setup.Size = new System.Drawing.Size(806, 18);
            this.LinkLabel_Game_Path_Setup.TabIndex = 200;
            this.LinkLabel_Game_Path_Setup.TabStop = true;
            this.LinkLabel_Game_Path_Setup.Text = "C:\\Soapbox Race World\\Game Files";
            // 
            // Label_Game_Current_Path_Setup
            // 
            this.Label_Game_Current_Path_Setup.BackColor = System.Drawing.Color.Transparent;
            this.Label_Game_Current_Path_Setup.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_Game_Current_Path_Setup.Location = new System.Drawing.Point(11, 177);
            this.Label_Game_Current_Path_Setup.Name = "Label_Game_Current_Path_Setup";
            this.Label_Game_Current_Path_Setup.Size = new System.Drawing.Size(359, 14);
            this.Label_Game_Current_Path_Setup.TabIndex = 199;
            this.Label_Game_Current_Path_Setup.Text = "CURRENT DIRECTORY:";
            this.Label_Game_Current_Path_Setup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button_Change_Game_Path_Setup
            // 
            this.Button_Change_Game_Path_Setup.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Button_Change_Game_Path_Setup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Change_Game_Path_Setup.Location = new System.Drawing.Point(14, 223);
            this.Button_Change_Game_Path_Setup.Name = "Button_Change_Game_Path_Setup";
            this.Button_Change_Game_Path_Setup.Size = new System.Drawing.Size(190, 25);
            this.Button_Change_Game_Path_Setup.TabIndex = 198;
            this.Button_Change_Game_Path_Setup.Text = "Change GameFiles Path";
            this.Button_Change_Game_Path_Setup.UseVisualStyleBackColor = false;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.ForeColor = System.Drawing.Color.DarkGray;
            this.label19.Location = new System.Drawing.Point(241, 137);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(576, 18);
            this.label19.TabIndex = 196;
            this.label19.Text = "Download Location for Fetching the base Game and VerifyHash files.";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_CDN_Current_Setup
            // 
            this.Label_CDN_Current_Setup.BackColor = System.Drawing.Color.Transparent;
            this.Label_CDN_Current_Setup.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_CDN_Current_Setup.Location = new System.Drawing.Point(11, 90);
            this.Label_CDN_Current_Setup.Name = "Label_CDN_Current_Setup";
            this.Label_CDN_Current_Setup.Size = new System.Drawing.Size(224, 16);
            this.Label_CDN_Current_Setup.TabIndex = 193;
            this.Label_CDN_Current_Setup.Text = "CURRENT CDN:";
            this.Label_CDN_Current_Setup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button_CDN_List_Setup
            // 
            this.Button_CDN_List_Setup.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Button_CDN_List_Setup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_CDN_List_Setup.Location = new System.Drawing.Point(14, 138);
            this.Button_CDN_List_Setup.Name = "Button_CDN_List_Setup";
            this.Button_CDN_List_Setup.Size = new System.Drawing.Size(154, 25);
            this.Button_CDN_List_Setup.TabIndex = 195;
            this.Button_CDN_List_Setup.Text = "Change CDN";
            this.Button_CDN_List_Setup.UseVisualStyleBackColor = false;
            // 
            // LinkLabel_CDN_Current_Setup
            // 
            this.LinkLabel_CDN_Current_Setup.BackColor = System.Drawing.Color.Transparent;
            this.LinkLabel_CDN_Current_Setup.Location = new System.Drawing.Point(11, 111);
            this.LinkLabel_CDN_Current_Setup.Name = "LinkLabel_CDN_Current_Setup";
            this.LinkLabel_CDN_Current_Setup.Size = new System.Drawing.Size(806, 18);
            this.LinkLabel_CDN_Current_Setup.TabIndex = 194;
            this.LinkLabel_CDN_Current_Setup.TabStop = true;
            this.LinkLabel_CDN_Current_Setup.Text = "https://localhost";
            // 
            // Label_Version
            // 
            this.Label_Version.BackColor = System.Drawing.Color.Transparent;
            this.Label_Version.ForeColor = System.Drawing.Color.Black;
            this.Label_Version.Location = new System.Drawing.Point(349, 405);
            this.Label_Version.Name = "Label_Version";
            this.Label_Version.Size = new System.Drawing.Size(141, 12);
            this.Label_Version.TabIndex = 192;
            this.Label_Version.Text = "Version: XX.XX.XX.XXXX";
            this.Label_Version.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Button_Save_Setup
            // 
            this.Button_Save_Setup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Save_Setup.Location = new System.Drawing.Point(696, 389);
            this.Button_Save_Setup.Name = "Button_Save_Setup";
            this.Button_Save_Setup.Size = new System.Drawing.Size(133, 28);
            this.Button_Save_Setup.TabIndex = 191;
            this.Button_Save_Setup.Text = "Save";
            this.Button_Save_Setup.UseVisualStyleBackColor = true;
            // 
            // Label_Introduction
            // 
            this.Label_Introduction.BackColor = System.Drawing.Color.Transparent;
            this.Label_Introduction.ForeColor = System.Drawing.Color.Black;
            this.Label_Introduction.Location = new System.Drawing.Point(280, 19);
            this.Label_Introduction.Name = "Label_Introduction";
            this.Label_Introduction.Size = new System.Drawing.Size(336, 69);
            this.Label_Introduction.TabIndex = 186;
            this.Label_Introduction.Text = "Checking API Status";
            this.Label_Introduction.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Button_Change_Tabs
            // 
            this.Button_Change_Tabs.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Button_Change_Tabs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Change_Tabs.Location = new System.Drawing.Point(515, 389);
            this.Button_Change_Tabs.Name = "Button_Change_Tabs";
            this.Button_Change_Tabs.Size = new System.Drawing.Size(160, 28);
            this.Button_Change_Tabs.TabIndex = 185;
            this.Button_Change_Tabs.Text = "Advanced";
            this.Button_Change_Tabs.UseVisualStyleBackColor = false;
            // 
            // TabPage_Settings
            // 
            this.TabPage_Settings.Controls.Add(this.Button_Save);
            this.TabPage_Settings.Controls.Add(this.Button_Exit);
            this.TabPage_Settings.Controls.Add(this.TabControl_Settings);
            this.TabPage_Settings.Location = new System.Drawing.Point(0, 24);
            this.TabPage_Settings.Name = "TabPage_Settings";
            this.TabPage_Settings.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Settings.Size = new System.Drawing.Size(839, 431);
            this.TabPage_Settings.TabIndex = 0;
            this.TabPage_Settings.Text = "Settings";
            this.TabPage_Settings.UseVisualStyleBackColor = true;
            // 
            // Button_Save
            // 
            this.Button_Save.BackColor = System.Drawing.Color.Transparent;
            this.Button_Save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Button_Save.FlatAppearance.BorderSize = 0;
            this.Button_Save.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Button_Save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Button_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Save.ForeColor = System.Drawing.Color.DarkGray;
            this.Button_Save.Location = new System.Drawing.Point(721, 389);
            this.Button_Save.Name = "Button_Save";
            this.Button_Save.Size = new System.Drawing.Size(100, 42);
            this.Button_Save.TabIndex = 157;
            this.Button_Save.Text = "SAVE";
            this.Button_Save.UseVisualStyleBackColor = false;
            // 
            // Button_Exit
            // 
            this.Button_Exit.BackColor = System.Drawing.Color.Transparent;
            this.Button_Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Button_Exit.FlatAppearance.BorderSize = 0;
            this.Button_Exit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Button_Exit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Button_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Exit.ForeColor = System.Drawing.Color.DarkGray;
            this.Button_Exit.Location = new System.Drawing.Point(611, 389);
            this.Button_Exit.Name = "Button_Exit";
            this.Button_Exit.Size = new System.Drawing.Size(100, 42);
            this.Button_Exit.TabIndex = 156;
            this.Button_Exit.Text = "EXIT";
            this.Button_Exit.UseVisualStyleBackColor = false;
            // 
            // TabControl_Settings
            // 
            this.TabControl_Settings.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.TabControl_Settings.BackColor = System.Drawing.Color.Gray;
            this.TabControl_Settings.Controls.Add(this.TabPage_Launcher);
            this.TabControl_Settings.Controls.Add(this.TabPage_Game);
            this.TabControl_Settings.Controls.Add(this.TabPage_API);
            this.TabControl_Settings.Controls.Add(this.TabPage_About);
            this.TabControl_Settings.DoubleBufferTabPages = true;
            this.TabControl_Settings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabControl_Settings.ForeColor = System.Drawing.Color.DarkCyan;
            this.TabControl_Settings.HotTrack = true;
            this.TabControl_Settings.Location = new System.Drawing.Point(2, 5);
            this.TabControl_Settings.Name = "TabControl_Settings";
            this.TabControl_Settings.SelectedIndex = 0;
            this.TabControl_Settings.SelectedTabColor = System.Drawing.Color.Red;
            this.TabControl_Settings.Size = new System.Drawing.Size(834, 380);
            this.TabControl_Settings.TabColor = System.Drawing.Color.Yellow;
            this.TabControl_Settings.TabIndex = 159;
            // 
            // TabPage_Launcher
            // 
            this.TabPage_Launcher.Controls.Add(this.TabControl_Launcher);
            this.TabPage_Launcher.Location = new System.Drawing.Point(0, 26);
            this.TabPage_Launcher.Name = "TabPage_Launcher";
            this.TabPage_Launcher.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Launcher.Size = new System.Drawing.Size(834, 354);
            this.TabPage_Launcher.TabIndex = 0;
            this.TabPage_Launcher.Text = "Launcher";
            this.TabPage_Launcher.UseVisualStyleBackColor = true;
            // 
            // TabControl_Launcher
            // 
            this.TabControl_Launcher.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.TabControl_Launcher.Controls.Add(this.TabPage_Launcher_Downloader);
            this.TabControl_Launcher.Controls.Add(this.TabPage_Launcher_Proxy);
            this.TabControl_Launcher.Controls.Add(this.TabPage_Launcher_Miscellaneous);
            this.TabControl_Launcher.HotTrack = true;
            this.TabControl_Launcher.Location = new System.Drawing.Point(1, 3);
            this.TabControl_Launcher.Name = "TabControl_Launcher";
            this.TabControl_Launcher.SelectedIndex = 0;
            this.TabControl_Launcher.Size = new System.Drawing.Size(832, 354);
            this.TabControl_Launcher.TabIndex = 185;
            // 
            // TabPage_Launcher_Downloader
            // 
            this.TabPage_Launcher_Downloader.Controls.Add(this.Label_GameFiles_Downloader_Raw);
            this.TabPage_Launcher_Downloader.Controls.Add(this.Label_GameFiles_Downloader_Pack);
            this.TabPage_Launcher_Downloader.Controls.Add(this.Label_GameFiles_Downloader_LZMA);
            this.TabPage_Launcher_Downloader.Controls.Add(this.Panel_GameFiles_Downloader);
            this.TabPage_Launcher_Downloader.Controls.Add(this.Label_CDN_Current_Details);
            this.TabPage_Launcher_Downloader.Controls.Add(this.Label_WebClient_Timeout_Details);
            this.TabPage_Launcher_Downloader.Controls.Add(this.Label_GameFiles_Downloader_Details);
            this.TabPage_Launcher_Downloader.Controls.Add(this.Label_Alt_WebCalls_Details);
            this.TabPage_Launcher_Downloader.Controls.Add(this.Label_CDN_Current);
            this.TabPage_Launcher_Downloader.Controls.Add(this.Button_CDN_List);
            this.TabPage_Launcher_Downloader.Controls.Add(this.LinkLabel_CDN_Current);
            this.TabPage_Launcher_Downloader.Controls.Add(this.Label_GameFiles_Downloader);
            this.TabPage_Launcher_Downloader.Controls.Add(this.CheckBox_LZMA_Downloader);
            this.TabPage_Launcher_Downloader.Controls.Add(this.CheckBox_Alt_WebCalls);
            this.TabPage_Launcher_Downloader.Controls.Add(this.Label_WebClient_Timeout);
            this.TabPage_Launcher_Downloader.Controls.Add(this.NumericUpDown_WebClient_Timeout);
            this.TabPage_Launcher_Downloader.Location = new System.Drawing.Point(0, 26);
            this.TabPage_Launcher_Downloader.Name = "TabPage_Launcher_Downloader";
            this.TabPage_Launcher_Downloader.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Launcher_Downloader.Size = new System.Drawing.Size(832, 328);
            this.TabPage_Launcher_Downloader.TabIndex = 0;
            this.TabPage_Launcher_Downloader.Text = "Downloader";
            this.TabPage_Launcher_Downloader.UseVisualStyleBackColor = true;
            // 
            // Label_GameFiles_Downloader_Raw
            // 
            this.Label_GameFiles_Downloader_Raw.BackColor = System.Drawing.Color.Transparent;
            this.Label_GameFiles_Downloader_Raw.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_GameFiles_Downloader_Raw.Location = new System.Drawing.Point(237, 231);
            this.Label_GameFiles_Downloader_Raw.Name = "Label_GameFiles_Downloader_Raw";
            this.Label_GameFiles_Downloader_Raw.Size = new System.Drawing.Size(576, 18);
            this.Label_GameFiles_Downloader_Raw.TabIndex = 192;
            this.Label_GameFiles_Downloader_Raw.Text = "Downloads files indivdually.";
            this.Label_GameFiles_Downloader_Raw.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_GameFiles_Downloader_Pack
            // 
            this.Label_GameFiles_Downloader_Pack.BackColor = System.Drawing.Color.Transparent;
            this.Label_GameFiles_Downloader_Pack.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_GameFiles_Downloader_Pack.Location = new System.Drawing.Point(237, 207);
            this.Label_GameFiles_Downloader_Pack.Name = "Label_GameFiles_Downloader_Pack";
            this.Label_GameFiles_Downloader_Pack.Size = new System.Drawing.Size(576, 18);
            this.Label_GameFiles_Downloader_Pack.TabIndex = 191;
            this.Label_GameFiles_Downloader_Pack.Text = "Downloads a single large file and extracts it. Download can be resumed if needed." +
    "";
            this.Label_GameFiles_Downloader_Pack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_GameFiles_Downloader_LZMA
            // 
            this.Label_GameFiles_Downloader_LZMA.BackColor = System.Drawing.Color.Transparent;
            this.Label_GameFiles_Downloader_LZMA.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_GameFiles_Downloader_LZMA.Location = new System.Drawing.Point(237, 183);
            this.Label_GameFiles_Downloader_LZMA.Name = "Label_GameFiles_Downloader_LZMA";
            this.Label_GameFiles_Downloader_LZMA.Size = new System.Drawing.Size(576, 18);
            this.Label_GameFiles_Downloader_LZMA.TabIndex = 190;
            this.Label_GameFiles_Downloader_LZMA.Text = "Downloads compressed files indivdually. Uses less bandwith.\r\n";
            this.Label_GameFiles_Downloader_LZMA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Panel_GameFiles_Downloader
            // 
            this.Panel_GameFiles_Downloader.BackColor = System.Drawing.Color.Transparent;
            this.Panel_GameFiles_Downloader.Controls.Add(this.Radio_Button_GameFiles_Downloader_LZMA);
            this.Panel_GameFiles_Downloader.Controls.Add(this.Radio_Button_GameFiles_Downloader_SBRW_Pack);
            this.Panel_GameFiles_Downloader.Controls.Add(this.Radio_Button_GameFiles_Downloader_Raw);
            this.Panel_GameFiles_Downloader.Location = new System.Drawing.Point(9, 178);
            this.Panel_GameFiles_Downloader.Name = "Panel_GameFiles_Downloader";
            this.Panel_GameFiles_Downloader.Size = new System.Drawing.Size(153, 77);
            this.Panel_GameFiles_Downloader.TabIndex = 189;
            this.Panel_GameFiles_Downloader.Tag = "SkidMarks";
            // 
            // Radio_Button_GameFiles_Downloader_LZMA
            // 
            this.Radio_Button_GameFiles_Downloader_LZMA.BackColor = System.Drawing.Color.Transparent;
            this.Radio_Button_GameFiles_Downloader_LZMA.Font = new System.Drawing.Font("DejaVu Sans", 9F);
            this.Radio_Button_GameFiles_Downloader_LZMA.Location = new System.Drawing.Point(5, 5);
            this.Radio_Button_GameFiles_Downloader_LZMA.Name = "Radio_Button_GameFiles_Downloader_LZMA";
            this.Radio_Button_GameFiles_Downloader_LZMA.Size = new System.Drawing.Size(80, 18);
            this.Radio_Button_GameFiles_Downloader_LZMA.TabIndex = 95;
            this.Radio_Button_GameFiles_Downloader_LZMA.TabStop = true;
            this.Radio_Button_GameFiles_Downloader_LZMA.Tag = "0";
            this.Radio_Button_GameFiles_Downloader_LZMA.Text = "LZMA";
            this.Radio_Button_GameFiles_Downloader_LZMA.UseVisualStyleBackColor = false;
            // 
            // Radio_Button_GameFiles_Downloader_SBRW_Pack
            // 
            this.Radio_Button_GameFiles_Downloader_SBRW_Pack.BackColor = System.Drawing.Color.Transparent;
            this.Radio_Button_GameFiles_Downloader_SBRW_Pack.Font = new System.Drawing.Font("DejaVu Sans", 9F);
            this.Radio_Button_GameFiles_Downloader_SBRW_Pack.Location = new System.Drawing.Point(5, 29);
            this.Radio_Button_GameFiles_Downloader_SBRW_Pack.Name = "Radio_Button_GameFiles_Downloader_SBRW_Pack";
            this.Radio_Button_GameFiles_Downloader_SBRW_Pack.Size = new System.Drawing.Size(80, 18);
            this.Radio_Button_GameFiles_Downloader_SBRW_Pack.TabIndex = 96;
            this.Radio_Button_GameFiles_Downloader_SBRW_Pack.TabStop = true;
            this.Radio_Button_GameFiles_Downloader_SBRW_Pack.Tag = "1";
            this.Radio_Button_GameFiles_Downloader_SBRW_Pack.Text = "Pack";
            this.Radio_Button_GameFiles_Downloader_SBRW_Pack.UseVisualStyleBackColor = false;
            // 
            // Radio_Button_GameFiles_Downloader_Raw
            // 
            this.Radio_Button_GameFiles_Downloader_Raw.BackColor = System.Drawing.Color.Transparent;
            this.Radio_Button_GameFiles_Downloader_Raw.Font = new System.Drawing.Font("DejaVu Sans", 9F);
            this.Radio_Button_GameFiles_Downloader_Raw.Location = new System.Drawing.Point(5, 53);
            this.Radio_Button_GameFiles_Downloader_Raw.Name = "Radio_Button_GameFiles_Downloader_Raw";
            this.Radio_Button_GameFiles_Downloader_Raw.Size = new System.Drawing.Size(80, 18);
            this.Radio_Button_GameFiles_Downloader_Raw.TabIndex = 97;
            this.Radio_Button_GameFiles_Downloader_Raw.TabStop = true;
            this.Radio_Button_GameFiles_Downloader_Raw.Tag = "2";
            this.Radio_Button_GameFiles_Downloader_Raw.Text = "Raw";
            this.Radio_Button_GameFiles_Downloader_Raw.UseVisualStyleBackColor = false;
            // 
            // Label_CDN_Current_Details
            // 
            this.Label_CDN_Current_Details.BackColor = System.Drawing.Color.Transparent;
            this.Label_CDN_Current_Details.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_CDN_Current_Details.Location = new System.Drawing.Point(237, 123);
            this.Label_CDN_Current_Details.Name = "Label_CDN_Current_Details";
            this.Label_CDN_Current_Details.Size = new System.Drawing.Size(576, 18);
            this.Label_CDN_Current_Details.TabIndex = 188;
            this.Label_CDN_Current_Details.Text = "Download Location for Fetching the base Game and VerifyHash files.";
            this.Label_CDN_Current_Details.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_WebClient_Timeout_Details
            // 
            this.Label_WebClient_Timeout_Details.BackColor = System.Drawing.Color.Transparent;
            this.Label_WebClient_Timeout_Details.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_WebClient_Timeout_Details.Location = new System.Drawing.Point(237, 55);
            this.Label_WebClient_Timeout_Details.Name = "Label_WebClient_Timeout_Details";
            this.Label_WebClient_Timeout_Details.Size = new System.Drawing.Size(576, 18);
            this.Label_WebClient_Timeout_Details.TabIndex = 187;
            this.Label_WebClient_Timeout_Details.Text = "Changes Game Files Downloader System\r\n";
            this.Label_WebClient_Timeout_Details.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_GameFiles_Downloader_Details
            // 
            this.Label_GameFiles_Downloader_Details.BackColor = System.Drawing.Color.Transparent;
            this.Label_GameFiles_Downloader_Details.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_GameFiles_Downloader_Details.Location = new System.Drawing.Point(237, 158);
            this.Label_GameFiles_Downloader_Details.Name = "Label_GameFiles_Downloader_Details";
            this.Label_GameFiles_Downloader_Details.Size = new System.Drawing.Size(576, 18);
            this.Label_GameFiles_Downloader_Details.TabIndex = 186;
            this.Label_GameFiles_Downloader_Details.Text = "Changes Game Files Downloader System\r\n";
            this.Label_GameFiles_Downloader_Details.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_Alt_WebCalls_Details
            // 
            this.Label_Alt_WebCalls_Details.BackColor = System.Drawing.Color.Transparent;
            this.Label_Alt_WebCalls_Details.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_Alt_WebCalls_Details.Location = new System.Drawing.Point(237, 10);
            this.Label_Alt_WebCalls_Details.Name = "Label_Alt_WebCalls_Details";
            this.Label_Alt_WebCalls_Details.Size = new System.Drawing.Size(576, 18);
            this.Label_Alt_WebCalls_Details.TabIndex = 185;
            this.Label_Alt_WebCalls_Details.Text = "Changes the internal method used by Launcher for Communications. Does not affect " +
    "Proxy.\r\n";
            this.Label_Alt_WebCalls_Details.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_CDN_Current
            // 
            this.Label_CDN_Current.BackColor = System.Drawing.Color.Transparent;
            this.Label_CDN_Current.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_CDN_Current.Location = new System.Drawing.Point(7, 81);
            this.Label_CDN_Current.Name = "Label_CDN_Current";
            this.Label_CDN_Current.Size = new System.Drawing.Size(224, 16);
            this.Label_CDN_Current.TabIndex = 175;
            this.Label_CDN_Current.Text = "CURRENT CDN:";
            this.Label_CDN_Current.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button_CDN_List
            // 
            this.Button_CDN_List.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Button_CDN_List.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_CDN_List.Location = new System.Drawing.Point(10, 124);
            this.Button_CDN_List.Name = "Button_CDN_List";
            this.Button_CDN_List.Size = new System.Drawing.Size(154, 25);
            this.Button_CDN_List.TabIndex = 184;
            this.Button_CDN_List.Text = "Change CDN";
            this.Button_CDN_List.UseVisualStyleBackColor = false;
            // 
            // LinkLabel_CDN_Current
            // 
            this.LinkLabel_CDN_Current.BackColor = System.Drawing.Color.Transparent;
            this.LinkLabel_CDN_Current.Location = new System.Drawing.Point(7, 102);
            this.LinkLabel_CDN_Current.Name = "LinkLabel_CDN_Current";
            this.LinkLabel_CDN_Current.Size = new System.Drawing.Size(806, 16);
            this.LinkLabel_CDN_Current.TabIndex = 176;
            this.LinkLabel_CDN_Current.TabStop = true;
            this.LinkLabel_CDN_Current.Text = "https://localhost";
            // 
            // Label_GameFiles_Downloader
            // 
            this.Label_GameFiles_Downloader.BackColor = System.Drawing.Color.Transparent;
            this.Label_GameFiles_Downloader.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_GameFiles_Downloader.Location = new System.Drawing.Point(8, 158);
            this.Label_GameFiles_Downloader.Name = "Label_GameFiles_Downloader";
            this.Label_GameFiles_Downloader.Size = new System.Drawing.Size(223, 16);
            this.Label_GameFiles_Downloader.TabIndex = 183;
            this.Label_GameFiles_Downloader.Text = "Game Files Downloader:";
            this.Label_GameFiles_Downloader.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // CheckBox_LZMA_Downloader
            // 
            this.CheckBox_LZMA_Downloader.BackColor = System.Drawing.Color.Transparent;
            this.CheckBox_LZMA_Downloader.ForeColor = System.Drawing.Color.Black;
            this.CheckBox_LZMA_Downloader.Location = new System.Drawing.Point(11, 281);
            this.CheckBox_LZMA_Downloader.Name = "CheckBox_LZMA_Downloader";
            this.CheckBox_LZMA_Downloader.Size = new System.Drawing.Size(225, 18);
            this.CheckBox_LZMA_Downloader.TabIndex = 159;
            this.CheckBox_LZMA_Downloader.Text = "Enable LZMA Downloader";
            this.CheckBox_LZMA_Downloader.UseVisualStyleBackColor = false;
            // 
            // CheckBox_Alt_WebCalls
            // 
            this.CheckBox_Alt_WebCalls.BackColor = System.Drawing.Color.Transparent;
            this.CheckBox_Alt_WebCalls.ForeColor = System.Drawing.Color.Black;
            this.CheckBox_Alt_WebCalls.Location = new System.Drawing.Point(7, 10);
            this.CheckBox_Alt_WebCalls.Name = "CheckBox_Alt_WebCalls";
            this.CheckBox_Alt_WebCalls.Size = new System.Drawing.Size(225, 18);
            this.CheckBox_Alt_WebCalls.TabIndex = 158;
            this.CheckBox_Alt_WebCalls.Text = "Enable Alternative WebCalls";
            this.CheckBox_Alt_WebCalls.UseVisualStyleBackColor = false;
            // 
            // Label_WebClient_Timeout
            // 
            this.Label_WebClient_Timeout.BackColor = System.Drawing.Color.Transparent;
            this.Label_WebClient_Timeout.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold);
            this.Label_WebClient_Timeout.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_WebClient_Timeout.Location = new System.Drawing.Point(6, 35);
            this.Label_WebClient_Timeout.Name = "Label_WebClient_Timeout";
            this.Label_WebClient_Timeout.Size = new System.Drawing.Size(226, 16);
            this.Label_WebClient_Timeout.TabIndex = 165;
            this.Label_WebClient_Timeout.Text = "Web Client Timeout:";
            this.Label_WebClient_Timeout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NumericUpDown_WebClient_Timeout
            // 
            this.NumericUpDown_WebClient_Timeout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.NumericUpDown_WebClient_Timeout.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NumericUpDown_WebClient_Timeout.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold);
            this.NumericUpDown_WebClient_Timeout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.NumericUpDown_WebClient_Timeout.Location = new System.Drawing.Point(10, 55);
            this.NumericUpDown_WebClient_Timeout.Maximum = new decimal(new int[] {
            179,
            0,
            0,
            0});
            this.NumericUpDown_WebClient_Timeout.Name = "NumericUpDown_WebClient_Timeout";
            this.NumericUpDown_WebClient_Timeout.Size = new System.Drawing.Size(61, 17);
            this.NumericUpDown_WebClient_Timeout.TabIndex = 166;
            this.NumericUpDown_WebClient_Timeout.Tag = "WebClientTimeNumeric";
            this.NumericUpDown_WebClient_Timeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TabPage_Launcher_Proxy
            // 
            this.TabPage_Launcher_Proxy.Controls.Add(this.Button_Launcher_logs);
            this.TabPage_Launcher_Proxy.Controls.Add(this.Label_Proxy_Logging_None);
            this.TabPage_Launcher_Proxy.Controls.Add(this.Label_Proxy_Logging_Responses);
            this.TabPage_Launcher_Proxy.Controls.Add(this.Label_Proxy_Logging_Requests);
            this.TabPage_Launcher_Proxy.Controls.Add(this.Label_Proxy_Logging_Errors);
            this.TabPage_Launcher_Proxy.Controls.Add(this.Label_Proxy_Logging_All);
            this.TabPage_Launcher_Proxy.Controls.Add(this.Panel_Proxy_Logging);
            this.TabPage_Launcher_Proxy.Controls.Add(this.Label_Proxy_Logging_Details);
            this.TabPage_Launcher_Proxy.Controls.Add(this.Label_Proxy_Logging);
            this.TabPage_Launcher_Proxy.Controls.Add(this.Label_Proxy_Port);
            this.TabPage_Launcher_Proxy.Controls.Add(this.NumericUpDown_Proxy_Port);
            this.TabPage_Launcher_Proxy.Controls.Add(this.CheckBox_Proxy);
            this.TabPage_Launcher_Proxy.Controls.Add(this.CheckBox_Proxy_Domain);
            this.TabPage_Launcher_Proxy.Controls.Add(this.CheckBox_Host_to_IP);
            this.TabPage_Launcher_Proxy.Location = new System.Drawing.Point(0, 26);
            this.TabPage_Launcher_Proxy.Name = "TabPage_Launcher_Proxy";
            this.TabPage_Launcher_Proxy.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Launcher_Proxy.Size = new System.Drawing.Size(832, 328);
            this.TabPage_Launcher_Proxy.TabIndex = 1;
            this.TabPage_Launcher_Proxy.Text = "Proxy";
            this.TabPage_Launcher_Proxy.UseVisualStyleBackColor = true;
            // 
            // Button_Launcher_logs
            // 
            this.Button_Launcher_logs.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Button_Launcher_logs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Launcher_logs.Location = new System.Drawing.Point(8, 261);
            this.Button_Launcher_logs.Name = "Button_Launcher_logs";
            this.Button_Launcher_logs.Size = new System.Drawing.Size(154, 25);
            this.Button_Launcher_logs.TabIndex = 201;
            this.Button_Launcher_logs.Text = "Clear Launcher Logs";
            this.Button_Launcher_logs.UseVisualStyleBackColor = false;
            // 
            // Label_Proxy_Logging_None
            // 
            this.Label_Proxy_Logging_None.BackColor = System.Drawing.Color.Transparent;
            this.Label_Proxy_Logging_None.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_Proxy_Logging_None.Location = new System.Drawing.Point(236, 229);
            this.Label_Proxy_Logging_None.Name = "Label_Proxy_Logging_None";
            this.Label_Proxy_Logging_None.Size = new System.Drawing.Size(576, 18);
            this.Label_Proxy_Logging_None.TabIndex = 200;
            this.Label_Proxy_Logging_None.Text = "Disables Logging and Improves Performance";
            this.Label_Proxy_Logging_None.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_Proxy_Logging_Responses
            // 
            this.Label_Proxy_Logging_Responses.BackColor = System.Drawing.Color.Transparent;
            this.Label_Proxy_Logging_Responses.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_Proxy_Logging_Responses.Location = new System.Drawing.Point(236, 205);
            this.Label_Proxy_Logging_Responses.Name = "Label_Proxy_Logging_Responses";
            this.Label_Proxy_Logging_Responses.Size = new System.Drawing.Size(576, 18);
            this.Label_Proxy_Logging_Responses.TabIndex = 199;
            this.Label_Proxy_Logging_Responses.Text = "Saves Responses Only";
            this.Label_Proxy_Logging_Responses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_Proxy_Logging_Requests
            // 
            this.Label_Proxy_Logging_Requests.BackColor = System.Drawing.Color.Transparent;
            this.Label_Proxy_Logging_Requests.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_Proxy_Logging_Requests.Location = new System.Drawing.Point(236, 181);
            this.Label_Proxy_Logging_Requests.Name = "Label_Proxy_Logging_Requests";
            this.Label_Proxy_Logging_Requests.Size = new System.Drawing.Size(576, 18);
            this.Label_Proxy_Logging_Requests.TabIndex = 198;
            this.Label_Proxy_Logging_Requests.Text = "Saves Requests Only";
            this.Label_Proxy_Logging_Requests.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_Proxy_Logging_Errors
            // 
            this.Label_Proxy_Logging_Errors.BackColor = System.Drawing.Color.Transparent;
            this.Label_Proxy_Logging_Errors.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_Proxy_Logging_Errors.Location = new System.Drawing.Point(236, 157);
            this.Label_Proxy_Logging_Errors.Name = "Label_Proxy_Logging_Errors";
            this.Label_Proxy_Logging_Errors.Size = new System.Drawing.Size(576, 18);
            this.Label_Proxy_Logging_Errors.TabIndex = 197;
            this.Label_Proxy_Logging_Errors.Text = "Saves Errors Only (Recommended)";
            this.Label_Proxy_Logging_Errors.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_Proxy_Logging_All
            // 
            this.Label_Proxy_Logging_All.BackColor = System.Drawing.Color.Transparent;
            this.Label_Proxy_Logging_All.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_Proxy_Logging_All.Location = new System.Drawing.Point(236, 133);
            this.Label_Proxy_Logging_All.Name = "Label_Proxy_Logging_All";
            this.Label_Proxy_Logging_All.Size = new System.Drawing.Size(576, 18);
            this.Label_Proxy_Logging_All.TabIndex = 196;
            this.Label_Proxy_Logging_All.Text = "Saves all Information";
            this.Label_Proxy_Logging_All.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Panel_Proxy_Logging
            // 
            this.Panel_Proxy_Logging.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Proxy_Logging.Controls.Add(this.Radio_Button_Proxy_Logging_None);
            this.Panel_Proxy_Logging.Controls.Add(this.Radio_Button_Proxy_Logging_Responses);
            this.Panel_Proxy_Logging.Controls.Add(this.Radio_Button_Proxy_Logging_All);
            this.Panel_Proxy_Logging.Controls.Add(this.Radio_Button_Proxy_Logging_Errors);
            this.Panel_Proxy_Logging.Controls.Add(this.Radio_Button_Proxy_Logging_Requests);
            this.Panel_Proxy_Logging.Location = new System.Drawing.Point(8, 128);
            this.Panel_Proxy_Logging.Name = "Panel_Proxy_Logging";
            this.Panel_Proxy_Logging.Size = new System.Drawing.Size(153, 125);
            this.Panel_Proxy_Logging.TabIndex = 195;
            this.Panel_Proxy_Logging.Tag = "SkidMarks";
            // 
            // Radio_Button_Proxy_Logging_None
            // 
            this.Radio_Button_Proxy_Logging_None.BackColor = System.Drawing.Color.Transparent;
            this.Radio_Button_Proxy_Logging_None.Font = new System.Drawing.Font("DejaVu Sans", 9F);
            this.Radio_Button_Proxy_Logging_None.Location = new System.Drawing.Point(6, 101);
            this.Radio_Button_Proxy_Logging_None.Name = "Radio_Button_Proxy_Logging_None";
            this.Radio_Button_Proxy_Logging_None.Size = new System.Drawing.Size(141, 18);
            this.Radio_Button_Proxy_Logging_None.TabIndex = 99;
            this.Radio_Button_Proxy_Logging_None.TabStop = true;
            this.Radio_Button_Proxy_Logging_None.Tag = "2";
            this.Radio_Button_Proxy_Logging_None.Text = "None";
            this.Radio_Button_Proxy_Logging_None.UseVisualStyleBackColor = false;
            // 
            // Radio_Button_Proxy_Logging_Responses
            // 
            this.Radio_Button_Proxy_Logging_Responses.BackColor = System.Drawing.Color.Transparent;
            this.Radio_Button_Proxy_Logging_Responses.Font = new System.Drawing.Font("DejaVu Sans", 9F);
            this.Radio_Button_Proxy_Logging_Responses.Location = new System.Drawing.Point(5, 77);
            this.Radio_Button_Proxy_Logging_Responses.Name = "Radio_Button_Proxy_Logging_Responses";
            this.Radio_Button_Proxy_Logging_Responses.Size = new System.Drawing.Size(141, 18);
            this.Radio_Button_Proxy_Logging_Responses.TabIndex = 98;
            this.Radio_Button_Proxy_Logging_Responses.TabStop = true;
            this.Radio_Button_Proxy_Logging_Responses.Tag = "2";
            this.Radio_Button_Proxy_Logging_Responses.Text = "Responses";
            this.Radio_Button_Proxy_Logging_Responses.UseVisualStyleBackColor = false;
            // 
            // Radio_Button_Proxy_Logging_All
            // 
            this.Radio_Button_Proxy_Logging_All.BackColor = System.Drawing.Color.Transparent;
            this.Radio_Button_Proxy_Logging_All.Font = new System.Drawing.Font("DejaVu Sans", 9F);
            this.Radio_Button_Proxy_Logging_All.Location = new System.Drawing.Point(5, 5);
            this.Radio_Button_Proxy_Logging_All.Name = "Radio_Button_Proxy_Logging_All";
            this.Radio_Button_Proxy_Logging_All.Size = new System.Drawing.Size(141, 18);
            this.Radio_Button_Proxy_Logging_All.TabIndex = 95;
            this.Radio_Button_Proxy_Logging_All.TabStop = true;
            this.Radio_Button_Proxy_Logging_All.Tag = "0";
            this.Radio_Button_Proxy_Logging_All.Text = "All";
            this.Radio_Button_Proxy_Logging_All.UseVisualStyleBackColor = false;
            // 
            // Radio_Button_Proxy_Logging_Errors
            // 
            this.Radio_Button_Proxy_Logging_Errors.BackColor = System.Drawing.Color.Transparent;
            this.Radio_Button_Proxy_Logging_Errors.Font = new System.Drawing.Font("DejaVu Sans", 9F);
            this.Radio_Button_Proxy_Logging_Errors.Location = new System.Drawing.Point(5, 29);
            this.Radio_Button_Proxy_Logging_Errors.Name = "Radio_Button_Proxy_Logging_Errors";
            this.Radio_Button_Proxy_Logging_Errors.Size = new System.Drawing.Size(141, 18);
            this.Radio_Button_Proxy_Logging_Errors.TabIndex = 96;
            this.Radio_Button_Proxy_Logging_Errors.TabStop = true;
            this.Radio_Button_Proxy_Logging_Errors.Tag = "1";
            this.Radio_Button_Proxy_Logging_Errors.Text = "Errors";
            this.Radio_Button_Proxy_Logging_Errors.UseVisualStyleBackColor = false;
            // 
            // Radio_Button_Proxy_Logging_Requests
            // 
            this.Radio_Button_Proxy_Logging_Requests.BackColor = System.Drawing.Color.Transparent;
            this.Radio_Button_Proxy_Logging_Requests.Font = new System.Drawing.Font("DejaVu Sans", 9F);
            this.Radio_Button_Proxy_Logging_Requests.Location = new System.Drawing.Point(5, 53);
            this.Radio_Button_Proxy_Logging_Requests.Name = "Radio_Button_Proxy_Logging_Requests";
            this.Radio_Button_Proxy_Logging_Requests.Size = new System.Drawing.Size(141, 18);
            this.Radio_Button_Proxy_Logging_Requests.TabIndex = 97;
            this.Radio_Button_Proxy_Logging_Requests.TabStop = true;
            this.Radio_Button_Proxy_Logging_Requests.Tag = "2";
            this.Radio_Button_Proxy_Logging_Requests.Text = "Requests";
            this.Radio_Button_Proxy_Logging_Requests.UseVisualStyleBackColor = false;
            // 
            // Label_Proxy_Logging_Details
            // 
            this.Label_Proxy_Logging_Details.BackColor = System.Drawing.Color.Transparent;
            this.Label_Proxy_Logging_Details.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_Proxy_Logging_Details.Location = new System.Drawing.Point(236, 108);
            this.Label_Proxy_Logging_Details.Name = "Label_Proxy_Logging_Details";
            this.Label_Proxy_Logging_Details.Size = new System.Drawing.Size(576, 18);
            this.Label_Proxy_Logging_Details.TabIndex = 194;
            this.Label_Proxy_Logging_Details.Text = "Changes the Logging System being saved to a File on the Drive";
            this.Label_Proxy_Logging_Details.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_Proxy_Logging
            // 
            this.Label_Proxy_Logging.BackColor = System.Drawing.Color.Transparent;
            this.Label_Proxy_Logging.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_Proxy_Logging.Location = new System.Drawing.Point(7, 108);
            this.Label_Proxy_Logging.Name = "Label_Proxy_Logging";
            this.Label_Proxy_Logging.Size = new System.Drawing.Size(223, 16);
            this.Label_Proxy_Logging.TabIndex = 193;
            this.Label_Proxy_Logging.Text = "Log to File:";
            this.Label_Proxy_Logging.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // Label_Proxy_Port
            // 
            this.Label_Proxy_Port.BackColor = System.Drawing.Color.Transparent;
            this.Label_Proxy_Port.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold);
            this.Label_Proxy_Port.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_Proxy_Port.Location = new System.Drawing.Point(6, 84);
            this.Label_Proxy_Port.Name = "Label_Proxy_Port";
            this.Label_Proxy_Port.Size = new System.Drawing.Size(95, 14);
            this.Label_Proxy_Port.TabIndex = 181;
            this.Label_Proxy_Port.Text = "Proxy Port:";
            this.Label_Proxy_Port.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NumericUpDown_Proxy_Port
            // 
            this.NumericUpDown_Proxy_Port.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.NumericUpDown_Proxy_Port.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NumericUpDown_Proxy_Port.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold);
            this.NumericUpDown_Proxy_Port.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.NumericUpDown_Proxy_Port.Location = new System.Drawing.Point(107, 84);
            this.NumericUpDown_Proxy_Port.Maximum = new decimal(new int[] {
            65353,
            0,
            0,
            0});
            this.NumericUpDown_Proxy_Port.Name = "NumericUpDown_Proxy_Port";
            this.NumericUpDown_Proxy_Port.Size = new System.Drawing.Size(61, 17);
            this.NumericUpDown_Proxy_Port.TabIndex = 182;
            this.NumericUpDown_Proxy_Port.Tag = "ProxypPortNumeric";
            this.NumericUpDown_Proxy_Port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CheckBox_Proxy
            // 
            this.CheckBox_Proxy.BackColor = System.Drawing.Color.Transparent;
            this.CheckBox_Proxy.ForeColor = System.Drawing.Color.Black;
            this.CheckBox_Proxy.Location = new System.Drawing.Point(9, 10);
            this.CheckBox_Proxy.Name = "CheckBox_Proxy";
            this.CheckBox_Proxy.Size = new System.Drawing.Size(222, 18);
            this.CheckBox_Proxy.TabIndex = 177;
            this.CheckBox_Proxy.Text = "Disable Proxy";
            this.CheckBox_Proxy.UseVisualStyleBackColor = false;
            // 
            // CheckBox_Proxy_Domain
            // 
            this.CheckBox_Proxy_Domain.BackColor = System.Drawing.Color.Transparent;
            this.CheckBox_Proxy_Domain.ForeColor = System.Drawing.Color.Black;
            this.CheckBox_Proxy_Domain.Location = new System.Drawing.Point(9, 58);
            this.CheckBox_Proxy_Domain.Name = "CheckBox_Proxy_Domain";
            this.CheckBox_Proxy_Domain.Size = new System.Drawing.Size(222, 18);
            this.CheckBox_Proxy_Domain.TabIndex = 178;
            this.CheckBox_Proxy_Domain.Text = "Enable Proxy Domain";
            this.CheckBox_Proxy_Domain.UseVisualStyleBackColor = false;
            // 
            // CheckBox_Host_to_IP
            // 
            this.CheckBox_Host_to_IP.BackColor = System.Drawing.Color.Transparent;
            this.CheckBox_Host_to_IP.ForeColor = System.Drawing.Color.Black;
            this.CheckBox_Host_to_IP.Location = new System.Drawing.Point(9, 34);
            this.CheckBox_Host_to_IP.Name = "CheckBox_Host_to_IP";
            this.CheckBox_Host_to_IP.Size = new System.Drawing.Size(222, 18);
            this.CheckBox_Host_to_IP.TabIndex = 162;
            this.CheckBox_Host_to_IP.Text = "Disable Legacy IP Converter";
            this.CheckBox_Host_to_IP.UseVisualStyleBackColor = false;
            // 
            // TabPage_Launcher_Miscellaneous
            // 
            this.TabPage_Launcher_Miscellaneous.Controls.Add(this.Label_Launcher_Builds_Branch_Developer);
            this.TabPage_Launcher_Miscellaneous.Controls.Add(this.Label_Launcher_Builds_Branch_Beta);
            this.TabPage_Launcher_Miscellaneous.Controls.Add(this.Label_Launcher_Builds_Branch_Stable);
            this.TabPage_Launcher_Miscellaneous.Controls.Add(this.Panel_Launcher_Builds_Branch);
            this.TabPage_Launcher_Miscellaneous.Controls.Add(this.Label_Launcher_Builds_Branch_Details);
            this.TabPage_Launcher_Miscellaneous.Controls.Add(this.Label_Launcher_Builds_Branch);
            this.TabPage_Launcher_Miscellaneous.Controls.Add(this.CheckBox_RPC);
            this.TabPage_Launcher_Miscellaneous.Controls.Add(this.LinkLabel_Launcher_Path);
            this.TabPage_Launcher_Miscellaneous.Controls.Add(this.CheckBox_JSON_Update_Cache);
            this.TabPage_Launcher_Miscellaneous.Controls.Add(this.Label_Launcher_Path);
            this.TabPage_Launcher_Miscellaneous.Controls.Add(this.CheckBox_Theme_Support);
            this.TabPage_Launcher_Miscellaneous.Controls.Add(this.CheckBox_Opt_Insider);
            this.TabPage_Launcher_Miscellaneous.Location = new System.Drawing.Point(0, 26);
            this.TabPage_Launcher_Miscellaneous.Name = "TabPage_Launcher_Miscellaneous";
            this.TabPage_Launcher_Miscellaneous.Size = new System.Drawing.Size(832, 328);
            this.TabPage_Launcher_Miscellaneous.TabIndex = 2;
            this.TabPage_Launcher_Miscellaneous.Text = "Miscellaneous";
            this.TabPage_Launcher_Miscellaneous.UseVisualStyleBackColor = true;
            // 
            // Label_Launcher_Builds_Branch_Developer
            // 
            this.Label_Launcher_Builds_Branch_Developer.BackColor = System.Drawing.Color.Transparent;
            this.Label_Launcher_Builds_Branch_Developer.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_Launcher_Builds_Branch_Developer.Location = new System.Drawing.Point(236, 155);
            this.Label_Launcher_Builds_Branch_Developer.Name = "Label_Launcher_Builds_Branch_Developer";
            this.Label_Launcher_Builds_Branch_Developer.Size = new System.Drawing.Size(576, 18);
            this.Label_Launcher_Builds_Branch_Developer.TabIndex = 198;
            this.Label_Launcher_Builds_Branch_Developer.Text = "Bleading Edge Updates. Unofficial and is not allowed on most servers. Can Break t" +
    "hings";
            this.Label_Launcher_Builds_Branch_Developer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_Launcher_Builds_Branch_Beta
            // 
            this.Label_Launcher_Builds_Branch_Beta.BackColor = System.Drawing.Color.Transparent;
            this.Label_Launcher_Builds_Branch_Beta.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_Launcher_Builds_Branch_Beta.Location = new System.Drawing.Point(236, 131);
            this.Label_Launcher_Builds_Branch_Beta.Name = "Label_Launcher_Builds_Branch_Beta";
            this.Label_Launcher_Builds_Branch_Beta.Size = new System.Drawing.Size(576, 18);
            this.Label_Launcher_Builds_Branch_Beta.TabIndex = 197;
            this.Label_Launcher_Builds_Branch_Beta.Text = "Insider/Beta Builds if available, otherwise, Release Builds";
            this.Label_Launcher_Builds_Branch_Beta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_Launcher_Builds_Branch_Stable
            // 
            this.Label_Launcher_Builds_Branch_Stable.BackColor = System.Drawing.Color.Transparent;
            this.Label_Launcher_Builds_Branch_Stable.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_Launcher_Builds_Branch_Stable.Location = new System.Drawing.Point(236, 107);
            this.Label_Launcher_Builds_Branch_Stable.Name = "Label_Launcher_Builds_Branch_Stable";
            this.Label_Launcher_Builds_Branch_Stable.Size = new System.Drawing.Size(576, 18);
            this.Label_Launcher_Builds_Branch_Stable.TabIndex = 196;
            this.Label_Launcher_Builds_Branch_Stable.Text = "Only Official Release Builds\r\n";
            this.Label_Launcher_Builds_Branch_Stable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Panel_Launcher_Builds_Branch
            // 
            this.Panel_Launcher_Builds_Branch.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Launcher_Builds_Branch.Controls.Add(this.Radio_Button_Launcher_Builds_Branch_Stable);
            this.Panel_Launcher_Builds_Branch.Controls.Add(this.Radio_Button_Launcher_Builds_Branch_Beta);
            this.Panel_Launcher_Builds_Branch.Controls.Add(this.Radio_Button_Launcher_Builds_Branch_Developer);
            this.Panel_Launcher_Builds_Branch.Location = new System.Drawing.Point(8, 102);
            this.Panel_Launcher_Builds_Branch.Name = "Panel_Launcher_Builds_Branch";
            this.Panel_Launcher_Builds_Branch.Size = new System.Drawing.Size(153, 77);
            this.Panel_Launcher_Builds_Branch.TabIndex = 195;
            this.Panel_Launcher_Builds_Branch.Tag = "SkidMarks";
            // 
            // Radio_Button_Launcher_Builds_Branch_Stable
            // 
            this.Radio_Button_Launcher_Builds_Branch_Stable.BackColor = System.Drawing.Color.Transparent;
            this.Radio_Button_Launcher_Builds_Branch_Stable.Font = new System.Drawing.Font("DejaVu Sans", 9F);
            this.Radio_Button_Launcher_Builds_Branch_Stable.Location = new System.Drawing.Point(5, 5);
            this.Radio_Button_Launcher_Builds_Branch_Stable.Name = "Radio_Button_Launcher_Builds_Branch_Stable";
            this.Radio_Button_Launcher_Builds_Branch_Stable.Size = new System.Drawing.Size(143, 18);
            this.Radio_Button_Launcher_Builds_Branch_Stable.TabIndex = 95;
            this.Radio_Button_Launcher_Builds_Branch_Stable.TabStop = true;
            this.Radio_Button_Launcher_Builds_Branch_Stable.Tag = "0";
            this.Radio_Button_Launcher_Builds_Branch_Stable.Text = "Stable";
            this.Radio_Button_Launcher_Builds_Branch_Stable.UseVisualStyleBackColor = false;
            // 
            // Radio_Button_Launcher_Builds_Branch_Beta
            // 
            this.Radio_Button_Launcher_Builds_Branch_Beta.BackColor = System.Drawing.Color.Transparent;
            this.Radio_Button_Launcher_Builds_Branch_Beta.Font = new System.Drawing.Font("DejaVu Sans", 9F);
            this.Radio_Button_Launcher_Builds_Branch_Beta.Location = new System.Drawing.Point(5, 29);
            this.Radio_Button_Launcher_Builds_Branch_Beta.Name = "Radio_Button_Launcher_Builds_Branch_Beta";
            this.Radio_Button_Launcher_Builds_Branch_Beta.Size = new System.Drawing.Size(143, 18);
            this.Radio_Button_Launcher_Builds_Branch_Beta.TabIndex = 96;
            this.Radio_Button_Launcher_Builds_Branch_Beta.TabStop = true;
            this.Radio_Button_Launcher_Builds_Branch_Beta.Tag = "1";
            this.Radio_Button_Launcher_Builds_Branch_Beta.Text = "Beta";
            this.Radio_Button_Launcher_Builds_Branch_Beta.UseVisualStyleBackColor = false;
            // 
            // Radio_Button_Launcher_Builds_Branch_Developer
            // 
            this.Radio_Button_Launcher_Builds_Branch_Developer.BackColor = System.Drawing.Color.Transparent;
            this.Radio_Button_Launcher_Builds_Branch_Developer.Font = new System.Drawing.Font("DejaVu Sans", 9F);
            this.Radio_Button_Launcher_Builds_Branch_Developer.Location = new System.Drawing.Point(5, 53);
            this.Radio_Button_Launcher_Builds_Branch_Developer.Name = "Radio_Button_Launcher_Builds_Branch_Developer";
            this.Radio_Button_Launcher_Builds_Branch_Developer.Size = new System.Drawing.Size(143, 18);
            this.Radio_Button_Launcher_Builds_Branch_Developer.TabIndex = 97;
            this.Radio_Button_Launcher_Builds_Branch_Developer.TabStop = true;
            this.Radio_Button_Launcher_Builds_Branch_Developer.Tag = "2";
            this.Radio_Button_Launcher_Builds_Branch_Developer.Text = "Developer";
            this.Radio_Button_Launcher_Builds_Branch_Developer.UseVisualStyleBackColor = false;
            // 
            // Label_Launcher_Builds_Branch_Details
            // 
            this.Label_Launcher_Builds_Branch_Details.BackColor = System.Drawing.Color.Transparent;
            this.Label_Launcher_Builds_Branch_Details.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_Launcher_Builds_Branch_Details.Location = new System.Drawing.Point(236, 82);
            this.Label_Launcher_Builds_Branch_Details.Name = "Label_Launcher_Builds_Branch_Details";
            this.Label_Launcher_Builds_Branch_Details.Size = new System.Drawing.Size(576, 18);
            this.Label_Launcher_Builds_Branch_Details.TabIndex = 194;
            this.Label_Launcher_Builds_Branch_Details.Text = "Changes Launcher Build Update prompts\r\n";
            this.Label_Launcher_Builds_Branch_Details.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_Launcher_Builds_Branch
            // 
            this.Label_Launcher_Builds_Branch.BackColor = System.Drawing.Color.Transparent;
            this.Label_Launcher_Builds_Branch.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_Launcher_Builds_Branch.Location = new System.Drawing.Point(7, 82);
            this.Label_Launcher_Builds_Branch.Name = "Label_Launcher_Builds_Branch";
            this.Label_Launcher_Builds_Branch.Size = new System.Drawing.Size(223, 16);
            this.Label_Launcher_Builds_Branch.TabIndex = 193;
            this.Label_Launcher_Builds_Branch.Text = "Launcher Builds Branch:";
            this.Label_Launcher_Builds_Branch.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // CheckBox_RPC
            // 
            this.CheckBox_RPC.BackColor = System.Drawing.Color.Transparent;
            this.CheckBox_RPC.ForeColor = System.Drawing.Color.Black;
            this.CheckBox_RPC.Location = new System.Drawing.Point(8, 8);
            this.CheckBox_RPC.Name = "CheckBox_RPC";
            this.CheckBox_RPC.Size = new System.Drawing.Size(222, 18);
            this.CheckBox_RPC.TabIndex = 155;
            this.CheckBox_RPC.Text = "Disable Discord RPC";
            this.CheckBox_RPC.UseVisualStyleBackColor = false;
            // 
            // LinkLabel_Launcher_Path
            // 
            this.LinkLabel_Launcher_Path.BackColor = System.Drawing.Color.Transparent;
            this.LinkLabel_Launcher_Path.Location = new System.Drawing.Point(5, 201);
            this.LinkLabel_Launcher_Path.Name = "LinkLabel_Launcher_Path";
            this.LinkLabel_Launcher_Path.Size = new System.Drawing.Size(360, 32);
            this.LinkLabel_Launcher_Path.TabIndex = 180;
            this.LinkLabel_Launcher_Path.TabStop = true;
            this.LinkLabel_Launcher_Path.Text = "ABC://Soapbox Race World";
            // 
            // CheckBox_JSON_Update_Cache
            // 
            this.CheckBox_JSON_Update_Cache.BackColor = System.Drawing.Color.Transparent;
            this.CheckBox_JSON_Update_Cache.ForeColor = System.Drawing.Color.Black;
            this.CheckBox_JSON_Update_Cache.Location = new System.Drawing.Point(8, 32);
            this.CheckBox_JSON_Update_Cache.Name = "CheckBox_JSON_Update_Cache";
            this.CheckBox_JSON_Update_Cache.Size = new System.Drawing.Size(222, 18);
            this.CheckBox_JSON_Update_Cache.TabIndex = 160;
            this.CheckBox_JSON_Update_Cache.Text = "Disable Frequent JSON Cache";
            this.CheckBox_JSON_Update_Cache.UseVisualStyleBackColor = false;
            // 
            // Label_Launcher_Path
            // 
            this.Label_Launcher_Path.BackColor = System.Drawing.Color.Transparent;
            this.Label_Launcher_Path.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_Launcher_Path.Location = new System.Drawing.Point(5, 186);
            this.Label_Launcher_Path.Name = "Label_Launcher_Path";
            this.Label_Launcher_Path.Size = new System.Drawing.Size(360, 14);
            this.Label_Launcher_Path.TabIndex = 179;
            this.Label_Launcher_Path.Text = "LAUNCHER FOLDER:";
            this.Label_Launcher_Path.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CheckBox_Theme_Support
            // 
            this.CheckBox_Theme_Support.BackColor = System.Drawing.Color.Transparent;
            this.CheckBox_Theme_Support.ForeColor = System.Drawing.Color.Black;
            this.CheckBox_Theme_Support.Location = new System.Drawing.Point(8, 56);
            this.CheckBox_Theme_Support.Name = "CheckBox_Theme_Support";
            this.CheckBox_Theme_Support.Size = new System.Drawing.Size(222, 18);
            this.CheckBox_Theme_Support.TabIndex = 157;
            this.CheckBox_Theme_Support.Text = "Enable Custom Theme Support";
            this.CheckBox_Theme_Support.UseVisualStyleBackColor = false;
            // 
            // CheckBox_Opt_Insider
            // 
            this.CheckBox_Opt_Insider.BackColor = System.Drawing.Color.Transparent;
            this.CheckBox_Opt_Insider.ForeColor = System.Drawing.Color.Black;
            this.CheckBox_Opt_Insider.Location = new System.Drawing.Point(8, 301);
            this.CheckBox_Opt_Insider.Name = "CheckBox_Opt_Insider";
            this.CheckBox_Opt_Insider.Size = new System.Drawing.Size(222, 18);
            this.CheckBox_Opt_Insider.TabIndex = 156;
            this.CheckBox_Opt_Insider.Text = "{PLACE HOLDER}";
            this.CheckBox_Opt_Insider.UseVisualStyleBackColor = false;
            // 
            // TabPage_Game
            // 
            this.TabPage_Game.Controls.Add(this.TabControl_Game);
            this.TabPage_Game.Location = new System.Drawing.Point(0, 26);
            this.TabPage_Game.Name = "TabPage_Game";
            this.TabPage_Game.Size = new System.Drawing.Size(834, 354);
            this.TabPage_Game.TabIndex = 3;
            this.TabPage_Game.Text = "Game";
            this.TabPage_Game.UseVisualStyleBackColor = true;
            // 
            // TabControl_Game
            // 
            this.TabControl_Game.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.TabControl_Game.Controls.Add(this.TabPage_Game_General);
            this.TabControl_Game.Controls.Add(this.TabPage_Game_Verify_Hash);
            this.TabControl_Game.Controls.Add(this.TabPage_Game_Security_Center);
            this.TabControl_Game.HotTrack = true;
            this.TabControl_Game.Location = new System.Drawing.Point(1, 3);
            this.TabControl_Game.Name = "TabControl_Game";
            this.TabControl_Game.SelectedIndex = 0;
            this.TabControl_Game.Size = new System.Drawing.Size(832, 354);
            this.TabControl_Game.TabIndex = 186;
            // 
            // TabPage_Game_General
            // 
            this.TabPage_Game_General.Controls.Add(this.Button_Clear_NFSWO_Logs);
            this.TabPage_Game_General.Controls.Add(this.Button_Clear_Server_Mods);
            this.TabPage_Game_General.Controls.Add(this.Button_Clear_Crash_Logs);
            this.TabPage_Game_General.Controls.Add(this.Label_Display_Timer);
            this.TabPage_Game_General.Controls.Add(this.Panel_Display_Timer);
            this.TabPage_Game_General.Controls.Add(this.CheckBox_Word_Filter_Check);
            this.TabPage_Game_General.Controls.Add(this.LinkLabel_Game_Path);
            this.TabPage_Game_General.Controls.Add(this.Label_Game_Current_Path);
            this.TabPage_Game_General.Controls.Add(this.Button_Game_User_Settings);
            this.TabPage_Game_General.Controls.Add(this.Label_Game_Settings);
            this.TabPage_Game_General.Controls.Add(this.ComboBox_Language_List);
            this.TabPage_Game_General.Controls.Add(this.Button_Game_Verify_Files);
            this.TabPage_Game_General.Controls.Add(this.Button_Change_Game_Path);
            this.TabPage_Game_General.Controls.Add(this.Label_Game_Files);
            this.TabPage_Game_General.Location = new System.Drawing.Point(0, 26);
            this.TabPage_Game_General.Name = "TabPage_Game_General";
            this.TabPage_Game_General.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Game_General.Size = new System.Drawing.Size(832, 328);
            this.TabPage_Game_General.TabIndex = 0;
            this.TabPage_Game_General.Text = "General";
            this.TabPage_Game_General.UseVisualStyleBackColor = true;
            // 
            // Button_Clear_NFSWO_Logs
            // 
            this.Button_Clear_NFSWO_Logs.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Button_Clear_NFSWO_Logs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Clear_NFSWO_Logs.Location = new System.Drawing.Point(10, 249);
            this.Button_Clear_NFSWO_Logs.Name = "Button_Clear_NFSWO_Logs";
            this.Button_Clear_NFSWO_Logs.Size = new System.Drawing.Size(154, 25);
            this.Button_Clear_NFSWO_Logs.TabIndex = 186;
            this.Button_Clear_NFSWO_Logs.Text = "Clear NFSWO Log";
            this.Button_Clear_NFSWO_Logs.UseVisualStyleBackColor = false;
            // 
            // Button_Clear_Server_Mods
            // 
            this.Button_Clear_Server_Mods.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Button_Clear_Server_Mods.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Clear_Server_Mods.Location = new System.Drawing.Point(10, 284);
            this.Button_Clear_Server_Mods.Name = "Button_Clear_Server_Mods";
            this.Button_Clear_Server_Mods.Size = new System.Drawing.Size(154, 25);
            this.Button_Clear_Server_Mods.TabIndex = 185;
            this.Button_Clear_Server_Mods.Text = "Clear Server Mods";
            this.Button_Clear_Server_Mods.UseVisualStyleBackColor = false;
            // 
            // Button_Clear_Crash_Logs
            // 
            this.Button_Clear_Crash_Logs.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Button_Clear_Crash_Logs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Clear_Crash_Logs.Location = new System.Drawing.Point(10, 214);
            this.Button_Clear_Crash_Logs.Name = "Button_Clear_Crash_Logs";
            this.Button_Clear_Crash_Logs.Size = new System.Drawing.Size(154, 25);
            this.Button_Clear_Crash_Logs.TabIndex = 184;
            this.Button_Clear_Crash_Logs.Text = "Clear Crash Logs";
            this.Button_Clear_Crash_Logs.UseVisualStyleBackColor = false;
            // 
            // Label_Display_Timer
            // 
            this.Label_Display_Timer.BackColor = System.Drawing.Color.Transparent;
            this.Label_Display_Timer.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold);
            this.Label_Display_Timer.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_Display_Timer.Location = new System.Drawing.Point(10, 128);
            this.Label_Display_Timer.Name = "Label_Display_Timer";
            this.Label_Display_Timer.Size = new System.Drawing.Size(288, 18);
            this.Label_Display_Timer.TabIndex = 183;
            this.Label_Display_Timer.Text = "Title Window Display Timer:";
            this.Label_Display_Timer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Panel_Display_Timer
            // 
            this.Panel_Display_Timer.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Display_Timer.Controls.Add(this.Radio_Button_Static_Timer);
            this.Panel_Display_Timer.Controls.Add(this.Radio_Button_Dynamic_Timer);
            this.Panel_Display_Timer.Controls.Add(this.Radio_Button_No_Timer);
            this.Panel_Display_Timer.Location = new System.Drawing.Point(10, 148);
            this.Panel_Display_Timer.Name = "Panel_Display_Timer";
            this.Panel_Display_Timer.Size = new System.Drawing.Size(288, 22);
            this.Panel_Display_Timer.TabIndex = 182;
            this.Panel_Display_Timer.Tag = "DisplayTimer";
            // 
            // Radio_Button_Static_Timer
            // 
            this.Radio_Button_Static_Timer.BackColor = System.Drawing.Color.Transparent;
            this.Radio_Button_Static_Timer.Font = new System.Drawing.Font("DejaVu Sans", 9F);
            this.Radio_Button_Static_Timer.ForeColor = System.Drawing.Color.Blue;
            this.Radio_Button_Static_Timer.Location = new System.Drawing.Point(5, 2);
            this.Radio_Button_Static_Timer.Name = "Radio_Button_Static_Timer";
            this.Radio_Button_Static_Timer.Size = new System.Drawing.Size(90, 18);
            this.Radio_Button_Static_Timer.TabIndex = 95;
            this.Radio_Button_Static_Timer.TabStop = true;
            this.Radio_Button_Static_Timer.Tag = "StaticTimer";
            this.Radio_Button_Static_Timer.Text = "Static";
            this.Radio_Button_Static_Timer.UseVisualStyleBackColor = false;
            // 
            // Radio_Button_Dynamic_Timer
            // 
            this.Radio_Button_Dynamic_Timer.BackColor = System.Drawing.Color.Transparent;
            this.Radio_Button_Dynamic_Timer.Font = new System.Drawing.Font("DejaVu Sans", 9F);
            this.Radio_Button_Dynamic_Timer.ForeColor = System.Drawing.Color.Blue;
            this.Radio_Button_Dynamic_Timer.Location = new System.Drawing.Point(99, 2);
            this.Radio_Button_Dynamic_Timer.Name = "Radio_Button_Dynamic_Timer";
            this.Radio_Button_Dynamic_Timer.Size = new System.Drawing.Size(90, 18);
            this.Radio_Button_Dynamic_Timer.TabIndex = 96;
            this.Radio_Button_Dynamic_Timer.TabStop = true;
            this.Radio_Button_Dynamic_Timer.Tag = "DynamicTimer";
            this.Radio_Button_Dynamic_Timer.Text = "Dynamic";
            this.Radio_Button_Dynamic_Timer.UseVisualStyleBackColor = false;
            // 
            // Radio_Button_No_Timer
            // 
            this.Radio_Button_No_Timer.BackColor = System.Drawing.Color.Transparent;
            this.Radio_Button_No_Timer.Font = new System.Drawing.Font("DejaVu Sans", 9F);
            this.Radio_Button_No_Timer.ForeColor = System.Drawing.Color.Blue;
            this.Radio_Button_No_Timer.Location = new System.Drawing.Point(193, 2);
            this.Radio_Button_No_Timer.Name = "Radio_Button_No_Timer";
            this.Radio_Button_No_Timer.Size = new System.Drawing.Size(90, 18);
            this.Radio_Button_No_Timer.TabIndex = 97;
            this.Radio_Button_No_Timer.TabStop = true;
            this.Radio_Button_No_Timer.Tag = "NoTimer";
            this.Radio_Button_No_Timer.Text = "None";
            this.Radio_Button_No_Timer.UseVisualStyleBackColor = false;
            // 
            // CheckBox_Word_Filter_Check
            // 
            this.CheckBox_Word_Filter_Check.BackColor = System.Drawing.Color.Transparent;
            this.CheckBox_Word_Filter_Check.ForeColor = System.Drawing.Color.Black;
            this.CheckBox_Word_Filter_Check.Location = new System.Drawing.Point(10, 183);
            this.CheckBox_Word_Filter_Check.Name = "CheckBox_Word_Filter_Check";
            this.CheckBox_Word_Filter_Check.Size = new System.Drawing.Size(222, 18);
            this.CheckBox_Word_Filter_Check.TabIndex = 181;
            this.CheckBox_Word_Filter_Check.Text = "Disable Chat Word Filtering";
            this.CheckBox_Word_Filter_Check.UseVisualStyleBackColor = false;
            // 
            // LinkLabel_Game_Path
            // 
            this.LinkLabel_Game_Path.BackColor = System.Drawing.Color.Transparent;
            this.LinkLabel_Game_Path.Location = new System.Drawing.Point(351, 28);
            this.LinkLabel_Game_Path.Name = "LinkLabel_Game_Path";
            this.LinkLabel_Game_Path.Size = new System.Drawing.Size(360, 32);
            this.LinkLabel_Game_Path.TabIndex = 180;
            this.LinkLabel_Game_Path.TabStop = true;
            this.LinkLabel_Game_Path.Text = "C:\\Soapbox Race World\\Game Files";
            // 
            // Label_Game_Current_Path
            // 
            this.Label_Game_Current_Path.BackColor = System.Drawing.Color.Transparent;
            this.Label_Game_Current_Path.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_Game_Current_Path.Location = new System.Drawing.Point(351, 13);
            this.Label_Game_Current_Path.Name = "Label_Game_Current_Path";
            this.Label_Game_Current_Path.Size = new System.Drawing.Size(359, 14);
            this.Label_Game_Current_Path.TabIndex = 179;
            this.Label_Game_Current_Path.Text = "CURRENT DIRECTORY:";
            this.Label_Game_Current_Path.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Button_Game_User_Settings
            // 
            this.Button_Game_User_Settings.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Button_Game_User_Settings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Game_User_Settings.Location = new System.Drawing.Point(187, 89);
            this.Button_Game_User_Settings.Name = "Button_Game_User_Settings";
            this.Button_Game_User_Settings.Size = new System.Drawing.Size(143, 25);
            this.Button_Game_User_Settings.TabIndex = 176;
            this.Button_Game_User_Settings.Text = "Edit UserSettings";
            this.Button_Game_User_Settings.UseVisualStyleBackColor = false;
            // 
            // Label_Game_Settings
            // 
            this.Label_Game_Settings.BackColor = System.Drawing.Color.Transparent;
            this.Label_Game_Settings.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_Game_Settings.Location = new System.Drawing.Point(10, 70);
            this.Label_Game_Settings.Name = "Label_Game_Settings";
            this.Label_Game_Settings.Size = new System.Drawing.Size(320, 14);
            this.Label_Game_Settings.TabIndex = 175;
            this.Label_Game_Settings.Text = "GAME SETTINGS:";
            this.Label_Game_Settings.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // ComboBox_Language_List
            // 
            this.ComboBox_Language_List.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ComboBox_Language_List.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Language_List.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComboBox_Language_List.FormattingEnabled = true;
            this.ComboBox_Language_List.Location = new System.Drawing.Point(12, 89);
            this.ComboBox_Language_List.Name = "ComboBox_Language_List";
            this.ComboBox_Language_List.Size = new System.Drawing.Size(164, 22);
            this.ComboBox_Language_List.TabIndex = 174;
            // 
            // Button_Game_Verify_Files
            // 
            this.Button_Game_Verify_Files.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Button_Game_Verify_Files.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Game_Verify_Files.Location = new System.Drawing.Point(208, 32);
            this.Button_Game_Verify_Files.Name = "Button_Game_Verify_Files";
            this.Button_Game_Verify_Files.Size = new System.Drawing.Size(122, 25);
            this.Button_Game_Verify_Files.TabIndex = 173;
            this.Button_Game_Verify_Files.Text = "Verify GameFiles";
            this.Button_Game_Verify_Files.UseVisualStyleBackColor = false;
            // 
            // Button_Change_Game_Path
            // 
            this.Button_Change_Game_Path.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Button_Change_Game_Path.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Change_Game_Path.Location = new System.Drawing.Point(12, 32);
            this.Button_Change_Game_Path.Name = "Button_Change_Game_Path";
            this.Button_Change_Game_Path.Size = new System.Drawing.Size(190, 25);
            this.Button_Change_Game_Path.TabIndex = 172;
            this.Button_Change_Game_Path.Text = "Change GameFiles Path";
            this.Button_Change_Game_Path.UseVisualStyleBackColor = false;
            // 
            // Label_Game_Files
            // 
            this.Label_Game_Files.BackColor = System.Drawing.Color.Transparent;
            this.Label_Game_Files.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_Game_Files.Location = new System.Drawing.Point(10, 13);
            this.Label_Game_Files.Name = "Label_Game_Files";
            this.Label_Game_Files.Size = new System.Drawing.Size(320, 14);
            this.Label_Game_Files.TabIndex = 171;
            this.Label_Game_Files.Text = "GAMEFILES:";
            this.Label_Game_Files.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TabPage_Game_Verify_Hash
            // 
            this.TabPage_Game_Verify_Hash.Controls.Add(this.VerifyHashWelcome);
            this.TabPage_Game_Verify_Hash.Controls.Add(this.DownloadProgressText_Alt);
            this.TabPage_Game_Verify_Hash.Controls.Add(this.DownloadProgressText);
            this.TabPage_Game_Verify_Hash.Controls.Add(this.VerifyHashText);
            this.TabPage_Game_Verify_Hash.Controls.Add(this.ScanProgressText);
            this.TabPage_Game_Verify_Hash.Controls.Add(this.ScanProgressBar);
            this.TabPage_Game_Verify_Hash.Controls.Add(this.DownloadProgressBar);
            this.TabPage_Game_Verify_Hash.Controls.Add(this.StartScanner);
            this.TabPage_Game_Verify_Hash.Controls.Add(this.StopScanner);
            this.TabPage_Game_Verify_Hash.Location = new System.Drawing.Point(0, 26);
            this.TabPage_Game_Verify_Hash.Name = "TabPage_Game_Verify_Hash";
            this.TabPage_Game_Verify_Hash.Size = new System.Drawing.Size(832, 328);
            this.TabPage_Game_Verify_Hash.TabIndex = 3;
            this.TabPage_Game_Verify_Hash.Text = "Verify Hash";
            this.TabPage_Game_Verify_Hash.UseVisualStyleBackColor = true;
            // 
            // VerifyHashWelcome
            // 
            this.VerifyHashWelcome.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.VerifyHashWelcome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VerifyHashWelcome.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VerifyHashWelcome.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.VerifyHashWelcome.Location = new System.Drawing.Point(22, 75);
            this.VerifyHashWelcome.Name = "VerifyHashWelcome";
            this.VerifyHashWelcome.Size = new System.Drawing.Size(384, 118);
            this.VerifyHashWelcome.TabIndex = 20;
            this.VerifyHashWelcome.Text = "Welcome!\r\n\r\nThe scanning process is pretty quick,\r\nbut may still take a while.\r\nD" +
    "epending on your connection,\r\nre-downloading will take the longest.\r\nPlease allo" +
    "w it to complete fully!";
            this.VerifyHashWelcome.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DownloadProgressText_Alt
            // 
            this.DownloadProgressText_Alt.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownloadProgressText_Alt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.DownloadProgressText_Alt.Location = new System.Drawing.Point(421, 75);
            this.DownloadProgressText_Alt.Margin = new System.Windows.Forms.Padding(0);
            this.DownloadProgressText_Alt.Name = "DownloadProgressText_Alt";
            this.DownloadProgressText_Alt.Size = new System.Drawing.Size(384, 118);
            this.DownloadProgressText_Alt.TabIndex = 19;
            this.DownloadProgressText_Alt.Text = "\r\nDownload Progress:";
            this.DownloadProgressText_Alt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DownloadProgressText
            // 
            this.DownloadProgressText.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownloadProgressText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.DownloadProgressText.Location = new System.Drawing.Point(421, 12);
            this.DownloadProgressText.Margin = new System.Windows.Forms.Padding(0);
            this.DownloadProgressText.Name = "DownloadProgressText";
            this.DownloadProgressText.Size = new System.Drawing.Size(384, 18);
            this.DownloadProgressText.TabIndex = 18;
            this.DownloadProgressText.Text = "Download Progress:";
            this.DownloadProgressText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // VerifyHashText
            // 
            this.VerifyHashText.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.VerifyHashText.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VerifyHashText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.VerifyHashText.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.VerifyHashText.Location = new System.Drawing.Point(222, 210);
            this.VerifyHashText.Name = "VerifyHashText";
            this.VerifyHashText.Size = new System.Drawing.Size(384, 66);
            this.VerifyHashText.TabIndex = 17;
            this.VerifyHashText.Text = "Please select \"Start Scan\"\r\nTo begin Validating Gamefiles";
            this.VerifyHashText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ScanProgressText
            // 
            this.ScanProgressText.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScanProgressText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.ScanProgressText.Location = new System.Drawing.Point(21, 12);
            this.ScanProgressText.Margin = new System.Windows.Forms.Padding(0);
            this.ScanProgressText.Name = "ScanProgressText";
            this.ScanProgressText.Size = new System.Drawing.Size(384, 18);
            this.ScanProgressText.TabIndex = 11;
            this.ScanProgressText.Text = "Scanning Progress:";
            this.ScanProgressText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ScanProgressBar
            // 
            this.ScanProgressBar.Location = new System.Drawing.Point(21, 40);
            this.ScanProgressBar.Name = "ScanProgressBar";
            this.ScanProgressBar.Size = new System.Drawing.Size(384, 23);
            this.ScanProgressBar.TabIndex = 10;
            // 
            // DownloadProgressBar
            // 
            this.DownloadProgressBar.Location = new System.Drawing.Point(421, 40);
            this.DownloadProgressBar.Name = "DownloadProgressBar";
            this.DownloadProgressBar.Size = new System.Drawing.Size(384, 23);
            this.DownloadProgressBar.TabIndex = 14;
            // 
            // StartScanner
            // 
            this.StartScanner.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.StartScanner.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.StartScanner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartScanner.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartScanner.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.StartScanner.Location = new System.Drawing.Point(368, 289);
            this.StartScanner.Name = "StartScanner";
            this.StartScanner.Size = new System.Drawing.Size(91, 24);
            this.StartScanner.TabIndex = 12;
            this.StartScanner.Text = "Start Scan";
            this.StartScanner.UseVisualStyleBackColor = true;
            // 
            // StopScanner
            // 
            this.StopScanner.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.StopScanner.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.StopScanner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopScanner.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopScanner.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(200)))), ((int)(((byte)(0)))));
            this.StopScanner.Location = new System.Drawing.Point(368, 289);
            this.StopScanner.Name = "StopScanner";
            this.StopScanner.Size = new System.Drawing.Size(91, 24);
            this.StopScanner.TabIndex = 13;
            this.StopScanner.Text = "Stop Scan";
            this.StopScanner.UseVisualStyleBackColor = true;
            this.StopScanner.Visible = false;
            // 
            // TabPage_Game_Security_Center
            // 
            this.TabPage_Game_Security_Center.Controls.Add(this.TabControl_Security_Center);
            this.TabPage_Game_Security_Center.Location = new System.Drawing.Point(0, 26);
            this.TabPage_Game_Security_Center.Name = "TabPage_Game_Security_Center";
            this.TabPage_Game_Security_Center.Size = new System.Drawing.Size(832, 328);
            this.TabPage_Game_Security_Center.TabIndex = 4;
            this.TabPage_Game_Security_Center.Text = "Security Center";
            this.TabPage_Game_Security_Center.UseVisualStyleBackColor = true;
            // 
            // TabControl_Security_Center
            // 
            this.TabControl_Security_Center.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.TabControl_Security_Center.Controls.Add(this.TabPage_Security_Center_Firewall);
            this.TabControl_Security_Center.Controls.Add(this.TabPage_Security_Center_Defender);
            this.TabControl_Security_Center.Controls.Add(this.TabPage_Security_Center_Permissons);
            this.TabControl_Security_Center.HotTrack = true;
            this.TabControl_Security_Center.Location = new System.Drawing.Point(0, 3);
            this.TabControl_Security_Center.Name = "TabControl_Security_Center";
            this.TabControl_Security_Center.SelectedIndex = 0;
            this.TabControl_Security_Center.Size = new System.Drawing.Size(830, 319);
            this.TabControl_Security_Center.TabIndex = 187;
            // 
            // TabPage_Security_Center_Firewall
            // 
            this.TabPage_Security_Center_Firewall.Controls.Add(this.GroupBox_Firewall);
            this.TabPage_Security_Center_Firewall.Controls.Add(this.ButtonFirewallRulesAPI);
            this.TabPage_Security_Center_Firewall.Controls.Add(this.ButtonFirewallRulesCheck);
            this.TabPage_Security_Center_Firewall.Controls.Add(this.ButtonFirewallRulesAddAll);
            this.TabPage_Security_Center_Firewall.Controls.Add(this.ButtonFirewallRulesAddGame);
            this.TabPage_Security_Center_Firewall.Controls.Add(this.ButtonFirewallRulesAddLauncher);
            this.TabPage_Security_Center_Firewall.Controls.Add(this.ButtonFirewallRulesRemoveGame);
            this.TabPage_Security_Center_Firewall.Controls.Add(this.ButtonFirewallRulesRemoveLauncher);
            this.TabPage_Security_Center_Firewall.Controls.Add(this.ButtonFirewallRulesRemoveAll);
            this.TabPage_Security_Center_Firewall.Controls.Add(this.TextWindowsFirewall);
            this.TabPage_Security_Center_Firewall.Location = new System.Drawing.Point(0, 26);
            this.TabPage_Security_Center_Firewall.Name = "TabPage_Security_Center_Firewall";
            this.TabPage_Security_Center_Firewall.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Security_Center_Firewall.Size = new System.Drawing.Size(830, 293);
            this.TabPage_Security_Center_Firewall.TabIndex = 0;
            this.TabPage_Security_Center_Firewall.Text = "Firewall";
            this.TabPage_Security_Center_Firewall.UseVisualStyleBackColor = true;
            // 
            // GroupBox_Firewall
            // 
            this.GroupBox_Firewall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.GroupBox_Firewall.Controls.Add(this.TextBox_Console_Firewall);
            this.GroupBox_Firewall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GroupBox_Firewall.Location = new System.Drawing.Point(12, 150);
            this.GroupBox_Firewall.Name = "GroupBox_Firewall";
            this.GroupBox_Firewall.Size = new System.Drawing.Size(805, 134);
            this.GroupBox_Firewall.TabIndex = 222;
            this.GroupBox_Firewall.TabStop = false;
            this.GroupBox_Firewall.Text = "Console:";
            this.GroupBox_Firewall.Visible = false;
            // 
            // TextBox_Console_Firewall
            // 
            this.TextBox_Console_Firewall.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBox_Console_Firewall.Location = new System.Drawing.Point(6, 17);
            this.TextBox_Console_Firewall.Multiline = true;
            this.TextBox_Console_Firewall.Name = "TextBox_Console_Firewall";
            this.TextBox_Console_Firewall.ReadOnly = true;
            this.TextBox_Console_Firewall.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox_Console_Firewall.Size = new System.Drawing.Size(793, 111);
            this.TextBox_Console_Firewall.TabIndex = 35;
            // 
            // ButtonFirewallRulesAPI
            // 
            this.ButtonFirewallRulesAPI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.ButtonFirewallRulesAPI.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.ButtonFirewallRulesAPI.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.ButtonFirewallRulesAPI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFirewallRulesAPI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonFirewallRulesAPI.ForeColor = System.Drawing.Color.Silver;
            this.ButtonFirewallRulesAPI.Location = new System.Drawing.Point(464, 36);
            this.ButtonFirewallRulesAPI.Name = "ButtonFirewallRulesAPI";
            this.ButtonFirewallRulesAPI.Size = new System.Drawing.Size(221, 25);
            this.ButtonFirewallRulesAPI.TabIndex = 217;
            this.ButtonFirewallRulesAPI.Text = "Check Firewall API";
            this.ButtonFirewallRulesAPI.UseVisualStyleBackColor = false;
            // 
            // ButtonFirewallRulesCheck
            // 
            this.ButtonFirewallRulesCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.ButtonFirewallRulesCheck.Enabled = false;
            this.ButtonFirewallRulesCheck.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.ButtonFirewallRulesCheck.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.ButtonFirewallRulesCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFirewallRulesCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonFirewallRulesCheck.ForeColor = System.Drawing.Color.Silver;
            this.ButtonFirewallRulesCheck.Location = new System.Drawing.Point(464, 67);
            this.ButtonFirewallRulesCheck.Name = "ButtonFirewallRulesCheck";
            this.ButtonFirewallRulesCheck.Size = new System.Drawing.Size(221, 25);
            this.ButtonFirewallRulesCheck.TabIndex = 216;
            this.ButtonFirewallRulesCheck.Text = "Check All Rules";
            this.ButtonFirewallRulesCheck.UseVisualStyleBackColor = false;
            // 
            // ButtonFirewallRulesAddAll
            // 
            this.ButtonFirewallRulesAddAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.ButtonFirewallRulesAddAll.Enabled = false;
            this.ButtonFirewallRulesAddAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.ButtonFirewallRulesAddAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.ButtonFirewallRulesAddAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFirewallRulesAddAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonFirewallRulesAddAll.ForeColor = System.Drawing.Color.Silver;
            this.ButtonFirewallRulesAddAll.Location = new System.Drawing.Point(10, 36);
            this.ButtonFirewallRulesAddAll.Name = "ButtonFirewallRulesAddAll";
            this.ButtonFirewallRulesAddAll.Size = new System.Drawing.Size(221, 25);
            this.ButtonFirewallRulesAddAll.TabIndex = 215;
            this.ButtonFirewallRulesAddAll.Text = "Add All SBRW Rules";
            this.ButtonFirewallRulesAddAll.UseVisualStyleBackColor = false;
            // 
            // ButtonFirewallRulesAddGame
            // 
            this.ButtonFirewallRulesAddGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.ButtonFirewallRulesAddGame.Enabled = false;
            this.ButtonFirewallRulesAddGame.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.ButtonFirewallRulesAddGame.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.ButtonFirewallRulesAddGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFirewallRulesAddGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonFirewallRulesAddGame.ForeColor = System.Drawing.Color.Silver;
            this.ButtonFirewallRulesAddGame.Location = new System.Drawing.Point(10, 98);
            this.ButtonFirewallRulesAddGame.Name = "ButtonFirewallRulesAddGame";
            this.ButtonFirewallRulesAddGame.Size = new System.Drawing.Size(221, 25);
            this.ButtonFirewallRulesAddGame.TabIndex = 214;
            this.ButtonFirewallRulesAddGame.Text = "Add Game Rules";
            this.ButtonFirewallRulesAddGame.UseVisualStyleBackColor = false;
            // 
            // ButtonFirewallRulesAddLauncher
            // 
            this.ButtonFirewallRulesAddLauncher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.ButtonFirewallRulesAddLauncher.Enabled = false;
            this.ButtonFirewallRulesAddLauncher.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.ButtonFirewallRulesAddLauncher.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.ButtonFirewallRulesAddLauncher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFirewallRulesAddLauncher.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonFirewallRulesAddLauncher.ForeColor = System.Drawing.Color.Silver;
            this.ButtonFirewallRulesAddLauncher.Location = new System.Drawing.Point(10, 67);
            this.ButtonFirewallRulesAddLauncher.Name = "ButtonFirewallRulesAddLauncher";
            this.ButtonFirewallRulesAddLauncher.Size = new System.Drawing.Size(221, 25);
            this.ButtonFirewallRulesAddLauncher.TabIndex = 213;
            this.ButtonFirewallRulesAddLauncher.Text = "Add Launcher Rules";
            this.ButtonFirewallRulesAddLauncher.UseVisualStyleBackColor = false;
            // 
            // ButtonFirewallRulesRemoveGame
            // 
            this.ButtonFirewallRulesRemoveGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.ButtonFirewallRulesRemoveGame.Enabled = false;
            this.ButtonFirewallRulesRemoveGame.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.ButtonFirewallRulesRemoveGame.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.ButtonFirewallRulesRemoveGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFirewallRulesRemoveGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonFirewallRulesRemoveGame.ForeColor = System.Drawing.Color.Silver;
            this.ButtonFirewallRulesRemoveGame.Location = new System.Drawing.Point(237, 98);
            this.ButtonFirewallRulesRemoveGame.Name = "ButtonFirewallRulesRemoveGame";
            this.ButtonFirewallRulesRemoveGame.Size = new System.Drawing.Size(221, 25);
            this.ButtonFirewallRulesRemoveGame.TabIndex = 212;
            this.ButtonFirewallRulesRemoveGame.Text = "Remove Game Rules";
            this.ButtonFirewallRulesRemoveGame.UseVisualStyleBackColor = false;
            // 
            // ButtonFirewallRulesRemoveLauncher
            // 
            this.ButtonFirewallRulesRemoveLauncher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.ButtonFirewallRulesRemoveLauncher.Enabled = false;
            this.ButtonFirewallRulesRemoveLauncher.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.ButtonFirewallRulesRemoveLauncher.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.ButtonFirewallRulesRemoveLauncher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFirewallRulesRemoveLauncher.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonFirewallRulesRemoveLauncher.ForeColor = System.Drawing.Color.Silver;
            this.ButtonFirewallRulesRemoveLauncher.Location = new System.Drawing.Point(237, 67);
            this.ButtonFirewallRulesRemoveLauncher.Name = "ButtonFirewallRulesRemoveLauncher";
            this.ButtonFirewallRulesRemoveLauncher.Size = new System.Drawing.Size(221, 25);
            this.ButtonFirewallRulesRemoveLauncher.TabIndex = 211;
            this.ButtonFirewallRulesRemoveLauncher.Text = "Remove Launcher Rules";
            this.ButtonFirewallRulesRemoveLauncher.UseVisualStyleBackColor = false;
            // 
            // ButtonFirewallRulesRemoveAll
            // 
            this.ButtonFirewallRulesRemoveAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.ButtonFirewallRulesRemoveAll.Enabled = false;
            this.ButtonFirewallRulesRemoveAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.ButtonFirewallRulesRemoveAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.ButtonFirewallRulesRemoveAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFirewallRulesRemoveAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonFirewallRulesRemoveAll.ForeColor = System.Drawing.Color.Silver;
            this.ButtonFirewallRulesRemoveAll.Location = new System.Drawing.Point(237, 36);
            this.ButtonFirewallRulesRemoveAll.Name = "ButtonFirewallRulesRemoveAll";
            this.ButtonFirewallRulesRemoveAll.Size = new System.Drawing.Size(221, 25);
            this.ButtonFirewallRulesRemoveAll.TabIndex = 210;
            this.ButtonFirewallRulesRemoveAll.Text = "Remove All SBRW Rules";
            this.ButtonFirewallRulesRemoveAll.UseVisualStyleBackColor = false;
            // 
            // TextWindowsFirewall
            // 
            this.TextWindowsFirewall.BackColor = System.Drawing.Color.Transparent;
            this.TextWindowsFirewall.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextWindowsFirewall.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TextWindowsFirewall.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TextWindowsFirewall.Location = new System.Drawing.Point(9, 12);
            this.TextWindowsFirewall.Name = "TextWindowsFirewall";
            this.TextWindowsFirewall.Size = new System.Drawing.Size(177, 14);
            this.TextWindowsFirewall.TabIndex = 209;
            this.TextWindowsFirewall.Text = "WINDOWS FIREWALL:";
            this.TextWindowsFirewall.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // TabPage_Security_Center_Defender
            // 
            this.TabPage_Security_Center_Defender.Controls.Add(this.GroupBox_Defender);
            this.TabPage_Security_Center_Defender.Controls.Add(this.ButtonDefenderExclusionAPI);
            this.TabPage_Security_Center_Defender.Controls.Add(this.ButtonDefenderExclusionAddGame);
            this.TabPage_Security_Center_Defender.Controls.Add(this.ButtonDefenderExclusionAddLauncher);
            this.TabPage_Security_Center_Defender.Controls.Add(this.ButtonDefenderExclusionAddAll);
            this.TabPage_Security_Center_Defender.Controls.Add(this.ButtonDefenderExclusionCheck);
            this.TabPage_Security_Center_Defender.Controls.Add(this.ButtonDefenderExclusionRemoveGame);
            this.TabPage_Security_Center_Defender.Controls.Add(this.ButtonDefenderExclusionRemoveLauncher);
            this.TabPage_Security_Center_Defender.Controls.Add(this.ButtonDefenderExclusionRemoveAll);
            this.TabPage_Security_Center_Defender.Controls.Add(this.TextWindowsDefender);
            this.TabPage_Security_Center_Defender.Location = new System.Drawing.Point(0, 27);
            this.TabPage_Security_Center_Defender.Name = "TabPage_Security_Center_Defender";
            this.TabPage_Security_Center_Defender.Size = new System.Drawing.Size(827, 292);
            this.TabPage_Security_Center_Defender.TabIndex = 3;
            this.TabPage_Security_Center_Defender.Text = "Defender";
            this.TabPage_Security_Center_Defender.UseVisualStyleBackColor = true;
            // 
            // GroupBox_Defender
            // 
            this.GroupBox_Defender.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.GroupBox_Defender.Controls.Add(this.textBox1);
            this.GroupBox_Defender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GroupBox_Defender.Location = new System.Drawing.Point(12, 150);
            this.GroupBox_Defender.Name = "GroupBox_Defender";
            this.GroupBox_Defender.Size = new System.Drawing.Size(805, 134);
            this.GroupBox_Defender.TabIndex = 221;
            this.GroupBox_Defender.TabStop = false;
            this.GroupBox_Defender.Text = "Console:";
            this.GroupBox_Defender.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(6, 17);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(793, 111);
            this.textBox1.TabIndex = 35;
            // 
            // ButtonDefenderExclusionAPI
            // 
            this.ButtonDefenderExclusionAPI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.ButtonDefenderExclusionAPI.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.ButtonDefenderExclusionAPI.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.ButtonDefenderExclusionAPI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDefenderExclusionAPI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonDefenderExclusionAPI.ForeColor = System.Drawing.Color.Silver;
            this.ButtonDefenderExclusionAPI.Location = new System.Drawing.Point(464, 33);
            this.ButtonDefenderExclusionAPI.Name = "ButtonDefenderExclusionAPI";
            this.ButtonDefenderExclusionAPI.Size = new System.Drawing.Size(221, 25);
            this.ButtonDefenderExclusionAPI.TabIndex = 218;
            this.ButtonDefenderExclusionAPI.Text = "Check Defender API";
            this.ButtonDefenderExclusionAPI.UseVisualStyleBackColor = false;
            // 
            // ButtonDefenderExclusionAddGame
            // 
            this.ButtonDefenderExclusionAddGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.ButtonDefenderExclusionAddGame.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.ButtonDefenderExclusionAddGame.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.ButtonDefenderExclusionAddGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDefenderExclusionAddGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonDefenderExclusionAddGame.ForeColor = System.Drawing.Color.Silver;
            this.ButtonDefenderExclusionAddGame.Location = new System.Drawing.Point(10, 95);
            this.ButtonDefenderExclusionAddGame.Name = "ButtonDefenderExclusionAddGame";
            this.ButtonDefenderExclusionAddGame.Size = new System.Drawing.Size(221, 25);
            this.ButtonDefenderExclusionAddGame.TabIndex = 217;
            this.ButtonDefenderExclusionAddGame.Text = "Add Game Exclusion";
            this.ButtonDefenderExclusionAddGame.UseVisualStyleBackColor = false;
            // 
            // ButtonDefenderExclusionAddLauncher
            // 
            this.ButtonDefenderExclusionAddLauncher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.ButtonDefenderExclusionAddLauncher.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.ButtonDefenderExclusionAddLauncher.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.ButtonDefenderExclusionAddLauncher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDefenderExclusionAddLauncher.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonDefenderExclusionAddLauncher.ForeColor = System.Drawing.Color.Silver;
            this.ButtonDefenderExclusionAddLauncher.Location = new System.Drawing.Point(10, 64);
            this.ButtonDefenderExclusionAddLauncher.Name = "ButtonDefenderExclusionAddLauncher";
            this.ButtonDefenderExclusionAddLauncher.Size = new System.Drawing.Size(221, 25);
            this.ButtonDefenderExclusionAddLauncher.TabIndex = 216;
            this.ButtonDefenderExclusionAddLauncher.Text = "Add Launcher Exclusion";
            this.ButtonDefenderExclusionAddLauncher.UseVisualStyleBackColor = false;
            // 
            // ButtonDefenderExclusionAddAll
            // 
            this.ButtonDefenderExclusionAddAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.ButtonDefenderExclusionAddAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.ButtonDefenderExclusionAddAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.ButtonDefenderExclusionAddAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDefenderExclusionAddAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonDefenderExclusionAddAll.ForeColor = System.Drawing.Color.Silver;
            this.ButtonDefenderExclusionAddAll.Location = new System.Drawing.Point(10, 33);
            this.ButtonDefenderExclusionAddAll.Name = "ButtonDefenderExclusionAddAll";
            this.ButtonDefenderExclusionAddAll.Size = new System.Drawing.Size(221, 25);
            this.ButtonDefenderExclusionAddAll.TabIndex = 215;
            this.ButtonDefenderExclusionAddAll.Text = "Add All SBRW Exclusions";
            this.ButtonDefenderExclusionAddAll.UseVisualStyleBackColor = false;
            // 
            // ButtonDefenderExclusionCheck
            // 
            this.ButtonDefenderExclusionCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.ButtonDefenderExclusionCheck.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.ButtonDefenderExclusionCheck.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.ButtonDefenderExclusionCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDefenderExclusionCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonDefenderExclusionCheck.ForeColor = System.Drawing.Color.Silver;
            this.ButtonDefenderExclusionCheck.Location = new System.Drawing.Point(464, 64);
            this.ButtonDefenderExclusionCheck.Name = "ButtonDefenderExclusionCheck";
            this.ButtonDefenderExclusionCheck.Size = new System.Drawing.Size(221, 25);
            this.ButtonDefenderExclusionCheck.TabIndex = 214;
            this.ButtonDefenderExclusionCheck.Text = "Check All Exclusions";
            this.ButtonDefenderExclusionCheck.UseVisualStyleBackColor = false;
            // 
            // ButtonDefenderExclusionRemoveGame
            // 
            this.ButtonDefenderExclusionRemoveGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.ButtonDefenderExclusionRemoveGame.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.ButtonDefenderExclusionRemoveGame.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.ButtonDefenderExclusionRemoveGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDefenderExclusionRemoveGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonDefenderExclusionRemoveGame.ForeColor = System.Drawing.Color.Silver;
            this.ButtonDefenderExclusionRemoveGame.Location = new System.Drawing.Point(237, 95);
            this.ButtonDefenderExclusionRemoveGame.Name = "ButtonDefenderExclusionRemoveGame";
            this.ButtonDefenderExclusionRemoveGame.Size = new System.Drawing.Size(221, 25);
            this.ButtonDefenderExclusionRemoveGame.TabIndex = 213;
            this.ButtonDefenderExclusionRemoveGame.Text = "Remove Game Exclusion";
            this.ButtonDefenderExclusionRemoveGame.UseVisualStyleBackColor = false;
            // 
            // ButtonDefenderExclusionRemoveLauncher
            // 
            this.ButtonDefenderExclusionRemoveLauncher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.ButtonDefenderExclusionRemoveLauncher.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.ButtonDefenderExclusionRemoveLauncher.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.ButtonDefenderExclusionRemoveLauncher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDefenderExclusionRemoveLauncher.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonDefenderExclusionRemoveLauncher.ForeColor = System.Drawing.Color.Silver;
            this.ButtonDefenderExclusionRemoveLauncher.Location = new System.Drawing.Point(237, 64);
            this.ButtonDefenderExclusionRemoveLauncher.Name = "ButtonDefenderExclusionRemoveLauncher";
            this.ButtonDefenderExclusionRemoveLauncher.Size = new System.Drawing.Size(221, 25);
            this.ButtonDefenderExclusionRemoveLauncher.TabIndex = 212;
            this.ButtonDefenderExclusionRemoveLauncher.Text = "Remove Launcher Exclusion";
            this.ButtonDefenderExclusionRemoveLauncher.UseVisualStyleBackColor = false;
            // 
            // ButtonDefenderExclusionRemoveAll
            // 
            this.ButtonDefenderExclusionRemoveAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.ButtonDefenderExclusionRemoveAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.ButtonDefenderExclusionRemoveAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.ButtonDefenderExclusionRemoveAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDefenderExclusionRemoveAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonDefenderExclusionRemoveAll.ForeColor = System.Drawing.Color.Silver;
            this.ButtonDefenderExclusionRemoveAll.Location = new System.Drawing.Point(237, 33);
            this.ButtonDefenderExclusionRemoveAll.Name = "ButtonDefenderExclusionRemoveAll";
            this.ButtonDefenderExclusionRemoveAll.Size = new System.Drawing.Size(221, 25);
            this.ButtonDefenderExclusionRemoveAll.TabIndex = 211;
            this.ButtonDefenderExclusionRemoveAll.Text = "Remove All SBRW Exclusions";
            this.ButtonDefenderExclusionRemoveAll.UseVisualStyleBackColor = false;
            // 
            // TextWindowsDefender
            // 
            this.TextWindowsDefender.BackColor = System.Drawing.Color.Transparent;
            this.TextWindowsDefender.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextWindowsDefender.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TextWindowsDefender.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TextWindowsDefender.Location = new System.Drawing.Point(7, 11);
            this.TextWindowsDefender.Name = "TextWindowsDefender";
            this.TextWindowsDefender.Size = new System.Drawing.Size(177, 14);
            this.TextWindowsDefender.TabIndex = 210;
            this.TextWindowsDefender.Text = "WINDOWS DEFENDER:";
            this.TextWindowsDefender.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // TabPage_Security_Center_Permissons
            // 
            this.TabPage_Security_Center_Permissons.Controls.Add(this.GroupBox_Permissons);
            this.TabPage_Security_Center_Permissons.Controls.Add(this.ButtonFolderPermissonCheck);
            this.TabPage_Security_Center_Permissons.Controls.Add(this.ButtonFolderPermissonSet);
            this.TabPage_Security_Center_Permissons.Controls.Add(this.TextFolderPermissions);
            this.TabPage_Security_Center_Permissons.Location = new System.Drawing.Point(0, 27);
            this.TabPage_Security_Center_Permissons.Name = "TabPage_Security_Center_Permissons";
            this.TabPage_Security_Center_Permissons.Size = new System.Drawing.Size(827, 292);
            this.TabPage_Security_Center_Permissons.TabIndex = 4;
            this.TabPage_Security_Center_Permissons.Text = "Permissons";
            this.TabPage_Security_Center_Permissons.UseVisualStyleBackColor = true;
            // 
            // GroupBox_Permissons
            // 
            this.GroupBox_Permissons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.GroupBox_Permissons.Controls.Add(this.TextBox_Live_Log);
            this.GroupBox_Permissons.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GroupBox_Permissons.Location = new System.Drawing.Point(12, 150);
            this.GroupBox_Permissons.Name = "GroupBox_Permissons";
            this.GroupBox_Permissons.Size = new System.Drawing.Size(805, 134);
            this.GroupBox_Permissons.TabIndex = 220;
            this.GroupBox_Permissons.TabStop = false;
            this.GroupBox_Permissons.Text = "Console:";
            this.GroupBox_Permissons.Visible = false;
            // 
            // TextBox_Live_Log
            // 
            this.TextBox_Live_Log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBox_Live_Log.Location = new System.Drawing.Point(6, 17);
            this.TextBox_Live_Log.Multiline = true;
            this.TextBox_Live_Log.Name = "TextBox_Live_Log";
            this.TextBox_Live_Log.ReadOnly = true;
            this.TextBox_Live_Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox_Live_Log.Size = new System.Drawing.Size(793, 111);
            this.TextBox_Live_Log.TabIndex = 35;
            // 
            // ButtonFolderPermissonCheck
            // 
            this.ButtonFolderPermissonCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.ButtonFolderPermissonCheck.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.ButtonFolderPermissonCheck.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.ButtonFolderPermissonCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFolderPermissonCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonFolderPermissonCheck.ForeColor = System.Drawing.Color.Silver;
            this.ButtonFolderPermissonCheck.Location = new System.Drawing.Point(237, 38);
            this.ButtonFolderPermissonCheck.Name = "ButtonFolderPermissonCheck";
            this.ButtonFolderPermissonCheck.Size = new System.Drawing.Size(221, 25);
            this.ButtonFolderPermissonCheck.TabIndex = 218;
            this.ButtonFolderPermissonCheck.Text = "Check Folder Permissions";
            this.ButtonFolderPermissonCheck.UseVisualStyleBackColor = false;
            // 
            // ButtonFolderPermissonSet
            // 
            this.ButtonFolderPermissonSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.ButtonFolderPermissonSet.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.ButtonFolderPermissonSet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.ButtonFolderPermissonSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFolderPermissonSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonFolderPermissonSet.ForeColor = System.Drawing.Color.Silver;
            this.ButtonFolderPermissonSet.Location = new System.Drawing.Point(10, 38);
            this.ButtonFolderPermissonSet.Name = "ButtonFolderPermissonSet";
            this.ButtonFolderPermissonSet.Size = new System.Drawing.Size(221, 25);
            this.ButtonFolderPermissonSet.TabIndex = 217;
            this.ButtonFolderPermissonSet.Text = "Set Folder Permissions";
            this.ButtonFolderPermissonSet.UseVisualStyleBackColor = false;
            // 
            // TextFolderPermissions
            // 
            this.TextFolderPermissions.BackColor = System.Drawing.Color.Transparent;
            this.TextFolderPermissions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextFolderPermissions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TextFolderPermissions.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TextFolderPermissions.Location = new System.Drawing.Point(8, 12);
            this.TextFolderPermissions.Name = "TextFolderPermissions";
            this.TextFolderPermissions.Size = new System.Drawing.Size(177, 14);
            this.TextFolderPermissions.TabIndex = 216;
            this.TextFolderPermissions.Text = "FOLDER PERMISSONS:";
            this.TextFolderPermissions.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // TabPage_API
            // 
            this.TabPage_API.Controls.Add(this.Label_API_Status_Five);
            this.TabPage_API.Controls.Add(this.Label_API_Status_Four);
            this.TabPage_API.Controls.Add(this.Label_API_Status_Three);
            this.TabPage_API.Controls.Add(this.Label_API_Status_Two);
            this.TabPage_API.Controls.Add(this.Label_API_Status_One);
            this.TabPage_API.Controls.Add(this.Label_API_Status);
            this.TabPage_API.Location = new System.Drawing.Point(0, 26);
            this.TabPage_API.Name = "TabPage_API";
            this.TabPage_API.Size = new System.Drawing.Size(834, 354);
            this.TabPage_API.TabIndex = 4;
            this.TabPage_API.Text = "API";
            this.TabPage_API.UseVisualStyleBackColor = true;
            // 
            // Label_API_Status_Five
            // 
            this.Label_API_Status_Five.BackColor = System.Drawing.Color.Transparent;
            this.Label_API_Status_Five.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_API_Status_Five.Location = new System.Drawing.Point(14, 98);
            this.Label_API_Status_Five.Name = "Label_API_Status_Five";
            this.Label_API_Status_Five.Size = new System.Drawing.Size(360, 14);
            this.Label_API_Status_Five.TabIndex = 178;
            this.Label_API_Status_Five.Text = "Backup CDN List API: PINGING";
            this.Label_API_Status_Five.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_API_Status_Five.Visible = false;
            // 
            // Label_API_Status_Four
            // 
            this.Label_API_Status_Four.BackColor = System.Drawing.Color.Transparent;
            this.Label_API_Status_Four.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_API_Status_Four.Location = new System.Drawing.Point(14, 82);
            this.Label_API_Status_Four.Name = "Label_API_Status_Four";
            this.Label_API_Status_Four.Size = new System.Drawing.Size(360, 14);
            this.Label_API_Status_Four.TabIndex = 177;
            this.Label_API_Status_Four.Text = "Backup CDN List API: PINGING";
            this.Label_API_Status_Four.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_API_Status_Four.Visible = false;
            // 
            // Label_API_Status_Three
            // 
            this.Label_API_Status_Three.BackColor = System.Drawing.Color.Transparent;
            this.Label_API_Status_Three.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_API_Status_Three.Location = new System.Drawing.Point(14, 64);
            this.Label_API_Status_Three.Name = "Label_API_Status_Three";
            this.Label_API_Status_Three.Size = new System.Drawing.Size(360, 14);
            this.Label_API_Status_Three.TabIndex = 176;
            this.Label_API_Status_Three.Text = "Backup Server List API: PINGING";
            this.Label_API_Status_Three.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_API_Status_Three.Visible = false;
            // 
            // Label_API_Status_Two
            // 
            this.Label_API_Status_Two.BackColor = System.Drawing.Color.Transparent;
            this.Label_API_Status_Two.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_API_Status_Two.Location = new System.Drawing.Point(14, 46);
            this.Label_API_Status_Two.Name = "Label_API_Status_Two";
            this.Label_API_Status_Two.Size = new System.Drawing.Size(360, 14);
            this.Label_API_Status_Two.TabIndex = 175;
            this.Label_API_Status_Two.Text = "Main CDN List API: PINGING";
            this.Label_API_Status_Two.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_API_Status_Two.Visible = false;
            // 
            // Label_API_Status_One
            // 
            this.Label_API_Status_One.BackColor = System.Drawing.Color.Transparent;
            this.Label_API_Status_One.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_API_Status_One.Location = new System.Drawing.Point(14, 28);
            this.Label_API_Status_One.Name = "Label_API_Status_One";
            this.Label_API_Status_One.Size = new System.Drawing.Size(360, 14);
            this.Label_API_Status_One.TabIndex = 174;
            this.Label_API_Status_One.Text = "Main Server List API: PINGING";
            this.Label_API_Status_One.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_API_Status
            // 
            this.Label_API_Status.BackColor = System.Drawing.Color.Transparent;
            this.Label_API_Status.ForeColor = System.Drawing.Color.DarkGray;
            this.Label_API_Status.Location = new System.Drawing.Point(14, 13);
            this.Label_API_Status.Name = "Label_API_Status";
            this.Label_API_Status.Size = new System.Drawing.Size(360, 14);
            this.Label_API_Status.TabIndex = 173;
            this.Label_API_Status.Text = "API CONNECTION STATUS:";
            this.Label_API_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TabPage_About
            // 
            this.TabPage_About.Controls.Add(this.label5);
            this.TabPage_About.Controls.Add(this.label6);
            this.TabPage_About.Controls.Add(this.label3);
            this.TabPage_About.Controls.Add(this.label4);
            this.TabPage_About.Controls.Add(this.PatchText1);
            this.TabPage_About.Controls.Add(this.label2);
            this.TabPage_About.Controls.Add(this.label1);
            this.TabPage_About.Controls.Add(this.Picture_Logo);
            this.TabPage_About.Controls.Add(this.Label_Version_Build_About);
            this.TabPage_About.Controls.Add(this.Label_Theme_Author);
            this.TabPage_About.Controls.Add(this.Label_Theme_Name);
            this.TabPage_About.Location = new System.Drawing.Point(0, 26);
            this.TabPage_About.Name = "TabPage_About";
            this.TabPage_About.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_About.Size = new System.Drawing.Size(834, 354);
            this.TabPage_About.TabIndex = 1;
            this.TabPage_About.Text = "Version: XXX.XXX.XXX";
            this.TabPage_About.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("DejaVu Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.label5.Location = new System.Drawing.Point(506, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(195, 165);
            this.label5.TabIndex = 158;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.DarkGray;
            this.label6.Location = new System.Drawing.Point(506, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(195, 25);
            this.label6.TabIndex = 157;
            this.label6.Text = "Speical Thanks:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("DejaVu Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.label3.Location = new System.Drawing.Point(305, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(195, 165);
            this.label3.TabIndex = 156;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.DarkGray;
            this.label4.Location = new System.Drawing.Point(305, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(195, 25);
            this.label4.TabIndex = 155;
            this.label4.Text = "Contributors:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // PatchText1
            // 
            this.PatchText1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.PatchText1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PatchText1.Font = new System.Drawing.Font("DejaVu Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PatchText1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.PatchText1.Location = new System.Drawing.Point(104, 184);
            this.PatchText1.Name = "PatchText1";
            this.PatchText1.Size = new System.Drawing.Size(195, 165);
            this.PatchText1.TabIndex = 154;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.DarkGray;
            this.label2.Location = new System.Drawing.Point(104, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 25);
            this.label2.TabIndex = 153;
            this.label2.Text = "Support Core:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(235, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(330, 14);
            this.label1.TabIndex = 83;
            this.label1.Text = "Build: 11-08-18";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Picture_Logo
            // 
            this.Picture_Logo.BackColor = System.Drawing.Color.Transparent;
            this.Picture_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Picture_Logo.Location = new System.Drawing.Point(292, 6);
            this.Picture_Logo.Name = "Picture_Logo";
            this.Picture_Logo.Size = new System.Drawing.Size(215, 71);
            this.Picture_Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Picture_Logo.TabIndex = 82;
            this.Picture_Logo.TabStop = false;
            // 
            // Label_Version_Build_About
            // 
            this.Label_Version_Build_About.BackColor = System.Drawing.Color.Transparent;
            this.Label_Version_Build_About.ForeColor = System.Drawing.Color.Black;
            this.Label_Version_Build_About.Location = new System.Drawing.Point(235, 124);
            this.Label_Version_Build_About.Name = "Label_Version_Build_About";
            this.Label_Version_Build_About.Size = new System.Drawing.Size(330, 14);
            this.Label_Version_Build_About.TabIndex = 81;
            this.Label_Version_Build_About.Text = "Version: XX.XX.XX.XXXX";
            this.Label_Version_Build_About.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_Theme_Author
            // 
            this.Label_Theme_Author.BackColor = System.Drawing.Color.Transparent;
            this.Label_Theme_Author.ForeColor = System.Drawing.Color.Black;
            this.Label_Theme_Author.Location = new System.Drawing.Point(235, 106);
            this.Label_Theme_Author.Name = "Label_Theme_Author";
            this.Label_Theme_Author.Size = new System.Drawing.Size(330, 14);
            this.Label_Theme_Author.TabIndex = 80;
            this.Label_Theme_Author.Text = "Theme Author: What a Long Theme Name :0 Wow!";
            this.Label_Theme_Author.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_Theme_Name
            // 
            this.Label_Theme_Name.BackColor = System.Drawing.Color.Transparent;
            this.Label_Theme_Name.ForeColor = System.Drawing.Color.Black;
            this.Label_Theme_Name.Location = new System.Drawing.Point(235, 88);
            this.Label_Theme_Name.Name = "Label_Theme_Name";
            this.Label_Theme_Name.Size = new System.Drawing.Size(330, 14);
            this.Label_Theme_Name.TabIndex = 79;
            this.Label_Theme_Name.Text = "Theme Name: Soapbox Race World - Launcher Gen";
            this.Label_Theme_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(770, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 159;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // Button_Settings
            // 
            this.Button_Settings.BackColor = System.Drawing.Color.Transparent;
            this.Button_Settings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Button_Settings.Location = new System.Drawing.Point(805, 15);
            this.Button_Settings.Name = "Button_Settings";
            this.Button_Settings.Size = new System.Drawing.Size(25, 25);
            this.Button_Settings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Button_Settings.TabIndex = 158;
            this.Button_Settings.TabStop = false;
            this.Button_Settings.Visible = false;
            // 
            // Button_Close
            // 
            this.Button_Close.BackColor = System.Drawing.Color.Transparent;
            this.Button_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Button_Close.InitialImage = null;
            this.Button_Close.Location = new System.Drawing.Point(840, 15);
            this.Button_Close.Name = "Button_Close";
            this.Button_Close.Size = new System.Drawing.Size(25, 25);
            this.Button_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Button_Close.TabIndex = 157;
            this.Button_Close.TabStop = false;
            // 
            // Button_Security_Center
            // 
            this.Button_Security_Center.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Button_Security_Center.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Security_Center.Location = new System.Drawing.Point(39, 12);
            this.Button_Security_Center.Name = "Button_Security_Center";
            this.Button_Security_Center.Size = new System.Drawing.Size(154, 25);
            this.Button_Security_Center.TabIndex = 183;
            this.Button_Security_Center.Text = "Security Center";
            this.Button_Security_Center.UseVisualStyleBackColor = false;
            // 
            // Clock
            // 
            this.Clock.Enabled = true;
            this.Clock.Interval = 1200;
            // 
            // Screen_Settings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(891, 529);
            this.Controls.Add(this.Panel_Form_Screens);
            this.Controls.Add(this.Button_Security_Center);
            this.Controls.Add(this.TabControl_Shared_Hub);
            this.Controls.Add(this.Button_Console_Submit);
            this.Controls.Add(this.Input_Console);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Button_Settings);
            this.Controls.Add(this.Button_Close);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Screen_Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings - SBRW Launcher";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.TabControl_Shared_Hub.ResumeLayout(false);
            this.TabPage_Setup.ResumeLayout(false);
            this.TabPage_Settings.ResumeLayout(false);
            this.TabControl_Settings.ResumeLayout(false);
            this.TabPage_Launcher.ResumeLayout(false);
            this.TabControl_Launcher.ResumeLayout(false);
            this.TabPage_Launcher_Downloader.ResumeLayout(false);
            this.Panel_GameFiles_Downloader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_WebClient_Timeout)).EndInit();
            this.TabPage_Launcher_Proxy.ResumeLayout(false);
            this.Panel_Proxy_Logging.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Proxy_Port)).EndInit();
            this.TabPage_Launcher_Miscellaneous.ResumeLayout(false);
            this.Panel_Launcher_Builds_Branch.ResumeLayout(false);
            this.TabPage_Game.ResumeLayout(false);
            this.TabControl_Game.ResumeLayout(false);
            this.TabPage_Game_General.ResumeLayout(false);
            this.Panel_Display_Timer.ResumeLayout(false);
            this.TabPage_Game_Verify_Hash.ResumeLayout(false);
            this.TabPage_Game_Security_Center.ResumeLayout(false);
            this.TabControl_Security_Center.ResumeLayout(false);
            this.TabPage_Security_Center_Firewall.ResumeLayout(false);
            this.GroupBox_Firewall.ResumeLayout(false);
            this.GroupBox_Firewall.PerformLayout();
            this.TabPage_Security_Center_Defender.ResumeLayout(false);
            this.GroupBox_Defender.ResumeLayout(false);
            this.GroupBox_Defender.PerformLayout();
            this.TabPage_Security_Center_Permissons.ResumeLayout(false);
            this.GroupBox_Permissons.ResumeLayout(false);
            this.GroupBox_Permissons.PerformLayout();
            this.TabPage_API.ResumeLayout(false);
            this.TabPage_About.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Picture_Logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Button_Settings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Button_Close)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolTip ToolTip_Hover;
        public System.IO.FileSystemWatcher fileSystemWatcher1;
        public System.Windows.Forms.Panel Panel_Form_Screens;
        public System.Windows.Forms.Button Button_Console_Submit;
        public System.Windows.Forms.TextBox Input_Console;
        public System.Windows.Forms.Button Button_Save;
        public System.Windows.Forms.Button Button_Exit;
        private System.Windows.Forms.TabPage TabPage_Launcher;
        private System.Windows.Forms.TabPage TabPage_Launcher_Downloader;
        public System.Windows.Forms.Label Label_GameFiles_Downloader_Raw;
        public System.Windows.Forms.Label Label_GameFiles_Downloader_Pack;
        public System.Windows.Forms.Label Label_GameFiles_Downloader_LZMA;
        private System.Windows.Forms.Panel Panel_GameFiles_Downloader;
        private System.Windows.Forms.RadioButton Radio_Button_GameFiles_Downloader_LZMA;
        private System.Windows.Forms.RadioButton Radio_Button_GameFiles_Downloader_SBRW_Pack;
        private System.Windows.Forms.RadioButton Radio_Button_GameFiles_Downloader_Raw;
        public System.Windows.Forms.Label Label_CDN_Current_Details;
        public System.Windows.Forms.Label Label_WebClient_Timeout_Details;
        public System.Windows.Forms.Label Label_GameFiles_Downloader_Details;
        public System.Windows.Forms.Label Label_Alt_WebCalls_Details;
        public System.Windows.Forms.Label Label_CDN_Current;
        public System.Windows.Forms.Button Button_CDN_List;
        public System.Windows.Forms.LinkLabel LinkLabel_CDN_Current;
        public System.Windows.Forms.Label Label_GameFiles_Downloader;
        public System.Windows.Forms.CheckBox CheckBox_LZMA_Downloader;
        public System.Windows.Forms.CheckBox CheckBox_Alt_WebCalls;
        public System.Windows.Forms.Label Label_WebClient_Timeout;
        public System.Windows.Forms.NumericUpDown NumericUpDown_WebClient_Timeout;
        private System.Windows.Forms.TabPage TabPage_Launcher_Proxy;
        public System.Windows.Forms.Label Label_Proxy_Port;
        public System.Windows.Forms.NumericUpDown NumericUpDown_Proxy_Port;
        public System.Windows.Forms.CheckBox CheckBox_Proxy;
        public System.Windows.Forms.CheckBox CheckBox_Proxy_Domain;
        public System.Windows.Forms.CheckBox CheckBox_Host_to_IP;
        private System.Windows.Forms.TabPage TabPage_Launcher_Miscellaneous;
        public System.Windows.Forms.Label Label_Launcher_Builds_Branch_Developer;
        public System.Windows.Forms.Label Label_Launcher_Builds_Branch_Beta;
        public System.Windows.Forms.Label Label_Launcher_Builds_Branch_Stable;
        private System.Windows.Forms.Panel Panel_Launcher_Builds_Branch;
        private System.Windows.Forms.RadioButton Radio_Button_Launcher_Builds_Branch_Stable;
        private System.Windows.Forms.RadioButton Radio_Button_Launcher_Builds_Branch_Beta;
        private System.Windows.Forms.RadioButton Radio_Button_Launcher_Builds_Branch_Developer;
        public System.Windows.Forms.Label Label_Launcher_Builds_Branch_Details;
        public System.Windows.Forms.Label Label_Launcher_Builds_Branch;
        public System.Windows.Forms.CheckBox CheckBox_RPC;
        public System.Windows.Forms.LinkLabel LinkLabel_Launcher_Path;
        public System.Windows.Forms.CheckBox CheckBox_JSON_Update_Cache;
        public System.Windows.Forms.Label Label_Launcher_Path;
        public System.Windows.Forms.CheckBox CheckBox_Theme_Support;
        public System.Windows.Forms.CheckBox CheckBox_Opt_Insider;
        private System.Windows.Forms.TabPage TabPage_Game;
        private System.Windows.Forms.TabPage TabPage_API;
        public System.Windows.Forms.Label Label_API_Status_Five;
        public System.Windows.Forms.Label Label_API_Status_Four;
        public System.Windows.Forms.Label Label_API_Status_Three;
        public System.Windows.Forms.Label Label_API_Status_Two;
        public System.Windows.Forms.Label Label_API_Status_One;
        public System.Windows.Forms.Label Label_API_Status;
        private System.Windows.Forms.TabPage TabPage_About;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label PatchText1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.PictureBox Picture_Logo;
        public System.Windows.Forms.Label Label_Version_Build_About;
        public System.Windows.Forms.Label Label_Theme_Author;
        public System.Windows.Forms.Label Label_Theme_Name;
        public System.Windows.Forms.Button Button_Change_Tabs;
        public Core.Theme.Control_TabControl TabControl_Shared_Hub;
        public Core.Theme.Control_TabControl TabControl_Settings;
        public Core.Theme.Control_TabControl TabControl_Launcher;
        public System.Windows.Forms.Label Label_Version;
        public System.Windows.Forms.Button Button_Save_Setup;
        public System.Windows.Forms.Label Label_Introduction;
        public System.Windows.Forms.Label label19;
        public System.Windows.Forms.Label Label_CDN_Current_Setup;
        public System.Windows.Forms.Button Button_CDN_List_Setup;
        public System.Windows.Forms.LinkLabel LinkLabel_CDN_Current_Setup;
        public System.Windows.Forms.LinkLabel LinkLabel_Game_Path_Setup;
        public System.Windows.Forms.Label Label_Game_Current_Path_Setup;
        public System.Windows.Forms.Button Button_Change_Game_Path_Setup;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.PictureBox Button_Settings;
        public System.Windows.Forms.PictureBox Button_Close;
        public System.Windows.Forms.TabPage TabPage_Settings;
        public System.Windows.Forms.TabPage TabPage_Setup;
        public System.Windows.Forms.Label Label_Proxy_Logging_Requests;
        public System.Windows.Forms.Label Label_Proxy_Logging_Errors;
        public System.Windows.Forms.Label Label_Proxy_Logging_All;
        private System.Windows.Forms.Panel Panel_Proxy_Logging;
        private System.Windows.Forms.RadioButton Radio_Button_Proxy_Logging_All;
        private System.Windows.Forms.RadioButton Radio_Button_Proxy_Logging_Errors;
        private System.Windows.Forms.RadioButton Radio_Button_Proxy_Logging_Requests;
        public System.Windows.Forms.Label Label_Proxy_Logging_Details;
        public System.Windows.Forms.Label Label_Proxy_Logging;
        private System.Windows.Forms.RadioButton Radio_Button_Proxy_Logging_Responses;
        private System.Windows.Forms.RadioButton Radio_Button_Proxy_Logging_None;
        public System.Windows.Forms.Label Label_Proxy_Logging_None;
        public System.Windows.Forms.Label Label_Proxy_Logging_Responses;
        public Core.Theme.Control_TabControl TabControl_Game;
        private System.Windows.Forms.TabPage TabPage_Game_General;
        public System.Windows.Forms.Label Label_Display_Timer;
        public System.Windows.Forms.Panel Panel_Display_Timer;
        public System.Windows.Forms.RadioButton Radio_Button_Static_Timer;
        public System.Windows.Forms.RadioButton Radio_Button_Dynamic_Timer;
        public System.Windows.Forms.RadioButton Radio_Button_No_Timer;
        public System.Windows.Forms.CheckBox CheckBox_Word_Filter_Check;
        public System.Windows.Forms.LinkLabel LinkLabel_Game_Path;
        public System.Windows.Forms.Label Label_Game_Current_Path;
        public System.Windows.Forms.Button Button_Game_User_Settings;
        public System.Windows.Forms.Label Label_Game_Settings;
        public System.Windows.Forms.ComboBox ComboBox_Language_List;
        public System.Windows.Forms.Button Button_Game_Verify_Files;
        public System.Windows.Forms.Button Button_Change_Game_Path;
        public System.Windows.Forms.Label Label_Game_Files;
        private System.Windows.Forms.TabPage TabPage_Game_Verify_Hash;
        private System.Windows.Forms.TabPage TabPage_Game_Security_Center;
        public System.Windows.Forms.Button Button_Launcher_logs;
        public System.Windows.Forms.Button Button_Clear_NFSWO_Logs;
        public System.Windows.Forms.Button Button_Clear_Server_Mods;
        public System.Windows.Forms.Button Button_Clear_Crash_Logs;
        public System.Windows.Forms.Button Button_Security_Center;
        private System.Windows.Forms.Label VerifyHashText;
        private System.Windows.Forms.Label ScanProgressText;
        private System.Windows.Forms.ProgressBar ScanProgressBar;
        private System.Windows.Forms.ProgressBar DownloadProgressBar;
        private System.Windows.Forms.Button StartScanner;
        private System.Windows.Forms.Button StopScanner;
        public Core.Theme.Control_TabControl TabControl_Security_Center;
        private System.Windows.Forms.TabPage TabPage_Security_Center_Firewall;
        private System.Windows.Forms.TabPage TabPage_Security_Center_Defender;
        private System.Windows.Forms.TabPage TabPage_Security_Center_Permissons;
        private System.Windows.Forms.Button ButtonFirewallRulesAPI;
        private System.Windows.Forms.Button ButtonFirewallRulesCheck;
        private System.Windows.Forms.Button ButtonFirewallRulesAddAll;
        private System.Windows.Forms.Button ButtonFirewallRulesAddGame;
        private System.Windows.Forms.Button ButtonFirewallRulesAddLauncher;
        private System.Windows.Forms.Button ButtonFirewallRulesRemoveGame;
        private System.Windows.Forms.Button ButtonFirewallRulesRemoveLauncher;
        private System.Windows.Forms.Button ButtonFirewallRulesRemoveAll;
        private System.Windows.Forms.Label TextWindowsFirewall;
        private System.Windows.Forms.Button ButtonDefenderExclusionAPI;
        private System.Windows.Forms.Button ButtonDefenderExclusionAddGame;
        private System.Windows.Forms.Button ButtonDefenderExclusionAddLauncher;
        private System.Windows.Forms.Button ButtonDefenderExclusionAddAll;
        private System.Windows.Forms.Button ButtonDefenderExclusionCheck;
        private System.Windows.Forms.Button ButtonDefenderExclusionRemoveGame;
        private System.Windows.Forms.Button ButtonDefenderExclusionRemoveLauncher;
        private System.Windows.Forms.Button ButtonDefenderExclusionRemoveAll;
        private System.Windows.Forms.Label TextWindowsDefender;
        public System.Windows.Forms.GroupBox GroupBox_Permissons;
        public System.Windows.Forms.TextBox TextBox_Live_Log;
        private System.Windows.Forms.Button ButtonFolderPermissonCheck;
        private System.Windows.Forms.Button ButtonFolderPermissonSet;
        private System.Windows.Forms.Label TextFolderPermissions;
        public System.Windows.Forms.GroupBox GroupBox_Defender;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.GroupBox GroupBox_Firewall;
        public System.Windows.Forms.TextBox TextBox_Console_Firewall;
        private System.Windows.Forms.Label DownloadProgressText;
        private System.Windows.Forms.Label VerifyHashWelcome;
        private System.Windows.Forms.Label DownloadProgressText_Alt;
        public System.Windows.Forms.Timer Clock;
    }
}