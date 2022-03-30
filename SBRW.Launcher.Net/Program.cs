using SBRW.Launcher.App.Classes.LauncherCore.Client;
using SBRW.Launcher.App.Classes.LauncherCore.Global;
using SBRW.Launcher.App.Classes.LauncherCore.Languages.Visual_Forms;
using SBRW.Launcher.App.Classes.LauncherCore.Logger;
using SBRW.Launcher.App.Classes.SystemPlatform.Components;
using SBRW.Launcher.App.Classes.SystemPlatform.Unix;
using SBRW.Launcher.App.UI_Forms;
using SBRW.Launcher.Core.Extension.Logging_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Runtime.InteropServices;
#if NETFRAMEWORK
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
#endif
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SBRW.Launcher.Net
{
    internal static class Program
    {
        public static bool LauncherMustRestart { get; set; }
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs Error)
        {
            try
            {
                LogToFileAddons.OpenLog("Thread Exception", Translations.Database("Application_Exception_Thread") + ": ",
                    Error.Exception, "Error", false);

                try
                {
                    Process[] allOfThem = Process.GetProcessesByName("nfsw");
                    if (allOfThem != null && allOfThem.Length >= 1)
                    {
                        foreach (var oneProcess in allOfThem)
                        {
                            Process.GetProcessById(oneProcess.Id).Kill();
                        }
                    }
                }
                catch { }
            }
            finally
            {
                Application.Exit();
                // If in Console Mode or if Form is Hidden and/or for Background Threads
                Environment.Exit(Environment.ExitCode);
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs Error)
        {
            try
            {
                LogToFileAddons.OpenLog("Unhandled Exception", Translations.Database("Application_Exception_Unhandled") + ": ",
                    (Exception)Error.ExceptionObject, "Error", false);

                try
                {
                    Process[] allOfThem = Process.GetProcessesByName("nfsw");
                    if (allOfThem != null && allOfThem.Length >= 1)
                    {
                        foreach (var oneProcess in allOfThem)
                        {
                            Process.GetProcessById(oneProcess.Id).Kill();
                        }
                    }
                }
                catch { }
            }
            finally
            {
                Application.Exit();
                /* If in Console Mode or if Form is Hidden and/or for Background Threads */
                Environment.Exit(Environment.ExitCode);
            }
        }

        static void Start()
        {
            try
            {
                Log.Info("MAINSCREEN: Program Started");
                Application.Run(new Parent_Screen());
            }
            catch (COMException Error)
            {
                LogToFileAddons.OpenLog("Main Screen [Application Run]", "Launcher Encounterd an Error.", Error, "Error", false);
                FunctionStatus.ErrorCloseLauncher("Main Screen [Application Run]", false);
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("Main Screen [Application Run]", "Launcher Encounterd an Error.", Error, "Error", false);
                FunctionStatus.ErrorCloseLauncher("Main Screen [Application Run]", false);
            }
        }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#region Core application Settings set By the Developer
            /* Application and Thread Language */
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo(InformationCache.Lang.Name);
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo(Translations.UI(Translations.Application_Language = InformationCache.Lang.Name));
            /* Custom Error Handling */
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
#if !NETFRAMEWORK
#if NET6_0
            AppContext.SetSwitch("System.Drawing.EnableUnixSupport", true);
#endif
            ApplicationConfiguration.Initialize();
#else
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /* We need to set these once and Forget about it (Unless there is a bug such as HttpWebClient) */
            AppContext.SetSwitch("Switch.System.Net.DontEnableSchUseStrongCrypto", false);
            AppContext.SetSwitch("Switch.System.Net.DontEnableSystemDefaultTlsVersions", false);
            ServicePointManager.DnsRefreshTimeout = (int)TimeSpan.FromSeconds(30).TotalMilliseconds;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.ServerCertificateValidationCallback = (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) =>
            {
                bool isOk = true;
                if (sslPolicyErrors != SslPolicyErrors.None)
                {
                    for (int i = 0; i < chain.ChainStatus.Length; i++)
                    {
                        if (chain.ChainStatus[i].Status == X509ChainStatusFlags.RevocationStatusUnknown)
                        {
                            continue;
                        }
                        chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                        chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                        chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 0, 15);
                        chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                        bool chainIsValid = chain.Build((X509Certificate2)certificate);
                        if (!chainIsValid)
                        {
                            isOk = false;
                            break;
                        }
                    }
                }
                return isOk;
            };
#endif
#endregion
#region Application Library File Checks and Process
            if (Debugger.IsAttached && !NFSW.IsRunning())
            {
                Start();
            }
            else
            {
                if (NFSW.IsRunning())
                {
                    if (NFSW.DetectGameProcess())
                    {
                        MessageBox.Show(null, Translations.Database("Program_TextBox_GameIsRunning"), "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (NFSW.DetectGameLauncherSimplified())
                    {
                        MessageBox.Show(null, Translations.Database("Program_TextBox_SimplifiedIsRunning"), "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show(null, Translations.Database("Program_TextBox_SBRWIsRunning"), "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    FunctionStatus.LauncherForceClose = true;
                }

                if (FunctionStatus.LauncherForceClose)
                {
                    FunctionStatus.ErrorCloseLauncher("User Tried to Launch SBRW Launcher with one Running Already", false);
                }
                else
                {
                    /* Check if File needs to be Downloaded */
                    string LZMAPath = Path.Combine(Locations.LauncherFolder, Locations.NameLZMA);

                    if (File.Exists(LZMAPath))
                    {
                        try
                        {
                            if (new FileInfo(LZMAPath).Length == 0)
                            {
                                File.Delete(LZMAPath);
                            }
                        }
                        catch { }
                    }
                    /* INFO: this is here because this dll is necessary for downloading game files and I want to make it async.
                    Updated RedTheKitsune Code so it downloads the file if its missing.
                    It also restarts the launcher if the user click on yes on Prompt. - DavidCarbon */
                    if (!File.Exists("LZMA.dll"))
                    {
                        try
                        {
                            Uri URLCall = new Uri(URLs.File + "/LZMA.dll");
#pragma warning disable SYSLIB0014 // Type or member is obsolete
                            ServicePointManager.FindServicePoint(URLCall).ConnectionLeaseTimeout = (int)TimeSpan.FromMinutes(1).TotalMilliseconds;
                            WebClient Client = new WebClient
                            {
                                Encoding = Encoding.UTF8,
                                CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
                            };
#pragma warning restore SYSLIB0014 // Type or member is obsolete
                            Client.Headers.Add("user-agent", "SBRW Launcher " +
                                Application.ProductVersion + " (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)");
                            Client.DownloadFileCompleted += (object sender, AsyncCompletedEventArgs e) =>
                            {
                                if (File.Exists(LZMAPath))
                                {
                                    try
                                    {
                                        if (new FileInfo(LZMAPath).Length == 0)
                                        {
                                            File.Delete(LZMAPath);
                                        }
                                    }
                                    catch { }
                                }
                            };

                            FunctionStatus.LauncherForceClose = true;

                            try
                            {
                                Client.DownloadFile(URLCall, LZMAPath);

                                if (MessageBox.Show(null, Translations.Database("Program_TextBox_LZMA_Redownloaded"),
                                    "GameLauncher Restart Required",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                {
                                    LauncherMustRestart = true;
                                }
                            }
                            catch (Exception Error)
                            {
                                FunctionStatus.LauncherForceCloseReason = Error.Message;
                            }
                            finally
                            {
                                if (Client != null)
                                {
                                    Client.Dispose();
                                }
                            }
                        }
                        catch { }
                    }

                    if (FunctionStatus.LauncherForceClose)
                    {
                        FunctionStatus.ErrorCloseLauncher("Closing From Downloaded Missing LZMA", LauncherMustRestart);
                    }
                    else
                    {
                        Mutex No_Java = new Mutex(false, "GameLauncherNFSW-MeTonaTOR");
                        try
                        {
                            if (No_Java.WaitOne(0, false))
                            {
                                if (UnixOS.Detected())
                                {
                                    /* MONO Hates this... */
                                    string[] File_List =
                                    {
                                        "DiscordRPC.dll - 1.0.175.0",
                                        "Flurl.dll - 3.0.2",
                                        "Flurl.Http.dll - 3.2.0",
                                        "LZMA.dll - 9.10 beta",
                                        "Newtonsoft.Json.dll - 13.0.1",
                                        "System.Runtime.InteropServices.RuntimeInformation.dll - 4.6.24705.01. " +
                                        "Commit Hash: 4d1af962ca0fede10beb01d197367c2f90e92c97",
                                        "System.ValueTuple.dll - 4.6.26515.06 @BuiltBy: dlab-DDVSOWINAGE059 " +
                                        "@Branch: release/2.1 @SrcCode: https://github.com/dotnet/corefx/tree/30ab651fcb4354552bd4891619a0bdd81e0ebdbf",
                                        "WindowsFirewallHelper.dll - 2.1.4.81",
                                        "SBRW.Ini.Parser.dll - 2.6.3",
                                        "SBRW.Launcher.Core.dll - 0.0.24",
                                        "SBRW.Nancy.dll - 2.0.10",
                                        "SBRW.Nancy.Hosting.Self.dll - 2.0.6",
                                        "SBRW.Launcher.Core.Extra.dll - 0.0.7",
                                        "SBRW.Launcher.Core.Discord.dll - 0.0.14",
                                        "SBRW.Launcher.Core.Proxy.dll - 0.0.12"
                                    };

                                    List<string> Missing_File_List = new List<string>();

                                    foreach (string File_String in File_List)
                                    {
                                        string[] Split_File_Version = File_String.Split(new string[] { " - " }, StringSplitOptions.None);

                                        if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), Split_File_Version[0])))
                                        {
                                            Missing_File_List.Add(Split_File_Version[0] + " - " + Translations.Database("Program_TextBox_File_NotFound"));
                                        }
                                        else
                                        {
                                            try
                                            {
                                                FileVersionInfo Version_Info = FileVersionInfo.GetVersionInfo(Split_File_Version[0]);
                                                string[] Version_Split = (Version_Info.ProductVersion??string.Empty).Split('+');
                                                string File_Version = Version_Split[0];

                                                if (File_Version == "")
                                                {
                                                    Missing_File_List.Add(Split_File_Version[0] + " - " + Translations.Database("Program_TextBox_File_Invalid"));
                                                }
                                                else
                                                {
                                                    if (!HardwareInfo.CheckArchitectureFile(Split_File_Version[0]))
                                                    {
                                                        Missing_File_List.Add(Split_File_Version[0] + " - " + Translations.Database("Program_TextBox_File_Invalid_CPU"));
                                                    }
                                                    else
                                                    {
                                                        if (File_Version != Split_File_Version[1])
                                                        {
                                                            Missing_File_List.Add(Split_File_Version[0] + " - " + Translations.Database("Program_TextBox_File_Invalid_Version") +
                                                                "(" + Split_File_Version[1] + " != " + File_Version + ")");
                                                        }
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                                Missing_File_List.Add(Split_File_Version[0] + " - " + Translations.Database("Program_TextBox_File_Invalid"));
                                            }
                                        }
                                    }

                                    if (Missing_File_List.Count != 0)
                                    {
                                        string Message_Display = Translations.Database("Program_TextBox_File_Invalid_Start");

                                        foreach (string File_String in Missing_File_List)
                                        {
                                            Message_Display += "� " + File_String + "\n";
                                        }

                                        FunctionStatus.LauncherForceClose = true;
                                        MessageBox.Show(null, Message_Display, "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }

                                if (FunctionStatus.LauncherForceClose)
                                {
                                    FunctionStatus.ErrorCloseLauncher("Closing From Missing .dll Files Check", LauncherMustRestart);
                                }
                                else
                                {
                                    Start();
                                }
                            }
                            else
                            {
                                MessageBox.Show(null, Translations.Database("Program_TextBox_SBRWIsRunning"),
                                    "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        finally
                        {
                            No_Java.Close();
                            No_Java.Dispose();
                        }
                    }
                }
            }
#endregion
        }
    }
}