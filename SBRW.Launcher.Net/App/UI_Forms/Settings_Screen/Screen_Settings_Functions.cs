using SBRW.Launcher.App.UI_Forms.Parent_Screen;
using SBRW.Launcher.Core.Extension.Api_;
using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.Core.Extension.String_;
using SBRW.Launcher.Core.Extra.File_;
using SBRW.Launcher.Core.Theme;
using SBRW.Launcher.RunTime.InsiderKit;
using SBRW.Launcher.RunTime.LauncherCore.APICheckers;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.Lists;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using SBRW.Launcher.RunTime.LauncherCore.ModNet;
using SBRW.Launcher.RunTime.LauncherCore.Support;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SBRW.Launcher.App.UI_Forms.Settings_Screen
{
    partial class Screen_Settings
    {
        #region Support Functions
        /// <summary>
        /// 
        /// </summary>
        private void Setup_Save_Button_Check()
        {
            if (!string.IsNullOrWhiteSpace(Save_Settings.Game_Archive_Path()) && !string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Launcher_CDN))
            {
                ButtonsColorSet(Button_Save_Setup, 1, true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void WindowsDefenderGameFilesDirctoryChange()
        {
#if !(RELEASE_UNIX || DEBUG_UNIX)
            /* Check if New Game! Files is not in Banned Folder Locations */
            CheckGameFilesDirectoryPrevention();
            /* Store Old Location for Security Panel to Use Later on */
            Save_Settings.Live_Data.Game_Path_Old = Save_Settings.Live_Data.Game_Path;
            Save_Settings.Live_Data.Firewall_Game = "Not Excluded";
            Save_Settings.Live_Data.Defender_Game = "Not Excluded";
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
        /// <summary>
        /// 
        /// </summary>
        private void CheckGameFilesDirectoryPrevention()
        {
#if !(RELEASE_UNIX || DEBUG_UNIX)
            bool FailSafePathCreation = false;
            switch (FunctionStatus.CheckFolder(NewGameFilesPath))
            {
                case FolderType.IsSameAsLauncherFolder:
                    FailSafePathCreation = true;
                    Save_Settings.Live_Data.Game_Path = Locations.GameFilesFailSafePath;
                    Log.Error("LAUNCHER: Installing NFSW in same location where the GameLauncher resides is NOT allowed.");
                    MessageBox.Show(null, string.Format("Installing NFSW in same location where the GameLauncher resides is NOT allowed." +
                        "\nInstead, we will install it at {0}.", Locations.GameFilesFailSafePath), "GameLauncher",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case FolderType.IsTempFolder:
                    FailSafePathCreation = true;
                    Save_Settings.Live_Data.Game_Path = Locations.GameFilesFailSafePath;
                    Log.Error("LAUNCHER: (╯°□°）╯︵ ┻━┻ Installing NFSW in the Temp Folder is NOT allowed!");
                    MessageBox.Show(null, string.Format("(╯°□°）╯︵ ┻━┻\n\nInstalling NFSW in the Temp Folder is NOT allowed!" +
                        "\nInstead, we will install it at {0}.", Locations.GameFilesFailSafePath + "\n\n┬─┬ ノ( ゜-゜ノ)"), "GameLauncher",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case FolderType.IsProgramFilesFolder:
                case FolderType.IsUsersFolders:
                case FolderType.IsWindowsFolder:
                    FailSafePathCreation = true;
                    Save_Settings.Live_Data.Game_Path = Locations.GameFilesFailSafePath;
                    Log.Error("LAUNCHER: Installing NFSW in a Special Directory is disadvised.");
                    MessageBox.Show(null, string.Format("Installing NFSW in a Special Directory is not recommended or allowed." +
                        "\nInstead, we will install it at {0}.", Locations.GameFilesFailSafePath), "GameLauncher",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
            Save_Settings.Save();

            if (FailSafePathCreation)
            {
                if (!Directory.Exists(Locations.GameFilesFailSafePath))
                {
                    try
                    {
                        Directory.CreateDirectory(Locations.GameFilesFailSafePath);
                    }
                    catch (Exception Error)
                    {
                        LogToFileAddons.OpenLog("Launcher", string.Empty, Error, string.Empty, true);
                    }
                }
            }
#endif
        }
        #endregion
        #region ComboxList Setup
        /// <summary>
        /// 
        /// </summary>
        private void RememberLastLanguage()
        {
            /* Last Selected CDN */
            Log.Core("SETTINGS LANGLIST: Checking...");
            Log.Core("SETTINGS LANGLIST: Setting first Language in list");
            Log.Core("SETTINGS LANGLIST: Checking if Language is set on INI File");

            try
            {
                if (!string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Launcher_Language))
                {
                    string SavedLang = Save_Settings.Live_Data.Launcher_Language.Trim();

                    Log.Core("SETTINGS LANGLIST: Found something!");
                    Log.Core("SETTINGS LANGLIST: Checking if Language exists on our database");

                    if (LanguageListUpdater.CleanList.FindIndex(i => string.Equals(i.Value_Ini, SavedLang)) != 0)
                    {
                        Log.Core("SETTINGS LANGLIST: Language found! Checking its Value");
                        var index = LanguageListUpdater.CleanList.FindIndex(i => string.Equals(i.Value_Ini, SavedLang));

                        Log.Core("SETTINGS LANGLIST: ID is " + index);
                        if (index >= 0)
                        {
                            Log.Core("SETTINGS LANGLIST: ID set correctly");
                            ComboBox_Language_List.SelectedIndex = index;
                        }
                        else if (index < 0)
                        {
                            ComboBox_Language_List.SelectedIndex = 1;
                        }
                    }
                    else
                    {
                        Log.Warning("SETTINGS LANGLIST: Unable to find anything, assuming default");
                        ComboBox_Language_List.SelectedIndex = 1;
                        Log.Warning("SETTINGS LANGLIST: Unknown entry value is " + SavedLang);
                    }
                    Log.Core("SETTINGS LANGLIST: All done");
                }
                else
                {
                    Log.Warning("SETTINGS LANGLIST: Unable to find anything, assuming default");
                    ComboBox_Language_List.SelectedIndex = 1;
                }
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("SETTINGS LANGLIST", string.Empty, Error, string.Empty, true);
            }
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        private void Display_Timer_Button()
        {
            if (Save_Settings.Live_Data.Launcher_Display_Timer == "1")
            {
                Radio_Button_Dynamic_Timer.Checked = true;
            }
            else if (Save_Settings.Live_Data.Launcher_Display_Timer == "2")
            {
                Radio_Button_No_Timer.Checked = true;
            }
            else
            {
                Radio_Button_Static_Timer.Checked = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GameDownloaderButtonSelection()
        {
            if (Radio_Button_GameFiles_Downloader_LZMA.Checked)
            {
                return "0";
            }
            else if (Radio_Button_GameFiles_Downloader_SBRW_Pack.Checked)
            {
                return "1";
            }
            else
            {
                return "2";
            }
        }
        /* CDN Display Playing Game! - DavidCarbon */
        /// <summary>
        /// 
        /// </summary>
        private async void PingSavedCDN()
        {
            if (!string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Launcher_CDN))
            {
                LinkLabel_CDN_Current.LinkColor = Color_Text.L_Two;
                Log.Info("SETTINGS PINGING CDN: Checking Current CDN from Settings.ini");

                if (Screen_Instance != null)
                {
                    await Task.Run(() =>
                    {
                        switch (API_Core.StatusCheck(Save_Settings.Live_Data.Launcher_CDN + "/index.xml", 10))
                        {
                            case APIStatus.Online:
                                if (Screen_Instance != null)
                                {
                                    LinkLabel_CDN_Current.SafeInvokeAction(() =>
                                    {
                                        LinkLabel_CDN_Current.LinkColor = Color_Text.S_Sucess;
                                    });
                                    Log.UrlCall("SETTINGS PINGING CDN: " + Save_Settings.Live_Data.Launcher_CDN + " Is Online!");
                                }
                                break;
                            default:
                                if (Screen_Instance != null)
                                {
                                    LinkLabel_CDN_Current.SafeInvokeAction(() =>
                                    {
                                        LinkLabel_CDN_Current.LinkColor = Color_Text.S_Error;
                                    });
                                    Log.UrlCall("SETTINGS PINGING CDN: " + Save_Settings.Live_Data.Launcher_CDN + " Is Offline!");
                                }
                                break;
                        }
                    });
                }
            }
            else
            {
                Log.Error("SETTINGS PINGING CDN: Settings.ini has an Empty CDN URL");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void PingAPIStatus()
        {
            if (VisualsAPIChecker.UnitedAPI())
            {
                Label_API_Status_One.Text = "[API] United: Online";
                Label_API_Status_One.ForeColor = Color_Text.S_Sucess;
            }
            else
            {
                Label_API_Status_One.ForeColor = Color_Text.S_Warning;
                if (VisualsAPIChecker.UnitedSL && !VisualsAPIChecker.UnitedCDNL)
                {
                    Label_API_Status_One.Text = "[API] United: Server List Only";
                }
                else if (!VisualsAPIChecker.UnitedSL && VisualsAPIChecker.UnitedCDNL)
                {
                    Label_API_Status_One.Text = "[API] United: CDN List Only";
                }
                else
                {
                    Label_API_Status_One.Text = "[API] United: " + Strings.Truncate(APIChecker.StatusStrings(VisualsAPIChecker.UnitedSC), 32);
                    Label_API_Status_One.ForeColor = Color_Text.S_Error;
                }
                Label_API_Status_Two.Visible = true;
            }

            if (VisualsAPIChecker.CarbonAPI())
            {
                Label_API_Status_Two.Text = "[API] Carbon: Online";
                Label_API_Status_Two.ForeColor = Color_Text.S_Sucess;
            }
            else
            {
                Label_API_Status_Two.ForeColor = Color_Text.S_Warning;
                if (VisualsAPIChecker.CarbonSL && !VisualsAPIChecker.CarbonCDNL)
                {
                    Label_API_Status_Two.Text = "[API] Carbon: Server List Only";
                }
                else if (!VisualsAPIChecker.CarbonSL && VisualsAPIChecker.CarbonCDNL)
                {
                    Label_API_Status_Two.Text = "[API] Carbon: CDN List Only";
                }
                else
                {
                    Label_API_Status_Two.Text = "[API] Carbon: " + Strings.Truncate(APIChecker.StatusStrings(VisualsAPIChecker.CarbonSC), 32);
                    Label_API_Status_Two.ForeColor = Color_Text.S_Error;
                }
                Label_API_Status_Three.Visible = true;
            }

            if (VisualsAPIChecker.CarbonAPITwo())
            {
                Label_API_Status_Three.Text = "[API] Carbon (2nd): Online";
                Label_API_Status_Three.ForeColor = Color_Text.S_Sucess;
            }
            else
            {
                Label_API_Status_Three.ForeColor = Color_Text.S_Warning;
                if (VisualsAPIChecker.CarbonTwoSL && !VisualsAPIChecker.CarbonTwoCDNL)
                {
                    Label_API_Status_Three.Text = "[API] Carbon (2nd): Server List Only";
                }
                else if (!VisualsAPIChecker.CarbonTwoSL && VisualsAPIChecker.CarbonTwoCDNL)
                {
                    Label_API_Status_Three.Text = "[API] Carbon (2nd): CDN List Only";
                }
                else
                {
                    Label_API_Status_Three.Text = "[API] Carbon (2nd): " + Strings.Truncate(APIChecker.StatusStrings(VisualsAPIChecker.CarbonTwoSC), 32);
                    Label_API_Status_Three.ForeColor = Color_Text.S_Error;
                }

                Label_API_Status_Four.Visible = true;
            }

            if (VisualsAPIChecker.Local_Cached_API())
            {
                Label_API_Status_Four.Text = "[API] Local Cache: Active";
                Label_API_Status_Four.ForeColor = Color_Text.S_Warning;
            }
            else
            {
                Label_API_Status_Four.ForeColor = Color_Text.S_Warning;
                if (VisualsAPIChecker.Local_Cached_SL && !VisualsAPIChecker.Local_Cached_CDNL)
                {
                    Label_API_Status_Four.Text = "[API] Local Cache: Server List Only";
                }
                else if (!VisualsAPIChecker.Local_Cached_SL && VisualsAPIChecker.Local_Cached_CDNL)
                {
                    Label_API_Status_Four.Text = "[API] Local Cache: CDN List Only";
                }
                else
                {
                    Label_API_Status_Four.Text = "[API] Local Cache: Not Found";
                    Label_API_Status_Four.ForeColor = Color_Text.S_Error;
                }
            }
        }
        /// <summary>
        /// Sets the Color for Buttons
        /// </summary>
        /// <param name="Elements">Button Control Name</param>
        /// <param name="Color">Range 0-3 Sets Colored Button.
        /// <code>"0" Checking Blue</code><code>"1" Success Green</code><code>"2" Warning Orange</code><code>"3" Error Red</code></param>
        /// <param name="EnabledORDisabled">Enables or Disables the Button</param>
        /// <remarks>Range 0-3 Sets Colored Button.
        /// <code>"0" Checking Blue</code><code>"1" Success Green</code><code>"2" Warning Orange</code><code>"3" Error Red</code></remarks>
        public static void ButtonsColorSet(Button Elements, int Color, bool EnabledORDisabled)
        {
            switch (Color)
            {
                /* Checking Blue */
                case 0:
                    Elements.SafeInvokeAction(() =>
                    {
                        Elements.ForeColor = Color_Winform_Buttons.Blue_Fore_Color;
                        Elements.BackColor = Color_Winform_Buttons.Blue_Back_Color;
                        Elements.FlatAppearance.BorderColor = Color_Winform_Buttons.Blue_Border_Color;
                        Elements.FlatAppearance.MouseOverBackColor = Color_Winform_Buttons.Blue_Mouse_Over_Back_Color;
                        Elements.Enabled = EnabledORDisabled;
                    });
                    break;
                /* Success Green */
                case 1:
                    Elements.SafeInvokeAction(() =>
                    {
                        Elements.ForeColor = Color_Winform_Buttons.Green_Fore_Color;
                        Elements.BackColor = Color_Winform_Buttons.Green_Back_Color;
                        Elements.FlatAppearance.BorderColor = Color_Winform_Buttons.Green_Border_Color;
                        Elements.FlatAppearance.MouseOverBackColor = Color_Winform_Buttons.Green_Mouse_Over_Back_Color;
                        Elements.Enabled = EnabledORDisabled;
                    });
                    break;
                /* Warning Orange */
                case 2:
                    Elements.SafeInvokeAction(() =>
                    {
                        Elements.ForeColor = Color_Winform_Buttons.Yellow_Fore_Color;
                        Elements.BackColor = Color_Winform_Buttons.Yellow_Back_Color;
                        Elements.FlatAppearance.BorderColor = Color_Winform_Buttons.Yellow_Border_Color;
                        Elements.FlatAppearance.MouseOverBackColor = Color_Winform_Buttons.Yellow_Mouse_Over_Back_Color;
                        Elements.Enabled = EnabledORDisabled;
                    });
                    break;
                /* Error Red */
                case 3:
                    Elements.SafeInvokeAction(() =>
                    {
                        Elements.ForeColor = Color_Winform_Buttons.Red_Fore_Color;
                        Elements.BackColor = Color_Winform_Buttons.Red_Back_Color;
                        Elements.FlatAppearance.BorderColor = Color_Winform_Buttons.Red_Border_Color;
                        Elements.FlatAppearance.MouseOverBackColor = Color_Winform_Buttons.Red_Mouse_Over_Back_Color;
                        Elements.Enabled = EnabledORDisabled;
                    });
                    break;
                /* Unknown Gray */
                default:
                    Elements.SafeInvokeAction(() =>
                    {
                        Elements.ForeColor = Color_Winform_Buttons.Gray_Fore_Color;
                        Elements.BackColor = Color_Winform_Buttons.Gray_Back_Color;
                        Elements.FlatAppearance.BorderColor = Color_Winform_Buttons.Gray_Border_Color;
                        Elements.FlatAppearance.MouseOverBackColor = Color_Winform_Buttons.Gray_Mouse_Over_Back_Color;
                        Elements.Enabled = EnabledORDisabled;
                    });
                    break;
            }
        }
        /// <summary>
        /// Translation for Game Display Timer
        /// </summary>
        /// <returns></returns>
        private string Display_Timer_Button_Selection()
        {
            if (Radio_Button_Dynamic_Timer.Checked)
            {
                return "1";
            }
            else if (Radio_Button_No_Timer.Checked)
            {
                return "2";
            }
            else
            {
                return "0";
            }
        }
        /// <summary>
        /// Translation for Log to File Selection
        /// </summary>
        /// <returns></returns>
        private string Log_To_File_Selection()
        {
            if (Radio_Button_Proxy_Logging_All.Checked)
            {
                return "1";
            }
            else if (Radio_Button_Proxy_Logging_Errors.Checked)
            {
                return "2";
            }
            else if (Radio_Button_Proxy_Logging_Requests.Checked)
            {
                return "3";
            }
            else if (Radio_Button_Proxy_Logging_Responses.Checked)
            {
                return "4";
            }
            else
            {
                return "0";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Enable_Affinity_Range"></param>
        private void Enable_Affinity_Range(bool Enable_Affinity_Range)
        {
            /* NOTE: We start counting up from Core 0 !!! So for 4 Cores its range is 0-3 */
            if (Enable_Affinity_Range)
            {
                if (BuildDevelopment.Allowed())
                {
                    NumericUpDown_Range_Affinity.Increment = 1;
                    NumericUpDown_Range_Affinity.Minimum = 0;
                    NumericUpDown_Range_Affinity.Maximum = Environment.ProcessorCount - 1;
                    NumericUpDown_Range_Affinity.Value = 3;
                }

                Label_Affinity_Core_Calculator.Visible = true;
                Panel_Affinity_Range.Visible = true;
            }
            else if (BuildDevelopment.Allowed())
            {
                NumericUpDown_Range_Affinity.Increment = 2;
                NumericUpDown_Range_Affinity.Minimum = 2;
                NumericUpDown_Range_Affinity.Maximum = Environment.ProcessorCount >= 8 ? 8 : Environment.ProcessorCount.Equals(6) ? 6 : 4;
                NumericUpDown_Range_Affinity.Value = Environment.ProcessorCount >= 4 ? 4 : 2;
                Label_Affinity_Core_Calculator.Visible = true;
                Panel_Affinity_Range.Visible = true;
                Label_Affinity_Core_Range.Text = $"{(int)NumericUpDown_Range_Affinity.Value} Cores";
            }
            else
            {
                Label_Affinity_Core_Calculator.Visible = false;
                Panel_Affinity_Range.Visible = false;
            }
        }
    }
}