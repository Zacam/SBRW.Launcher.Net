using ClassicGameLauncher.App.Classes.LauncherCore.Client.Auth;
using ClassicGameLauncher.App.Classes.LauncherCore.Client.Web;
using ClassicGameLauncher.App.Classes.LauncherCore.Global;
using ClassicGameLauncher.App.Classes.LauncherCore.Hashes;
using ClassicGameLauncher.App.Classes.LauncherCore.Visuals;
using ClassicGameLauncher.App.Classes.LauncherCore.ModNet;
using ClassicGameLauncher.App.Classes.LauncherCore.Lists.JSON;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ClassicGameLauncher.App.Classes.LauncherCore.Lists;
using GameLauncherSimplified.App.Classes.LauncherCore;

namespace ClassicGameLauncher
{
    public partial class Form1 : Form 
    {
        /* START Login Checks */
        private bool _modernAuthSupport = false;
        private bool _ticketRequired;
        /* END Login Checks */

        /* START ModNet Global Functions */
        public static String ModNetFileNameInUse = String.Empty;
        readonly Queue<Uri> modFilesDownloadUrls = new Queue<Uri>();
        bool isDownloadingModNetFiles = false;
        int CurrentModFileCount = 0;
        int TotalModFileCount = 0;
        /* END ModNet Global Functions */

        /* START SpeedBug Timer */
        public static bool GameKilledBySpeedBugCheck = false;
        private int _nfswPid;
        public static int secondsToShutDown = 0;
        public static bool CheatsWasUsed = false;
        /* END SpeedBug Timer */

        /* START GetServerInformation Cache */
        JSONNode result;
        /* END GetServerInformation Cache  */

        /* START Selected Server Cache */
        public static string SelectedServerName = "Unknown";
        public static string SelectedServerIP = "localhost";
        public static string SelectedServerIPRaw = "http://localhost";
        /* END Selected Server Cache */

        private static string GameFiles = AppDomain.CurrentDomain.BaseDirectory;
        private static string LinksFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\.links");
        public Form1() 
        {
            InitializeComponent();

            Load += new EventHandler(Form1_Load);
            ServerDropDownList.SelectedIndexChanged += new EventHandler(serverPick_SelectedIndexChanged);

            actionText.Text = "Ready!";
        }

        private void Form1_Load(object sender, EventArgs e) 
        {
            ServerDropDownList.DisplayMember = "Text";
            ServerDropDownList.ValueMember = "Value";

            ServerDropDownList.DataSource = ServerListUpdater.ServerList;
            ServerDropDownList.SelectedIndex = 0;
        }

