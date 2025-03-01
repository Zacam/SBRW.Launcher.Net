﻿using SBRW.Launcher.Core.Theme;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.Support;
using SBRW.Launcher.RunTime.LauncherCore.Visuals;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SBRW.Launcher.App.UI_Forms.Parent_Screen
{
    /// <summary>
    /// 
    /// </summary>
    partial class Screen_Parent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClose_MouseDown(object sender, EventArgs e)
        {
            Button_Close.BackgroundImage = Button_Close.Icon_Order(SVG_Icon.Cross, SVG_Color.White_Select);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClose_MouseEnter(object sender, EventArgs e)
        {
            Button_Close.BackgroundImage = Button_Close.Icon_Order(SVG_Icon.Cross, SVG_Color.White_Highlight);
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
        private void Set_Visuals()
        {
            #region Custom EventHandlers
            MouseMove += new MouseEventHandler(Move_Window_Mouse_Move);
            MouseUp += new MouseEventHandler(Move_Window_Mouse_Up);
            MouseDown += new MouseEventHandler(Move_Window_Mouse_Down);

            Panel_Splash_Screen.MouseMove += new MouseEventHandler(Move_Window_Mouse_Move);
            Panel_Splash_Screen.MouseUp += new MouseEventHandler(Move_Window_Mouse_Up);
            Panel_Splash_Screen.MouseDown += new MouseEventHandler(Move_Window_Mouse_Down);

            Panel_Form_Screens.MouseMove += new MouseEventHandler(Move_Window_Mouse_Move);
            Panel_Form_Screens.MouseUp += new MouseEventHandler(Move_Window_Mouse_Up);
            Panel_Form_Screens.MouseDown += new MouseEventHandler(Move_Window_Mouse_Down);

            PictureBox_Screen_Splash.MouseMove += new MouseEventHandler(Move_Window_Mouse_Move);
            PictureBox_Screen_Splash.MouseUp += new MouseEventHandler(Move_Window_Mouse_Up);
            PictureBox_Screen_Splash.MouseDown += new MouseEventHandler(Move_Window_Mouse_Down);

            Load += new EventHandler(Parent_Screen_Load);
            Shown += new EventHandler(Parent_Screen_Shown);
            Clock.Tick += new EventHandler(Clock_Tick);

            Button_Close.MouseEnter += new EventHandler(ButtonClose_MouseEnter);
            Button_Close.MouseLeave += new EventHandler(ButtonClose_MouseLeaveANDMouseUp);
            Button_Close.MouseUp += new MouseEventHandler(ButtonClose_MouseLeaveANDMouseUp);
            Button_Close.MouseDown += new MouseEventHandler(ButtonClose_MouseDown);
            Button_Close.Click += new EventHandler(Button_Close_Click);
            #endregion

            #region Custom Theme
            /*******************************/
            /* Set Font                     /
            /*******************************/
#if !(RELEASE_UNIX || DEBUG_UNIX)
            float MainFontSize = 9f * 96f / CreateGraphics().DpiY;
#else
            float MainFontSize = 9f;
#endif

            Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            TextBox_Live_Log.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            GroupBox_Launcherlog.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);

            /********************************/
            /* Set Theme Colors & Images     /
            /********************************/

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);

            TransparencyKey = Color_Screen.BG_Splash;
            BackgroundImage = Image_Background.Settings;

            Button_Close.BackgroundImage = Button_Close.Icon_Order(SVG_Icon.Cross, SVG_Color.White);
            PictureBox_Screen_Splash.BackgroundImage = Image_Other.Logo_Splash;

            ForeColor = Color_Winform.Text_Fore_Color;
            BackColor = Color_Winform.BG_Fore_Color;

            GroupBox_Launcherlog.ForeColor = Color_Winform.Text_Fore_Color;
            TextBox_Live_Log.ForeColor = Color_Winform.Secondary_Text_Fore_Color;
            TextBox_Live_Log.BackColor = Color_Winform.BG_Darker_Fore_Color;

            /*******************************/
            /* Set Window Name              /
            /*******************************/

            Icon = FormsIcon.Retrive_Icon();
            Text = "SBRW Launcher: " + Application.ProductVersion;

            this.Closing += (x, y) =>
            {
                if (FunctionStatus.LoadingComplete)
                {
                    ClosingTasks();
                }
                else
                {
                    FunctionStatus.LauncherForceClose = true;
                }

                Screen_Instance = null;
            };
            #endregion

            #region Update Variables
            Screen_Instance = this;
            //Button_One.Text = DialogResult.OK.ToString();
            #endregion
        }
    }
}
