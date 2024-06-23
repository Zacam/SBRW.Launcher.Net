using SBRW.Launcher.App.UI_Forms.Main_Screen;
using SBRW.Launcher.App.UI_Forms.Settings_Screen;
using SBRW.Launcher.Core.Discord.RPC_;
using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.Core.Extra.File_;
using SBRW.Launcher.Core.Proxy.Nancy_;
using SBRW.Launcher.RunTime.LauncherCore.APICheckers;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.LauncherUpdater;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using SBRW.Launcher.RunTime.LauncherCore.ModNet;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SBRW.Launcher.Core.Extra.File_.Save_;
using SBRW.Launcher.RunTime.LauncherCore.Support;

namespace SBRW.Launcher.App.UI_Forms.Parent_Screen
{
    /// <summary>
    /// 
    /// </summary>
    partial class Screen_Parent
    {
        /// <summary>
        /// 
        /// </summary>
        public void Position_Window_Set()
        {
            FunctionStatus.CenterScreen(this);
        }
        /// <summary>
        /// 
        /// </summary>
        public static async void First_Time_Run()
        {
            if (!LauncherUpdateCheck.UpdatePopupStoppedSplashScreen)
            {
                FunctionStatus.LoadingComplete = true;
            }

            if (!string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Game_Path))
            {
                LogToFileAddons.Parent_Log_Screen(11, "LAUNCHER", "Checking Installation Directory at " + Save_Settings.Live_Data.Game_Path);
            }

            LogToFileAddons.Parent_Log_Screen(2, "LAUNCHER", "Checking Game Installation");
            if (string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Game_Path) ||
                (Save_Settings.Live_Data.Launcher_CDN.Contains("http://localhost") &&
                !Save_Settings.Live_Data.Launcher_CDN.Contains(".")))
            {
                Presence_Launcher.Status(0, "Doing First Time Run");
                LogToFileAddons.Parent_Log_Screen(11, "LAUNCHER", "First run!");

                try
                {
                    if (Screen_Instance != default)
                    {
                        Launcher_Setup = 1;
                        Screen_Instance.Text = "Setup - SBRW Launcher: " + Application.ProductVersion;
                        Screen_Settings Custom_Instance_Settings = new Screen_Settings() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true, FormBorderStyle = FormBorderStyle.None };
                        Screen_Instance.Panel_Form_Screens.Visible = true;
                        Screen_Instance.Panel_Form_Screens.Controls.Add(Custom_Instance_Settings);
                        Custom_Instance_Settings.Show();
                        Screen_Instance.Panel_Splash_Screen.Visible = false;

                        await Task.Run(() =>
                        {
                            while (Launcher_Setup.Equals(1))
                            {
                                /* Just keep looping until the user completes the setup (Screen) */
                                Thread.Sleep(1000);
                            }
                        });

                        if (Launcher_Setup.Equals(0))
                        {
                            Screen_Instance.Text = "SBRW Launcher: " + Application.ProductVersion;
                            Screen_Instance.Panel_Splash_Screen.Visible = true;
                            Screen_Instance.Panel_Form_Screens.Visible = false;
                        }
                        else
                        {
                            FunctionStatus.LauncherForceClose = true;
                        }
                    }
                    else
                    {
                        FunctionStatus.LauncherForceClose = true;
                    }
                }
                catch (Exception Error)
                {
                    LogToFileAddons.OpenLog("FOLDER SELECT DIALOG", string.Empty, Error, string.Empty, true);

                    if (string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Launcher_CDN))
                    {
                        LogToFileAddons.Parent_Log_Screen(4, "LAUNCHER", "CDN Source URL was Empty! Setting a Null Safe URL 'http://localhost'");
                        Save_Settings.Live_Data.Launcher_CDN = "http://localhost";
                        Save_Settings.Save();
                    }

