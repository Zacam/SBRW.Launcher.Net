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
        /// Sets the Color for Buttons
        /// </summary>
        /// <param name="Elements">Button Control Name</param>
        /// <param name="Color">Range 0-3 Sets Colored Button.
        /// <code>"0" Checking Blue</code><code>"1" Success Green</code><code>"2" Warning Orange</code><code>"3" Error Red</code></param>
        /// <param name="EnabledORDisabled">Enables or Disables the Button</param>
        /// <remarks>Range 0-3 Sets Colored Button.
        /// <code>"0" Checking Blue</code><code>"1" Success Green</code><code>"2" Warning Orange</code><code>"3" Error Red</code></remarks>
        public void ColorSet(Button Elements, int Color, bool EnabledORDisabled)
        {
            switch (Color)
            {
                /* Checking Blue */
                case 0:
                    Elements.ForeColor = Color_Winform_Buttons.Blue_Fore_Color;
                    Elements.BackColor = Color_Winform_Buttons.Blue_Back_Color;
                    Elements.FlatAppearance.BorderColor = Color_Winform_Buttons.Blue_Border_Color;
                    Elements.FlatAppearance.MouseOverBackColor = Color_Winform_Buttons.Blue_Mouse_Over_Back_Color;
                    break;
                /* Success Green */
                case 1:
                    Elements.ForeColor = Color_Winform_Buttons.Green_Fore_Color;
                    Elements.BackColor = Color_Winform_Buttons.Green_Back_Color;
                    Elements.FlatAppearance.BorderColor = Color_Winform_Buttons.Green_Border_Color;
                    Elements.FlatAppearance.MouseOverBackColor = Color_Winform_Buttons.Green_Mouse_Over_Back_Color;
                    break;
                /* Warning Orange */
                case 2:
                    Elements.ForeColor = Color_Winform_Buttons.Yellow_Fore_Color;
                    Elements.BackColor = Color_Winform_Buttons.Yellow_Back_Color;
                    Elements.FlatAppearance.BorderColor = Color_Winform_Buttons.Yellow_Border_Color;
                    Elements.FlatAppearance.MouseOverBackColor = Color_Winform_Buttons.Yellow_Mouse_Over_Back_Color;
                    break;
                /* Error Red */
                case 3:
                    Elements.ForeColor = Color_Winform_Buttons.Red_Fore_Color;
                    Elements.BackColor = Color_Winform_Buttons.Red_Back_Color;
                    Elements.FlatAppearance.BorderColor = Color_Winform_Buttons.Red_Border_Color;
                    Elements.FlatAppearance.MouseOverBackColor = Color_Winform_Buttons.Red_Mouse_Over_Back_Color;
                    break;
                /* Unknown Gray */
                default:
                    Elements.ForeColor = Color_Winform_Buttons.Gray_Fore_Color;
                    Elements.BackColor = Color_Winform_Buttons.Gray_Back_Color;
                    Elements.FlatAppearance.BorderColor = Color_Winform_Buttons.Gray_Border_Color;
                    Elements.FlatAppearance.MouseOverBackColor = Color_Winform_Buttons.Gray_Mouse_Over_Back_Color;
                    break;
            }

            Elements.Enabled = EnabledORDisabled;
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

            /* Set Background with Transparent Key */
            BackgroundImage = Image_Background.Settings;
            TransparencyKey = Color_Screen.BG_Settings;

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

            KeyPreview = true;
            BackColor = Color_Winform_About.BG_Fore_Color;
            ForeColor = Color_Winform_About.Text_Fore_Color;

            /* Buttons */
            ColorSet(Button_Add, 1, true);
            ColorSet(Button_Update, 2, true);
            ColorSet(Button_Delete, 3, true);

            /* Input Boxes */
            TextBox_Email.BackColor = Color_Winform_Other.Input;
            TextBox_Email.ForeColor = Color_Text.L_Five;
            TextBox_Password.BackColor = Color_Winform_Other.Input;
            TextBox_Password.ForeColor = Color_Text.L_Five;
            TextBox_Nickname.BackColor = Color_Winform_Other.Input;
            TextBox_Nickname.ForeColor = Color_Text.L_Five;
            TextBox_ID.BackColor = Color_Winform_Other.Input;
            TextBox_ID.ForeColor = Color_Text.L_Five;
            TextBox_ID_Account.BackColor = Color_Winform_Other.Input;
            TextBox_ID_Account.ForeColor = Color_Text.L_Five;
            TextBox_Min.BackColor = Color_Winform_Other.Input;
            TextBox_Min.ForeColor = Color_Text.L_Five;
            TextBox_Max.BackColor = Color_Winform_Other.Input;
            TextBox_Max.ForeColor = Color_Text.L_Five;

            /* Check Boxes */
            CheckBox_Password_Reveal.ForeColor = Color_Winform_Other.CheckBoxes_Settings;

            /* Text Labels */
            Label_Email.ForeColor = Color_Text.L_Five;
            Label_Password.ForeColor = Color_Text.L_Five;
            Label_Nickname.ForeColor = Color_Text.L_Five;
            Label_ID.ForeColor = Color_Text.L_Five;
            Label_ID_Account.ForeColor = Color_Text.L_Five;
            Label_Min.ForeColor = Color_Text.L_Five;
            Label_Max.ForeColor = Color_Text.L_Five;

            /* Secondary Buttons */
            Button_Close.BackgroundImage = Button_Close.Icon_Order(SVG_Icon.Cross, SVG_Color.White);

            /* DataGrid */
            DataGridView_Account_List.BackgroundColor = Color.FromArgb(22, 29, 38);
            DataGridView_Account_List.GridColor = Color.FromArgb(41, 54, 70);
            DataGridView_Account_List.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridView_Account_List.ReadOnly = true;
            DataGridView_Account_List.TabStop = false;
            DataGridView_Account_List.RowHeadersVisible = false;
            DataGridView_Account_List.AllowUserToAddRows = false;
            DataGridView_Account_List.ClearSelection();
            DataGridView_Account_List.BorderStyle = BorderStyle.None;

            /* DataGridView Control Data Style Settings */
            DataGridView_Account_List.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridView_Account_List.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            DataGridView_Account_List.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // Colors Text
            DataGridView_Account_List.DefaultCellStyle.ForeColor = Color.FromArgb(178, 210, 255);
            // Colors Row(s)
            DataGridView_Account_List.DefaultCellStyle.BackColor = Color.FromArgb(31, 41, 54);
            // Colors Selected Row
            DataGridView_Account_List.DefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 54, 70);
            // Colors Selection Text
            DataGridView_Account_List.DefaultCellStyle.SelectionForeColor = Color.FromArgb(193, 219, 255);

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

            Shown += new EventHandler(Screen_Account_Manager_Shown);
            #endregion
        }
    }
}
