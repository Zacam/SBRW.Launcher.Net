using GameLauncher.App.Classes.LauncherCore.Client.Auth;
using GameLauncher.App.Classes.LauncherCore.Client.Web;
using GameLauncher.App.Classes.LauncherCore.Hashes;
using GameLauncher.App.Classes.LauncherCore.Validator;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace GameLauncher.App
{
    public partial class ScreenRegister : Form
    {
        public static bool TicketRequired;

        public ScreenRegister()
        {
            InitializeComponent();
        }

        private void ButtonRegister_Click(object sender, EventArgs e)
        {
            if (!IsValid.Email(UserEmailBox.Text))
            {
                ActionText.Text = "Please type your email!";
            }
            else if (String.IsNullOrEmpty(UserPasswordBox.Text))
            {
                ActionText.Text = "Please type your password!";
            }
            else if (String.IsNullOrEmpty(UserConfirmPasswordBox.Text))
            {
                ActionText.Text = "Please type your confirmation password!";
            }
            else if (UserPasswordBox.Text != UserConfirmPasswordBox.Text)
            {
                ActionText.Text = "Password doesn't match!";
            }
            else if (TicketRequired)
            {
                if (String.IsNullOrEmpty(UserTicketBox.Text))
                {
                    ActionText.Text = "Ticket is required to play on this server!";
                }
                else
                {
                    CreateAccount();
                }
            }
            else
            {
                CreateAccount();
            }
        }

        private void CreateAccount()
        {
            String token = (TicketRequired) ? UserTicketBox.Text : null;
            Tokens.IPAddress = Form1.SelectedServerIP;
            Tokens.ServerName = Form1.SelectedServerName;

            if (Form1.ModernAuthSupport == false)
            {
                ClassicAuth.Register(UserEmailBox.Text, SHA.HashPassword(UserPasswordBox.Text), token);
            }
            else
            {
                ModernAuth.Register(UserEmailBox.Text, UserPasswordBox.Text, token);
            }

            if (!String.IsNullOrEmpty(Tokens.Success))
            {
                MessageBox.Show(null, Tokens.Success, UserAgent.AgentAltName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ActionText.Text = Tokens.Success;
            }
            else
            {
                MessageBox.Show(null, Tokens.Error, UserAgent.AgentAltName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ActionText.Text = Tokens.Error;
            }

            ButtonRegister.Enabled = false;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
