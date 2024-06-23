#region Usings
using Newtonsoft.Json;
using SBRW.Launcher.Core.Cache;
using SBRW.Launcher.Core.Discord.Reference_.List_;
using SBRW.Launcher.Core.Discord.RPC_;
using SBRW.Launcher.Core.Extension.Hash_;
using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.Core.Extension.String_;
using SBRW.Launcher.Core.Extension.Validation_;
using SBRW.Launcher.Core.Extra.File_;
using SBRW.Launcher.Core.Proxy.Nancy_;
using SBRW.Launcher.Core.Recommended.Process_;
using SBRW.Launcher.Core.Recommended.Time_;
using SBRW.Launcher.Core.Reference.Json_.Newtonsoft_;
using SBRW.Launcher.Core.Required.Anti_Cheat;
using SBRW.Launcher.Core.Theme;
using SBRW.Launcher.RunTime.LauncherCore.Client;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.Lists;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using SBRW.Launcher.RunTime.LauncherCore.ModNet;
using SBRW.Launcher.RunTime.LauncherCore.ModNet.JSON;
using SBRW.Launcher.RunTime.LauncherCore.Support;
using SBRW.Launcher.RunTime.SystemPlatform.Windows;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Cache;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SBRW.Launcher.Core.Extension.Web_;
using SBRW.Launcher.Core.Extension.Validation_.Json_.Newtonsoft_;
using SBRW.Launcher.App.UI_Forms.Parent_Screen;
using SBRW.Launcher.RunTime.SystemPlatform.Unix;
using SBRW.Launcher.Core.Extra.File_.Save_;
#endregion