                    if (string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Game_Path))
                    {
                        LogToFileAddons.Parent_Log_Screen(11, "LAUNCHER", "Installation Directory was Empty! Creating and Setting Directory at " + Locations.GameFilesFailSafePath);
                        Save_Settings.Live_Data.Game_Path = Locations.GameFilesFailSafePath;
                        Save_Settings.Save();
                    }
                }

                if (FunctionStatus.LauncherForceClose)
                {
                    FunctionStatus.ErrorCloseLauncher("Closing From Setup Screen", false);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Game_Path))
                    {
                        Presence_Launcher.Status(0, "User Selecting/Inputting Game Files Folder");

                        try
                        {
#if !(RELEASE_UNIX || DEBUG_UNIX)

                            OpenFileDialog FolderDialog = new OpenFileDialog
                            {
                                InitialDirectory = "C:\\",
                                ValidateNames = false,
                                CheckFileExists = false,
                                CheckPathExists = true,
                                AutoUpgradeEnabled = false,
                                Title = "Select the location to Find or Download nfsw.exe",
                                FileName = "   Select Game Files Folder"
                            };

                            if (FolderDialog.ShowDialog() == DialogResult.OK)
                            {
                                if (!string.IsNullOrWhiteSpace(FolderDialog.FileName))
                                {
                                    Save_Settings.Live_Data.Game_Path = Path.GetDirectoryName(FolderDialog.FileName) ?? string.Empty;
                                }
                            }

                            FolderDialog.Dispose();
#endif
                            if (string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Game_Path))
                            {
                                await Task.Run(() =>
                                {
                                    try
                                    {
                                        Save_Settings.Live_Data.Game_Path = Path.GetFullPath("GameFiles");
                                    }
                                    catch
                                    {
                                        Save_Settings.Live_Data.Game_Path = "GameFiles";
                                    }
                                });
                            }

                            await Task.Run(() =>
                            {
                                if (!string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Game_Path))
                                {
#pragma warning disable CS8604
                                    Save_Settings.Live_Data.Game_Path.IsRestrictedGameFolderLocation(0);
#pragma warning restore CS8604
                                }
                                else
                                {
                                    FunctionStatus.LauncherForceClose = true;
                                }
                            });

                            if (!string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Game_Path))
                            {
                                LogToFileAddons.Parent_Log_Screen(2, "CLEANLINKS", "Game Path");
                                await Task.Run(() =>
                                {
                                    if (File.Exists(Path.Combine(Save_Settings.Live_Data.Game_Path, Locations.NameModLinks)))
                                    {
                                        ModNetHandler.CleanLinks(Save_Settings.Live_Data.Game_Path);
                                        LogToFileAddons.Parent_Log_Screen(3, "CLEANLINKS", "Done");
                                    }
                                    else
                                    {
                                        LogToFileAddons.Parent_Log_Screen(3, "CLEANLINKS", "Not Present");
                                    }
                                });
                            }
                        }
                        catch (Exception Error)
                        {
                            FunctionStatus.LauncherForceClose = true;
                            FunctionStatus.LauncherForceCloseReason = Error.Message;
                            LogToFileAddons.OpenLog("FOLDER SELECT DIALOG", string.Empty, Error, string.Empty, true);
                            if (Error.InnerException != null && !string.IsNullOrWhiteSpace(Error.InnerException.Message))
                            {
                                LogToFileAddons.Parent_Log_Screen(5, "FOLDER SELECT DIALOG", Error.InnerException.Message, false, true);
                            }
                        }
                    }
#pragma warning disable CS8604 // Possible null reference argument.
                    else if (Save_Settings.Live_Data.Game_Path.IsRestrictedGameFolderLocation(1))
                    {
                        LogToFileAddons.Parent_Log_Screen(12, "LAUNCHER", "Folder Check Trigger in 'FOLDER SELECT DIALOG'");
                    }
#pragma warning restore CS8604 // Possible null reference argument.
                }
            }
#pragma warning disable CS8604 // Possible null reference argument.
            else if (Save_Settings.Live_Data.Game_Path.IsRestrictedGameFolderLocation(1))
            {
                LogToFileAddons.Parent_Log_Screen(12, "LAUNCHER", "Folder Check Trigger");
            }
