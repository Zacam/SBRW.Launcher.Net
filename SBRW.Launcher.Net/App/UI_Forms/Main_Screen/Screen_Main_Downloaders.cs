#region Usings
using SBRW.Launcher.Core.Cache;
using SBRW.Launcher.Core.Discord.RPC_;
using SBRW.Launcher.Core.Downloader.LZMA;
using SBRW.Launcher.Core.Downloader;
using SBRW.Launcher.Core.Extension.Api_;
using SBRW.Launcher.Core.Extension.Hash_;
using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.Core.Extra.File_;
using SBRW.Launcher.Core.Extra.Reference.System_;
using SBRW.Launcher.RunTime.LauncherCore.FileReadWrite;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.Languages.Visual_Forms;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using SBRW.Launcher.RunTime.LauncherCore.Support;
using System;
using System.IO;
using System.Linq;
using System.Net.Cache;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using SBRW.Launcher.Core.Theme;
using SBRW.Launcher.Core.Extension.Web_;
using SBRW.Launcher.Core.Extension.Validation_;
using SBRW.Launcher.RunTime.LauncherCore.ModNet;
using SBRW.Launcher.Core.Extra.File_.Save_;
#endregion

namespace SBRW.Launcher.App.UI_Forms.Main_Screen
{
    partial class Screen_Main
    {
        #region Game Files Downloader Components (LZMA [.dat])
        /// <summary>
        /// 
        /// </summary>
        public static void RemoveTracksHighFiles()
        {
            try
            {
                string SpecificTracksHighPath = Path.Combine(Save_Settings.Live_Data.Game_Path, "TracksHigh");
                if (File.Exists(Path.Combine(SpecificTracksHighPath, "STREAML5RA_98.BUN")))
                {
                    Directory.Delete(SpecificTracksHighPath, true);
                }
            }
            catch { }
        }
        /// <summary>
        /// 
        /// </summary>
        public void DownloadTracksFiles()
        {
            if (Screen_Instance != null && !(IsDisposed || Disposing))
            {
                if (UI_MODE != 3)
                {
                    UI_MODE = 3;
                }

                if (Label_Download_Information.InvokeRequired)
                {
                    Label_Download_Information.Invoke(new Action(delegate ()
                    {
                        Label_Download_Information_Support.Text = "Checking Tracks Files...".ToUpper();
                    }));
                }
                else
                {
                    Label_Download_Information_Support.Text = "Checking Tracks Files...".ToUpper();
                }

                string SpecificTracksFilePath = Path.Combine(Save_Settings.Live_Data.Game_Path, "Tracks", "STREAML5RA_98.BUN");
                if (!File.Exists(SpecificTracksFilePath) && (LZMA_Downloader != null))
                {
                    if (Label_Download_Information.InvokeRequired)
                    {
                        Label_Download_Information.Invoke(new Action(delegate ()
                        {
                            Label_Download_Information_Support.Text = "Downloading: Tracks Data".ToUpper();
                        }));
                    }
                    else
                    {
                        Label_Download_Information_Support.Text = "Downloading: Tracks Data".ToUpper();
                    }

                    Log.Info("DOWNLOAD: Getting Tracks Folder");
                    Download_Settings.Alternative_WebCalls(Launcher_Value.Launcher_Alternative_Webcalls());
                    LZMA_Downloader.StartDownload(Save_Settings.Live_Data.Launcher_CDN, "Tracks", Save_Settings.Live_Data.Game_Path, false, false, 615494528);
                }
                else
                {
                    DownloadSpeechFiles();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void DownloadSpeechFiles()
        {
            if (Screen_Instance != null && !(IsDisposed || Disposing))
            {
                string speechFile = string.Empty;
                int speechSize = 0;

                if (UI_MODE != 3)
                {
                    UI_MODE = 3;
                }

                if (Label_Download_Information.InvokeRequired)
                {
                    Label_Download_Information.Invoke(new Action(delegate ()
                    {
                        Label_Download_Information_Support.Text = "Looking for correct Speech Files...".ToUpper();
                    }));
                }
                else
                {
                    Label_Download_Information_Support.Text = "Looking for correct Speech Files...".ToUpper();
                }

                try
                {
                    speechFile = Translations.Speech_Files(Save_Settings.Live_Data.Launcher_Language);

                    Uri URLCall = new Uri(Save_Settings.Live_Data.Launcher_CDN + "/" + speechFile + "/index.xml");
                    ServicePointManager.FindServicePoint(URLCall).ConnectionLeaseTimeout = (int)TimeSpan.FromSeconds(Launcher_Value.Launcher_WebCall_Timeout_Enable ?
                                Launcher_Value.Launcher_WebCall_Timeout() : 60).TotalMilliseconds;
                    var Client = new WebClient
                    {
                        Encoding = Encoding.UTF8,
                        CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
                    };
                    if (!Launcher_Value.Launcher_Alternative_Webcalls())
                    {
                        Client = new WebClientWithTimeout { Encoding = Encoding.UTF8, CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore) };
                    }
                    else
                    {
                        Client.Headers.Add("user-agent", "SBRW Launcher " +
                        Application.ProductVersion + " (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)");
                    }

                    try
                    {
                        string response = Client.DownloadString(URLCall);

                        XmlDocument speechFileXml = new XmlDocument();
                        speechFileXml.LoadXml(response);

                        if (speechFileXml != default)
                        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                            XmlNode speechSizeNode = speechFileXml.SelectSingleNode("index/header/compressed");
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                            speechSize = Convert.ToInt32(speechSizeNode.InnerText);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                        }
                        else
                        {
                            speechFile = Translations.Speech_Files(InformationCache.Lang.ThreeLetterISOLanguageName);
                            speechSize = Translations.Speech_Files_Size();
                        }
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        Client?.Dispose();
                    }
                }
                catch (Exception Error)
                {
                    LogToFileAddons.OpenLog("Download Speech Files", string.Empty, Error, string.Empty, true);
                    speechFile = Translations.Speech_Files(InformationCache.Lang.ThreeLetterISOLanguageName ?? string.Empty);
                    speechSize = Translations.Speech_Files_Size();
                }

                if (Label_Download_Information.InvokeRequired)
                {
                    Label_Download_Information.Invoke(new Action(delegate ()
                    {
                        Label_Download_Information_Support.Text = string.Format("Checking for {0} Speech Files.", speechFile).ToUpper();
                    }));
                }
                else
                {
                    Label_Download_Information_Support.Text = string.Format("Checking for {0} Speech Files.", speechFile).ToUpper();
                }

                string SoundSpeechPath = Path.Combine(Save_Settings.Live_Data.Game_Path, "Sound", "Speech", "copspeechsth_" + speechFile + ".big");
                if (!File.Exists(SoundSpeechPath) && LZMA_Downloader != null)
                {
                    if (Label_Download_Information.InvokeRequired)
                    {
                        Label_Download_Information.Invoke(new Action(delegate ()
                        {
                            Label_Download_Information_Support.Text = "Downloading: Language Audio".ToUpper();
                        }));
                    }
                    else
                    {
                        Label_Download_Information_Support.Text = "Downloading: Language Audio".ToUpper();
                    }

                    Log.Info("DOWNLOAD: Getting Speech/Audio Files");
                    Download_Settings.Alternative_WebCalls(Launcher_Value.Launcher_Alternative_Webcalls());
                    LZMA_Downloader.StartDownload(Save_Settings.Live_Data.Launcher_CDN, speechFile, Save_Settings.Live_Data.Game_Path, false, false, speechSize);
                }
                else
                {
                    if (Label_Download_Information.InvokeRequired)
                    {
                        Label_Download_Information.Invoke(new Action(delegate ()
                        {
                            Label_Download_Information_Support.Text = string.Empty;
                        }));
                    }
                    else
                    {
                        Label_Download_Information_Support.Text = string.Empty;
                    }

                    OnDownloadFinished();
                    Log.Info("DOWNLOAD: Game Files Download is Complete!");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void OnDownloadFinished()
        {
            try
            {
                if (LZMA_Downloader != null)
                {
                    if (LZMA_Downloader.Downloading)
                    {
                        LZMA_Downloader.Stop();
                    }
                }

                if (Pack_SBRW_Downloader != null)
                {
                    if (!Pack_SBRW_Downloader.Cancel)
                    {
                        Pack_SBRW_Downloader.Cancel = true;
                    }

                    if (Pack_SBRW_Unpacker != null)
                    {
                        if (!Pack_SBRW_Unpacker.Cancel)
                        {
                            Pack_SBRW_Unpacker.Cancel = true;
                        }
                    }
                }

                Pack_SBRW_Downloader_Unpack_Lock = false;
            }
            catch (Exception Error_Live)
            {
                LogToFileAddons.OpenLog("Progress Bar/Outline ODF", string.Empty, Error_Live, string.Empty, true);
            }

            try
            {
                if (!string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Game_Path))
                {
                    string GFX_BootFlow_File_Path = Path.Combine(Save_Settings.Live_Data.Game_Path, "GFX", "BootFlow.gfx");

                    if (Hashes.Hash_SHA(GFX_BootFlow_File_Path) != "97ED41D1A44ACF58AF2613C243BDD88E8C3806EB")
                    {
                        if (File.Exists(GFX_BootFlow_File_Path))
                        {
                            File.Delete(GFX_BootFlow_File_Path);
                        }

                        File.WriteAllBytes(GFX_BootFlow_File_Path, Core.Theme.Conversion_.Embeded_Files.BootFlow_GFX_Bytes());
                    }
                }
            }
            catch (Exception Error_Live)
            {
                LogToFileAddons.OpenLog("BootFlow GFX File", string.Empty, Error_Live, string.Empty, true);
            }

            if (Save_Settings.Live_Data.Game_Integrity == "Unknown")
            {
                Save_Settings.Live_Data.Game_Integrity = "Good";
                Save_Settings.Save();
            }

            Presence_Launcher.Download = false;
            Presence_Launcher.Status(4);

            try
            {
                if (!Screen_Instance.DisposedForm())
                {
                    if (UI_MODE != 5)
                    {
                        UI_MODE = 5;
                    }

                    Screen_Instance.Label_Download_Information.Text = "Ready!".ToUpper();

                    IsDownloading = false;
                    Playenabled = true;
                }
            }
            catch (Exception Error_Live)
            {
                LogToFileAddons.OpenLog("Progress Bar/Outline ODF", string.Empty, Error_Live, string.Empty, true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Error"></param>
        private void OnDownloadFailed(Exception Error)
        {
            try
            {
                LZMA_Downloader?.Stop();

                if (Pack_SBRW_Downloader != null)
                {
                    if (!Pack_SBRW_Downloader.Cancel)
                    {
                        Pack_SBRW_Downloader.Cancel = true;
                    }

                    if (Pack_SBRW_Unpacker != null)
                    {
                        if (!Pack_SBRW_Unpacker.Cancel)
                        {
                            Pack_SBRW_Unpacker.Cancel = true;
                        }
                    }
                }

                Pack_SBRW_Downloader_Unpack_Lock = false;
            }
            catch (Exception Error_Live)
            {
                LogToFileAddons.OpenLog("Progress Bar/Outline ODF", string.Empty, Error_Live, string.Empty, true);
            }

            if (Screen_Instance != null && (!IsDisposed || !Disposing))
            {
                Presence_Launcher.Download = false;
                Presence_Launcher.Status(3);

                try
                {
                    if (UI_MODE != -1)
                    {
                        UI_MODE = -1;
                    }

                    Label_Download_Information.SafeInvokeAction(() => Label_Download_Information.Text = ((Error != null) ? Error.Message : "Download Failed. No Reason Provided").ToUpper(), this);

                    if (Error != null)
                    {
                        string TempEmailCache = string.Empty;
                        if (!string.IsNullOrWhiteSpace(Input_Email.Text))
                        {
                            TempEmailCache = Input_Email.Text;
                            Input_Email.SafeInvokeAction(() => Input_Email.Text = "EMAIL IS HIDDEN", this);
                        }

                        string LogMessage = "CDN Downloader Encountered an Error:";
                        LogToFileAddons.OpenLog("Game Download", LogMessage, Error, "Error", false);

                        if (!string.IsNullOrWhiteSpace(TempEmailCache))
                        {
                            Input_Email.SafeInvokeAction(() => Input_Email.Text = TempEmailCache, this);
                        }
                    }
                }
                catch (Exception Error_Live)
                {
                    LogToFileAddons.OpenLog("Progress Bar/Outline ODF", string.Empty, Error_Live, string.Empty, true);
                }

                FunctionStatus.IsVerifyHashDisabled = true;
            }
        }
        #endregion

        #region Game Files Downloader (SBRW Pack [.pack.sbrw])
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// potential error is that the Pack_SBRW_Unpacker variable is being assigned a new Download_Extract object every time the 
        /// Game_Pack_Downloader method is called, but it's not being disposed of or set to null afterwards. 
        /// This could lead to memory leaks if the method is called repeatedly.
        /// @DavidCarbon or @DavidCarbon-SBRW/launcher-team
        /// </remarks>
        /// <param name="Provided_File_Path"></param>
        private void Game_Pack_Unpacker(string Provided_File_Path)
        {
            if (!Pack_SBRW_Downloader_Unpack_Lock)
            {
                Pack_SBRW_Downloader_Unpack_Lock = true;
                Pack_SBRW_Unpacker = new Download_Extract();
                Pack_SBRW_Unpacker.Internal_Error += (_, U_Live_Events) =>
                {
                    if (U_Live_Events.Recorded_Exception != null && !Disposing && !IsDisposed)
                    {
                        LogToFileAddons.OpenLog("Pack_SBRW_Unpacker", string.Empty, U_Live_Events.Recorded_Exception, string.Empty, true);
                        OnDownloadFailed(U_Live_Events.Recorded_Exception);
                    }
                };
                Pack_SBRW_Unpacker.Live_Progress += (_, U_Live_Events) =>
                {
                    if (U_Live_Events != null && !Disposing && !IsDisposed)
                    {
                        if (UI_MODE != 4)
                        {
                            UI_MODE = 4;
                        }
                    }
                };
                Pack_SBRW_Unpacker.Complete += (_, U_Live_Events) =>
                {
                    if (U_Live_Events != null && !Disposing && !IsDisposed)
                    {
                        Label_Download_Information_Support.SafeInvokeAction(() =>
                        {
                            Label_Download_Information_Support.Visible = false;
                            Label_Download_Information_Support.Text = string.Empty;
                        }, this);

                        IsDownloading = false;
                        OnDownloadFinished();
                        try
                        {
                            if (NotifyIcon_Notification != default)
                            {
                                NotifyIcon_Notification.Visible = true;
                                NotifyIcon_Notification.BalloonTipIcon = ToolTipIcon.Info;
                                NotifyIcon_Notification.BalloonTipTitle = "SBRW Launcher";
                                NotifyIcon_Notification.BalloonTipText = "Your game is now ready to launch!";
                                NotifyIcon_Notification.BalloonTipClicked += (x, D_Live_Events) =>
                                {
                                    return;
                                };
                                NotifyIcon_Notification.BalloonTipClosed += (x, D_Live_Events) =>
                                {
                                    return;
                                };
                                NotifyIcon_Notification.ShowBalloonTip(5000);
                            }
                        }
                        catch (Exception Error)
                        {
                            LogToFileAddons.OpenLog("NotifyIcon_Notification Unpack", string.Empty, Error, string.Empty, true);
                        }
                    }
                };
                Pack_SBRW_Unpacker.Custom_Unpack(Provided_File_Path, Save_Settings.Live_Data.Game_Path);
            }
        }
        /// <summary>
        /// SBRW Pack Downloader
        /// </summary>
        /// <remarks>That's right the Protype Extractor from 2.1.5.x, now back from the dead - DavidCarbon</remarks>
        private void Game_Pack_Downloader()
        {
            if (!Screen_Instance.DisposedForm())
            {
                if (UI_MODE != 9)
                {
                    UI_MODE = 9;
                }

                if (!Screen_Instance.DisposedForm())
                {
                    Screen_Instance.Label_Download_Information.Text = "Please Wait, Calculating Game Folder Size.";
                }

                long Game_Folder_Size = File_and_Folder_Extention.GetDirectorySize_GameFiles(new DirectoryInfo(Save_Settings.Live_Data.Game_Path));
                /* TODO: Check for other files and Folder Size */
                if ((Game_Folder_Size == -1) &&
                    (("Seems like we are unable to determine the Games Folder Size" +
                        "\nDo you have the Game Files Already Downloaded?").Message_Box(MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
                {
                    Game_Folder_Size = 3296810469;
                }
                else
                {
                    Log.Debug($"Game Folder Size: {Game_Folder_Size}");
                }

                if (UI_MODE != 3)
                {
                    UI_MODE = 3;
                }

                if (!Game_Folder_Size.GameInstall_Found() || !File.Exists(Path.Combine(Save_Settings.Live_Data.Game_Path, "nfsw.exe")))
                {
                    if (UI_MODE != 10)
                    {
                        UI_MODE = 10;
                    }

                    if (Hashes.Hash_SHA(Save_Settings.Live_Data.Game_Archive_Location) == "88C886B6D131C052365C3D6D14E14F67A4E2C253")
                    {
                        Game_Pack_Unpacker(Save_Settings.Live_Data.Game_Archive_Location);
                    }
                    else
                    {
                        switch (API_Core.StatusCheck(Save_Settings.Live_Data.Launcher_CDN + "/GameFiles.sbrwpack", 10))
                        {
                            case APIStatus.Online:
                                if (UI_MODE != 11)
                                {
                                    UI_MODE = 11;
                                }

                                Pack_SBRW_Downloader = new Download_Client()
                                {
                                    Folder_Path = Save_Settings.Live_Data.Game_Path,
                                    File_Path = Save_Settings.Live_Data.Game_Archive_Location,
                                    File_Name = "GameFiles.sbrwpack",
                                    Web_File_Size = 3862102244,
                                    Web_URL = Save_Settings.Live_Data.Launcher_CDN + "/GameFiles.sbrwpack",
                                    File_Hash = "88C886B6D131C052365C3D6D14E14F67A4E2C253",
                                    File_Removal = true,
                                    Download_Retry_Attempts = 100
                                };
                                /* @DavidCarbon or @Zacam (Translation Strings Required) */
                                Pack_SBRW_Downloader.Internal_Error += (_, D_Live_Events) =>
                                {
                                    if (D_Live_Events.Recorded_Exception != null && !Pack_SBRW_Downloader.Cancel)
                                    {
                                        LogToFileAddons.OpenLog("Pack_SBRW_Downloader", string.Empty, D_Live_Events.Recorded_Exception, string.Empty, true);

                                        if (D_Live_Events.Recorded_Exception is WebException)
                                        {
                                            string Status_Code_Explaination = "Unknown";
                                            bool Allow_Restart = true;
                                            switch (API_Core.StatusCodes(Save_Settings.Live_Data.Launcher_CDN, D_Live_Events.Recorded_Exception as WebException, null))
                                            {
                                                /* SSL Chain Validation Error */
                                                case APIStatus.TrustFailure:
                                                case APIStatus.SecureChannelFailure:
                                                case APIStatus.InvaildSSL:
                                                case APIStatus.SSLFailed:
                                                    Status_Code_Explaination = "Unable to Create a Secure Connection." +
                                                    "\nSSL may be invalid, System has blocked connection, or System is unable to handle TLS 1.2 and higher with C# Apps."
#if !(RELEASE_UNIX || DEBUG_UNIX)
                                                    ;
#else
                                                + "\nCheck if Alternative WebCalls is Enabled to Fix the issue";
#endif
                                                    Allow_Restart = false;
                                                    break;
                                                /* The following Error Codes Means Internal Error Had Occurred */
                                                case APIStatus.ProtocolError:
                                                case APIStatus.UnknownError:
                                                case APIStatus.UnknownStatusCode:
                                                case APIStatus.Unknown:
                                                    Status_Code_Explaination = "Internal Error had occurred." +
                                                        "\nCheck Launcher Log for more Details.";
                                                    break;
                                                /* Unable to reach online server */
                                                case APIStatus.Offline:
                                                case APIStatus.NameResolutionFailure:
                                                case APIStatus.OriginUnreachable:
                                                case APIStatus.ServerUnavailable:
                                                    Status_Code_Explaination = "Unable to Connect to CDN." +
                                                        "\nCheck Launcher Log for more Details.";
                                                    Allow_Restart = false;
                                                    break;
                                                /* Not Found, Don't Retry */
                                                case APIStatus.NotFound:
                                                    Status_Code_Explaination = "File Not Found." +
                                                        "\nAsk for Assistance or Change to another CDN.";
                                                    Allow_Restart = false;
                                                    break;
                                                case APIStatus.Forbidden:
                                                    Status_Code_Explaination = "No Permission to Access this File or Server" +
                                                        "\nCheck Launcher Log for more Details." +
                                                        "\nAsk for Assistance or Change to another CDN.";
                                                    Allow_Restart = false;
                                                    break;
                                                /* Generic Error Type */
                                                default:
                                                    Status_Code_Explaination = "A Generic Error was encountered" +
                                                        "\nCheck Launcher Log for more Details.";
                                                    break;
                                            }

                                            DialogResult User_Prompt_Box = Status_Code_Explaination.Message_Box(
                                                Allow_Restart ? MessageBoxButtons.RetryCancel : MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            if (User_Prompt_Box == DialogResult.Retry)
                                            {
                                                Game_Pack_Downloader();
                                            }
                                            else
                                            {
                                                OnDownloadFailed(D_Live_Events.Recorded_Exception);
                                            }
                                        }
                                        else
                                        {
                                            OnDownloadFailed(D_Live_Events.Recorded_Exception);
                                        }
                                    }
                                };
                                Pack_SBRW_Downloader.Live_Progress += (_, D_Live_Events) =>
                                {
                                    if (UI_MODE != 1)
                                    {
                                        UI_MODE = 1;
                                    }
                                };
                                Pack_SBRW_Downloader.Complete += (_, D_Live_Events) =>
                                {
                                    if (D_Live_Events.Complete && !(this.Disposing || this.IsDisposed))
                                    {
                                        if (UI_MODE != 2)
                                        {
                                            UI_MODE = 2;
                                        }
                                        /* Unpack Local GameFiles Pack */
                                        Game_Pack_Unpacker(D_Live_Events.Download_Location ?? InformationCache.Default_Game_Archive_Path());
                                    }
                                };
                                /* Main Note: Current Revision File Size (in long) is: 3862102244 */
                                Pack_SBRW_Downloader.Download();

                                break;
                            case APIStatus.Forbidden:
                            case APIStatus.NotFound:
                                if (("Game Archive is Not Present on Current Saved CDN." +
                                    "\nWould you like to check for LZMA Support? This would switch to the old LZMA Downloader." +
                                    "\nOtherwise, please switch to another CDN.").Message_Box(
                                    MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Retry)
                                {
                                    switch (API_Core.StatusCheck(Save_Settings.Live_Data.Launcher_CDN + "/en/index.xml", 10))
                                    {
                                        case APIStatus.Online:
                                            Game_Downloaders(true);
                                            break;
                                        case APIStatus.Forbidden:
                                        case APIStatus.NotFound:
                                            OnDownloadFailed(new Exception("Game Archive & LZMA Not Present on Server. Please Choose Another CDN"));
                                            break;
                                        default:
                                            OnDownloadFailed(new Exception("Please Choose Another CDN"));
                                            break;
                                    }
                                }
                                else
                                {
                                    OnDownloadFailed(new Exception("Game Archive Not Present on Server. Please Choose Another CDN"));
                                }
                                break;
                            default:
                                OnDownloadFailed(new Exception("Unable to Connect to CDN. Choose Another CDN or look at Logs for Details"));
                                break;
                        }
                    }
                }
                else
                {
                    OnDownloadFinished();
                }
            }
        }
        #endregion

        #region Game Downloader Background Thread Support Functions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="From_PackDownloader"></param>
        public async void Game_Downloaders(bool From_PackDownloader = false)
        {
            await Task.Run(() =>
            {
                if (Screen_Instance != null && (!IsDisposed || !Disposing))
                {
                    try
                    {
                        if (UI_MODE != 3)
                        {
                            UI_MODE = 3;
                        }

                        if (Label_Download_Information.InvokeRequired)
                        {
                            Label_Download_Information.Invoke(new Action(delegate ()
                            {
                                Label_Download_Information.Text = "Loading list of files to download...".ToUpper();
                            }));
                        }
                        else
                        {
                            Label_Download_Information.Text = "Loading list of files to download...".ToUpper();
                        }
                    }
                    catch (Exception Error)
                    {
                        LogToFileAddons.OpenLog("Progress Bar/Outline", string.Empty, Error, string.Empty, true);
                    }
                }

                try
                {
                    if (LZMA_Downloader != null)
                    {
                        if (LZMA_Downloader.Downloading)
                        {
                            LZMA_Downloader.Stop();
                        }
                    }
                }
                catch (Exception Error)
                {
                    LogToFileAddons.OpenLog("CDN DOWNLOADER [LZMA]", string.Empty, Error, string.Empty, true);
                }

                try
                {
                    if (Pack_SBRW_Downloader != null)
                    {
                        if (!Pack_SBRW_Downloader.Cancel)
                        {
                            Pack_SBRW_Downloader.Cancel = true;
                        }

                        if (Pack_SBRW_Unpacker != null)
                        {
                            if (!Pack_SBRW_Unpacker.Cancel)
                            {
                                Pack_SBRW_Unpacker.Cancel = true;
                            }
                        }
                    }
                }
                catch (Exception Error)
                {
                    LogToFileAddons.OpenLog("CDN DOWNLOADER", string.Empty, Error, string.Empty, true);
                }

                /* Use Local Packed Archive for Install Source - DavidCarbon */
                if (Save_Settings.Downloader_Game_Pack() && !From_PackDownloader)
                {
                    try
                    {
                        //@DavidCarbon -> 9-15-2022
                        Game_Pack_Downloader();
                    }
                    catch (Exception Error)
                    {
                        LogToFileAddons.OpenLog("New Download Game Files", string.Empty, Error, string.Empty, true);
                    }
                }
                else if (Save_Settings.Downloader_Game_LZMA())
                {
                    try
                    {
                        LZMA_Downloader = new Download_LZMA_Data(3, 2, 16);

                        LZMA_Downloader.Internal_Error += (_, D_Live_Events) =>
                        {
                            if (D_Live_Events.Recorded_Exception != null && LZMA_Downloader.Downloading)
                            {
                                LogToFileAddons.OpenLog("LZMA_SBRW_Downloader", string.Empty, D_Live_Events.Recorded_Exception, string.Empty, true);
                                OnDownloadFailed(D_Live_Events.Recorded_Exception);
                            }
                        };

                        LZMA_Downloader.Complete += (_, Live_Data) =>
                        {
                            if ((!IsDisposed || !Disposing) && Live_Data.Complete)
                            {
                                Game_Downloaders();
                            }
                        };

                        LZMA_Downloader.Live_Progress += (_, Live_Data) =>
                        {
                            if ((!IsDisposed || !Disposing) && LZMA_Downloader != null)
                            {
                                if (LZMA_Downloader.Downloading)
                                {
                                    if (UI_MODE != 15)
                                    {
                                        UI_MODE = 15;
                                    }
                                }
                                else if (LZMA_Downloader != null)
                                {
                                    if (LZMA_Downloader.Downloading)
                                    {
                                        LZMA_Downloader.Stop();
                                    }
                                }
                            }
                            else if (LZMA_Downloader != null)
                            {
                                if (LZMA_Downloader.Downloading)
                                {
                                    LZMA_Downloader.Stop();
                                }
                            }
                        };

                        if (Screen_Instance != null && (!IsDisposed || !Disposing))
                        {
                            if (Label_Download_Information.InvokeRequired)
                            {
                                Label_Download_Information.Invoke(new Action(delegate ()
                                {
                                    Label_Download_Information.Text = "Checking Core Files...".ToUpper();
                                }));
                            }
                            else
                            {
                                Label_Download_Information.Text = "Checking Core Files...".ToUpper();
                            }

                            string GameExePath = Path.Combine(Save_Settings.Live_Data.Game_Path, "nfsw.exe");

                            if (!File.Exists(GameExePath) && LZMA_Downloader != null)
                            {
                                if (Label_Download_Information.InvokeRequired)
                                {
                                    Label_Download_Information.Invoke(new Action(delegate ()
                                    {
                                        Label_Download_Information_Support.Text = "Downloading: Core GameFiles".ToUpper();
                                    }));
                                }
                                else
                                {
                                    Label_Download_Information_Support.Text = "Downloading: Core GameFiles".ToUpper();
                                }

                                Log.Info("DOWNLOAD: Getting Core Game Files");
#if (RELEASE_UNIX || DEBUG_UNIX)
                                Download_LZMA_Settings.System_Unix = Download_Settings.System_Unix = true;
#endif
                                Download_LZMA_Settings.Alternative_WebCalls = Download_Settings.Alternative_WebCalls(Launcher_Value.Launcher_Alternative_Webcalls());
                                LZMA_Downloader.StartDownload(Save_Settings.Live_Data.Launcher_CDN, string.Empty, Save_Settings.Live_Data.Game_Path, false, false, 1130632198);
                            }
                            else
                            {
                                DownloadTracksFiles();
                            }
                        }
                    }
                    catch (Exception Error)
                    {
                        LogToFileAddons.OpenLog("Classic Download Game Files", string.Empty, Error, string.Empty, true);
                    }
                }
                else
                {
                    Log.Debug("[GAME DOWNLOADER] RAW DOWNLOADER NOT IMPLEMENTED");
                }
            });
        }
        /// <summary>
        /// 
        /// </summary>
#if (DEBUG_UNIX || RELEASE_UNIX)
        public void Game_Folder_Checks(bool Bypass_Storage_Requirement = false)
#else
        public void Game_Folder_Checks()
#endif
        {
            if (Screen_Instance != null && (!IsDisposed || !Disposing))
            {
                if (UI_MODE != 3)
                {
                    UI_MODE = 3;
                }

                Button_Play_OR_Update.SafeInvokeAction(() =>
                {
                    Button_Play_OR_Update.BackgroundImage = Image_Button.Play;
                    Button_Play_OR_Update.ForeColor = Color_Text.L_Three;
                }, this, false);

                Label_Download_Information.SafeInvokeAction(() =>
                Label_Download_Information.Text = "Checking up all files".ToUpper(), this);

                try
                {
                    Label_Download_Information.SafeInvokeAction(() =>
                    Label_Download_Information.Text = "Checking Drive Format and Space".ToUpper(), this);

                    Format_System_Storage Detected_Drive = System_Storage.Drive_Full_Info(Save_Settings.Live_Data.Game_Path, false, true);

#if !(RELEASE_UNIX || DEBUG_UNIX)
                    if (Detected_Drive.TotalFreeSpace < 8000000000 ||
                        !string.Equals(Detected_Drive.DriveFormat, "NTFS", StringComparison.InvariantCultureIgnoreCase))
#else
                    if (Detected_Drive.TotalFreeSpace < 8000000000 && !Bypass_Storage_Requirement && Save_Settings.Live_Data.Alert_Storage_Space == "0")
#endif
                    {
                        if (UI_MODE != 6)
                        {
                            UI_MODE = 6;
                        }

#if !(RELEASE_UNIX || DEBUG_UNIX)
                        if (!string.Equals(Detected_Drive.DriveFormat, "NTFS", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Label_Download_Information_Support.SafeInvokeAction(() =>
                            Label_Download_Information_Support.Text = ("Playing the game on a non-NTFS-formatted drive is not supported.").ToUpper(), this, false);
                            Label_Download_Information.SafeInvokeAction(() =>
                            Label_Download_Information.Text = ("Drive '" + Detected_Drive.Name + "' is formatted with: " + Detected_Drive.DriveFormat + " Type.").ToUpper(), this);
                        }
                        else
                        {
                            Label_Download_Information.SafeInvokeAction(() =>
                            Label_Download_Information.Text = ("Make sure you have at least 8GB of free space on hard drive.").ToUpperInvariant(), this);
                        }

                        FunctionStatus.IsVerifyHashDisabled = true;
#else
                        Label_Download_Information.SafeInvokeAction(() =>
                        Label_Download_Information.Text = ("Make sure you have at least 8GB of free space on hard drive.").ToUpperInvariant(), this);

                        DialogResult Live_Prompt_Data = new Update_Popup_Screen.Screen_Update_Popup(false).ShowDialog();
                        /*"Click Ignore to Enable Storage Detection Bypass (Unix Builds Only) and Restarts the Downloader" +
                            "Click Retry to temporary bypass the Storage Detection." +
                            "Click Ok, to Close this Message";
                         */
                        switch (Live_Prompt_Data)
                        {
                            case DialogResult.Ignore:
                                Save_Settings.Live_Data.Alert_Storage_Space = "1";
                                Game_Folder_Checks(true);
                                break;
                            case DialogResult.Retry:
                                Game_Folder_Checks(true);
                                break;
                        }
#endif
                    }
                    else if (Save_Settings.Live_Data.Launcher_CDN.StartsWith("http://localhost") || Save_Settings.Live_Data.Launcher_CDN.StartsWith("https://localhost"))
                    {
                        if (UI_MODE != 6)
                        {
                            UI_MODE = 6;
                        }

                        Label_Download_Information_Support.SafeInvokeAction(() => Label_Download_Information_Support.Text = "Failsafe CDN Detected".ToUpperInvariant(), this, false);
                        Label_Download_Information.SafeInvokeAction(() => Label_Download_Information.Text = "Please Choose a CDN from Settings Screen".ToUpperInvariant(), this);
                    }
                    else
                    {
                        Game_Downloaders();
                    }
                }
                catch (IOException Error)
                {
                    LogToFileAddons.OpenLog("Game Folder Checks [I.O.E.]", string.Empty, Error, string.Empty, true);
                }
                catch (UnauthorizedAccessException Error)
                {
                    LogToFileAddons.OpenLog("Game Folder Checks [U.A.E.]", string.Empty, Error, string.Empty, true);
                }
                catch (Exception Error)
                {
                    LogToFileAddons.OpenLog("Game Folder Checks", string.Empty, Error, string.Empty, true);
                }
            }
        }
        #endregion

        #region ModNet Downloader
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void DownloadModNetFilesRightNow(string path)
        {
            if (!Screen_Instance.DisposedForm())
            {
                while (IsDownloadingModNetFiles == false)
                {
                    CurrentModFileCount++;
                    Uri url = ModFilesDownloadUrls.Dequeue();
                    string FileName = url.ToString().Substring(url.ToString().LastIndexOf("/") + 1, (url.ToString().Length - url.ToString().LastIndexOf("/") - 1));

                    ModNetFileNameInUse = FileName;
                    ServicePointManager.FindServicePoint(url).ConnectionLeaseTimeout = (int)TimeSpan.FromSeconds(Launcher_Value.Launcher_WebCall_Timeout_Enable ?
                                        Launcher_Value.Launcher_WebCall_Timeout() : 60).TotalMilliseconds;
                    var Client = new WebClient
                    {
                        Encoding = Encoding.UTF8,
                        CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
                    };
                    if (!Launcher_Value.Launcher_Alternative_Webcalls())
                    {
                        Client = new WebClientWithTimeout { Encoding = Encoding.UTF8, CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore) };
                    }
                    else
                    {
                        Client.Headers.Add("user-agent", "SBRW Launcher " +
                        Application.ProductVersion + " (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)");
                    }

                    try
                    {
                        Client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Client_DownloadProgressChanged_RELOADED);
                        Client.DownloadFileCompleted += (Live_Object, Live_Final_Results) =>
                        {
#if !DEBUG
                            if (Live_Final_Results.Error != null)
                            {
                                LogToFileAddons.OpenLog("Modnet Server Files", string.Empty, Live_Final_Results.Error, string.Empty, true);
                            }
                            else if (Live_Final_Results.Cancelled)
                            {
                                Log.Core("LAUNCHER: Modnet Server Files Download was Cancelled");
                            }
                            else
                            {
#endif
                                Log.Core("LAUNCHER: Downloaded: " + FileName);
                                IsDownloadingModNetFiles = false;
                                if (!ModFilesDownloadUrls.Any())
                                {
                                    ModNet_Download_Status = new Download_Information_ModNet()
                                    {
                                        Download_Percentage = 100,
                                        Download_Complete = true
                                    };

                                    Game_Check_Launch();
                                }
                                else
                                {
                                    /* Redownload other file */
                                    DownloadModNetFilesRightNow(path);
                                }
#if !DEBUG
                            }
#endif
                        };
                        Client.DownloadFileAsync(url, path + "/" + FileName);
                    }
                    catch (Exception Error)
                    {
                        LogToFileAddons.OpenLog("Modnet Server Files", string.Empty, Error, string.Empty, true);

                        if (Screen_Instance != null && (!IsDisposed || !Disposing))
                        {
                            Label_Information_Window.SafeInvokeAction(() =>
                            Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Is_Email.Mask(Save_Account.Live_Data.User_Raw_Email)).ToUpper());
                        }
                    }
                    finally
                    {
                        if (Client != null)
                        {
                            Client.Dispose();
                        }
                    }

                    IsDownloadingModNetFiles = true;
                }
            }
        }
        #endregion
    }
}
