using ClassicGameLauncher.App.Classes.LauncherCore.Global;
using System;
using System.Collections.Generic;
using System.Net;

namespace ClassicGameLauncher.App.Classes.LauncherCore.Lists
{
    public class ServerListUpdater
    {
        public static string ServerListStatus = "Unknown";

        public static List<Object> ServerList = new List<Object>();

        public static void GetList()
        {
            foreach (var serverListURL in URLs.serverlisturl)
            {
                try
                {
                    var wc = new WebClient();
                    var response = wc.DownloadString(serverListURL);

                    try
                    {
                        String[] substrings = response.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                        foreach (var substring in substrings)
                        {
                            if (!String.IsNullOrEmpty(substring))
                            {
                                String[] substrings22 = substring.Split(new string[] { ";" }, StringSplitOptions.None);
                                ServerList.Add(new
                                {
                                    Text = substrings22[0],
                                    Value = substrings22[1]
                                });
                            }
                        }
                        ServerListStatus = "Loaded";
                        break;
                    }
                    catch (Exception)
                    {
                        ServerListStatus = "Error";
                    }
                }
                catch (Exception)
                {
                    ServerList.Add(new
                    {
                        Text = "Server List Error",
                        Value = "http://localhost"
                    });
                    ServerListStatus = "Failed";
                }
            }
        }
    }
}
