using SBRW.Launcher.Core.Discord.RPC_;
using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.Support;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SBRW.Launcher.Core.Extra.File_.Save_;
using SBRW.Launcher.RunTime.LauncherCore.ModNet;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using System.Net;
using System.Net.Cache;
using SBRW.Launcher.Core.Cache;
using SBRW.Launcher.Core.Extension.Hash_;
using SBRW.Launcher.App.UI_Forms.Main_Screen;
using SBRW.Launcher.RunTime.LauncherCore.Visuals;
using SBRW.Launcher.Core.Extension.Web_;
using System.ComponentModel;
using SBRW.Launcher.Core.Reference.Json_.Newtonsoft_;
using SBRW.Launcher.RunTime.LauncherCore.Lists.JSON;
using System.Net.Http;

namespace SBRW.Launcher.App.UI_Forms.Settings_Screen
{
    partial class Screen_Settings
    {
        public BackgroundWorker Start()
        {
            BackgroundWorker Creator = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            Creator.DoWork += Start_File_Scan;
            Creator.RunWorkerCompleted += Start_File_Scan_Completed;

            return Creator;
        }
        /// <summary>
        /// 
        /// </summary>
        /// TODO: UPDATE TEXT FOR TRANSLATIONS
        private void Start_File_Scan(object _, DoWorkEventArgs Live_Events)
        {
            if (!string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Game_Path))
            {
                Verify_Hash_Status = Raw_Download_Progress.Scanning;
                Presence_Launcher.Status(25);
                Log.Info("VERIFY HASH: Checking and Deleting '.orig' Files and Symbolic Folders");
                //Label_Verify_Scan.SafeInvokeAction(() => Label_Verify_Scan.Text = "Removing any '.orig' Files in Game Directory");

                DirectoryInfo Game_Files_Directory = new DirectoryInfo(Save_Settings.Live_Data.Game_Path);
                /* */
                if (Game_Files_Directory.Exists)
                {
                    /* */
                    foreach (FileInfo Matched_File in Game_Files_Directory.EnumerateFiles("*.orig", SearchOption.AllDirectories))
                    {
                        if (!Live_Events.Cancel)
                        {
                            Generated_Scanned_List.Add(new Json_List_Scanned_Game_Files() { File_Info = Matched_File });
                        }
                        else
                        {
                            break;
                        }
                    }
                    /* */
                    foreach (DirectoryInfo Found_Directory in Game_Files_Directory.EnumerateDirectories())
                    {
                        if (!Live_Events.Cancel)
                        {
                            if (ModNetHandler.IsSymbolic(Found_Directory.FullName))
                            {
                                if (Directory.Exists(Found_Directory.FullName))
                                {
                                    Generated_Scanned_List.Add(new Json_List_Scanned_Game_Files() { Directory_Info = Found_Directory });
                                }
                                else if (File.Exists(Found_Directory.FullName))
                                {
                                    Generated_Scanned_List.Add(new Json_List_Scanned_Game_Files() { File_System_Info = Found_Directory });
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    /* */
                    foreach (DirectoryInfo Found_Directory in Game_Files_Directory.GetDirectories())
                    {
                        if (!Live_Events.Cancel)
                        {
                            foreach (FileInfo Matched_File in Found_Directory.EnumerateFiles("*.orig", SearchOption.AllDirectories))
                            {
                                if (!Live_Events.Cancel)
                                {
                                    Generated_Scanned_List.Add(new Json_List_Scanned_Game_Files() { File_Info = Matched_File });
                                }
                                else
                                {
                                    break;
                                }
                            }
                            /* */
                            foreach (DirectoryInfo Found_Directories in Found_Directory.EnumerateDirectories())
                            {
                                if (!Live_Events.Cancel)
                                {
                                    if (ModNetHandler.IsSymbolic(Found_Directories.FullName))
                                    {
                                        if (Directory.Exists(Found_Directories.FullName))
                                        {
                                            Generated_Scanned_List.Add(new Json_List_Scanned_Game_Files() { Directory_Info = Found_Directories });
                                        }
                                        else if (File.Exists(Found_Directories.FullName))
                                        {
                                            Generated_Scanned_List.Add(new Json_List_Scanned_Game_Files() { File_System_Info = Found_Directories });
                                        }
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    /* */
                    DirectoryInfo Scripts_Folder = new DirectoryInfo(Path.Combine(Save_Settings.Live_Data.Game_Path, "scripts"));
                    /* */
                    if (Scripts_Folder.Exists)
                    {
                        foreach (FileInfo Scripts_files in Scripts_Folder.GetFiles())
                        {
                            if (!Live_Events.Cancel)
                            {
                                Generated_Scanned_List.Add(new Json_List_Scanned_Game_Files() { File_Info = Scripts_files, Skip = Skip_Scripts_Folder });
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_"></param>
        /// <param name="Live_Events"></param>
        private void Start_File_Scan_Completed(object _, RunWorkerCompletedEventArgs Live_Events)
        {
            if (!Live_Events.Cancelled)
            {
                Verify_Hash_Status = Raw_Download_Progress.Removing;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void Start_Removal(object _, DoWorkEventArgs Live_Events)
        {
            if (!Live_Events.Cancel)
            {
                Verify_Hash_Status = Raw_Download_Progress.Removing;
                foreach (Json_List_Scanned_Game_Files Bad_File_OR_Folder in Generated_Scanned_List)
                {
                    if (Bad_File_OR_Folder != default)
                    {
                        if (!Bad_File_OR_Folder.Skip)
                        {
                            /* FileSystemInfo can be either a File or Directory, so do two Checks to ensure we know what it is */
                            if (Bad_File_OR_Folder.File_System_Info != default)
                            {
                                try
                                {
                                    if (Directory.Exists(Bad_File_OR_Folder.File_System_Info.FullName))
                                    {
                                        Directory.Delete(Bad_File_OR_Folder.File_System_Info.FullName, true);
                                        Log_Verify.Deleted("Folder - [FSI]: " + Bad_File_OR_Folder.File_System_Info.Name);
                                    }
                                    else if (File.Exists(Bad_File_OR_Folder.File_System_Info.FullName))
                                    {
                                        File.Delete(Bad_File_OR_Folder.File_System_Info.FullName);
                                        Log_Verify.Deleted("File - [FSI]: " + Bad_File_OR_Folder.File_System_Info.Name);
                                    }
                                }
                                catch (Exception Error)
                                {
                                    Files_Deletion_Error_Total++;
                                    Log_Verify.Error("FSI: " + Bad_File_OR_Folder.File_System_Info.Name + " Error: " + Error.Message);
                                    Log_Verify.ErrorIC("FSI: " + Bad_File_OR_Folder.File_System_Info.Name + " Error: " + Error.HResult);
                                    Log_Verify.ErrorFR("FSI: " + Bad_File_OR_Folder.File_System_Info.Name + " Error: " + Error.ToString());
                                }
                            }
                            /* Detected as a File */
                            if (Bad_File_OR_Folder.File_Info != default)
                            {
                                try
                                {
                                    if (File.Exists(Bad_File_OR_Folder.File_Info.FullName))
                                    {
                                        Bad_File_OR_Folder.File_Info.Delete();
                                        Log_Verify.Deleted("File: " + Bad_File_OR_Folder.File_Info.Name);
                                    }
                                }
                                catch (Exception Error)
                                {
                                    Files_Deletion_Error_Total++;
                                    Log_Verify.Error("File: " + Bad_File_OR_Folder.File_Info.Name + " Error: " + Error.Message);
                                    Log_Verify.ErrorIC("File: " + Bad_File_OR_Folder.File_Info.Name + " Error: " + Error.HResult);
                                    Log_Verify.ErrorFR("File: " + Bad_File_OR_Folder.File_Info.Name + " Error: " + Error.ToString());
                                }
                            }
                            /* Detected as a Directory */
                            if (Bad_File_OR_Folder.Directory_Info != default)
                            {
                                try
                                {
                                    if (Directory.Exists(Bad_File_OR_Folder.Directory_Info.FullName))
                                    {
                                        Directory.Delete(Bad_File_OR_Folder.Directory_Info.FullName, true);
                                        Log_Verify.Deleted("Folder: " + Bad_File_OR_Folder.Directory_Info.Name);
                                    }
                                }
                                catch (Exception Error)
                                {
                                    Files_Deletion_Error_Total++;
                                    Log_Verify.Error("Folder: " + Bad_File_OR_Folder.Directory_Info.Name + " Error: " + Error.Message);
                                    Log_Verify.ErrorIC("Folder: " + Bad_File_OR_Folder.Directory_Info.Name + " Error: " + Error.HResult);
                                    Log_Verify.ErrorFR("Folder: " + Bad_File_OR_Folder.Directory_Info.Name + " Error: " + Error.ToString());
                                }
                            }
                        }

                        Generated_Scanned_List.Remove(Bad_File_OR_Folder);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_"></param>
        /// <param name="Live_Events"></param>
        private void Start_Removal_Completed(object _, RunWorkerCompletedEventArgs Live_Events)
        {
            if (!Live_Events.Cancelled)
            {
                Verify_Hash_Status = Raw_Download_Progress.Checksums_File;
            }
            else
            {
                Verify_Hash_Status = Raw_Download_Progress.Stopped;
            }
        }
        private void Start_Checksums_Download(object _, DoWorkEventArgs Live_Events)
        {
            if (!Live_Events.Cancel)
            {
                bool CheckSums_File_Found = "checksums.dat".Hash_SHA() == "80D272597981DABA49F5022BBF36FF302FC9D13E";

                if (CheckSums_File_Found)
                {
                    Verify_Hash_Status = Raw_Download_Progress.Checksums_File_Found;
                    /* Read Local checksums.dat */
                    File_Checksum = File.ReadAllLines("checksums.dat");
                }
                else
                {
                    /* Fetch and Read Remote checksums.dat */
                    //Label_Verify_Scan.SafeInvokeAction(() => Label_Verify_Scan.Text = "Downloading Checksums File");

                    Uri URLCall = new Uri(Verify_CDN_URL + "/unpacked/checksums.dat");
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

                    bool ErrorFree = true;

                    try
                    {
                        File_Checksum = Client.DownloadString(URLCall).Split('\n');
                    }
                    catch (Exception Error)
                    {
                        LogToFileAddons.OpenLog("VERIFY HASH CHECKSUMS", "Downloading of the Checksums File has Encountered an Error", Error, "Error", false);
                        ErrorFree = false;
                    }
                    finally
                    {
                        Client?.Dispose();
                    }

                    if (ErrorFree)
                    {
                        File.WriteAllLines("checksums.dat", File_Checksum);
                    }
                    else
                    {
                        Verify_Hash_Status = Raw_Download_Progress.Checksums_File_Error;
                    }
                }

                if (CheckSums_File_Found)
                {
                    /* We need to Verify that the CDN Supports Verify or Raw Download - DavidCarbon */
                    using (HttpClient Alpha_Client = new HttpClient())
                    {
                        try
                        {
                            Alpha_Client.Timeout = TimeSpan.FromSeconds(30);
                            HttpRequestMessage Client_Request = new HttpRequestMessage(HttpMethod.Head, Verify_CDN_URL + "/unpacked/checksums.dat");
                            HttpResponseMessage Client_Response = Alpha_Client.SendAsync(Client_Request).GetAwaiter().GetResult();
                            Verify_Hash_Status = Client_Response.IsSuccessStatusCode ? Raw_Download_Progress.Verifying : Raw_Download_Progress.Checksums_Not_Available;
                        }
                        catch
                        {
                            Verify_Hash_Status = Raw_Download_Progress.Checksums_Not_Available;
                        }
                    }
                }
            }
        }
        private void Start_Checksums_Completed(object _, RunWorkerCompletedEventArgs Live_Events)
        {
            if (!Live_Events.Cancelled)
            {
                Stage_Process = 3;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_"></param>
        /// <param name="Live_Events"></param>
        private void Start_File_Download(object _, DoWorkEventArgs Live_Events)
        {
            if (!Live_Events.Cancel)
            {
                Presence_Launcher.Status(26);
                /* START Show Redownloader Progress
                StartScanner.SafeInvokeAction(() => StartScanner.Visible = false);
                StopScanner.SafeInvokeAction(() => StopScanner.Visible = true);
                

                if (!Screen_Instance.DisposedForm())
                {
                    Screen_Instance.Label_Verify_Scan.Text = "Currently (re)downloading files. This part may take awhile depending on your connection.";
                }*/

                if (Generated_Scanned_Invalid_List.Any())
                {
                    //DownloadProgressText.SafeInvokeAction(() => DownloadProgressText.Text = "\nPreparing to Download Files");

                    Files_Total = Generated_Scanned_Invalid_List.Count;

                    foreach (Json_List_Invalid_Game_Files Found_File_Invaild in Generated_Scanned_Invalid_List)
                    {
                        if (!Live_Events.Cancel)
                        {
                            try
                            {
                                while (File_Downloading)
                                {
                                    if (Live_Events.Cancel)
                                    {
                                        break;
                                    }
                                }

                                if (!Live_Events.Cancel)
                                {
                                    Uri URLCall = new Uri(Found_File_Invaild.Download_Url);
                                    int Timeout = (int)TimeSpan.FromMinutes(5).TotalMilliseconds;

                                    if (Found_File_Invaild.Download_Url.Contains("copspeechdat"))
                                    {
                                        Timeout = (int)TimeSpan.FromMinutes(30).TotalMilliseconds;
                                    }
                                    else if (Found_File_Invaild.Download_Url.Contains("nfs09mx.mus"))
                                    {
                                        Timeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
                                    }

                                    ServicePointManager.FindServicePoint(URLCall).ConnectionLeaseTimeout = Timeout;

                                    var Client = new WebClient()
                                    {
                                        Encoding = Encoding.UTF8,
                                        CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
                                    };
                                    if (!Launcher_Value.Launcher_Alternative_Webcalls())
                                    {
                                        Client = new WebClientWithTimeout()
                                        {
                                            Encoding = Encoding.UTF8,
                                            CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
                                        };
                                    }
                                    else
                                    {
                                        Client.Headers.Add("user-agent", "SBRW Launcher " +
                                        Application.ProductVersion + " (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)");
                                    }

                                    Client.DownloadProgressChanged += (Systems, RecevingData) =>
                                    {
                                        if (RecevingData.TotalBytesToReceive >= 1 && !Live_Events.Cancel)
                                        {
                                            /*
                                            if (Screen_Instance != default)
                                            {
                                                if (!(Screen_Instance.Disposing || Screen_Instance.IsDisposed))
                                                {
                                                    Screen_Instance.Label_Verify_Scan.Text = "Currently (re)downloading files. This part may take awhile depending on your connection.";
                                                }
                                            }
                                            
                                            TextBox_Verify_Scan
                                            DownloadProgressText.SafeInvokeAction(() =>
                                            DownloadProgressText.Text = "Downloading File [ " + RedownloadedCount + " / " +
                                            CurrentCount + " ]:\n" + CurrentDownloadingFile + "\n" + Time_Conversion.FormatFileSize(RecevingData.BytesReceived) +
                                            " of " + Time_Conversion.FormatFileSize(RecevingData.TotalBytesToReceive));
                                            */
                                        }
                                        else if (Live_Events.Cancel)
                                        {
                                            Client.CancelAsync();
                                        }
                                    };
                                    Client.DownloadFileCompleted += new AsyncCompletedEventHandler(Client_DownloadFileCompleted);

                                    try
                                    {
                                        Client.DownloadFileAsync(URLCall, Found_File_Invaild.Path_Full);
                                        File_Downloading = true;
                                    }
                                    catch (Exception Error)
                                    {
                                        if (!Live_Events.Cancel) 
                                        {
                                            Files_Download_Error_Total++;
                                            File_Downloading = false;
                                        }

                                        LogToFileAddons.OpenLog("VERIFY HASH", string.Empty, Error, string.Empty, true);
                                    }
                                    finally
                                    {
                                        Client?.Dispose();
                                    }

                                    Generated_Scanned_Invalid_List.Remove(Found_File_Invaild);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            catch (Exception Error)
                            {
                                if (!Live_Events.Cancel) 
                                { 
                                    Files_Download_Error_Total++;
                                }

                                LogToFileAddons.OpenLog("VERIFY HASH", string.Empty, Error, string.Empty, true);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void Start_File_Scan_Invalid(object _, DoWorkEventArgs Live_Events)
        {
            if (!Live_Events.Cancel)
            {
                try
                {
                    FunctionStatus.IsVerifyHashDisabled = true;



                    Files_Total = getFilesToCheck.Length;
                    Files_Scanned_Total = 0;

                    for (var i = 0; i < Files_Total; i++)
                    {
                        if (!Live_Events.Cancel)
                        {
                            Generated_Scanned_Invalid_List.Add(new Json_List_Invalid_Game_Files()
                            {
                                Hash = getFilesToCheck[i].Split(' ')[0].Trim(),
                                Path_Truncated = getFilesToCheck[i].Split(' ')[1].Trim(),
                                Path_Full = Path.Combine(Save_Settings.Live_Data.Game_Path + getFilesToCheck[i].Split(' ')[1].Trim()),
                                Download_Url = Verify_CDN_URL + "/unpacked" + getFilesToCheck[i].Split(' ')[1].Trim().Replace("\\", "/")
                            });
                        }
                        else
                        {
                            break;
                        }
                    }

                    foreach (Json_List_Invalid_Game_Files Current_File_Scan in Generated_Scanned_Invalid_List)
                    {
                        if (!Live_Events.Cancel)
                        {
                            if (!File.Exists(Current_File_Scan.Path_Full))
                            {
                                Log_Verify.Missing("File: " + Current_File_Scan.Path_Truncated);
                            }
                            else
                            {
                                if (Current_File_Scan.Hash != Current_File_Scan.Path_Full.Hash_SHA().Trim())
                                {
                                    Log_Verify.Invalid("File: " + Current_File_Scan.Path_Truncated);
                                }
                                else
                                {
                                    Generated_Scanned_Invalid_List.Remove(Current_File_Scan);
                                    Log_Verify.Valid("File: " + Current_File_Scan.Path_Truncated);
                                }
                            }

                            Files_Scanned_Total++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    Log.Info("VERIFY HASH: Scan Completed");
                    if (!Generated_Scanned_Invalid_List.Any() || !Live_Events.Cancel)
                    {
                        Verify_Hash_Status = Raw_Download_Progress.Passed;
                    }
                    else
                    {
                        Log.Info("VERIFY HASH: Found Invalid or Missing Files and will Start File Downloader");
                        Verify_Hash_Status = Raw_Download_Progress.Removing;
                    }
                }
                catch (Exception Error)
                {
                    LogToFileAddons.OpenLog("VERIFY HASH", string.Empty, Error, string.Empty, true);
                }
            }
        }

        private void Integrity()
        {
            Presence_Launcher.Status(27);
            Save_Settings.Live_Data.Game_Integrity = "Good";
            Save_Settings.Save();
            /* @DavidCarbon OR @Zacam 
            * Ini File Save Error Happens Above
            */
            if (!Screen_Main.Screen_Instance.DisposedForm())
            {
                if (Screen_Main.Screen_Instance.Button_Settings.InvokeRequired)
                {
                    Screen_Main.Screen_Instance.Button_Settings.SafeInvokeAction(() =>
                    Screen_Main.Screen_Instance.Button_Settings.BackgroundImage =
                    Screen_Main.Screen_Instance.Button_Settings.Icon_Order(SVG_Icon.Gear, SVG_Color.White));
                }
                else
                {
                    Screen_Main.Screen_Instance.Button_Settings.BackgroundImage =
                        Screen_Main.Screen_Instance.Button_Settings.Icon_Order(SVG_Icon.Gear, SVG_Color.White);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
    }
}
