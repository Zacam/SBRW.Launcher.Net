﻿using Newtonsoft.Json;
using SBRW.Launcher.App.UI_Forms.Main_Screen;
using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.SystemPlatform.Unix;
using System.ComponentModel;
using System.IO;
using System.ServiceProcess;

namespace SBRW.Launcher.App.UI_Forms.Account_Manager_Screen
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// JSON Format for Reading an Account List
    /// </summary>
    /// <remarks><i>Requires <b>Newtonsoft.Json</b> Library</i></remarks>
    /// /* accounts.json */
    public class Json_List_Account
    {
        /// <summary>
        /// Name of Service
        /// </summary>
        [JsonProperty("target")]

        public string Target { get; set; }

        /// <summary>
        /// Custom Nickname
        /// </summary>
        /// <remarks>Also known as Description</remarks>
        [JsonProperty("nickname")]
        public string Nickname { get; set; }
        /// <summary>
        /// User Account Email
        /// </summary>
        /// <remarks>Also known as Username</remarks>
        [JsonProperty("email")]
        public string Email { get; set; }
        /// <summary>
        /// User Account Password
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }
        /// <summary>
        /// When User Information was Created
        /// </summary>
        /// <remarks>Also known as LastWritten</remarks>
        [JsonProperty("created")]
        public string Created { get; set; }
        /// <summary>
        /// Used to Tag accounts
        /// </summary>
        /// <remarks>Account ID</remarks>
        [JsonProperty("aid")]
        public int AID { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    partial class Screen_Account_Manager
    {
        public static BindingList<Json_List_Account> Read_Data_Base()
        {
            try
            {
                string oldcontent = string.Empty;

                if (File.Exists(Path.Combine(Locations.RoamingAppDataFolder_Launcher, Locations.NameAccountsJSON)))
                {
                    using (StreamReader sr = new StreamReader(Path.Combine(Locations.RoamingAppDataFolder_Launcher, Locations.NameAccountsJSON))) 
                    {
                        oldcontent = sr.ReadToEnd();
                        sr.Close();
                    }
                }

                if (!string.IsNullOrWhiteSpace(oldcontent))
                {
#pragma warning disable CS8603
                    return JsonConvert.DeserializeObject<BindingList<Json_List_Account>>(oldcontent);
#pragma warning restore CS8603
                }
            }
            finally { }

            return new BindingList<Json_List_Account>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool WindowsCredentialRunning()
        {
            try
            {
                if (!UnixOS.Detected())
                {
                    using (ServiceController sc = new ServiceController("VaultSvc"))
                    {
                        return sc.Status.Equals(ServiceControllerStatus.Running);
                    }
                }
            }
            catch
            {

            }

            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        public static void ListCredentials()
        {
            Auto_ID = 0;
            Accounts_Cache.Clear();

            foreach (var Queried_Account in Read_Data_Base())
            {
                if (Auto_ID == 0)
                {
                    if (Screen_Instance != default)
                    {
                        if (Screen_Instance.TextBox_Min.Visible)
                        {
                            Screen_Instance.TextBox_Min.Text = (Auto_ID = Queried_Account.AID).ToString();
                        }
                    }

                    Auto_ID = Queried_Account.AID;
                }
                else if (Queried_Account.AID >= Auto_ID)
                {
                    if (Screen_Instance != default)
                    {
                        if (Screen_Instance.TextBox_Max.Visible)
                        {
                            Screen_Instance.TextBox_Max.Text = (Auto_ID = Queried_Account.AID).ToString();
                        }
                    }

                    Auto_ID = Queried_Account.AID;
                }

#if !(RELEASE_UNIX || DEBUG_UNIX)
                if (WindowsCredentialRunning())
                {
                    if (new CredentialManagement.Credential() { Target = Queried_Account.Target, Type = CredentialManagement.CredentialType.Generic }.Load())
                    {
                        Accounts_Cache.Add(Queried_Account);
                    }
                }
                else
                {
                    Accounts_Cache.Add(Queried_Account);
                }
#else
                Accounts_Cache.Add(Queried_Account);
#endif
            }

            if (Screen_Instance != default)
            {
                if (Screen_Instance.DataGridView_Account_List.Visible)
                {
                    Screen_Instance.DataGridView_Account_List.DataSource = Accounts_Cache;
                    Screen_Instance.DataGridView_Account_List.Refresh();
                    Screen_Instance.DataGridView_Account_List.ClearSelection();
                }
            }

            if (Screen_Main.Screen_Instance != default)
            {
                if (Screen_Main.Screen_Instance.ComboBox_Accounts.Visible)
                {
                    /* Accounts Display List */
                    Screen_Main.Screen_Instance.ComboBox_Accounts.DisplayMember = "Nickname";
                    Log.Core("LAUNCHER: Setting Account list");
                    Screen_Main.Screen_Instance.ComboBox_Accounts.DataSource = Accounts_Cache;
                    Screen_Main.Screen_Instance.ComboBox_Accounts.Refresh();
                }
            }
        }
    }
}
