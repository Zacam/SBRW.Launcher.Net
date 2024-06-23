using SBRW.Launcher.Core.Cache;
using SBRW.Launcher.Core.Discord.RPC_;
using SBRW.Launcher.Core.Extension.Hash_;
using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.Core.Extension.Registry_;
using SBRW.Launcher.Core.Extension.Time_;
using SBRW.Launcher.Core.Extension.Web_;
using SBRW.Launcher.Core.Extra.File_;
using SBRW.Launcher.Core.Required.System.Windows_;
using SBRW.Launcher.Core.Required.Certificate;
using SBRW.Launcher.Core.Theme;
using SBRW.Launcher.RunTime.InsiderKit;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.Languages.Visual_Forms;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using SBRW.Launcher.RunTime.LauncherCore.ModNet;
using SBRW.Launcher.RunTime.LauncherCore.Support;
using SBRW.Launcher.RunTime.LauncherCore.Visuals;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using SBRW.Launcher.RunTime.SystemPlatform.Windows;
using SBRW.Launcher.Core.Extra.File_.Save_;
#if !(RELEASE_UNIX || DEBUG_UNIX)
using SBRW.Launcher.RunTime.SystemPlatform.Components;
using SBRW.Launcher.Core.Extra.Ini_;
#else
using SBRW.Launcher.Core.Required.DLL;
using SBRW.Launcher.RunTime.SystemPlatform.Unix;
#endif

