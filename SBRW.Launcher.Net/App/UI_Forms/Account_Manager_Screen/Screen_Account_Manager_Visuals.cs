using SBRW.Launcher.App.UI_Forms.Parent_Screen;
using SBRW.Launcher.Core.Theme;
using SBRW.Launcher.RunTime.LauncherCore.Support;
using SBRW.Launcher.RunTime.LauncherCore.Visuals;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SBRW.Launcher.App.UI_Forms.Account_Manager_Screen
{
    partial class Screen_Account_Manager
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
            #region Primary Settings
            /*******************************/
            /* Set Window Name              /
            /*******************************/

            Icon = FormsIcon.Retrive_Icon();
            Text = "Accounts - SBRW Launcher: " + Application.ProductVersion;

            /*******************************/
            /* Set Background Image         /
            /*******************************/


            /*******************************/
            /* Set Font                     /
            /*******************************/
#if !(RELEASE_UNIX || DEBUG_UNIX)
            float MainFontSize = 9f * 96f / CreateGraphics().DpiY;
            float SecondaryFontSize = 8f * 96f / CreateGraphics().DpiY;
#else
            float MainFontSize = 9f;
            float SecondaryFontSize = 8f;
#endif
            /* General */
            Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Regular);
            /* Inputs Controls */
            TextBox_Email.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            TextBox_ID.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            TextBox_ID_Account.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            TextBox_Max.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            TextBox_Min.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            TextBox_Nickname.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            TextBox_Password.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);
            /* Buttons Controls */
            Button_Add.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Button_Delete.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Button_Update.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            /* Label Controls */
            Label_Email.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_ID.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_ID_Account.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Max.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Min.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Nickname.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Label_Password.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            /* Checkbox Controls */
            CheckBox_Password_Reveal.Font = new Font(FormsFont.Primary(), MainFontSize, FontStyle.Regular);

            /********************************/
            /* Set Theme Colors & Images     /
            /********************************/

            /* Buttons */


            /* Label Links */
            //Label_Email.ForeColor = Color_Winform.Text_Fore_Color;

            /* Labels */
            //Label_Game_Current_Path.ForeColor = Color_Text.L_Five;

            /* Input Boxes */
            TextBox_Email.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            TextBox_Email.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;

            /* Check boxes */
            CheckBox_Password_Reveal.ForeColor = Color_Winform_Other.CheckBoxes_Settings;

            Label_Email.BackColor = Color_Winform_Other.Input;
            Label_Email.ForeColor = Color_Text.L_Five;

            /* Secondary Buttons */
            Button_Close.BackgroundImage = Button_Close.Icon_Order(SVG_Icon.Cross, SVG_Color.White);

            /********************************/
            /* Events                        /
            /********************************/

            /* DataGridView Control Events */
            DataGridView_Account_List.CellClick += new DataGridViewCellEventHandler(DataGridView_Account_List_CellClick);
            /* Buttons Control Events */
            Button_Add.Click += new EventHandler(Button_Add_Click);
            Button_Update.Click += new EventHandler(Button_Update_Click);
            Button_Delete.Click += new EventHandler(Button_Delete_Click);
            /* */
            CheckBox_Password_Reveal.CheckedChanged += new EventHandler(CheckBox_Password_Reveal_CheckedChanged);
            CheckBox_Password_Reveal.CheckStateChanged += new EventHandler(CheckBox_Password_Reveal_CheckedChanged);
            CheckBox_Password_Reveal.CausesValidationChanged += new EventHandler(CheckBox_Password_Reveal_CheckedChanged);
            CheckBox_Password_Reveal.Click += new EventHandler(CheckBox_Password_Reveal_CheckedChanged);
            /* Close */
            Button_Close.MouseEnter += new EventHandler(ButtonClose_MouseEnter);
            Button_Close.MouseLeave += new EventHandler(ButtonClose_MouseLeaveANDMouseUp);
            Button_Close.MouseUp += new MouseEventHandler(ButtonClose_MouseLeaveANDMouseUp);
            Button_Close.MouseDown += new MouseEventHandler(ButtonClose_MouseDown);
            Button_Close.Click += new EventHandler(ButtonClose_Click);

            if (Screen_Parent.Screen_Instance != null)
            {
                MouseMove += new MouseEventHandler(Screen_Parent.Screen_Instance.Move_Window_Mouse_Move);
                MouseUp += new MouseEventHandler(Screen_Parent.Screen_Instance.Move_Window_Mouse_Up);
                MouseDown += new MouseEventHandler(Screen_Parent.Screen_Instance.Move_Window_Mouse_Down);
            }

            Load += new EventHandler(Screen_Account_Manager_Load);
            Shown += new EventHandler(Screen_Account_Manager_Shown);

            /* DataGridView Control Data Style Settings */
            DataGridView_Account_List.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridView_Account_List.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            DataGridView_Account_List.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            KeyPreview = true;

            BackColor = Color_Winform_About.BG_Fore_Color;
            ForeColor = Color_Winform_About.Text_Fore_Color;

            /* Tabs Global Background Color */
            //TabControl_Shared_Hub.BackColor = TabControl_Settings.BackColor = TabControl_Launcher.BackColor = TabControl_Game.BackColor = TabControl_Security_Center.BackColor = Color.FromArgb(22, 29, 38);
            /* Tabs (Menu) Text Color */
            //TabControl_Shared_Hub.ForeColor = TabControl_Settings.ForeColor = TabControl_Launcher.ForeColor = TabControl_Game.ForeColor = TabControl_Security_Center.ForeColor = Color.FromArgb(192, 192, 192);
            /* Tabs Current Selected & Hover Menu Tab */
            //TabControl_Shared_Hub.SelectedTabColor = TabControl_Settings.SelectedTabColor = TabControl_Launcher.SelectedTabColor = TabControl_Game.SelectedTabColor = TabControl_Security_Center.SelectedTabColor = Color.FromArgb(128, 44, 58, 76);
            /* Tabs Other Menu Tab */
            //TabControl_Shared_Hub.TabColor = TabControl_Settings.TabColor = TabControl_Launcher.TabColor = TabControl_Game.TabColor = TabControl_Security_Center.TabColor = Color.FromArgb(44, 58, 76);
            /* */
            //TabControl_Shared_Hub.TabsHide = true;
            /* */
            #endregion
            /* Theming, Function, EventHandlers, Etc. Meant to load critial functions before the forms loads */
        }
    }
}