        private void serverPick_SelectedIndexChanged(object sender, EventArgs e) 
        {
            Tokens.Clear();
            actionText.Text = "Loading info...";

            try 
            {
                ButtonLogin.Enabled = true;
                ButtonRegister.Enabled = true;

                SelectedServerName = ServerDropDownList.Text.ToString().ToUpper();
                SelectedServerIP = new Uri(ServerDropDownList.SelectedValue.ToString()).Host;
                SelectedServerIPRaw = ServerDropDownList.SelectedValue.ToString();

                WebClientWithTimeout serverval = new WebClientWithTimeout();
                var stringToUri = new Uri(ServerDropDownList.SelectedValue.ToString() + "/GetServerInformation");
                String serverdata = serverval.DownloadString(stringToUri);

                result = JSON.Parse(serverdata);

                actionText.Text = "Players on server: " + result["onlineNumber"];

                try 
                {
                    if (string.IsNullOrEmpty(result["modernAuthSupport"])) 
                    {
                        _modernAuthSupport = false;
                    } 
                    else if (result["modernAuthSupport"]) 
                    {
                        if (stringToUri.Scheme == "https") 
                        {
                            _modernAuthSupport = true;
                        } 
                        else 
                        {
                            _modernAuthSupport = false;
                        }
                    } 
                    else 
                    {
                        _modernAuthSupport = false;
                    }
                } 
                catch 
                {
                    _modernAuthSupport = false;
                }

                try 
                {
                    _ticketRequired = (bool)result["requireTicket"];
                } 
                catch 
                {
                    _ticketRequired = true; //lets assume yes, we gonna check later if ticket is empty or not.
                }
            } 
            catch 
            {
                ButtonLogin.Enabled = false;
                ButtonRegister.Enabled = false;

                SelectedServerName = "Offline";
                SelectedServerIP = "http://localhost";

                actionText.Text = "Server is offline.";
            }

            ticketBox.Enabled = _ticketRequired;
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            Tokens.Clear();
            if (!validateEmail(loginEmailBox.Text)) 
            {
                actionText.Text = "Please type your email!";
            } 
            else if (String.IsNullOrEmpty(loginPasswordBox.Text)) 
            {
                actionText.Text = "Please type your password!";
            } 
            else 
            {
                Tokens.IPAddress = ServerDropDownList.SelectedValue.ToString();
                Tokens.ServerName = ServerDropDownList.SelectedItem.ToString();

                if (_modernAuthSupport == false) 
                {
                    ClassicAuth.Login(loginEmailBox.Text, SHA.HashPassword(loginPasswordBox.Text).ToLower());
                } else 
                {
                    ModernAuth.Login(loginEmailBox.Text, loginPasswordBox.Text);
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
                    actionText.Text = (String.IsNullOrEmpty(Tokens.Error)) ? "An error occurred." : Tokens.Error;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) 
        {
            if (SelectedServerName == "WORLDUNITED OFFICIAL")
            {
                Process.Start(result["homePageUrl"]);
                MessageBox.Show(null, "A browser window has been opened to complete registration on " + SelectedServerName, UserAgent.AgentAltName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (!validateEmail(registerEmail.Text)) 
            {
                actionText.Text = "Please type your email!";
            } 
            else if (String.IsNullOrEmpty(registerPassword.Text)) 
            {
                actionText.Text = "Please type your password!";
            } 
            else if (String.IsNullOrEmpty(registerPassword2.Text)) 
            {
                actionText.Text = "Please type your confirmation password!";
            } 
            else if (registerPassword.Text != registerPassword2.Text) 
            {
                actionText.Text = "Password doesn't match!";
            } 
            else if(_ticketRequired) 
            {
                if(String.IsNullOrEmpty(ticketBox.Text)) 
                {
                    actionText.Text = "Ticket is required to play on this server!";
                } 
                else 
                {
                    createAccount();
                }
            } 
            else 
            {
                createAccount();
            }
        }

        private void createAccount() 
        {
            String token = (_ticketRequired) ? ticketBox.Text : null;
            Tokens.IPAddress = ServerDropDownList.SelectedValue.ToString();
            Tokens.ServerName = ServerDropDownList.SelectedItem.ToString();

            if (_modernAuthSupport == false)
            {
                ClassicAuth.Register(registerEmail.Text, SHA.HashPassword(registerPassword.Text), token);
            }
            else
            {
                ModernAuth.Register(registerEmail.Text, registerPassword.Text, token);
            }

            if (!String.IsNullOrEmpty(Tokens.Success))
            {
                MessageBox.Show(null, Tokens.Success, UserAgent.AgentAltName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                actionText.Text = Tokens.Success;

                tabControl1.Visible = true;
            }
            else
            {
                MessageBox.Show(null, Tokens.Error, UserAgent.AgentAltName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                actionText.Text = Tokens.Error;
            }
        }

        public static bool validateEmail(string email) 
        {
            if (String.IsNullOrEmpty(email)) return false;

            String theEmailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                   + "@"
                                   + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

            return Regex.IsMatch(email, theEmailPattern);
        }

        private void LaunchGame()
        {
            WindowState = FormWindowState.Minimized;

            var args = "EU " + Tokens.IPAddress + " " + Tokens.LoginToken + " " + Tokens.UserId;
            var psi = new ProcessStartInfo();


            psi.WorkingDirectory = Directory.GetCurrentDirectory();
            psi.FileName = "nfsw.exe";
            psi.Arguments = args;

            var nfswProcess = Process.Start(psi);
            nfswProcess.PriorityClass = ProcessPriorityClass.AboveNormal;

            var processorAffinity = 0;
            for (var i = 0; i < Math.Min(Math.Max(1, Environment.ProcessorCount), 8); i++)
            {
                processorAffinity |= 1 << i;
            }

            nfswProcess.ProcessorAffinity = (IntPtr)processorAffinity;

            AntiCheat.process_id = nfswProcess.Id;
            AntiCheat.Checks();

            //TIMER HERE
            secondsToShutDown = (result["secondsToShutDown"].AsInt == 0) ? result["secondsToShutDown"].AsInt : 2 * 60 * 60;
            System.Timers.Timer shutdowntimer = new System.Timers.Timer();
            shutdowntimer.Elapsed += (x2, y2) =>
            {
                Process[] allOfThem = Process.GetProcessesByName("nfsw");

                if (secondsToShutDown <= 0)
                {
                    GameKilledBySpeedBugCheck = true;

                    foreach (var oneProcess in allOfThem)
                    {
                        Process.GetProcessById(oneProcess.Id).Kill();
                    }
                }

                //change title

                foreach (var oneProcess in allOfThem)
                {
                    long p = oneProcess.MainWindowHandle.ToInt64();
                    TimeSpan t = TimeSpan.FromSeconds(secondsToShutDown);
                    string secondsToShutDownNamed = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);

                    User32.SetWindowText((IntPtr)p, "NEED FOR SPEED™ WORLD | Server: " + SelectedServerName + " | " + UserAgent.WindowTextForGame + " | Force Restart In: " + secondsToShutDownNamed);
                }

                --secondsToShutDown;
            };

            shutdowntimer.Interval = 1000;
            shutdowntimer.Enabled = true;

            if (nfswProcess != null)
            {
                nfswProcess.EnableRaisingEvents = true;
                _nfswPid = nfswProcess.Id;

                nfswProcess.Exited += (sender2, e2) =>
                {
                    _nfswPid = 0;
                    var exitCode = nfswProcess.ExitCode;

                    if (GameKilledBySpeedBugCheck == true && CheatsWasUsed == false) exitCode = 2137;
                    if (CheatsWasUsed == true) exitCode = 2017;

                    if (exitCode == 0)
                    {
                        CloseButton(null, null);
                    }
                    else
                    {
                        try
                        {
                            AntiCheat.thread.Abort();
                        }
                        catch { }

                        String errorMsg = "Game Crash with exitcode: " + exitCode.ToString() + " (0x" + exitCode.ToString("X") + ")";
                        if (exitCode == -1073741819) errorMsg = "Game Crash: Access Violation (0x" + exitCode.ToString("X") + ")";
                        if (exitCode == -1073740940) errorMsg = "Game Crash: Heap Corruption (0x" + exitCode.ToString("X") + ")";
                        if (exitCode == -1073740791) errorMsg = "Game Crash: Stack buffer overflow (0x" + exitCode.ToString("X") + ")";
                        if (exitCode == -805306369) errorMsg = "Game Crash: Application Hang (0x" + exitCode.ToString("X") + ")";
                        if (exitCode == -1073741515) errorMsg = "Game Crash: Missing dependency files (0x" + exitCode.ToString("X") + ")";
                        if (exitCode == -1073740972) errorMsg = "Game Crash: Debugger crash (0x" + exitCode.ToString("X") + ")";
                        if (exitCode == -1073741676) errorMsg = "Game Crash: Division by Zero (0x" + exitCode.ToString("X") + ")";
                        if (exitCode == 1) errorMsg = "The process nfsw.exe was killed via Task Manager";
                        if (exitCode == 2017) errorMsg = "Server replied with Code: " + Tokens.UserId + " (0x" + exitCode.ToString("X") + ")";
                        if (exitCode == 2137) errorMsg = "Launcher killed your game to prevent SpeedBugging.";
                        if (exitCode == -3) errorMsg = "The Server was unable to resolve the request";
                        if (exitCode == -4) errorMsg = "Another instance is already executed";
                        if (exitCode == -5) errorMsg = "DirectX Device was not found. Please install GPU Drivers before playing";
                        if (exitCode == -6) errorMsg = "Server was unable to resolve your request";
                        //ModLoader
                        if (exitCode == 2) errorMsg = "ModNet: Game was launched with invalid command line parameters.";
                        if (exitCode == 3) errorMsg = "ModNet: .links file should not exist upon startup!";
                        if (exitCode == 4) errorMsg = "ModNet: An Unhandled Error Appeared";

                        if (_nfswPid != 0)
                        {
                            try
                            {
                                Process.GetProcessById(_nfswPid).Kill();
                            }
                            catch { /* ignored */ }
                        }

                        DialogResult restartApp = MessageBox.Show(null, errorMsg + "\nWould you like to restart the launcher?", "GameLauncher", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (restartApp == DialogResult.Yes)
                        {
                            Application.Restart();
                            Application.ExitThread();
                        }
                        else
                        {
                            CloseButton(null,null);
                        }
                    }
                };
            }
        }

        private void CloseButton(object sender, DownloadProgressChangedEventArgs e)
        {
            if (File.Exists(LinksFile))
            {
                ModNetLinksCleanup.CleanLinks(LinksFile);
            }

            Close();
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

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) 
        {
            this.BeginInvoke((MethodInvoker)delegate {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                actionText.Text = ("Downloaded " + FormatFileSize(e.BytesReceived) + " of " + FormatFileSize(e.TotalBytesToReceive));
            });
        }

        public void DoModNetJob() 
        {
            /* Disable Any Buttons */
            ServerDropDownList.Enabled = false;
            ButtonLogin.Enabled = false;
            ButtonRegister.Enabled = false;
            ForgotPassLink.Enabled = false;

            if (Directory.Exists("modules")) Directory.Delete("modules", true);
            if (!Directory.Exists("scripts")) Directory.CreateDirectory("scripts");

            String[] RemoveAllFiles = new string[] { "modules/udpcrc.soapbox.module", "modules/udpcrypt1.soapbox.module", "modules/udpcrypt2.soapbox.module", 
                "modules/xmppsubject.soapbox.module", "scripts/global.ini", "lightfx.dll", "PocoFoundation.dll", "PocoNet.dll", "ModManager.dat"};

            foreach (string file in RemoveAllFiles) 
            {
                if (File.Exists(file)) 
                {
                    try {
                        File.Delete(file);
                    } catch {
                        MessageBox.Show($"File {file} cannot be deleted.", "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            
            String jsonModNet = ModNetReloaded.ModNetSupported(ServerDropDownList.SelectedValue.ToString());

            if (jsonModNet != String.Empty) 
            {
                actionText.Text = "Detecting ModNetSupport for " + ServerDropDownList.SelectedItem.ToString();

                try 
                {
                    try { if (File.Exists("lightfx.dll")) File.Delete("lightfx.dll"); } catch { }

                    /* Get Remote ModNet list to process for checking required ModNet files are present and current */
                    String modules = new WebClient().DownloadString(URLs.modnetserver + "/launcher-modules/modules.json");
                    string[] modules_newlines = modules.Split(new string[] { "\n" }, StringSplitOptions.None);

                    foreach (String modules_newline in modules_newlines)
                    {
                        if (modules_newline.Trim() == "{" || modules_newline.Trim() == "}") continue;

                        String trim_modules_newline = modules_newline.Trim();
                        String[] modules_files = trim_modules_newline.Split(new char[] { ':' });

                        String ModNetList = modules_files[0].Replace("\"", "").Trim();
                        String ModNetSHA = modules_files[1].Replace("\"", "").Replace(",", "").Trim();

                        if (SHATwoFiveSix.HashFile(AppDomain.CurrentDomain.BaseDirectory + "\\" + ModNetList).ToLower() != ModNetSHA || !File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\" + ModNetList))
                        {
                            actionText.Text = ("ModNet: Downloading " + ModNetList).ToUpper();

                            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\" + ModNetList))
                            {
                                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\" + ModNetList);
                            }

                            WebClient newModNetFilesDownload = new WebClient();
                            newModNetFilesDownload.DownloadFile(URLs.modnetserver + "/launcher-modules/" + ModNetList, AppDomain.CurrentDomain.BaseDirectory + "/" + ModNetList);
                        }
                        else
                        {
                            actionText.Text = ("ModNet: Up to Date " + ModNetList).ToUpper();
                        }

                        Application.DoEvents();
                    }

                    JSONNode MainJson = JSON.Parse(jsonModNet);

                    Uri newIndexFile = new Uri(MainJson["basePath"] + "/index.json");
                    String jsonindex = new WebClient().DownloadString(newIndexFile);

                    JSONNode IndexJson = JSON.Parse(jsonindex);


                    String path = Path.Combine("MODS", MDFive.HashPassword(MainJson["serverID"]).ToLower());
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
                        actionText.Text = "Launching game...";
                        LaunchGame();
                    }
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            } 
            else 
            {
                //Yikes from me Coders - DavidCarbon
                actionText.Text = "Launching game...";
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

                    client2.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                    client2.DownloadFileCompleted += (test, stuff) =>
                    {
                        isDownloadingModNetFiles = false;
                        if (modFilesDownloadUrls.Any() == false)
                        {
                            actionText.Text = "Launching game...";
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
                    actionText.Text = error.Message;
                }

                isDownloadingModNetFiles = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) 
        {
            if (!string.IsNullOrEmpty(result["WebRecoveryUrl"]))
            {
                Process.Start(result["WebRecoveryUrl"]);
                MessageBox.Show(null, "A browser window has been opened to complete password recovery on " + ServerDropDownList.SelectedItem.ToString(), "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                string send = Prompt.ShowDialog("Please specify your email address.", UserAgent.AgentAltName);

                if (send != String.Empty)
                {
                    String responseString;
                    try
                    {
                        Uri resetPasswordUrl = new Uri(ServerDropDownList.SelectedValue.ToString() + "/RecoveryPassword/forgotPassword");

                        var request = (HttpWebRequest)System.Net.WebRequest.Create(resetPasswordUrl);
                        var postData = "email=" + send;
                        var data = Encoding.ASCII.GetBytes(postData);
                        request.Method = "POST";
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.ContentLength = data.Length;

                        using (var stream = request.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }

                        var response = (HttpWebResponse)request.GetResponse();
                        responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    }
                    catch
                    {
                        responseString = "Failed to send email!";
                    }

                    MessageBox.Show(null, responseString, UserAgent.AgentAltName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
