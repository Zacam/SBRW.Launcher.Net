using SBRW.Launcher.Core.Extension.Hash_;
using SBRW.Launcher.Core.Reference.Json_.Newtonsoft_;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using System;
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
            DataGridView_Account_List.Columns["Email"].Visible = false;
            DataGridView_Account_List.Columns["Password"].Visible = false;
            DataGridView_Account_List.Columns["Played"].Visible = false;
            DataGridView_Account_List.Columns["AID"].Visible = false;
            /* Load Data for DataGridView */
            Credentials_Load();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_Account_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedAccount = DataGridView_Account_List.SelectedRows[0].DataBoundItem as Json_List_Account;

            if (selectedAccount != default)
            {
                TextBox_ID.Text = selectedAccount.Target;
                TextBox_Email.Text = selectedAccount.Email.Decrypt_AES();
                TextBox_Password.Text = selectedAccount.Password.Decrypt_AES();
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
            try
            {
                if ((TextBox_Email.Text.Length > 1) && (TextBox_Password.Text.Length > 1))
                {
                    Auto_ID++;

                    Accounts_Cache.Add(new Json_List_Account()
                    {
                        Target = $"{Application.ProductName}.Account.{Auto_ID}",
                        Email = TextBox_Email.Text.Encrypt_AES(),
                        Password = TextBox_Password.Text.Encrypt_AES(),
                        Nickname = !Accounts_Cache.Any(Nickname_Exists => Nickname_Exists.Nickname == TextBox_Nickname.Text)
                            ? TextBox_Nickname.Text : string.Empty,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        AID = Auto_ID
                    });

                    if (Credentials_DB_Save())
                    {
                        Credentials_Load();
                    }
                }
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("Credentials Account Add", string.Empty, Error, string.Empty, true);
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
                        Accounts_Cache.Remove(Account_Information);

                        if (Credentials_DB_Save())
                        {
                            Credentials_Load();
                        }
                    }
                }
                catch (Exception Error)
                {
                    LogToFileAddons.OpenLog("Credentials Account Remove", string.Empty, Error, string.Empty, true);
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
                        var Account_Index = Accounts_Cache.Select((Item_Account, Item_Index) => new { Item_Account, Item_Index })
                                .LastOrDefault(x => x.Item_Account.Target == Account_Information.Target);

                        if (Account_Index != default)
                        {
                            bool Update_Info_Tag = false;

                            if (!TextBox_Password.Text.Encrypt_AES().Equals(Accounts_Cache[Account_Index.Item_Index].Password))
                            {
                                Accounts_Cache[Account_Index.Item_Index].Password = TextBox_Password.Text.Encrypt_AES();
                                Update_Info_Tag = true;
                            }

                            if (!TextBox_Email.Text.Encrypt_AES().Equals(Accounts_Cache[Account_Index.Item_Index].Email))
                            {
                                Accounts_Cache[Account_Index.Item_Index].Email = TextBox_Email.Text.Encrypt_AES();
                                Update_Info_Tag = true;
                            }

                            if (!TextBox_Nickname.Text.Equals(Accounts_Cache[Account_Index.Item_Index].Nickname))
                            {
                                if (!Accounts_Cache.Any(Nickname_Exists => Nickname_Exists.Nickname == TextBox_Nickname.Text))
                                {
                                    Accounts_Cache[Account_Index.Item_Index].Nickname = TextBox_Nickname.Text;
                                    Update_Info_Tag = true;
                                }
                            }

                            if (Update_Info_Tag)
                            {
                                Accounts_Cache[Account_Index.Item_Index].Updated = DateTime.Now;
                            }
                        }

                        if (Credentials_DB_Save())
                        {
                            Credentials_Load();
                        }
                    }
                }
                catch (Exception Error)
                {
                    LogToFileAddons.OpenLog("Credentials Account Update", string.Empty, Error, string.Empty, true);
                }
            }
        }
    }
}
