﻿using SBRW.Launcher.App.UI_Forms.About_Screen;
using SBRW.Launcher.App.UI_Forms.Main_Screen;
using SBRW.Launcher.App.UI_Forms.Parent_Screen;
using SBRW.Launcher.App.UI_Forms.Selection_CDN_Screen;
using SBRW.Launcher.App.UI_Forms.User_Settings_Editor_Screen;
using SBRW.Launcher.App.UI_Forms.VerifyHash_Screen;
using SBRW.Launcher.Core.Cache;
using SBRW.Launcher.Core.Extension.Api_;
using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.Core.Extension.String_;
using SBRW.Launcher.Core.Extra.File_.Save_;
using SBRW.Launcher.Core.Required.System.Windows_;
using SBRW.Launcher.RunTime.InsiderKit;
using SBRW.Launcher.RunTime.LauncherCore.APICheckers;
using SBRW.Launcher.RunTime.LauncherCore.Downloader;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.Lists;
using SBRW.Launcher.RunTime.LauncherCore.Lists.JSON;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using SBRW.Launcher.RunTime.LauncherCore.ModNet;
using SBRW.Launcher.RunTime.LauncherCore.Support;
using SBRW.Launcher.RunTime.LauncherCore.Visuals;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SBRW.Launcher.App.UI_Forms.Settings_Screen
{
    partial class Screen_Settings
    {
        #region Event Functions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Oem3)
            {
                // Handle key at form level.
                // Do not send event to focused control by returning true.

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                Label_Version_Build_Click(default, default);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_Version_Build_Click(object sender, EventArgs e)
        {
            if (!this.Disposing || !this.IsDisposed)
            {
                if (!Button_Console_Submit.Visible)
                {
                    Button_Console_Submit.Visible = Input_Console.Visible = true;
                }
                else
                {
                    Button_Console_Submit.Visible = Input_Console.Visible = false;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_CDN_Selector_Click(object sender, EventArgs e)
        {
            if (VisualsAPIChecker.Local_Cached_API())
            {
                Screen_CDN_Selection.OpenScreen(2);
            }
            else
            {
                ButtonsColorSet(Button_CDN_List, 4, true);
                if (Screen_Parent.Launcher_Setup.Equals(1))
                {
                    ButtonsColorSet(Button_CDN_List_Setup, 4, true);
                }
                "Launcher failed to reach any APIs. CDN Selection Screen is not available.".Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Verify_Scan_Click(object sender, EventArgs e)
        {
            if (Download_Raw.Thread != default)
            {
                Button_Verify_Scan.Text = "Stop Scan";
                /* Downloader is Running */
                Download_Raw.Stop();
            }
            else
            {
                Button_Verify_Scan.Text = "Start Scan";
                /* Downloader is Running */
                Download_Raw.Verify_CDN_URL = Save_Settings.Live_Data.Launcher_CDN;
                Download_Raw.Start();
            }
        }
        /// <summary>
        /// Settings Verify Hash
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Game_Verify_Files_Click(object sender, EventArgs e)
        {
            if (FunctionStatus.IsVerifyHashDisabled)
            {
                ButtonsColorSet(Button_Verify_Scan, 3, true);
                if (!File.Exists(Path.Combine(Save_Settings.Live_Data.Game_Path, "nfsw.exe")))
                {
                    ("You need to Download the Game Files first " +
                        "before you can have access to run Verify Hash").Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    ("You have already done a 'Verify GameFiles' Scan" +
                    "\nPlease Restart Launcher to do a new Verify GameFiles Scan").Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (!FunctionStatus.DoesCDNSupportVerifyHash)
            {
                ButtonsColorSet(Button_Verify_Scan, 3, true);
                ("The current saved CDN does not support 'Verify GameFiles' Scan" +
                    "\nPlease Choose Another CDN from the list").Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ButtonsColorSet(Button_Verify_Scan, (Save_Settings.Live_Data.Game_Integrity != "Good") ? 2 : 0, true);
                Screen_Verify_Hash.OpenScreen();
            }
        }
        #region Draw and Regular Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DropDownMenu_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
        #endregion
        #region Settings Save
        /// <summary>
        /// Settings Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SettingsSave_Click(object sender, EventArgs e)
        {
            bool Stop_and_Restart_Downloader = false;
            bool CDN_Changed_Button_Update = false;

            if (TabControl_Shared_Hub.SelectedTab == TabPage_Settings)
            {
                Button_Save.Text = "SAVING";
            }

            if (!string.IsNullOrWhiteSpace(NewGameFilesPath))
            {
#if !(RELEASE_UNIX || DEBUG_UNIX)
                if (Product_Version.GetWindowsNumber() >= 10.0 && (Save_Settings.Live_Data.Game_Path != NewGameFilesPath))
                {
                    WindowsDefenderGameFilesDirctoryChange();
                }
                else
#endif
                if (Save_Settings.Live_Data.Game_Path != NewGameFilesPath)
                {
#if !(RELEASE_UNIX || DEBUG_UNIX)
                    /* Check if New Game! Files is not in Banned Folder Locations */
                    CheckGameFilesDirectoryPrevention();
                    /* Store Old Location for Security Panel to Use Later on */
                    Save_Settings.Live_Data.Game_Path_Old = Save_Settings.Live_Data.Game_Path;
                    Save_Settings.Live_Data.Firewall_Game = "Not Excluded";
#endif

                    Save_Settings.Live_Data.Game_Path = NewGameFilesPath;

                    /* Clean Mods Files from New Dirctory (If it has .links in directory) */
                    if (File.Exists(Path.Combine(NewGameFilesPath, Locations.NameModLinks)))
                    {
                        ModNetHandler.CleanLinks(NewGameFilesPath);
                        Log.Completed("CLEANLINKS: Done");
                    }

                    ButtonsColorSet(Button_Change_Game_Path, 1, true);
                    if (Screen_Parent.Launcher_Setup.Equals(1))
                    {
                        ButtonsColorSet(Button_Change_Game_Path_Setup, 1, true);
                        Setup_Save_Button_Check();
                    }
                    RestartRequired = true;
                }
            }

            if (Save_Settings.Live_Data.Launcher_CDN != New_Choosen_CDN)
            {
                Save_Settings.Live_Data.Launcher_CDN = New_Choosen_CDN;
                Label_CDN_Current.Text = "CHANGED CDN:";
                CDN_Changed_Button_Update = Stop_and_Restart_Downloader = RestartRequired = true;
                ButtonsColorSet(Button_Verify_Scan, 0, false);
            }

            if (TabControl_Shared_Hub.SelectedTab == TabPage_Settings)
            {
                if (Save_Settings.Live_Data.Launcher_Proxy != (CheckBox_Proxy.Checked ? "0" : "1"))
                {
                    Save_Settings.Live_Data.Launcher_Proxy = CheckBox_Proxy.Checked ? "0" : "1";

                    if (Save_Settings.Live_Data.Launcher_Proxy == "1" && InformationCache.SelectedServerEnforceProxy)
                    {
                        (ServerListUpdater.ServerName("Settings") + " requires Proxy to be Enabled.\nThe launcher will turn on Proxy, " +
                        "even if you have chosen to Disable it").Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                if (Save_Settings.Live_Data.Launcher_Proxy_Port != NumericUpDown_Proxy_Port.Value.ToStringInvariant())
                {
                    Save_Settings.Live_Data.Launcher_Proxy_Port = NumericUpDown_Proxy_Port.Value.ToStringInvariant();
                }

                if (Save_Settings.Live_Data.Launcher_Legacy_Host_To_IP != (CheckBox_Host_to_IP.Checked ? "1" : "0"))
                {
                    Save_Settings.Live_Data.Launcher_Legacy_Host_To_IP = CheckBox_Host_to_IP.Checked ? "1" : "0";
                }

                if (Save_Settings.Live_Data.Launcher_Proxy_Domain != (CheckBox_Proxy_Domain.Checked ? "1" : "0"))
                {
                    Save_Settings.Live_Data.Launcher_Proxy_Domain = CheckBox_Proxy_Domain.Checked ? "1" : "0";
                }

                if (Save_Settings.Live_Data.Launcher_Discord_Presence != (CheckBox_RPC.Checked ? "0" : "1"))
                {
                    Save_Settings.Live_Data.Launcher_Discord_Presence = CheckBox_RPC.Checked ? "0" : "1";
                }

                if (Save_Settings.Live_Data.Launcher_Theme_Support != (CheckBox_Theme_Support.Checked ? "1" : "0"))
                {
                    Save_Settings.Live_Data.Launcher_Theme_Support = CheckBox_Theme_Support.Checked ? "1" : "0";
                    RestartRequired = true;
                }

                if (Save_Settings.Live_Data.Launcher_WebClient_Method != (CheckBox_Alt_WebCalls.Checked ? "WebClientWithTimeout" : "WebClient"))
                {
                    Save_Settings.Live_Data.Launcher_WebClient_Method = CheckBox_Alt_WebCalls.Checked ? "WebClientWithTimeout" : "WebClient";
                    Launcher_Value.Launcher_Alternative_Webcalls(Save_Settings.Live_Data.Launcher_WebClient_Method == "WebClient");
                }

                if (Save_Settings.Live_Data.Launcher_Display_Timer != Display_Timer_Button_Selection())
                {
                    Save_Settings.Live_Data.Launcher_Display_Timer = Display_Timer_Button_Selection();
                }

                if (Save_Settings.Live_Data.Launcher_WebCall_TimeOut_Time != NumericUpDown_WebClient_Timeout.Value.ToString())
                {
                    Save_Settings.Live_Data.Launcher_WebCall_TimeOut_Time = NumericUpDown_WebClient_Timeout.Value.ToString();

                    if (NumericUpDown_WebClient_Timeout.Value > 0)
                    {
                        Launcher_Value.Launcher_WebCall_Timeout_Enable = true;
                    }
                    else
                    {
                        Launcher_Value.Launcher_WebCall_Timeout_Enable = false;
                    }
                }

                if (Save_Settings.Live_Data.Launcher_Game_Downloader != GameDownloaderButtonSelection())
                {
                    Save_Settings.Live_Data.Launcher_Game_Downloader = GameDownloaderButtonSelection();
                    Stop_and_Restart_Downloader = true;
                }

                if (Save_Settings.Live_Data.Launcher_JSON_Frequency_Update_Cache != (CheckBox_JSON_Update_Cache.Checked ? "1" : "0"))
                {
                    Save_Settings.Live_Data.Launcher_JSON_Frequency_Update_Cache = CheckBox_JSON_Update_Cache.Checked ? "1" : "0";
                }

                if (Save_Settings.Live_Data.Launcher_Certificate_Mode != (CheckBox_Custom_Certificate.Checked ? "1" : "0"))
                {
                    Save_Settings.Live_Data.Launcher_Certificate_Mode = CheckBox_Custom_Certificate.Checked ? "1" : "0";
                }

                if (Save_Settings.Live_Data.Launcher_Account_Manager != (CheckBox_Account_Manager.Checked ? "1" : "0"))
                {
                    Save_Settings.Live_Data.Launcher_Account_Manager = CheckBox_Account_Manager.Checked ? "1" : "0";

                    if (!Screen_Main.Screen_Instance.DisposedForm())
                    {
                        /* Account Manager Controls */
                        Screen_Main.Screen_Instance.ComboBox_Accounts.Visible = CheckBox_Account_Manager.Checked;
                        Screen_Main.Screen_Instance.Button_Account_Manager.Visible = CheckBox_Account_Manager.Checked;
                        /* Regular Login Controls */
                        Screen_Main.Screen_Instance.Input_Email.Visible = !CheckBox_Account_Manager.Checked;
                        Screen_Main.Screen_Instance.Input_Password.Visible = !CheckBox_Account_Manager.Checked;
                        Screen_Main.Screen_Instance.Picture_Input_Email.Visible = !CheckBox_Account_Manager.Checked;
                        Screen_Main.Screen_Instance.Picture_Input_Password.Visible = !CheckBox_Account_Manager.Checked;
                    }
                }

                /* Proxy Logging */
                if (ComboBox_Proxy_Logging.SelectedItem != default)
                {
                    if (Save_Settings.Proxy_Log_Mode() != ((Json_List_Proxy_Logging)ComboBox_Proxy_Logging.SelectedItem).Mode)
                    {
                        Save_Settings.Live_Data.Launcher_Proxy_Log_Mode =
                            ((int)((Json_List_Proxy_Logging)ComboBox_Proxy_Logging.SelectedItem).Mode).ToString();
                    }
                }
                /* Proxy GZip Version */
                if (ComboBox_Proxy_GZip_Version.SelectedItem != default)
                {
                    if (Save_Settings.Proxy_GZip_Version() != ((Json_List_Proxy_GZip_Version)ComboBox_Proxy_GZip_Version.SelectedItem).Version)
                    {
                        Save_Settings.Live_Data.Launcher_Proxy_GZip_Version =
                            ((int)((Json_List_Proxy_GZip_Version)ComboBox_Proxy_GZip_Version.SelectedItem).Version).ToString();
                    }
                }
                /* Launcher Logging */
                if (ComboBox_Launcher_Logging.SelectedItem != default)
                {
                    if (Save_Settings.Log_Mode() != ((Json_List_Launcher_Logging)ComboBox_Launcher_Logging.SelectedItem).Mode)
                    {
                        Save_Settings.Live_Data.Launcher_Log_Mode =
                            ((int)((Json_List_Launcher_Logging)ComboBox_Launcher_Logging.SelectedItem).Mode).ToString();
                    }
                }
                /* Launcher Builds */
                if (ComboBox_Launcher_Builds_Branch.SelectedItem != default)
                {
                    if (Save_Settings.Preview_Mode_Int() != ((Json_List_Launcher_Builds)ComboBox_Launcher_Builds_Branch.SelectedItem).Value)
                    {
                        Save_Settings.Live_Data.Launcher_Insider =
                            ((Json_List_Launcher_Builds)ComboBox_Launcher_Builds_Branch.SelectedItem).Value.ToString();
                        RestartRequired = true;
                    }
                }

                try
                {
                    /* Actually lets check those 2 files */
                    if (File.Exists(Save_Settings.Live_Data.Game_Path + "/profwords") && File.Exists(Save_Settings.Live_Data.Game_Path + "/profwords_dis"))
                    {
                        File.Delete(Save_Settings.Live_Data.Game_Path + "/profwords_dis");
                    }

                    /* Delete/Enable profwords filter here */
                    if (CheckBox_Word_Filter_Check.Checked)
                    {
                        if (File.Exists(Save_Settings.Live_Data.Game_Path + "/profwords"))
                        {
                            File.Move(Save_Settings.Live_Data.Game_Path + "/profwords", Save_Settings.Live_Data.Game_Path + "/profwords_dis");
                        }
                    }
                    else
                    {
                        if (File.Exists(Save_Settings.Live_Data.Game_Path + "/profwords_dis"))
                        {
                            File.Move(Save_Settings.Live_Data.Game_Path + "/profwords_dis", Save_Settings.Live_Data.Game_Path + "/profwords");
                        }
                    }
                }
                catch (Exception Error)
                {
                    LogToFileAddons.OpenLog("SETTINGS SAVE [Profwords]", string.Empty, Error, string.Empty, true);
                }
            }

            /* Save Settings */
            Save_Settings.Save();

            if (TabControl_Shared_Hub.SelectedTab == TabPage_Settings)
            {
                Button_Save.Text = "SAVED";
            }

            if (RestartRequired)
            {
                "In order to see settings changes, you need to restart the Launcher manually.".Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (Stop_and_Restart_Downloader && Screen_Parent.Launcher_Setup == 0)
            {
                try
                {
                    if (Screen_Main.Screen_Instance != null)
                    {
                        Screen_Main.Screen_Instance.Game_Folder_Checks();
                    }
                }
                catch (Exception Error)
                {
                    LogToFileAddons.OpenLog("SETTINGS to Main Screen Instance", string.Empty, Error, string.Empty, true);
                }

                if (CDN_Changed_Button_Update)
                {
                    await Task.Run(() =>
                    {
                        if (Screen_Instance != null)
                        {
                            switch (API_Core.StatusCheck(Save_Settings.Live_Data.Launcher_CDN + "/unpacked/checksums.dat", 10))
                            {
                                case APIStatus.Online:
                                    if (Screen_Instance != null)
                                    {
                                        FunctionStatus.DoesCDNSupportVerifyHash = true;
                                        ButtonsColorSet(Button_Verify_Scan, (Save_Settings.Live_Data.Game_Integrity != "Good" ? 2 : 0), true);
                                    }
                                    break;
                                default:
                                    if (Screen_Instance != null)
                                    {
                                        FunctionStatus.DoesCDNSupportVerifyHash = false;
                                        ButtonsColorSet(Button_Verify_Scan, 3, true);
                                    }
                                    break;
                            }
                        }
                    });
                }
            }

            if (Screen_Parent.Launcher_Setup == 1)
            {
                Screen_Parent.Launcher_Setup = 0;
                Close();
            }
            else
            {
                Screen_Parent.Launcher_Setup = 0;
            }
        }
        /// <summary>
        /// DEBUG FEATURES GOES HERE!!!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Experiments_Click(object sender, EventArgs e)
        {
            
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericUpDown_Range_Affinity_ValueChanged(object sender, EventArgs e)
        {
            if (CheckBox_Enable_Affinity_Range.Checked)
            {
                int Max_Processor_Count = Environment.ProcessorCount - 1;
                int New_Start = 0;
                int New_End = 0;

                if ((int)NumericUpDown_Range_Affinity.Value >= Max_Processor_Count)
                {
                    // Ensure we don't go above the max core count
                    New_Start = (int)Math.Max(0, NumericUpDown_Range_Affinity.Value - 3);
                    New_End = Max_Processor_Count;
                }
                else if ((Max_Processor_Count - (int)NumericUpDown_Range_Affinity.Value) >= 0 &&
                    (Max_Processor_Count - (int)NumericUpDown_Range_Affinity.Value) <= 3)
                {
                    New_Start = Max_Processor_Count - 3;
                    New_End = Max_Processor_Count;
                }
                else
                {
                    // Normal add mode range
                    New_Start = (int)NumericUpDown_Range_Affinity.Value;
                    New_End = (int)Math.Min(Max_Processor_Count, NumericUpDown_Range_Affinity.Value + 3);
                }

                Screen_Main.Game_Affinity_Range = new int[] { New_Start, New_End };
                Label_Affinity_Core_Range.Text = $"Range: {Screen_Main.Game_Affinity_Range[0]}-{Screen_Main.Game_Affinity_Range[1]}";
            }
            else
            {
                Screen_Main.Game_Affinity_Range = new int[] { 0, BuildDevelopment.Allowed() ? (int)NumericUpDown_Range_Affinity.Value : 4 };
                Label_Affinity_Core_Range.Text = $"{(int)NumericUpDown_Range_Affinity.Value} Cores";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_RPC_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox_RPC.Text = $"Discord RPC {(CheckBox_RPC.Checked ? "(Enabled)" : "(Disabled)")}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_JSON_Update_Cache_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox_JSON_Update_Cache.Text = $"JSON Cache {(CheckBox_JSON_Update_Cache.Checked ? "(Enabled)" : "(Disabled)")}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Theme_Support_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox_Theme_Support.Text = $"Custom Theme {(CheckBox_Theme_Support.Checked ? "(Enabled)" : "(Disabled)")}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Account_Manager_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox_Account_Manager.Text = $"Account_Manager {(CheckBox_Account_Manager.Checked ? "(Enabled)" : "(Disabled)")}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Alt_WebCalls_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox_Alt_WebCalls.Text = $"Alternative WebCalls {(CheckBox_Alt_WebCalls.Checked ? "(Enabled)" : "(Disabled)")}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Proxy_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox_Proxy.Text = $"Proxy {(CheckBox_Proxy.Checked ? "(Enabled)" : "(Disabled)")}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Host_to_IP_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox_Host_to_IP.Text = $"IP Converter {(CheckBox_Host_to_IP.Checked ? "(Enabled)" : "(Disabled)")}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Proxy_Domain_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox_Proxy_Domain.Text = $"Proxy Domain {(CheckBox_Proxy_Domain.Checked ? "(Enabled)" : "(Disabled)")}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Custom_Certificate_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox_Custom_Certificate.Text = $"Custom Certificate {(CheckBox_Custom_Certificate.Checked ? "(Enabled)" : "(Disabled)")}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Enable_Affinity_Range_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox_Enable_Affinity_Range.Text = $"Affinity Range {(CheckBox_Enable_Affinity_Range.Checked ? "(Enabled)" :"(Disabled)")}";
            Enable_Affinity_Range(CheckBox_Enable_Affinity_Range.Checked);
        }
        /// <summary>
        /// Settings Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsCancel_Click(object sender, EventArgs e)
        {
            if (Screen_Parent.Launcher_Setup.Equals(1))
            {
                TabControl_Shared_Hub.SelectedTab = TabPage_Setup;
            }
            else
            {
                Close();
            }
        }
        /// <summary>
        /// Settings UserSettings XML Editor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsUEditorButton_Click(object sender, EventArgs e)
        {
            try
            {
                Screen_User_Settings_Editor Custom_Instance_Settings = new Screen_User_Settings_Editor() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true, FormBorderStyle = FormBorderStyle.None };
                Panel_Form_Screens.Controls.Add(Custom_Instance_Settings);
                Panel_Form_Screens.Visible = true;
                Custom_Instance_Settings.Show();
                Text = "Game Settings Editor - SBRW Launcher: " + Application.ProductVersion;
            }
            catch (Exception Error)
            {
                string ErrorMessage = "Game Settings Editor Screen Encountered an Error";
                LogToFileAddons.OpenLog("GAME SETTINGS SCREEN", ErrorMessage, Error, "Exclamation", false);
            }
        }
        /// <summary>
        /// Settings Clear ModNet Cache
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsClearServerModCacheButton_Click(object sender, EventArgs e)
        {
            DialogResult SettingsClearServerModCacheConfirmation = 
                ("Warning: you are about the Delete Server Mods Cache" +
            "\nBy Deleting the Cache, you will have to re-download the Server Mods Again." +
            "\n\nClick Yes to Delete Mods Cache \nor \nClick No to Keep Mods Cache").Message_Box(MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (SettingsClearServerModCacheConfirmation == DialogResult.Yes)
            {
                try
                {
                    if (Directory.Exists(Save_Settings.Live_Data.Game_Path + "/.data"))
                    {
                        Directory.Delete(Save_Settings.Live_Data.Game_Path + "/.data", true);
                    }
                    if (Directory.Exists(Save_Settings.Live_Data.Game_Path + "/MODS"))
                    {
                        Directory.Delete(Save_Settings.Live_Data.Game_Path + "/MODS", true);
                    }
                    Log.Warning("LAUNCHER: User Confirmed to Delete Server Mods Cache");
                    ButtonsColorSet(Button_Clear_Server_Mods, 1, false);
                    "Deleted Server Mods Cache".Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception Error)
                {
                    ButtonsColorSet(Button_Clear_Server_Mods, 3, true);
                    LogToFileAddons.OpenLog("SETTINGS CLEAR", "Unable to Delete Server Mods Cache", Error, "Exclamation", false);
                }
            }
        }
        /// <summary>
        /// Settings Clear Communication Logs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsClearCommunicationLogButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Save_Settings.Live_Data.Game_Path + "/NFSWO_COMMUNICATION_LOG.txt"))
                {
                    File.Delete(Save_Settings.Live_Data.Game_Path + "/NFSWO_COMMUNICATION_LOG.txt");
                }
                ButtonsColorSet(Button_Clear_NFSWO_Logs, 1, false);
                "Deleted NFSWO Communication Log".Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception Error)
            {
                ButtonsColorSet(Button_Clear_NFSWO_Logs, 3, true);
                LogToFileAddons.OpenLog("SETTINGS CLEAR", "Unable to Delete NFSWO Communication Log", Error, "Exclamation", false);
            }
        }
        /// <summary>
        /// Settings Clear Game Crash Logs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsClearCrashLogsButton_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo CrashLogFilesDirectory = new DirectoryInfo(Save_Settings.Live_Data.Game_Path);

                foreach (FileInfo LocatedFile in CrashLogFilesDirectory.EnumerateFiles("SBRCrashDump_CL0*.dmp", SearchOption.TopDirectoryOnly))
                {
                    LocatedFile.Delete();
                }

                foreach (FileInfo LocatedFile in CrashLogFilesDirectory.EnumerateFiles("SBRCrashDump_CL0*.txt", SearchOption.TopDirectoryOnly))
                {
                    LocatedFile.Delete();
                }

                foreach (FileInfo LocatedFile in CrashLogFilesDirectory.EnumerateFiles("NFSCrashDump_CL0*.dmp", SearchOption.TopDirectoryOnly))
                {
                    LocatedFile.Delete();
                }

                ButtonsColorSet(Button_Clear_Crash_Logs, 1, false);
                "Deleted Crash Logs".Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception Error)
            {
                ButtonsColorSet(Button_Clear_Crash_Logs, 3, true);
                LogToFileAddons.OpenLog("SETTINGS CLEAR", "Unable to Delete Crash Logs", Error, "Exclamation", false);
            }
        }
        /// <summary>
        /// Switch Tabs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Change_Tabs_Click(object sender, EventArgs e)
        {
            TabControl_Shared_Hub.SelectedTab = TabPage_Settings;
        }
        /// <summary>
        /// Settings Clear Old Launcher Logs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsClearLauncherLogsButton_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo InstallationDirectory = new DirectoryInfo(Log_Location.LogFolder);

                foreach (DirectoryInfo Folder in InstallationDirectory.EnumerateDirectories())
                {
                    if (Directory.Exists(Folder.FullName))
                    {
                        if (Folder.FullName != Log_Location.LogCurrentFolder)
                        {
                            Directory.Delete(Folder.FullName, true);
                        }
                    }
                }

                ButtonsColorSet(Button_Launcher_logs, 1, false);
                "Deleted Old Launcher Logs".Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception Error)
            {
                ButtonsColorSet(Button_Launcher_logs, 3, true);
                LogToFileAddons.OpenLog("SETTINGS CLEAR", "Unable to Delete Old Launcher Logs", Error, "Exclamation", false);
            }
        }
        /// <summary>
        /// Settings Change Game Files Location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsGameFiles_Click(object sender, EventArgs e)
        {
            DialogResult Status_Dialog_Result = default;
#if !(RELEASE_UNIX || DEBUG_UNIX)
            OpenFileDialog changeGameFilesPath = new OpenFileDialog
            {
                InitialDirectory = "C:\\",
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                AutoUpgradeEnabled = false,
                Title = "Select the location to Find or Download nfsw.exe",
                FileName = "   Select Game Files Folder"
            };

            if ((Status_Dialog_Result = changeGameFilesPath.ShowDialog(Screen_Parent.Screen_Instance)) == DialogResult.OK)
            {
                NewGameFilesPath = Path.GetDirectoryName(changeGameFilesPath.FileName) ?? "Invalid Folder Path";
                Label_Game_Current_Path_Setup.Text = Label_Game_Current_Path.Text = "NEW DIRECTORY";
                LinkLabel_Game_Path_Setup.Text = LinkLabel_Game_Path.Text = NewGameFilesPath;
            }

            changeGameFilesPath.Dispose();
#else
            FolderBrowserDialog changeGameFilesPath = new FolderBrowserDialog();

            if ((Status_Dialog_Result = changeGameFilesPath.ShowDialog(Screen_Parent.Screen_Instance)) == DialogResult.OK)
            {
                NewGameFilesPath = Path.GetFullPath(changeGameFilesPath.SelectedPath);
                Label_Game_Current_Path_Setup.Text = Label_Game_Current_Path.Text = "NEW DIRECTORY";
                LinkLabel_Game_Path_Setup.Text = LinkLabel_Game_Path.Text = NewGameFilesPath;
            }
#endif
            if (Screen_Parent.Launcher_Setup ==1 && (Status_Dialog_Result == DialogResult.OK))
            {
                ButtonsColorSet(Button_Change_Game_Path, 1, true);
                ButtonsColorSet(Button_Change_Game_Path_Setup, 1, true);

                if (!string.IsNullOrWhiteSpace(New_Choosen_CDN))
                {
                    ButtonsColorSet(Button_Save_Setup, 0, true);
                }
            }
        }
        /// <summary>
        /// Settings Open Current CDN in Browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsCDNCurrent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Launcher_CDN))
            {
                Process.Start(Save_Settings.Live_Data.Launcher_CDN);
            }
        }
        /// <summary>
        /// Settings Open Current Launcher Path in Explorer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsLauncherPathCurrent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewLauncherPath))
            {
                Process.Start(NewLauncherPath);
            }
        }
        /// <summary>
        /// Settings Open Current Game Files Path in Explorer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsGameFilesCurrent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewGameFilesPath))
            {
                Process.Start(NewGameFilesPath);
            }
        }
        /// <summary>
        /// Settings Open About Dialog 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsAboutButton_Click(object sender, EventArgs e)
        {
            Screen_About.OpenScreen();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Console_Enter(object sender, EventArgs e)
        {
            FunctionEvents.Console_Commands(Input_Console.Text);
            Input_Console.Text = string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_Proxy_Logging_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Screen_Instance.DisposedForm())
            {
                Label_Proxy_Logging_Selected_Details.Text = ((Json_List_Proxy_Logging)ComboBox_Proxy_Logging.SelectedItem).Details;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_Proxy_GZip_Version_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Screen_Instance.DisposedForm())
            {
                Label_Proxy_GZip_Version_Selected_Details.Text = ((Json_List_Proxy_GZip_Version)ComboBox_Proxy_GZip_Version.SelectedItem).Details;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_Launcher_Logging_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Screen_Instance.DisposedForm())
            {
                Label_Launcher_Logging_Selected_Details.Text = ((Json_List_Launcher_Logging)ComboBox_Launcher_Logging.SelectedItem).Details;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_Launcher_Builds_Branch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Screen_Instance.DisposedForm())
            {
                Label_Launcher_Builds_Branch_Selected_Details.Text = ((Json_List_Launcher_Builds)ComboBox_Launcher_Builds_Branch.SelectedItem).Details;
            }
        }
        #region Settings Load
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Screen_Settings_Load(object sender, EventArgs e)
        {

            /*******************************/
            /* Read Settings.ini            /
            /*******************************/

            if (File.Exists(Save_Settings.Live_Data.Game_Path + "/profwords") || File.Exists(Save_Settings.Live_Data.Game_Path + "/profwords_dis"))
            {
                CheckBox_Word_Filter_Check.Checked = !File.Exists(Save_Settings.Live_Data.Game_Path + "/profwords");
            }
            else
            {
                CheckBox_Word_Filter_Check.Enabled = false;
            }

            Label_Theme_Name.Text = "Theme Name: " + Theming.ThemeName;
            Label_Theme_Author.Text = "Theme Author: " + Theming.ThemeAuthor;

            /*******************************/
            /* Folder Locations             /
            /*******************************/

            NewGameFilesPath = Save_Settings.Live_Data.Game_Path;
            NewLauncherPath = Locations.LauncherFolder;

            CheckBox_Proxy.Checked = Save_Settings.Proxy_RunTime();
            CheckBox_RPC.Checked = Save_Settings.RPC_Discord();
            CheckBox_Alt_WebCalls.Checked = Save_Settings.WebCalls_Alt();
            CheckBox_Theme_Support.Checked = Save_Settings.Theme_Custom();
            CheckBox_JSON_Update_Cache.Checked = Save_Settings.Update_Frequency_JSON();
            CheckBox_Proxy_Domain.Checked = Save_Settings.Proxy_Domain();
            CheckBox_Host_to_IP.Checked = Save_Settings.Legacy_Host_To_IP();
            CheckBox_Account_Manager.Checked = Save_Settings.Account_Manager();
            CheckBox_Custom_Certificate.Checked = Save_Settings.Certificate_Mode();

            /* Trigger Events for CheckBox Text */
            CheckBox_Enable_Affinity_Range_CheckedChanged(default, default);
            CheckBox_RPC_CheckedChanged(default, default);
            CheckBox_JSON_Update_Cache_CheckedChanged(default, default);
            CheckBox_Theme_Support_CheckedChanged(default, default);
            CheckBox_Alt_WebCalls_CheckedChanged(default, default);
            CheckBox_Proxy_CheckedChanged(default, default);
            CheckBox_Host_to_IP_CheckedChanged(default, default);
            CheckBox_Proxy_Domain_CheckedChanged(default, default);
            CheckBox_Account_Manager_CheckedChanged(default, default);
            CheckBox_Custom_Certificate_CheckedChanged(default, default);

            switch (Save_Settings.Downloader_Game())
            {
                case 0:
                    Radio_Button_GameFiles_Downloader_LZMA.Checked = true;
                    break;
                case 1:
                    Radio_Button_GameFiles_Downloader_SBRW_Pack.Checked = true;
                    break;
                case 2:
                    Radio_Button_GameFiles_Downloader_Raw.Checked = true;
                    break;
            }

            int Proxy_Port_Convert = 0;
            if (int.TryParse(Save_Settings.Live_Data.Launcher_Proxy_Port, out Proxy_Port_Convert))
            {
                if ((Proxy_Port_Convert < 0) || (Proxy_Port_Convert > 65353))
                {
                    Proxy_Port_Convert = 0;
                }
            }
            else
            {
                Proxy_Port_Convert = 0;
            }

            NumericUpDown_Proxy_Port.Value = Proxy_Port_Convert;

            int WebClient_Timeout_Convert = 0;
            if (int.TryParse(Save_Settings.Live_Data.Launcher_WebCall_TimeOut_Time, out WebClient_Timeout_Convert))
            {
                if ((WebClient_Timeout_Convert < 0) || (WebClient_Timeout_Convert > 179))
                {
                    WebClient_Timeout_Convert = 0;
                }
            }
            else
            {
                WebClient_Timeout_Convert = 0;
            }

            NumericUpDown_WebClient_Timeout.Value = WebClient_Timeout_Convert;

            Display_Timer_Button();

            /*******************************/
            /* Enable/Disable Visuals       /
            /*******************************/

            if (!string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Game_Path))
            {
                if (File.Exists(Path.Combine(Save_Settings.Live_Data.Game_Path, "NFSWO_COMMUNICATION_LOG.txt")))
                {
                    ButtonsColorSet(Button_Clear_NFSWO_Logs, 2, true);
                }
                else
                {
                    ButtonsColorSet(Button_Clear_NFSWO_Logs, 4, false);
                }
            }
            else
            {
                ButtonsColorSet(Button_Clear_NFSWO_Logs, 4, false);
            }

            if (Directory.Exists(Save_Settings.Live_Data.Game_Path + "/.data"))
            {
                ButtonsColorSet(Button_Clear_Server_Mods, 2, true);
            }
            else
            {
                ButtonsColorSet(Button_Clear_Server_Mods, 4, false);
            }

            if (!string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Game_Path))
            {
                await Task.Run(() =>
                {
                    try
                    {
                        DirectoryInfo CrashLogFilesDirectory = new DirectoryInfo(Save_Settings.Live_Data.Game_Path);

                        if (CrashLogFilesDirectory.EnumerateFiles("SBRCrashDump_CL0*.dmp", SearchOption.TopDirectoryOnly).Count() != 0)
                        {
                            ButtonsColorSet(Button_Clear_Crash_Logs, 2, true);
                        }
                        else if (CrashLogFilesDirectory.EnumerateFiles("SBRCrashDump_CL0*.dmp", SearchOption.TopDirectoryOnly).Count() == 0)
                        {
                            ButtonsColorSet(Button_Clear_Crash_Logs, 4, false);
                        }
                        else
                        {
                            ButtonsColorSet(Button_Clear_Crash_Logs, 1, false);
                        }
                    }
                    catch (Exception Error)
                    {
                        ButtonsColorSet(Button_Clear_Crash_Logs, 3, false);
                        LogToFileAddons.OpenLog("SettingsScreen [SBRCrashDump_Check]", string.Empty, Error, string.Empty, true);
                    }
                });
            }


            await Task.Run(() =>
            {
                try
                {
                    DirectoryInfo LauncherLogFilesDirectory = new DirectoryInfo(Log_Location.LogFolder);

                    if (LauncherLogFilesDirectory.EnumerateDirectories().Count() != 1)
                    {
                        ButtonsColorSet(Button_Launcher_logs, 2, true);
                    }
                    else
                    {
                        ButtonsColorSet(Button_Launcher_logs, 1, false);
                    }
                }
                catch (Exception Error)
                {
                    ButtonsColorSet(Button_Launcher_logs, 3, false);
                    LogToFileAddons.OpenLog("SettingsScreen [Launcher Log Check]", string.Empty, Error, string.Empty, true);
                }
            });

            try
            {
                Log.Info("SETTINGS VERIFYHASH: Checking Characters in URL");
                Save_Settings.Live_Data.Launcher_CDN = Save_Settings.Live_Data.Launcher_CDN.EndsWith("/") ? Save_Settings.Live_Data.Launcher_CDN.TrimEnd('/') : Save_Settings.Live_Data.Launcher_CDN;
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("SETTINGS CDN URL TRIM", string.Empty, Error, string.Empty, true);
            }

            /********************************/
            /* CDN, APIs, & Restore Last CDN /
            /********************************/

            /* Check If Launcher Failed to Connect to any APIs */
            if (!VisualsAPIChecker.Local_Cached_API())
            {
                ("Unable to Connect to any CDN List API. Please check your connection." +
                "\nCDN Dropdown List will not be available on Settings Screen").Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            try
            {
                if (BuildDevelopment.Allowed())
                {
                    FunctionStatus.DoesCDNSupportVerifyHash = true;
                    ButtonsColorSet(Button_Verify_Scan, 4, true);
                }
                else
                {
                    ButtonsColorSet(Button_Verify_Scan, 0, false);
                    await Task.Run(() =>
                    {
                        if (!Application.OpenForms[this.Name].IsDisposed)
                        {
                            if (!Application.OpenForms[this.Name].Disposing)
                            {
                                switch (API_Core.StatusCheck(Save_Settings.Live_Data.Launcher_CDN + "/unpacked/checksums.dat", 10))
                                {
                                    case APIStatus.Online:
                                        FunctionStatus.DoesCDNSupportVerifyHash = true;
                                        ButtonsColorSet(Button_Verify_Scan, (Save_Settings.Live_Data.Game_Integrity != "Good") ? 2 : 0, true);
                                        break;
                                    default:
                                        FunctionStatus.DoesCDNSupportVerifyHash = false;
                                        ButtonsColorSet(Button_Verify_Scan, 3, true);
                                        break;
                                }
                            }
                        }
                    });
                }
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("SETTINGS VERIFYHASH", string.Empty, Error, string.Empty, true);
            }
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Console_Quick_Send(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Console_Enter(sender, e);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            if (Screen_Parent.Launcher_Setup.Equals(1))
            {
                Screen_Parent.Launcher_Setup = -1;
            }

            Close();
        }
        #endregion
    }
}