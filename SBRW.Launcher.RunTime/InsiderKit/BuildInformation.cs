using SBRW.Launcher.RunTime.LauncherCore.Languages.Visual_Forms;
using SBRW.Launcher.Core.Cache;

namespace SBRW.Launcher.RunTime.InsiderKit
{
    /* This sets Build Number Information */
    public class BuildInformation
    {
        /* Current month, day, year (2 digits), and letter! Ex: 12-15-20-A */
        /* If a second build gets release within the same day bump letter version up (No R2 or D2)*/

        const string DATE = "";
        const string TIME = "";
        const string TIME_ZONE = "";

        private static string BUILD { get; set; } = DATE + "-" + TIME + " " + TIME_ZONE;

        public static string BuildNumberOnly()
        {
            return Launcher_Value.Launcher_Insider_Version = DATE + "-" + TIME;
        }
        
        public static string BuildNumber()
        {
            if (BuildDevelopment.Allowed())
            {
                return Translations.Database("KitEnabler_Dev") + ": " + BUILD;
            }
            else if (BuildBeta.Allowed())
            {
                return Translations.Database("KitEnabler_Beta") + ": " + BUILD;
            }

            return Translations.Database("KitEnabler_Public") + ": " + BUILD;
        }
    }
}