#pragma warning restore CS8604 // Possible null reference argument.

            LogToFileAddons.Parent_Log_Screen(11, "LAUNCHER", "Game Installation Path Done");

            if (FunctionStatus.LauncherForceClose)
            {
                FunctionStatus.ErrorCloseLauncher("Closing From Folder Dialog", false);
            }
            else
            {
#if !(RELEASE_UNIX || DEBUG_UNIX)
                LogToFileAddons.Parent_Log_Screen(2, "LAUNCHER", "Checking Game Path Location");
                Presence_Launcher.Status(0, "Checking Game Files Folder Location");

                await Task.Run(() =>
                {
                    switch (FunctionStatus.CheckFolder(Save_Settings.Live_Data.Game_Path))
                    {
                        case FolderType.IsSameAsLauncherFolder:
                            try
                            {
                                if (!Directory.Exists(Locations.GameFilesFailSafePath))
                                {
                                    Directory.CreateDirectory(Locations.GameFilesFailSafePath);
                                    LogToFileAddons.Parent_Log_Screen(11, "FOLDER", "Created Game Files Directory at " + Locations.GameFilesFailSafePath);
                                }
                            }
                            catch (Exception Error)
                            {
                                LogToFileAddons.OpenLog("FOLDER CREATE", string.Empty, Error, string.Empty, true);
                                if (Error.InnerException != null && !string.IsNullOrWhiteSpace(Error.InnerException.Message))
                                {
                                    LogToFileAddons.Parent_Log_Screen(5, "FOLDER Create", Error.InnerException.Message, false, true);
                                }
                            }
                            LogToFileAddons.Parent_Log_Screen(4, "LAUNCHER", "Installing NFSW in same location where the GameLauncher resides is NOT allowed.", false, true);
                            string.Format("Installing NFSW in same location where the GameLauncher resides is NOT allowed.\n" +
                                "Instead, we will install it at {0}.", Locations.GameFilesFailSafePath).Message_Box(
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Save_Settings.Live_Data.Game_Path = Locations.GameFilesFailSafePath;
                            break;
                        case FolderType.IsTempFolder:
                        case FolderType.IsUsersFolders:
                        case FolderType.IsProgramFilesFolder:
                        case FolderType.IsWindowsFolder:
                        case FolderType.IsRootFolder:
                            string constructMsg = string.Empty;
                            constructMsg += "Using this location for Game Files is not allowed.\n\n";
                            constructMsg += "The following locations are also NOT allowed:\n";
                            constructMsg += "• X:\\nfsw.exe (Root of Drive, such as C:\\ or D:\\, must be in a folder)\n";
                            constructMsg += "• C:\\Program Files\n";
                            constructMsg += "• C:\\Program Files (x86)\n";
                            constructMsg += "• C:\\Users (Includes 'Desktop', 'Documents', 'Downloads')\n";
                            constructMsg += "• C:\\Windows\n\n";
                            constructMsg += "Instead, we will install the NFSW Game at " + Locations.GameFilesFailSafePath;
                            try
                            {
                                if (!Directory.Exists(Locations.GameFilesFailSafePath))
                                {
                                    Directory.CreateDirectory(Locations.GameFilesFailSafePath);
                                    LogToFileAddons.Parent_Log_Screen(11, "FOLDER", "Created Game Files Directory at " + Locations.GameFilesFailSafePath);
                                }
                            }
                            catch (Exception Error)
                            {
                                LogToFileAddons.OpenLog("FOLDER CREATE", string.Empty, Error, string.Empty, true);
                                if (Error.InnerException != null && !string.IsNullOrWhiteSpace(Error.InnerException.Message))
                                {
                                    LogToFileAddons.Parent_Log_Screen(5, "FOLDER Create", Error.InnerException.Message, false, true);
                                }
                            }
                            LogToFileAddons.Parent_Log_Screen(4, "LAUNCHER", "Installing NFSW in a Restricted Location is not allowed.", false, true);
                            constructMsg.Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Save_Settings.Live_Data.Game_Path = Locations.GameFilesFailSafePath;
                            break;
                    }
                    Save_Settings.Save();
                });

                LogToFileAddons.Parent_Log_Screen(11, "LAUNCHER", "Done Checking Game Path Location");
#endif

                /* Check If Launcher Failed to Connect to any APIs */
                if (!VisualsAPIChecker.Local_Cached_API())
                {
                    Presence_Launcher.Status(0, "Launcher Encountered API Errors");

                    DialogResult restartAppNoApis = ("There is no internet connection or local cache, Launcher might crash." +
                        "\n\nClick Yes to Close GameLauncher" +
                        "\nor" +
                        "\nClick No Continue").Message_Box(MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (restartAppNoApis == DialogResult.Yes)
                    {
                        FunctionStatus.LauncherForceClose = true;
                    }
                }

                if (FunctionStatus.LauncherForceClose)
                {
                    FunctionStatus.ErrorCloseLauncher("Closing From API Error", false);
                }
                else
                {
                    try
                    {

                        if (Screen_Instance != null)
                        {
                            Screen_Instance.Text = "SBRW Launcher: " + Application.ProductVersion;
                            Screen_Main Custom_Instance_Settings = new Screen_Main() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true, FormBorderStyle = FormBorderStyle.None };
                            Screen_Instance.Panel_Form_Screens.Visible = true;
                            Screen_Instance.Panel_Form_Screens.Controls.Add(Custom_Instance_Settings);
                            Custom_Instance_Settings.Show();
                            Screen_Instance.Panel_Splash_Screen.Visible = false;
                            Screen_Instance.Size = new Size(891, 529);
                            Screen_Instance.Position_Window_Set();
                            LogToFileAddons.Parent_Log_Screen(1, "MAINSCREEN", "Hello World!", true);
                        }
                    }
                    catch (COMException Error)
                    {
                        LogToFileAddons.OpenLog("Main Screen", "Launcher Encounterd an Error.", Error, "Error", false);
                        FunctionStatus.ErrorCloseLauncher("Main Screen [Application Run]", false);
                    }
                    catch (Exception Error)
                    {
                        LogToFileAddons.OpenLog("Main Screen", "Launcher Encounterd an Error.", Error, "Error", false);
                        FunctionStatus.ErrorCloseLauncher("Main Screen [Application Run]", false);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void ClosingTasks()
        {
            Save_Settings.Save();
            Save_Account.Save();

            try
            {
                if (Screen_Main.LZMA_Downloader != null)
                {
                    if (Screen_Main.LZMA_Downloader.Downloading)
                    {
                        Screen_Main.LZMA_Downloader.Stop();
                    }
                }
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("CDN DOWNLOADER [LZMA]", string.Empty, Error, string.Empty, true);
            }

            try
            {
                if (Screen_Main.Pack_SBRW_Unpacker != null)
                {
                    Screen_Main.Pack_SBRW_Unpacker.Cancel = true;
                }

                if (Screen_Main.Pack_SBRW_Downloader != null)
                {
                    Screen_Main.Pack_SBRW_Downloader.Cancel = true;
                }
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("CDN DOWNLOADER", string.Empty, Error, string.Empty, true);
            }

            try
            {
                if (FunctionStatus.LauncherBattlePass)
                {
                    Process.GetProcessById(Screen_Main.NfswPid).Kill();
                }
                else
                {
                    Process[] allOfThem = Process.GetProcessesByName("nfsw");

                    if (allOfThem != null && allOfThem.Any())
                    {
                        foreach (var oneProcess in allOfThem)
                        {
                            Process.GetProcessById(oneProcess.Id).Kill();
                        }
                    }
                }
            }
            catch { }

            if (Presence_Launcher.Running())
            {
                Presence_Launcher.Stop("Close");
            }

            if (Proxy_Settings.Running())
            {
                Proxy_Server.Instance.Stop("Main Screen");
            }

            if (!string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Game_Path))
            {
                if (File.Exists(Path.Combine(Save_Settings.Live_Data.Game_Path, Locations.NameModLinks)) && !FunctionStatus.LauncherBattlePass)
                {
                    ModNetHandler.CleanLinks(Save_Settings.Live_Data.Game_Path);
                }
            }

            try
            {
                if (Screen_Main.Screen_Instance != null)
                {
                    if (Screen_Main.Screen_Instance.NotifyIcon_Notification.Visible)
                    {
                        Screen_Main.Screen_Instance.NotifyIcon_Notification.Visible = false;
                        Screen_Main.Screen_Instance.NotifyIcon_Notification.Dispose();
                    }
                }
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("Notification Disposal", string.Empty, Error, string.Empty, true);
            }

            Log_Verify.Stop = Log.Stop = true;
        }
    }
}
