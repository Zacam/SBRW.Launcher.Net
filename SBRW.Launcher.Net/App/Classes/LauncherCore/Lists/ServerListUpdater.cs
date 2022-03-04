﻿using SBRW.Launcher.App.Classes.LauncherCore.Global;
using SBRW.Launcher.App.Classes.LauncherCore.LauncherUpdater;
using SBRW.Launcher.App.Classes.LauncherCore.Logger;
using Newtonsoft.Json;
using SBRW.Launcher.Core.Cache;
using SBRW.Launcher.Core.Extension.Hash_;
using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.Core.Reference.Json_.Newtonsoft_;
using SBRW.Launcher.Core.Discord.RPC_;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using SBRW.Launcher.App.UI_Forms.Custom_Server_Screen;
using SBRW.Launcher.App.Classes.LauncherCore.APICheckers;

namespace SBRW.Launcher.App.Classes.LauncherCore.Lists
{
    public class ServerListUpdater
    {
        public static bool LoadedList { get; set; }
        public static List<Json_List_Server> NoCategoryList { get; set; } = new List<Json_List_Server>();
        public static List<Json_List_Server> NoCategoryList_CSO { get; set; } = new List<Json_List_Server>();
        public static List<Json_List_Server> CleanList { get; set; } = new List<Json_List_Server>();
        public static string CachedJSONList { get; set; } = string.Empty;

        public static void GetList()
        {
            LogToFileAddons.Parent_Log_Screen(2, "SERVER LIST CORE", "Creating Server List");
            Presence_Launcher.Status("Start Up", "Creating Server List");

            List<Json_List_Server> serverInfos = new List<Json_List_Server>();

            List<Json_List_Server> CustomServerInfos = new List<Json_List_Server>();

            try
            {
                serverInfos.AddRange(JsonConvert.DeserializeObject<List<Json_List_Server>>(CachedJSONList));
                LoadedList = true;

                if (VisualsAPIChecker.CarbonAPITwo())
                {
                    DateTime Time_Check = DateTime.Now.Date;
                    string Launcher_Data_Folder = Path.Combine("Launcher_Data", "JSON", "Lists");
                    string Time_Stamp = Path.Combine(Launcher_Data_Folder, "Time_Stamp.txt");

                    if (File.Exists(Time_Stamp))
                    {
                        try
                        {
                            Time_Check = DateTime.Parse(File.ReadLines(Time_Stamp).First()).Date;
                        }
                        catch
                        {

                        }
                        finally
                        {
                            GC.Collect();
                        }
                    }

                    if ((Time_Check < DateTime.Now.Date) || !File.Exists(Time_Stamp))
                    {
                        if (!Directory.Exists(Launcher_Data_Folder))
                        {
                            Directory.CreateDirectory(Launcher_Data_Folder);
                        }
                        
                        File.WriteAllText(Path.Combine(Launcher_Data_Folder, "Game_Servers.json"), CachedJSONList);
                        File.WriteAllText(Path.Combine(Launcher_Data_Folder, "Content_Delivery_Networks.json"), CDNListUpdater.CachedJSONList);
                        File.WriteAllText(Time_Stamp, DateTime.Now.ToString());
                    }
                }
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("SERVER LIST CORE", string.Empty, Error, string.Empty, true);

                if (Error.InnerException != null && !string.IsNullOrWhiteSpace(Error.InnerException.Message))
                {
                    LogToFileAddons.Parent_Log_Screen(5, "SERVER LIST CORE", Error.InnerException.Message, false, true);
                }
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(CachedJSONList))
                {
                    CachedJSONList = string.Empty;
                }

                GC.Collect();
            }

            if (File.Exists(Locations.LauncherCustomServers))
            {
                try
                {
                    List<Json_List_Server> fileItems = JsonConvert.DeserializeObject<List<Json_List_Server>>
                    (File.ReadAllText(Locations.LauncherCustomServers)) ?? new List<Json_List_Server>();

                    if (fileItems.Count > 0)
                    {
                        fileItems.Select(si =>
                        {
                            si.ID = string.IsNullOrWhiteSpace(si.ID) ? Hashes.Hash_String(1, $"{si.Name}:{si.ID}:{si.IPAddress}") : si.ID;
                            si.IsSpecial = false;
                            si.Category = string.IsNullOrWhiteSpace(si.Category) ? "CUSTOM" : si.Category;

                            return si;
                        }).ToList().ForEach(si => CustomServerInfos.Add(si));
                        serverInfos.AddRange(CustomServerInfos);
                        LoadedList = true;
                    }
                }
                catch (Exception Error)
                {
                    LogToFileAddons.OpenLog("SERVER LIST CORE", string.Empty, Error, string.Empty, true);

                    if (Error.InnerException != null && !string.IsNullOrWhiteSpace(Error.InnerException.Message))
                    {
                        LogToFileAddons.Parent_Log_Screen(5, "SERVER LIST CORE", Error.InnerException.Message, false, true);
                    }
                }
                finally
                {
                    GC.Collect();
                }
            }

            if (File.Exists("libOfflineServer.dll"))
            {
                serverInfos.Add(new Json_List_Server
                {
                    Name = "Offline Built-In Server",
                    Category = "OFFLINE",
                    IsSpecial = false,
                    IPAddress = "http://localhost:4416/sbrw/Engine.svc",
                    ID = "OFFLINE"
                });
            }

