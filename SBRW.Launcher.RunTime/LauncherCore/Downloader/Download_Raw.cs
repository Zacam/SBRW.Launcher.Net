using SBRW.Launcher.Core.Cache;
using SBRW.Launcher.Core.Discord.RPC_;
using SBRW.Launcher.Core.Extension.Hash_;
using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.Core.Extra.File_.Save_;
using SBRW.Launcher.Core.Reference.Json_.Newtonsoft_;
using SBRW.Launcher.RunTime.LauncherCore.Lists.JSON;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using SBRW.Launcher.RunTime.LauncherCore.ModNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Cache;
using System.Net.Http;
using System.Net;
using System.Text;
using SBRW.Launcher.Core.Extension.Web_;
using System.Windows.Forms;
using System.Linq;

namespace SBRW.Launcher.RunTime.LauncherCore.Downloader
{
    public class Download_Raw
{
        public static BackgroundWorker? Thread { get; set; }
        /* VerifyHash */
        public static int Files_Total { get; set; }
        public static int Files_Scanned_Total { get; set; }
        public static int Files_Invaild_Total { get; set; }
        public static int Files_Downloaded_Total { get; set; }
        public static int Files_Download_Error_Total { get; set; }
        public static int Files_Deletion_Error_Total { get; set; }
        public static string Verify_CDN_URL { get; set; } = string.Empty;
        public static bool File_Downloading { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public static bool Skip_Scripts_Folder { get; set; }
        public static List<Json_List_Scanned_Game_Files> Generated_Scanned_List { get; set; } = new List<Json_List_Scanned_Game_Files>();
        public static List<Json_List_Invalid_Game_Files> Generated_Scanned_Invalid_List { get; set; } = new List<Json_List_Invalid_Game_Files>();
        public static string[] File_Checksum { get; set; } = { };
        public static void Start()
        {
            if (Thread != default)
            {
                Thread.CancelAsync();
            }

            Thread = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };
            Thread.DoWork += Start_Worker;
            Thread.RunWorkerCompleted += Start_Worker_Completed;
        }
        /// <summary>
        /// 
        /// </summary>
        public static void Stop()
        {
            if (Thread != default)
            {
                Thread.CancelAsync();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// TODO: UPDATE TEXT FOR TRANSLATIONS
        public static void Start_Worker(object _, DoWorkEventArgs Live_Events)
        {
            if (!string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Game_Path))
            {
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
                else if (!Game_Files_Directory.Exists)
                {
                    /* Directory Does not Exist 
                     Lets Create one - DavidCarbon */
                    Game_Files_Directory.Create();
                }

                if (!Live_Events.Cancel)
                {
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
                                        $"{Bad_File_OR_Folder.File_System_Info.Name}".Full_Verify(Error);
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
                                        $"{Bad_File_OR_Folder.File_Info.Name}".Full_Verify(Error);
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
                                        $"{Bad_File_OR_Folder.Directory_Info.Name}".Full_Verify(Error);
                                    }
                                }
                            }

                            Generated_Scanned_List.Remove(Bad_File_OR_Folder);
                        }
                    }
                }
                else
                {
                    /* TODO: ADD FUNCTION FOR STOP */
                    return;
                }

                bool Supported_CDN = false;

                if (!Live_Events.Cancel)
                {
                    bool CheckSums_File_Found = "checksums.dat".Hash_SHA() == "80D272597981DABA49F5022BBF36FF302FC9D13E";

                    if (CheckSums_File_Found)
                    {
                        /* TODO: ADD CHECKSUMS FOUND MESSAGE */
                        /* Read Local checksums.dat */
                        File_Checksum = File.ReadAllLines("checksums.dat");
                    }
                    else
                    {
                        /* TODO: ADD CHECKSUMS DOWNLOAD MESSAGE */
                        //Label_Verify_Scan.SafeInvokeAction(() => Label_Verify_Scan.Text = "Downloading Checksums File");

                        /* Fetch and Read Remote checksums.dat */
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
                            $"CHECKSUMS File".Full_Verify(Error);
                            $"CHECKSUMS File".Full(Error);

                            /* The Following will need to replaced or subbed in with an alternative method of displaying the information */
                            //LogToFileAddons.OpenLog("VERIFY HASH CHECKSUMS", "Downloading of the Checksums File has Encountered an Error", Error, "Error", false);
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
                            /* TODO: ADD CHECKSUMS ERROR MESSAGE */
                        }
                    }

                    if (CheckSums_File_Found)
                    {
                        /* We need to Verify that the CDN Supports Verify or Raw Download - DavidCarbon */
                        using (HttpClient Alpha_Client = new HttpClient())
                        {
                            try
                            {
                                Alpha_Client.Timeout = TimeSpan.FromSeconds(Launcher_Value.Launcher_WebCall_Timeout_Enable ?
                                    Launcher_Value.Launcher_WebCall_Timeout() : 60);
                                HttpRequestMessage Client_Request = new HttpRequestMessage(HttpMethod.Head, Verify_CDN_URL + "/unpacked/checksums.dat");
                                HttpResponseMessage Client_Response = Alpha_Client.SendAsync(Client_Request).GetAwaiter().GetResult();
                                Supported_CDN = Client_Response.IsSuccessStatusCode;
                            }
                            catch
                            {
                                Supported_CDN = false;
                            }
                        }
                    }
                }
                else
                {
                    /* TODO: ADD FUNCTION FOR STOP */
                    return;
                }

                bool Invalid_Detected = false;

                if (!Live_Events.Cancel)
                {
                    if (Supported_CDN)
                    {
                        try
                        {
                            Files_Total = File_Checksum.Length;
                            Files_Scanned_Total = 0;

                            for (var i = 0; i < Files_Total; i++)
                            {
                                if (!Live_Events.Cancel)
                                {
                                    Generated_Scanned_Invalid_List.Add(new Json_List_Invalid_Game_Files()
                                    {
                                        Hash = File_Checksum[i].Split(' ')[0].Trim(),
                                        Path_Truncated = File_Checksum[i].Split(' ')[1].Trim(),
                                        Path_Full = Path.Combine(Save_Settings.Live_Data.Game_Path + File_Checksum[i].Split(' ')[1].Trim()),
                                        Download_Url = Verify_CDN_URL + "/unpacked" + File_Checksum[i].Split(' ')[1].Trim().Replace("\\", "/")
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

                            Invalid_Detected = Generated_Scanned_Invalid_List.Any();
                            Log.Info("VERIFY HASH: Scan Completed");
                            /*
                            if (!Live_Events.Cancel)
                            {
                                Verify_Hash_Status = Raw_Download_Progress.Passed;
                            }
                            else
                            {
                                Log.Info("VERIFY HASH: Found Invalid or Missing Files and will Start File Downloader");
                                Verify_Hash_Status = Raw_Download_Progress.Removing;
                            }
                            */
                        }
                        catch (Exception Error)
                        {
                            LogToFileAddons.OpenLog("VERIFY HASH", string.Empty, Error, string.Empty, true);
                        }
                    }
                    else
                    {
                        /* TODO: ADD NOT SUPPORTED CDN MESSAGE */
                        return;
                    }
                }
                else
                {
                    /* TODO: ADD FUNCTION FOR STOP */
                    return;
                }

                if (!Live_Events.Cancel)
                {
                    if (Invalid_Detected)
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
                                            //Client.DownloadFileCompleted += new AsyncCompletedEventHandler(Client_DownloadFileCompleted);

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
                    else
                    {
                        /* TODO: ADD CLEAN FILES MESSAGE (ALSO KNOWN AS VALID FILES) */
                        return;
                    }
                }
                else
                {
                    /* TODO: ADD FUNCTION FOR STOP */
                    return;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_"></param>
        /// <param name="Live_Events"></param>
        public static void Start_Worker_Completed(object _, RunWorkerCompletedEventArgs Live_Events)
        {
            if (Live_Events.Cancelled)
            {

            }
            else
            {

            }
        }
    }
}
