using SBRW.Launcher.Core.Discord.RPC_;
using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.Core.Extra.File_.Save_;
using SBRW.Launcher.Core.Theme;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.Support;
using System;
using System.ComponentModel;
using System.IO;

namespace SBRW.Launcher.App.UI_Forms.Settings_Screen
{
    partial class Screen_Settings
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VerifyHash_Load(object sender, EventArgs e)
        {
            Log.Core("VERIFY HASH: Opened");

            if (!FunctionStatus.IsVerifyHashDisabled)
            {
                Log_Verify.Start();

                /* Clean up previous logs and start logging */
                string[] filestocheck = new string[] { "checksums.dat", "validfiles.dat", "invalidfiles.dat", "Verify.log" };
                foreach (String file in filestocheck)
                {
                    if (File.Exists(file))
                    {
                        try { File.Delete(file); }
                        catch (Exception Error)
                        {
                            DeletionError++;
                            Log_Verify.Error("File: " + file + " Error: " + Error.Message);
                            Log_Verify.ErrorIC("File: " + file + " Error: " + Error.HResult);
                            Log_Verify.ErrorFR("File: " + file + " Error: " + Error.ToString());
                        }
                    }
                }

                Log_Verify.Info("VERIFYHASH: Checking Characters in URL");
                if (Save_Settings.Live_Data.Launcher_CDN.EndsWith("/"))
                {
                    char[] charsToTrim = { '/' };
                    FinalCDNURL = Save_Settings.Live_Data.Launcher_CDN.TrimEnd(charsToTrim);
                    Log_Verify.Info("VERIFYHASH: Trimed end of CDN URL -> " + FinalCDNURL);
                }
                else
                {
                    FinalCDNURL = Save_Settings.Live_Data.Launcher_CDN;
                    Log_Verify.Info("VERIFYHASH: Choosen CDN URL -> " + FinalCDNURL);
                }
            }
            else
            {
                StartScanner.Enabled = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            StillDownloading = false;

            if (e.Error != null)
            {
                RedownloadErrorCount++;
                Log_Verify.Downloaded("File: " + CurrentDownloadingFile);
                Presence_Launcher.Status(26, RedownloadedCount + RedownloadErrorCount + " out of " + CurrentCount);

                DownloadProgressText.SafeInvokeAction(() =>
                DownloadProgressText.Text = "Failed To Download File [ " +
                RedownloadedCount + RedownloadErrorCount + " / " + CurrentCount + " ]:" + "\n" + CurrentDownloadingFile);

                DownloadProgressBar.SafeInvokeAction(() => DownloadProgressBar.Value = RedownloadedCount + RedownloadErrorCount * 100 / CurrentCount);

                Log_Verify.Error("Download for [" + CurrentDownloadingFile + "] - " +
                (e.Error != null ? (string.IsNullOrWhiteSpace(e.Error.Message) ? e.Error.ToString() : e.Error.Message) : "No Exception Error Provided"));

                if (RedownloadedCount + RedownloadErrorCount == CurrentCount)
                {
                    StartScanner.SafeInvokeAction(() => StartScanner.Visible = false);
                    Button_Verify_Scan.SafeInvokeAction(() => Button_Verify_Scan.Visible = false);

                    DownloadProgressText.SafeInvokeAction(() =>
                         DownloadProgressText.Text = "\n" + RedownloadedCount + " Invalid/Missing File(s) were Redownloaded");

                    VerifyHashText.SafeInvokeAction(() =>
                    {
                        VerifyHashText.ForeColor = Color_Winform.Warning_Text_Fore_Color;
                        VerifyHashText.Text = RedownloadErrorCount + " Files Failed to Download. Check Log for Details";
                    }, this);

                    DownloadErrorEncountered = true;
                    GameScanner(false);
                }
            }
            else if (IsVerifyHashOpen && !ForceStopScan)
            {
                RedownloadedCount++;

                Presence_Launcher.Status(26, RedownloadedCount + " out of " + CurrentCount);
                Log_Verify.Downloaded("File: " + CurrentDownloadingFile);

                DownloadProgressText.SafeInvokeAction(() =>
                DownloadProgressText.Text = "Downloaded File [ " + RedownloadedCount + " / " + CurrentCount + " ]:\n" + CurrentDownloadingFile);
                DownloadProgressBar.SafeInvokeAction(() => DownloadProgressBar.Value = RedownloadedCount * 100 / CurrentCount);

                if (RedownloadedCount == CurrentCount)
                {
                    Integrity();
                    Log.Info("VERIFY HASH: Re-downloaded Count: " + RedownloadedCount + " Current File Count: " + CurrentCount);
                    DownloadProgressText.SafeInvokeAction(() => DownloadProgressText.Text = "\n" + RedownloadedCount + " Invalid/Missing File(s) were downloaded");

                    VerifyHashText.SafeInvokeAction(() =>
                    {
                        VerifyHashText.ForeColor = Color_Winform.Warning_Text_Fore_Color;
                        VerifyHashText.Text = "Yay! Scanning and Downloading\n is now completed on Gamefiles";
                    }, this);

                    StartScanner.SafeInvokeAction(() => StartScanner.Visible = false);
                    Button_Verify_Scan.SafeInvokeAction(() => Button_Verify_Scan.Visible = false);

                    GameScanner(false);
                }
                else if (RedownloadedCount + RedownloadErrorCount == CurrentCount)
                {
                    DownloadProgressText.SafeInvokeAction(() => DownloadProgressText.Text = "\n" + RedownloadedCount + " Invalid/Missing File(s) were downloaded");

                    VerifyHashText.SafeInvokeAction(() =>
                    {
                        VerifyHashText.ForeColor = Color_Winform.Warning_Text_Fore_Color;
                        VerifyHashText.Text = RedownloadErrorCount + " Files Failed to Download. Check Log for Details";
                    }, this);

                    StartScanner.SafeInvokeAction(() => StartScanner.Visible = false);
                    Button_Verify_Scan.SafeInvokeAction(() => Button_Verify_Scan.Visible = false);

                    DownloadErrorEncountered = true;
                    GameScanner(false);
                }
            }
            else if (IsVerifyHashOpen && ForceStopScan)
            {
                Log.Info("VERIFY HASH: Download Process has Stopped");
                Presence_Launcher.Status(26, RedownloadedCount + " out of " + CurrentCount);

                DownloadProgressText.SafeInvokeAction(() =>
                DownloadProgressText.Text = "Download Stopped on File [ " +
                RedownloadedCount + " / " + CurrentCount + " ]:" + "\n" + CurrentDownloadingFile);

                DownloadProgressBar.SafeInvokeAction(() => DownloadProgressBar.Value = RedownloadedCount * 100 / CurrentCount);

                Log_Verify.Error("Download for [" + CurrentDownloadingFile + "] -  has been Cancelled");

                StartScanner.SafeInvokeAction(() => StartScanner.Visible = false);
                Button_Verify_Scan.SafeInvokeAction(() => Button_Verify_Scan.Visible = false);
                VerifyHashText.SafeInvokeAction(() =>
                {
                    VerifyHashText.Text = "Verify Hash Download Process has been Terminated";
                }, this);
            }
            else if (ForceStopScan)
            {
                Log.Info("VERIFY HASH: Download Process has Stopped");
                Log_Verify.Error("Download for [" + CurrentDownloadingFile + "] -  has been Cancelled");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartScanner_Click(object sender, EventArgs e)
        {
            StartScanner.SafeInvokeAction(() => StartScanner.Visible = false);
            Button_Verify_Scan.SafeInvokeAction(() => Button_Verify_Scan.Visible = true);

            GameScanner(true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopScanner_Click(object? sender, EventArgs? e)
        {
            StartScanner.SafeInvokeAction(() => StartScanner.Visible = false);
            Button_Verify_Scan.SafeInvokeAction(() => Button_Verify_Scan.Visible = false);

            GameScanner(false);
        }
    }
}
