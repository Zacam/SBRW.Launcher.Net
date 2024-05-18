using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SBRW.Concepts
{
    public partial class MemoryLeakControlTest : Form
    {
        public MemoryLeakControlTest()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = TabControl_Shared_Hub.TabPages[e.Index];
            e.Graphics.FillRectangle(new SolidBrush(page.BackColor), e.Bounds);

            Rectangle paddedBounds = e.Bounds;
            int yOffset = (e.State == DrawItemState.Selected) ? -2 : 1;
            paddedBounds.Offset(1, yOffset);
            TextRenderer.DrawText(e.Graphics, page.Text, e.Font, paddedBounds, page.ForeColor);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_Settings_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawTabControlTabs(TabControl_Settings, e);
        }
        /// <summary>Draws the specified tab control.</summary>
        /// <param name="tabControl">The control on which to draw.</param>
        /// <param name="e">The event arguments.</param>
        private void DrawTabControlTabs(TabControl tabControl, DrawItemEventArgs e)
        {
            // Get the bounding end of tab strip rectangles.
            Rectangle tabstripEndRect = tabControl.GetTabRect(tabControl.TabPages.Count - 1);
            RectangleF tabstripEndRectF = new RectangleF(tabstripEndRect.X + tabstripEndRect.Width, tabstripEndRect.Y - 5,
         tabControl.Width - (tabstripEndRect.X + tabstripEndRect.Width), tabstripEndRect.Height + 5);

            // First, do the end of the tab strip.
            // If we have an image use it.
            if (tabControl.Parent.BackgroundImage != null)
            {
                RectangleF src = new RectangleF(tabstripEndRectF.X + tabControl.Left, tabstripEndRectF.Y + tabControl.Top, tabstripEndRectF.Width, tabstripEndRectF.Height);
                e.Graphics.DrawImage(tabControl.Parent.BackgroundImage, tabstripEndRectF, src, GraphicsUnit.Pixel);
            }
            // If we have no image, use the background color.
            else
            {
                using (Brush backBrush = new SolidBrush(tabControl.Parent.BackColor))
                {
                    e.Graphics.FillRectangle(backBrush, tabstripEndRectF);
                }
            }

            // Set up the page and the various pieces.
            TabPage page = tabControl.TabPages[e.Index];
            Brush BackBrush = new SolidBrush(page.BackColor);
            Brush ForeBrush = new SolidBrush(page.ForeColor);
            string TabName = page.Text;

            // Set up the offset for an icon, the bounding rectangle and image size and then fill the background.
            int iconOffset = 0;
            Rectangle tabBackgroundRect = e.Bounds;
            e.Graphics.FillRectangle(BackBrush, tabBackgroundRect);

            // Draw out the label.
            Rectangle labelRect = new Rectangle(tabBackgroundRect.X + iconOffset, tabBackgroundRect.Y + 3,
         tabBackgroundRect.Width - iconOffset, tabBackgroundRect.Height - 3);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(TabName, e.Font, ForeBrush, labelRect, sf);

            //Dispose objects
            sf.Dispose();
            BackBrush.Dispose();
            ForeBrush.Dispose();
        }
    }
}
