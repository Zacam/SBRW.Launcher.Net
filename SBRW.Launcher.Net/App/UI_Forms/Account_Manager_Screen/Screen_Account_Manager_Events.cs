using CredentialManagement;
using Newtonsoft.Json;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SBRW.Launcher.App.UI_Forms.Account_Manager_Screen
{
    /// <summary>
    /// 
    /// </summary>
    partial class Screen_Account_Manager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Password_Reveal_CheckedChanged(object sender, EventArgs e)
        {
            TextBox_Password.UseSystemPasswordChar = CheckBox_Password_Reveal.CheckState == CheckState.Checked;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Screen_Account_Manager_Shown(object sender, EventArgs e)
        {
            /* Set Datasource so we can Hide Columns */
            DataGridView_Account_List.DataSource = Accounts_Cache;
            DataGridView_Account_List.Columns["Target"].Visible = false;
            DataGridView_Account_List.Columns["Password"].Visible = false;
            DataGridView_Account_List.Columns["AID"].Visible = false;
            /* Load Data for DataGridView */
            ListCredentials();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Screen_Account_Manager_Load(object sender, EventArgs e)
        {
            DataGridView_Account_List.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Account_List.ReadOnly = true;

            selectionBackColor = DataGridView_Account_List.DefaultCellStyle.BackColor;
            selectionForeColor = DataGridView_Account_List.DefaultCellStyle.ForeColor;

            DataGridView_Account_List.DefaultCellStyle.SelectionBackColor = DataGridView_Account_List.DefaultCellStyle.BackColor;
            DataGridView_Account_List.DefaultCellStyle.SelectionForeColor = DataGridView_Account_List.DefaultCellStyle.ForeColor;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_Account_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView_Account_List.DefaultCellStyle.SelectionBackColor = selectionBackColor;
            DataGridView_Account_List.DefaultCellStyle.SelectionForeColor = selectionForeColor;

            var selectedAccount = DataGridView_Account_List.SelectedRows[0].DataBoundItem as Json_List_Account;

            if (selectedAccount != default)
            {
                TextBox_ID.Text = selectedAccount.Target;
                TextBox_Email.Text = selectedAccount.Email;
                TextBox_Password.Text = selectedAccount.Password;
                TextBox_Nickname.Text = selectedAccount.Nickname;
                TextBox_ID_Account.Text = selectedAccount.AID.ToString();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Add_Click(object sender, EventArgs e)
        {
            string Email_Live = TextBox_Email.Text;
            string Password_Live = TextBox_Password.Text;

            if ((Email_Live.Length > 1) && (Password_Live.Length > 1))
            {
                Auto_ID++;
                string App_Name_Target = $"{Application.ProductName}.Account.{Auto_ID}";

                Credential New_Credential = new Credential()
                {
                    Target = App_Name_Target,
                    Username = TextBox_Email.Text,
                    Password = TextBox_Password.Text,
                    Description = !Accounts_Cache.Any(Nickname_Exists => Nickname_Exists.Nickname == TextBox_Nickname.Text)
                    ? TextBox_Nickname.Text : string.Empty,
                    Type = CredentialType.Generic,
                    PersistanceType = PersistanceType.LocalComputer,
                    MaxCredentialBlobSize = Use_New_Max_Blob_Size
                };

                /* If Windows Credential is Running, use the Credential() to provide the bool otherwise return true to save to JSON file */
                if (!(WindowsCredentialRunning() ? New_Credential.Save() : true))
                {
                    MessageBox.Show($"Failed to save credential for {App_Name_Target}");
                }
                else
                {
                    Accounts_Cache.Add(new Json_List_Account()
                    {
                        Target = App_Name_Target,
                        Email = New_Credential.Username,
                        Password = New_Credential.Password,
                        Nickname = New_Credential.Description,
                        Created = New_Credential.LastWriteTime.ToString(),
                        AID = Auto_ID
                    });

                    File.WriteAllText(Path.Combine(Locations.RoamingAppDataFolder_Launcher, Locations.NameAccountsJSON), JsonConvert.SerializeObject(Accounts_Cache));
                    ListCredentials();
                }
            }
            else
            {
                MessageBox.Show("Enter Account Info");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Delete_Click(object sender, EventArgs e)
        {
            if (Accounts_Cache.Count > 0)
            {
                try
                {
                    Json_List_Account? Account_Information = DataGridView_Account_List.SelectedRows[0].DataBoundItem as Json_List_Account;
                    if (Account_Information != default)
                    {
                        /* Get "Target" entry for removal */

                        /* If Windows Credential is Running, use the Credential() to provide the bool otherwise return true to save to JSON file */
                        if (!(WindowsCredentialRunning() ? new Credential() { Target = Account_Information.Target }.Delete() : true))
                        {
                            MessageBox.Show($"Failed to Remove credential: {Account_Information.Target}");
                        }
                        else
                        {
                            Accounts_Cache.Remove(Account_Information);

                            File.WriteAllText(Path.Combine(Locations.RoamingAppDataFolder_Launcher, Locations.NameAccountsJSON), JsonConvert.SerializeObject(Accounts_Cache));
                            ListCredentials();
                        }
                    }
                }
                catch (Exception Error)
                {
                    MessageBox.Show(Error.ToString());
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Update_Click(object sender, EventArgs e)
        {
            if (Accounts_Cache.Count > 0)
            {
                try
                {
                    Json_List_Account? Account_Information = DataGridView_Account_List.SelectedRows[0].DataBoundItem as Json_List_Account;
                    if (Account_Information != default)
                    {
                        /* Get "Target" entry for information update */
                        Credential Updated_Credential = new Credential()
                        {
                            Target = Account_Information.Target,
                            Username = TextBox_Email.Text,
                            Password = TextBox_Password.Text,
                            Description = TextBox_Nickname.Text,
                            Type = CredentialType.Generic,
                            PersistanceType = PersistanceType.LocalComputer,
                            MaxCredentialBlobSize = Use_New_Max_Blob_Size
                        };

                        /* If Windows Credential is Running, use the Credential() to provide the bool otherwise return true to save to JSON file */
                        if (!(WindowsCredentialRunning() ? Updated_Credential.Save() : true))
                        {
                            MessageBox.Show($"Failed to Update credential: {Account_Information.Target}");
                        }
                        else
                        {
                            var Account_Index = Accounts_Cache.Select((Item_Account, Item_Index) => new { Item_Account, Item_Index })
                                .LastOrDefault(x => x.Item_Account.Target == Account_Information.Target);
                            if (Account_Index != default)
                            {
                                if (!TextBox_Password.Text.Equals(Accounts_Cache[Account_Index.Item_Index].Password))
                                {
                                    Accounts_Cache[Account_Index.Item_Index].Password = TextBox_Password.Text;
                                }

                                if (!TextBox_Email.Text.Equals(Accounts_Cache[Account_Index.Item_Index].Email))
                                {
                                    Accounts_Cache[Account_Index.Item_Index].Email = TextBox_Email.Text;
                                }

                                if (!TextBox_Nickname.Text.Equals(Accounts_Cache[Account_Index.Item_Index].Nickname))
                                {
                                    if (!Accounts_Cache.Any(Nickname_Exists => Nickname_Exists.Nickname == TextBox_Nickname.Text))
                                    {
                                        Accounts_Cache[Account_Index.Item_Index].Nickname = TextBox_Nickname.Text;
                                    }
                                }
                            }

                            File.WriteAllText(Path.Combine(Locations.RoamingAppDataFolder_Launcher, Locations.NameAccountsJSON), JsonConvert.SerializeObject(Accounts_Cache));
                            ListCredentials();
                        }
                    }
                }
                catch (Exception Error)
                {
                    MessageBox.Show(Error.ToString());
                }
            }
        }
    }
}
