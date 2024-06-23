#region Usings
using SBRW.Launcher.App.UI_Forms.About_Screen;
using SBRW.Launcher.App.UI_Forms.Account_Manager_Screen;
using SBRW.Launcher.App.UI_Forms.Custom_Server_Screen;
using SBRW.Launcher.App.UI_Forms.Parent_Screen;
using SBRW.Launcher.App.UI_Forms.Register_Screen;
using SBRW.Launcher.App.UI_Forms.Settings_Screen;
using SBRW.Launcher.Core.Cache;
using SBRW.Launcher.Core.Extension.Hash_;
using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.Core.Extension.Numbers_;
using SBRW.Launcher.Core.Extra.File_;
using SBRW.Launcher.Core.Reference.Json_.Newtonsoft_;
using SBRW.Launcher.Core.Theme;
using SBRW.Launcher.RunTime.Auth;
using SBRW.Launcher.RunTime.InsiderKit;
using SBRW.Launcher.RunTime.LauncherCore.Client.Auth;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.Languages.Visual_Forms;
using SBRW.Launcher.RunTime.LauncherCore.LauncherUpdater;
using SBRW.Launcher.RunTime.LauncherCore.Lists;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using SBRW.Launcher.RunTime.LauncherCore.ModNet;
using SBRW.Launcher.RunTime.LauncherCore.Support;
using SBRW.Launcher.RunTime.LauncherCore.Visuals;
using SBRW.Launcher.Core.Extra.File_.Save_;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
#endregion

