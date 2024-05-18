using SBRW.Launcher.Core.Theme.Conversion_;
using Svg;
using System.Drawing;

namespace SBRW.Concepts
{
    public partial class Form1
    {
        private SvgDocument Icon_Order(SVG_Icon Icon_Form)
        {
            return Icon_Order(Icon_Form, SVG_Color.Unknown);
        }
        private SvgDocument Icon_Order(SVG_Icon Icon_Form, SVG_Color Icon_Color)
        {
            Color Custom_Color;
            SvgDocument Custom_SVG;

            switch (Icon_Color)
            {
                case SVG_Color.Base:
                    Custom_Color = Color.FromArgb(66, 179, 189);
                    break;
                case SVG_Color.Error:
                    Custom_Color = Color.FromArgb(254, 0, 0);
                    break;
                case SVG_Color.Success:
                    Custom_Color = Color.FromArgb(159, 193, 32);
                    break;
                case SVG_Color.Unknown:
                    Custom_Color = Color.FromArgb(132, 132, 132);
                    break;
                case SVG_Color.Warning:
                    Custom_Color = Color.FromArgb(230, 159, 0);
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
                case SVG_Icon.Plug:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Plug_Connect());
                    Custom_SVG.Fill = new SvgColourServer(Custom_Color);
                    break;
                case SVG_Icon.Save:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Save());
                    Custom_SVG.Stroke = new SvgColourServer(Custom_Color);
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
                    Custom_SVG.GetElementById("Border").Stroke = new SvgColourServer(Custom_Color);
                    break;
                case SVG_Icon.Input_Box_Email:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Inputs_Box());
                    Custom_SVG.GetElementById("Mail").Display = " ";
                    Custom_SVG.GetElementById("Border").Stroke = new SvgColourServer(Custom_Color);
                    Custom_SVG.GetElementById("Mail").Fill = new SvgColourServer(Custom_Color);
                    break;
                case SVG_Icon.Input_Box_Password:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Inputs_Box());
                    Custom_SVG.GetElementById("Key").Display = "";
                    Custom_SVG.GetElementById("Border").Stroke = new SvgColourServer(Custom_Color);
                    Custom_SVG.GetElementById("Key").Fill = new SvgColourServer(Custom_Color);
                    break;
                case SVG_Icon.Input_Box_Ticket:
                    Custom_SVG = SvgDocument.FromSvg<SvgDocument>(Embeded_Files.SvG_Inputs_Box());
                    Custom_SVG.GetElementById("Ticket").Display = "";
                    Custom_SVG.GetElementById("Border").Stroke = new SvgColourServer(Custom_Color);
                    Custom_SVG.GetElementById("Ticket").Fill = new SvgColourServer(Custom_Color);
                    break;
                default:
                    Custom_SVG = default;
                    break;
            }

            return Custom_SVG;
        }

        public enum SVG_Icon
        {
            Check_Engine,
            Cross,
            Discord,
            Facebook,
            Gear,
            Globe,
            Home,
            Plug,
            Save,
            Shield,
            Twitter,
            Input_Box,
            Input_Box_Email,
            Input_Box_Password,
            Input_Box_Ticket
        }

        public enum SVG_Color
        {
            Base,
            Error,
            Success,
            Unknown,
            Warning
        }

    }
}
