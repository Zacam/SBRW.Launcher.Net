using SBRW.Launcher.Core.Theme.Conversion_;
using Svg;
using System.Drawing;
using System.Windows.Forms;

namespace SBRW.Launcher.RunTime.LauncherCore.Visuals
{
    /// <summary>
    /// 
    /// </summary>
    public static class EmbeddedSVGs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Control_Form"></param>
        /// <param name="Icon_Form"></param>
        /// <returns></returns>
        public static Bitmap Icon_Order(this Control Control_Form, SVG_Icon Icon_Form)
        {
            return Control_Form.Icon_Order(Icon_Form, SVG_Color.White);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Control_Form"></param>
        /// <param name="Icon_Form"></param>
        /// <param name="Icon_Color"></param>
        /// <returns></returns>
        public static Bitmap Icon_Order(this Control Control_Form, SVG_Icon Icon_Form, SVG_Color Icon_Color)
        {
            return Icon_Form.Icon_Order(Icon_Color, Control_Form.Width, Control_Form.Height);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Control_Form"></param>
        /// <param name="Icon_Form"></param>
        /// <param name="Icon_Color"></param>
        /// <param name="Control_Width"></param>
        /// <param name="Control_Height"></param>
        /// <returns></returns>
        public static Bitmap Icon_Order(this SVG_Icon Icon_Form, SVG_Color Icon_Color, 
            int Control_Width, int Control_Height)
        {
            return Icon_Form.Icon_Order(Icon_Color).Draw(Control_Width, Control_Height);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Icon_Form"></param>
        /// <returns></returns>
        public static SvgDocument Icon_Order(this SVG_Icon Icon_Form)
        {
            return Icon_Form.Icon_Order(SVG_Color.White);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Icon_Form"></param>
        /// <param name="Icon_Color"></param>
        /// <returns></returns>
        public static SvgDocument Icon_Order(this SVG_Icon Icon_Form, SVG_Color Icon_Color)
        {
            Color Custom_Color;
            SvgDocument Custom_SVG;

            switch (Icon_Color)
            {
                case SVG_Color.Base:
                    Custom_Color = Color.FromArgb(66, 179, 189);
                    break;
                case SVG_Color.Base_Highlight:
                    Custom_Color = Color.FromArgb(84, 186, 195);
                    break;
                case SVG_Color.Base_Select:
                    Custom_Color = Color.FromArgb(59, 161, 170);
                    break;
                case SVG_Color.Error:
                    Custom_Color = Color.FromArgb(254, 0, 0);
                    break;
                case SVG_Color.Error_Highlight:
                    Custom_Color = Color.FromArgb(254, 25, 25);
                    break;
                case SVG_Color.Error_Select:
                    Custom_Color = Color.FromArgb(228, 0, 0);
                    break;
                case SVG_Color.Success:
                    Custom_Color = Color.FromArgb(159, 193, 32);
                    break;
                case SVG_Color.Success_Highlight:
                    Custom_Color = Color.FromArgb(168, 199, 54);
                    break;
                case SVG_Color.Success_Select:
                    Custom_Color = Color.FromArgb(143, 173, 28);
                    break;
                case SVG_Color.Unknown:
                    Custom_Color = Color.FromArgb(132, 132, 132);
                    break;
                case SVG_Color.Unknown_Highlight:
                    Custom_Color = Color.FromArgb(144, 144, 144);
                    break;
                case SVG_Color.Unknown_Select:
                    Custom_Color = Color.FromArgb(118, 118, 118);
                    break;
                case SVG_Color.Warning:
                    Custom_Color = Color.FromArgb(230, 159, 0);
                    break;
                case SVG_Color.Warning_Highlight:
                    Custom_Color = Color.FromArgb(232, 168, 25);
                    break;
                case SVG_Color.Warning_Select:
                    Custom_Color = Color.FromArgb(207, 143, 0);
                    break;
                case SVG_Color.White:
                    Custom_Color = Color.FromArgb(229, 229, 229);
                    break;
                case SVG_Color.White_Highlight:
                    Custom_Color = Color.FromArgb(255, 255, 255);
                    break;
                case SVG_Color.White_Select:
                    Custom_Color = Color.FromArgb(204, 204, 204);
                    break;
                default:
                    Custom_Color = default(Color);
                    break;
            }
            switch (Icon_Form)
            {
                case SVG_Icon.Check_Engine:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Check_Engine());
                    Custom_SVG.Fill = new SvgColourServer(Custom_Color);
                    break;
                case SVG_Icon.Cross:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Cross());
                    Custom_SVG.Fill = new SvgColourServer(Custom_Color);
                    break;
                case SVG_Icon.Discord:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Discord());
                    Custom_SVG.Fill = new SvgColourServer(Custom_Color);
                    break;
                case SVG_Icon.Facebook:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Facebook());
                    Custom_SVG.Fill = new SvgColourServer(Custom_Color);
                    Custom_SVG.GetElementById("Background").Fill = new SvgColourServer(Custom_Color);
                    break;
                case SVG_Icon.Gear:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Gear());
                    Custom_SVG.Fill = new SvgColourServer(Custom_Color);
                    break;
                case SVG_Icon.Globe:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Globe());
                    Custom_SVG.Stroke = new SvgColourServer(Custom_Color);
                    break;
                case SVG_Icon.Home:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Home());
                    Custom_SVG.Fill = new SvgColourServer(Custom_Color);
                    break;
                case SVG_Icon.Offline:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Offline());
                    Custom_SVG.Fill = new SvgColourServer(Custom_Color);
                    break;
                case SVG_Icon.Plug:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Plug_Connect());
                    Custom_SVG.Fill = new SvgColourServer(Custom_Color);
                    break;
                case SVG_Icon.Save:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Save());
                    Custom_SVG.Stroke = new SvgColourServer(Custom_Color);
                    break;
                case SVG_Icon.Server:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Server());
                    Custom_SVG.Fill = new SvgColourServer(Custom_Color);
                    break;
                case SVG_Icon.Shield:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Shield());
                    Custom_SVG.Fill = new SvgColourServer(Custom_Color);
                    break;
                case SVG_Icon.Twitter:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Twitter());
                    Custom_SVG.Fill = new SvgColourServer(Custom_Color);
                    break;
                case SVG_Icon.Input_Box:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Inputs_Box());
                    Custom_SVG.GetElementById("Background").Stroke = new SvgColourServer(Custom_Color);
                    break;
                case SVG_Icon.Input_Box_Email:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Inputs_Box());
                    Custom_SVG.GetElementById("Mail").Display = " ";
                    Custom_SVG.GetElementById("Background").Stroke = new SvgColourServer(Custom_Color);
                    Custom_SVG.GetElementById("Mail").Fill = new SvgColourServer(Custom_Color);
                    break;
                case SVG_Icon.Input_Box_Password:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Inputs_Box());
                    Custom_SVG.GetElementById("Key").Display = "";
                    Custom_SVG.GetElementById("Background").Stroke = new SvgColourServer(Custom_Color);
                    Custom_SVG.GetElementById("Key").Fill = new SvgColourServer(Custom_Color);
                    break;
                case SVG_Icon.Input_Box_Ticket:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Inputs_Box());
                    Custom_SVG.GetElementById("Ticket").Display = "";
                    Custom_SVG.GetElementById("Background").Stroke = new SvgColourServer(Custom_Color);
                    Custom_SVG.GetElementById("Ticket").Fill = new SvgColourServer(Custom_Color);
                    break;
                default:
                    Custom_SVG = default;
                    break;
            }

            return Custom_SVG;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum SVG_Icon
    {
        Check_Engine,
        Cross,
        Discord,
        Facebook,
        Gear,
        Globe,
        Home,
        Offline,
        Plug,
        Save,
        Server,
        Shield,
        Twitter,
        Input_Box,
        Input_Box_Email,
        Input_Box_Password,
        Input_Box_Ticket
    }
    /// <summary>
    /// 
    /// </summary>
    public enum SVG_Color
    {
        Base,
        Base_Highlight,
        Base_Select,
        Error,
        Error_Highlight,
        Error_Select,
        Success,
        Success_Highlight,
        Success_Select,
        Unknown,
        Unknown_Highlight,
        Unknown_Select,
        Warning,
        Warning_Highlight,
        Warning_Select,
        White,
        White_Highlight,
        White_Select
    }
}