namespace SBRW.Launcher.App.UI_Forms.Parent_Screen
{
    /// <summary>
    /// 
    /// </summary>
    partial class Screen_Parent
    {
        #region Dragable Form Window & Functions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Move_Window_Mouse_Down(object sender, MouseEventArgs e)
        {
            if (e.Y <= 90)
            {
                Mouse_Down_Point = new Point(e.X, e.Y);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Move_Window_Mouse_Up(object sender, MouseEventArgs e)
        {
            Mouse_Down_Point = Point.Empty;
            Opacity = 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Move_Window_Mouse_Move(object sender, MouseEventArgs e)
        {
            if (Mouse_Down_Point.IsEmpty)
            {
                return;
            }
            else
            {
                Form Main_Local_Window = this as Form;
                Main_Local_Window.Location = new Point(Main_Local_Window.Location.X + (e.X - Mouse_Down_Point.X), Main_Local_Window.Location.Y + (e.Y - Mouse_Down_Point.Y));
                InformationCache.ParentScreenLocation = new Point(Main_Local_Window.Location.X + (e.X - Mouse_Down_Point.X), Main_Local_Window.Location.Y + (e.Y - Mouse_Down_Point.Y));
                Opacity = 0.9;
            }
        }
        #endregion
        #region Parent Screen Load and Shown
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Parent_Screen_Load(object sender, EventArgs e)
        {
            if (e != null)
            {
                LogToFileAddons.Parent_Log_Screen(11, "LAUNCHER", "Set Parent Window location");
                Position_Window_Set();
                LogToFileAddons.Parent_Log_Screen(3, "LAUNCHER", "Set Parent Window location");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Parent_Screen_Shown(object sender, EventArgs e)
        {
            if (e == null)
            {
                return;
            }

            Presence_Launcher.Start();

#if !(RELEASE_UNIX || DEBUG_UNIX)
            Presence_Launcher.Status(0, "Checking .NET Framework");
            await Task.Run(() =>
            {
                try
                {
                    /* Check if User has a compatible .NET Framework Installed */
                    if (int.TryParse(Registry_Core.Read("Release", @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\"), out int NetFrame_Version))
                    {
                        /* For now, allow edge case of Windows 8.0 to run .NET 4.6.1 where upgrading to 8.1 is not possible */
                        if (Product_Version.GetWindowsNumber() == 6.2 && NetFrame_Version <= 394254)
                        {
                            if ((Translations.Database("Program_TextBox_NetFrame_P1") +
                            " .NETFramework, Version=v4.6.1 \n\n" + Translations.Database("Program_TextBox_NetFrame_P2"))
                                .Message_Box(MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                            {
#if NETFRAMEWORK
                                Process.Start("https://dotnet.microsoft.com/download/dotnet-framework/net461");
#else
                                    Process.Start(new ProcessStartInfo { FileName = "https://dotnet.microsoft.com/download/dotnet-framework/net461", UseShellExecute = true });
#endif
                            }

                            FunctionStatus.LauncherForceClose = true;
                        }
                        /* Otherwise, all other OS Versions should have 4.6.2 as a Minimum Version */
                        else if (NetFrame_Version <= 394802)
                        {
                            if ((Translations.Database("Program_TextBox_NetFrame_P1") +
                            " .NETFramework, Version=v4.6.2 \n\n" + Translations.Database("Program_TextBox_NetFrame_P2"))
                            .Message_Box(MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                            {
#if NETFRAMEWORK
                                Process.Start("https://dotnet.microsoft.com/download/dotnet-framework");
#else
                                    Process.Start(new ProcessStartInfo { FileName = "https://dotnet.microsoft.com/download/dotnet-framework", UseShellExecute = true });
#endif
                            }

                            FunctionStatus.LauncherForceClose = true;
                        }
                        else
                        {
                            LogToFileAddons.Parent_Log_Screen(7, "NET-FRAMEWORK", "Supported Installed Version");
                        }
                    }
                    else
                    {
                        LogToFileAddons.Parent_Log_Screen(4, "NET-FRAMEWORK", "Failed to Parse Version");
                    }
                }
                catch
                {
                    FunctionStatus.LauncherForceClose = true;
                }
            });
#endif

            if (FunctionStatus.LauncherForceClose)
            {
                FunctionStatus.ErrorCloseLauncher("Closing From .NET Framework Check", false);
            }
            else
            {
                /* Set Launcher Directory */
                LogToFileAddons.Parent_Log_Screen(2, "SETUP", "Setting Launcher Folder Directory");
                Directory.SetCurrentDirectory(Locations.LauncherFolder);
                LogToFileAddons.Parent_Log_Screen(3, "SETUP", "Current Directory now Set at -> " + Locations.LauncherFolder);

#if !(RELEASE_UNIX || DEBUG_UNIX)
                LogToFileAddons.Parent_Log_Screen(2, "FOLDER LOCATION", "Checking Launcher Folder Directory");
                Presence_Launcher.Status(0, "Checking Launcher Folder Locations");

                await Task.Run(() =>
                {
                    switch (FunctionStatus.CheckFolder(Locations.LauncherFolder))
                    {
                        case FolderType.IsTempFolder:
                        case FolderType.IsUsersFolders:
                        case FolderType.IsProgramFilesFolder:
                        case FolderType.IsWindowsFolder:
                        case FolderType.IsRootFolder:
                            string Constructed_Msg = string.Empty;

                            Constructed_Msg += Translations.Database("Program_TextBox_Folder_Check_Launcher") + "\n\n";
                            Constructed_Msg += Translations.Database("Program_TextBox_Folder_Check_Launcher_P2") + "\n";
                            Constructed_Msg += "• X:\\GameLauncher.exe " + Translations.Database("Program_TextBox_Folder_Check_Launcher_P3") + "\n";
                            Constructed_Msg += "• C:\\Program Files\n";
                            Constructed_Msg += "• C:\\Program Files (x86)\n";
                            Constructed_Msg += "• C:\\Users " + Translations.Database("Program_TextBox_Folder_Check_Launcher_P4") + "\n";
                            Constructed_Msg += "• C:\\Windows\n\n";
                            Constructed_Msg += Translations.Database("Program_TextBox_Folder_Check_Launcher_P5") + "\n";
                            Constructed_Msg += "• 'C:\\Soapbox Race World' " + Translations.Database("Program_TextBox_Folder_Check_Launcher_P6") + " 'C:\\SBRW'\n";
                            Constructed_Msg += Translations.Database("Program_TextBox_Folder_Check_Launcher_P7") + "\n\n";

                            Constructed_Msg.Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Error);
                            FunctionStatus.LauncherForceClose = true;
                            break;
                    }
                });

                LogToFileAddons.Parent_Log_Screen(3, "FOLDER LOCATION", "Done");
#endif
                if (FunctionStatus.LauncherForceClose)
                {
                    FunctionStatus.ErrorCloseLauncher("Closing From Invalid Launcher Location", false);
                }
                else if (FunctionStatus.HasWriteAccessToFolder(Locations.LauncherFolder) == 0)
                {
                    FunctionStatus.LauncherForceClose = true;
                    FunctionStatus.LauncherForceCloseReason = Translations.Database("Program_TextBox_Folder_Write_Test");
                    FunctionStatus.ErrorCloseLauncher("Closing From No Write Access", false);
                }
                else
                {
                    if (Debugger.IsAttached)
                    {
                        LogToFileAddons.Parent_Log_Screen(1, "Debug Mode", "Enabled for Current Session");
                    }

                    Log.Start();
                    await Task.Run(() => Log_Location.RemoveLegacyLogs());

                    LogToFileAddons.Parent_Log_Screen(1, "CURRENT DATE", Time_Clock.GetTime(0));
                    LogToFileAddons.Parent_Log_Screen(2, "LAUNCHER MIGRATION", "Appdata and/or Roaming Folders");
                    /* Deletes Folders that will Crash the Launcher (Cleanup Migration) */
                    await Task.Run(() =>
                    {
                        try
                        {
                            if (!Directory.Exists(Locations.RoamingAppDataFolder_Launcher))
                            {
                                Directory.CreateDirectory(Locations.RoamingAppDataFolder_Launcher);
                            }
                            if (Directory.Exists(Path.Combine(Locations.LocalAppDataFolder, "Soapbox_Race_World")))
                            {
                                Directory.Delete(Path.Combine(Locations.LocalAppDataFolder, "Soapbox_Race_World"), true);
                            }
                            if (Directory.Exists(Path.Combine(Locations.RoamingAppDataFolder, "Soapbox_Race_World")))
                            {
                                Directory.Delete(Path.Combine(Locations.RoamingAppDataFolder, "Soapbox_Race_World"), true);
                            }
                            if (Directory.Exists(Path.Combine(Locations.LocalAppDataFolder, "SoapBoxRaceWorld")))
                            {
                                Directory.Delete(Path.Combine(Locations.LocalAppDataFolder, "SoapBoxRaceWorld"), true);
                            }
                            if (Directory.Exists(Path.Combine(Locations.RoamingAppDataFolder, "SoapBoxRaceWorld")))
                            {
                                Directory.Delete(Path.Combine(Locations.RoamingAppDataFolder, "SoapBoxRaceWorld"), true);
                            }
                            if (Directory.Exists(Path.Combine(Locations.LocalAppDataFolder, "WorldUnited.gg")))
                            {
                                Directory.Delete(Path.Combine(Locations.LocalAppDataFolder, "WorldUnited.gg"), true);
                            }
                            if (Directory.Exists(Path.Combine(Locations.RoamingAppDataFolder, "WorldUnited.gg")))
                            {
                                Directory.Delete(Path.Combine(Locations.RoamingAppDataFolder, "WorldUnited.gg"), true);
                            }
                            if (!Directory.Exists(Path.Combine(Locations.LauncherFolder, "Launcher_Data", "Archive", "Game Files")))
                            {
                                Directory.CreateDirectory(Path.Combine(Locations.LauncherFolder, "Launcher_Data", "Archive", "Game Files"));
                            }
                        }
                        catch (Exception Error)
                        {
                            LogToFileAddons.OpenLog("LAUNCHER MIGRATION", string.Empty, Error, string.Empty, true);
                            if (Error.InnerException != null && !string.IsNullOrWhiteSpace(Error.InnerException.Message))
                            {
                                LogToFileAddons.Parent_Log_Screen(5, "LAUNCHER MIGRATION", Error.InnerException.Message, false, true);
                            }
                        }
                        finally
                        {
                            LogToFileAddons.Parent_Log_Screen(3, "LAUNCHER MIGRATION", "Done");
                        }
                    });

                    LogToFileAddons.Parent_Log_Screen(2, "LAUNCHER XML", "If File Exists or Not");
                    Presence_Launcher.Status(0, "Checking if UserSettings XML Exists");
                    /* Create Default Configuration Files (if they don't already exist) */
                    await Task.Run(() =>
                    {
                        if (!File.Exists(Locations.UserSettingsXML))
                        {
                            try
                            {
                                if (!Directory.Exists(Locations.UserSettingsFolder))
                                {
                                    Directory.CreateDirectory(Locations.UserSettingsFolder);
                                }

                                File.WriteAllBytes(Locations.UserSettingsXML, Core.Extra.Conversion_.Embeded_Files.User_Settings_XML_Bytes());
                            }
                            catch (Exception Error)
                            {
                                LogToFileAddons.OpenLog("LAUNCHER XML", string.Empty, Error, string.Empty, true);
                            }
                        }

                        LogToFileAddons.Parent_Log_Screen(3, "LAUNCHER XML", "Done");
                    });

                    LogToFileAddons.Parent_Log_Screen(8,
                        BuildDevelopment.Allowed() ? "DEV TEST " : (BuildBeta.Allowed() ? "BETA TEST " : ""),
                        "SBRW.Launcher " + Application.ProductVersion + " - (" + BuildInformation.NumberOnly() + ")");

                    LogToFileAddons.Parent_Log_Screen(2, "OS", "Detecting");
                    Presence_Launcher.Status(0, "Checking Operating System");
                    await Task.Run(() =>
                    {
                        try
                        {
#if !(RELEASE_UNIX || DEBUG_UNIX)
                            LogToFileAddons.Parent_Log_Screen(7, "Detected OS", Launcher_Value.System_OS_Name = Product_Version.ConvertWindowsNumberToName());
                            LogToFileAddons.Parent_Log_Screen(7, "Windows Build", Product_Version.GetWindowsBuildNumber().ToString());
                            LogToFileAddons.Parent_Log_Screen(7, "NT Version", Environment.OSVersion.VersionString);
                            LogToFileAddons.Parent_Log_Screen(7, "Video Card", HardwareInfo.GPU.CardName());
                            LogToFileAddons.Parent_Log_Screen(7, "Driver Version", HardwareInfo.GPU.DriverVersion());
#else
                            LogToFileAddons.Parent_Log_Screen(7, "Detected OS", Launcher_Value.System_OS_Name = UnixOS.FullName());
                            LogToFileAddons.Parent_Log_Screen(7, "Wine Version", DLL_NTDLL.WineVersion());
                            LogToFileAddons.Parent_Log_Screen(7, "Wine Build ID", DLL_NTDLL.WineBuildId());
#endif
                            LogToFileAddons.Parent_Log_Screen(3, "OS", "Detected");
                        }
                        catch (Exception Error)
                        {
                            LogToFileAddons.OpenLog("SYSTEM", string.Empty, Error, string.Empty, true);
                            FunctionStatus.LauncherForceCloseReason = "Code: 0\n" + Translations.Database("Program_TextBox_System_Detection") + "\n" + Error.Message;
                            FunctionStatus.LauncherForceClose = true;
                            if (Error.InnerException != null && !string.IsNullOrWhiteSpace(Error.InnerException.Message))
                            {
                                LogToFileAddons.Parent_Log_Screen(5, "LAUNCHER XML", Error.InnerException.Message, false, true);
                            }
                        }
                    });

                    if (FunctionStatus.LauncherForceClose)
                    {
                        FunctionStatus.ErrorCloseLauncher("Closing From Operating System Check", false);
                    }
                    else
                    {
                        if (FunctionStatus.HasWriteAccessToFolder(Locations.LauncherFolder) == 0)
                        {
                            FunctionStatus.LauncherForceClose = true;
                            FunctionStatus.LauncherForceCloseReason = Translations.Database("Program_TextBox_Folder_Write_Test");
                            FunctionStatus.ErrorCloseLauncher("Closing From No Write Access", false);
                        }
                        else
                        {
                            LogToFileAddons.Parent_Log_Screen(3, "WRITE TEST", "Passed");
#if !(RELEASE_UNIX || DEBUG_UNIX)
                            /* Location Migration */
                            LogToFileAddons.Parent_Log_Screen(2, "Account File Migration", "Doing Migration");
                            Presence_Launcher.Status(0, "Doing Ini File Migration");
                            await Task.Run(() =>
                            {
                                if (File.Exists(Ini_Location.Name_Account_Ini))
                                {
                                    try
                                    {
                                        if (File.Exists(Ini_Location.Launcher_Account))
                                        {
                                            File.Move(Ini_Location.Launcher_Account,
                                                Path.Combine(Locations.RoamingAppDataFolder_Launcher, Time_Folder.DateAndTime() + "_" + Ini_Location.Name_Account_Ini));
                                        }

                                        File.Move(Ini_Location.Name_Account_Ini, Ini_Location.Launcher_Account);
                                    }
                                    catch (Exception Error)
                                    {
                                        LogToFileAddons.OpenLog("Account File Migration", string.Empty, Error, string.Empty, true);
                                        FunctionStatus.LauncherForceClose = true;
                                        if (Error.InnerException != null && !string.IsNullOrWhiteSpace(Error.InnerException.Message))
                                        {
                                            LogToFileAddons.Parent_Log_Screen(5, "Account File Migration", Error.InnerException.Message, false, true);
                                        }
                                    }
                                }
                                else
                                {
                                    LogToFileAddons.Parent_Log_Screen(3, "Account File Migration", "Already Migrated");
                                }
                            });

                            LogToFileAddons.Parent_Log_Screen(3, "Account File Migration", "Done");
#endif

                            if (FunctionStatus.LauncherForceClose)
                            {
                                ///@DavidCarbon or @Zacam - Remember to Translate This!
                                FunctionStatus.LauncherForceCloseReason = "Failed to Successfully Migrate Ini File(s)";
                                FunctionStatus.ErrorCloseLauncher("Closing Ini Migration", false);
                            }
                            else
                            {
                                LogToFileAddons.Parent_Log_Screen(1, "File Archive Path", "Checking Default Game Archive Locations");
                                await Task.Run(() =>
                                {
                                    try
                                    {
                                        if (Hashes.Hash_SHA(InformationCache.Secondary_Game_Archive_Path()) == "88C886B6D131C052365C3D6D14E14F67A4E2C253")
                                        {
                                            Save_Settings.Live_Data.Game_Archive_Location = InformationCache.Secondary_Game_Archive_Path();
                                        }
                                        else if (Hashes.Hash_SHA(InformationCache.Secondary_Game_Archive_Path_Old()) == "88C886B6D131C052365C3D6D14E14F67A4E2C253")
                                        {
                                            Save_Settings.Live_Data.Game_Archive_Location = InformationCache.Secondary_Game_Archive_Path_Old();
                                        }
                                        else if (Hashes.Hash_SHA(InformationCache.Legacy_Game_Archive_Path()) == "88C886B6D131C052365C3D6D14E14F67A4E2C253")
                                        {
                                            Save_Settings.Live_Data.Game_Archive_Location = InformationCache.Legacy_Game_Archive_Path();
                                        }

                                        if (!string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Game_Archive_Location))
                                        {
                                            LogToFileAddons.Parent_Log_Screen(1, "File Archive Path", "Using Pre-downloaded File: " + Save_Settings.Live_Data.Game_Archive_Location);
                                        }
                                    }
                                    catch (Exception Error)
                                    {
                                        LogToFileAddons.OpenLog("File Archive Path", string.Empty, Error, string.Empty, true);
                                        if (Error.InnerException != null && !string.IsNullOrWhiteSpace(Error.InnerException.Message))
                                        {
                                            LogToFileAddons.Parent_Log_Screen(5, "File Archive Path", Error.InnerException.Message, false, true);
                                        }
                                    }
                                });
                                LogToFileAddons.Parent_Log_Screen(2, "INI FILES", "Doing Nullsafe");
                                Presence_Launcher.Status(0, "Doing NullSafe ini Files");
                                await Task.Run(() =>
                                {
                                    Save_Settings.NullSafe();
                                    Save_Account.NullSafe();
                                });
                                LogToFileAddons.Parent_Log_Screen(3, "INI FILES", "Done");
                                /* Sets up Theming */
                                LogToFileAddons.Parent_Log_Screen(2, "LAUNCHER THEME", "Checking");
                                await Task.Run(() =>
                                {
                                    Theming.CheckIfThemeExists();
                                });
                                LogToFileAddons.Parent_Log_Screen(2, "LAUNCHER THEME", "Done");

                                LogToFileAddons.Parent_Log_Screen(12, "APPLICATION", "Setting Language");
                                CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo(Translations.UI(Translations.Application_Language = Save_Settings.Live_Data.Launcher_Language.ToLower(), true));
                                LogToFileAddons.Parent_Log_Screen(3, "APPLICATION", "Done Setting Language '" + Translations.UI(Translations.Application_Language) + "'");

                                /* Windows 7 TLS Check */
                                if (Product_Version.GetWindowsNumber() == 6.1)
                                {
                                    LogToFileAddons.Parent_Log_Screen(2, "SSL/TLS", "Windows 7 Detected");
                                    Presence_Launcher.Status(0, "Checking Windows 7 SSL/TLS");

                                    try
                                    {
                                        string MessageBoxPopupTLS = string.Empty;

                                        await Task.Run(() =>
                                        {
                                            if (string.IsNullOrWhiteSpace(Registry_Core.Read("DisabledByDefault", @"SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.2\Client")))
                                            {
                                                MessageBoxPopupTLS = Translations.Database("Program_TextBox_W7_TLS_P1") + "\n\n";

                                                MessageBoxPopupTLS += "- HKLM/SYSTEM/CurrentControlSet/Control/SecurityProviders\n  /SCHANNEL/Protocols/TLS 1.2/Client\n";
                                                MessageBoxPopupTLS += "- Value: DisabledByDefault -> 0\n\n";

                                                MessageBoxPopupTLS += Translations.Database("Program_TextBox_W7_TLS_P2") + "\n\n";
                                                MessageBoxPopupTLS += Translations.Database("Program_TextBox_W7_TLS_P3");

                                                /* There is only 'OK' Available because this IS Required */
                                                if (MessageBoxPopupTLS.Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                                                {
                                                    Registry_Core.Write("DisabledByDefault", 0x0,
                                                        @"SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.2\Client");
                                                    Translations.Database("Program_TextBox_W7_TLS_P4").Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }

                                                LogToFileAddons.Parent_Log_Screen(3, "SSL/TLS", "Added Registry Key");
                                            }
                                            else
                                            {
                                                LogToFileAddons.Parent_Log_Screen(3, "SSL/TLS", "Done");
                                            }
                                        });
                                    }
                                    catch (Exception Error)
                                    {
                                        LogToFileAddons.OpenLog("SSL/TLS", string.Empty, Error, string.Empty, true);
                                        if (Error.InnerException != null && !string.IsNullOrWhiteSpace(Error.InnerException.Message))
                                        {
                                            LogToFileAddons.Parent_Log_Screen(5, "SSL/TLS", Error.InnerException.Message, false, true);
                                        }
                                    }
                                }

                                /* Windows 7 HotFix Check */
                                if (Product_Version.GetWindowsNumber() == 6.1 && string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Win_7_Patches))
                                {
                                    LogToFileAddons.Parent_Log_Screen(2, "HotFixes", "Windows 7 Detected");
                                    Presence_Launcher.Status(0, "Checking Windows 7 HotFixes");

                                    try
                                    {
                                        if (!ManagementSearcher.GetInstalledHotFix("KB3020369") || !ManagementSearcher.GetInstalledHotFix("KB3125574"))
                                        {
                                            string MessageBoxPopupKB = string.Empty;
                                            MessageBoxPopupKB = Translations.Database("Program_TextBox_W7_KB_P1") + "\n";
                                            MessageBoxPopupKB += Translations.Database("Program_TextBox_W7_KB_P2") + "\n\n";

                                            if (!ManagementSearcher.GetInstalledHotFix("KB3020369"))
                                            {
                                                MessageBoxPopupKB += "- " + Translations.Database("Program_TextBox_W7_KB_P3") + " KB3020369\n";
                                            }

                                            if (!ManagementSearcher.GetInstalledHotFix("KB3125574"))
                                            {
                                                MessageBoxPopupKB += "- " + Translations.Database("Program_TextBox_W7_KB_P3") + " KB3125574\n";
                                            }
                                            MessageBoxPopupKB += "\n" + Translations.Database("Program_TextBox_W7_KB_P4") + "\n";

                                            if (MessageBoxPopupKB.Message_Box(MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                                            {
                                                /* Since it's Informational we just need to know if they clicked 'OK' */
                                                Save_Settings.Live_Data.Win_7_Patches = "1";
                                            }
                                            else
                                            {
                                                /* or if they clicked 'Cancel' */
                                                Save_Settings.Live_Data.Win_7_Patches = "0";
                                            }

                                            Save_Settings.Save();
                                        }

                                        LogToFileAddons.Parent_Log_Screen(3, "HotFixes", "Done");
                                    }
                                    catch (Exception Error)
                                    {
                                        LogToFileAddons.OpenLog("HotFixes", string.Empty, Error, string.Empty, true);
                                        if (Error.InnerException != null && !string.IsNullOrWhiteSpace(Error.InnerException.Message))
                                        {
                                            LogToFileAddons.Parent_Log_Screen(5, "HotFixes", Error.InnerException.Message, false, true);
                                        }
                                    }
                                }

                                try
                                {
                                    LogToFileAddons.Parent_Log_Screen(2, "FOLDER", "Launcher Data Folder");

                                    await Task.Run(() =>
                                    {
                                        if (!Directory.Exists(Locations.LauncherDataFolder))
                                        {
                                            Directory.CreateDirectory(Locations.LauncherDataFolder);
                                        }
                                    });
                                }
                                catch (Exception Error)
                                {
                                    LogToFileAddons.OpenLog("FOLDER Launcher Data", string.Empty, Error, string.Empty, true);
                                    if (Error.InnerException != null && !string.IsNullOrWhiteSpace(Error.InnerException.Message))
                                    {
                                        LogToFileAddons.Parent_Log_Screen(5, "FOLDER Launcher Data", Error.InnerException.Message, false, true);
                                    }
                                }
                                finally
                                {
                                    LogToFileAddons.Parent_Log_Screen(3, "FOLDER", "Launcher Data Done");
                                }

                                await Task.Run(() =>
                                {
                                    try
                                    {
                                        LogToFileAddons.Parent_Log_Screen(2, "JSON", "Servers File");

                                        if (File.Exists(Path.Combine(Locations.LauncherFolder, Locations.NameOldServersJSON)))
                                        {
                                            if (File.Exists(Locations.LauncherCustomServers))
                                            {
                                                File.Delete(Locations.LauncherCustomServers);
                                            }

                                            File.Move(Path.Combine(Locations.LauncherFolder, Locations.NameOldServersJSON),
                                                Locations.LauncherCustomServers);

                                            LogToFileAddons.Parent_Log_Screen(3, "FOLDER", "Renaming Servers File");
                                        }
#if !(RELEASE_UNIX || DEBUG_UNIX)
                                        else if (File.Exists(Path.Combine(Locations.LauncherFolder, Locations.NameNewServersJSON)))
                                        {
                                            File.Move(Path.Combine(Locations.LauncherFolder, Locations.NameNewServersJSON), Locations.LauncherCustomServers);
                                        }
#endif
                                        else if (!File.Exists(Locations.LauncherCustomServers))
                                        {
                                            try
                                            {
                                                File.WriteAllText(Locations.LauncherCustomServers, "[]");
                                                LogToFileAddons.Parent_Log_Screen(3, "FOLDER", "Created Servers File");
                                            }
                                            catch (Exception Error)
                                            {
                                                LogToFileAddons.OpenLog("JSON SERVER FILE", string.Empty, Error, string.Empty, true);
                                            }
                                        }
                                    }
                                    catch (Exception Error)
                                    {
                                        LogToFileAddons.OpenLog("JSON SERVER FILE", string.Empty, Error, string.Empty, true);
                                        if (Error.InnerException != null && !string.IsNullOrWhiteSpace(Error.InnerException.Message))
                                        {
                                            LogToFileAddons.Parent_Log_Screen(5, "JSON SERVER FILE", Error.InnerException.Message, false, true);
                                        }
                                    }
                                    finally
                                    {
                                        LogToFileAddons.Parent_Log_Screen(3, "FOLDER", "Done");
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

                                LogToFileAddons.Parent_Log_Screen(2, "PROXY", "Checking if Proxy Is Disabled from User Settings! It's value is " + Save_Settings.Live_Data.Launcher_Proxy);
                                LogToFileAddons.Parent_Log_Screen(2, "CLIENT", "Checking Alternative WebCalls, it's value is " + Save_Settings.Live_Data.Launcher_WebClient_Method);

                                LogToFileAddons.Parent_Log_Screen(2, "PRELOAD", "Headers");
                                await Task.Run(() => Custom_Header.Headers_WHC());
                                LogToFileAddons.Parent_Log_Screen(3, "PRELOAD", "Headers");
                                Presence_Launcher.Status(0, "Checking Root Certificate Authority");
                                await Task.Run(() => Certificate_Store.Latest());

                                LogToFileAddons.Parent_Log_Screen(1, "REDISTRIBUTABLE", "Moved to Function");
                                /* (Starts Function Chain) Check if Redistributable Packages are Installed */
                                Redistributable.Check();
                            }
                        }
                    }
                }
            }
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clock_Tick(object sender, EventArgs e)
        {
            if (e != null)
            {
                if ((FunctionStatus.LoadingComplete || FunctionStatus.LauncherForceClose || Clock_Tick_Theme_Update) && (Screen_Instance != null))
                {
                    if (Clock.Enabled)
                    {
                        Screen_Instance.SafeInvokeAction(() => Screen_Instance.Clock.Stop(), this);
                    }
                }
                else if ((PictureBox_Screen_Splash.BackgroundImage != Image_Other.Logo_Splash) && (Screen_Instance != null))
                {
                    Clock_Tick_Theme_Update = true;
                    Button_Close.SafeInvokeAction(() => Button_Close.BackgroundImage = Button_Close.Icon_Order(SVG_Icon.Cross, SVG_Color.White), this);
                    PictureBox_Screen_Splash.SafeInvokeAction(() => PictureBox_Screen_Splash.BackgroundImage = Image_Other.Logo_Splash, this);
                }

                GarbageCollections.Cleanup();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Button_Close_Click(object sender, EventArgs e)
        {
            if (FunctionStatus.LoadingComplete)
            {
                ClosingTasks();
            }
            else
            {
                FunctionStatus.LauncherForceClose = true;
            }

            /* Leave this here. Its to properly close the launcher from Visual Studio (And Close the Launcher a well) 
             * If the Boolen is true it will restart the Application
             */
            if (Launcher_Restart)
            {
                Application.Restart();
            }
            else if (Application.MessageLoop)
            {
                // WinForms Mode
                Application.Exit();
            }

            // If in Console Mode or if Form is Hidden and/or for Background Threads
            Environment.Exit(Environment.ExitCode);
        }
    }
}
