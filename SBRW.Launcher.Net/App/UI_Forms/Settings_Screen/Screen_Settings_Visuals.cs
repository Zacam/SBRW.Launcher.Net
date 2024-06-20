using SBRW.Launcher.App.UI_Forms.Parent_Screen;
using SBRW.Launcher.Core.Extra.File_.Save_;
using SBRW.Launcher.Core.Extra.XML_;
using SBRW.Launcher.Core.Reference.Json_.Newtonsoft_;
using SBRW.Launcher.Core.Theme;
using SBRW.Launcher.RunTime.LauncherCore.APICheckers;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.Lists;
using SBRW.Launcher.RunTime.LauncherCore.Support;
using SBRW.Launcher.RunTime.LauncherCore.Visuals;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SBRW.Launcher.App.UI_Forms.Settings_Screen
{
    partial class Screen_Settings
    {
        public static void Clear_Hide_Screen_Form_Panel()
        {
            if (Screen_Instance != null)
            {
                if (Screen_Instance.Panel_Form_Screens.Visible)
                {
                    Screen_Instance.Panel_Form_Screens.Controls.Clear();
                    Screen_Instance.Panel_Form_Screens.Visible = false;
                }
            }

            if (Screen_Instance != null)
            {
                Screen_Instance.Text = "Settings - SBRW Launcher: " + Application.ProductVersion;
            }
        }
        #region Update Values and Visuals
        /// <summary>
        /// Sets the Colors for the Proxy Logging Drop Down Menu
        /// </summary>
        /// <remarks>Dropdown Menu Visual</remarks>
        private void ComboBox_Proxy_Logging_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                string Log_Mode_Name = string.Empty;

                if (sender is ComboBox cb)
                {
                    if (e.Index != -1 && cb.Items != null)
                    {
                        if (cb.Items[e.Index] is Json_List_Proxy_Logging si)
                        {
                            Log_Mode_Name = si.Name;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(Log_Mode_Name) && sender != null)
                {
                    Font font = ((ComboBox)sender).Font;
                    Brush backgroundColor;
                    Brush textColor;

                    font = new Font(font, FontStyle.Bold);
                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected && e.State != DrawItemState.ComboBoxEdit)
                    {
                        backgroundColor = SystemBrushes.Highlight;
                        textColor = SystemBrushes.HighlightText;
                    }
                    else
                    {
                        backgroundColor = new SolidBrush(Color_Winform_Other.DropMenu_Background_ForeColor);
                        textColor = new SolidBrush(Color_Winform_Other.DropMenu_Text_ForeColor);
                    }

                    e.Graphics.FillRectangle(backgroundColor, e.Bounds);
                    e.Graphics.DrawString("    " + Log_Mode_Name, font, textColor, e.Bounds);
                }
            }
            catch { }
        }
        /// <summary>
        /// Sets the Colors for the Proxy Gzip Version Drop Down Menu
        /// </summary>
        /// <remarks>Dropdown Menu Visual</remarks>
        private void ComboBox_Proxy_Gzip_Version_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                string Gzip_Version_Name = string.Empty;

                if (sender is ComboBox cb)
                {
                    if (e.Index != -1 && cb.Items != null)
                    {
                        if (cb.Items[e.Index] is Json_List_Proxy_GZip_Version si)
                        {
                            Gzip_Version_Name = si.Name;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(Gzip_Version_Name) && sender != null)
                {
                    Font font = ((ComboBox)sender).Font;
                    Brush backgroundColor;
                    Brush textColor;

                    font = new Font(font, FontStyle.Bold);
                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected && e.State != DrawItemState.ComboBoxEdit)
                    {
                        backgroundColor = SystemBrushes.Highlight;
                        textColor = SystemBrushes.HighlightText;
                    }
                    else
                    {
                        backgroundColor = new SolidBrush(Color_Winform_Other.DropMenu_Background_ForeColor);
                        textColor = new SolidBrush(Color_Winform_Other.DropMenu_Text_ForeColor);
                    }

                    e.Graphics.FillRectangle(backgroundColor, e.Bounds);
                    e.Graphics.DrawString("    " + Gzip_Version_Name, font, textColor, e.Bounds);
                }
            }
            catch { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Greenbutton_hover_MouseEnter(object sender, EventArgs e)
        {
            if (Button_Save.Image != Image_Button.Green_Hover)
            {
                Button_Save.Image = Image_Button.Green_Hover;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Greenbutton_MouseLeave(object sender, EventArgs e)
        {
            if (Button_Save.Image != Image_Button.Green)
            {
                Button_Save.Image = Image_Button.Green;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Greenbutton_hover_MouseUp(object sender, EventArgs e)
        {
            if (Button_Save.Image != Image_Button.Green_Hover)
            {
                Button_Save.Image = Image_Button.Green_Hover;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Greenbutton_click_MouseDown(object sender, EventArgs e)
        {
            if (Button_Save.Image != Image_Button.Green_Click)
            {
                Button_Save.Image = Image_Button.Green_Click;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Graybutton_click_MouseDown(object sender, EventArgs e)
        {
            if (Button_Exit.Image != Image_Button.Grey_Click)
            {
                Button_Exit.Image = Image_Button.Grey_Click;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Graybutton_hover_MouseEnter(object sender, EventArgs e)
        {
            if (Button_Exit.Image != Image_Button.Grey_Hover)
            {
                Button_Exit.Image = Image_Button.Grey_Hover;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Graybutton_MouseLeave(object sender, EventArgs e)
        {
            if (Button_Exit.Image != Image_Button.Grey)
            {
                Button_Exit.Image = Image_Button.Grey;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Graybutton_hover_MouseUp(object sender, EventArgs e)
        {
            if (Button_Exit.Image != Image_Button.Grey_Hover)
            {
                Button_Exit.Image = Image_Button.Grey_Hover;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClose_MouseDown(object sender, EventArgs e)
        {
            Button_Close.BackgroundImage = Button_Close.Icon_Order(SVG_Icon.Cross, SVG_Color.White_Select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClose_MouseEnter(object sender, EventArgs e)
        {
            Button_Close.BackgroundImage = Button_Close.Icon_Order(SVG_Icon.Cross, SVG_Color.White_Highlight);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClose_MouseLeaveANDMouseUp(object sender, EventArgs e)
        {
            Button_Close.BackgroundImage = Button_Close.Icon_Order(SVG_Icon.Cross, SVG_Color.White);
        }
        /// <summary>
        /// Sets the Button, Image, Text, and Fonts. Enables/Disables Certain Elements of the Screen for Certain Platforms. Also contains functions that act as helper functions
        /// </summary>
        /// <remarks>Settings Screen Visuals</remarks>
        private void Set_Visuals()
        {
            #region Primary Settings
            /*******************************/
            /* Set Window Name              /
            /*******************************/

            Icon = FormsIcon.Retrive_Icon();
            Text = "Settings - SBRW Launcher: " + Application.ProductVersion;

            /*******************************/
            /* Set Background Image         /
            /*******************************/

            BackgroundImage = Image_Background.Settings;
            TransparencyKey = Color_Screen.BG_Settings;

            /*******************************/
            /* Set Hardcoded Text           /
            /*******************************/

            New_Choosen_CDN = LinkLabel_CDN_Current.Text = Save_Settings.Live_Data.Launcher_CDN;
            LinkLabel_Game_Path.Text = Save_Settings.Live_Data.Game_Path;
            LinkLabel_Launcher_Path.Text = AppDomain.CurrentDomain.BaseDirectory;
            TabPage_About.Text = Label_Version_Build_About.Text = "Version: " + Application.ProductVersion;

            //TODO Add Implementation for Setting Range if User has Range Enabled
            Enable_Affinity_Range(CheckBox_Enable_Affinity_Range.Checked);

            /*******************************/
            /* Set Font                     /
            /*******************************/
#if !(RELEASE_UNIX || DEBUG_UNIX)
            float MainFontSize = 9f * 96f / CreateGraphics().DpiY;
            float SecondaryFontSize = 8f * 96f / CreateGraphics().DpiY;
#else
            float MainFontSize = 9f;
            float SecondaryFontSize = 8f;
#endif
            /* General */
            Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Regular);

            Button_Console_Submit.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            Input_Console.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            /* */
            Label_Game_Files.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Button_Change_Game_Path.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Button_Change_Game_Path_Setup.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Button_Game_Verify_Files.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_Game_Settings.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Button_Game_User_Settings.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Button_Clear_Crash_Logs.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            Button_Launcher_logs.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            Button_Clear_NFSWO_Logs.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            Button_Clear_Server_Mods.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            #region FONT: Setup Tab
            /* Setup Tab */
            Button_CDN_List_Setup.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            #endregion
            #region FONT: Settings Tab
            /* Global */
            Button_Save.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Button_Exit.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            #region FONT: Launcher Tab
            /* About Tab */
            Label_Version_Build_About.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_Theme_Name.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_Theme_Author.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            /* API Tab */
            Label_API_Status.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_API_Status_One.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_API_Status_Two.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_API_Status_Three.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_API_Status_Four.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_API_Status_Five.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            /* Downloader Tab */
            CheckBox_Alt_WebCalls.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_WebClient_Timeout.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Alt_WebCalls_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_WebClient_Timeout_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            NumericUpDown_WebClient_Timeout.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_CDN_Current.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_CDN_Current_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            LinkLabel_CDN_Current.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Button_CDN_List.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            Label_GameFiles_Downloader.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_GameFiles_Downloader_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_GameFiles_Downloader_LZMA.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_GameFiles_Downloader_Pack.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_GameFiles_Downloader_Raw.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Radio_Button_GameFiles_Downloader_LZMA.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Radio_Button_GameFiles_Downloader_SBRW_Pack.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Radio_Button_GameFiles_Downloader_Raw.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            /* Proxy Tab */
            CheckBox_Proxy.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            CheckBox_Host_to_IP.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            CheckBox_Proxy_Domain.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_Proxy_Port.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            NumericUpDown_Proxy_Port.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Proxy_Logging.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Proxy_Logging_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Proxy_Logging_Selected_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            /* Miscellaneous */
            CheckBox_RPC.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            CheckBox_JSON_Update_Cache.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            CheckBox_Theme_Support.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_Launcher_Builds_Branch.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Launcher_Builds_Branch_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Launcher_Builds_Branch_Stable.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Launcher_Builds_Branch_Beta.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Launcher_Builds_Branch_Developer.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Radio_Button_Launcher_Builds_Branch_Stable.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Radio_Button_Launcher_Builds_Branch_Beta.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Radio_Button_Launcher_Builds_Branch_Developer.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_Launcher_Path.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            LinkLabel_Launcher_Path.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            #endregion
            #region FONT: Game Tab
            Label_Game_Current_Path.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            LinkLabel_Game_Path.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);

            Label_Display_Timer.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Radio_Button_Static_Timer.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Radio_Button_Dynamic_Timer.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Radio_Button_No_Timer.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            CheckBox_Word_Filter_Check.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            #endregion
            #endregion

            /********************************/
            /* Set Theme Colors & Images     /
            /********************************/

            /* Buttons */
            ButtonsColorSet(Button_Change_Game_Path, Screen_Parent.Launcher_Setup.Equals(1) ? 2 : 0, true);
            ButtonsColorSet(Button_Change_Game_Path_Setup, Screen_Parent.Launcher_Setup.Equals(1) ? 2 : 0, true);
            ButtonsColorSet(Button_Game_Verify_Files, 0, false);
            ButtonsColorSet(Button_Game_User_Settings, 0, true);
            ButtonsColorSet(Button_Clear_Crash_Logs, 0, false);
            ButtonsColorSet(Button_Launcher_logs, 0, true);
            ButtonsColorSet(Button_Clear_NFSWO_Logs, 0, false);
            ButtonsColorSet(Button_Clear_Server_Mods, 0, false);
            ButtonsColorSet(Button_CDN_List, VisualsAPIChecker.Local_Cached_API() ? (Screen_Parent.Launcher_Setup.Equals(1) ? 2 : 0) : 4, true);
            ButtonsColorSet(Button_CDN_List_Setup, VisualsAPIChecker.Local_Cached_API() ? (Screen_Parent.Launcher_Setup.Equals(1) ? 2 : 0) : 4, true);
            ButtonsColorSet(Button_Console_Submit, 1, true);
            ButtonsColorSet(Button_Save_Setup, 4, false);
            ButtonsColorSet(Button_Change_Tabs, 0, true);

            /* Label Links */
            LinkLabel_Game_Path.LinkColor = Color_Winform_Other.Link_Settings;
            LinkLabel_Game_Path.ActiveLinkColor = Color_Winform_Other.Link_Settings_Active;
            LinkLabel_CDN_Current.LinkColor = Color_Winform_Other.Link_Settings;
            LinkLabel_CDN_Current.ActiveLinkColor = Color_Winform_Other.Link_Settings_Active;
            LinkLabel_Launcher_Path.LinkColor = Color_Winform_Other.Link_Settings;
            LinkLabel_Launcher_Path.ActiveLinkColor = Color_Winform_Other.Link_Settings_Active;

            /* Labels */
            Label_Game_Current_Path.ForeColor = Color_Text.L_Five;
            Label_Game_Current_Path.ForeColor = Color_Text.L_Five;
            Label_CDN_Current.ForeColor = Color_Text.L_Five;
            Label_Launcher_Path.ForeColor = Color_Text.L_Five;
            Label_GameFiles_Downloader.ForeColor = Color_Text.L_Five;
            Label_Game_Settings.ForeColor = Color_Text.L_Five;
            Label_API_Status.ForeColor = Color_Text.L_Five;
            Label_Display_Timer.ForeColor = Color_Text.L_Five;
            Label_WebClient_Timeout.ForeColor = Color_Text.L_Five;
            Label_Proxy_Port.ForeColor = Color_Text.L_Five;

            /* Input Boxes */
            NumericUpDown_WebClient_Timeout.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            NumericUpDown_WebClient_Timeout.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            NumericUpDown_Proxy_Port.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            NumericUpDown_Proxy_Port.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;

            /* Check boxes */
            CheckBox_Word_Filter_Check.ForeColor = Color_Winform_Other.CheckBoxes_Settings;
            CheckBox_Proxy.ForeColor = Color_Winform_Other.CheckBoxes_Settings;
            CheckBox_RPC.ForeColor = Color_Winform_Other.CheckBoxes_Settings;
            CheckBox_Alt_WebCalls.ForeColor = Color_Winform_Other.CheckBoxes_Settings;
            CheckBox_Theme_Support.ForeColor = Color_Winform_Other.CheckBoxes_Settings;
            CheckBox_JSON_Update_Cache.ForeColor = Color_Winform_Other.CheckBoxes_Settings;
            CheckBox_Host_to_IP.ForeColor = Color_Winform_Other.CheckBoxes_Settings;
            CheckBox_Proxy_Domain.ForeColor = Color_Winform_Other.CheckBoxes_Settings;

            /* Radio Buttons */
            Radio_Button_Static_Timer.ForeColor = Color_Winform.Text_Fore_Color;
            Radio_Button_Dynamic_Timer.ForeColor = Color_Winform.Text_Fore_Color;
            Radio_Button_No_Timer.ForeColor = Color_Winform.Text_Fore_Color;
            Radio_Button_GameFiles_Downloader_LZMA.ForeColor = Color_Winform.Text_Fore_Color;
            Radio_Button_GameFiles_Downloader_SBRW_Pack.ForeColor = Color_Winform.Text_Fore_Color;
            Radio_Button_GameFiles_Downloader_Raw.ForeColor = Color_Winform.Text_Fore_Color;

            /* Bottom Left */
            Label_Version_Build_About.ForeColor = Color_Text.L_Five;
            Label_Theme_Name.ForeColor = Color_Text.L_Five;
            Label_Theme_Author.ForeColor = Color_Text.L_Five;

            /* Main Settings Buttons (Save or Cancel) */
            Button_Save.ForeColor = Color_Text.L_Seven;
            Button_Save.Image = Image_Button.Green;
            Button_Exit.Image = Image_Button.Grey;
            Button_Exit.ForeColor = Color_Text.L_One;

            Input_Console.BackColor = Color_Winform_Other.Input;
            Input_Console.ForeColor = Color_Text.L_Five;

            /* Secondary Buttons */
            Button_Close.BackgroundImage = Button_Close.Icon_Order(SVG_Icon.Cross, SVG_Color.White);

            /*******************************/
            /* Load CDN List                /
            /*******************************/

            if (!CDNListUpdater.LoadedList)
            {
                CDNListUpdater.GetList();
            }
            
            ComboBox_Proxy_Logging.DisplayMember = "Name";
            ComboBox_Proxy_Logging.DataSource = ProxySettingsListUpdater.Proxy_Logging;
            ComboBox_Proxy_GZip_Version.DisplayMember = "Name";
            ComboBox_Proxy_GZip_Version.DataSource = ProxySettingsListUpdater.Proxy_GZip_Version;

            /********************************/
            /* Events                        /
            /********************************/

            ComboBox_Proxy_Logging.DrawItem += new DrawItemEventHandler(ComboBox_Proxy_Logging_DrawItem);
            ComboBox_Proxy_Logging.SelectedIndexChanged += new EventHandler(ComboBox_Proxy_Logging_SelectedIndexChanged);
            ComboBox_Proxy_Logging.MouseWheel += new MouseEventHandler(DropDownMenu_MouseWheel);

            ComboBox_Proxy_GZip_Version.DrawItem += new DrawItemEventHandler(ComboBox_Proxy_Gzip_Version_DrawItem);
            ComboBox_Proxy_GZip_Version.SelectedIndexChanged += new EventHandler(ComboBox_Proxy_GZip_Version_SelectedIndexChanged);
            ComboBox_Proxy_GZip_Version.MouseWheel += new MouseEventHandler(DropDownMenu_MouseWheel);

            Button_Save.MouseEnter += new EventHandler(Greenbutton_hover_MouseEnter);
            Button_Save.MouseLeave += new EventHandler(Greenbutton_MouseLeave);
            Button_Save.MouseUp += new MouseEventHandler(Greenbutton_hover_MouseUp);
            Button_Save.MouseDown += new MouseEventHandler(Greenbutton_click_MouseDown);

            Button_Exit.MouseEnter += new EventHandler(Graybutton_hover_MouseEnter);
            Button_Exit.MouseLeave += new EventHandler(Graybutton_MouseLeave);
            Button_Exit.MouseUp += new MouseEventHandler(Graybutton_hover_MouseUp);
            Button_Exit.MouseDown += new MouseEventHandler(Graybutton_click_MouseDown);

            Input_Console.KeyDown += new KeyEventHandler(Console_Quick_Send);
            Button_Console_Submit.Click += new EventHandler(Console_Enter);
            Button_CDN_List.Click += new EventHandler(Button_CDN_Selector_Click);
            Button_CDN_List_Setup.Click += new EventHandler(Button_CDN_Selector_Click);
            Button_Game_Verify_Files.Click += new EventHandler(Button_Game_Verify_Files_Click);
            Button_Save.Click += new EventHandler(SettingsSave_Click);
            Button_Save_Setup.Click += new EventHandler(SettingsSave_Click);
            Button_Experiments.Click += new EventHandler(Button_Experiments_Click);
            Button_Exit.Click += new EventHandler(SettingsCancel_Click);
            Button_Game_User_Settings.Click += new EventHandler(SettingsUEditorButton_Click);
            Button_Clear_Server_Mods.Click += new EventHandler(SettingsClearServerModCacheButton_Click);
            Button_Clear_NFSWO_Logs.Click += new EventHandler(SettingsClearCommunicationLogButton_Click);
            Button_Clear_Crash_Logs.Click += new EventHandler(SettingsClearCrashLogsButton_Click);
            Label_Version_Build_About.Click += new EventHandler(Label_Version_Build_Click);
            Button_Change_Game_Path.Click += new EventHandler(SettingsGameFiles_Click);
            Button_Change_Game_Path_Setup.Click += new EventHandler(SettingsGameFiles_Click);
            Button_Launcher_logs.Click += new EventHandler(SettingsClearLauncherLogsButton_Click);

            Button_Change_Tabs.Click += new EventHandler(Button_Change_Tabs_Click);
            /* Close */
            Button_Close.MouseEnter += new EventHandler(ButtonClose_MouseEnter);
            Button_Close.MouseLeave += new EventHandler(ButtonClose_MouseLeaveANDMouseUp);
            Button_Close.MouseUp += new MouseEventHandler(ButtonClose_MouseLeaveANDMouseUp);
            Button_Close.MouseDown += new MouseEventHandler(ButtonClose_MouseDown);
            Button_Close.Click += new EventHandler(ButtonClose_Click);

            LinkLabel_Launcher_Path.LinkClicked += new LinkLabelLinkClickedEventHandler(SettingsLauncherPathCurrent_LinkClicked);
            LinkLabel_CDN_Current.LinkClicked += new LinkLabelLinkClickedEventHandler(SettingsCDNCurrent_LinkClicked);
            LinkLabel_Game_Path.LinkClicked += new LinkLabelLinkClickedEventHandler(SettingsGameFilesCurrent_LinkClicked);

            if (Screen_Parent.Screen_Instance != null)
            {
                MouseMove += new MouseEventHandler(Screen_Parent.Screen_Instance.Move_Window_Mouse_Move);
                MouseUp += new MouseEventHandler(Screen_Parent.Screen_Instance.Move_Window_Mouse_Up);
                MouseDown += new MouseEventHandler(Screen_Parent.Screen_Instance.Move_Window_Mouse_Down);
            }

            Load += new EventHandler(Screen_Settings_Load);

            KeyPreview = true;

            NumericUpDown_Range_Affinity.ValueChanged += new EventHandler(NumericUpDown_Range_Affinity_ValueChanged);
            CheckBox_Enable_Affinity_Range.CheckedChanged += new EventHandler(CheckBox_Enable_Affinity_Range_CheckedChanged);
            CheckBox_RPC.CheckedChanged += new EventHandler(CheckBox_RPC_CheckedChanged);
            CheckBox_JSON_Update_Cache.CheckedChanged += new EventHandler(CheckBox_JSON_Update_Cache_CheckedChanged);
            CheckBox_Theme_Support.CheckedChanged += new EventHandler(CheckBox_Theme_Support_CheckedChanged);
            CheckBox_Enable_ACM.CheckedChanged += new EventHandler(CheckBox_Enable_ACM_CheckedChanged);

            /********************************/
            /* Sets Red Buttons/Disables     /
            /********************************/

            if (FunctionStatus.IsVerifyHashDisabled)
            {
                ButtonsColorSet(Button_Game_Verify_Files, 3, true);
            }

            /*******************************/
            /* Set ToolTip Texts            /
            /*******************************/

            ToolTip_Hover.SetToolTip(Button_Change_Game_Path, "Change the location of where the \'nfsw.exe\' that the Launcher will run");
            ToolTip_Hover.SetToolTip(Button_Change_Game_Path_Setup, "Change the location of where the \'nfsw.exe\' that the Launcher will run");
            ToolTip_Hover.SetToolTip(Button_Game_Verify_Files, "Checks and Restores GameFiles back to \"Stock\"");
            ToolTip_Hover.SetToolTip(Button_CDN_List, "Download Location for Fetching the base GameFiles\n" +
                "Can also be a Soruce for VerifyHash to get replacement files");
            ToolTip_Hover.SetToolTip(Button_CDN_List_Setup, "Download Location for Fetching the base GameFiles\n" +
                "Can also be a Soruce for VerifyHash to get replacement files");
            ToolTip_Hover.SetToolTip(Button_Game_User_Settings, "Opens a UserSettings.xml Editor\nAllows in-depth control over Game Settings");

            ToolTip_Hover.SetToolTip(Button_Clear_Crash_Logs, "Removes \"SBRCrashLogs_*\" DMP and TXT files from GameFiles Folder");

            ToolTip_Hover.SetToolTip(Button_Launcher_logs, "Removes all but current session \"LOGS\\\" folders");
            ToolTip_Hover.SetToolTip(Button_Clear_Server_Mods, "Erases all Server Mods from .data/MODS folders");

            ToolTip_Hover.SetToolTip(CheckBox_Word_Filter_Check, "Disables the In-Game Chat \"censor\" or word filter.");
            ToolTip_Hover.SetToolTip(CheckBox_Proxy, "Disables the Launcher Proxy communications hook.\n" +
                "Can not be turned off for httpS Servers.\n" +
                "Will also impact/limit the DiscordRPC functions.");
            ToolTip_Hover.SetToolTip(CheckBox_RPC, "Prevents Launcher from sending Discord Presence information.");

            ToolTip_Hover.SetToolTip(CheckBox_Theme_Support, "Enables supporting External Themes for the Launcher");
            /* @Zacam: Update Text to reflect new options 
            ToolTip_Hover.SetToolTip(CheckBox_Legacy_Timer, "Setting for Legacy Timer:\n" +
                "If Checked, this restores count down timer on Window Title\n" +
                "If Unchecked, displays the time on when the session ends"); */
            ToolTip_Hover.SetToolTip(CheckBox_Alt_WebCalls, "Changes the internal method used by Launcher for Communications\n" +
                "Unchecked: Uses \'standard\' WebClient calls\n" +
                "Checked: Uses WebClientWithTimeout");
            ToolTip_Hover.SetToolTip(CheckBox_JSON_Update_Cache, "Setting for JSON Update Cache Frequency:\n" +
                "If Checked, this enables daily cache update for Launcher Related JSON Files\n" +
                "If Unchecked, enables hourly cache update for Launcher Related JSON Files");

            Shown += (x, y) =>
            {
                RememberLastProxySettings();
                PingSavedCDN();
                PingAPIStatus();
            };

            Picture_Logo.BackgroundImage = Image_Other.Logo;

            BackColor = Color_Winform_About.BG_Fore_Color;
            ForeColor = Color_Winform_About.Text_Fore_Color;

            /* Tabs Global Background Color */
            TabControl_Shared_Hub.BackColor = TabControl_Settings.BackColor = TabControl_Launcher.BackColor = TabControl_Game.BackColor = TabControl_Security_Center.BackColor = Color.FromArgb(22, 29, 38);
            /* Tabs (Menu) Text Color */
            TabControl_Shared_Hub.ForeColor = TabControl_Settings.ForeColor = TabControl_Launcher.ForeColor = TabControl_Game.ForeColor = TabControl_Security_Center.ForeColor = Color.FromArgb(192, 192, 192);
            /* Tabs Current Selected & Hover Menu Tab */
            TabControl_Shared_Hub.SelectedTabColor = TabControl_Settings.SelectedTabColor = TabControl_Launcher.SelectedTabColor = TabControl_Game.SelectedTabColor = TabControl_Security_Center.SelectedTabColor = Color.FromArgb(128, 44, 58, 76);
            /* Tabs Other Menu Tab */
            TabControl_Shared_Hub.TabColor = TabControl_Settings.TabColor = TabControl_Launcher.TabColor = TabControl_Game.TabColor = TabControl_Security_Center.TabColor = Color.FromArgb(44, 58, 76);
            /* */
            TabControl_Shared_Hub.TabsHide = true;
            /* */
            Button_Save.DialogResult = DialogResult.OK;
            Button_Exit.DialogResult = DialogResult.Cancel;

            if (Screen_Parent.Launcher_Setup.Equals(1))
            {
                Button_Exit.Text = "Basic";
            }
            else
            {
                /* */
                ((Control)TabPage_Setup).Enabled = false;
            }
            #endregion
            /* Theming, Function, EventHandlers, Etc. Meant to load critial functions before the forms loads */
            #region Security Center Tab
            /********************************/
            /* Set Theme Colors & Images     /
            /********************************/

            TextWindowsFirewall.ForeColor = Color_Text.L_Five;
            TextWindowsDefender.ForeColor = Color_Text.L_Five;
            TextFolderPermissions.ForeColor = Color_Text.L_Five;

            /*******************************/
            /* Set Colored Buttons          /
            /*******************************/

            ButtonsColorSet(ButtonFirewallRulesAPI, 2, true);
            ButtonsColorSet(ButtonFirewallRulesCheck, 2017, false);
            ButtonsColorSet(ButtonFirewallRulesAddAll, 2017, false);
            ButtonsColorSet(ButtonFirewallRulesAddLauncher, 2017, false);
            ButtonsColorSet(ButtonFirewallRulesAddGame, 2017, false);
            ButtonsColorSet(ButtonFirewallRulesRemoveAll, 2017, false);
            ButtonsColorSet(ButtonFirewallRulesRemoveLauncher, 2017, false);
            ButtonsColorSet(ButtonFirewallRulesRemoveGame, 2017, false);
            ButtonsColorSet(ButtonDefenderExclusionAPI, 2, true);
            ButtonsColorSet(ButtonDefenderExclusionCheck, 2017, false);
            ButtonsColorSet(ButtonDefenderExclusionAddAll, 2017, false);
            ButtonsColorSet(ButtonDefenderExclusionAddLauncher, 2017, false);
            ButtonsColorSet(ButtonDefenderExclusionAddGame, 2017, false);
            ButtonsColorSet(ButtonDefenderExclusionRemoveAll, 2017, false);
            ButtonsColorSet(ButtonDefenderExclusionRemoveLauncher, 2017, false);
            ButtonsColorSet(ButtonDefenderExclusionRemoveGame, 2017, false);
            ButtonsColorSet(ButtonFolderPermissonCheck, 2, true);
            ButtonsColorSet(ButtonFolderPermissonSet, 2017, false);

            /*******************************/
            /* Set Font                     /
            /*******************************/

            /* Text */
            TextWindowsFirewall.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            TextWindowsDefender.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            TextFolderPermissions.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            /* Firewall */
            ButtonFirewallRulesAPI.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonFirewallRulesCheck.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonFirewallRulesAddAll.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonFirewallRulesAddLauncher.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonFirewallRulesAddGame.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonFirewallRulesRemoveAll.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonFirewallRulesRemoveLauncher.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonFirewallRulesRemoveGame.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            /* Defender */
            ButtonFirewallRulesAPI.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonFirewallRulesCheck.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonFirewallRulesAddAll.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonDefenderExclusionAddLauncher.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonDefenderExclusionAddGame.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonDefenderExclusionRemoveAll.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonDefenderExclusionRemoveLauncher.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonDefenderExclusionRemoveGame.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            /* File/Folder Permission */
            ButtonFolderPermissonCheck.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonFolderPermissonSet.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);

            /*******************************/
            /* Set Event Handlers           /
            /*******************************/
#if !(RELEASE_UNIX || DEBUG_UNIX)
            /* Firewall Checks */
            ButtonFirewallRulesAPI.Click += new EventHandler(ButtonFirewallRulesAPI_Click);
            ButtonFirewallRulesCheck.Click += new EventHandler(ButtonFirewallRulesCheck_Click);
            /* Firewall Add */
            ButtonFirewallRulesAddAll.Click += new EventHandler(ButtonFirewallRulesAddAll_Click);
            ButtonFirewallRulesAddLauncher.Click += new EventHandler(ButtonFirewallRulesAddLauncher_Click);
            ButtonFirewallRulesAddGame.Click += new EventHandler(ButtonFirewallRulesAddGame_Click);
            /* Firewall Remove */
            ButtonFirewallRulesRemoveAll.Click += new EventHandler(ButtonFirewallRulesRemoveAll_Click);
            ButtonFirewallRulesRemoveLauncher.Click += new EventHandler(ButtonFirewallRulesRemoveLauncher_Click);
            ButtonFirewallRulesRemoveGame.Click += new EventHandler(ButtonFirewallRulesRemoveGame_Click);
            /* Defender Checks */
            ButtonDefenderExclusionAPI.Click += new EventHandler(ButtonDefenderExclusionAPI_Click);
            ButtonDefenderExclusionCheck.Click += new EventHandler(ButtonDefenderExclusionCheck_Click);
            /* Defender Add */
            ButtonDefenderExclusionAddAll.Click += new EventHandler(ButtonDefenderExclusionAddAll_Click);
            ButtonDefenderExclusionAddLauncher.Click += new EventHandler(ButtonDefenderExclusionAddLauncher_Click);
            ButtonDefenderExclusionAddGame.Click += new EventHandler(ButtonDefenderExclusionAddGame_Click);
            /* Defender Remove */
            ButtonDefenderExclusionRemoveAll.Click += new EventHandler(ButtonDefenderExclusionRemoveAll_Click);
            ButtonDefenderExclusionRemoveLauncher.Click += new EventHandler(ButtonDefenderExclusionRemoveLauncher_Click);
            ButtonDefenderExclusionRemoveGame.Click += new EventHandler(ButtonDefenderExclusionRemoveGame_Click);
            /* Permission Checks */
            ButtonFolderPermissonCheck.Click += new EventHandler(ButtonFolderPermissonCheck_Click);
            ButtonFolderPermissonSet.Click += new EventHandler(ButtonFolderPermissonSet_Click);
#endif
            #endregion
            #region Verfy Hash
            VerifyHashWelcome.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Verify_Scan.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            DownloadProgressText.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            StartScanner.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Button_Verify_Scan.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            VerifyHashText.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);

            /********************************/
            /* Set Theme Colors              /
            /********************************/

            ForeColor = Color_Winform.Text_Fore_Color;
            BackColor = Color_Winform.BG_Fore_Color;

            DownloadProgressText.ForeColor = Color_Winform.Text_Fore_Color;
            Label_Verify_Scan.ForeColor = Color_Winform.Text_Fore_Color;

            VerifyHashWelcome.ForeColor = Color_Winform.Secondary_Text_Fore_Color;
            VerifyHashText.ForeColor = Color_Winform.Success_Text_Fore_Color;

            StartScanner.ForeColor = Color_Winform.Success_Text_Fore_Color;
            StartScanner.BackColor = Color_Winform_Buttons.Blue_Back_Color;
            StartScanner.FlatAppearance.BorderColor = Color_Winform_Buttons.Blue_Border_Color;
            StartScanner.FlatAppearance.MouseOverBackColor = Color_Winform_Buttons.Blue_Mouse_Over_Back_Color;

            Button_Verify_Scan.ForeColor = Color_Winform.Warning_Text_Fore_Color;
            Button_Verify_Scan.BackColor = Color_Winform_Buttons.Blue_Back_Color;
            Button_Verify_Scan.FlatAppearance.BorderColor = Color_Winform_Buttons.Blue_Border_Color;
            Button_Verify_Scan.FlatAppearance.MouseOverBackColor = Color_Winform_Buttons.Blue_Mouse_Over_Back_Color;

            /********************************/
            /* Events Handlers               /
            /********************************/

            StartScanner.Click += new EventHandler(StartScanner_Click);
            Button_Verify_Scan.Click += new EventHandler(StopScanner_Click);

            /********************************/
            /* Hardcoded Text [Linux Fix]    /
            /********************************/

            VerifyHashWelcome.Text = "Welcome!\n\nThe scanning process is pretty quick,\nbut may still take a while." +
                "\nDepending on your connection,\nre-downloading will take the longest\nPlease allow it to complete fully!";
            Label_Verify_Scan.Text = "Scanning Progress:";
            DownloadProgressText.Text = "Download Progress:";
            VerifyHashText.Text = "Please select \"Start Scan\" \nTo begin Validating Gamefiles";
            #endregion
        }
        #endregion
    }
}