namespace SBRW.Launcher.App.UI_Forms.Main_Screen
{
    partial class Screen_Main
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Server_Host_Url"></param>
        /// <param name="Ping_Timeout"></param>
        private void Server_Ping(string Server_Host_Url, int Ping_Timeout)
        {
            Ping? CheckMate = default;

            try
            {
                if (!(Disposing || IsDisposed))
                {
                    if (!Label_Client_Ping.Equals(string.Empty))
                    {
                        Label_Client_Ping.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrWhiteSpace(Server_Host_Url))
                {
                    Json_List_Server Cached_Server_GSI = Launcher_Value.Launcher_Select_Server_Data;

                    CheckMate = new Ping();
                    CheckMate.PingCompleted += (_sender, _e) =>
                    {
                        if (Cached_Server_GSI.Equals(Launcher_Value.Launcher_Select_Server_Data))
                        {
                            if (_e.Cancelled)
                            {
                                Log.Warning("SERVER PING: Ping Canceled for " + ServerListUpdater.ServerName("Ping"));
                            }
                            else if (_e.Error != null)
                            {
                                Log.Error("SERVER PING: Ping Failed for " + ServerListUpdater.ServerName("Ping") + " -> " + _e.Error.ToString());
                            }
                            else if (_e.Reply != null)
                            {
                                if (_e.Reply.Status == IPStatus.Success && ServerListUpdater.ServerName("Ping") != "Offline Built-In Server")
                                {
                                    if (!(Disposing || IsDisposed))
                                    {
                                        Label_Client_Ping.Text = string.Format("Your Ping to the Server \n{0}".ToUpper(), _e.Reply.RoundtripTime + "ms");
                                    }

                                    Log.Info("SERVER PING: " + _e.Reply.RoundtripTime + "ms for " + ServerListUpdater.ServerName("Ping"));
                                }
                                else
                                {
                                    Log.Warning("SERVER PING: " + ServerListUpdater.ServerName("Ping") + " is " + _e.Reply.Status);
                                }
                            }
                            else
                            {
                                Log.Warning("SERVER PING:  Unable to Ping " + ServerListUpdater.ServerName("Ping"));
                            }

                            if (_e.UserState != null)
                            {
#pragma warning disable CS8602 // Null Safe Check is done Above.
                                (_e.UserState as AutoResetEvent).Set();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                            }
                        }
                    };

                    if (Cached_Server_GSI.Equals(Launcher_Value.Launcher_Select_Server_Data))
                    {
                        CheckMate.SendAsync(Server_Host_Url,
                            (Launcher_Value.Launcher_WebCall_Timeout_Enable && (Launcher_Value.Launcher_WebCall_Timeout() > 0) ?
                                        (Launcher_Value.Launcher_WebCall_Timeout() * 1000) : Ping_Timeout), new byte[1], new PingOptions(30, true), new AutoResetEvent(false));
                    }
                }
            }
            catch (PingException Error)
            {
                LogToFileAddons.OpenLog("Pinging", string.Empty, Error, string.Empty, true);
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("Ping", string.Empty, Error, string.Empty, true);
            }
            finally
            {
                if (CheckMate != default)
                {
                    CheckMate.Dispose();
                    CheckMate = null;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Return_Value"></param>
        /// <returns></returns>
        private bool DisablePlayButton(bool Return_Value = false)
        {
            IsDownloading = false;
            Playenabled = false;

            return Return_Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="LoginToken"></param>
        private void Game_Bootup(string UserID, string LoginToken)
        {
            if (!(Disposing || IsDisposed))
            {
                if (InformationCache.SelectedServerEnforceProxy)
                {
                    if (!Proxy_Settings.Running())
                    {
                        Proxy_Server.Instance.Start("Start Game");
                    }
                }

                Launcher_Value.Launcher_Proxy = Proxy_Settings.Running();

                Nfswstarted = new Thread(() =>
                {
                    if (Proxy_Settings.Running())
                    {
                        Game_Live_Data(UserID, LoginToken, "http://127.0.0.1:" + Proxy_Settings.Port + "/nfsw/Engine.svc", this);
                    }
                    else
                    {
                        Uri convert = new Uri(Launcher_Value.Launcher_Select_Server_Data.IPAddress);

                        if (convert.Scheme == "http")
                        {
                            Match match = Regex.Match(convert.Host, @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
                            if (!match.Success)
                            {
                                Launcher_Value.Launcher_Select_Server_Data.IPAddress =
                                Launcher_Value.Launcher_Select_Server_Data.IPAddress.Replace(convert.Host, FunctionStatus.HostName2IP(convert.Host, !Save_Settings.Legacy_Host_To_IP()));
                            }
                        }

                        Game_Live_Data(UserID, LoginToken, Launcher_Value.Launcher_Select_Server_Data.IPAddress, this);
                    }
                })
                { IsBackground = true };

                Nfswstarted.Start();
                Presence_Launcher.Status(28, string.Empty);
            }
        }

        /// <summary>
        /// Launch game
        /// </summary>
        private void Game_Check_Launch()
        {
            if (!(Disposing || IsDisposed))
            {
                Presence_Launcher.Start(false, Presence_Launcher.ApplicationID());

                CurrentModFileCount = 0;
                TotalModFileCount = 0;

                if (ModFilesDownloadUrls != default)
                {
                    ModFilesDownloadUrls.Clear();
                }

                try
                {
                    if (UI_MODE != 12)
                    {
                        UI_MODE = 12;
                    }

                    string GameExePath = Path.Combine(Save_Settings.Live_Data.Game_Path, "nfsw.exe");
                    string GameExehash = Hashes.Hash_SHA(GameExePath);
                    if
                      (
                        GameExehash.Equals("7C0D6EE08EB1EDA67D5E5087DDA3762182CDE4AC") ||
                        GameExehash.Equals("DB9287FB7B0CDA237A5C3885DD47A9FFDAEE1C19") ||
                        GameExehash.Equals("E69890D31919DE1649D319956560269DB88B8F22") ||
                        GameExehash.Equals("3CBE3FAAFF00FAD84F78A2AFEA4FFFC78294EEA2")
                      )
                    {
                        Launcher_Value.Game_Server_Name = ServerListUpdater.ServerName("Proxy");
                        Launcher_Value.Game_Server_IP = Launcher_Value.Launcher_Select_Server_Data.IPAddress;

                        Launcher_Value.Game_User_ID = UserId;
                        Launcher_Value.Game_Server_IP_Host = new Uri(Launcher_Value.Launcher_Select_Server_Data.IPAddress).Host;

                        /* REMOVED MODNET FILE COMPLETION */

                        if (UI_MODE != 8)
                        {
                            UI_MODE = 8;
                        }

                        Game_Bootup(UserId, LoginToken);
                    }
                    else if (!File.Exists(GameExePath))
                    {
                        Display_Color_Icons(2);
                        if (!Save_Settings.Account_Manager())
                        {
                            Screen_Instance.Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Is_Email.Mask(Save_Account.Live_Data.User_Raw_Email)).ToUpper();
                        }
                        ("You do not have the Game Downloaded. " +
                            "Please Verify Game Files installation path.").Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Button_Login_Logout_Modes(true, true);
                        Display_Color_Icons();
                    }
                    else
                    {
                        Display_Color_Icons(2);
                        if (!Save_Settings.Account_Manager())
                        {
                            Screen_Instance.Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Is_Email.Mask(Save_Account.Live_Data.User_Raw_Email)).ToUpper();
                        }
                        ("Your NFSW.exe is Modified. Please Verify Game Files.").Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Button_Login_Logout_Modes(true, true);
                        Display_Color_Icons();
                    }
                }
                catch (Exception Error)
                {
                    Display_Color_Icons(2);
                    if (!Save_Settings.Account_Manager())
                    {
                        Screen_Instance.Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Is_Email.Mask(Save_Account.Live_Data.User_Raw_Email)).ToUpper();
                    }
                    LogToFileAddons.OpenLog("GAME LAUNCH", Error.Message, Error, "Error", false);
                    Button_Login_Logout_Modes(true, true);
                    Display_Color_Icons();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Live_Form"></param>
        /// <param name="Process_Exit_Code"></param>
        /// <param name="Process_ID"></param>
        /// <param name="Did_Game_Start"></param>
        /// <param name="Icon_Box_Art"></param>
        private void Launcher_Close_Check(Form Live_Form, int Process_Exit_Code, int Process_ID = 0, bool Did_Game_Start = false, MessageBoxIcon Icon_Box_Art = MessageBoxIcon.Asterisk)
        {
            Presence_Launcher.Start();
            /* The process is adorable like a puppy, Kill it (https://youtu.be/mY3sM0jtwaA) */
            Log.Core("LAUNCHER: Killing any left over Processes related to NFSW");

            try
            {
                if (Process_ID != 0)
                {
                    if (!Process.GetProcessById(NfswPid).HasExited)
                    {
                        if (!Process.GetProcessById(NfswPid).CloseMainWindow())
                        {
                            Process.GetProcessById(NfswPid).Kill();
                        }
                    }
                }
            }
            catch { }

            try
            {
                Process[] Its_The_Law = Process.GetProcessesByName("nfsw");
                if (Its_The_Law != null)
                {
                    if (Its_The_Law.Length > 0)
                    {
                        foreach (Process Papers_Please in Its_The_Law)
                        {
                            try
                            {
                                if (!Process.GetProcessById(Papers_Please.Id).HasExited)
                                {
                                    if (!Process.GetProcessById(Papers_Please.Id).CloseMainWindow())
                                    {
                                        Process.GetProcessById(Papers_Please.Id).Kill();
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }
            }
            catch { }

            Launcher_Value.Game_In_Event_Bug = FunctionStatus.LauncherBattlePass = false;

            if (Live_Action_Timer != null)
            {
                if (Live_Action_Timer.Enabled)
                {
                    Live_Action_Timer.Stop();
                }
            }

#if NETFRAMEWORK
            Nfswstarted?.Abort();
#endif
            string Error_Msg = NFSW.ErrorTranslation(Process_Exit_Code);

            if (Did_Game_Start && !string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Game_Path) && !FunctionStatus.LauncherBattlePass)
            {
                if (File.Exists(Path.Combine(Save_Settings.Live_Data.Game_Path, Locations.NameModLinks)))
                {
                    ModNetHandler.CleanLinks(Save_Settings.Live_Data.Game_Path);
                }
            }

            Live_Form.SafeEndInvokeAsyncCatch(Live_Form.SafeBeginInvokeActionAsync(Launcher_X_Form =>
            {
                UI_MODE = 14;

                DisableLogout = false;

                if (!Screen_Instance.DisposedForm())
                {
                    if (!Save_Settings.Account_Manager())
                    {
                        Screen_Instance.Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Is_Email.Mask(Save_Account.Live_Data.User_Raw_Email)).ToUpper();
                    }

                    Screen_Instance.Label_Download_Information.Text = Error_Msg.ToUpper();
                    Screen_Instance.Button_Play_OR_Update.Visible = false;
                }

                if (Did_Game_Start)
                {
                    Presence_Launcher.Status(0, "Game Closed with Error Code: " + Process_Exit_Code.ToString());
                    Log.Error("GAME CRASH [EXIT CODE]: " + Process_Exit_Code.ToString() + " HEX: (0x" + Process_Exit_Code.ToString("X") + ")" + " REASON: " + Error_Msg);

                    if (Screen_Instance != null)
                    {
                        Display_Color_Icons(3);
                    }
                }
                else
                {
                    Presence_Launcher.Status(0, "Game Failed to Launch");
                    Log.Core("LAUNCHER: Game failed to Launch. Forcing User to Login again.");

                    if (Screen_Instance != null)
                    {
                        Display_Color_Icons(3);
                    }
                }

                if (Error_Msg.Message_Box(MessageBoxButtons.OK, Icon_Box_Art) == DialogResult.OK)
                {
                    Display_Color_Icons(1);
                }
            }));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="LoginToken"></param>
        /// <param name="ServerIP"></param>
        /// <param name="Live_Form"></param>
        private void Game_Live_Data(string UserID, string LoginToken, string ServerIP, Form Live_Form)
        {
            if (!(Disposing || IsDisposed))
            {
                if (new Process_Start_Game() { AffinityMask = Game_Affinity_Range}.Initialize(
                    Save_Settings.Live_Data.Game_Path, ServerIP, LoginToken, UserID, 
                    Launcher_Value.Launcher_Select_Server_Data.ID.ToUpper(), !UnixOS.Detected(), "nfsw.exe") != null)
                {
                    /* Request a New Session */
                    Time_Window.Client_Session();
                    Session_Timer.Remaining = Launcher_Value.Launcher_Select_Server_JSON.Server_Session_Timer != 0 ? 
                        Launcher_Value.Launcher_Select_Server_JSON.Server_Session_Timer : 2 * 60 * 60;
                    FunctionStatus.LauncherBattlePass = Process_Start_Game.Live_Process.EnableRaisingEvents = true;
                    NfswPid = Process_Start_Game.Live_Process.Id;
                    Process_Start_Game.Live_Process.Exited += (Send, It) =>
                    {
                        NfswPid = 0;
                        int exitCode = Process_Start_Game.Live_Process.ExitCode;

                        FunctionStatus.LauncherBattlePass = false;

                        if (Launcher_Value.Game_In_Event_Bug)
                        {
                            if (AC_Core.Status)
                            {
                                exitCode = 2017;
#if NETFRAMEWORK
                                ContextMenu = new ContextMenu();
                                ContextMenu.MenuItems.Add(new MenuItem("Ezekiel was Here - Sent from Mars (C&T)", (b, n) =>
                                {
#if NETFRAMEWORK
                                    Process.Start("https://www.youtube.com/watch?v=T-AF81iBCi0");
#else
                                Process.Start("explorer.exe", "https://www.youtube.com/watch?v=T-AF81iBCi0");
#endif
                                }));
                                ContextMenu.MenuItems.Add("-");
                                if (Screen_Parent.Screen_Instance != null)
                                {
                                    ContextMenu.MenuItems.Add(new MenuItem("Close Launcher", Screen_Parent.Screen_Instance.Button_Close_Click));
                                }

                                NotifyIcon_Notification.ContextMenu = ContextMenu;
#endif
                            }
                            else
                            {
                                exitCode = 2137;
#if NETFRAMEWORK
                                ContextMenu = new ContextMenu();
                                ContextMenu.MenuItems.Add(new MenuItem("One more Minute", (b, n) =>
                                {
#if NETFRAMEWORK
                                    Process.Start("https://youtu.be/HNuOQlt1KEM");
#else
                                    Process.Start("explorer.exe", "https://youtu.be/HNuOQlt1KEM");
#endif
                                }));
                                ContextMenu.MenuItems.Add("-");
                                if (Screen_Parent.Screen_Instance != null)
                                {
                                    ContextMenu.MenuItems.Add(new MenuItem("Close Launcher", Screen_Parent.Screen_Instance.Button_Close_Click));
                                }

                                NotifyIcon_Notification.ContextMenu = ContextMenu;
#endif
                            }
                        }
                        if (exitCode == 0 && !Launcher_Value.Game_In_Event_Bug && AC_Core.Stop_Check())
                        {
                            Screen_Parent.Screen_Instance?.Button_Close_Click(new object(), new EventArgs());
                        }
                        else if (AC_Core.Stop_Check())
                        {
                            Launcher_Close_Check(Live_Form, exitCode, NfswPid, true, MessageBoxIcon.Error);
                        }
                    };

#if !(DEBUG_UNIX || RELEASE_UNIX)
                    /* Wait 60 Seconds */
                    bool Game_Fully_Loaded = Process_Start_Game.Live_Process.WaitForInputIdle(60000);

                    if (!Process_Start_Game.Live_Process.HasExited && Game_Fully_Loaded)
#else
                    /* REASON: Wine/Mono seems to cause issues when using WaitForInputIdle Function,
                     * so lets use the old method to check if the game started
                     * Thanks Wine/Mono - DavidCarbon
                     */
                    while (Process_Start_Game.Live_Process.MainWindowHandle == IntPtr.Zero && !Process_Start_Game.Live_Process.HasExited)
                    {
                        /* Loop Here until the game Window Appears */
                    }

                    if (!Process_Start_Game.Live_Process.HasExited)
#endif
                    {
                        Presence_Launcher.Status(28, string.Empty);

                        /* TIMER HERE */
                        Live_Action_Timer = new System.Timers.Timer();
                        Live_Action_Timer.Elapsed += new System.Timers.ElapsedEventHandler(Time_Window.ClockWork_Planet);
                        Time_Window.Session_Alert += (x, D_Live_Events) =>
                        {
                            if (D_Live_Events != null)
                            {
                                try
                                {
                                    if (NotifyIcon_Notification != default)
                                    {
                                        NotifyIcon_Notification.Visible = D_Live_Events.Valid;
                                        NotifyIcon_Notification.BalloonTipIcon = ToolTipIcon.Info;
                                        NotifyIcon_Notification.BalloonTipTitle = "Force Restart - " + Launcher_Value.Game_Server_Name;
                                        NotifyIcon_Notification.BalloonTipText = "Game will shutdown by " + (D_Live_Events.Session_End_Time ?? DateTime.Now.AddMinutes(5)).ToString("t") + ". Please restart it manually before the launcher does it.";
                                        NotifyIcon_Notification.ShowBalloonTip(TimeSpan.FromMinutes(2).Seconds);
                                        NotifyIcon_Notification.BalloonTipClicked += (x, D_Live_Events) =>
                                        {
                                            return;
                                        };
                                        NotifyIcon_Notification.BalloonTipClosed += (x, D_Live_Events) =>
                                        {
                                            return;
                                        };
                                    }
                                }
                                catch (Exception Error)
                                {
                                    LogToFileAddons.OpenLog("NotifyIcon_Notification Timer", string.Empty, Error, "Error", true);
                                }
                            }
                        };

                        /* 0 = Static Timer, 1 = Dynamic Timer, 2 = No Timer */
                        if (Save_Settings.Live_Data.Launcher_Display_Timer == "1")
                        {
                            Time_Window.Timer_Dynamic = true;
                        }
                        else if (Save_Settings.Live_Data.Launcher_Display_Timer == "2")
                        {
                            /* Notes: This actually does not Display Timers on the Title Window and 'Time_Window.Live_Stream' will be renamed in the future */
                            Time_Window.Timer_None = true;
                        }
                        else
                        {
                            Time_Window.Timer_None = Time_Window.Timer_Dynamic = false;
                        }

                        Live_Action_Timer.Interval = 30000;
                        Live_Action_Timer.Enabled = true;

#if NETFRAMEWORK
                        ContextMenu = new ContextMenu();
                        ContextMenu.MenuItems.Add(new MenuItem("Running Out of Time", (b, n) =>
                        {
#if NETFRAMEWORK
                            Process.Start("https://youtu.be/vq9-bmoI-RI");
#else
                        Process.Start("explorer.exe", "https://youtu.be/vq9-bmoI-RI");
#endif
                        }));
#if EXPERIMENTAL
                        ContextMenu.MenuItems.Add(new MenuItem("Borderless", (b, n) =>
                        {
                            var hWnd = Process_Start_Game.Live_Process.MainWindowHandle;
                            // Get the current window style
                            int currentStyle = GetWindowLong(hWnd, GWL_STYLE);
                            //int currentExStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
                            // Remove border and caption from the window style
                            int newStyle = currentStyle & ~(WS_BORDER | WS_CAPTION | WS_SYSMENU | WS_EX_WINDOWEDGE);
                            // Remove window edge from extended window style
                            //int newExStyle = currentExStyle & ~WS_EX_WINDOWEDGE;
                            // Add WS_EX_APPWINDOW to show the window in the taskbar (optional)
                            //newExStyle |= WS_EX_APPWINDOW;
                            // Set the new window style
                            SetWindowLongPtr(hWnd, GWL_STYLE, (IntPtr)newStyle);
                            //SetWindowLongPtr(hWnd, GWL_EXSTYLE, (IntPtr)newExStyle);

                            if (DialogResult.Yes.Equals(MessageBox.Show("", "", MessageBoxButtons.YesNo)))
                            {
                                Rectangle screenInfo = System.Windows.Forms.Screen.FromHandle(hWnd).Bounds;
                                Log.Debug(string.Format("Screen: Width -> {0} Height -> {1}", screenInfo.Width, screenInfo.Height));
                                /* Set Screen Size of Window */
                                SetWindowPos(hWnd, SpecialWindowHandles.HWND_TOP, 0, 0, screenInfo.Width, screenInfo.Height, SetWindowPosFlags.SWP_FRAMECHANGED | SetWindowPosFlags.SWP_NOMOVE);
                                
                                RECT rect;
                                bool locationLookupSucceeded = GetWindowRect(hWnd, out rect);
                                if (locationLookupSucceeded)
                                {
                                    Point pt = new Point(((screenInfo.Left + screenInfo.Width) / 2) - ((rect.Right - rect.Left) / 2), ((screenInfo.Top + screenInfo.Height) / 2) - ((rect.Bottom - rect.Top) / 2));
                                    Log.Debug(string.Format("Center: X -> {0}, Y -> {1}", pt.X, pt.Y));
                                    /* Set Window to Center of Whole Screen 
                                    * Can not be combined with the Screen size otherwise its broken */
                                    SetWindowPos(hWnd, SpecialWindowHandles.HWND_TOP, pt.X, pt.Y, 0, 0, SetWindowPosFlags.SWP_FRAMECHANGED | SetWindowPosFlags.SWP_NOZORDER | SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_SHOWWINDOW);
                                    //MessageBox.Show(string.Format("  Position: {0},{1}", screenWidth, screenHeight));
                                }
                            }
                        }));
#endif
                        ContextMenu.MenuItems.Add("-");
                        if (Screen_Parent.Screen_Instance != null)
                        {
                            ContextMenu.MenuItems.Add(new MenuItem("Close Game and Launcher", Screen_Parent.Screen_Instance.Button_Close_Click));
                        }

                        NotifyIcon_Notification.ContextMenu = ContextMenu;
#endif
                        UI_MODE = 13;
                        Log.Core("LAUNCHER: Game has Fully Launched, Minimized Launcher");
                    }
                    else if (FunctionStatus.LauncherBattlePass)
                    {
                        Launcher_Close_Check(Live_Form, 2020, NfswPid, true, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void PlayButton_Click(object sender, EventArgs e)
        {
#if !(RELEASE_UNIX || DEBUG_UNIX)
            DriveInfo driveInfo = new DriveInfo(Save_Settings.Live_Data.Game_Path);

            if (!string.Equals(driveInfo.DriveFormat, "NTFS", StringComparison.InvariantCultureIgnoreCase))
            {
                Picture_Information_Window.Image = Image_Other.Information_Window_Error;
                if (!Save_Settings.Account_Manager())
                {
                    Screen_Instance.Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Is_Email.Mask(Save_Account.Live_Data.User_Raw_Email)).ToUpper();
                }
                ($"Playing the game on a non-NTFS-formatted drive is not supported." +
                    $"\nDrive '{driveInfo.Name}' is formatted with: {driveInfo.DriveFormat}").Message_Box(MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Picture_Information_Window.Image = Image_Other.Information_Window_Success;
                if (!Save_Settings.Account_Manager())
                {
                    Screen_Instance.Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Save_Account.Live_Data.User_Raw_Email).ToUpper();
                }
                return;
            }
#endif

            if (Save_Settings.Live_Data.Game_Integrity == "Ignore")
            {
                Display_Color_Icons(3);
            }
            else if (Save_Settings.Live_Data.Game_Integrity != "Good")
            {
                Display_Color_Icons(3);
                if (!Save_Settings.Account_Manager())
                {
                    Screen_Instance.Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Is_Email.Mask(Save_Account.Live_Data.User_Raw_Email)).ToUpper();
                }
                ("GameLauncher has detected a GameFiles Integrity Error" +
                    "\nPlease 'Verify GameFiles' in the Settings Screen").Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Display_Color_Icons();
                if (!Save_Settings.Account_Manager())
                {
                    Screen_Instance.Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Save_Account.Live_Data.User_Raw_Email).ToUpper();
                }
                return;
            }

            if (!Redistributable.Error_Free)
            {
                Picture_Information_Window.Image = Image_Other.Information_Window_Error;
                if (!Save_Settings.Account_Manager())
                {
                    Screen_Instance.Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Is_Email.Mask(Save_Account.Live_Data.User_Raw_Email)).ToUpper();
                }
                ("GameLauncher has detected that the 2015-2019 (or newer) VC++ Redistributable Package is not installed or may be damaged\n" +
                    "Please manually Install or Repair the Packages for your Operating System").Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Error);
                Picture_Information_Window.Image = Image_Other.Information_Window_Success;
                if (!Save_Settings.Account_Manager())
                {
                    Screen_Instance.Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Save_Account.Live_Data.User_Raw_Email).ToUpper();
                }
                return;
            }

            if (File.Exists(Path.Combine(Save_Settings.Live_Data.Game_Path, Locations.NameModLinks)))
            {
                try
                {
                    File.Delete(Path.Combine(Save_Settings.Live_Data.Game_Path, Locations.NameModLinks));
                }
                catch { }
            }

            Button_Login_Logout_Modes();
            Display_Color_Icons(1, false);

            ModNetHandler.FileANDFolder(Save_Settings.Live_Data.Game_Path);
            Log.Core("LAUNCHER: Installing ModNet");
            Label_Download_Information.Text = ("Detecting ModNet Support for " + ServerListUpdater.ServerName("ModNet")).ToUpper();

            if (ModNetHandler.Supported())
            {
                /* Caches (In Order of Excution) */
                string ModulesJSON = string.Empty;
                string ServerModInfo = string.Empty;
                GetModInfo? json2 = null;
                string remoteCarsFile = string.Empty;
                string remoteEventsFile = string.Empty;
                string ServerModListJSON = string.Empty;
                ServerModList? json3 = null;

                try
                {
                    Presence_Launcher.Status(5);
                    /* Get Remote ModNet list to process for checking required ModNet files are present and current */
                    Uri ModNetURI = new Uri(URLs.ModNet + "/launcher-modules/modules.json");
                    ServicePointManager.FindServicePoint(ModNetURI).ConnectionLeaseTimeout = (int)TimeSpan.FromSeconds(Launcher_Value.Launcher_WebCall_Timeout_Enable ?
                                    Launcher_Value.Launcher_WebCall_Timeout() : 60).TotalMilliseconds;
                    var ModNetJsonURI = new WebClient
                    {
                        Encoding = Encoding.UTF8,
                        CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
                    };
                    if (!Launcher_Value.Launcher_Alternative_Webcalls())
                    {
                        ModNetJsonURI = new WebClientWithTimeout { Encoding = Encoding.UTF8, CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore) };
                    }
                    else
                    {
                        ModNetJsonURI.Headers.Add("user-agent", "SBRW Launcher " +
                        Application.ProductVersion + " (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)");
                    }

                    /* ModNet Cache File Comparison */
                    DateTime Time_Check = DateTime.Now.Date;
                    string Launcher_Data_Folder = Path.Combine("Launcher_Data", "JSON", "ModNet");
                    string Time_Stamp = Path.Combine(Launcher_Data_Folder, "Time_Stamp.txt");
                    string Server_List_Cache = Path.Combine(Launcher_Data_Folder, "Modules.json");
                    bool ModNet_Offline = false;

                    if (File.Exists(Time_Stamp))
                    {
                        try
                        {
                            Time_Check = DateTime.Parse(File.ReadLines(Time_Stamp).First()).Date;
                        }
                        catch
                        {

                        }
                    }

                    try
                    {
                        try
                        {
                            ModulesJSON = ModNetJsonURI.DownloadString(ModNetURI);
                            Label_Download_Information.Text = "JSON: Retrieved ModNet Files Information".ToUpper();
                        }
                        catch
                        {
                            ModNet_Offline = true;
                            if (File.Exists(Server_List_Cache))
                            {
                                Log.Warning("MODNET FILE CACHE: Found");

                                bool Allow_ModNet_Cache = false;
                                if (Time_Check < DateTime.Now.Date)
                                {
                                    Display_Color_Icons(3);
                                    if (!Save_Settings.Account_Manager())
                                    {
                                        Screen_Instance.Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Is_Email.Mask(Save_Account.Live_Data.User_Raw_Email)).ToUpper();
                                    }
                                    if (("Launcher has found a ModNet Cache File." +
                                        "\nHowever, its a day old." +
                                        "\nWould you like to use the old File Cache?").Message_Box(MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                    {
                                        Allow_ModNet_Cache = true;
                                    }
                                    Display_Color_Icons();
                                    if (!Save_Settings.Account_Manager())
                                    {
                                        Screen_Instance.Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Save_Account.Live_Data.User_Raw_Email).ToUpper();
                                    }
                                }
                                else
                                {
                                    Allow_ModNet_Cache = true;
                                }

                                if (Allow_ModNet_Cache)
                                {
                                    try
                                    {
                                        Log.Warning("MODNET FILE CACHE: Loading");
                                        ModulesJSON = File.ReadAllText(Server_List_Cache);
                                        Log.Warning("MODNET FILE CACHE: Loaded");
                                    }
                                    catch
                                    {
                                        throw;
                                    }
                                }
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
                    catch (Exception Error)
                    {
                        Display_Color_Icons(2);
                        Label_Download_Information.Text = ("JSON: Unable to Retrieve ModNet Files Information").ToUpper();
                        Presence_Launcher.Status(8);
                        if (!Save_Settings.Account_Manager())
                        {
                            Screen_Instance.Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Is_Email.Mask(Save_Account.Live_Data.User_Raw_Email)).ToUpper();
                        }
                        string LogMessage = "There was an error with ModNet JSON Retrieval:";
                        LogToFileAddons.OpenLog("MODNET FILES", LogMessage, Error, "Error", false);
                        Button_Login_Logout_Modes(true, true);
                        Display_Color_Icons();
                    }
                    finally
                    {
                        if (ModNetJsonURI != null)
                        {
                            ModNetJsonURI.Dispose();
                        }
                    }

                    if (string.IsNullOrWhiteSpace(ModulesJSON) || !ModulesJSON.Valid_Json())
                    {
                        Display_Color_Icons(2);
                        Label_Download_Information.Text = ("JSON: Invalid ModNet Files Information").ToUpper();
                        Presence_Launcher.Status(8);
                        if (!Save_Settings.Account_Manager())
                        {
                            Screen_Instance.Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Is_Email.Mask(Save_Account.Live_Data.User_Raw_Email)).ToUpper();
                        }
                        Button_Login_Logout_Modes(true, true);
                        Display_Color_Icons();
                        ModulesJSON = string.Empty;
                        return;
                    }
                    else
                    {
                        try
                        {
                            if (!ModNet_Offline)
                            {
                                await Task.Run(() =>
                                {
                                    try
                                    {
                                        if ((Time_Check < DateTime.Now.Date) || !File.Exists(Time_Stamp))
                                        {
                                            if (!Directory.Exists(Launcher_Data_Folder))
                                            {
                                                Directory.CreateDirectory(Launcher_Data_Folder);
                                            }

                                            File.WriteAllText(Server_List_Cache, ModulesJSON);
                                            File.WriteAllText(Time_Stamp, DateTime.Now.ToString());
                                        }
                                    }
                                    catch { }
                                });
                            }

                            Label_Download_Information.Text = ("ModNet: Checking Local Files. This may take awhile.").ToUpper();

                            string[] modules_newlines = ModulesJSON.Split(new string[] { "\n" }, StringSplitOptions.None);
                            foreach (string modules_newline in modules_newlines)
                            {
                                if (modules_newline.Trim().ToStringInvariant() == "{" || modules_newline.Trim().ToStringInvariant() == "}")
                                {
                                    continue;
                                }

                                string trim_modules_newline = modules_newline.Trim().ToStringInvariant();
                                string[] modules_files = trim_modules_newline.Split(new char[] { ':' });

                                string ModNetList = modules_files[0].Replace("\"", "").Trim().ToStringInvariant();
                                string ModNetSHA = modules_files[1].Replace("\"", "").Replace(",", "").Trim().ToLowerInvariant();

                                string ModNetFilePath = Path.Combine(Save_Settings.Live_Data.Game_Path, ModNetList);
                                string ModNetLocalFileHash = string.Empty;

                                await Task.Run(() =>
                                {
                                    ModNetLocalFileHash = Hashes.Hash_SHA256(ModNetFilePath).ToLowerInvariant();
                                });

                                if (!ModNetLocalFileHash.Equals(ModNetSHA) || !File.Exists(ModNetFilePath))
                                {
                                    Label_Download_Information.Text = ("ModNet: Downloading " + ModNetList).ToUpper();

                                    Log.Warning("MODNET CORE: " + ModNetList + " Does not match SHA Hash on File Server -> Online Hash: '" + ModNetSHA + "'");

                                    if (File.Exists(ModNetFilePath))
                                    {
                                        File.Delete(ModNetFilePath);
                                    }

                                    Presence_Launcher.Status(7, ModNetList);

                                    Uri URLCall = new Uri(URLs.ModNet + "/launcher-modules/" + ModNetList);
                                    ServicePointManager.FindServicePoint(URLCall).ConnectionLeaseTimeout = (int)TimeSpan.FromSeconds(Launcher_Value.Launcher_WebCall_Timeout_Enable ?
                                    Launcher_Value.Launcher_WebCall_Timeout() : 60).TotalMilliseconds;
                                    var newModNetFilesDownload = new WebClient
                                    {
                                        Encoding = Encoding.UTF8,
                                        CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
                                    };
                                    if (!Launcher_Value.Launcher_Alternative_Webcalls())
                                    {
                                        newModNetFilesDownload = new WebClientWithTimeout { Encoding = Encoding.UTF8, CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore) };
                                    }
                                    else
                                    {
                                        newModNetFilesDownload.Headers.Add("user-agent", "SBRW Launcher " +
                                        Application.ProductVersion + " (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)");
                                    }
                                    newModNetFilesDownload.DownloadFile(URLCall, ModNetFilePath);
                                }
                                else
                                {
                                    Label_Download_Information.Text = ("ModNet: Up to Date " + ModNetList).ToUpper();
                                    Log.Info("MODNET CORE: " + ModNetList + " Is Up to Date!");
                                }
                            }
                        }
                        catch (Exception Error)
                        {
                            Display_Color_Icons(2);
                            Presence_Launcher.Status(8);
                            if (!Save_Settings.Account_Manager())
                            {
                                Screen_Instance.Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Is_Email.Mask(Save_Account.Live_Data.User_Raw_Email)).ToUpper();
                            }
                            string LogMessage = "There was an error with ModNet Files Check:";
                            LogToFileAddons.OpenLog("MODNET CORE", LogMessage, Error, "Error", false);
                            Button_Login_Logout_Modes(true, true);
                            Display_Color_Icons();

                            return;
                        }
                        finally
                        {
                            if (!string.IsNullOrWhiteSpace(ModulesJSON))
                            {
                                ModulesJSON = string.Empty;
                            }
                        }

                        Server_Ping(Launcher_Value.Launcher_Select_Server_Data.IPAddress, 5000);

                        Uri newModNetUri = new Uri(Launcher_Value.Launcher_Select_Server_Data.IPAddress + "/Modding/GetModInfo");
                        ServicePointManager.FindServicePoint(newModNetUri).ConnectionLeaseTimeout = (int)TimeSpan.FromSeconds(Launcher_Value.Launcher_WebCall_Timeout_Enable ?
                                    Launcher_Value.Launcher_WebCall_Timeout() : 60).TotalMilliseconds;
                        var ModInfoJson = new WebClient
                        {
                            Encoding = Encoding.UTF8,
                            CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
                        };
                        if (!Launcher_Value.Launcher_Alternative_Webcalls())
                        {
                            ModInfoJson = new WebClientWithTimeout { Encoding = Encoding.UTF8, CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore) };
                        }
                        else
                        {
                            ModInfoJson.Headers.Add("user-agent", "SBRW Launcher " +
                            Application.ProductVersion + " (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)");
                        }

                        try
                        {
                            ServerModInfo = ModInfoJson.DownloadString(newModNetUri);
                            Label_Download_Information.Text = ("JSON: Retrieved Server Mod Information").ToUpper();
                        }
                        catch (Exception Error)
                        {
                            Label_Download_Information.Text = ("JSON: Unable to Retrieve Server Mod Information").ToUpper();
                            Presence_Launcher.Status(10);
                            if (!Save_Settings.Account_Manager())
                            {
                                Screen_Instance.Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Is_Email.Mask(Save_Account.Live_Data.User_Raw_Email)).ToUpper();
                            }
                            string LogMessage = "There was an error with Server Mod Information Retrieval:";
                            LogToFileAddons.OpenLog("SERVER MOD INFO", LogMessage, Error, "Error", false);
                            Button_Login_Logout_Modes(true, true);
                            Display_Color_Icons();
                        }
                        finally
                        {
                            ModInfoJson?.Dispose();
                        }

                        if (string.IsNullOrWhiteSpace(ServerModInfo) || !ServerModInfo.Valid_Json())
                        {
                            Display_Color_Icons(2);
                            Label_Download_Information.Text = ("JSON: Invalid Server Mod Information").ToUpper();
                            Presence_Launcher.Status(10);
                            if (!Save_Settings.Account_Manager())
                            {
                                Screen_Instance.Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Is_Email.Mask(Save_Account.Live_Data.User_Raw_Email)).ToUpper();
                            }
                            Button_Login_Logout_Modes(true, true);
                            Display_Color_Icons();
                            ServerModInfo = string.Empty;
                            return;
                        }
                        else
                        {
                            /* get files now */
                            json2 = JsonConvert.DeserializeObject<GetModInfo>(ServerModInfo);
                            ServerModInfo = string.Empty;

                            /* Set and Get for RemoteRPC Files */
#pragma warning disable CS8602 // Null Safe Check Done Before This Section
                            Uri URLCall_A = new Uri(json2.basePath + "/cars.json");
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                            ServicePointManager.FindServicePoint(URLCall_A).ConnectionLeaseTimeout = (int)TimeSpan.FromSeconds(Launcher_Value.Launcher_WebCall_Timeout_Enable ?
                                    Launcher_Value.Launcher_WebCall_Timeout() : 60).TotalMilliseconds;
                            var CarsJson = new WebClient
                            {
                                Encoding = Encoding.UTF8,
                                CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
                            };
                            if (!Launcher_Value.Launcher_Alternative_Webcalls())
                            {
                                CarsJson = new WebClientWithTimeout { Encoding = Encoding.UTF8, CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore) };
                            }
                            else
                            {
                                CarsJson.Headers.Add("user-agent", "SBRW Launcher " +
                                Application.ProductVersion + " (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)");
                            }

                            try
                            {
                                remoteCarsFile = CarsJson.DownloadString(URLCall_A);
                            }
                            catch { }
                            finally
                            {
                                CarsJson?.Dispose();
                            }

                            Uri URLCall_B = new Uri(json2.basePath + "/events.json");
                            ServicePointManager.FindServicePoint(URLCall_B).ConnectionLeaseTimeout = (int)TimeSpan.FromSeconds(Launcher_Value.Launcher_WebCall_Timeout_Enable ?
                                    Launcher_Value.Launcher_WebCall_Timeout() : 60).TotalMilliseconds;
                            var EventsJson = new WebClient
                            {
                                Encoding = Encoding.UTF8,
                                CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
                            };
                            if (!Launcher_Value.Launcher_Alternative_Webcalls())
                            {
                                EventsJson = new WebClientWithTimeout { Encoding = Encoding.UTF8, CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore) };
                            }
                            else
                            {
                                EventsJson.Headers.Add("user-agent", "SBRW Launcher " +
                                Application.ProductVersion + " (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)");
                            }

                            try
                            {
                                remoteEventsFile = EventsJson.DownloadString(URLCall_B);
                            }
                            catch { }
                            finally
                            {
                                EventsJson?.Dispose();
                            }

                            /* Version 1.3 @metonator - DavidCarbon */
                            if (remoteCarsFile.Valid_Json())
                            {
                                Log.Info("DISCORD: Found RemoteRPC List for cars.json");
                                Cars.List_File = remoteCarsFile;
                                remoteCarsFile = string.Empty;
                            }
                            else
                            {
                                Log.Warning("DISCORD: RemoteRPC list for cars.json does not exist");
                                Cars.List_File = string.Empty;
                            }

                            if (remoteEventsFile.Valid_Json())
                            {
                                Log.Info("DISCORD: Found RemoteRPC List for events.json");
                                SBRW.Launcher.Core.Discord.Reference_.List_.Events.List_File = remoteEventsFile;
                                remoteEventsFile = string.Empty;
                            }
                            else
                            {
                                Log.Warning("DISCORD: RemoteRPC list for events.json does not exist");
                                SBRW.Launcher.Core.Discord.Reference_.List_.Events.List_File = string.Empty;
                            }

                            Log.Core("CORE: Loading Server Mods List");
                            /* Get Server Mod Index */
                            Uri newIndexFile = new Uri(json2.basePath + "/index.json");
                            ServicePointManager.FindServicePoint(newIndexFile).ConnectionLeaseTimeout = (int)TimeSpan.FromSeconds(Launcher_Value.Launcher_WebCall_Timeout_Enable ?
                                    Launcher_Value.Launcher_WebCall_Timeout() : 60).TotalMilliseconds;
                            var ServerModsList = new WebClient
                            {
                                Encoding = Encoding.UTF8,
                                CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
                            };
                            if (!Launcher_Value.Launcher_Alternative_Webcalls())
                            {
                                ServerModsList = new WebClientWithTimeout { Encoding = Encoding.UTF8, CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore) };
                            }
                            else
                            {
                                ServerModsList.Headers.Add("user-agent", "SBRW Launcher " +
                                Application.ProductVersion + " (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)");
                            }

                            try
                            {
                                Log.Core("CORE: Retrieved Server Mods List");
                                ServerModListJSON = ServerModsList.DownloadString(newIndexFile);
                                Label_Download_Information.Text = ("JSON: Retrieved Server Mod List Information").ToUpper();
                            }
                            catch (Exception Error)
                            {
                                Display_Color_Icons(2);
                                Label_Download_Information.Text = ("JSON: Unable to Retrieve Server Mod List Information").ToUpper();
                                Presence_Launcher.Status(10);
                                if (!Save_Settings.Account_Manager())
                                {
                                    Screen_Instance.Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Is_Email.Mask(Save_Account.Live_Data.User_Raw_Email)).ToUpper();
                                }
                                string LogMessage = "There was an error with Server Mod List Information Retrieval:";
                                LogToFileAddons.OpenLog("SERVER MOD JSON", LogMessage, Error, "Error", false);
                                Button_Login_Logout_Modes(true, true);
                                Display_Color_Icons();
                            }
                            finally
                            {
                                ServerModsList?.Dispose();
                            }

                            if (string.IsNullOrWhiteSpace(ServerModListJSON) || !ServerModListJSON.Valid_Json())
                            {
                                Display_Color_Icons(2);
                                Label_Download_Information.Text = ("JSON: Invalid Server Mod List Information").ToUpper();
                                Presence_Launcher.Status(10, null);
                                if (!Save_Settings.Account_Manager())
                                {
                                    Screen_Instance.Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Is_Email.Mask(Save_Account.Live_Data.User_Raw_Email)).ToUpper();
                                }
                                Button_Login_Logout_Modes(true, true);
                                Display_Color_Icons();
                                ServerModListJSON = string.Empty;
                                return;
                            }
                            else
                            {
                                try
                                {
                                    Label_Download_Information.Text = ("Server Mods: Folder & File Check").ToUpper();
                                    json3 = JsonConvert.DeserializeObject<ServerModList>(ServerModListJSON);
                                    ServerModListJSON = string.Empty;
                                    string ModFolderCache = Path.Combine(Save_Settings.Live_Data.Game_Path, "MODS", json2.serverID.Hash_String(0).ToLower());
                                    if (!Directory.Exists(ModFolderCache))
                                    {
                                        Directory.CreateDirectory(ModFolderCache);
                                    }

                                    string[] Directory_Cache_List_Files = new string[] { };

                                    await Task.Run(() =>
                                    {
                                        Directory_Cache_List_Files = Directory.GetFiles(ModFolderCache);
                                    });

                                    if (Directory_Cache_List_Files.Length > 0)
                                    {
                                        /* (FILENAME.mods) 
                                     * Checks for any Files that Don't match the Server Index Json and Removes that File  */
                                        foreach (string file in Directory_Cache_List_Files)
                                        {
                                            string name = Path.GetFileName(file);

#pragma warning disable CS8602 // Null Safe Check Done Before This Section
                                            if (json3.entries.All(en => en.Name != name))
                                            {
                                                try
                                                {
                                                    File.Delete(file);
                                                    Log.Core("LAUNCHER: Removed Stale Mod Package: " + file);
                                                }
                                                catch (Exception Error)
                                                {
                                                    LogToFileAddons.OpenLog("SERVER MOD CACHE", string.Empty, Error, string.Empty, true);
                                                }
                                            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                                        }
                                    }

                                    Label_Download_Information.Text = ("Server Mods: Folder & File Check").ToUpper();
                                    /* (OLD-FILENAME.mods != NEW-FILENAME.mods)
                                     * Checks for the file and if the File Hash does not match it will be added to a list to be downloaded 
                                     * If a file exists and doesn't match a the server provided index json it will be deleted 
                                     * 5/22/2021: If a Server Extracted Mods Directory is present and 
                                     * if a Server Mod File no longer matches it will now delete the folder (.data/SERVER-ID-HASH) - DavidCarbon
                                     */
                                    int ExtractedServerFolderRunTime = 0;

#pragma warning disable CS8602 // Null Safe Check Done Before This Section
                                    foreach (ServerModFileEntry modfile in json3.entries)
                                    {
                                        string ModCachedFile = Path.Combine(ModFolderCache, modfile.Name);
                                        string Mod_File_Hash_Local = string.Empty;

                                        await Task.Run(() =>
                                        {
                                            Mod_File_Hash_Local = Hashes.Hash_SHA(ModCachedFile).ToLowerInvariant();
                                        });


                                        if (!Mod_File_Hash_Local.Equals(modfile.Checksum))
                                        {
                                            try
                                            {
                                                if (ExtractedServerFolderRunTime == 0)
                                                {
                                                    string ExtractedServerFolder = Path.Combine(Save_Settings.Live_Data.Game_Path, ".data", json2.serverID.Hash_String(0).ToLower());
                                                    if (Directory.Exists(ExtractedServerFolder))
                                                    {
                                                        Directory.Delete(ExtractedServerFolder, true);
                                                        Log.Core("LAUNCHER: Removed Extracted Server Mods Folder: .data/" + json2.serverID.Hash_String(0).ToLower());
                                                    }

                                                    ExtractedServerFolderRunTime++;
                                                }

                                                if (File.Exists(ModCachedFile))
                                                {
                                                    File.Delete(ModCachedFile);
                                                    Log.Core("LAUNCHER: Removed Old Mod Package: " + modfile.Name);
                                                }
                                            }
                                            catch (Exception Error)
                                            {
                                                LogToFileAddons.OpenLog("SERVER MOD CACHE FILE", string.Empty, Error, string.Empty, true);
                                            }

                                            ModFilesDownloadUrls.Enqueue(new Uri(json2.basePath + "/" + modfile.Name));
                                            TotalModFileCount++;
                                        }
                                        else
                                        {
                                            Label_Download_Information.Text = ("Server Mods: Up to Date " + modfile.Name).ToUpper();
                                        }
                                    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                                    if (!(Disposing || IsDisposed))
                                    {
                                        if (ModFilesDownloadUrls.Count != 0)
                                        {
                                            this.DownloadModNetFilesRightNow(ModFolderCache);
                                            Presence_Launcher.Status(9);
                                        }
                                        else
                                        {
                                            Game_Check_Launch();
                                        }
                                    }
                                }
                                catch (Exception Error)
                                {
                                    Display_Color_Icons(2);
                                    Presence_Launcher.Status(10);
                                    if (!Save_Settings.Account_Manager())
                                    {
                                        Screen_Instance.Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Is_Email.Mask(Save_Account.Live_Data.User_Raw_Email)).ToUpper();
                                    }
                                    string LogMessage = "There was an error with Server Mods Check:";
                                    LogToFileAddons.OpenLog("SERVER MOD DOWNLOAD", LogMessage, Error, "Error", false);
                                    Button_Login_Logout_Modes(true, true);
                                    Display_Color_Icons();
                                    return;
                                }
                                finally
                                {
                                    if (!string.IsNullOrWhiteSpace(ModulesJSON))
                                    {
                                        ModulesJSON = string.Empty;
                                    }
                                    if (!string.IsNullOrWhiteSpace(ServerModInfo))
                                    {
                                        ServerModInfo = string.Empty;
                                    }
                                    if (json2 != null)
                                    {
                                        json2 = null;
                                    }
                                    if (!string.IsNullOrWhiteSpace(remoteCarsFile))
                                    {
                                        remoteCarsFile = string.Empty;
                                    }
                                    if (!string.IsNullOrWhiteSpace(remoteEventsFile))
                                    {
                                        remoteEventsFile = string.Empty;
                                    }
                                    if (!string.IsNullOrWhiteSpace(ServerModListJSON))
                                    {
                                        ServerModListJSON = string.Empty;
                                    }
                                    if (json3 != null)
                                    {
                                        json3 = null;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception Error)
                {
                    Display_Color_Icons(2);
                    Presence_Launcher.Status(8);
                    if (!Save_Settings.Account_Manager())
                    {
                        Screen_Instance.Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Is_Email.Mask(Save_Account.Live_Data.User_Raw_Email)).ToUpper();
                    }
                    string LogMessage = "There was an error downloading ModNet Files:";
                    LogToFileAddons.OpenLog("MODNET FILES", LogMessage, Error, "Error", false);
                    Button_Login_Logout_Modes(true, true);
                    Display_Color_Icons();
                    return;
                }
                finally
                {
                    if (!string.IsNullOrWhiteSpace(ModulesJSON))
                    {
                        ModulesJSON = string.Empty;
                    }
                    if (!string.IsNullOrWhiteSpace(ServerModInfo))
                    {
                        ServerModInfo = string.Empty;
                    }
                    if (json2 != null)
                    {
                        json2 = null;
                    }
                    if (!string.IsNullOrWhiteSpace(remoteCarsFile))
                    {
                        remoteCarsFile = string.Empty;
                    }
                    if (!string.IsNullOrWhiteSpace(remoteEventsFile))
                    {
                        remoteEventsFile = string.Empty;
                    }
                    if (!string.IsNullOrWhiteSpace(ServerModListJSON))
                    {
                        ServerModListJSON = string.Empty;
                    }
                    if (json3 != null)
                    {
                        json3 = null;
                    }
                }
            }
            else
            {
                Game_Check_Launch();
            }
        }

    }
}
