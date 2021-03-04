using System.Windows.Forms;

namespace ClassicGameLauncher.App.Classes.LauncherCore.Client.Web
{
    class UserAgent
    {
        public static string ProjectLink = "(+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)";
        public static string ProjectAltLink = "WinForms (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)";
        public static string AgentName = "GameLauncher";
        public static string AgentAltName = "GameLauncherReborn";
        public static string WindowTextForLauncher = "SBRW Simplified Launcher";
        public static string Build = Application.ProductVersion;


        public static string UserAgentName = AgentName + " " + ProjectLink;
        public static string WindowTextForGame = WindowTextForLauncher + ": v" + Build;
        public static string UserAgentNameWithBuild = AgentAltName + " " + Build;
        public static string UserAgentHeaderName = AgentAltName + " 2.1.7.6 " + ProjectAltLink;
    }
}
