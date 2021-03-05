using GameLauncher.App.Classes.LauncherCore.FileReadWrite;
using GameLauncher.App.Classes.LauncherCore.Global;
using GameLauncher.App.Classes.LauncherCore.Hashes;
using GameLauncher.App.Classes.LauncherCore.Lists.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;

namespace GameLauncher.App.Classes.LauncherCore.Lists
{
    public class ServerListUpdater
    {
        public static string ServerListStatus = "Unknown";

        public static List<JsonServerList> NoCategoryList = new List<JsonServerList>();

        public static List<JsonServerList> CleanList = new List<JsonServerList>();

        public static void GetList()
        {
            List<JsonServerList> serverInfos = new List<JsonServerList>();

            foreach (var serverListURL in URLs.serverlisturl)
            {
                try
                {
                    Log.UrlCall("LIST CORE: Loading Server List from: " + serverListURL);
                    var wc = new WebClient();
                    var response = wc.DownloadString(serverListURL);
                    Log.UrlCall("LIST CORE: Loaded Server List from: " + serverListURL);

                    try
                    {
                        serverInfos.AddRange(
                            JsonConvert.DeserializeObject<List<JsonServerList>>(response));
                        ServerListStatus = "Loaded";
                        break;
                    }
                    catch (Exception error)
                    {
                        Log.Error("LIST CORE: Error occurred while deserializing Server List from [" + serverListURL + "]: " + error.Message);
                        ServerListStatus = "Error";
                    }
                }
                catch (Exception error)
                {
                    Log.Error("LIST CORE: Error occurred while loading Server List from [" + serverListURL + "]: " + error.Message);
                    ServerListStatus = "Error";
                }
            }

            if (File.Exists("servers.json"))
            {
                var fileItems = JsonConvert.DeserializeObject<List<JsonServerList>>(File.ReadAllText("servers.json")) ?? new List<JsonServerList>();

                if (fileItems.Count > 0)
                {
                    fileItems.Select(si =>
                    {
                        si.DistributionUrl = "";
                        si.DiscordPresenceKey = "";
                        si.Id = SHA.HashPassword($"{si.Name}:{si.Id}:{si.IpAddress}");
                        si.IsSpecial = false;
                        si.Category = "CUSTOM";

                        return si;
                    }).ToList().ForEach(si => serverInfos.Add(si));
                }
            }

            if (File.Exists("libOfflineServer.dll"))
            {
                serverInfos.Add(new JsonServerList
                {
                    Name = "Offline Built-In Server",
                    Category = "OFFLINE",
                    DiscordPresenceKey = "",
                    IsSpecial = false,
                    DistributionUrl = "",
                    IpAddress = "http://localhost:4416/sbrw/Engine.svc",
                    Id = "OFFLINE"
                });
            }

            if (Debugger.IsAttached)
            {
                serverInfos.Add(new JsonServerList
                {
                    Name = "Local Debug Server",
                    Category = "DEBUG",
                    DiscordPresenceKey = "",
                    IsSpecial = false,
                    DistributionUrl = "",
                    IpAddress = "http://localhost:8680",
                    Id = "DEV"
                });
            }

            /* Create Final Server List without Categories */
            foreach (JsonServerList NoCatList in serverInfos)
            {
                if (NoCategoryList.FindIndex(i => string.Equals(i.Name, NoCatList.Name)) == -1)
                {
                    NoCategoryList.Add(NoCatList);
                }
            }

            /* Create Rough Draft Server List with Categories */
            List<JsonServerList> RawList = new List<JsonServerList>();

            foreach (var serverItemGroup in serverInfos.GroupBy(s => s.Category))
            {
                if (RawList.FindIndex(i => string.Equals(i.Name, $"<GROUP>{serverItemGroup.Key} Servers")) == -1)
                {
                    RawList.Add(new JsonServerList
                    {
                        Id = $"__category-{serverItemGroup.Key}__",
                        Name = $"<GROUP>{serverItemGroup.Key} Servers",
                        IsSpecial = true
                    });
                }
                RawList.AddRange(serverItemGroup.ToList());
            }

            /* Create Final Server List with Categories */
            foreach (JsonServerList CList in RawList)
            {
                if (CleanList.FindIndex(i => string.Equals(i.Name, CList.Name)) == -1)
                {
                    CleanList.Add(CList);
                }
            }
        }

        /* Converts 2 Letter Country Code and Returns Full Country Name (In English) */
        public static string CountryName(string twoLetterCountryCode)
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (CultureInfo culture in cultures)
            {
                RegionInfo region = new RegionInfo(culture.LCID);
                if (region.TwoLetterISORegionName.ToUpper() == twoLetterCountryCode.ToUpper())
                {
                    return region.EnglishName;
                }
            }

            return "Unknown";
        }
    }
}
