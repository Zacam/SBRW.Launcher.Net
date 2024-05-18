using SBRW.Launcher.Core.Theme.Conversion_;
using Svg;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SBRW.Concepts
{
    public partial class Form1 : Form
    {
        private static SvgDocument? Test_Cache_SVG { get;set; }
        private static SvgDocument? Test_Cache_SVG_2 { get; set; }
        private static SvgDocument? Test_Cache_SVG_3 { get; set; }
        private static SvgDocument? Test_Cache_SVG_4 { get; set; }        
        private static SvgDocument? Test_Cache_SVG_5 { get; set; }
        private static SvgDocument? Test_Cache_SVG_6 { get; set; }
        private static string Folder_Icon_DEBUG_Path { get; set; } = "..\\SBRW.Launcher.Net\\SBRW.Launcher.Net\\bin\\Debug\\net461\\Launcher_Data\\Icons\\";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            // Set the PictureBox image to the created bitmap
            /*
            var test = SvgDocument.Open((Folder_Icon_DEBUG_Path + "Input_Global.svg"));
            test.GetElementById("Mail").Display = "";
            test.GetElementById("Border").Stroke = test.GetElementById("Mail").Fill = new SvgColourServer(Color.FromArgb(230, 159, 0));
            test.GetElementById("Border").Fill = new SvgColourServer(Color.FromArgb(0, 191, 255));
            Picture_Input_Password.BackgroundImage = test.Draw();
            */
            Picture_Input_Password.BackgroundImage = Icon_Order(SVG_Icon.Input_Box_Password).Draw();
            Picture_Input_Email.BackgroundImage = Icon_Order(SVG_Icon.Input_Box_Email).Draw();
            //Picture_Logo.BackgroundImage = SvgDocument.Open((Folder_Icon_DEBUG_Path + "Logo.svg")).Draw();
            Picture_Icon_Version.BackgroundImage = Icon_Order(SVG_Icon.Check_Engine, SVG_Color.Warning).Draw();
            Picture_Icon_Server.BackgroundImage = Icon_Order(SVG_Icon.Globe, SVG_Color.Warning).Draw();
            Picture_Icon_API.BackgroundImage = Icon_Order(SVG_Icon.Plug, SVG_Color.Warning).Draw();
            Picture_Icon_Server_Discord.BackgroundImage = Icon_Order(SVG_Icon.Discord, SVG_Color.Warning).Draw();
            Picture_Icon_Server_Facebook.BackgroundImage = Icon_Order(SVG_Icon.Facebook, SVG_Color.Warning).Draw();
            Picture_Icon_Server_Home.BackgroundImage = Icon_Order(SVG_Icon.Home, SVG_Color.Warning).Draw();
            Picture_Icon_Server_Twitter.BackgroundImage = Icon_Order(SVG_Icon.Twitter, SVG_Color.Warning).Draw();
            Button_Close.BackgroundImage = Icon_Order(SVG_Icon.Cross, SVG_Color.Warning).Draw();
            Button_Settings.BackgroundImage = Icon_Order(SVG_Icon.Gear, SVG_Color.Warning).Draw();
            Button_Security_Center.BackgroundImage = Icon_Order(SVG_Icon.Shield, SVG_Color.Warning).Draw();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Picture_Server_Banner.Size = new System.Drawing.Size(523, 223);
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            Picture_Server_Banner.Size = new System.Drawing.Size(723, 431);
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            Picture_Server_Banner.Size = new System.Drawing.Size(923, 631);
        }
    }
}
