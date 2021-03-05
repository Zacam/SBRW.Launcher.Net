using GameLauncher.App.Classes.LauncherCore.Client.Auth;
using GameLauncher.App.Classes.LauncherCore.Lists.JSON;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameLauncher.App.Classes.LauncherCore.Visuals
{
    class FunctionEvents
    {
        /* Server Drop Down List [Sets Categories] (Used on Login Screen) */
        public static void ServerDropDownList_DrawItem(object sender, DrawItemEventArgs e)
        {
            var font = (sender as ComboBox).Font;
            Brush backgroundColor;
            Brush textColor;

            var serverListText = "";

            if (sender is ComboBox cb)
            {
                if (cb.Items[e.Index] is JsonServerList si)
                {
                    serverListText = si.Name;
                }
            }

            if (serverListText.StartsWith("<GROUP>"))
            {
                font = new Font(font, FontStyle.Bold);
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
                e.Graphics.DrawString(serverListText.Replace("<GROUP>", string.Empty), font, Brushes.Black, e.Bounds);
            }
            else
            {
                font = new Font(font, FontStyle.Regular);
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected && e.State != DrawItemState.ComboBoxEdit)
                {
                    backgroundColor = SystemBrushes.Highlight;
                    textColor = SystemBrushes.HighlightText;
                }
                else
                {
                    font = new Font(font, FontStyle.Bold);
                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected && e.State != DrawItemState.ComboBoxEdit)
                    {
                        backgroundColor = SystemBrushes.Highlight;
                        textColor = SystemBrushes.HighlightText;
                    }
                    else
                    {
                        backgroundColor = Brushes.White;
                        textColor = Brushes.Black;
                    }
                }

                e.Graphics.FillRectangle(backgroundColor, e.Bounds);
                e.Graphics.DrawString("    " + serverListText, font, textColor, e.Bounds);
            }
        }

        public static bool ServerIsOnline = false;

        /* Server Drop Down List [When Index Changes] (Used on Login Screen) */
        public static void ServerPick_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tokens.Clear();

            try
            {
                SelectedServerName = ServerDropDownList.Text.ToString().ToUpper();
                SelectedServerIP = new Uri(ServerDropDownList.SelectedValue.ToString()).Host;
                SelectedServerIPRaw = ServerDropDownList.SelectedValue.ToString();

                WebClientWithTimeout serverval = new WebClientWithTimeout();
                var stringToUri = new Uri(ServerDropDownList.SelectedValue.ToString() + "/GetServerInformation");
                String serverdata = serverval.DownloadString(stringToUri);

                Form1.result = JSON.Parse(serverdata);

                Form1.ActionText.Text = "Players on server: " + Form1.result["onlineNumber"];

                try
                {
                    if (string.IsNullOrEmpty(Form1.result["modernAuthSupport"]))
                    {
                        Form1.ModernAuthSupport = false;
                    }
                    else if (Form1.result["modernAuthSupport"])
                    {
                        if (stringToUri.Scheme == "https")
                        {
                            Form1.ModernAuthSupport = true;
                        }
                        else
                        {
                            Form1.ModernAuthSupport = false;
                        }
                    }
                    else
                    {
                        Form1.ModernAuthSupport = false;
                    }
                }
                catch
                {
                    Form1.ModernAuthSupport = false;
                }

                try
                {
                    Form1.TicketRequired = (bool)Form1.result["requireTicket"];
                }
                catch
                {
                    Form1.TicketRequired = true; //lets assume yes, we gonna check later if ticket is empty or not.
                }

                ServerIsOnline = true;
            }
            catch
            {
                ServerIsOnline = false;

                SelectedServerName = "Offline";
                SelectedServerIP = "http://localhost";

                ActionText.Text = "Server is offline.";
            }

            RegisterTicketBox.Enabled = Form1.TicketRequired;
        }
    }
}
