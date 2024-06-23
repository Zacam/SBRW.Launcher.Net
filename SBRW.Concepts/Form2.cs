using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using CredentialManagement;
using Newtonsoft.Json;

namespace SBRW.Concepts
{
    public partial class Form2 : Form
    {
        BindingList<Json_List_Account> Accounts_Cache = new BindingList<Json_List_Account>();
        int Auto_ID = 0;
        Color selectionBackColor;
        Color selectionForeColor;
        string FILE_ACCOUNTS_PATH = "accounts.json";

        public BindingList<Json_List_Account> Read_Data_Base()
        {
            try
            {
                string oldcontent = string.Empty;

                if (File.Exists(FILE_ACCOUNTS_PATH))
                {
                    StreamReader sr = new StreamReader(FILE_ACCOUNTS_PATH);
                    oldcontent = sr.ReadToEnd();
                    sr.Close();
                    sr.Dispose();
                }

                if (!string.IsNullOrWhiteSpace(oldcontent))
                {
                    return JsonConvert.DeserializeObject<BindingList<Json_List_Account>>(oldcontent);
                }
            }
            finally { }

            return new BindingList<Json_List_Account>();
        }

        public void ListCredentials()
        {
            Auto_ID = 0;
            Accounts_Cache.Clear();

            foreach (var Queried_Account in Read_Data_Base())
            {
                if (Auto_ID == 0)
                {
                    TextBox_Min.Text = (Auto_ID = Queried_Account.AID).ToString();
                }
                else if (Queried_Account.AID >= Auto_ID)
                {
                    TextBox_Max.Text = (Auto_ID = Queried_Account.AID).ToString();
                }

                if (new Credential() { Target = Queried_Account.Target, Type = CredentialType.Generic }.Load())
                {
                    Accounts_Cache.Add(Queried_Account);
                }
            }

            DataGridView_Account_List.DataSource = Accounts_Cache;
            DataGridView_Account_List.Refresh();
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DataGridView_Account_List.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Account_List.ReadOnly = true;

            selectionBackColor = DataGridView_Account_List.DefaultCellStyle.BackColor;
            selectionForeColor = DataGridView_Account_List.DefaultCellStyle.ForeColor;

            DataGridView_Account_List.DefaultCellStyle.SelectionBackColor = DataGridView_Account_List.DefaultCellStyle.BackColor;
            DataGridView_Account_List.DefaultCellStyle.SelectionForeColor = DataGridView_Account_List.DefaultCellStyle.ForeColor;
        }

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

        private void Button_Add_Click(object sender, EventArgs e)
        {
            string Email_Live = TextBox_Email.Text;
            string Password_Live = TextBox_Password.Text;

            if((Email_Live.Length > 1) && (Password_Live.Length > 1))
            {
                Auto_ID++;

                Credential New_Credential = new Credential()
                {
                    Target = $"SBRW.Concepts.{Auto_ID}",
                    Username = TextBox_Email.Text,
                    Password = TextBox_Password.Text,
                    Description = !Accounts_Cache.Any(Nickname_Exists => Nickname_Exists.Nickname == TextBox_Nickname.Text) 
                    ? TextBox_Nickname.Text : string.Empty,
                    Type = CredentialType.Generic,
                    PersistanceType = PersistanceType.LocalComputer
                };

                if (!New_Credential.Save())
                {
                    $"Failed to save credential for SBRW.Concepts.{Auto_ID}".Message_Box();
                }
                else
                {
                    Accounts_Cache.Add(new Json_List_Account() { Target = $"SBRW.Concepts.{Auto_ID}", Email = New_Credential.Username, 
                        Password = New_Credential.Password, Nickname = New_Credential.Description, 
                        Created = New_Credential.LastWriteTime.ToString(), AID = Auto_ID});

                    File.WriteAllText(FILE_ACCOUNTS_PATH, JsonConvert.SerializeObject(Accounts_Cache));
                    ListCredentials();
                }
            }
            else
            {
                "Enter Account Info".Message_Box();
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            if(Accounts_Cache.Count > 0)
            {
                try
                {
                    Json_List_Account? Account_Information = DataGridView_Account_List.SelectedRows[0].DataBoundItem as Json_List_Account;
                    if (Account_Information != default)
                    {
                        /* Get "Target" entry for removal */

                        if (!new Credential() { Target = Account_Information.Target }.Delete())
                        {
                            $"Failed to Remove credential: SBRW.Concepts.{Account_Information.Target}".Message_Box();
                        }
                        else
                        {
                            Accounts_Cache.Remove(Account_Information);

                            File.WriteAllText(FILE_ACCOUNTS_PATH, JsonConvert.SerializeObject(Accounts_Cache));
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

        private void button_Update_Click(object sender, EventArgs e)
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
                            PersistanceType = PersistanceType.LocalComputer
                        };

                        if (!Updated_Credential.Save())
                        {
                            $"Failed to Update credential: SBRW.Concepts.{Account_Information.Target}".Message_Box();
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

                            File.WriteAllText(FILE_ACCOUNTS_PATH, JsonConvert.SerializeObject(Accounts_Cache));
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

        private void Form2_Shown(object sender, EventArgs e)
        {
            /* Set Datasource so we can Hide Columns */
            DataGridView_Account_List.DataSource = Accounts_Cache;
            DataGridView_Account_List.Columns["Target"].Visible = false;
            DataGridView_Account_List.Columns["Password"].Visible = false;
            DataGridView_Account_List.Columns["AID"].Visible = false;
            /* Load Data for DataGridView */
            ListCredentials();
        }

        private void CheckBox_Password_Reveal_CheckedChanged(object sender, EventArgs e)
        {
            TextBox_Password.UseSystemPasswordChar = CheckBox_Password_Reveal.CheckState == CheckState.Checked;
        }
    }
}
