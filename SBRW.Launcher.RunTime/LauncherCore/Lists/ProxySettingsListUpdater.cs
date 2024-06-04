using SBRW.Launcher.RunTime.LauncherCore.APICheckers;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using SBRW.Launcher.Core.Reference.Json_.Newtonsoft_;
using SBRW.Launcher.Core.Discord.RPC_;
using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace SBRW.Launcher.RunTime.LauncherCore.Lists
{
    /// <summary>
    /// TODO: Translation
    /// </summary>
    public class ProxySettingsListUpdater
    {
        public static List<Json_List_Proxy_Logging> Proxy_Logging { get; set; } = new List<Json_List_Proxy_Logging>();
        public static List<Json_List_Proxy_GZip_Version> Proxy_GZip_Version { get; set; } = new List<Json_List_Proxy_GZip_Version>();

        public static void GetList()
        {
            LogToFileAddons.Parent_Log_Screen(2, "LIST CORE", "Creating Proxy Settings List");
            Presence_Launcher.Status(0, "Creating Proxy Settings List");

            try
            {
                /* Proxy Logging */
                Proxy_Logging.Add(new Json_List_Proxy_Logging()
                {
                    Name = "All",
                    Details = "Saves all Information",
                    Mode = Core.Proxy.Log_.CommunicationLogRecord.All
                });
                Proxy_Logging.Add(new Json_List_Proxy_Logging()
                {
                    Name = "Error",
                    Details = "Saves Errors Only (Recommended)",
                    Mode = Core.Proxy.Log_.CommunicationLogRecord.Errors
                });
                Proxy_Logging.Add(new Json_List_Proxy_Logging()
                {
                    Name = "Responses",
                    Details = "Saves Responses Only",
                    Mode = Core.Proxy.Log_.CommunicationLogRecord.Responses
                });
                Proxy_Logging.Add(new Json_List_Proxy_Logging()
                {
                    Name = "Requests",
                    Details = "Saves Requests Only",
                    Mode = Core.Proxy.Log_.CommunicationLogRecord.Requests
                });
                Proxy_Logging.Add(new Json_List_Proxy_Logging()
                {
                    Name = "None",
                    Details = "Disables Logging and Improves Performance",
                    Mode = Core.Proxy.Log_.CommunicationLogRecord.None
                });
                /* Proxy GZip Version */
                Proxy_GZip_Version.Add(new Json_List_Proxy_GZip_Version()
                {
                    Name = "4.0",
                    Details = "Uses Launcher Proxy Version of 2.3.X",
                    Version = Core.Proxy.Nancy_.GzipVersion.Four
                });
                Proxy_GZip_Version.Add(new Json_List_Proxy_GZip_Version()
                {
                    Name = "3.0",
                    Details = "Uses Launcher Proxy Version of 2.1.8.X",
                    Version = Core.Proxy.Nancy_.GzipVersion.Three
                });
                Proxy_GZip_Version.Add(new Json_List_Proxy_GZip_Version()
                {
                    Name = "2.0",
                    Details = "Uses Launcher Proxy Version of 2.1.7.2",
                    Version = Core.Proxy.Nancy_.GzipVersion.Two
                });
                Proxy_GZip_Version.Add(new Json_List_Proxy_GZip_Version()
                {
                    Name = "1.1",
                    Details = "Uses Launcher Proxy Version of 2.1.6.6 (Revised)",
                    Version = Core.Proxy.Nancy_.GzipVersion.OneV2
                });
                Proxy_GZip_Version.Add(new Json_List_Proxy_GZip_Version()
                {
                    Name = "1.0",
                    Details = "Uses Launcher Proxy Version of 2.1.6.6",
                    Version = Core.Proxy.Nancy_.GzipVersion.One
                });
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("LIST CORE Compile", string.Empty, Error, string.Empty, true);
                if (Error.InnerException != null && !string.IsNullOrWhiteSpace(Error.InnerException.Message))
                {
                    LogToFileAddons.Parent_Log_Screen(5, "LIST CORE Compile", Error.InnerException.Message, false, true);
                }
            }

            LogToFileAddons.Parent_Log_Screen(3, "LIST CORE", "Done");

            LogToFileAddons.Parent_Log_Screen(1, "API", "Moved to Function");
            /* Run the API Checks to Make Sure it Visually Displayed Correctly */
            VisualsAPIChecker.PingAPIStatus();
        }
    }
}