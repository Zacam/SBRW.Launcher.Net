#region Usings
using SBRW.Launcher.Core.Cache;
using SBRW.Launcher.Core.Extension.Hash_;
using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.Core.Extra.File_;
using SBRW.Launcher.Core.Theme;
using SBRW.Launcher.RunTime.LauncherCore.APICheckers;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.Lists;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using SBRW.Launcher.RunTime.LauncherCore.Support;
using System;
using System.Linq;
using System.Net.Cache;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SBRW.Launcher.Core.Extension.String_;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using SBRW.Launcher.Core.Extension.Web_;
using SBRW.Launcher.Core.Reference.Json_.Newtonsoft_;
using SBRW.Launcher.RunTime.InsiderKit;
using SBRW.Launcher.Core.Extension.Time_;
using SBRW.Launcher.RunTime.LauncherCore.Languages.Visual_Forms;
using Newtonsoft.Json;
using SBRW.Launcher.Core.Discord.RPC_;
using SBRW.Launcher.Core.Downloader.Extension_;
using SBRW.Launcher.Core.Downloader.LZMA.Extension_;
using SBRW.Launcher.Core.Extension.Numbers_;
using SBRW.Launcher.Core.Extension.Validation_;
using System.Diagnostics;
using SBRW.Launcher.Core.Extension.Validation_.Json_.Newtonsoft_;
using SBRW.Launcher.App.UI_Forms.Account_Manager_Screen;
using SBRW.Launcher.App.UI_Forms.Parent_Screen;
using SBRW.Launcher.RunTime.LauncherCore.Visuals;
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
        private void ButtonClose_MouseDown(object sender, EventArgs e)
        {
            Button_Close.BackgroundImage = Button_Close.Icon_Order(SVG_Icon.Cross, SVG_Color.Error_Select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClose_MouseEnter(object sender, EventArgs e)
        {
            Button_Close.BackgroundImage = Button_Close.Icon_Order(SVG_Icon.Cross, SVG_Color.Error_Highlight);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClose_MouseLeaveANDMouseUp(object sender, EventArgs e)
        {
            Button_Close.BackgroundImage = Button_Close.Icon_Order(SVG_Icon.Cross, SVG_Color.White);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSecurityCenter_MouseDown(object sender, EventArgs e)
        {
            Button_Security_Center.BackgroundImage = Button_Security_Center.SecurityCenterIcon(1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSecurityCenter_MouseEnter(object sender, EventArgs e)
        {
            Button_Security_Center.BackgroundImage = Button_Security_Center.SecurityCenterIcon(2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSecurityCenter_MouseLeaveANDMouseUp(object sender, EventArgs e)
        {
            Button_Security_Center.BackgroundImage = Button_Security_Center.SecurityCenterIcon(0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSettings_MouseDown(object sender, EventArgs e)
        {
            Button_Settings.BackgroundImage = (Save_Settings.Live_Data.Game_Integrity == "Good") ?
                Button_Settings.Icon_Order(SVG_Icon.Gear, SVG_Color.White_Select) :
                Button_Settings.Icon_Order(SVG_Icon.Gear, SVG_Color.Warning_Select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSettings_MouseEnter(object sender, EventArgs e)
        {
            Button_Settings.BackgroundImage = (Save_Settings.Live_Data.Game_Integrity == "Good") ?
                Button_Settings.Icon_Order(SVG_Icon.Gear, SVG_Color.White_Highlight) :
                Button_Settings.Icon_Order(SVG_Icon.Gear, SVG_Color.Warning_Highlight);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSettings_MouseLeaveANDMouseUp(object sender, EventArgs e)
        {
            Button_Settings.BackgroundImage = (Save_Settings.Live_Data.Game_Integrity == "Good") ?
                Button_Settings.Icon_Order(SVG_Icon.Gear, SVG_Color.White) :
                Button_Settings.Icon_Order(SVG_Icon.Gear, SVG_Color.Warning);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Greenbutton_hover_MouseEnter(object sender, EventArgs e)
        {
            Button_Register.BackgroundImage = Image_Button.Green_Hover;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Greenbutton_MouseLeave(object sender, EventArgs e)
        {
            Button_Register.BackgroundImage = Image_Button.Green;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Greenbutton_hover_MouseUp(object sender, EventArgs e)
        {
            Button_Register.BackgroundImage = Image_Button.Green_Hover;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Greenbutton_click_MouseDown(object sender, EventArgs e)
        {
            Button_Register.BackgroundImage = Image_Button.Green_Click;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Email_TextChanged(object sender, EventArgs e)
        {
            Picture_Input_Email.Image = Picture_Input_Email.Icon_Order(SVG_Icon.Input_Box_Email, SVG_Color.Base);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Password_TextChanged(object sender, EventArgs e)
        {
            Picture_Input_Email.Image = Picture_Input_Email.Icon_Order(SVG_Icon.Input_Box_Email, SVG_Color.Base);
            Picture_Input_Password.Image = Picture_Input_Password.Icon_Order(SVG_Icon.Input_Box_Password, SVG_Color.Base);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Graybutton_click_MouseDown(object sender, EventArgs e)
        {
            if (Button_Logout.BackgroundImage != Image_Button.Grey_Click)
            {
                Button_Logout.BackgroundImage = Image_Button.Grey_Click;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Graybutton_hover_MouseEnter(object sender, EventArgs e)
        {
            if (Button_Logout.BackgroundImage != Image_Button.Grey_Hover)
            {
                Button_Logout.BackgroundImage = Image_Button.Grey_Hover;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Graybutton_MouseLeave(object sender, EventArgs e)
        {
            if (Button_Logout.BackgroundImage != Image_Button.Grey)
            {
                Button_Logout.BackgroundImage = Image_Button.Grey;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Graybutton_hover_MouseUp(object sender, EventArgs e)
        {
            if (Button_Logout.BackgroundImage != Image_Button.Grey_Hover)
            {
                Button_Logout.BackgroundImage = Image_Button.Grey_Hover;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Loginbuttonenabler(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Input_Email.Text) || string.IsNullOrWhiteSpace(Input_Password.Text))
            {
                LoginEnabled = false;
                Button_Login.BackgroundImage = Image_Button.Grey;
                Button_Login.ForeColor = Color_Text.L_Six;
            }
            else
            {
                LoginEnabled = true;
                Button_Login.BackgroundImage = Image_Button.Grey;
                Button_Login.ForeColor = Color_Text.L_Five;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_MouseUp(object sender, EventArgs e)
        {
            if (LoginEnabled || Builtinserver)
            {
                Button_Login.BackgroundImage = Image_Button.Grey_Hover;
            }
            else
            {
                Button_Login.BackgroundImage = Image_Button.Grey;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_MouseDown(object sender, EventArgs e)
        {
            if (LoginEnabled || Builtinserver)
            {
                Button_Login.BackgroundImage = Image_Button.Grey_Click;
            }
            else
            {
                Button_Login.BackgroundImage = Image_Button.Grey;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_MouseEnter(object sender, EventArgs e)
        {
            Button_Login.BackgroundImage = (LoginEnabled || Builtinserver) ? Image_Button.Grey_Hover : Image_Button.Grey;
            Button_Login.ForeColor = (LoginEnabled || Builtinserver) ? Color_Text.L_Five : Color_Text.L_Six;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_MouseLeave(object sender, EventArgs e)
        {
            Button_Login.BackgroundImage = (LoginEnabled || Builtinserver) ? Image_Button.Grey : Image_Button.Grey;
            Button_Login.ForeColor = (LoginEnabled || Builtinserver) ? Color_Text.L_Five : Color_Text.L_Six;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogoutButton_Click(object sender, EventArgs e)
        {
            if (DisableLogout == true)
            {
                return;
            }

            LoggedInFormElements(false);
            LoginFormElements(true);

            UserId = string.Empty;
            LoginToken = string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayButton_MouseUp(object sender, EventArgs e)
        {
            if (Playenabled == false)
            {
                return;
            }

            if (Button_Play_OR_Update.BackgroundImage != Image_Button.Play_Hover)
            {
                Button_Play_OR_Update.BackgroundImage = Image_Button.Play_Hover;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayButton_MouseDown(object sender, EventArgs e)
        {
            if (Playenabled == false)
            {
                return;
            }

            if (Button_Play_OR_Update.BackgroundImage != Image_Button.Play_Click)
            {
                Button_Play_OR_Update.BackgroundImage = Image_Button.Play_Click;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayButton_MouseEnter(object sender, EventArgs e)
        {
            if (Playenabled == false)
            {
                return;
            }

            if (Button_Play_OR_Update.BackgroundImage != Image_Button.Play_Hover)
            {
                Button_Play_OR_Update.BackgroundImage = Image_Button.Play_Hover;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayButton_MouseLeave(object sender, EventArgs e)
        {
            if (Playenabled == false)
            {
                return;
            }

            if (Button_Play_OR_Update.BackgroundImage != Image_Button.Play)
            {
                Button_Play_OR_Update.BackgroundImage = Image_Button.Play;
            }
        }
        /// <summary>
        /// After Successful Login, Hide Login Forms
        /// </summary>
        /// <param name="hideElements"></param>
        private void LoggedInFormElements(bool hideElements)
        {
            if (hideElements == true)
            {
                try
                {
                    DateTime currentTime = DateTime.Now;

                    if ((currentTime.Hour >= 5) && (currentTime.Hour < 12))
                    {
                        LoginWelcomeTime = "Good Morning";
                    }
                    else if ((currentTime.Hour >= 12) && (currentTime.Hour < 18))
                    {
                        LoginWelcomeTime = "Good Afternoon";
                    }
                    else if ((currentTime.Hour >= 18) && (currentTime.Hour < 22))
                    {
                        LoginWelcomeTime = "Good Evening";
                    }
                    else
                    {
                        LoginWelcomeTime = "Hello Night Owl";
                    }
                }
                catch (Exception Error)
                {
                    LogToFileAddons.OpenLog("LOGIN TIME", string.Empty, Error, string.Empty, true);
                    LoginWelcomeTime = "Pshhh Pshhh";
                }

                Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Input_Email.Text).ToUpper();
                if (Picture_Information_Window.Image != Image_Other.Information_Window_Success)
                {
                    Picture_Information_Window.Image = Image_Other.Information_Window_Success;
                }

                if (Picture_Information_Window.Image != Image_Other.Information_Window_Success)
                {
                    Picture_Information_Window.Image = Image_Other.Information_Window_Success;
                }
                Button_Play_OR_Update.ForeColor = Color_Text.L_Five;
                Button_Play_OR_Update.Visible = hideElements;

                if (Button_Logout.BackgroundImage != Image_Button.Grey)
                {
                    Button_Logout.BackgroundImage = Image_Button.Grey;
                }
                Button_Logout.ForeColor = Color_Text.L_Five;
            }

            Panel_Launch.Visible = hideElements;
        }
        /// <summary>
        /// After Logout, Show Login Forms
        /// </summary>
        /// <param name="hideElements"></param>
        private void LoginFormElements(bool hideElements)
        {
            if (hideElements == true)
            {
                Label_Information_Window.Text = "Enter Your Account Information to Log In".ToUpper();
            }

            CheckBox_Remember_Us.Visible = hideElements;
            Button_Login.Visible = hideElements;

            Button_Register.Visible = hideElements;
            Input_Email.Visible = hideElements;
            Input_Password.Visible = hideElements;
            LinkLabel_Forgot_Password.Visible = hideElements;
            Button_Settings.Visible = hideElements;
            Button_Security_Center.Visible = hideElements;

            Button_Custom_Server.Enabled = hideElements;
            ComboBox_Server_List.Enabled = hideElements;

            /* Input Strokes */
            Picture_Input_Email.Visible = hideElements;
            Picture_Input_Password.Visible = hideElements;
            Picture_Input_Email.Image = Picture_Input_Email.Icon_Order(SVG_Icon.Input_Box_Email, SVG_Color.Base);
            Picture_Input_Password.Image = Picture_Input_Password.Icon_Order(SVG_Icon.Input_Box_Password, SVG_Color.Base);

            if (Picture_Information_Window.Image != Image_Other.Information_Window)
            {
                Picture_Information_Window.Image = Image_Other.Information_Window;
            }
        }
        /// <summary>
        /// Social Panel | Ping or Offline or DEV Servers |
        /// </summary>
        private void DisableSocialPanelandClearIt()
        {
            /* Hides Social Panel */
            Panel_Server_Information.Visible = false;
            /* Home */
            Picture_Icon_Server_Home.BackgroundImage = Picture_Icon_Server_Home.Icon_Order(SVG_Icon.Home, SVG_Color.Unknown);
            LinkLabel_Server_Home.Enabled = false;
            /* Discord */
            Picture_Icon_Server_Discord.BackgroundImage = Picture_Icon_Server_Discord.Icon_Order(SVG_Icon.Discord, SVG_Color.Unknown);
            LinkLabel_Server_Discord.Enabled = false;
            /* Facebook */
            Picture_Icon_Server_Facebook.BackgroundImage = Picture_Icon_Server_Facebook.Icon_Order(SVG_Icon.Facebook, SVG_Color.Unknown);
            LinkLabel_Server_Facebook.Enabled = false;
            /* Twitter */
            Picture_Icon_Server_Twitter.BackgroundImage = Picture_Icon_Server_Twitter.Icon_Order(SVG_Icon.Twitter, SVG_Color.Unknown);
            LinkLabel_Server_Twitter.Enabled = false;
            /* Scenery */
            Label_Server_Scenery.Text = "But It's Me!";
            /* Restart Timer */
            Label_Server_Force_Restart_Timer.Text = "Game Launcher!";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Color_Mode"></param>
        /// <param name="Login_Icon_Color"></param>
        private void Display_Color_Icons(int Color_Mode = 0, bool Login_Icon_Color = true)
        {
            try
            {
                if (ProgressBar.Value < 100)
                {
                    if (ProgressBar.InvokeRequired)
                    {
                        ProgressBar.Invoke(new Action(delegate ()
                        {
                            ProgressBar.Value = 100;
                        }));
                    }
                    else
                    {
                        ProgressBar.Value = 100;
                    }
                }

                switch (Color_Mode)
                {
                    /* Checking (Pinging Blue) */
                    case 1:
                        if (Picture_Bar_Outline.Tag == default || !Picture_Bar_Outline.Tag.Equals(0))
                        {
                            if (Picture_Bar_Outline.InvokeRequired)
                            {
                                Picture_Bar_Outline.SafeInvokeAction(() =>
                                {
                                    Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Checking_Outline;
                                    Picture_Bar_Outline.Tag = Image_ProgressBar.Checking_Outline.Tag;
                                }, this);
                            }
                            else
                            {
                                Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Checking_Outline;
                                Picture_Bar_Outline.Tag = Image_ProgressBar.Checking_Outline.Tag;
                            }
                        }

                        if (ProgressBar.BackColor != Color_Winform_Other.ProgressBar_Loading_Top)
                        {
                            if (ProgressBar.InvokeRequired)
                            {
                                ProgressBar.Invoke(new Action(delegate ()
                                {
                                    ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Loading_Top;
                                    ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Loading_Bottom;

                                }));
                            }
                            else
                            {
                                ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Loading_Top;
                                ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Loading_Bottom;

                            }
                        }

                        if (Login_Icon_Color)
                        {
                            if (Picture_Information_Window.Tag == default || !Picture_Information_Window.Tag.Equals(0))
                            {
                                if (Picture_Information_Window.InvokeRequired)
                                {
                                    Picture_Information_Window.SafeInvokeAction(() =>
                                    {
                                        Picture_Information_Window.Image = Image_Other.Information_Window;
                                        Picture_Information_Window.Tag = 0;
                                    }, this);
                                }
                                else
                                {
                                    Picture_Information_Window.Image = Image_Other.Information_Window;
                                    Picture_Information_Window.Tag = 0;
                                }
                            }
                        }
                        break;
                    /* Error (Red) */
                    case 2:
                        if (Picture_Bar_Outline.Tag == default || !Picture_Bar_Outline.Tag.Equals(3))
                        {
                            if (Picture_Bar_Outline.InvokeRequired)
                            {
                                Picture_Bar_Outline.SafeInvokeAction(() =>
                                {
                                    Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Error_Outline;
                                    Picture_Bar_Outline.Tag = Image_ProgressBar.Error_Outline.Tag;
                                }, this);
                            }
                            else
                            {
                                Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Error_Outline;
                                Picture_Bar_Outline.Tag = Image_ProgressBar.Error_Outline.Tag;
                            }
                        }

                        if (ProgressBar.BackColor != Color_Winform_Other.ProgressBar_Error_Top)
                        {
                            if (ProgressBar.InvokeRequired)
                            {
                                ProgressBar.Invoke(new Action(delegate ()
                                {
                                    ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Error_Top;
                                    ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Error_Bottom;

                                }));
                            }
                            else
                            {
                                ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Error_Top;
                                ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Error_Bottom;

                            }
                        }

                        if (Login_Icon_Color)
                        {
                            if (Picture_Information_Window.Tag == default || !Picture_Information_Window.Tag.Equals(3))
                            {
                                if (Picture_Information_Window.InvokeRequired)
                                {
                                    Picture_Information_Window.SafeInvokeAction(() =>
                                    {
                                        Picture_Information_Window.Image = Image_Other.Information_Window_Error;
                                        Picture_Information_Window.Tag = 3;
                                    }, this);
                                }
                                else
                                {
                                    Picture_Information_Window.Image = Image_Other.Information_Window_Error;
                                    Picture_Information_Window.Tag = 3;
                                }
                            }
                        }
                        break;
                    /* Warning (Yellow) */
                    case 3:
                        if (Picture_Bar_Outline.Tag == default || !Picture_Bar_Outline.Tag.Equals(2))
                        {
                            if (Picture_Bar_Outline.InvokeRequired)
                            {
                                Picture_Bar_Outline.SafeInvokeAction(() =>
                                {
                                    Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Warning_Outline;
                                    Picture_Bar_Outline.Tag = Image_ProgressBar.Warning_Outline.Tag;
                                }, this);
                            }
                            else
                            {
                                Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Warning_Outline;
                                Picture_Bar_Outline.Tag = Image_ProgressBar.Warning_Outline.Tag;
                            }
                        }

                        if (ProgressBar.BackColor != Color_Winform_Other.ProgressBar_Warning_Top)
                        {
                            if (ProgressBar.InvokeRequired)
                            {
                                ProgressBar.Invoke(new Action(delegate ()
                                {
                                    ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Warning_Top;
                                    ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Warning_Bottom;

                                }));
                            }
                            else
                            {
                                ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Warning_Top;
                                ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Warning_Bottom;

                            }
                        }

                        if (Login_Icon_Color)
                        {
                            if (Picture_Information_Window.Tag == default || !Picture_Information_Window.Tag.Equals(2))
                            {
                                if (Picture_Information_Window.InvokeRequired)
                                {
                                    Picture_Information_Window.SafeInvokeAction(() =>
                                    {
                                        Picture_Information_Window.Image = Image_Other.Information_Window_Warning;
                                        Picture_Information_Window.Tag = 2;
                                    }, this);
                                }
                                else
                                {
                                    Picture_Information_Window.Image = Image_Other.Information_Window_Warning;
                                    Picture_Information_Window.Tag = 2;
                                }
                            }
                        }
                        break;
                    /* Unknown (Gray) */
                    case 4:
                        if (Picture_Bar_Outline.Tag == default || !Picture_Bar_Outline.Tag.Equals(4))
                        {
                            if (Picture_Bar_Outline.InvokeRequired)
                            {
                                Picture_Bar_Outline.SafeInvokeAction(() =>
                                {
                                    Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Preload_Outline;
                                    Picture_Bar_Outline.Tag = Image_ProgressBar.Preload_Outline.Tag;
                                }, this);
                            }
                            else
                            {
                                Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Preload_Outline;
                                Picture_Bar_Outline.Tag = Image_ProgressBar.Preload_Outline.Tag;
                            }
                        }

                        if (ProgressBar.BackColor != Color_Winform_Other.ProgressBar_Unknown_Top)
                        {
                            if (ProgressBar.InvokeRequired)
                            {
                                ProgressBar.Invoke(new Action(delegate ()
                                {
                                    ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Unknown_Top;
                                    ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Unknown_Bottom;

                                }));
                            }
                            else
                            {
                                ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Unknown_Top;
                                ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Unknown_Bottom;

                            }
                        }

                        if (Login_Icon_Color)
                        {
                            if (Picture_Information_Window.Tag == default || !Picture_Information_Window.Tag.Equals(4))
                            {
                                if (Picture_Information_Window.InvokeRequired)
                                {
                                    Picture_Information_Window.SafeInvokeAction(() =>
                                    {
                                        Picture_Information_Window.Image = Image_Other.Information_Window_Unknown;
                                        Picture_Information_Window.Tag = 4;
                                    }, this);
                                }
                                else
                                {
                                    Picture_Information_Window.Image = Image_Other.Information_Window_Unknown;
                                    Picture_Information_Window.Tag = 4;
                                }
                            }
                        }
                        break;
                    /* Complete (Green) */
                    default:
                        if (Picture_Bar_Outline.Tag == default || !Picture_Bar_Outline.Tag.Equals(1))
                        {
                            if (Picture_Bar_Outline.InvokeRequired)
                            {
                                Picture_Bar_Outline.SafeInvokeAction(() =>
                                {
                                    Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Complete_Outline;
                                    Picture_Bar_Outline.Tag = Image_ProgressBar.Complete_Outline.Tag;
                                }, this);
                            }
                            else
                            {
                                Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Complete_Outline;
                                Picture_Bar_Outline.Tag = Image_ProgressBar.Complete_Outline.Tag;
                            }
                        }

                        if (ProgressBar.BackColor != Color_Winform_Other.ProgressBar_Sucess_Top)
                        {
                            if (ProgressBar.InvokeRequired)
                            {
                                ProgressBar.Invoke(new Action(delegate ()
                                {
                                    ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Sucess_Top;
                                    ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Sucess_Bottom;

                                }));
                            }
                            else
                            {
                                ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Sucess_Top;
                                ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Sucess_Bottom;

                            }
                        }

                        if (Login_Icon_Color)
                        {
                            if (Picture_Information_Window.Tag == default || !Picture_Information_Window.Tag.Equals(1))
                            {
                                if (Picture_Information_Window.InvokeRequired)
                                {
                                    Picture_Information_Window.SafeInvokeAction(() =>
                                    {
                                        Picture_Information_Window.Image = Image_Other.Information_Window_Success;
                                        Picture_Information_Window.Tag = 1;
                                    }, this);
                                }
                                else
                                {
                                    Picture_Information_Window.Image = Image_Other.Information_Window_Success;
                                    Picture_Information_Window.Tag = 1;
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception Error_Live)
            {
                LogToFileAddons.OpenLog("Display_Color_Icons", string.Empty, Error_Live, string.Empty, true);
            }
        }
        /// <summary>
        /// Disable Play Button and Logout Buttons
        /// </summary>
        /// <param name="Enabler_Mode"></param>
        private void Button_Login_Logout_Modes(bool Enabler_Mode = false, bool Disable_Play_Button = false)
        {
            if (Enabler_Mode)
            {
                Button_Play_OR_Update.Visible = Disable_Play_Button ? false : true;
                Button_Logout.Visible = EnablePlayButton(true);
                DisableLogout = false;
            }
            else
            {
                Button_Play_OR_Update.Visible = Button_Logout.Visible = DisablePlayButton();
                DisableLogout = true;
            }
        }
        /// <summary>
        /// Check Serverlist API Status Upon Main Screen load - DavidCarbon
        /// </summary>
        private void PingServerListAPIStatus()
        {
            Label_Status_API.Text = "United API:\n - Online";
            Label_Status_API.ForeColor = Color_Text.S_Sucess;
            Label_Status_API_Details.Text = "Connected to API";
            Picture_Icon_API.BackgroundImage = Picture_Icon_API.Icon_Order(SVG_Icon.Globe, SVG_Color.Success);

            if (!VisualsAPIChecker.UnitedAPI())
            {
                Label_Status_API.Text = "Carbon API:\n - Online";
                Label_Status_API.ForeColor = Color_Text.S_Sucess;
                Label_Status_API_Details.Text = "Connected to API";
                Picture_Icon_API.BackgroundImage = Picture_Icon_API.Icon_Order(SVG_Icon.Globe, SVG_Color.Success);

                if (!VisualsAPIChecker.CarbonAPI())
                {
                    Label_Status_API.Text = "Carbon 2nd API:\n - Online";
                    Label_Status_API.ForeColor = Color_Text.S_Sucess;
                    Label_Status_API_Details.Text = "Connected to API";
                    Picture_Icon_API.BackgroundImage = Picture_Icon_API.Icon_Order(SVG_Icon.Globe, SVG_Color.Success);

                    if (!VisualsAPIChecker.CarbonAPITwo())
                    {
                        Label_Status_API.Text = "Cached API:\n - Local";
                        Label_Status_API.ForeColor = Color_Text.S_Warning;
                        Label_Status_API_Details.Text = "Using Local Cache";
                        Picture_Icon_API.BackgroundImage = Picture_Icon_API.Icon_Order(SVG_Icon.Plug, SVG_Color.Warning);

                        if (!VisualsAPIChecker.Local_Cached_API())
                        {
                            Label_Status_API.Text = "Connection API:\n - Error";
                            Label_Status_API.ForeColor = Color_Text.S_Error;
                            Label_Status_API_Details.Text = "Launcher is Offline";
                            Picture_Icon_API.BackgroundImage = Picture_Icon_API.Icon_Order(SVG_Icon.Offline, SVG_Color.Error);
                            Log.Api("PINGING API: Failed to Connect to APIs! Quick Hide and Bunker Down! (Ask for help)");
                        }
                        else
                        {
                            Log.Api("PINGING API: Failed to Connect to APIs! Using Local Cache! (Ask for help)");
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="From_Registration"></param>
        public static void Clear_Hide_Screen_Form_Panel(bool From_Registration = false)
        {
            if (Screen_Instance != null)
            {
                if (Screen_Instance.Panel_Form_Screens.Visible)
                {
                    Screen_Instance.Panel_Form_Screens.Controls.Clear();
                    Screen_Instance.Panel_Form_Screens.Visible = false;
                }
                else if (Screen_Instance.Panel_Register_Screen.Visible)
                {
                    Screen_Instance.Panel_Register_Screen.Controls.Clear();
                    Screen_Instance.Panel_Register_Screen.Visible = false;
                }
            }

            if (Screen_Parent.Screen_Instance != null)
            {
                Screen_Parent.Screen_Instance.Text = "SBRW Launcher: " + Application.ProductVersion;
            }
        }
        #region Game Server Information Download
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_Server_List_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picture_Input_Email.Image = Picture_Input_Email.Icon_Order(SVG_Icon.Input_Box_Email, SVG_Color.Base);
            Picture_Input_Password.Image = Picture_Input_Password.Icon_Order(SVG_Icon.Input_Box_Password, SVG_Color.Base);
            /* Disable Certain Functions */
            LoginEnabled = ServerEnabled = FunctionStatus.AllowRegistration = false;
            Launcher_Value.Launcher_Select_Server_JSON = null;
            /* Disable Login & Register Button */
            Button_Login.Enabled = Button_Register.Enabled = false;
            /* Disable Social Panel when switching */
            DisableSocialPanelandClearIt();

            if (!ServerListUpdater.LoadedList && Launcher_Value.Launcher_Select_Server_Data == null)
            {
                Label_Status_Game_Server.Text = "Launcher Offline:\n - Unknown";
                Label_Status_Game_Server.ForeColor = Color_Text.L_Three;
                Label_Status_Game_Server_Data.Text = string.Empty;
                Picture_Icon_Server.BackgroundImage = Picture_Icon_Server.Icon_Order(SVG_Icon.Offline, SVG_Color.Unknown);
                return;
            }

            /* Stops any actions for a Server by comparing Live Instance of the Selected Cache */
            Json_List_Server Cached_Server_GSI = Launcher_Value.Launcher_Select_Server_Data = (Json_List_Server)ComboBox_Server_List.SelectedItem;

            if (Launcher_Value.Launcher_Select_Server_Data.IsSpecial)
            {
                ComboBox_Server_List.SelectedIndex = LastSelectedServerId;
                return;
            }

            if (!SkipServerTrigger) { return; }

            LastSelectedServerId = ComboBox_Server_List.SelectedIndex;

            Label_Status_Game_Server.Text = "Server Status:\n - Pinging";
            Label_Status_Game_Server.ForeColor = Color_Text.L_Two;
            Label_Status_Game_Server_Data.Text = string.Empty;
            Picture_Icon_Server.BackgroundImage = Picture_Icon_Server.Icon_Order(SVG_Icon.Server, SVG_Color.Base);

            Button_Login.ForeColor = Color_Text.L_Six;
            string Banner_Cache_Folder = Path.Combine(Locations.LauncherDataFolder, "Bin", "Server", "Banner", "EyeCatcher");
            string Banner_Cache_File = Path.Combine(Banner_Cache_Folder, Launcher_Value.Launcher_Select_Server_Data.IPAddress.Hash_String(1) + ".bin");
            Picture_Server_Banner.Image = Image_Handler.Grayscale(Banner_Cache_File) ?? Image_Other.Server_Banner;
            Picture_Server_Banner.BackColor = Color.Transparent;
            string ImageUrl = string.Empty;
            string numPlayers = string.Empty;
            string numRegistered = string.Empty;

            if (ComboBox_Server_List.GetItemText(ComboBox_Server_List.SelectedItem) == "Offline Built-In Server")
            {
                Builtinserver = true;
                if (Button_Login.BackgroundImage != Image_Button.Grey)
                {
                    Button_Login.BackgroundImage = Image_Button.Grey;
                }
                Button_Login.Text = "Launch".ToUpper();
                Button_Login.ForeColor = Color_Text.L_Five;
                Panel_Server_Information.Visible = false;
            }
            else
            {
                Builtinserver = false;
                if (Button_Login.BackgroundImage != Image_Button.Grey)
                {
                    Button_Login.BackgroundImage = Image_Button.Grey;
                }
                Button_Login.Text = "Login".ToUpper();
                Button_Login.ForeColor = Color_Text.L_Six;
                Panel_Server_Information.Visible = false;
            }

            Uri ServerURI = new Uri(Launcher_Value.Launcher_Select_Server_Data.IPAddress + "/GetServerInformation");
            ServicePointManager.FindServicePoint(ServerURI).ConnectionLeaseTimeout = (int)TimeSpan.FromSeconds(Launcher_Value.Launcher_WebCall_Timeout_Enable ?
                                    Launcher_Value.Launcher_WebCall_Timeout() : 60).TotalMilliseconds;
            var Client = new WebClient
            {
                Encoding = Encoding.UTF8,
                CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
            };
            if (!Launcher_Value.Launcher_Alternative_Webcalls())
            {
                Client = new WebClientWithTimeout { Encoding = Encoding.UTF8, CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore) };
            }
            else
            {
                Client.Headers.Add("user-agent", "SBRW Launcher " +
                Application.ProductVersion + " (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)");
            }

            Client.DownloadStringAsync(ServerURI);

            System.Timers.Timer aTimer = new System.Timers.Timer(Launcher_Value.Launcher_WebCall_Timeout_Enable ? Launcher_Value.Launcher_WebCall_Timeout() * 1000 : 10000);
            aTimer.Elapsed += (x, y) =>
            {
                if (Client != default)
                {
                    Client.CancelAsync();
                }

                try
                {
                    if (aTimer != default)
                    {
                        if (aTimer.Enabled)
                        {
                            aTimer.Stop();
                            aTimer.Dispose();
                        }
                    }
                }
#if (DEBUG || DEBUG_UNIX)
                catch (Exception Error)
                {
                    LogToFileAddons.OpenLog("aTimer", string.Empty, Error, string.Empty, true);
                }
#else
                catch
                {

                }
#endif
            };
            aTimer.AutoReset = false;
            aTimer.Enabled = true;

            Client.DownloadStringCompleted += (sender2, e2) =>
            {
                try
                {
                    if (aTimer.Enabled)
                    {
                        aTimer.Stop();
                        aTimer.Dispose();
                    }
                }
#if (DEBUG || DEBUG_UNIX)
                catch (Exception Error)
                {
                    LogToFileAddons.OpenLog("aTimer [Download Complete]", string.Empty, Error, string.Empty, true);
                }
#else
                catch
                {

                }
#endif

                bool GSIErrorFree = true;

                if (e2.Cancelled || e2.Error != null)
                {
                    Launcher_Value.Launcher_Select_Server_JSON = null;
                    Picture_Icon_Server.BackgroundImage = Picture_Icon_Server.Icon_Order(SVG_Icon.Offline, SVG_Color.Error);
                    Label_Status_Game_Server.Text = "Server Status:\n - Offline ( OFF )";
                    Label_Status_Game_Server.ForeColor = Color_Text.S_Error;
                    Label_Status_Game_Server_Data.Text = (e2.Error != null) ?
                    (e2.Error.Message ?? "Server Seems to be Offline").Encode_UTF8() : "Failed to Connect to Server";

                    if (!InformationCache.ServerStatusBook.ContainsKey(Launcher_Value.Launcher_Select_Server_Data.ID))
                    {
                        InformationCache.ServerStatusBook.Add(Launcher_Value.Launcher_Select_Server_Data.ID, (e2.Error != null) ? 0 : 3);
                    }
                    else
                    {
                        InformationCache.ServerStatusBook[Launcher_Value.Launcher_Select_Server_Data.ID] = (e2.Error != null) ? 0 : 3;
                    }

                    if (e2.Error != null)
                    {
                        LogToFileAddons.OpenLog("JSON GSI", string.Empty, e2.Error, string.Empty, true);
                    }

                    Client?.Dispose();
                }
                else if (Cached_Server_GSI.Equals(Launcher_Value.Launcher_Select_Server_Data))
                {
                    if (ServerListUpdater.ServerName("Ping") == "Offline Built-In Server")
                    {
                        numPlayers = "∞";
                        numRegistered = "∞";
                    }
                    else
                    {
                        try
                        {
                            Launcher_Value.Launcher_Select_Server_JSON = JsonConvert.DeserializeObject<Json_Server_Info>(e2.Result);
                        }
                        catch (Exception Error)
                        {
                            if (BuildBeta.Allowed() || BuildDevelopment.Allowed())
                            {
                                try { Log.Error("JSON GSI (Received): " + e2.Result); }
                                catch { Log.Error("JSON GSI (Received): Unable to Get Result"); }
                            }
                            else
                            {
                                Log.Error("JSON GSI: Invalid");
                            }

                            LogToFileAddons.OpenLog("JSON GSI", string.Empty, Error, string.Empty, true);
                            GSIErrorFree = false;
                            Launcher_Value.Launcher_Select_Server_JSON = null;
                        }

                        if (!InformationCache.ServerStatusBook.ContainsKey(Launcher_Value.Launcher_Select_Server_Data.ID))
                        {
                            InformationCache.ServerStatusBook.Add(Launcher_Value.Launcher_Select_Server_Data.ID, (!GSIErrorFree) ? 3 : 1);
                        }
                        else
                        {
                            InformationCache.ServerStatusBook[Launcher_Value.Launcher_Select_Server_Data.ID] = (!GSIErrorFree) ? 3 : 1;
                        }

                        if (GSIErrorFree && (Launcher_Value.Launcher_Select_Server_JSON != null))
                        {
                            try
                            {
                                if (!string.IsNullOrWhiteSpace(Launcher_Value.Launcher_Select_Server_JSON.Server_Banner))
                                {
                                    bool ServerBannerResult;

                                    try
                                    {
                                        ServerBannerResult = Uri.TryCreate(Launcher_Value.Launcher_Select_Server_JSON.Server_Banner, UriKind.Absolute, out Uri? uriResult) &&
                                        (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                                    }
                                    catch
                                    {
                                        ServerBannerResult = false;
                                    }

                                    ImageUrl = ServerBannerResult ? Launcher_Value.Launcher_Select_Server_JSON.Server_Banner : string.Empty;
                                }
                                else
                                {
                                    ImageUrl = string.Empty;
                                }
                            }
                            catch
                            {

                            }

                            /* Social Panel Core */

                            /* Discord Invite Display */
                            try
                            {
                                bool ServerDiscordLink;
                                try
                                {
                                    ServerDiscordLink = Uri.TryCreate(Launcher_Value.Launcher_Select_Server_JSON.Server_Social_Discord, UriKind.Absolute, out Uri? uriResult) &&
                                                             (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                                }
                                catch
                                {
                                    ServerDiscordLink = false;
                                }
                                Picture_Icon_Server_Discord.BackgroundImage = ServerDiscordLink ? 
                                Picture_Icon_Server_Discord.Icon_Order(SVG_Icon.Discord, SVG_Color.Base) : 
                                Picture_Icon_Server_Discord.Icon_Order(SVG_Icon.Discord, SVG_Color.Unknown);
                                LinkLabel_Server_Discord.Enabled = ServerDiscordLink;
                                LinkLabel_Server_Discord.Text = ServerDiscordLink ? "Discord Invite" : string.Empty;
                            }
                            catch
                            {

                            }

                            /* Homepage Display */
                            try
                            {
                                bool ServerWebsiteLink;
                                try
                                {
                                    ServerWebsiteLink = Uri.TryCreate(Launcher_Value.Launcher_Select_Server_JSON.Server_Social_Home, UriKind.Absolute, out Uri? uriResult) &&
                                              (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                                }
                                catch
                                {
                                    ServerWebsiteLink = false;
                                }
                                Picture_Icon_Server_Home.BackgroundImage = ServerWebsiteLink ? 
                                Picture_Icon_Server_Home.Icon_Order(SVG_Icon.Home, SVG_Color.Base) :
                                Picture_Icon_Server_Home.Icon_Order(SVG_Icon.Home, SVG_Color.Unknown);
                                LinkLabel_Server_Home.Enabled = ServerWebsiteLink;
                                LinkLabel_Server_Home.Text = ServerWebsiteLink ? "Home Page" : string.Empty;
                            }
                            catch
                            {

                            }

                            /* Facebook Group Display */
                            try
                            {
                                bool ServerFacebookLink;
                                try
                                {
                                    ServerFacebookLink = Uri.TryCreate(Launcher_Value.Launcher_Select_Server_JSON.Server_Social_Facebook, UriKind.Absolute, out Uri? uriResult) &&
                                                         (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                                }
                                catch
                                {
                                    ServerFacebookLink = false;
                                }
                                Picture_Icon_Server_Facebook.BackgroundImage = ServerFacebookLink ? 
                                Picture_Icon_Server_Facebook.Icon_Order(SVG_Icon.Facebook, SVG_Color.Base) :
                                Picture_Icon_Server_Facebook.Icon_Order(SVG_Icon.Facebook, SVG_Color.Unknown);
                                LinkLabel_Server_Facebook.Enabled = ServerFacebookLink;
                                LinkLabel_Server_Facebook.Text = ServerFacebookLink ? "Facebook Page" : string.Empty;
                            }
                            catch
                            {

                            }

                            /* Twitter Account Display */
                            try
                            {
                                bool ServerTwitterLink = Uri.TryCreate(Launcher_Value.Launcher_Select_Server_JSON.Server_Social_Twitter, UriKind.Absolute, out Uri? uriResult) &&
                                                         (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                                Picture_Icon_Server_Twitter.BackgroundImage = ServerTwitterLink ?
                                Picture_Icon_Server_Twitter.Icon_Order(SVG_Icon.Twitter, SVG_Color.Base) :
                                Picture_Icon_Server_Twitter.Icon_Order(SVG_Icon.Twitter, SVG_Color.Unknown);
                                LinkLabel_Server_Twitter.Enabled = ServerTwitterLink;
                                LinkLabel_Server_Twitter.Text = ServerTwitterLink ? "Twitter Feed" : string.Empty;
                            }
                            catch
                            {

                            }

                            /* Server Set Speedbug Timer Display */
                            try
                            {
                                ServerSecondsToShutDown =
                                (Launcher_Value.Launcher_Select_Server_JSON.Server_Session_Timer != 0) ? Launcher_Value.Launcher_Select_Server_JSON.Server_Session_Timer : 7200;
                                Label_Server_Force_Restart_Timer.Text = string.Format(Translations.Database("MainScreen_Text_ServerShutDown") +
                                    " " + Time_Conversion.RelativeTime(ServerSecondsToShutDown));
                            }
                            catch
                            {

                            }

                            try
                            {
                                string SceneryStatus = string.Join("", Launcher_Value.Launcher_Select_Server_JSON.Server_Active_Scenery) switch
                                {
                                    "SCENERY_GROUP_NEWYEARS" => "Scenery: New Years",
                                    "SCENERY_GROUP_VALENTINES" => "Scenery: Valentines",
                                    "SCENERY_GROUP_OKTOBERFEST" => "Scenery: Oktoberfest",
                                    "SCENERY_GROUP_HALLOWEEN" => "Scenery: Halloween",
                                    "SCENERY_GROUP_CHRISTMAS" => "Scenery: Christmas",
                                    _ => "Scenery: Normal",
                                };
                                Label_Server_Scenery.Text = SceneryStatus;
                            }
                            catch
                            {

                            }

                            /* Check Selected Server Address for since nfsw currently requires being proxied for https */
                            if (Launcher_Value.Launcher_Select_Server_Data.IPAddress.StartsWith("https"))
                            {
                                if (BuildBeta.Allowed() || BuildDevelopment.Allowed())
                                {
                                    Log.Debug("Server is Https");
                                }
                                /* This is in case it is not listed in GSI at all || is present in GSI and is set to true */
                                if ((!Launcher_Value.Launcher_Select_Server_JSON.Server_Proxy_Forced) || (Launcher_Value.Launcher_Select_Server_JSON.Server_Proxy_Forced != false))
                                {   /* So we can force the Proxy On even if a User has Disabled it */
                                    InformationCache.SelectedServerEnforceProxy = true;
                                    if (BuildBeta.Allowed() || BuildDevelopment.Allowed())
                                    {
                                        Log.Debug("Server is Https: Case 1");
                                    }
                                }
                                /* but still allow that nfsw might get patched to do https raw? */
                                else if (Launcher_Value.Launcher_Select_Server_JSON.Server_Proxy_Forced == false)
                                {   /* In which case, respect the GSI set value */
                                    InformationCache.SelectedServerEnforceProxy = false;
                                    if (BuildBeta.Allowed() || BuildDevelopment.Allowed())
                                    {
                                        Log.Debug("Server is Https: Case 2 (Never Gets Touched Because of First If Statement)");
                                    }
                                }
                            }
                            /* If it's an HTTP Server, check if Proxy is being requested as Enforced On */
                            else if (Launcher_Value.Launcher_Select_Server_JSON.Server_Proxy_Forced != true)
                            {   /* This is set so that it doesn't try to enforce Proxy On if user switches
                                 * to a server that doesn't have enforceLauncherProxy set or true */
                                InformationCache.SelectedServerEnforceProxy = false;
                                if (BuildBeta.Allowed() || BuildDevelopment.Allowed())
                                {
                                    Log.Debug("Server is not Https: Case 3");
                                }
                            }

                            Launcher_Value.Launcher_Server_Crew_Tags = Launcher_Value.Launcher_Select_Server_JSON.Server_Enable_Crew_Tags;

                            if (Launcher_Value.Launcher_Select_Server_JSON.Server_User_Online_Peak != 0)
                            {
                                numPlayers = string.Format("{0} / {1}", Launcher_Value.Launcher_Select_Server_JSON.Server_User_Online, Launcher_Value.Launcher_Select_Server_JSON.Server_User_Online_Peak);
                                numRegistered = string.Format("{0}", Launcher_Value.Launcher_Select_Server_JSON.Server_User_Registered);
                            }
                            else if (Launcher_Value.Launcher_Select_Server_JSON.Server_User_Online_Max != 0)
                            {
                                numPlayers = string.Format("{0} / {1}", Launcher_Value.Launcher_Select_Server_JSON.Server_User_Online, Launcher_Value.Launcher_Select_Server_JSON.Server_User_Online_Max);
                                numRegistered = string.Format("{0}", Launcher_Value.Launcher_Select_Server_JSON.Server_User_Registered);
                            }
                            else if ((Launcher_Value.Launcher_Select_Server_JSON.Server_User_Online_Max == 0) || (Launcher_Value.Launcher_Select_Server_JSON.Server_User_Online_Peak == 0))
                            {
                                numPlayers = string.Format("{0}", Launcher_Value.Launcher_Select_Server_JSON.Server_User_Online);
                                numRegistered = string.Format("{0}", Launcher_Value.Launcher_Select_Server_JSON.Server_User_Registered);
                            }

                            FunctionStatus.AllowRegistration = true;
                        }
                    }

                    if (!GSIErrorFree)
                    {
                        try
                        {
                            Label_Status_Game_Server.Text = "Server Connection:\n - Unstable";
                            Label_Status_Game_Server.ForeColor = Color_Text.S_Warning;
                            Label_Status_Game_Server_Data.Text = "Received Invalid JSON Game Server Info.";
                            Picture_Icon_Server.BackgroundImage = Picture_Icon_Server.Icon_Order(SVG_Icon.Server, SVG_Color.Warning);
                        }
                        catch
                        {
                            /* Sad Noises */
                        }
                    }
                    else
                    {
                        try
                        {
                            Label_Status_Game_Server.Text = "Server Status:\n - Online ( ON )";
                            Label_Status_Game_Server.ForeColor = Color_Text.S_Sucess;
                            Picture_Icon_Server.BackgroundImage = Picture_Icon_Server.Icon_Order(SVG_Icon.Server, SVG_Color.Success);
                            /* Enable Login & Register Button */
                            LoginEnabled = true;
                            Button_Login.ForeColor = Color_Text.L_Five;
                            Button_Login.Enabled = !IsDownloading;
                            Button_Register.Enabled = true;
                            Launcher_Value.Launcher_Select_Server_Category = ((Json_List_Server)ComboBox_Server_List.SelectedItem).Category ?? string.Empty;

                            if (Launcher_Value.Launcher_Select_Server_Category.ToUpper() == "DEV" ||
                            Launcher_Value.Launcher_Select_Server_Category.ToUpper() == "OFFLINE")
                            {
                                /* Disable Social Panel */
                                DisableSocialPanelandClearIt();
                            }
                            else
                            {
                                /* Enable Social Panel  */
                                Panel_Server_Information.Visible = true;
                            }
                        }
                        catch
                        {
                            /* ¯\_(ツ)_/¯ */
                        }
                        finally
                        {
                            Client?.Dispose();
                        }

                        /* For Thread Safety */
                        try
                        {
                            Label_Status_Game_Server_Data.Text = string.Format("Online: {0}\nRegistered: {1}", numPlayers, numRegistered);
                        }
                        catch
                        {

                        }

                        Server_Ping(ServerURI.Host, 5000);

                        ServerEnabled = true;

                        if (Cached_Server_GSI.Equals(Launcher_Value.Launcher_Select_Server_Data))
                        {
                            try
                            {
                                if (!Directory.Exists(Banner_Cache_Folder)) { Directory.CreateDirectory(Banner_Cache_Folder); }

                                if (!string.IsNullOrWhiteSpace(ImageUrl))
                                {

                                    Uri URICall_A = new Uri(ImageUrl);
                                    ServicePointManager.FindServicePoint(URICall_A).ConnectionLeaseTimeout = (int)TimeSpan.FromSeconds(Launcher_Value.Launcher_WebCall_Timeout_Enable ?
                                        Launcher_Value.Launcher_WebCall_Timeout() : 60).TotalMilliseconds;
                                    var Client_A = new WebClient
                                    {
                                        Encoding = Encoding.UTF8,
                                        CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
                                    };
                                    if (!Launcher_Value.Launcher_Alternative_Webcalls())
                                    {
                                        Client_A = new WebClientWithTimeout { Encoding = Encoding.UTF8, CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore) };
                                    }
                                    else
                                    {
                                        Client_A.Headers.Add("user-agent", "SBRW Launcher " +
                                        Application.ProductVersion + " (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)");
                                    }

                                    Client_A.DownloadDataAsync(URICall_A);
                                    Client_A.DownloadProgressChanged += (Object_A, Events_A) =>
                                    {
                                        if (!Cached_Server_GSI.Equals(Launcher_Value.Launcher_Select_Server_Data))
                                        {
                                            Client_A.CancelAsync();
                                            Log.Info("BANNER: Stopping " + ServerListUpdater.ServerName("Ping") + " Server Banner Download");
                                        }
                                        else if (Events_A.TotalBytesToReceive > 2000000)
                                        {
                                            Client_A.CancelAsync();
                                            Log.Warning("BANNER: Unable to Cache " + ServerListUpdater.ServerName("Ping") + " Server Banner! {Over 2MB?}");
                                        }
                                    };

                                    Client_A.DownloadDataCompleted += (Object_A, Events_A) =>
                                    {
                                        if (Events_A.Cancelled)
                                        {
                                            Client_A?.Dispose();
                                        }
                                        else if (Cached_Server_GSI.Equals(Launcher_Value.Launcher_Select_Server_Data))
                                        {
                                            if (Events_A.Error != null)
                                            {
                                                if (Cached_Server_GSI.Equals(Launcher_Value.Launcher_Select_Server_Data))
                                                {
                                                    /* Load cached banner! */
                                                    if (Picture_Server_Banner.Image != (Image_Handler.Grayscale(Banner_Cache_File) ?? Image_Other.Server_Banner))
                                                    {
                                                        Picture_Server_Banner.Image = Image_Handler.Grayscale(Banner_Cache_File) ?? Image_Other.Server_Banner;
                                                    }
                                                }

                                                Client_A?.Dispose();
                                            }
                                            else if (Cached_Server_GSI.Equals(Launcher_Value.Launcher_Select_Server_Data) && Events_A.Result != null)
                                            {
                                                try
                                                {
                                                    try
                                                    {
                                                        if (ServerRawBanner != null)
                                                        {
                                                            ServerRawBanner.Close();
                                                            ServerRawBanner.Dispose();
                                                        }
                                                    }
                                                    catch { }

                                                    ServerRawBanner = new MemoryStream(Events_A.Result)
                                                    {
                                                        Position = 0
                                                    };

                                                    if (Picture_Server_Banner.Image != (Image.FromStream(ServerRawBanner) ?? Image_Other.Server_Banner))
                                                    {
                                                        Picture_Server_Banner.Image = Image.FromStream(ServerRawBanner) ?? Image_Other.Server_Banner;
                                                    }

                                                    if (Strings.GetExtension(ImageUrl) == "gif")
                                                    {
                                                        Image.FromStream(ServerRawBanner).Save(Banner_Cache_File);
                                                    }
                                                    else
                                                    {
                                                        File.WriteAllBytes(Banner_Cache_File, ServerRawBanner.ToArray());
                                                    }
                                                }
                                                catch (Exception Error)
                                                {
                                                    LogToFileAddons.OpenLog("Server Banner", string.Empty, Error, string.Empty, true);
                                                    Picture_Server_Banner.BackColor = Color_Winform_Other.Server_Banner_BackColor;
                                                }
                                                finally
                                                {
                                                    Client_A?.Dispose();
                                                }
                                            }
                                        }
                                    };
                                }
                                else if (File.Exists(Banner_Cache_File) && !(Application.OpenForms[this.Name].IsDisposed || Application.OpenForms[this.Name].Disposing))
                                {
                                    /* Load cached banner! */
                                    if (Picture_Server_Banner.Image != (Image_Handler.Grayscale(Banner_Cache_File) ?? Image_Other.Server_Banner))
                                    {
                                        Picture_Server_Banner.Image = Image_Handler.Grayscale(Banner_Cache_File) ?? Image_Other.Server_Banner;
                                    }
                                }
                                else if (!Application.OpenForms[this.Name].IsDisposed)
                                {
                                    Picture_Server_Banner.BackColor = Color_Winform_Other.Server_Banner_BackColor;
                                }
                            }
                            catch (Exception Error)
                            {
                                LogToFileAddons.OpenLog("BANNER Cache", string.Empty, Error, string.Empty, true);
                            }
                        }
                    }
                }
                else
                {
                    /* Just ingore this. */
                }
            };
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_Timer_Tick(object sender, EventArgs e)
        {
            if (!this.Disposing || !this.IsDisposed)
            {
                switch (UI_MODE)
                {
                    /* Download Failed */
                    case -1:
                        if (UI_MODE != 0)
                        {
                            UI_MODE = 0;
                        }

                        if (Picture_Bar_Outline.Tag == default || !Picture_Bar_Outline.Tag.Equals(3))
                        {
                            Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Error_Outline;
                            Picture_Bar_Outline.Tag = Image_ProgressBar.Error_Outline.Tag;
                        }

                        ProgressBar.Value = 100;
                        if (ProgressBar.ID != 3)
                        {
                            ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Error_Top;
                            ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Error_Bottom;
                            ProgressBar.ID = 3;
                        }

                        break;
                    /* Pack Downloader (In-Progress) */
                    case 1:
                        if (Pack_SBRW_Downloader != null)
                        {
                            Download_Information? Cached_Status = Pack_SBRW_Downloader.Download_Status();
                            if ((Cached_Status != null) && !Pack_SBRW_Downloader.Cancel)
                            {
                                if (Cached_Status.Download_Attempts > 0)
                                {
                                    Label_Download_Information_Support.Text = string.Format("Download Attempt {0}: Core Game Files Package",
                                        Cached_Status.Download_Attempts).ToUpper();
                                }

                                Label_Download_Information.Text = (Time_Conversion.FormatFileSize(Cached_Status.File_Size_Current) + " of " + Time_Conversion.FormatFileSize(Cached_Status.File_Size_Total) +
                                    " (" + Cached_Status.Download_Percentage + "%) - " +
                                    Time_Conversion.EstimateFinishTime(Cached_Status.File_Size_Current, Cached_Status.File_Size_Total, Cached_Status.Start_Time)).ToUpperInvariant();

                                ProgressBar.Value = Cached_Status.Download_Percentage.Clamp(0, 100);

                                if (ProgressBar.ID != 0)
                                {
                                    ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Loading_Top;
                                    ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Loading_Bottom;
                                    ProgressBar.ID = 0;
                                }

                                if (Picture_Bar_Outline.Tag == default || !Picture_Bar_Outline.Tag.Equals(0))
                                {
                                    Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Checking_Outline;
                                    Picture_Bar_Outline.Tag = Image_ProgressBar.Checking_Outline.Tag;
                                }

                                Presence_Launcher.Status(2, string.Format("Downloaded {0}% of the Game!", Cached_Status.Download_Percentage));
                            }
                        }
                        break;
                    /* Pack Downloader (Progress Complete) */
                    case 2:
                        ProgressBar.Value = 0;

                        if (ProgressBar.ID != 0)
                        {
                            ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Loading_Top;
                            ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Loading_Bottom;
                            ProgressBar.ID = 0;
                        }

                        if (Picture_Bar_Outline.Tag == default || !Picture_Bar_Outline.Tag.Equals(0))
                        {
                            Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Checking_Outline;
                            Picture_Bar_Outline.Tag = Image_ProgressBar.Checking_Outline.Tag;
                        }

                        Label_Download_Information.Text = "Checking Package Integrity".ToUpperInvariant();
                        Label_Download_Information_Support.Text = "Downloaded: SBRW Game Files Package".ToUpperInvariant();
                        break;
                    /* Generic Loading */
                    case 3:
                        if (UI_MODE != 0)
                        {
                            UI_MODE = 0;
                        }

                        Label_Download_Information.Text = "Loading".ToUpperInvariant();
                        ProgressBar.Value = 0;

                        if (ProgressBar.ID != 0)
                        {
                            ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Loading_Top;
                            ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Loading_Bottom;
                            ProgressBar.ID = 0;
                        }

                        if (Picture_Bar_Outline.Tag == default || !Picture_Bar_Outline.Tag.Equals(0))
                        {
                            Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Checking_Outline;
                            Picture_Bar_Outline.Tag = Image_ProgressBar.Checking_Outline.Tag;
                        }
                        break;
                    /* Unpack Archive */
                    case 4:
                        if (Pack_SBRW_Unpacker != null)
                        {
                            Extract_Information? Cached_Status = Pack_SBRW_Unpacker.Extract_Status();
                            if ((Cached_Status != null) && !Pack_SBRW_Unpacker.Cancel)
                            {
                                ProgressBar.Value = Cached_Status.Extract_Percentage.Clamp(0, 100);

                                if (!string.IsNullOrWhiteSpace(Cached_Status.File_Current_Name))
                                {
#pragma warning disable CS8602 // Dereference of a possibly null reference. (.NET 6)
                                    Label_Download_Information.Text = ("Unpacking " + Cached_Status.File_Current_Name.Replace(Pack_SBRW_Unpacker.File_Extension_Replacement, string.Empty)).ToUpperInvariant();
#pragma warning restore CS8602 // Dereference of a possibly null reference. (.NET 6)
                                }

                                Label_Download_Information_Support.Text = Cached_Status.Extract_Percentage + "% [" + Cached_Status.File_Current + " / " + Cached_Status.File_Total + "]".ToUpperInvariant();

                                Presence_Launcher.Status(1, string.Format("Unpacking Game: {0}%", Cached_Status.Extract_Percentage));

                                if (ProgressBar.ID != 0)
                                {
                                    ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Loading_Top;
                                    ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Loading_Bottom;
                                    ProgressBar.ID = 0;
                                }

                                if (Picture_Bar_Outline.Tag == default || !Picture_Bar_Outline.Tag.Equals(0))
                                {
                                    Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Checking_Outline;
                                    Picture_Bar_Outline.Tag = Image_ProgressBar.Checking_Outline.Tag;
                                }
                            }
                        }
                        break;
                    /* Generic Complete */
                    case 5:
                        if (UI_MODE != 0)
                        {
                            UI_MODE = 0;
                        }

                        ProgressBar.Value = 100;

                        if (ProgressBar.ID != 1)
                        {
                            ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Sucess_Top;
                            ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Sucess_Bottom;
                            ProgressBar.ID = 1;
                        }

                        if (Picture_Bar_Outline.Tag == default || !Picture_Bar_Outline.Tag.Equals(1))
                        {
                            Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Complete_Outline;
                            Picture_Bar_Outline.Tag = Image_ProgressBar.Complete_Outline.Tag;
                        }
                        break;
                    /* Generic Warning */
                    case 6:
                        if (UI_MODE != 0)
                        {
                            UI_MODE = 0;
                        }

                        ProgressBar.Value = 100;

                        if (ProgressBar.ID != 2)
                        {
                            ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Warning_Top;
                            ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Warning_Bottom;
                            ProgressBar.ID = 2;
                        }

                        if (Picture_Bar_Outline.Tag == default || !Picture_Bar_Outline.Tag.Equals(2))
                        {
                            Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Warning_Outline;
                            Picture_Bar_Outline.Tag = Image_ProgressBar.Warning_Outline.Tag;
                        }
                        break;
                    /* ModNet Progress (Downloading) */
                    case 7:
                        if (ModNet_Download_Status != null)
                        {
                            if (ModNet_Download_Status.Download_Complete)
                            {
                                if (UI_MODE != 8)
                                {
                                    UI_MODE = 8;
                                }

                                ModNet_Download_Status = null;
                            }
                            else
                            {
                                Label_Download_Information_Support.Text = ("Downloading - [" + CurrentModFileCount + " / " + TotalModFileCount + "] :").ToUpperInvariant();
                                Label_Download_Information.Text = (" Server Mods: " + ModNetFileNameInUse + " - " + Time_Conversion.FormatFileSize(ModNet_Download_Status.File_Size_Current) + " of " + Time_Conversion.FormatFileSize(ModNet_Download_Status.File_Size_Total)).ToUpperInvariant();

                                ProgressBar.Value = ModNet_Download_Status.Download_Percentage.Clamp(0, 100);

                                if (ProgressBar.ID != 0)
                                {
                                    ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Loading_Top;
                                    ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Loading_Bottom;
                                    ProgressBar.ID = 0;
                                }

                                if (Picture_Bar_Outline.Tag == default || !Picture_Bar_Outline.Tag.Equals(0))
                                {
                                    Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Checking_Outline;
                                    Picture_Bar_Outline.Tag = Image_ProgressBar.Checking_Outline.Tag;
                                }

                                if (ModNet_Download_Status.Download_Percentage >= 100)
                                {
                                    Presence_Launcher.Status(28, string.Empty);
                                }
                                else
                                {
                                    Presence_Launcher.Status(2, string.Format("Downloaded {0}% of Server Game Mods!", ModNet_Download_Status.Download_Percentage));
                                }
                            }
                        }
                        break;
                    /* Loading Game */
                    case 8:
                        if (UI_MODE != 0)
                        {
                            UI_MODE = 0;
                        }

                        Presence_Launcher.Status(28, string.Empty);

                        if (Builtinserver)
                        {
                            Label_Download_Information.Text = "Soapbox server launched. Waiting for queries.".ToUpperInvariant();
                        }
                        else
                        {
                            Display_Color_Icons();
                            Label_Download_Information.Text = "Loading game. Launcher will minimize once Game has Loaded".ToUpperInvariant();
                            Label_Download_Information_Support.Text = string.Empty;
                            Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Is_Email.Mask(Save_Account.Live_Data.User_Raw_Email)).ToUpper();
#if NETFRAMEWORK
                            ContextMenu = new ContextMenu();
                            ContextMenu.MenuItems.Add(new MenuItem("Now Loading!!!", (b, n) =>
                            {
#if NETFRAMEWORK
                                Process.Start("https://www.youtube.com/watch?v=kq3X78ngFAY");
#else
                                Process.Start("explorer.exe", "https://www.youtube.com/watch?v=kq3X78ngFAY");
#endif
                            }));

                            ContextMenu.MenuItems.Add("-");
                            if (Screen_Parent.Screen_Instance != null)
                            {
                                ContextMenu.MenuItems.Add(new MenuItem("Close Game and Launcher", Screen_Parent.Screen_Instance.Button_Close_Click));
                            }
#endif
#if NETFRAMEWORK
                            NotifyIcon_Notification.ContextMenu = ContextMenu;
#endif
                        }
                        break;
                    /* Checking Folder Size (Pack Downloader) */
                    case 9:
                        if (UI_MODE != 0)
                        {
                            UI_MODE = 0;
                        }

                        Label_Download_Information.Text = "Calculating Game Folder Size".ToUpperInvariant();
                        ProgressBar.Value = 0;

                        if (ProgressBar.ID != 0)
                        {
                            ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Loading_Top;
                            ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Loading_Bottom;
                            ProgressBar.ID = 0;
                        }

                        if (Picture_Bar_Outline.Tag == default || !Picture_Bar_Outline.Tag.Equals(0))
                        {
                            Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Checking_Outline;
                            Picture_Bar_Outline.Tag = Image_ProgressBar.Checking_Outline.Tag;
                        }
                        break;
                    case 10:
                        if (UI_MODE != 0)
                        {
                            UI_MODE = 0;
                        }

                        Label_Download_Information_Support.Text = "Checking Game Files Package Hash".ToUpper();
                        ProgressBar.Value = 100;

                        if (ProgressBar.ID != 4)
                        {
                            ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Unknown_Top;
                            ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Unknown_Bottom;
                            ProgressBar.ID = 4;
                        }

                        if (Picture_Bar_Outline.Tag == default || !Picture_Bar_Outline.Tag.Equals(4))
                        {
                            Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Preload_Outline;
                            Picture_Bar_Outline.Tag = Image_ProgressBar.Preload_Outline.Tag;
                        }
                        break;
                    case 11:
                        if (UI_MODE != 0)
                        {
                            UI_MODE = 0;
                        }

                        Label_Download_Information_Support.Text = "Downloading: Core Game Files Package".ToUpper();
                        ProgressBar.Value = 0;

                        if (ProgressBar.ID != 0)
                        {
                            ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Loading_Top;
                            ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Loading_Bottom;
                            ProgressBar.ID = 0;
                        }

                        if (Picture_Bar_Outline.Tag == default || !Picture_Bar_Outline.Tag.Equals(0))
                        {
                            Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Checking_Outline;
                            Picture_Bar_Outline.Tag = Image_ProgressBar.Checking_Outline.Tag;
                        }
                        break;
                    case 12:
                        if (UI_MODE != 0)
                        {
                            UI_MODE = 0;
                        }

                        Label_Download_Information.Text = "Launcher: Checking NFSW EXE File Hash".ToUpperInvariant();
                        Label_Download_Information_Support.Text = string.Empty;
                        Label_Information_Window.Text = string.Format(LoginWelcomeTime + "\n{0}", Is_Email.Mask(Save_Account.Live_Data.User_Raw_Email)).ToUpper();
                        break;
                    case 13:
                        if (UI_MODE != 0)
                        {
                            UI_MODE = 0;
                        }
                        if (!BuildDevelopment.Allowed())
                        {
                            if ((!IsDisposed || !Disposing))
                            {
                                Button_Close.Visible = false;

                                if (Screen_Parent.Screen_Instance != default)
                                {
                                    Screen_Parent.Screen_Instance.WindowState = FormWindowState.Minimized;
                                    Screen_Parent.Screen_Instance.ShowInTaskbar = false;
                                }
                            }
                        }
                        break;
                    case 14:
                        if (UI_MODE != 0)
                        {
                            UI_MODE = 0;
                        }

                        if (!BuildDevelopment.Allowed())
                        {
                            if (!IsDisposed || !Disposing)
                            {
                                Button_Close.Visible = Button_Logout.Visible = EnablePlayButton(true);

                                if (Screen_Parent.Screen_Instance != default)
                                {
                                    Screen_Parent.Screen_Instance.WindowState = FormWindowState.Normal;
                                    Screen_Parent.Screen_Instance.ShowInTaskbar = true;
                                }
                            }
                        }
                        
                        break;
                    /* LZMA Downloader Progress */
                    case 15:
                        if (LZMA_Downloader != null)
                        {
                            Download_Information_LZMA? Cached_Status = LZMA_Downloader.Download_Status();
                            if ((Cached_Status != null) && LZMA_Downloader.Downloading)
                            {
                                Label_Download_Information.Text = (Time_Conversion.FormatFileSize(Cached_Status.File_Size_Current) + " of " + Time_Conversion.FormatFileSize(Cached_Status.File_Size_Total) +
                                    " (" + Cached_Status.Download_Percentage + "%) - " +
                                    Time_Conversion.EstimateFinishTime(Cached_Status.File_Size_Current, Cached_Status.File_Size_Total, Cached_Status.Start_Time)).ToUpperInvariant();

                                ProgressBar.Value = Cached_Status.Download_Percentage.Clamp(0, 100);

                                if (ProgressBar.ID != 0)
                                {
                                    ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Loading_Top;
                                    ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Loading_Bottom;
                                    ProgressBar.ID = 0;
                                }

                                if (Picture_Bar_Outline.Tag == default || !Picture_Bar_Outline.Tag.Equals(0))
                                {
                                    Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Checking_Outline;
                                    Picture_Bar_Outline.Tag = Image_ProgressBar.Checking_Outline.Tag;
                                }

                                Presence_Launcher.Status(2, string.Format("Downloaded {0}% of the Game!", Cached_Status.Download_Percentage));
                            }
                        }
                        break;
                }

                GarbageCollections.Cleanup();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ServerList_Menu_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                string serverListText = string.Empty;
                /* 0 = Offline | 1 = Online | 2 = Checking | 3 = GSI Error */
                int onlineStatus = 2;

                if (sender is ComboBox cb)
                {
                    if (e.Index != -1 && cb.Items != null)
                    {
                        if (cb.Items[e.Index] is Json_List_Server si)
                        {
                            serverListText = si.Name;
                            onlineStatus = InformationCache.ServerStatusBook.ContainsKey(si.ID) ? InformationCache.ServerStatusBook[si.ID] : 2;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(serverListText) && sender != null)
                {
                    Font font = ((ComboBox)sender).Font;
                    Brush backgroundColor;
                    Brush textColor;

                    if (serverListText.StartsWith("<GROUP>"))
                    {
                        font = new Font(font, FontStyle.Bold);
                        e.Graphics.FillRectangle(new SolidBrush(Color_Winform_Other.DropMenu_White), e.Bounds);
                        e.Graphics.DrawString(serverListText.Replace("<GROUP>", string.Empty), font, new SolidBrush(Color_Winform_Other.DropMenu_Black), e.Bounds);
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
                            backgroundColor = onlineStatus switch
                            {
                                1 => new SolidBrush(Color_Winform_Other.DropMenu_Ping_Success),/* ONLINE */
                                2 => new SolidBrush(Color_Winform_Other.DropMenu_Ping_Checking),/* CHECKING */
                                3 => new SolidBrush(Color_Winform_Other.DropMenu_Ping_Warning),/* GSI ERROR */
                                _ => new SolidBrush(Color_Winform_Other.DropMenu_Ping_Error),/* OFFLINE */
                            };
                            textColor = new SolidBrush(Color_Winform_Other.DropMenu_Black);
                        }

                        e.Graphics.FillRectangle(backgroundColor, e.Bounds);
                        e.Graphics.DrawString("    " + serverListText, font, textColor, e.Bounds);
                    }
                }
            }
            catch { }
        }
        /// <summary>
        /// Sets the Category for the Accounts Drop Down Menu with its set of Colors
        /// </summary>
        /// <remarks>Dropdown Menu Visual</remarks>
        private void ComboBox_Account_List_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                string Account_Nickname = string.Empty;

                if (sender is ComboBox cb)
                {
                    if (e.Index != -1 && cb.Items != null)
                    {
                        if (cb.Items[e.Index] is Json_List_Account si)
                        {
                            Account_Nickname = si.Nickname;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(Account_Nickname) && sender != null)
                {
                    Font font = ((ComboBox)sender).Font;
                    Brush backgroundColor;
                    Brush textColor;

                    font = new Font(font, FontStyle.Bold);
                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected && e.State != DrawItemState.ComboBoxEdit)
                    {
                        backgroundColor = SystemBrushes.Highlight;
                        textColor = SystemBrushes.HighlightText;
                    }
                    else
                    {
                        backgroundColor = new SolidBrush(Color_Winform_Other.DropMenu_Background_ForeColor);
                        textColor = new SolidBrush(Color_Winform_Other.DropMenu_Text_ForeColor);
                    }

                    e.Graphics.FillRectangle(backgroundColor, e.Bounds);
                    e.Graphics.DrawString("    " + Account_Nickname, font, textColor, e.Bounds);
                }
            }
            catch { }
        }
        /// <summary>
        /// Theme and Functions
        /// </summary>
        private void Set_Visuals()
        {
            /*******************************/
            /* Set Window Name              /
            /*******************************/

            Icon = FormsIcon.Retrive_Icon();
            Text = "SBRW Launcher: " + Application.ProductVersion;

            /*******************************/
            /* Set Font                     /
            /*******************************/

#if !(RELEASE_UNIX || DEBUG_UNIX)
            float MainFontSize = 9f * 96f / CreateGraphics().DpiY;
            float SecondaryFontSize = 8f * 96f / CreateGraphics().DpiY;
            float ThirdFontSize = 10f * 96f / CreateGraphics().DpiY;
            float FourthFontSize = 14f * 96f / CreateGraphics().DpiY;
#else
            float MainFontSize = 9f;
            float SecondaryFontSize = 8f;
            float ThirdFontSize = 10f;
            float FourthFontSize = 14f;
#endif
            Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Regular);

            /* Front Screen */
            Label_Insider_Build_Number.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Button_Select_Server.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_Debug_Language.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Regular);
            ComboBox_Server_List.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Button_Custom_Server.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            Panel_Server_Information.Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Regular);
            Label_Information_Window.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Status_Launcher.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Status_Launcher_Version.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_Status_Game_Server.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Status_Game_Server_Data.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_Status_API.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Status_API_Details.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            ProgressBar.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            /* Social Panel */
            Panel_Server_Information.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            LinkLabel_Server_Home.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            LinkLabel_Server_Discord.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            LinkLabel_Server_Facebook.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            LinkLabel_Server_Twitter.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Label_Server_Scenery.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Server_Force_Restart_Timer.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            /* Log In Panel */
            Input_Email.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Input_Password.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            CheckBox_Remember_Us.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            LinkLabel_Forgot_Password.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            Button_Login.Font = new Font(FormsFont.Primary_Bold(), ThirdFontSize, FontStyle.Bold);
            Button_Register.Font = new Font(FormsFont.Primary_Bold(), ThirdFontSize, FontStyle.Bold);
            Label_Client_Ping.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Button_Logout.Font = new Font(FormsFont.Primary_Bold(), ThirdFontSize, FontStyle.Bold);
            Button_Play_OR_Update.Font = new Font(FormsFont.Primary_Bold(), FourthFontSize, FontStyle.Bold);
            Label_Download_Information.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Download_Information_Support.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            /* Console */
            Button_Console_Submit.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            Input_Console.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);

            /********************************/
            /* Set Theme Colors & Images     /
            /********************************/

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);

            /* Set Background with Transparent Key */
            BackgroundImage = Image_Background.Login;
            TransparencyKey = Color_Screen.BG_Main;

            Picture_Logo.BackgroundImage = Image_Other.Logo;
            Button_Settings.BackgroundImage = (Save_Settings.Live_Data.Game_Integrity == "Bad") ?
                Button_Settings.Icon_Order(SVG_Icon.Gear, SVG_Color.Warning) :
                Button_Settings.Icon_Order(SVG_Icon.Gear);
            Button_Close.BackgroundImage = Button_Close.Icon_Order(SVG_Icon.Cross);
            Button_Security_Center.BackgroundImage = Button_Security_Center.SecurityCenterIcon(1);

            Picture_Bar_Outline.BackgroundImage = Image_ProgressBar.Preload_Outline;
            ProgressBar.BackColor = Color_Winform_Other.ProgressBar_Unknown_Top;
            ProgressBar.ForeColor = Color_Winform_Other.ProgressBar_Unknown_Bottom;
            ProgressBar.OuterRectangleBackColor = Color_Winform_Other.ProgressBar_Background;

            Label_Download_Information.ForeColor = Color_Text.L_Five;
            Label_Download_Information_Support.ForeColor = Color_Text.L_Five;

            Picture_Input_Email.Image = Picture_Input_Email.Icon_Order(SVG_Icon.Input_Box_Email, SVG_Color.Base);
            Picture_Input_Password.Image = Picture_Input_Password.Icon_Order(SVG_Icon.Input_Box_Password, SVG_Color.Base);
            Picture_Information_Window.Image = Image_Other.Information_Window;

            Label_Information_Window.ForeColor = Color_Text.L_Five;

            Label_Status_Launcher_Version.ForeColor = Color_Text.L_Five;
            Label_Status_Game_Server.ForeColor = Color_Text.L_Five;
            Label_Status_Game_Server_Data.ForeColor = Color_Text.L_Five;
            Label_Status_API_Details.ForeColor = Color_Text.L_Five;

            Button_Login.ForeColor = Color_Text.L_Five;
            Button_Login.BackgroundImage = Image_Button.Grey;

            Button_Register.ForeColor = Color_Text.L_Seven;
            Button_Register.BackgroundImage = Image_Button.Green;

            CheckBox_Remember_Us.ForeColor = Color_Text.L_Five;

            LinkLabel_Forgot_Password.ActiveLinkColor = Color_Winform_Other.Link_Active;
            LinkLabel_Forgot_Password.LinkColor = Color_Winform_Other.Link;

            Input_Email.BackColor = Color_Winform_Other.Input;
            Input_Email.ForeColor = Color_Text.L_Five;
            Input_Password.BackColor = Color_Winform_Other.Input;
            Input_Password.ForeColor = Color_Text.L_Five;

            Panel_Server_Information.BackgroundImage = Image_Background.Server_Information;

            Label_Server_Force_Restart_Timer.ForeColor = Color_Text.L_Two;
            Label_Server_Scenery.ForeColor = Color_Text.L_Two;

            LinkLabel_Server_Twitter.LinkColor = Color_Text.L_Two;
            LinkLabel_Server_Facebook.LinkColor = Color_Text.L_Two;
            LinkLabel_Server_Discord.LinkColor = Color_Text.L_Two;
            LinkLabel_Server_Home.LinkColor = Color_Text.L_Two;

            LinkLabel_Server_Twitter.ActiveLinkColor = Color_Text.L_Five;
            LinkLabel_Server_Facebook.ActiveLinkColor = Color_Text.L_Five;
            LinkLabel_Server_Discord.ActiveLinkColor = Color_Text.L_Five;
            LinkLabel_Server_Home.ActiveLinkColor = Color_Text.L_Five;

            Label_Insider_Build_Number.ForeColor = Color_Text.L_Five;

            Input_Console.BackColor = Color_Winform_Other.Input;
            Input_Console.ForeColor = Color_Text.L_Five;

            Button_Console_Submit.ForeColor = Color_Winform_Buttons.Green_Fore_Color;
            Button_Console_Submit.BackColor = Color_Winform_Buttons.Green_Back_Color;
            Button_Console_Submit.FlatAppearance.BorderColor = Color_Winform_Buttons.Green_Border_Color;
            Button_Console_Submit.FlatAppearance.MouseOverBackColor = Color_Winform_Buttons.Green_Mouse_Over_Back_Color;

            /********************************/
            /* Events                        /
            /********************************/

            Label_Status_Launcher.Click += new EventHandler(Update_Popup_Click);
            Label_Status_Launcher_Version.Click += new EventHandler(Update_Popup_Click);

            LinkLabel_Server_Twitter.LinkClicked += new LinkLabelLinkClickedEventHandler(FunctionEvents.TwitterAccountLink_LinkClicked);
            LinkLabel_Server_Facebook.LinkClicked += new LinkLabelLinkClickedEventHandler(FunctionEvents.FacebookGroupLink_LinkClicked);
            LinkLabel_Server_Discord.LinkClicked += new LinkLabelLinkClickedEventHandler(FunctionEvents.DiscordInviteLink_LinkClicked);
            LinkLabel_Server_Home.LinkClicked += new LinkLabelLinkClickedEventHandler(FunctionEvents.HomePageLink_LinkClicked);

            Button_Select_Server.Click += new EventHandler(FunctionEvents.SelectServerBtn_Click);

            Button_Close.MouseEnter += new EventHandler(ButtonClose_MouseEnter);
            Button_Close.MouseLeave += new EventHandler(ButtonClose_MouseLeaveANDMouseUp);
            Button_Close.MouseUp += new MouseEventHandler(ButtonClose_MouseLeaveANDMouseUp);
            Button_Close.MouseDown += new MouseEventHandler(ButtonClose_MouseDown);
            if (Screen_Parent.Screen_Instance != null)
            {
                Button_Close.Click += new EventHandler(Screen_Parent.Screen_Instance.Button_Close_Click);
            }

            Button_Settings.MouseEnter += new EventHandler(ButtonSettings_MouseEnter);
            Button_Settings.MouseLeave += new EventHandler(ButtonSettings_MouseLeaveANDMouseUp);
            Button_Settings.MouseUp += new MouseEventHandler(ButtonSettings_MouseLeaveANDMouseUp);
            Button_Settings.MouseDown += new MouseEventHandler(ButtonSettings_MouseDown);
            Button_Settings.Click += new EventHandler(SettingsButton_Click);

            Button_Security_Center.MouseEnter += new EventHandler(ButtonSecurityCenter_MouseEnter);
            Button_Security_Center.MouseLeave += new EventHandler(ButtonSecurityCenter_MouseLeaveANDMouseUp);
            Button_Security_Center.MouseUp += new MouseEventHandler(ButtonSecurityCenter_MouseLeaveANDMouseUp);
            Button_Security_Center.MouseDown += new MouseEventHandler(ButtonSecurityCenter_MouseDown);
            Button_Security_Center.Click += new EventHandler(ButtonSecurityCenter_Click);

            Button_Login.MouseEnter += new EventHandler(LoginButton_MouseEnter);
            Button_Login.MouseLeave += new EventHandler(LoginButton_MouseLeave);
            Button_Login.MouseUp += new MouseEventHandler(LoginButton_MouseUp);
            Button_Login.MouseDown += new MouseEventHandler(LoginButton_MouseDown);
            Button_Login.Click += new EventHandler(LoginButton_Click);

            Button_Logout.MouseEnter += new EventHandler(Graybutton_hover_MouseEnter);
            Button_Logout.MouseLeave += new EventHandler(Graybutton_MouseLeave);
            Button_Logout.MouseUp += new MouseEventHandler(Graybutton_hover_MouseUp);
            Button_Logout.MouseDown += new MouseEventHandler(Graybutton_click_MouseDown);
            Button_Logout.Click += new EventHandler(LogoutButton_Click);

            Button_Custom_Server.Click += new EventHandler(FunctionEvents.AddServer_Click);
            Button_Account_Manager.Click += new EventHandler(Button_Account_Manager_Click);

            Input_Email.KeyUp += new KeyEventHandler(Loginbuttonenabler);
            Input_Email.KeyDown += new KeyEventHandler(LoginEnter);
            Input_Password.KeyUp += new KeyEventHandler(Loginbuttonenabler);
            Input_Password.KeyDown += new KeyEventHandler(LoginEnter);

            ComboBox_Server_List.SelectedIndexChanged += new EventHandler(ComboBox_Server_List_SelectedIndexChanged);
            ComboBox_Server_List.DrawItem += new DrawItemEventHandler(ServerList_Menu_DrawItem);
            ComboBox_Server_List.MouseWheel += new MouseEventHandler(ComboBox_MouseWheel);

            //TODO
            //ComboBox_Accounts.SelectedIndexChanged += new EventHandler(ComboBox_Server_List_SelectedIndexChanged);
            ComboBox_Accounts.DrawItem += new DrawItemEventHandler(ComboBox_Account_List_DrawItem);
            ComboBox_Accounts.MouseWheel += new MouseEventHandler(ComboBox_MouseWheel);

            LinkLabel_Forgot_Password.LinkClicked += new LinkLabelLinkClickedEventHandler(FunctionEvents.ForgotPassword_LinkClicked);

            if (Screen_Parent.Screen_Instance != null)
            {
                MouseMove += new MouseEventHandler(Screen_Parent.Screen_Instance.Move_Window_Mouse_Move);
                MouseUp += new MouseEventHandler(Screen_Parent.Screen_Instance.Move_Window_Mouse_Up);
                MouseDown += new MouseEventHandler(Screen_Parent.Screen_Instance.Move_Window_Mouse_Down);

                Picture_Logo.MouseMove += new MouseEventHandler(Screen_Parent.Screen_Instance.Move_Window_Mouse_Move);
                Picture_Logo.MouseUp += new MouseEventHandler(Screen_Parent.Screen_Instance.Move_Window_Mouse_Up);
                Picture_Logo.MouseDown += new MouseEventHandler(Screen_Parent.Screen_Instance.Move_Window_Mouse_Down);

                Label_Debug_Language.MouseMove += new MouseEventHandler(Screen_Parent.Screen_Instance.Move_Window_Mouse_Move);
                Label_Debug_Language.MouseUp += new MouseEventHandler(Screen_Parent.Screen_Instance.Move_Window_Mouse_Up);
                Label_Debug_Language.MouseDown += new MouseEventHandler(Screen_Parent.Screen_Instance.Move_Window_Mouse_Down);
            }

            Button_Play_OR_Update.MouseEnter += new EventHandler(PlayButton_MouseEnter);
            Button_Play_OR_Update.MouseLeave += new EventHandler(PlayButton_MouseLeave);
            Button_Play_OR_Update.MouseUp += new MouseEventHandler(PlayButton_MouseUp);
            Button_Play_OR_Update.MouseDown += new MouseEventHandler(PlayButton_MouseDown);
            Button_Play_OR_Update.Click += new EventHandler(PlayButton_Click);

            Button_Register.MouseEnter += new EventHandler(Greenbutton_hover_MouseEnter);
            Button_Register.MouseLeave += new EventHandler(Greenbutton_MouseLeave);
            Button_Register.MouseUp += new MouseEventHandler(Greenbutton_hover_MouseUp);
            Button_Register.MouseDown += new MouseEventHandler(Greenbutton_click_MouseDown);
            Button_Register.Click += new EventHandler(Button_Register_Click);

            Load += new EventHandler(MainScreen_Load);

            Input_Console.KeyDown += new KeyEventHandler(Console_Quick_Send);
            Button_Console_Submit.Click += new EventHandler(Console_Enter);

            KeyPreview = true;

            /********************************/
            /* Enable/Disable Visuals        /
            /********************************/

            Button_Select_Server.Visible = BuildDevelopment.Allowed();

            /********************************/
            /* Set Hardcoded Text            /
            /********************************/

            Label_Client_Ping.Text = string.Empty;

            /********************************/
            /* Functions                     /
            /********************************/

            Closing += (x, y) =>
            {
                Screen_Instance = null;
            };

            Shown += async (x, y) =>
            {
                Game_Folder_Checks();
                await Task.Run(() =>
                {
                    Presence_Launcher.Update();

                    if (ServerListUpdater.NoCategoryList != null && ServerListUpdater.NoCategoryList.Any())
                    {
                        foreach (Json_List_Server Servers in ServerListUpdater.NoCategoryList)
                        {
                            if (Nfswstarted != null || Screen_Parent.Screen_Instance == null || Screen_Instance == null)
                            {
                                break;
                            }
                            else
                            {
                                try
                                {
                                    while (StillCheckingLastServer) { }
                                    Uri URLCall = new Uri(Servers.IPAddress + "/GetServerInformation");
                                    ServicePointManager.FindServicePoint(URLCall).ConnectionLeaseTimeout = (int)TimeSpan.FromSeconds(Launcher_Value.Launcher_WebCall_Timeout_Enable ?
                                    Launcher_Value.Launcher_WebCall_Timeout() : 60).TotalMilliseconds;
                                    var Client = new WebClient
                                    {
                                        Encoding = Encoding.UTF8,
                                        CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
                                    };
                                    if (!Launcher_Value.Launcher_Alternative_Webcalls())
                                    {
                                        Client = new WebClientWithTimeout
                                        {
                                            Encoding = Encoding.UTF8,
                                            CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
                                        };
                                    }
                                    else
                                    {
                                        Client.Headers.Add("user-agent", "SBRW Launcher " +
                                        Application.ProductVersion + " (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)");
                                    }

                                    try
                                    {
                                        JsonGSI = Client.DownloadString(URLCall);
                                        StillCheckingLastServer = true;
                                        bool GSIErrorFree = true;

                                        if (!JsonGSI.Valid_Json())
                                        {
                                            GSIErrorFree = false;
                                            if (BuildBeta.Allowed() || BuildDevelopment.Allowed())
                                            {
                                                Log.Error("Pinging GSI (Received): " + JsonGSI);
                                            }
                                        }

                                        if (!InformationCache.ServerStatusBook.ContainsKey(Servers.ID))
                                        {
                                            InformationCache.ServerStatusBook.Add(Servers.ID, (!GSIErrorFree) ? 3 : 1);
                                        }
                                    }
                                    catch (Exception Error)
                                    {
                                        LogToFileAddons.OpenLog("Pinging GSI [DownloadString]", string.Empty, Error, string.Empty, true);

                                        if (!InformationCache.ServerStatusBook.ContainsKey(Servers.ID))
                                        {
                                            InformationCache.ServerStatusBook.Add(Servers.ID, 0);
                                        }
                                    }
                                    finally
                                    {
                                        StillCheckingLastServer = false;

                                        Client?.Dispose();
                                    }
                                }
                                catch (Exception Error)
                                {
                                    LogToFileAddons.OpenLog("Pinging GSI [WebClient]", string.Empty, Error, string.Empty, true);
                                }
                                finally
                                {
                                    if (!string.IsNullOrWhiteSpace(JsonGSI))
                                    {
                                        JsonGSI = string.Empty;
                                    }
                                }
                            }
                        }
                    }
                });
            };
        }
    }
}
