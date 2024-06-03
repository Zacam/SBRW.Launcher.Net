using Newtonsoft.Json;
using SBRW.Launcher.App.UI_Forms.Main_Screen;
using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.Core.Reference.Json_.Newtonsoft_;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using System;
using System.ComponentModel;
using System.IO;

namespace SBRW.Launcher.App.UI_Forms.Account_Manager_Screen
{
    partial class Screen_Account_Manager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static BindingList<Json_List_Account> Credentials_DB_Read()
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
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("Credentials DB Read".ToUpper(), string.Empty, Error, string.Empty, true);
            }

            return new BindingList<Json_List_Account>();
        }
        /// <summary>
        /// 
        /// </summary>
        public static void Credentials_Load()
        {
            try
            {
                Auto_ID = 0;
                Accounts_Cache.Clear();

                foreach (var Queried_Account in Credentials_DB_Read())
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

                    Accounts_Cache.Add(Queried_Account);
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
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("Credentials Load".ToUpper(), string.Empty, Error, string.Empty, true);
            }
        }
        public static bool Credentials_DB_Save()
        {
            try
            {
                if (!File.Exists(Locations.UserAccountsJSON))
                {
                    File.WriteAllText(Locations.UserAccountsJSON, JsonConvert.SerializeObject(Accounts_Cache));
                    return true;
                }
                else if (!new FileInfo(Locations.UserAccountsJSON).IsReadOnly)
                {
                    File.WriteAllText(Locations.UserAccountsJSON, JsonConvert.SerializeObject(Accounts_Cache));
                    return true;
                }
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("Credentials DB Save".ToUpper(), string.Empty, Error, string.Empty, true);
            }

            return false;
        }
    }
}