            if (Debugger.IsAttached)
            {
                serverInfos.Add(new Json_List_Server
                {
                    Name = "Local Debug Server",
                    Category = "DEBUG",
                    IsSpecial = false,
                    IPAddress = "http://localhost:8680",
                    ID = "DEV"
                });
            }

            try
            {
                if (serverInfos != null)
                {
                    if (serverInfos.Any())
                    {
                        /* Create Final Server List without Categories (All Servers) */
                        foreach (Json_List_Server NoCatList in serverInfos)
                        {
                            if (NoCategoryList.FindIndex(i => string.Equals(i.Name, NoCatList.Name)) == -1)
                            {
                                NoCategoryList.Add(NoCatList);
                            }
                        }

                        if (CustomServerInfos.Count >= 1)
                        {
                            /* Create Final Server List without Categories (Custom Servers Only) */
                            foreach (Json_List_Server NoCatList in CustomServerInfos)
                            {
                                if (NoCategoryList_CSO.FindIndex(i => string.Equals(i.Name, NoCatList.Name)) == -1)
                                {
                                    NoCategoryList_CSO.Add(NoCatList);
                                }
                            }
                        }

                        /* Create Rough Draft Server List with Categories */
                        List<Json_List_Server> RawList = new List<Json_List_Server>();

                        foreach (var serverItemGroup in serverInfos.GroupBy(s => s.Category))
                        {
                            if (RawList.FindIndex(i => string.Equals(i.Name, $"<GROUP>{serverItemGroup.Key} Servers")) == -1)
                            {
                                RawList.Add(new Json_List_Server
                                {
                                    ID = $"__category-{serverItemGroup.Key}__",
                                    Name = $"<GROUP>{serverItemGroup.Key} Servers",
                                    IsSpecial = true
                                });
                            }
                            RawList.AddRange(serverItemGroup.ToList());
                        }

                        /* Create Final Server List with Categories */
                        foreach (Json_List_Server CList in RawList)
                        {
                            if (CleanList.FindIndex(i => string.Equals(i.Name, CList.Name)) == -1)
                            {
                                CleanList.Add(CList);
                            }
                        }

                        LogToFileAddons.Parent_Log_Screen(3, "SERVER LIST CORE", "Done");
                    }
                    else
                    {
                        LogToFileAddons.Parent_Log_Screen(3, "SERVER LIST CORE", "Server List has no Elements");
                    }
                }
                else
                {
                    LogToFileAddons.Parent_Log_Screen(3, "SERVER LIST CORE", "Server List is NULL");
                }
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("SERVER LIST CORE", string.Empty, Error, string.Empty, true);

                if (Error.InnerException != null && !string.IsNullOrWhiteSpace(Error.InnerException.Message))
                {
                    LogToFileAddons.Parent_Log_Screen(5, "SERVER LIST CORE", Error.InnerException.Message, false, true);
                }
            }
            finally
            {
                GC.Collect();
            }

            LogToFileAddons.Parent_Log_Screen(1, "LAUNCHER UPDATER", "Moved to Function");
            /* (Start Process) Check If Updater Exists or Requires an Update */
            UpdaterExecutable.Check();
        }

        /* Converts 2 Letter Country Code and Returns Full Country Name (In English) */
        public static string CountryName(string twoLetterCountryCode)
        {
            try
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
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("COUNTRYNAME", string.Empty, Error, string.Empty, true);

                if (Error.InnerException != null && !string.IsNullOrWhiteSpace(Error.InnerException.Message))
                {
                    LogToFileAddons.Parent_Log_Screen(5, "COUNTRYNAME", Error.InnerException.Message, false, true);
                }
            }
            finally
            {
                GC.Collect();
            }

            return "Unknown";
        }

        /* Server Name */
        /** Returns a Server Name by first checking if the Server has provided one
         *  if so use that, otherwise it will see if the ServerList has provide one
         *  if not then it will see if its a custom server, which will provide "Custom"
         *  otherwise it will be "uknown" **/
        public static string ServerName(string State)
        {
            try
            {
                if (Launcher_Value.Launcher_Select_Server_JSON != null &&
                    !string.IsNullOrWhiteSpace(Launcher_Value.Launcher_Select_Server_JSON.Server_Name))
                {
                    return Launcher_Value.Launcher_Select_Server_JSON.Server_Name;
                }
                else if (Launcher_Value.Launcher_Select_Server_Data != null &&
                    !string.IsNullOrWhiteSpace(Launcher_Value.Launcher_Select_Server_Data.Name))
                {
                    return Launcher_Value.Launcher_Select_Server_Data.Name;
                }
                else
                {
                    switch (State)
                    {
                        case "Register":
                        case "RPC":
                            return "Custom Server";
                        case "Settings":
                            return "The Selected Server";
                        case "Select Server":
                            if (Screen_Custom_Server.ServerJsonData != null &&
                                !string.IsNullOrWhiteSpace(Screen_Custom_Server.ServerJsonData.Server_Name))
                            {
                                return Screen_Custom_Server.ServerJsonData.Server_Name;
                            }
                            else
                            {
                                return Screen_Custom_Server.ServerName;
                            }
                        default:
                            return "Custom";
                    }
                }
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("Server Name Provider", string.Empty, Error, string.Empty, true);
                return "Unknown";
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}