using GameLauncher.App.Classes.LauncherCore.Client.Auth;
using GameLauncher.App.Classes.LauncherCore.Client.Web;
using GameLauncher.App.Classes.LauncherCore.Global;
using GameLauncher.App.Classes.LauncherCore.Hashes;
using GameLauncher.App.Classes.LauncherCore.Visuals;
using GameLauncher.App.Classes.LauncherCore.ModNet;
using GameLauncher.App.Classes.LauncherCore.Lists.JSON;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using GameLauncher.App.Classes.LauncherCore.Lists;
using GameLauncher.App.Classes.LauncherCore.RPC;
using GameLauncher.App.Classes.LauncherCore.Client.Game;
using GameLauncher.App.Classes.LauncherCore.FileReadWrite;
using GameLauncher.App.Classes.LauncherCore.Validator;
using GameLauncher.App;

namespace GameLauncher
{
    public partial class Form1 : Form 
    {
        /* START Login Checks */
        public static bool ModernAuthSupport = false;
        /* END Login Checks */

        /* START ModNet Global Functions */
        public static String ModNetFileNameInUse = String.Empty;
        readonly Queue<Uri> modFilesDownloadUrls = new Queue<Uri>();
        bool isDownloadingModNetFiles = false;
        public int CurrentModFileCount = 0;
        public int TotalModFileCount = 0;
        /* END ModNet Global Functions */

        /* START GetServerInformation Cache */
        public static JSONNode result;
        /* END GetServerInformation Cache  */

        /* START Selected Server Cache */

        public static JsonServerList SelectedServer = null;
        public static string SelectedServerName = "Unknown";
        public static string SelectedServerIP = "http://localhost";
        /* END Selected Server Cache */

        public static string GameFiles = FileSettingsSave.GameInstallation;
        public static string LinksFile = Path.Combine(GameFiles + "\\.links");
        public Form1() 
        {
            InitializeComponent();

            Load += new EventHandler(Form1_Load);
            ServerDropDownList.SelectedIndexChanged += new EventHandler(ServerPick_SelectedIndexChanged);
            ServerDropDownList.DrawItem += new DrawItemEventHandler(FunctionEvents.ServerDropDownList_DrawItem);
            ForgotPassLink.LinkClicked += new LinkLabelLinkClickedEventHandler(FunctionEvents.ForgotPass_LinkClicked);

            ActionText.Text = "Ready!";
        }

        private void Form1_Load(object sender, EventArgs e) 
        {
            ServerDropDownList.DisplayMember = "Name";
            ServerDropDownList.DataSource = ServerListUpdater.CleanList;

            if (!string.IsNullOrEmpty(FileAccountSave.ChoosenGameServer))
            {

                if (ServerListUpdater.CleanList.FindIndex(i => string.Equals(i.IpAddress, FileAccountSave.ChoosenGameServer)) != 0)
                {
                    var index = ServerListUpdater.CleanList.FindIndex(i => string.Equals(i.IpAddress, FileAccountSave.ChoosenGameServer));

                    if (index >= 0)
                    {
                        ServerDropDownList.SelectedIndex = index;
                    }
                }
                else
                {
                    ServerDropDownList.SelectedIndex = 1;
                }

                if (ServerDropDownList.SelectedIndex == 1)
                {
                    ServerPick_SelectedIndexChanged(sender, e);
                }
            }
            else
            {
                ServerDropDownList.SelectedIndex = 1;
            }
        }

