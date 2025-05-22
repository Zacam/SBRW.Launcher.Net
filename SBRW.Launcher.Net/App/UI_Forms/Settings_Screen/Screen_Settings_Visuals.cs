using SBRW.Launcher.App.UI_Forms.Parent_Screen;
using SBRW.Launcher.Core.Extra.File_.Save_;
using SBRW.Launcher.Core.Extra.XML_;
using SBRW.Launcher.Core.Reference.Json_.Newtonsoft_;
using SBRW.Launcher.Core.Theme;
using SBRW.Launcher.RunTime.LauncherCore.APICheckers;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.Lists;
using SBRW.Launcher.RunTime.LauncherCore.Lists.JSON;
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
        private void ComboBox_Launcher_Logging_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                string Logging_Name = string.Empty;

                if (sender is ComboBox cb)
                {
                    if (e.Index != -1 && cb.Items != null)
                    {
                        if (cb.Items[e.Index] is Json_List_Launcher_Logging si)
                        {
                            Logging_Name = si.Name;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(Logging_Name) && sender != null)
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
                    e.Graphics.DrawString("    " + Logging_Name, font, textColor, e.Bounds);
                }
            }
            catch { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_Launcher_Logging_Cleanup_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                string Logging_Cleanup_Name = string.Empty;

                if (sender is ComboBox cb)
                {
                    if (e.Index != -1 && cb.Items != null)
                    {
                        if (cb.Items[e.Index] is Json_List_Launcher_Logging_Cleanup si)
                        {
                            Logging_Cleanup_Name = si.Name;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(Logging_Cleanup_Name) && sender != null)
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
                    e.Graphics.DrawString("    " + Logging_Cleanup_Name, font, textColor, e.Bounds);
                }
            }
            catch { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_Launcher_Builds_Branch_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                string Gzip_Version_Name = string.Empty;

                if (sender is ComboBox cb)
                {
                    if (e.Index != -1 && cb.Items != null)
                    {
                        if (cb.Items[e.Index] is Json_List_Launcher_Builds si)
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
        private void ComboBox_Display_Timer_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                string Gzip_Version_Name = string.Empty;

                if (sender is ComboBox cb)
                {
                    if (e.Index != -1 && cb.Items != null)
                    {
                        if (cb.Items[e.Index] is Json_List_Tile_Window_Display_Timer si)
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
            #region Form
            /*
             * Set Font
             */
#if !(RELEASE_UNIX || DEBUG_UNIX)
            float MainFontSize = 9f * 96f / CreateGraphics().DpiY;
            float SecondaryFontSize = 8f * 96f / CreateGraphics().DpiY;
            float ThirdFontSize = 10f * 96f / CreateGraphics().DpiY;
#else
            float MainFontSize = 9f;
            float SecondaryFontSize = 8f;
            float ThirdFontSize = 10f;
#endif
            Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Regular);
            Button_Save.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Button_Exit.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Button_Console_Submit.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            Input_Console.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            /*
             * Set Values
             */
            KeyPreview = true;
            New_Choosen_CDN = Save_Settings.Live_Data.Launcher_CDN;
            Text = "Settings - SBRW Launcher: " + Application.ProductVersion;
            /*
             * Buttons
             */
            ButtonsColorSet(Button_Verify_Scan, 0, false);
            ButtonsColorSet(Button_Console_Submit, 1, true);
            /*
             * Theme
             */
            Icon = FormsIcon.Retrive_Icon();
            /* Background */
            BackgroundImage = Image_Background.Settings;
            TransparencyKey = Color_Screen.BG_Settings;
            /* Main Settings Buttons (Save or Cancel) */
            Button_Save.ForeColor = Color_Text.L_Seven;
            Button_Save.Image = Image_Button.Green;
            Button_Exit.Image = Image_Button.Grey;
            Button_Exit.ForeColor = Color_Text.L_One;
            Input_Console.BackColor = Color_Winform_Other.Input;
            Input_Console.ForeColor = Color_Text.L_Five;
            /* Secondary Buttons */
            Button_Close.BackgroundImage = Button_Close.Icon_Order(SVG_Icon.Cross, SVG_Color.White);
            Picture_Logo.BackgroundImage = Image_Other.Logo;
            BackColor = Color_Winform_About.BG_Fore_Color;
            ForeColor = Color_Winform_About.Text_Fore_Color;
            /*
             * Functions
             */
            if (!CDNListUpdater.LoadedList)
            {
                CDNListUpdater.GetList();
            }
            /*
             * Events
             */
            Button_Save.MouseEnter += new EventHandler(Greenbutton_hover_MouseEnter);
            Button_Save.MouseLeave += new EventHandler(Greenbutton_MouseLeave);
            Button_Save.MouseUp += new MouseEventHandler(Greenbutton_hover_MouseUp);
            Button_Save.MouseDown += new MouseEventHandler(Greenbutton_click_MouseDown);
            Button_Save.Click += new EventHandler(SettingsSave_Click);
            Button_Save_Setup.Click += new EventHandler(SettingsSave_Click);
            Button_Exit.MouseEnter += new EventHandler(Graybutton_hover_MouseEnter);
            Button_Exit.MouseLeave += new EventHandler(Graybutton_MouseLeave);
            Button_Exit.MouseUp += new MouseEventHandler(Graybutton_hover_MouseUp);
            Button_Exit.MouseDown += new MouseEventHandler(Graybutton_click_MouseDown);
            Button_Exit.Click += new EventHandler(SettingsCancel_Click);
            Input_Console.KeyDown += new KeyEventHandler(Console_Quick_Send);
            Button_Console_Submit.Click += new EventHandler(Console_Enter);
            Button_Experiments.Click += new EventHandler(Button_Experiments_Click);
            Button_Close.MouseEnter += new EventHandler(ButtonClose_MouseEnter);
            Button_Close.MouseLeave += new EventHandler(ButtonClose_MouseLeaveANDMouseUp);
            Button_Close.MouseUp += new MouseEventHandler(ButtonClose_MouseLeaveANDMouseUp);
            Button_Close.MouseDown += new MouseEventHandler(ButtonClose_MouseDown);
            Button_Close.Click += new EventHandler(ButtonClose_Click);
            if (Screen_Parent.Screen_Instance != null)
            {
                MouseMove += new MouseEventHandler(Screen_Parent.Screen_Instance.Move_Window_Mouse_Move);
                MouseUp += new MouseEventHandler(Screen_Parent.Screen_Instance.Move_Window_Mouse_Up);
                MouseDown += new MouseEventHandler(Screen_Parent.Screen_Instance.Move_Window_Mouse_Down);
            }
            Load += new EventHandler(Screen_Settings_Load);
            Shown += (x, y) =>
            {
                RememberLastSettingsLists();
                PingSavedCDN();
                PingAPIStatus();
            };
            #endregion
            #region Parent Tab(s)
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
            TabPage_About.Text = "Version: " + Application.ProductVersion;

            if (Screen_Parent.Launcher_Setup == 1)
            {
                Button_Exit.Text = "Basic";
            }
            else
            {
                /* */
                ((Control)TabPage_Setup).Enabled = false;
            }
            #endregion
            #region Setup Tab
            /*
             * Setup Tab - Set Values
             */
            Label_Version_Setup.Text = "Version: " + Application.ProductVersion;
            Label_API_Status_List_Setup.Text = "API: United";
            LinkLabel_CDN_Current_Setup.Text = Save_Settings.Live_Data.Launcher_CDN;
            LinkLabel_Game_Path_Setup.Text = Save_Settings.Live_Data.Game_Path;
            Label_Introduction_Setup.Text = "Howdy!\n" +
                    "Looks like this is the first time this launcher has been started.\n" +
                    "Please select from the options below in order to continue this setup.";

            if (!VisualsAPIChecker.UnitedAPI())
            {
                Label_API_Status_List_Setup.Text = "API: Carbon";

                if (!VisualsAPIChecker.CarbonAPI())
                {
                    Label_API_Status_List_Setup.Text = "API: Carbon (Backup)";

                    if (!VisualsAPIChecker.CarbonAPITwo())
                    {
                        Label_API_Status_List_Setup.Text = "API: Local Cache";

                        if (!VisualsAPIChecker.Local_Cached_API())
                        {
                            Label_API_Status_List_Setup.Text = "API: Connection - Error";
                            Launcher_API_Error_Bypass = true;
                        }
                    }
                }
            }
            /*
             * Setup Tab - FONT
             */
            Label_Introduction_Setup.Font = new Font(FormsFont.Primary_Bold(), ThirdFontSize, FontStyle.Bold);
            Label_API_Status_List_Setup.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_CDN_Current_Setup.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            LinkLabel_CDN_Current_Setup.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Button_CDN_List_Setup.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            Label_CDN_List_Setup_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Game_Current_Path_Setup.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            LinkLabel_Game_Path_Setup.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Button_Change_Game_Path_Setup.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Change_Game_Path_Setup_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Version_Setup.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            /*
             * Setup Tab - Buttons
             */
            /* If the launcher has connection issues, have user use Simple Mode for bypass not Advanced (since they will give up on this mode) */
            ButtonsColorSet(Button_Save_Setup, 4, Launcher_API_Error_Bypass);
            ButtonsColorSet(Button_Change_Tabs_Setup, 0, true);
            ButtonsColorSet(Button_Change_Game_Path_Setup, Screen_Parent.Launcher_Setup.Equals(1) ? 2 : 0, true);
            ButtonsColorSet(Button_CDN_List_Setup, VisualsAPIChecker.Local_Cached_API() ? (Screen_Parent.Launcher_Setup.Equals(1) ? 2 : 0) : 4, true);
            /*
             * Setup Tab - Theme
             */
            Label_Introduction_Setup.ForeColor = Color_Winform.Secondary_Text_Fore_Color;
            Label_API_Status_List_Setup.ForeColor = Color_Text.L_Five;
            Label_Game_Current_Path_Setup.ForeColor = Color_Text.L_Five;
            Label_CDN_Current_Setup.ForeColor = Color_Text.L_Five;
            LinkLabel_CDN_Current_Setup.LinkColor = Color_Winform_Other.Link;
            LinkLabel_CDN_Current_Setup.ActiveLinkColor = Color_Winform_Other.Link_Active;
            Label_CDN_List_Setup_Details.ForeColor = Color_Text.L_Five;
            LinkLabel_Game_Path_Setup.LinkColor = Color_Winform_Other.Link_Settings;
            LinkLabel_Game_Path_Setup.ActiveLinkColor = Color_Winform_Other.Link_Settings_Active;
            Label_Change_Game_Path_Setup_Details.ForeColor = Color_Text.L_Five;
            Label_Version_Setup.ForeColor = Color_Text.L_Five;
            /* 
             * Setup Tab - Events
             */
            Button_Change_Game_Path_Setup.Click += new EventHandler(SettingsGameFiles_Click);
            Button_Change_Tabs_Setup.Click += new EventHandler(Button_Change_Tabs_Click);
            #endregion
            #region About Tab
            /* 
            * About Tab - Font
            */
            Label_Version_Build_About.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_Theme_Name.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_Theme_Author.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            /*
             * About Tab - Theme
             */
            Label_Version_Build_About.ForeColor = Color_Text.L_Five;
            Label_Theme_Name.ForeColor = Color_Text.L_Five;
            Label_Theme_Author.ForeColor = Color_Text.L_Five;
            #endregion
            #region API Tab
            /* 
             * API Tab - Font
             */
            Label_API_Status.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_API_Status_One.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_API_Status_Two.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_API_Status_Three.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_API_Status_Four.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_API_Status_Five.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            /* 
             * API Tab - Theme
             */
            Label_API_Status.ForeColor = Color_Text.L_Five;
            /* 
             * API Tab - Set Values
             */
            Label_Version_Build_About.Text = "Version: " + Application.ProductVersion;
            /*
             * API Tab - Events
             */
            Label_Version_Build_About.Click += new EventHandler(Label_Version_Build_Click);
            #endregion
            #region Downloader Tab
            /* 
             * Downloader Tab - Font
             */
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
            /* 
             * Downloader Tab - Buttons
             */
            ButtonsColorSet(Button_CDN_List, VisualsAPIChecker.Local_Cached_API() ? (Screen_Parent.Launcher_Setup.Equals(1) ? 2 : 0) : 4, true);
            /* 
             * Downloader Tab - Theme
             */
            CheckBox_Alt_WebCalls.ForeColor = Color_Winform_Other.CheckBoxes_Settings;
            Label_WebClient_Timeout.ForeColor = Color_Text.L_Five;
            NumericUpDown_WebClient_Timeout.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            NumericUpDown_WebClient_Timeout.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            Label_CDN_Current.ForeColor = Color_Text.L_Five;
            LinkLabel_CDN_Current.LinkColor = Color_Winform_Other.Link_Settings;
            LinkLabel_CDN_Current.ActiveLinkColor = Color_Winform_Other.Link_Settings_Active;
            Label_GameFiles_Downloader.ForeColor = Color_Text.L_Five;
            Radio_Button_GameFiles_Downloader_LZMA.ForeColor = Color_Winform.Text_Fore_Color;
            Radio_Button_GameFiles_Downloader_SBRW_Pack.ForeColor = Color_Winform.Text_Fore_Color;
            Radio_Button_GameFiles_Downloader_Raw.ForeColor = Color_Winform.Text_Fore_Color;
            /*
             * Downloader Tab - Events
             */
            CheckBox_Alt_WebCalls.CheckedChanged += new EventHandler(CheckBox_Alt_WebCalls_CheckedChanged);
            LinkLabel_CDN_Current.LinkClicked += new LinkLabelLinkClickedEventHandler(SettingsCDNCurrent_LinkClicked);
            Button_CDN_List.Click += new EventHandler(Button_CDN_Selector_Click);
            Button_CDN_List_Setup.Click += new EventHandler(Button_CDN_Selector_Click);
            /*
             * Downloader Tab - Set Value
             */
            LinkLabel_CDN_Current.Text = Save_Settings.Live_Data.Launcher_CDN;
            #endregion
            #region Proxy Tab
            /* 
             * Proxy Tab - Font
             */
            CheckBox_Proxy.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_Proxy_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            CheckBox_Host_to_IP.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_Host_to_IP_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            CheckBox_Proxy_Domain.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_Proxy_Domain_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Proxy_Port.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            NumericUpDown_Proxy_Port.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Proxy_Port_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Proxy_Logging.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Proxy_Logging_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Proxy_Logging_Selected_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Proxy_GZip_Version.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Proxy_GZip_Version_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Proxy_GZip_Version_Selected_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            /* 
            * Proxy Tab - Theme
            */
            CheckBox_Proxy.ForeColor = Color_Winform_Other.CheckBoxes_Settings;
            CheckBox_Host_to_IP.ForeColor = Color_Winform_Other.CheckBoxes_Settings;
            CheckBox_Proxy_Domain.ForeColor = Color_Winform_Other.CheckBoxes_Settings;
            Label_Proxy_Port.ForeColor = Color_Text.L_Five;
            NumericUpDown_Proxy_Port.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            NumericUpDown_Proxy_Port.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            /*
             * Proxy Tab - Set Values 
             */
            ComboBox_Proxy_Logging.DisplayMember = "Name";
            ComboBox_Proxy_Logging.DataSource = SettingsListUpdater.Proxy_Logging;
            ComboBox_Proxy_GZip_Version.DisplayMember = "Name";
            ComboBox_Proxy_GZip_Version.DataSource = SettingsListUpdater.Proxy_GZip_Version;
            /*
             * Proxy Tab - Events
             */
            CheckBox_Proxy.CheckedChanged += new EventHandler(CheckBox_Proxy_CheckedChanged);
            CheckBox_Host_to_IP.CheckedChanged += new EventHandler(CheckBox_Host_to_IP_CheckedChanged);
            CheckBox_Proxy_Domain.CheckedChanged += new EventHandler(CheckBox_Proxy_Domain_CheckedChanged);
            ComboBox_Proxy_Logging.DrawItem += new DrawItemEventHandler(ComboBox_Proxy_Logging_DrawItem);
            ComboBox_Proxy_Logging.SelectedIndexChanged += new EventHandler(ComboBox_Proxy_Logging_SelectedIndexChanged);
            ComboBox_Proxy_Logging.MouseWheel += new MouseEventHandler(DropDownMenu_MouseWheel);
            ComboBox_Proxy_GZip_Version.DrawItem += new DrawItemEventHandler(ComboBox_Proxy_Gzip_Version_DrawItem);
            ComboBox_Proxy_GZip_Version.SelectedIndexChanged += new EventHandler(ComboBox_Proxy_GZip_Version_SelectedIndexChanged);
            ComboBox_Proxy_GZip_Version.MouseWheel += new MouseEventHandler(DropDownMenu_MouseWheel);
            #endregion
            #region Miscellaneous Tab (Launcher Tab)
            /* 
             * Miscellaneous - Font
             */
            Label_Launcher_Path.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            LinkLabel_Launcher_Path.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            CheckBox_RPC.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_RPC_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            CheckBox_JSON_Update_Cache.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_JSON_Update_Cache_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            CheckBox_Theme_Support.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_Theme_Support_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            CheckBox_Account_Manager.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_Account_Manager_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            CheckBox_Custom_Certificate.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_Custom_Certificate_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Launcher_Builds_Branch.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Launcher_Builds_Branch_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Launcher_Builds_Branch_Selected_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Launcher_Logging.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Launcher_Logging_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Launcher_Logging_Selected_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Launcher_Logging_Cleanup.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Launcher_Logging_Cleanup_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Launcher_Logging_Cleanup_Selected_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            /*
             * Miscellaneous - Theme
             */
            Label_Launcher_Path.ForeColor = Color_Text.L_Five;
            LinkLabel_Launcher_Path.LinkColor = Color_Winform_Other.Link_Settings;
            LinkLabel_Launcher_Path.ActiveLinkColor = Color_Winform_Other.Link_Settings_Active;
            CheckBox_RPC.ForeColor = Color_Winform_Other.CheckBoxes_Settings;
            CheckBox_JSON_Update_Cache.ForeColor = Color_Winform_Other.CheckBoxes_Settings;
            CheckBox_Theme_Support.ForeColor = Color_Winform_Other.CheckBoxes_Settings;
            /*
             * Miscellaneous Tab - Set Values
             */
            LinkLabel_Launcher_Path.Text = AppDomain.CurrentDomain.BaseDirectory;
            ComboBox_Launcher_Builds_Branch.DisplayMember = "Name";
            ComboBox_Launcher_Builds_Branch.DataSource = SettingsListUpdater.Launcher_Builds;
            ComboBox_Launcher_Logging.DisplayMember = "Name";
            ComboBox_Launcher_Logging.DataSource = SettingsListUpdater.Launcher_Logging;
            ComboBox_Launcher_Logging_Cleanup.DisplayMember = "Name";
            ComboBox_Launcher_Logging_Cleanup.DataSource = SettingsListUpdater.Launcher_Logging_Cleanup;
            /*
            * Miscellaneous Tab - Events
            */
            LinkLabel_Launcher_Path.LinkClicked += new LinkLabelLinkClickedEventHandler(SettingsLauncherPathCurrent_LinkClicked);
            CheckBox_RPC.CheckedChanged += new EventHandler(CheckBox_RPC_CheckedChanged);
            CheckBox_JSON_Update_Cache.CheckedChanged += new EventHandler(CheckBox_JSON_Update_Cache_CheckedChanged);
            CheckBox_Theme_Support.CheckedChanged += new EventHandler(CheckBox_Theme_Support_CheckedChanged);
            CheckBox_Account_Manager.CheckedChanged += new EventHandler(CheckBox_Account_Manager_CheckedChanged);
            CheckBox_Custom_Certificate.CheckedChanged += new EventHandler(CheckBox_Custom_Certificate_CheckedChanged);
            ComboBox_Launcher_Builds_Branch.DrawItem += new DrawItemEventHandler(ComboBox_Launcher_Builds_Branch_DrawItem);
            ComboBox_Launcher_Builds_Branch.SelectedIndexChanged += new EventHandler(ComboBox_Launcher_Builds_Branch_SelectedIndexChanged);
            ComboBox_Launcher_Builds_Branch.MouseWheel += new MouseEventHandler(DropDownMenu_MouseWheel);
            ComboBox_Launcher_Logging.DrawItem += new DrawItemEventHandler(ComboBox_Launcher_Logging_DrawItem);
            ComboBox_Launcher_Logging.SelectedIndexChanged += new EventHandler(ComboBox_Launcher_Logging_SelectedIndexChanged);
            ComboBox_Launcher_Logging.MouseWheel += new MouseEventHandler(DropDownMenu_MouseWheel);
            ComboBox_Launcher_Logging_Cleanup.DrawItem += new DrawItemEventHandler(ComboBox_Launcher_Logging_Cleanup_DrawItem);
            ComboBox_Launcher_Logging_Cleanup.SelectedIndexChanged += new EventHandler(ComboBox_Launcher_Logging_Cleanup_SelectedIndexChanged);
            ComboBox_Launcher_Logging_Cleanup.MouseWheel += new MouseEventHandler(DropDownMenu_MouseWheel);
            #endregion
            #region General Tab (Game Tab)
            /* 
             * General Tab - Font
             */
            Label_Game_Current_Path.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            LinkLabel_Game_Path.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_Game_Files.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Button_Change_Game_Path.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Change_Game_Path_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Game_Settings.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Button_Game_User_Settings.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Game_User_Settings_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Display_Timer.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Display_Timer_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Display_Timer_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Display_Timer_Selected_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            CheckBox_InGame_Word_Filter.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_InGame_Word_Filter_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            /* 
             * General Tab - Buttons
             */
            ButtonsColorSet(Button_Change_Game_Path, Screen_Parent.Launcher_Setup.Equals(1) ? 2 : 0, true);
            ButtonsColorSet(Button_Game_User_Settings, 0, true);
            /*
             * General Tab - Theme
             */
            Label_Game_Current_Path.ForeColor = Color_Text.L_Five;
            LinkLabel_Game_Path.LinkColor = Color_Winform_Other.Link_Settings;
            LinkLabel_Game_Path.ActiveLinkColor = Color_Winform_Other.Link_Settings_Active;
            Label_Game_Settings.ForeColor = Color_Text.L_Five;
            Label_Display_Timer.ForeColor = Color_Text.L_Five;
            CheckBox_InGame_Word_Filter.ForeColor = Color_Winform_Other.CheckBoxes_Settings;
            /*
             * General Tab - Set Values
             */
            ComboBox_Display_Timer.DisplayMember = "Name";
            ComboBox_Display_Timer.DataSource = SettingsListUpdater.Title_Window_Display_Timer;
            LinkLabel_Game_Path.Text = Save_Settings.Live_Data.Game_Path;
            /*
             * General Tab - Events
             */
            LinkLabel_Game_Path.LinkClicked += new LinkLabelLinkClickedEventHandler(SettingsGameFilesCurrent_LinkClicked);
            Button_Change_Game_Path.Click += new EventHandler(SettingsGameFiles_Click);
            Button_Game_User_Settings.Click += new EventHandler(SettingsUEditorButton_Click);
            ComboBox_Display_Timer.DrawItem += new DrawItemEventHandler(ComboBox_Display_Timer_DrawItem);
            ComboBox_Display_Timer.SelectedIndexChanged += new EventHandler(ComboBox_Display_Timer_SelectedIndexChanged);
            ComboBox_Display_Timer.MouseWheel += new MouseEventHandler(DropDownMenu_MouseWheel);
            #endregion
            #region Verify Tab
            /*
             * Verify Tab - Font
             */
            Label_Verify_Scan_Details.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Verify_Scan_Progress.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Verify_Scan_Scripts_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Button_Verify_Scan.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            /*
             * Verify Tab - Theme
             */
            Label_Verify_Scan_Progress.ForeColor = Color_Winform.Text_Fore_Color;
            Label_Verify_Scan_Details.ForeColor = Color_Winform.Secondary_Text_Fore_Color;
            Button_Verify_Scan.ForeColor = Color_Winform.Success_Text_Fore_Color;
            Button_Verify_Scan.BackColor = Color_Winform_Buttons.Blue_Back_Color;
            Button_Verify_Scan.FlatAppearance.BorderColor = Color_Winform_Buttons.Blue_Border_Color;
            Button_Verify_Scan.FlatAppearance.MouseOverBackColor = Color_Winform_Buttons.Blue_Mouse_Over_Back_Color;
            Button_Verify_Scan.ForeColor = Color_Winform.Warning_Text_Fore_Color;
            Button_Verify_Scan.BackColor = Color_Winform_Buttons.Blue_Back_Color;
            Button_Verify_Scan.FlatAppearance.BorderColor = Color_Winform_Buttons.Blue_Border_Color;
            Button_Verify_Scan.FlatAppearance.MouseOverBackColor = Color_Winform_Buttons.Blue_Mouse_Over_Back_Color;
            GroupBox_Verify_Scan.ForeColor = Color_Winform.Text_Fore_Color;
            TextBox_Verify_Scan.ForeColor = Color_Winform.Secondary_Text_Fore_Color;
            TextBox_Verify_Scan.BackColor = Color_Winform.BG_Darker_Fore_Color;
            /*
             * Verify Tab - Buttons
             */
            if (FunctionStatus.IsVerifyHashDisabled)
            {
                ButtonsColorSet(Button_Verify_Scan, 3, true);
            }
            /*
             * Verify Tab - Events
             */
            Button_Verify_Scan.Click += new EventHandler(Button_Verify_Scan_Click);
            CheckBox_Verify_Scan_Scripts.CheckedChanged += new EventHandler(CheckBox_Verify_Scan_Scripts_CheckedChanged);
            /*
             * Verify Tab - Set Value
             */
            /* Hardcoded Text [Linux Fix], Maybe it can be fixed with translations down the line- DavidCarbon */
            TextBox_Verify_Scan.AppendText(
                $"Welcome!{Environment.NewLine}The scanning process is pretty quick, but may still take a while." +
                $"{Environment.NewLine}Depending on your connection, re-downloading will take the longest. " +
                $"{Environment.NewLine}Please allow it to complete fully!");
            Label_Verify_Scan_Progress.Text = "Scanning Progress:";
            #endregion
            #region Firewall Tab
            /*
             * Firewall Tab - Font
             */
            TextWindowsFirewall.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            ButtonFirewallRulesAPI.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonFirewallRulesCheck.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonFirewallRulesAddAll.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonFirewallRulesAddLauncher.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonFirewallRulesAddGame.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonFirewallRulesRemoveAll.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonFirewallRulesRemoveLauncher.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonFirewallRulesRemoveGame.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            /*
             * Firewall Tab - Buttons
             */
            ButtonsColorSet(ButtonFirewallRulesAPI, 2, true);
            ButtonsColorSet(ButtonFirewallRulesCheck, 2017, false);
            ButtonsColorSet(ButtonFirewallRulesAddAll, 2017, false);
            ButtonsColorSet(ButtonFirewallRulesAddLauncher, 2017, false);
            ButtonsColorSet(ButtonFirewallRulesAddGame, 2017, false);
            ButtonsColorSet(ButtonFirewallRulesRemoveAll, 2017, false);
            ButtonsColorSet(ButtonFirewallRulesRemoveLauncher, 2017, false);
            ButtonsColorSet(ButtonFirewallRulesRemoveGame, 2017, false);
            /*
             * Firewall Tab - Theme
             */
            TextWindowsFirewall.ForeColor = Color_Text.L_Five;
            /*
             * Firewall Tab - Events
             */
#if !(RELEASE_UNIX || DEBUG_UNIX)
            ButtonFirewallRulesAPI.Click += new EventHandler(ButtonFirewallRulesAPI_Click);
            ButtonFirewallRulesCheck.Click += new EventHandler(ButtonFirewallRulesCheck_Click);
            ButtonFirewallRulesAddAll.Click += new EventHandler(ButtonFirewallRulesAddAll_Click);
            ButtonFirewallRulesAddLauncher.Click += new EventHandler(ButtonFirewallRulesAddLauncher_Click);
            ButtonFirewallRulesAddGame.Click += new EventHandler(ButtonFirewallRulesAddGame_Click);
            ButtonFirewallRulesRemoveAll.Click += new EventHandler(ButtonFirewallRulesRemoveAll_Click);
            ButtonFirewallRulesRemoveLauncher.Click += new EventHandler(ButtonFirewallRulesRemoveLauncher_Click);
            ButtonFirewallRulesRemoveGame.Click += new EventHandler(ButtonFirewallRulesRemoveGame_Click);
#endif
            #endregion
            #region Defender Tab
            /*
             * Defender Tab - Font
             */
            TextWindowsDefender.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            ButtonDefenderExclusionAPI.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonDefenderExclusionCheck.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonDefenderExclusionAddAll.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonDefenderExclusionAddLauncher.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonDefenderExclusionAddGame.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonDefenderExclusionRemoveAll.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonDefenderExclusionRemoveLauncher.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonDefenderExclusionRemoveGame.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            /*
             * Defender Tab - Buttons
             */
            ButtonsColorSet(ButtonDefenderExclusionAPI, 2, true);
            ButtonsColorSet(ButtonDefenderExclusionCheck, 2017, false);
            ButtonsColorSet(ButtonDefenderExclusionAddAll, 2017, false);
            ButtonsColorSet(ButtonDefenderExclusionAddLauncher, 2017, false);
            ButtonsColorSet(ButtonDefenderExclusionAddGame, 2017, false);
            ButtonsColorSet(ButtonDefenderExclusionRemoveAll, 2017, false);
            ButtonsColorSet(ButtonDefenderExclusionRemoveLauncher, 2017, false);
            ButtonsColorSet(ButtonDefenderExclusionRemoveGame, 2017, false);
            /*
             * Defender Tab - Theme
             */
            TextWindowsDefender.ForeColor = Color_Text.L_Five;
            /*
             * Defender Tab - Events
             */
#if !(RELEASE_UNIX || DEBUG_UNIX)
            ButtonDefenderExclusionAPI.Click += new EventHandler(ButtonDefenderExclusionAPI_Click);
            ButtonDefenderExclusionCheck.Click += new EventHandler(ButtonDefenderExclusionCheck_Click);
            ButtonDefenderExclusionAddAll.Click += new EventHandler(ButtonDefenderExclusionAddAll_Click);
            ButtonDefenderExclusionAddLauncher.Click += new EventHandler(ButtonDefenderExclusionAddLauncher_Click);
            ButtonDefenderExclusionAddGame.Click += new EventHandler(ButtonDefenderExclusionAddGame_Click);
            ButtonDefenderExclusionRemoveAll.Click += new EventHandler(ButtonDefenderExclusionRemoveAll_Click);
            ButtonDefenderExclusionRemoveLauncher.Click += new EventHandler(ButtonDefenderExclusionRemoveLauncher_Click);
            ButtonDefenderExclusionRemoveGame.Click += new EventHandler(ButtonDefenderExclusionRemoveGame_Click);
#endif
            #endregion
            #region Permissions Tab
            /*
             * Permissions Tab - Font
             */
            TextFolderPermissions.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            ButtonFolderPermissonCheck.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ButtonFolderPermissonSet.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            /*
             * Permissions Tab - Buttons
             */
            ButtonsColorSet(ButtonFolderPermissonCheck, 2, true);
            ButtonsColorSet(ButtonFolderPermissonSet, 2017, false);
            /*
             * Permissions Tab - Theme
             */
            TextFolderPermissions.ForeColor = Color_Text.L_Five;
            /*
             * Permissions Tab - Events
             */
#if !(RELEASE_UNIX || DEBUG_UNIX)
            ButtonFolderPermissonCheck.Click += new EventHandler(ButtonFolderPermissonCheck_Click);
            ButtonFolderPermissonSet.Click += new EventHandler(ButtonFolderPermissonSet_Click);
#endif
            #endregion
            #region Miscellaneous Tab (Game Tab)
            /*
             * Miscellaneous Tab - Font
             */
            CheckBox_Enable_Affinity_Range.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_Enable_Affinity_Range_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Label_Affinity_Core_Calculator.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Affinity_Core_Range.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            NumericUpDown_Range_Affinity.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Affinity_Core_Range_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Button_Clear_Crash_Logs.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            Label_Clear_Crash_Logs_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Button_Clear_NFSWO_Logs.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            Label_Clear_NFSWO_Logs_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            Button_Clear_Server_Mods.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            Label_Clear_Server_Mods_Details.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Italic);
            /*
             * Miscellaneous Tab - Buttons
             */
            ButtonsColorSet(Button_Clear_Crash_Logs, 0, false);
            ButtonsColorSet(Button_Clear_NFSWO_Logs, 0, false);
            ButtonsColorSet(Button_Clear_Server_Mods, 0, false);
            /*
             * Miscellaneous Tab - Events
             */
            CheckBox_Enable_Affinity_Range.CheckedChanged += new EventHandler(CheckBox_Enable_Affinity_Range_CheckedChanged);
            NumericUpDown_Range_Affinity.ValueChanged += new EventHandler(NumericUpDown_Range_Affinity_ValueChanged);
            NumericUpDown_Range_Affinity.MouseWheel += new MouseEventHandler(NumericUpDown_Range_Affinity_MouseWheel);
            Button_Clear_Crash_Logs.Click += new EventHandler(SettingsClearCrashLogsButton_Click);
            Button_Clear_NFSWO_Logs.Click += new EventHandler(SettingsClearCommunicationLogButton_Click);
            Button_Clear_Server_Mods.Click += new EventHandler(SettingsClearServerModCacheButton_Click);
            /*
             * Miscellaneous Tab - Call Functions
             */
            Enable_Affinity_Range(Save_Settings.Game_Affinity_Range_Mode());
            #endregion
        }
        #endregion
    }
}