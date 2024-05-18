using SBRW.Launcher.RunTime.LauncherCore.Visuals;
using SBRW.Launcher.Core.Extension.Security_;
using SBRW.Launcher.Core.Extra.Conversion_;
using SBRW.Launcher.Core.Theme;
using System.Drawing;
using System.Windows.Forms;
using Svg;

namespace SBRW.Launcher.RunTime.LauncherCore.Support
{
    public static class SecurityCenter
    {
        /// <summary>Returns the Shield Image for Security Panel Button
        /// <code>"0" Regular Colored Image</code>
        /// <code>"1" Clickage Colored Image</code>
        /// <code>"2" Hover Colored Image</code>
        /// </summary>
        /// <param name="Control_Form"></param>
        /// <param name="ImageState">
        /// <code>"0" Regular Colored Image</code>
        /// <code>"1" Clickage Colored Image</code>
        /// <code>"2" Hover Colored Image</code>
        /// </param>
        /// <returns>Button Image</returns>
        public static Bitmap SecurityCenterIcon(this Control Control_Form, int ImageState)
        {
            switch (Security_Codes_Reference.Check())
            {
                case SecurityCenterCodes.Unix:
                    if (ImageState == 1) { return Control_Form.Icon_Order(SVG_Icon.Shield, SVG_Color.Base_Select); }
                    else if (ImageState == 2) { return Control_Form.Icon_Order(SVG_Icon.Shield, SVG_Color.Base_Highlight); }
                    else { return Control_Form.Icon_Order(SVG_Icon.Shield, SVG_Color.Base); }
                case SecurityCenterCodes.Firewall_Outdated:
                case SecurityCenterCodes.Defender_Outdated:
                case SecurityCenterCodes.Permissions_Outdated:
                    if (ImageState == 1) { return Control_Form.Icon_Order(SVG_Icon.Shield, SVG_Color.Warning_Select); }
                    else if (ImageState == 2) { return Control_Form.Icon_Order(SVG_Icon.Shield, SVG_Color.Warning_Highlight); }
                    else { return Control_Form.Icon_Order(SVG_Icon.Shield, SVG_Color.Warning); }
                case SecurityCenterCodes.Firewall_Error:
                case SecurityCenterCodes.Defender_Error:
                case SecurityCenterCodes.Permissions_Error:
                    if (ImageState == 1) { return Control_Form.Icon_Order(SVG_Icon.Shield, SVG_Color.Error_Select); }
                    else if (ImageState == 2) { return Control_Form.Icon_Order(SVG_Icon.Shield, SVG_Color.Error_Highlight); }
                    else { return Control_Form.Icon_Order(SVG_Icon.Shield, SVG_Color.Warning); }
                case SecurityCenterCodes.Firewall_Updated:
                case SecurityCenterCodes.Defender_Updated:
                case SecurityCenterCodes.Permissions_Updated:
                    if (ImageState == 1) { return Control_Form.Icon_Order(SVG_Icon.Shield, SVG_Color.Success_Select); }
                    else if (ImageState == 2) { return Control_Form.Icon_Order(SVG_Icon.Shield, SVG_Color.Success_Highlight); }
                    else { return Control_Form.Icon_Order(SVG_Icon.Shield, SVG_Color.Success); }
                default:
                    if (ImageState == 1) { return Control_Form.Icon_Order(SVG_Icon.Shield, SVG_Color.Unknown_Select); }
                    else if (ImageState == 2) { return Control_Form.Icon_Order(SVG_Icon.Shield, SVG_Color.Unknown_Highlight); }
                    else { return Control_Form.Icon_Order(SVG_Icon.Shield, SVG_Color.Unknown); }
            }
        }
    }
}
