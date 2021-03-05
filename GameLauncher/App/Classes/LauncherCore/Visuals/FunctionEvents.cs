using GameLauncher.App.Classes.LauncherCore.Client.Auth;
using GameLauncher.App.Classes.LauncherCore.Client.Web;
using GameLauncher.App.Classes.LauncherCore.Lists.JSON;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
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
                e.Graphics.FillRectangle(Brushes.LightGray, e.Bounds);
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

        public static void ForgotPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ScreenLogin.result["webRecoveryUrl"]))
            {
                Process.Start(ScreenLogin.result["webRecoveryUrl"]);
                MessageBox.Show(null, "A browser window has been opened to complete password recovery on " + ScreenLogin.SelectedServerName, "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        Uri resetPasswordUrl = new Uri(ScreenLogin.SelectedServerIP + "/RecoveryPassword/forgotPassword");

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
