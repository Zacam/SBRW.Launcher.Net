using Svg;
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
            // Load SVG document
            //Test_Cache_SVG = Test_Cache_SVG_2 = Test_Cache_SVG_3 = Test_Cache_SVG_4 = Test_Cache_SVG_5 = Test_Cache_SVG_6 = SvgDocument.Open(svgFilePath);
            //Test_Cache_SVG = Test_Cache_SVG_2 = Test_Cache_SVG_3 = Test_Cache_SVG_4 = Test_Cache_SVG_5 = Test_Cache_SVG_6 = SvgDocument.Open<SvgDocument>(new MemoryStream(Extract_Resource.AsByte("SBRW.Concepts.Logo.svg")));
            //Test_Cache_SVG = Test_Cache_SVG_2 = Test_Cache_SVG_3 = Test_Cache_SVG_4 = Test_Cache_SVG_5 = Test_Cache_SVG_6 = (SvgDocument)new SvgImage().GetImage("https://dev.w3.org/SVG/tools/svgweb/samples/svg-files/AJ_Digital_Camera.svg");

            Test_Cache_SVG = SvgDocument.Open(Folder_Icon_DEBUG_Path + "Icon_Check_Engine.svg");
            Color Base_Colour = Color.FromArgb(66, 179, 189);
            Color Error_Colour = Color.FromArgb(254, 0, 0);
            Color Success_Colour = Color.FromArgb(159, 193, 32);
            Color Unknown_Colour = Color.FromArgb(132, 132, 132);
            Color Warning_Colour = Color.FromArgb(230, 159, 0);
            Test_Cache_SVG.Fill = new SvgColourServer(Warning_Colour);

            Test_Cache_SVG_4 = SvgDocument.Open(Folder_Icon_DEBUG_Path + "Input_Global.svg");
            Test_Cache_SVG_3 = SvgDocument.Open(Folder_Icon_DEBUG_Path + "Input_Global.svg");
            Test_Cache_SVG_2 = SvgDocument.Open(Folder_Icon_DEBUG_Path + "Input_Global.svg");
            Test_Cache_SVG_3.GetElementById("Key").Display = "";
            Test_Cache_SVG_4.GetElementById("Mail").Display = "";
            // Set the PictureBox image to the created bitmap
            Picture_Input_Password.BackgroundImage = Test_Cache_SVG_3.Draw();
            Picture_Input_Email.BackgroundImage = Test_Cache_SVG_4.Draw();
            Picture_Logo.BackgroundImage = SvgDocument.Open((Folder_Icon_DEBUG_Path + "Logo.svg")).Draw();
            Picture_Icon_Version.BackgroundImage = Test_Cache_SVG.Draw();
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
