using SBRW.Launcher.Core.Theme;
using SBRW.Launcher.RunTime.LauncherCore.Visuals;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SBRW.Launcher.App.UI_Forms.Main_Screen
{
    public partial class Screen_Main
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_Button_Green_MouseEnterUp(object sender, EventArgs e)
        {
            if (sender is PictureBox Live_PictureBox)
            {
                if (Live_PictureBox.BackgroundImage != Image_Button.Green_Hover)
                {
                    Live_PictureBox.BackgroundImage = Image_Button.Green_Hover;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_Button_Green_MouseLeave(object sender, EventArgs e)
        {
            if (sender is PictureBox Live_PictureBox)
            {
                if (Live_PictureBox.BackgroundImage != Image_Button.Green)
                {
                    Live_PictureBox.BackgroundImage = Image_Button.Green;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_Button_Green_MouseDown(object sender, EventArgs e)
        {
            if (sender is PictureBox Live_PictureBox)
            {
                if (Live_PictureBox.BackgroundImage != Image_Button.Green_Click)
                {
                    Live_PictureBox.BackgroundImage = Image_Button.Green_Click;
                }
            }
        }
        private void UpdatePictureBoxImage(object sender, Image newImage)
        {
            if (sender is PictureBox pictureBox)
            {
                if (pictureBox.BackgroundImage != newImage)
                {
                    pictureBox.BackgroundImage = newImage;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_Button_Gray_MouseDown(object sender, EventArgs e)
        {
            UpdatePictureBoxImage(sender, Image_Button.Grey_Click);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_Button_Gray_MouseEnterUp(object sender, EventArgs e)
        {
            UpdatePictureBoxImage(sender, Image_Button.Grey_Hover);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_Button_Gray_MouseLeave(object sender, EventArgs e)
        {
            UpdatePictureBoxImage(sender, Image_Button.Grey);
        }
    }
}