        private void ServerPick_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((JsonServerList)ServerDropDownList.SelectedItem).IsSpecial)
            {
                ServerDropDownList.SelectedIndex = 1;
                return;
            }
            else
            {
                Tokens.Clear();
                ActionText.Text = "Loading info...";

                try
                {
                    LoginButton.Enabled = true;
                    RegisterButton.Enabled = true;

                    SelectedServer = (JsonServerList)ServerDropDownList.SelectedItem;
                    SelectedServerName = SelectedServer.Name;
                    SelectedServerIP = SelectedServer.IpAddress;

                    WebClientWithTimeout serverval = new WebClientWithTimeout();
                    var stringToUri = new Uri(SelectedServerIP + "/GetServerInformation");
                    String serverdata = serverval.DownloadString(stringToUri);

                    result = JSON.Parse(serverdata);

                    ActionText.Text = "Players on server: " + result["onlineNumber"];

                    try
                    {
                        if (string.IsNullOrEmpty(result["modernAuthSupport"]))
                        {
                            ModernAuthSupport = false;
                        }
                        else if (result["modernAuthSupport"])
                        {
                            if (stringToUri.Scheme == "https")
                            {
                                ModernAuthSupport = true;
                            }
                            else
                            {
                                ModernAuthSupport = false;
                            }
                        }
                        else
                        {
                            ModernAuthSupport = false;
                        }
                    }
                    catch
                    {
                        ModernAuthSupport = false;
                    }

                    try
                    {
                        ScreenRegister.TicketRequired = (bool)result["requireTicket"];
                    }
                    catch
                    {
                        ScreenRegister.TicketRequired = true; //lets assume yes, we gonna check later if ticket is empty or not.
                    }
                }
                catch
                {
                    LoginButton.Enabled = false;
                    RegisterButton.Enabled = false;

                    SelectedServerName = "Offline";
                    SelectedServerIP = "http://localhost";

                    ActionText.Text = "Server is offline.";
                }
            }
        }

        private void LoginButton_Click(object sender, EventArgs e) 
        {
            Tokens.Clear();
            if (!IsValid.Email(LoginEmailBox.Text)) 
            {
                ActionText.Text = "Please type your email!";
            } 
            else if (String.IsNullOrEmpty(LoginPasswordBox.Text)) 
            {
                ActionText.Text = "Please type your password!";
            } 
            else 
            {
                Tokens.IPAddress = SelectedServerIP;
                Tokens.ServerName = SelectedServerName;

                if (ModernAuthSupport == false) 
                {
                    ClassicAuth.Login(LoginEmailBox.Text, SHA.HashPassword(LoginPasswordBox.Text).ToLower());
                } else 
                {
                    ModernAuth.Login(LoginEmailBox.Text, LoginPasswordBox.Text);
                }

                if (String.IsNullOrEmpty(Tokens.Error)) 
                {
                    if (!String.IsNullOrEmpty(Tokens.Warning)) 
                    {
                        MessageBox.Show(null, Tokens.Warning, UserAgent.AgentAltName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    //TODO: MODS GOES HERE
                    DoModNetJob();
                    //
                } 
                else 
                {
                    MessageBox.Show(null, Tokens.Error, UserAgent.AgentAltName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ActionText.Text = (String.IsNullOrEmpty(Tokens.Error)) ? "An error occurred." : Tokens.Error;
                }
            }
        }

        private void RegisterButton_Click(object sender, EventArgs e) 
        {
            if (SelectedServerName == "WorldUnited OFFICIAL")
            {
                Process.Start(result["homePageUrl"]);
                MessageBox.Show(null, "A browser window has been opened to complete registration on " + SelectedServerName, UserAgent.AgentAltName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                new ScreenRegister().ShowDialog();
            }
        }

        private string FormatFileSize(long byteCount) 
        {
            var numArray = new double[] { 1000000000, 1000000, 1000, 0 };
            var strArrays = new[] { "GB", "MB", "KB", "Bytes" };
            for (var i = 0; i < numArray.Length; i++) {
                if (byteCount >= numArray[i]) {
                    return string.Concat($"{byteCount / numArray[i]:0.00} ", strArrays[i]);
                }
            }

            return "0 Bytes";
        }

        void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) 
        {
            this.BeginInvoke((MethodInvoker)delegate {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                //ActionText.Text = ("Downloaded " + FormatFileSize(e.BytesReceived) + " of " + FormatFileSize(e.TotalBytesToReceive));
                ActionText.Text = ("Downloading [" + CurrentModFileCount + " / " + TotalModFileCount + "] : " + FormatFileSize(e.BytesReceived) + " of " + FormatFileSize(e.TotalBytesToReceive));
            });
        }

        public void DoModNetJob() 
        {
            /* Disable Any Buttons */
            ServerDropDownList.Enabled = false;
            LoginButton.Enabled = false;
            RegisterButton.Enabled = false;
            ForgotPassLink.Enabled = false;

            if (Directory.Exists(GameFiles + "\\modules")) Directory.Delete(GameFiles + "\\modules", true);
            if (!Directory.Exists(GameFiles + "\\scripts")) Directory.CreateDirectory(GameFiles + "\\scripts");

            String[] RemoveAllFiles = new string[] { "modules/udpcrc.soapbox.module", "modules/udpcrypt1.soapbox.module", "modules/udpcrypt2.soapbox.module", 
                "modules/xmppsubject.soapbox.module", "scripts/global.ini", "lightfx.dll", "PocoFoundation.dll", "PocoNet.dll", "ModManager.dat"};

            foreach (string file in RemoveAllFiles) 
            {
                if (File.Exists(GameFiles + file)) 
                {
                    try {
                        File.Delete(GameFiles + file);
                    } catch {
                        MessageBox.Show($"File {GameFiles + file} cannot be deleted.", "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            
            String jsonModNet = ModNetReloaded.ModNetSupported(SelectedServerIP);

            if (jsonModNet != String.Empty) 
            {
                ActionText.Text = "Detecting ModNetSupport for " + ServerDropDownList.SelectedItem.ToString();

                try 
                {
                    /* Get Remote ModNet list to process for checking required ModNet files are present and current */
                    String modules = new WebClient().DownloadString(URLs.ModNet + "/launcher-modules/modules.json");
                    string[] modules_newlines = modules.Split(new string[] { "\n" }, StringSplitOptions.None);

                    foreach (String modules_newline in modules_newlines)
                    {
                        if (modules_newline.Trim() == "{" || modules_newline.Trim() == "}") continue;

                        String trim_modules_newline = modules_newline.Trim();
                        String[] modules_files = trim_modules_newline.Split(new char[] { ':' });

                        String ModNetList = modules_files[0].Replace("\"", "").Trim();
                        String ModNetSHA = modules_files[1].Replace("\"", "").Replace(",", "").Trim();

                        if (SHATwoFiveSix.HashFile(FileSettingsSave.GameInstallation + "\\" + ModNetList).ToLower() != ModNetSHA || !File.Exists(FileSettingsSave.GameInstallation + "\\" + ModNetList))
                        {
                            ActionText.Text = ("ModNet: Downloading " + ModNetList).ToUpper();

                            if (File.Exists(FileSettingsSave.GameInstallation + "\\" + ModNetList))
                            {
                                File.Delete(FileSettingsSave.GameInstallation + "\\" + ModNetList);
                            }

                            WebClient newModNetFilesDownload = new WebClient();
                            newModNetFilesDownload.DownloadFile(URLs.ModNet + "/launcher-modules/" + ModNetList, FileSettingsSave.GameInstallation + "/" + ModNetList);
                        }
                        else
                        {
                            ActionText.Text = ("ModNet: Up to Date " + ModNetList).ToUpper();
                        }

                        Application.DoEvents();
                    }

                    JSONNode MainJson = JSON.Parse(jsonModNet);

                    Uri newIndexFile = new Uri(MainJson["basePath"] + "/index.json");
                    String jsonindex = new WebClient().DownloadString(newIndexFile);

                    JSONNode IndexJson = JSON.Parse(jsonindex);

                    String path = Path.Combine(FileSettingsSave.GameInstallation, "MODS", MDFive.HashPassword(MainJson["serverID"]).ToLower());
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    /* new */
                    foreach (JSONNode modfile in IndexJson["entries"])
                    {
                        if (SHA.HashFile(path + "/" + modfile["Name"]).ToLower() != modfile["Checksum"])
                        {
                            modFilesDownloadUrls.Enqueue(new Uri(MainJson["basePath"] + "/" + modfile["Name"]));
                            TotalModFileCount++;

                            if (File.Exists(path + "/" + modfile["Name"]))
                            {
                                File.Delete(path + "/" + modfile["Name"]);
                            }
                        }
                    }

                    if (modFilesDownloadUrls.Count != 0)
                    {
                        this.DownloadModNetFilesRightNow(path);
                    }
                    else
                    {
                        LaunchGame();
                    }
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            } 
            else 
            {
                //Yikes from me Coders - DavidCarbon
                LaunchGame();
            }
        }

        public void DownloadModNetFilesRightNow(string path)
        {
            while (isDownloadingModNetFiles == false)
            {
                CurrentModFileCount++;
                var url = modFilesDownloadUrls.Dequeue();
                string FileName = url.ToString().Substring(url.ToString().LastIndexOf("/") + 1, (url.ToString().Length - url.ToString().LastIndexOf("/") - 1));

                ModNetFileNameInUse = FileName;

                try
                {
                    WebClient client2 = new WebClient();

                    client2.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Client_DownloadProgressChanged);
                    client2.DownloadFileCompleted += (test, stuff) =>
                    {
                        isDownloadingModNetFiles = false;
                        if (modFilesDownloadUrls.Any() == false)
                        {
                            LaunchGame();
                        }
                        else
                        {
                            //Redownload other file
                            DownloadModNetFilesRightNow(path);
                        }
                    };
                    client2.DownloadFileAsync(url, path + "/" + FileName);
                }
                catch (Exception error)
                {
                    ActionText.Text = error.Message;
                }

                isDownloadingModNetFiles = true;
            }
        }

        private void LaunchGame()
        {
            ActionText.Text = "Launching game...";
            WindowState = FormWindowState.Minimized;
            Launch.Game();
        }

        public static void LauncherState(string Function)
        {
            DiscordLauncherPresense.Stop();
            if (File.Exists(LinksFile))
            {
                ModNetLinksCleanup.CleanLinks(LinksFile);
            }

            if (Function == "Launcher Restart")
            {
                Application.Restart();
                Application.ExitThread();
            }
            else if (Function == "Launcher Shutdown")
            {
                Application.Exit();
            }
            else
            {
                Log.Error("CORE: Unable to Determine Function state for Launcher");
            }
        }
    }
}