namespace SBRW.Launcher.App.UI_Forms.Main_Screen
{
    partial class Screen_Main
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && LoginEnabled)
            {
                LoginButton_Click(null, null);
                e.SuppressKeyPress = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Oem3)
            {
                // Handle key at form level.
                // Do not send event to focused control by returning true.

                if (!this.Disposing || !this.IsDisposed)
                {
                    if (!Button_Console_Submit.Visible)
                    {
                        Button_Console_Submit.Visible = Input_Console.Visible = true;
                    }
                    else
                    {
                        Button_Console_Submit.Visible = Input_Console.Visible = false;
                    }
                }

                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Console_Enter(object sender, EventArgs e)
        {
            FunctionEvents.Console_Commands(Input_Console.Text);
            Input_Console.Text = string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Console_Quick_Send(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Console_Enter(sender, e);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_Click(object? sender, EventArgs? e)
        {
            if ((LoginEnabled == false || ServerEnabled == false) && Builtinserver == false)
            {
                return;
            }

            if (IsDownloading)
            {
                string TempEmailCache = string.Empty;
                if (!string.IsNullOrWhiteSpace(Input_Email.Text))
                {
                    TempEmailCache = Input_Email.Text;
                    Input_Email.Text = "EMAIL IS HIDDEN";
                }
                "Please wait while the GameLauncher is still downloading the game files.".Message_Box(MessageBoxButtons.OK);
                if (!string.IsNullOrWhiteSpace(TempEmailCache))
                {
                    Input_Email.Text = TempEmailCache;
                }

                return;
            }

            Tokens.Clear();

            string Email;
            string Password;

            Tokens.IPAddress = Launcher_Value.Launcher_Select_Server_Data.IPAddress;
            Tokens.ServerName = ServerListUpdater.ServerName("Login");

            if (Save_Settings.Account_Manager())
            {
                if (ComboBox_Accounts.SelectedItem != default)
                {
                    Button_Login.Text = "Decrypting".ToUpper();
                    Email = ((Json_List_Account)ComboBox_Accounts.SelectedItem).Email.Decrypt_AES();
                    Password = ((Json_List_Account)ComboBox_Accounts.SelectedItem).Password.Decrypt_AES();
                    Button_Login.Text = "Login".ToUpper();
                }
                else
                {
                    "Add Account Information through the Manager before login.".Message_Box();
                    return;
                }
            }
            else
            {
                switch (Authentication.HashType(Launcher_Value.Launcher_Select_Server_JSON.Server_Authentication_Version ?? string.Empty))
                {
                    case AuthHash.H10:
                        Email = Input_Email.Text.ToString();
                        Password = Input_Password.Text.ToString();
                        break;
                    case AuthHash.H11:
                        Email = Input_Email.Text.ToString();
                        Password = Input_Password.Text.Hash_String(0).ToLower();
                        break;
                    case AuthHash.H12:
                        Email = Input_Email.Text.ToString();
                        Password = Input_Password.Text.Hash_String(1).ToLower();
                        break;
                    case AuthHash.H13:
                        Email = Input_Email.Text.ToString();
                        Password = Input_Password.Text.Hash_String(2).ToLower();
                        break;
                    case AuthHash.H20:
                        Email = Input_Email.Text.Hash_String(0).ToLower();
                        Password = Input_Password.Text.Hash_String(0).ToLower();
                        break;
                    case AuthHash.H21:
                        Email = Input_Email.Text.Hash_String(1).ToLower();
                        Password = Input_Password.Text.Hash_String(1).ToLower();
                        break;
                    case AuthHash.H22:
                        Email = Input_Email.Text.Hash_String(2).ToLower();
                        Password = Input_Password.Text.Hash_String(2).ToLower();
                        break;
                    default:
                        Log.Error("HASH TYPE: Unknown Hash Standard was Provided");
                        return;
                }
            }

            Authentication.Client("Login", Launcher_Value.Launcher_Select_Server_JSON.Server_Authentication_Post, Email, Password, string.Empty);

            if (string.IsNullOrWhiteSpace(Tokens.Error))
            {
                try
                {
                    if (!(ComboBox_Server_List.SelectedItem is Json_List_Server server)) return;
                    Save_Account.Live_Data.Saved_Server_Address = server.IPAddress;
                }
                catch { }

                UserId = Tokens.UserId;
                LoginToken = Tokens.LoginToken;
                Launcher_Value.Launcher_Select_Server_Data.IPAddress = Tokens.IPAddress;

                /* Tells the FileAccountSave to Actually Save the Information or Not */
                Save_Account.SaveLoginInformation = CheckBox_Remember_Us.Checked;
                if (!Save_Settings.Account_Manager())
                {
                    Save_Account.Live_Data.User_Raw_Email = Input_Email.Text.ToString();
                    Save_Account.Live_Data.User_Raw_Password = Input_Password.Text.ToString();

                    switch (Authentication.HashType(Launcher_Value.Launcher_Select_Server_JSON.Server_Authentication_Version ?? string.Empty))
                    {
                        case AuthHash.H10:
                            Save_Account.Live_Data.Saved_Server_Hash_Version = "1.0";
                            Save_Account.Live_Data.User_Hashed_Email = string.Empty;
                            Save_Account.Live_Data.User_Hashed_Password = string.Empty;
                            break;
                        case AuthHash.H11:
                            Save_Account.Live_Data.Saved_Server_Hash_Version = "1.1";
                            Save_Account.Live_Data.User_Hashed_Email = string.Empty;
                            Save_Account.Live_Data.User_Hashed_Password = Input_Password.Text.Hash_String(0).ToLower();
                            break;
                        case AuthHash.H12:
                            Save_Account.Live_Data.Saved_Server_Hash_Version = "1.2";
                            Save_Account.Live_Data.User_Hashed_Email = string.Empty;
                            Save_Account.Live_Data.User_Hashed_Password = Input_Password.Text.Hash_String(1).ToLower();
                            break;
                        case AuthHash.H13:
                            Save_Account.Live_Data.Saved_Server_Hash_Version = "1.3";
                            Save_Account.Live_Data.User_Hashed_Email = string.Empty;
                            Save_Account.Live_Data.User_Hashed_Password = Input_Password.Text.Hash_String(2).ToLower();
                            break;
                        case AuthHash.H20:
                            Save_Account.Live_Data.Saved_Server_Hash_Version = "2.0";
                            Save_Account.Live_Data.User_Hashed_Email = Input_Email.Text.Hash_String(0).ToLower();
                            Save_Account.Live_Data.User_Hashed_Password = Input_Password.Text.Hash_String(0).ToLower();
                            break;
                        case AuthHash.H21:
                            Save_Account.Live_Data.Saved_Server_Hash_Version = "2.1";
                            Save_Account.Live_Data.User_Hashed_Email = Input_Email.Text.Hash_String(1).ToLower();
                            Save_Account.Live_Data.User_Hashed_Password = Input_Password.Text.Hash_String(1).ToLower();
                            break;
                        case AuthHash.H22:
                            Save_Account.Live_Data.Saved_Server_Hash_Version = "2.2";
                            Save_Account.Live_Data.User_Hashed_Email = Input_Email.Text.Hash_String(2).ToLower();
                            Save_Account.Live_Data.User_Hashed_Password = Input_Password.Text.Hash_String(2).ToLower();
                            break;
                        default:
                            Save_Account.Live_Data.Saved_Server_Hash_Version = "Unknown";
                            Save_Account.Live_Data.User_Hashed_Email = string.Empty;
                            Save_Account.Live_Data.User_Hashed_Password = string.Empty;
                            Log.Error("HASH TYPE: Unknown Hash Standard was Provided");
                            return;
                    }
                }
                else if (ComboBox_Accounts.SelectedItem != default)
                {
                    Save_Account.Live_Data.User_Account_Index = ComboBox_Accounts.SelectedIndex.ToString();
                }

                Save_Account.Save();

                if (!string.IsNullOrWhiteSpace(Tokens.Warning))
                {
                    Input_Email.Text = "EMAIL IS HIDDEN";
                    Tokens.Warning.Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Input_Email.Text = Email;
                }

                LoginFormElements(false);
                LoggedInFormElements(true);
            }
            else
            {
                /* Main Screen Login */
                Picture_Input_Email.Image = Picture_Input_Email.Icon_Order(SVG_Icon.Input_Box_Email, SVG_Color.Error);
                Picture_Input_Password.Image = Picture_Input_Password.Icon_Order(SVG_Icon.Input_Box_Password, SVG_Color.Error);
                if (Picture_Information_Window.Image != Image_Other.Information_Window_Error)
                {
                    Picture_Information_Window.Image = Image_Other.Information_Window_Error;
                }
                Input_Email.Text = "EMAIL IS HIDDEN";
                Tokens.Error.Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Error);
                Input_Email.Text = Email;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Popup_Click(object sender, EventArgs e)
        {
            if (LauncherUpdateCheck.UpgradeAvailable)
            {
                /* Reason: The specified executable is not a valid application for this OS platform.
                 * Code: -2147467259
                 * Full Log: System.ComponentModel.Win32Exception (0x80004005): The specified executable is not a valid application for this OS platform.*/
                LauncherUpdateCheck.UpdateStatusResult(true);
            }
        }
        /// <summary>
        /// Register PAGE LAYOUT 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Button_Register_Click(object sender, EventArgs e)
        {
            if (FunctionStatus.AllowRegistration)
            {
                if (Launcher_Value.Launcher_Select_Server_JSON != default)
                {
                    if (!string.IsNullOrWhiteSpace(Launcher_Value.Launcher_Select_Server_JSON.Server_Registration_Page))
                    {
#if NETFRAMEWORK
                        Process.Start(Launcher_Value.Launcher_Select_Server_JSON.Server_Registration_Page);
#else
                        Process.Start(new ProcessStartInfo { FileName = Launcher_Value.Launcher_Select_Server_JSON.Server_Registration_Page, UseShellExecute = true });
#endif
                        ("A browser window has been opened to complete registration on " +
                            ServerListUpdater.ServerName("Register")).Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (Launcher_Value.Launcher_Select_Server_Data.Name.ToUpper() == "WORLDUNITED OFFICIAL")
                    {
#if NETFRAMEWORK
                        Process.Start("https://signup.worldunited.gg/");
#else
                        Process.Start(new ProcessStartInfo { FileName = "https://signup.worldunited.gg/", UseShellExecute = true });
#endif
                        ("A browser window has been opened to complete registration on " +
                            Launcher_Value.Launcher_Select_Server_Data.Name).Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        try
                        {
                            Screen_Register Custom_Instance_Register = new Screen_Register() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true, FormBorderStyle = FormBorderStyle.None };
                            Panel_Register_Screen.Controls.Add(Custom_Instance_Register);
                            Panel_Register_Screen.Visible = true;
                            Custom_Instance_Register.Show();
                            Text = "Register - SBRW Launcher: " + Application.ProductVersion;
                        }
                        catch (Exception Error)
                        {
                            string ErrorMessage = "Register Screen Encountered an Error";
                            LogToFileAddons.OpenLog("SETTINGS Register", ErrorMessage, Error, "Exclamation", false);
                        }
                    }
                }
                else
                {
                    "Loading Server Information. Please try again.".Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                "Server seems to be Offline.".Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// SETTINGS PAGE LAYOUT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsButton_Click(object sender, EventArgs e)
        {
            try
            {
                Screen_Settings Custom_Instance_Settings = new Screen_Settings() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true, FormBorderStyle = FormBorderStyle.None };
                ((Control)Custom_Instance_Settings.TabPage_Setup).Enabled = false;
                Panel_Form_Screens.Controls.Add(Custom_Instance_Settings);
                Panel_Form_Screens.Visible = true;
                Custom_Instance_Settings.Show();
                Text = "Settings - SBRW Launcher: " + Application.ProductVersion;
            }
            catch (Exception Error)
            {
                string ErrorMessage = "Settings Screen Encountered an Error";
                LogToFileAddons.OpenLog("SETTINGS SCREEN", ErrorMessage, Error, "Exclamation", false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_"></param>
        /// <param name="e"></param>
        private void Client_DownloadProgressChanged_RELOADED(object _, DownloadProgressChangedEventArgs e)
        {
            if (Screen_Instance != null && !IsDisposed && !Disposing)
            {
                try
                {
                    ModNet_Download_Status = new Download_Information_ModNet()
                    {
                        Download_Percentage = (int)((double)CurrentModFileCount / (double)TotalModFileCount * 100).Clamp(0, 100),
                        File_Name = ModNetFileNameInUse,
                        File_Size_Total = e.TotalBytesToReceive,
                        File_Size_Current = e.BytesReceived,
                        File_Size_Remaining = e.TotalBytesToReceive - e.BytesReceived,
                        Download_Complete = false
                    };

                    if (UI_MODE != 7)
                    {
                        UI_MODE = 7;
                    }
                }
                catch (Exception Error)
                {
                    LogToFileAddons.OpenLog("Client_DownloadProgressChanged_RELOADED", string.Empty, Error, string.Empty, true);
                }
            }
        }
        /// <summary>
        /// Loads, Set, and Start Certain Functions (CORE)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainScreen_Load(object sender, EventArgs e)
        {
            Log.Visuals("CORE: Loading Main Screen");
            Application.OpenForms[this.Name].Activate();

            if (!string.IsNullOrWhiteSpace(BuildInformation.SHORT_INFO_WITH_SECONDS))
            {
                if (BuildDevelopment.Allowed() || BuildBeta.Allowed())
                {
                    Label_Insider_Build_Number.Visible = true;
                    Label_Insider_Build_Number.Text = BuildInformation.SHORT_INFO_WITH_SECONDS;
                }
                else
                {
                    Label_Insider_Build_Number.Visible = Label_Debug_Language.Visible = false;
                }
            }

            Log.Core("LAUNCHER: NFSW Download Source is now: " + Save_Settings.Live_Data.Launcher_CDN);

            Input_Email.Text = Save_Account.Live_Data.User_Raw_Email;
            Input_Password.Text = Save_Account.Live_Data.User_Raw_Password;

            Log.Core("LAUNCHER: Checking for password");
            LoginEnabled = ServerEnabled = !string.IsNullOrWhiteSpace(Save_Account.Live_Data.User_Raw_Password);
            Button_Login.BackgroundImage = Image_Button.Grey;
            Button_Login.ForeColor = LoginEnabled ? Color_Text.L_Five : Color_Text.L_Six;

            if (!string.IsNullOrWhiteSpace(Save_Account.Live_Data.User_Raw_Email) &&
                (!string.IsNullOrWhiteSpace(Save_Account.Live_Data.User_Hashed_Password) || !string.IsNullOrWhiteSpace(Save_Account.Live_Data.User_Raw_Password)))
            {
                Log.Core("LAUNCHER: Restoring last saved email and password");
                CheckBox_Remember_Us.Checked = true;
                Save_Account.SaveLoginInformation = true;
            }

            /* Server Display List */
            ComboBox_Server_List.DisplayMember = "Name";
            Log.Core("LAUNCHER: Setting server list");
            ComboBox_Server_List.DataSource = ServerListUpdater.CleanList;
            /* Accounts Display List */
            Screen_Account_Manager.Credentials_Load();

            /* Now Reflect the User Choice on Account Manager usage */
            ComboBox_Accounts.Visible = Save_Settings.Account_Manager();
            Button_Account_Manager.Visible = Save_Settings.Account_Manager();
            Input_Email.Visible = !Save_Settings.Account_Manager();
            Input_Password.Visible = !Save_Settings.Account_Manager();
            Picture_Input_Email.Visible = !Save_Settings.Account_Manager();
            Picture_Input_Password.Visible = !Save_Settings.Account_Manager();

            /* Display Server List Dialog if Server IP Doesn't Exist */
            if (string.IsNullOrWhiteSpace(Save_Account.Live_Data.Saved_Server_Address))
            {
                Screen_Custom_Server.OpenScreen(false);

                if (ServerListUpdater.SelectedServer != null)
                {
                    Save_Account.Live_Data.Saved_Server_Address = ServerListUpdater.SelectedServer.IPAddress;
                    Save_Account.Save();
                }
                else
                {
                    FunctionStatus.LauncherForceClose = true;
                }
            }

            if (FunctionStatus.LauncherForceClose)
            {
                FunctionStatus.ErrorCloseLauncher("Closing From SelectServer Dialog", false);
            }
            else
            {
                Log.Core("SERVERLIST: Checking...");
                Log.Core("SERVERLIST: Setting first server in list");
                Log.Core("SERVERLIST: Checking if server is set on INI File");
                try
                {
                    if (string.IsNullOrWhiteSpace(Save_Account.Live_Data.Saved_Server_Address))
                    {
                        Log.Warning("SERVERLIST: Failed to find anything... assuming " + ((Json_List_Server)ComboBox_Server_List.SelectedItem).IPAddress);
                        Save_Account.Live_Data.Saved_Server_Address = ((Json_List_Server)ComboBox_Server_List.SelectedItem).IPAddress;
                        Save_Account.Save();
                    }
                }
                catch
                {
                    Log.Error("SERVERLIST: Failed to write anything...");
                    Save_Account.Live_Data.Saved_Server_Address = string.Empty;
                    Save_Account.Save();
                }

                Log.Core("SERVERLIST: Re-Checking if server is set on INI File");
                if (!string.IsNullOrWhiteSpace(Save_Account.Live_Data.Saved_Server_Address))
                {
                    Log.Core("SERVERLIST: Found something!");
                    SkipServerTrigger = true;

                    Log.Core("SERVERLIST: Checking if server exists on our database");

                    try
                    {
                        if (ServerListUpdater.CleanList.Count != 0)
                        {
                            if (ServerListUpdater.CleanList.FindIndex(i => string.Equals(i.IPAddress, Save_Account.Live_Data.Saved_Server_Address)) != 0)
                            {
                                Log.Core("SERVERLIST: Server found! Checking ID");
                                var index = ServerListUpdater.CleanList.FindIndex(i => string.Equals(i.IPAddress, Save_Account.Live_Data.Saved_Server_Address));

                                Log.Core("SERVERLIST: ID is " + index);
                                if (index >= 0)
                                {
                                    Log.Core("SERVERLIST: ID set correctly");
                                    ComboBox_Server_List.SelectedIndex = index;
                                }
                                else
                                {
                                    ComboBox_Server_List.SelectedIndex = 1;
                                }
                            }
                            else
                            {
                                Log.Warning("SERVERLIST: Unable to find anything, assuming default");
                                ComboBox_Server_List.SelectedIndex = 1;
                                Log.Warning("SERVERLIST: Deleting unknown entry");
                                Save_Account.Live_Data.Saved_Server_Address = string.Empty;
                                Save_Account.Save();
                            }

                            Log.Core("SERVERLIST: Triggering server change");
                            if (ComboBox_Server_List.SelectedIndex == 1)
                            {
                                ComboBox_Server_List_SelectedIndexChanged(sender, e);
                            }

                            Log.Completed("SERVERLIST: All done");
                        }
                        else { ComboBox_Server_List_SelectedIndexChanged(sender, e); Log.Completed("SERVERLIST: Empty List. Not Setting Index"); }
                    }
                    catch (Exception Error)
                    {
                        LogToFileAddons.OpenLog("Serverlist", string.Empty, Error, string.Empty, true);
                    }
                }

                Log.Core("LAUNCHER: Re-checking InstallationDirectory: " + Save_Settings.Live_Data.Game_Path);

                string Drive = Path.GetPathRoot(Save_Settings.Live_Data.Game_Path) ?? string.Empty;
                if (!Directory.Exists(Drive))
                {
                    if (string.IsNullOrWhiteSpace(Drive))
                    {
                        Save_Settings.Live_Data.Game_Path = Locations.GameFilesFailSafePath;
                        Save_Settings.Save();
                        string Display_Message = Translations.Database("MainScreen_TextBox_GameFiles_Invalid_Location");
                        Log.Error(string.Format("LAUNCHER: Drive {0} was not found. Your actual installation directory is set to {1} now.",
                            Drive, Locations.GameFilesFailSafePath));

                        string TempEmailCache = string.Empty;
                        if (!string.IsNullOrWhiteSpace(Input_Email.Text))
                        {
                            TempEmailCache = Input_Email.Text;
                            Input_Email.Text = "EMAIL IS HIDDEN";
                        }
                        string.Format("Drive {0} was not found. Your actual installation directory is set to {1} now.",
                            Drive, Locations.GameFilesFailSafePath).Message_Box(MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (!string.IsNullOrWhiteSpace(TempEmailCache))
                        {
                            Input_Email.Text = TempEmailCache;
                        }
                    }
                }

                try
                {
                    new LauncherUpdateCheck(Picture_Icon_Version, Label_Status_Launcher, Label_Status_Launcher_Version).ChangeVisualStatus();
                }
                catch
                {
#if RELEASE_UNIX || DEBUG_UNIX
                    Picture_Icon_Version.BackgroundImage = Picture_Icon_Version.Icon_Order(SVG_Icon.Offline, SVG_Color.Unknown);
                    Label_Status_Launcher.Text = "Launcher Status:\n - Linux Build";
                    Label_Status_Launcher.ForeColor = Color_Text.L_Three;
                    Label_Status_Launcher_Version.Text = "Version: " + Application.ProductVersion;
#endif
                }

                PingServerListAPIStatus();

                Log.Visuals("CORE: Applyinng ContextMenu");
#if NETFRAMEWORK
                ContextMenu = new ContextMenu();
                try
                {
                    /* Internal Message Reference Time: 01/14/2023 1:31 AM PST */
                    if (DateTime.Now == new DateTime(DateTime.Now.Year, 1, 14) || DateTime.Now == new DateTime(DateTime.Now.Year, 4, 18))
                    {
                        ContextMenu.MenuItems.Add(new MenuItem("RIP M.L. (1925-2023)", (_, E) =>
                        {
#if NETFRAMEWORK
                            Process.Start("https://www.youtube.com/watch?v=wctrwXZkUK0");
#else
                            Process.Start("explorer.exe", "https://www.youtube.com/watch?v=wctrwXZkUK0");
#endif
                        }));
                        ContextMenu.MenuItems.Add("-");
                    }
                }
                catch
                {
                    ContextMenu.MenuItems.Add(new MenuItem("RIP M.L. (1925-2023)", (_, E) =>
                    {
#if NETFRAMEWORK
                        Process.Start("https://www.youtube.com/watch?v=rANqc5br7dc");
#else
                        Process.Start("explorer.exe", "https://www.youtube.com/watch?v=rANqc5br7dc");
#endif
                    }));
                    ContextMenu.MenuItems.Add("-");
                }

                try
                {
                    if (DateTime.Now == new DateTime(DateTime.Now.Year, 4, 1))
                    {
                        ContextMenu.MenuItems.Add(new MenuItem("The Mermaid Sisters is Here!", (_, E) =>
                        {
#if NETFRAMEWORK
                            Process.Start("https://www.youtube.com/watch?v=OFjqEexH0Tg");
#else
                            Process.Start("explorer.exe", "https://www.youtube.com/watch?v=OFjqEexH0Tg");
#endif
                        }));
                        ContextMenu.MenuItems.Add("-");
                    }
                }
                catch
                {
                    /* Show it Anyways, lets be real here */
                    ContextMenu.MenuItems.Add(new MenuItem("The Mermaid Sisters is Here!", (_, E) =>
                    {
#if NETFRAMEWORK
                        Process.Start("https://www.youtube.com/watch?v=OFjqEexH0Tg");
#else
                        Process.Start("explorer.exe", "https://www.youtube.com/watch?v=OFjqEexH0Tg");
#endif
                    }));
                    ContextMenu.MenuItems.Add("-");
                }

                try
                {
                    if (DateTime.Now == new DateTime(DateTime.Now.Year, 7, 4))
                    {
                        ContextMenu.MenuItems.Add(new MenuItem("Fireworks", (_, E) =>
                        {
#if NETFRAMEWORK
                            Process.Start("https://youtu.be/2m5vQo81Jik");
#else
                                Process.Start("explorer.exe", "https://youtu.be/2m5vQo81Jik");
#endif
                        }));
                        ContextMenu.MenuItems.Add("-");
                    }
                    else if (DateTime.Now == new DateTime(DateTime.Now.Year, 6, 4))
                    {
                        ContextMenu.MenuItems.Add(new MenuItem(
                            ((BuildBeta.Allowed() || BuildDevelopment.Allowed()) ?
                            "" : "2017/06/04 - ") + "Development Cycle", (_, E) =>
                            {
#if NETFRAMEWORK
                                Process.Start("https://www.youtube.com/watch?v=5hv2p0RtVY0");
#else
                            Process.Start("explorer.exe", "https://www.youtube.com/watch?v=5hv2p0RtVY0");
#endif
                            }));
                        ContextMenu.MenuItems.Add("-");
                    }
                    /* Development Release Year: 2017 */
                    else if (DateTime.Now == new DateTime(DateTime.Now.Year, 6, 18))
                    {
                        ContextMenu.MenuItems.Add(new MenuItem("Happy Birthday Interface 1", (_, E) =>
                        {
#if NETFRAMEWORK
                            Process.Start("https://raw.githubusercontent.com/SoapboxRaceWorld/GameLauncher_NFSW/interface_v1/screenshot.png");
#else
                            Process.Start("explorer.exe", "https://raw.githubusercontent.com/SoapboxRaceWorld/GameLauncher_NFSW/interface_v1/screenshot.png");
#endif
                        }));
                        ContextMenu.MenuItems.Add("-");
                    }
                    /* Development Release Year: 2017 */
                    else if (DateTime.Now == new DateTime(DateTime.Now.Year, 11, 2))
                    {
                        ContextMenu.MenuItems.Add(new MenuItem("Happy Birthday Interface 2!", (_, E) =>
                        {
#if NETFRAMEWORK
                            Process.Start("https://raw.githubusercontent.com/SoapboxRaceWorld/GameLauncher_NFSW/interface_v2/screenshot.png");
#else
                            Process.Start("explorer.exe", "https://raw.githubusercontent.com/SoapboxRaceWorld/GameLauncher_NFSW/interface_v2/screenshot.png");
#endif
                        }));
                        ContextMenu.MenuItems.Add("-");
                    }
                    /* Development Release Year: 2018 */
                    else if (DateTime.Now == new DateTime(DateTime.Now.Year, 11, 8))
                    {
                        ContextMenu.MenuItems.Add(new MenuItem("Happy Birthday Interface 3!", (_, E) =>
                        {
#if NETFRAMEWORK
                            Process.Start("https://raw.githubusercontent.com/SoapboxRaceWorld/GameLauncher_NFSW/Net.Standard/01-Main_Screen.png");
#else
                            Process.Start("explorer.exe", "https://raw.githubusercontent.com/SoapboxRaceWorld/GameLauncher_NFSW/Net.Standard/01-Main_Screen.png");
#endif
                        }));
                        ContextMenu.MenuItems.Add("-");
                    }
                }
                catch
                {
                    /* Forget about it Cuh */
                }

                ContextMenu.MenuItems.Add(new MenuItem("About", (O, K) => { Screen_About.OpenScreen(); }));
                if (LauncherUpdateCheck.UpgradeAvailable)
                {
                    ContextMenu.MenuItems.Add("-");
                    ContextMenu.MenuItems.Add(new MenuItem("Obsolete", (N, O) =>
                    {
#if NETFRAMEWORK
                        Process.Start("https://www.youtube.com/watch?v=LutDfASARmE");
#else
                        Process.Start("explorer.exe", "https://www.youtube.com/watch?v=LutDfASARmE");
#endif
                    }));
                }
                ContextMenu.MenuItems.Add("-");
                if (Screen_Parent.Screen_Instance != null)
                {
                    ContextMenu.MenuItems.Add(new MenuItem("Close Launcher", Screen_Parent.Screen_Instance.Button_Close_Click));
                }

                NotifyIcon_Notification.ContextMenu = ContextMenu;
                ContextMenu = null;
#endif
                NotifyIcon_Notification.Icon = FormsIcon.Retrive_Icon();
                NotifyIcon_Notification.Text = "SBRW Launcher";
                NotifyIcon_Notification.Visible = true;

                /* Remove TracksHigh Folder and Files */
                RemoveTracksHighFiles();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Account_Manager_Click(object sender, EventArgs e)
        {
            try
            {
                Screen_Account_Manager Custom_Instance_Settings = new Screen_Account_Manager() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true, FormBorderStyle = FormBorderStyle.None };
                Panel_Form_Screens.Controls.Add(Custom_Instance_Settings);
                Panel_Form_Screens.Visible = true;
                Custom_Instance_Settings.Show();
                Text = "Accounts - SBRW Launcher: " + Application.ProductVersion;
            }
            catch (Exception Error)
            {
                string ErrorMessage = "Accounts Screen Encountered an Error";
                LogToFileAddons.OpenLog("Accounts SCREEN", ErrorMessage, Error, "Exclamation", false);
            }
        }
    }
}
