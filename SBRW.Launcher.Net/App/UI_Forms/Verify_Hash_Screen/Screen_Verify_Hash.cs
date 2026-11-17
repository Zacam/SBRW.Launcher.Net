using SBRW.Launcher.App.UI_Forms.Main_Screen;
using SBRW.Launcher.App.UI_Forms.Verify_Hash_Screen;
using SBRW.Launcher.Core.Cache;
using SBRW.Launcher.Core.Discord.RPC_;
using SBRW.Launcher.Core.Downloader;
using SBRW.Launcher.Core.Downloader.Models_;
using SBRW.Launcher.Core.Extension.Hash_;
using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.Core.Extension.Time_;
using SBRW.Launcher.Core.Extension.Web_;
using SBRW.Launcher.Core.Extra.File_;
using SBRW.Launcher.Core.Extra.File_.Save_;
using SBRW.Launcher.Core.Theme;
using SBRW.Launcher.RunTime.InsiderKit;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using SBRW.Launcher.RunTime.LauncherCore.ModNet;
using SBRW.Launcher.RunTime.LauncherCore.Support;
using SBRW.Launcher.RunTime.LauncherCore.Visuals;
using SBRW.Launcher.RunTime.SystemPlatform.Unix;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SBRW.Launcher.App.UI_Forms.VerifyHash_Screen
{
    public partial class Screen_Verify_Hash : Form
    {
        private static bool IsVerifyHashOpen { get; set; }
        /* DEBUG NEW CODE */

        // Use a single, static HttpClient as recommended
        private static readonly HttpClient _httpClient = new HttpClient();

        private Download_Raw? _verifier;
        private CancellationTokenSource? _cts;

        // We still need this to prevent concurrent scans from the static OpenScreen method
        private static bool _isScanning;

        public static void OpenScreen()
        {
            if (IsVerifyHashOpen || Application.OpenForms["Screen_Verify_Hash"] != null)
            {
                if (!Application.OpenForms["Screen_Verify_Hash"].IsDisposed) { Application.OpenForms["Screen_Verify_Hash"].Activate(); }
            }
            else
            {
                try { new Screen_Verify_Hash().ShowDialog(); }
                catch (Exception Error)
                {
                    string ErrorMessage = "Verify Hash Screen Encountered an Error";
                    LogToFileAddons.OpenLog("Verify Hash Screen", ErrorMessage, Error, "Exclamation", false);
                }
            }
        }

        public Screen_Verify_Hash()
        {
            IsVerifyHashOpen = true;
            Presence_Launcher.Status(24);
            InitializeComponent();
            Icon = FormsIcon.Retrive_Icon();
            SetVisuals();
            this.Closing += (x, CloseForm) =>
            {
                if (_isScanning)
                {
                    if ("Do you really want to exit the VerifyHash process?".Message_Box(MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        CloseForm.Cancel = true;
                    }
                    else
                    {
                        StopScanner_Click(null, null); // Cancel the scan
                        Log_Verify.Stop = true;
                    }
                }
                else
                {
                    Log_Verify.Stop = true;
                }

                IsVerifyHashOpen = false;
                Presence_Launcher.Status(22);
            };
        }

        private void VerifyHash_Load(object sender, EventArgs e)
        {
            VersionLabel.Text = "Version: " + Application.ProductVersion;
            Log.Core("VERIFY HASH: Opened");

            if (FunctionStatus.IsVerifyHashDisabled)
            {
                StartScanner.Enabled = false;
                VerifyHashText.Text = "Verify Hash has already done a Full Scan this run.\n" +
                                      "Please restart the GameLauncher to do a New Scan.";
                return;
            }

            // Start logging
            Log_Verify.Start();
            CleanPreviousLogs();
        }

        private void CleanPreviousLogs()
        {
            string[] filestocheck = { "checksums.dat", "validfiles.dat", "invalidfiles.dat", "Verify.log" };
            foreach (String file in filestocheck)
            {
                if (File.Exists(file))
                {
                    try { File.Delete(file); }
                    catch (Exception Error)
                    {
                        Log_Verify.Error("File: " + file + " Error: " + Error.Message);
                    }
                }
            }
        }

        private async void StartScanner_Click(object sender, EventArgs e)
        {
            if (_isScanning) return;

            // Disable scanning for this launcher session
            FunctionStatus.IsVerifyHashDisabled = true;
            _isScanning = true;
            StartScanner.Visible = false;
            StopScanner.Visible = true;
            Presence_Launcher.Status(25);

            // UI setup for scanning
            VerifyHashText.ForeColor = Color_Winform.Warning_Text_Fore_Color;
            VerifyHashText.Text = "Warning:\n Stopping the Scan before it is complete\nWill result in needing to start over!";
            ScanProgressBar.Value = 0;
            DownloadProgressBar.Value = 0;
            DownloadProgressBar.Visible = true;
            DownloadProgressText.Visible = true;

            // 1. Prompt user *before* starting
            bool cleanScripts = Save_Settings.Verify_Script_Removal();

            // 2. Configure the verifier
            var config = new Download_Raw_Config
            {
                GamePath = Save_Settings.Live_Data.Game_Path,
                CdnBaseUrl = Save_Settings.Live_Data.Launcher_CDN,
                CdnUnpackedPath = "/unpacked", // From original code
                ChecksumFileName = "checksums.dat",
                CleanScriptsFolder = cleanScripts
            };

            // Use our logger implementation
            _verifier = new Download_Raw(config, new Verify_Logger(), _httpClient);

            // 3. Set up progress reporting
            var progress = new Progress<Download_Raw_Progress_Model>(ReportProgress);
            _cts = new CancellationTokenSource();

            // 4. Run the verifier
            try
            {
                Download_Raw_Result_Model result = await _verifier.RunAsync(progress, _cts.Token);
                HandleVerificationComplete(result);
            }
            catch (Exception ex)
            {
                Log.Error("VERIFY HASH: A critical error occurred. " + ex.Message);
                VerifyHashText.Text = "A critical error occurred. Check logs.";
            }
            finally
            {
                _isScanning = false;
                StopScanner.Visible = false;
                _cts?.Dispose();
                _cts = null;
            }
        }

        private void ReportProgress(Download_Raw_Progress_Model p)
        {
            // This is called on the UI thread automatically by IProgress<T>

            // Handle scan progress
            if (p.Status == Download_Raw_Status.Scanning_Files)
            {
                ScanProgressText.Text = p.Message;
                ScanProgressBar.Value = p.Percentage;
            }
            // Handle download progress
            else if (p.Status == Download_Raw_Status.Downloading)
            {
                Presence_Launcher.Status(26);
                ScanProgressText.Text = "Scan Complete. Downloading files...";
                DownloadProgressText.Text = $"{p.Message} {p.CurrentFile}\n" +
                    $"{Time_Conversion.FormatFileSize(p.BytesDownloaded)} of {Time_Conversion.FormatFileSize(p.TotalBytesToDownload)}";
                DownloadProgressBar.Value = p.Percentage;
            }
            // Handle other status messages
            else
            {
                ScanProgressText.Text = p.Message;
            }
        }

        private void HandleVerificationComplete(Download_Raw_Result_Model result)
        {
            if (result.WasCancelled)
            {
                VerifyHashText.Text = "Verify Hash process has been Terminated";
            }
            else if (result.FilesFailedToDownload > 0)
            {
                VerifyHashText.ForeColor = Color_Winform.Warning_Text_Fore_Color;
                VerifyHashText.Text = $"{result.FilesFailedToDownload} Files Failed to Download. Check Log for Details";
                DownloadProgressText.Text = $"{result.FilesSuccessfullyDownloaded} files downloaded. {result.FilesFailedToDownload} failed.";

                // Prompt to open log
                if (("Verify Hash has encountered Download Errors.\n" +
                    "Would you like to Open Verify.Log").Message_Box(MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (File.Exists(Log_Location.LogVerify))
                    {
                        Process.Start(Log_Location.LogCurrentFolder);
                        Process.Start(Log_Location.LogVerify);
                    }
                }
            }
            else if (result.TotalFilesMissingOrInvalid == 0)
            {
                VerifyHashText.ForeColor = Color_Winform.Success_Text_Fore_Color;
                VerifyHashText.Text = "Excellent News! There are ZERO\nmissing or invalid Gamefiles!";
                DownloadProgressBar.Visible = false;
                DownloadProgressText.Visible = false;
                SetGameIntegrityGood();
            }
            else // Downloads finished successfully
            {
                VerifyHashText.ForeColor = Color_Winform.Success_Text_Fore_Color;
                VerifyHashText.Text = "Yay! Scanning and Downloading\n is now completed on Gamefiles";
                DownloadProgressText.Text = $"{result.FilesSuccessfullyDownloaded} Invalid/Missing File(s) were downloaded";
                SetGameIntegrityGood();
            }
        }

        private void SetGameIntegrityGood()
        {
            Presence_Launcher.Status(27);
            Save_Settings.Live_Data.Game_Integrity = "Good";
            Save_Settings.Save();

            // Update main screen icon
            if (Screen_Main.Screen_Instance != null)
            {
                Screen_Main.Screen_Instance.Button_Settings.SafeInvokeAction(() =>
                    Screen_Main.Screen_Instance.Button_Settings.BackgroundImage =
                    Screen_Main.Screen_Instance.Button_Settings.Icon_Order(SVG_Icon.Gear, SVG_Color.White));
            }
        }

        private void StopScanner_Click(object? sender, EventArgs? e)
        {
            StartScanner.SafeInvokeAction(() => StartScanner.Visible = false);
            StopScanner.SafeInvokeAction(() => StopScanner.Visible = false);

            if (_isScanning && _cts != null)
            {
                _cts.Cancel(); // Request cancellation
                Log.Info("VERIFY HASH: User requested stop.");
                VerifyHashText.Text = "Stopping... Please wait.";
                StopScanner.Enabled = false; // Prevent multiple clicks
            }
        }

        private void SetVisuals()
        {
            /*******************************/
            /* Set Font                     /
            /*******************************/
#if !(RELEASE_UNIX || DEBUG_UNIX)
            float MainFontSize = 9f * 96f / CreateGraphics().DpiY;
#else
            float MainFontSize = 9f;
#endif
            Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);

            VerifyHashWelcome.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            ScanProgressText.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            DownloadProgressText.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            StartScanner.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            StopScanner.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            VerifyHashText.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            VersionLabel.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);

            /********************************/
            /* Set Theme Colors              /
            /********************************/

            ForeColor = Color_Winform.Text_Fore_Color;
            BackColor = Color_Winform.BG_Fore_Color;

            DownloadProgressText.ForeColor = Color_Winform.Text_Fore_Color;
            ScanProgressText.ForeColor = Color_Winform.Text_Fore_Color;

            VerifyHashWelcome.ForeColor = Color_Winform.Secondary_Text_Fore_Color;
            VerifyHashText.ForeColor = Color_Winform.Success_Text_Fore_Color;

            VersionLabel.ForeColor = Color_Winform.Text_Fore_Color;

            StartScanner.ForeColor = Color_Winform.Success_Text_Fore_Color;
            StartScanner.BackColor = Color_Winform_Buttons.Blue_Back_Color;
            StartScanner.FlatAppearance.BorderColor = Color_Winform_Buttons.Blue_Border_Color;
            StartScanner.FlatAppearance.MouseOverBackColor = Color_Winform_Buttons.Blue_Mouse_Over_Back_Color;

            StopScanner.ForeColor = Color_Winform.Warning_Text_Fore_Color;
            StopScanner.BackColor = Color_Winform_Buttons.Blue_Back_Color;
            StopScanner.FlatAppearance.BorderColor = Color_Winform_Buttons.Blue_Border_Color;
            StopScanner.FlatAppearance.MouseOverBackColor = Color_Winform_Buttons.Blue_Mouse_Over_Back_Color;

            /********************************/
            /* Events Handlers               /
            /********************************/

            StartScanner.Click += new EventHandler(StartScanner_Click);
            StopScanner.Click += new EventHandler(StopScanner_Click);

            /********************************/
            /* Hardcoded Text [Linux Fix]    /
            /********************************/

            VerifyHashWelcome.Text = "Welcome!\n\nThe scanning process is pretty quick,\nbut may still take a while." +
                "\nDepending on your connection,\nre-downloading will take the longest\nPlease allow it to complete fully!";
            ScanProgressText.Text = "Scanning Progress:";
            DownloadProgressText.Text = "Download Progress:";
            VerifyHashText.Text = "Please select \"Start Scan\" \nTo begin Validating Gamefiles";

            Shown += (x, y) =>
            {
                Application.OpenForms[this.Name].Activate();
                this.BringToFront();
            };
        }
    }
}
