using System;
using System.Windows.Forms;

namespace SBRW.Launcher.App.UI_Forms.MessageBox_Screen
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Screen_MessageBox : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public MessageBoxIcon Icon_MB { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MessageBoxButtons Buttons_MB { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DialogResult Result_MB { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Pending_MB { get; private set; } = true;
        /// <summary>
        /// 
        /// </summary>
        public bool Window_Controls_MB { get; set; } = true;
        /// <summary>
        /// 
        /// </summary>
        public Exception? Exception_MB { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Screen_MessageBox()
        {
            InitializeComponent();
            Set_Visuals();
        }
    }
}
