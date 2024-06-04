using SBRW.Launcher.RunTime.LauncherCore.Languages.Visual_Forms;
using SBRW.Launcher.Core.Cache;
using System.Text;

namespace SBRW.Launcher.RunTime.InsiderKit
{
    /* This sets Build Number Information */
    public class BuildInformation
    {
        /* Current month, day, year (2 digits), and letter! Ex: 12-15-20-A */
        /* If a second build gets release within the same day bump letter version up (No R2 or D2)*/
        const string DATE = "06-04-2024";
        const string DATE_SHORT = "06-04-24";
        const string TIME = "0052";
        const string TIME_SECONDS = "54";
        const string TIME_ZONE = "-07:00";
        /// <summary>
        /// Build Information <i>(Full Information)</i>
        /// </summary>
        /// <returns>MM-dd-yyyy-HHmmss -00:00</returns>
        public static string FULL_INFO { get; private set; } = DATE + "-" + TIME + TIME_SECONDS + " " + TIME_ZONE;
        /// <summary>
        /// Build Information <i>(Shorten Information)</i>
        /// </summary>
        /// <returns>MM-dd-yy-HHmm</returns>
        public static string SHORT_INFO { get; private set; } = DATE_SHORT + "-" + TIME;
        /// <summary>
        /// Build Information <i>(Shorten Information)</i>
        /// </summary>
        /// <returns>MM-dd-yy-HHmmss</returns>
        public static string SHORT_INFO_WITH_SECONDS { get; private set; } = DATE_SHORT + "-" + TIME + TIME_SECONDS;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string NumberOnly()
        {
            return Launcher_Value.Launcher_Insider_Version = SHORT_INFO;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string NumberDisplayFull()
        {
            if (BuildDevelopment.Allowed())
            {
                return Translations.Database("KitEnabler_Dev") + ": " + FULL_INFO;
            }
            else if (BuildBeta.Allowed())
            {
                return Translations.Database("KitEnabler_Beta") + ": " + FULL_INFO;
            }

            return Translations.Database("KitEnabler_Public") + ": " + FULL_INFO;
        }
    }
}