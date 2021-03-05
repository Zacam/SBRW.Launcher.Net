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
    }
}
