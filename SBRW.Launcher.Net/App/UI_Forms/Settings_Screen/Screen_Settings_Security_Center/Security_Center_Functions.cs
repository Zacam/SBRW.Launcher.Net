#if (DEBUG || RELEASE)
using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.Core.Extra.File_.Save_;
using SBRW.Launcher.Core.Required.System.Windows_;
using SBRW.Launcher.RunTime.InsiderKit;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using WindowsFirewallHelper.Exceptions;
using WindowsFirewallHelper.FirewallRules;
using WindowsFirewallHelper;

namespace SBRW.Launcher.App.UI_Forms.Settings_Screen
{
    partial class Screen_Settings
    {
        /// <summary>Checks WMI Query on if Windows Defender is Enabled</summary>
        /// <param name="Query">Query a Specific Collection</param>
        /// <returns><code>True or False</code></returns>
        private bool GetDefenderStatus(string Query)
        {
            if (Product_Version.GetWindowsNumber() >= 10)
            {
                ManagementObjectSearcher? ObjectPath = null;
                ManagementObjectCollection? ObjectCollection = null;

                try
                {
                    ObjectPath = new ManagementObjectSearcher(Path.Combine("root", "Microsoft", "Windows", "Defender"),
                        "SELECT * FROM MSFT_MpComputerStatus");
                    ObjectCollection = ObjectPath.Get();

                    foreach (ManagementBaseObject SearchBase in ObjectCollection)
                    {
                        if (ObjectCollection != null)
                        {
                            if (bool.TryParse(SearchBase.Properties[Query].Value.ToString(), out bool TrueOrFalse))
                            {
                                return (bool)SearchBase.Properties[Query].Value;
                            }
                        }
                    }
                }
                catch (ManagementException Error)
                {
                    LogToFileAddons.OpenLog("Windows Defender Status [M.E.]", string.Empty, Error, string.Empty, true);
                }
                catch (COMException Error)
                {
                    LogToFileAddons.OpenLog("Windows Defender Status [C.O.M.]", string.Empty, Error, string.Empty, true);
                }
                catch (Exception Error)
                {
                    LogToFileAddons.OpenLog("Windows Defender Status", string.Empty, Error, string.Empty, true);
                }
                finally
                {
                    ObjectPath?.Dispose();
                    ObjectCollection?.Dispose();
                }
            }

            return false;
        }
        /// <summary>Checks Windows Defender on if it's Enabled or Disabled by the User or Third-Party Program</summary>
        /// <remarks>Doesn't checks If the Service is Disabled or a Third-Party Program reports an Incorrect Value</remarks>
        /// <returns><code>True or False</code></returns>
        public bool Defender()
        {
            return GetDefenderStatus("AntivirusEnabled") &&
                    GetDefenderStatus("AntispywareEnabled") &&
                    GetDefenderStatus("RealTimeProtectionEnabled");
        }
        /// <summary>Windows Defender: Checks Defender's Current Exclusion List</summary>
        /// <returns>String-Array of Exclusions</returns>
        private string[] ExclusionCheck()
        {
            ManagementObjectSearcher? ObjectPath = null;
            ManagementObjectCollection? ObjectCollection = null;

            try
            {
                ObjectPath = new ManagementObjectSearcher(Path.Combine("root", "Microsoft", "Windows", "Defender"),
                    "SELECT * FROM MSFT_MpPreference");
                ObjectCollection = ObjectPath.Get();

                foreach (ManagementBaseObject SearchBase in ObjectCollection)
                {
                    if (ObjectCollection != null)
                    {
                        return (string[])SearchBase.Properties["ExclusionPath"].Value;
                    }
                }
            }
            catch (ManagementException Error)
            {
                LogToFileAddons.OpenLog("Windows Defender Exclusion Path Check [M.E.]", string.Empty, Error, string.Empty, true);
            }
            catch (COMException Error)
            {
                LogToFileAddons.OpenLog("Windows Defender Exclusion Path Check [C.O.M.]", string.Empty, Error, string.Empty, true);
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("Windows Defender Exclusion Path Check", string.Empty, Error, string.Empty, true);
            }
            finally
            {
                ObjectPath?.Dispose();
                ObjectCollection?.Dispose();
            }

            return Array.Empty<string>();
        }
        /// <summary>
        /// Finds a Defender Exclusion in Defenders "Database"
        /// </summary>
        /// <param name="FilePath">Enter file Path</param>
        /// <returns><code>True or False</code></returns>
        private bool ExclusionExist(string FilePath)
        {
            if (ExclusionCheck() != null)
            {
                return ExclusionCheck().Any(FilePath.Contains);
                /*
                foreach (string ExistingPaths in ExclusionCheck())
                {
                    if (ExistingPaths == FilePath)
                    {
                        return true;
                    }
                }

                return false; */
            }
            else
            {
                return false;
            }
        }
        /// <summary>Windows Defender: Add an Exclusion</summary>
        /// <param name="AppName">Enter the name of the Application</param>
        /// <param name="AppPath">Enter the Application Folder</param>
        /// <returns><code>True or False</code></returns>
        private bool AddApplicationExclusion(string AppName, string AppPath)
        {
            bool Completed = false;
            try
            {
                if (!ExclusionExist(AppPath))
                {
                    /* Remove current Exclusion and Add new location for Exclusion (Game Files Only!) */
                    using (PowerShell AddScript = PowerShell.Create())
                    {
                        AddScript.AddScript($"Add-MpPreference -ExclusionPath \"{AppPath}\"");
                        AddScript.Invoke();
                    }

                    Completed = true;
                    Log.Completed("Windows Defender: ".ToUpper() + "Folder is now Excluded. -> " + AppPath);
                }
                else { Log.Completed("WINDOWS FIREWALL: " + AppName + " Rule is already Added"); Completed = true; }
            }
            catch (COMException Error)
            {
                LogToFileAddons.OpenLog("WINDOWS FIREWALL Add Script [C.O.M.]", string.Empty, Error, string.Empty, true);
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("WINDOWS FIREWALL Add Script", string.Empty, Error, string.Empty, true);
            }

            return Completed;
        }
        /// <summary>Windows Defender: Removes an Exclusion</summary>
        /// <param name="AppName">Enter the name of the Application</param>
        /// <param name="AppPath">Enter the Application Folder</param>
        /// <returns><code>True or False</code></returns>
        private bool RemoveExclusion(string AppName, string AppPath)
        {
            bool Completed = false;
            try
            {
                if (ExclusionExist(AppPath))
                {
                    /* Remove current Exclusion and Add new location for Exclusion (Game Files Only!) */
                    using (PowerShell RemovalScript = PowerShell.Create())
                    {
                        RemovalScript.AddScript($"Remove-MpPreference -ExclusionPath \"{AppPath}\"");
                        RemovalScript.Invoke();
                    }

                    Completed = true;
                    Log.Completed("Windows Defender: ".ToUpper() + "Folder is no longer Excluded. -> " + AppPath);
                }
                else { Log.Completed("WINDOWS FIREWALL: " + AppName + " Rule is already Removed"); Completed = true; }
            }
            catch (COMException Error)
            {
                LogToFileAddons.OpenLog("WINDOWS FIREWALL Removal Script [C.O.M.]", string.Empty, Error, string.Empty, true);
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("WINDOWS FIREWALL Removal Script", string.Empty, Error, string.Empty, true);
            }

            return Completed;
        }
        /// <summary>
        /// Checks the Firewall API Version Dynamically
        /// </summary>
        /// <returns>Firewall API Version</returns>
        private FirewallAPIVersion FirewallAPI()
        {
            try
            {
                return FirewallManager.Version;
            }
            catch
            {
                return FirewallAPIVersion.None;
            }
        }
        /// <summary>
        /// Checks the Firewall API Version against Versions that isn't supported
        /// </summary>
        /// <returns><code>True or False</code></returns>
        private bool FirewallSupported()
        {
            return FirewallAPI() != FirewallAPIVersion.None;
        }
        /// <summary>
        /// Checks Windows Firewall on if it's Enabled or Disabled by the User or Third-Party Program
        /// </summary>
        /// <remarks>Checks the Firewall Service at the same time</remarks>
        /// <returns><code>True or False</code></returns>
        private bool Firewall()
        {
            try
            {
                if (bool.TryParse(FirewallManager.IsServiceRunning.ToString(), out bool Service_Result) && Service_Result)
                {
                    if (bool.TryParse(FirewallManager.Instance.GetActiveProfile().IsActive.ToString(), out bool Profile_Result))
                    {
                        return Profile_Result;
                    }
                    /* @DavidCarbon Remember to Debug This!
                    Type NetFwMgrType = Type.GetTypeFromProgID("HNetCfg.FwMgr", true);
                    INetFwMgr Mana = (INetFwMgr)Activator.CreateInstance(NetFwMgrType);

                    if (bool.TryParse(Mana.LocalPolicy.CurrentProfile.FirewallEnabled.ToString(), out bool Results))
                    {
                        return Mana.LocalPolicy.CurrentProfile.FirewallEnabled;
                    }
                    */
                }
            }
            catch (COMException Error)
            {
                LogToFileAddons.OpenLog("WINDOWS FIREWALL Check", string.Empty, Error, string.Empty, true);
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("WINDOWS FIREWALL Check", string.Empty, Error, string.Empty, true);
            }

            return false;
        }
        /// <summary>
        /// Used to find a Application Rule on the system by searching Firewall "Database"
        /// </summary>
        /// <param name="Mode">Used to Specifiy how to find a Rule. Enter "Name" or "Path"</param>
        /// <param name="AppName">Used to Specifiy how to find a Rule. Provide the name of Application</param>
        /// <param name="AppPath">Used to Specifiy how to find a Rule. Provide Application Path</param>
        /// <returns>An Array of Rules</returns>
        private IEnumerable<IFirewallRule> FindRules(string Mode, string AppName, string AppPath)
        {
            try
            {
                if (Firewall() && FirewallSupported() && (FirewallAPI() != FirewallAPIVersion.None))
                {
                    if (FirewallManager.Instance.Rules.Count != 0)
                    {
                        if (Mode == "Name")
                        {
                            return FirewallManager.Instance.Rules.Where(r =>
                            string.Equals(r.Name, AppName, StringComparison.OrdinalIgnoreCase)).ToArray();
                        }
                        else if (Mode == "Path")
                        {
                            return FirewallManager.Instance.Rules.Where(r =>
                            string.Equals(r.ApplicationName, AppPath, StringComparison.OrdinalIgnoreCase)).ToArray();
                        }
                    }
                }
            }
            catch (NotSupportedException Error)
            {
                LogToFileAddons.OpenLog("WINDOWS FIREWALL [Not Supported]", string.Empty, Error, string.Empty, true);
            }
            catch (COMException Error)
            {
                LogToFileAddons.OpenLog("WINDOWS FIREWALL [COM]", string.Empty, Error, string.Empty, true);
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("WINDOWS FIREWALL", string.Empty, Error, string.Empty, true);
            }

            return Enumerable.Empty<IFirewallRule>();
        }
        /// <summary>
        /// Finds a Firewall Rule and Attempts to Remove it. 
        /// If the Rule List is empty or Encounters an Issue, it will be False.
        /// If the rule list succeeds to remove a Rule, it will be True
        /// </summary>
        /// <param name="Mode"> Used in Find Rules Helper Function. Choose and Type "Name" or "Path"</param>
        /// <param name="AppName">Used in Find Rules Helper Function. Enter name of Application</param>
        /// <param name="AppPath">Used in Find Rules Helper Function. Enter file Path</param>
        /// <param name="F_LogNote">Used to Log which additional Details</param>
        /// <returns><code>True or False</code></returns>
        private bool RemoveRules(string Mode, string AppName, string AppPath, string F_LogNote)
        {
            try
            {
                var myRule = FindRules(Mode, AppName, AppPath).ToArray();

                if (myRule != null)
                {
                    if (Enumerable.Any(myRule))
                    {
                        int ErrorsRate = 0;

                        foreach (var rule in myRule)
                        {
                            try
                            {
                                FirewallManager.Instance.Rules.Remove(rule);
                                Log.Warning("WINDOWS FIREWALL: Removed " + AppName + " {" + F_LogNote + "} From Firewall!");
                            }
                            catch (Exception Error)
                            {
                                LogToFileAddons.OpenLog("WINDOWS FIREWALL", string.Empty, Error, string.Empty, true);
                                ErrorsRate++;
                            }
                        }

                        if (ErrorsRate == 0) { return true; }
                    }
                }
            }
            catch { }

            return false;
        }
        /// <summary>
        /// Finds a Firewall Rule in Firewall "Database"
        /// </summary>
        /// <param name="AppName">Used in Find Rules Helper Function. Enter name of Application</param>
        /// <param name="AppPath">Used in Find Rules Helper Function. Enter file Path</param>
        /// <returns><code>True or False</code></returns>
        private bool RuleExist(string Mode, string AppName, string AppPath)
        {
            if (FindRules(Mode, AppName, AppPath) != null)
            {
                foreach (IFirewallRule Single_Rule in FindRules(Mode, AppName, AppPath))
                {
                    if (Single_Rule != null)
                    {
                        if (BuildBeta.Allowed() || BuildDevelopment.Allowed())
                        {
                            Log.Ignore("---Firewall---");
                            Log.Debug("Name: " + Single_Rule.Name);
                            Log.Debug("Friendly Name: " + Single_Rule.FriendlyName);
                            Log.Debug("Application Name: " + Single_Rule.ApplicationName);
                            Log.Debug("Direction: " + Single_Rule.Direction);
                            Log.Debug("Scope: " + Single_Rule.Scope);
                            Log.Debug("Enabled: " + Single_Rule.IsEnable);
                            Log.Ignore("------End------");
                        }

                        if (Single_Rule.Name.Equals(AppName) && Single_Rule.ApplicationName.Equals(AppPath))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            else
            {
                return false;
            }
        }
        /// <summary>Windows Firewall: Adds an Exclusion</summary>
        /// <param name="AppName">Enter the name of the Application</param>
        /// <param name="AppPath">Enter the Application Path (Must include exe)</param>
        /// <param name="GroupKey">Sets the rule grouping string</param>
        /// <param name="C_Description">Sets the description string of this rule</param>
        /// <param name="C_Direction">Data direction in which this rule applies to</param>
        /// <param name="C_Protocol">Sets the protocol that the rule applies to</param>
        /// <param name="F_LogNote">Notes for Logging</param>
        /// <returns><code>True or False</code></returns>
        private bool AddApplicationRule(string AppName, string AppPath, string GroupKey, string C_Description,
                            FirewallDirection C_Direction, FirewallProtocol C_Protocol, string F_LogNote)
        {
            bool Completed = false;
            try
            {
                FirewallAPIVersion CachedAPIVersion = FirewallAPI();
                if (CachedAPIVersion == FirewallAPIVersion.None)
                {
                    Log.Warning("WINDOWS FIREWALL: API Version not Supported");
                }
                else if (CachedAPIVersion != FirewallAPIVersion.FirewallLegacy)
                {
                    Log.Info("WINDOWS FIREWALL: Supported Firewall [WASRuleWin8]");
                    FirewallWASRuleWin8 Rule = new FirewallWASRuleWin8(AppPath, FirewallAction.Allow, C_Direction,
                        FirewallProfiles.Domain | FirewallProfiles.Private | FirewallProfiles.Public)
                    {
                        ApplicationName = AppPath,
                        Name = AppName,
                        Grouping = GroupKey,
                        Description = C_Description,
                        NetworkInterfaceTypes = NetworkInterfaceTypes.Lan | NetworkInterfaceTypes.RemoteAccess | NetworkInterfaceTypes.Wireless,
                        Protocol = C_Protocol
                    };

                    if (C_Direction == FirewallDirection.Inbound)
                    {
                        Rule.EdgeTraversalOptions = EdgeTraversalAction.Allow;
                    }

                    FirewallManager.Instance.Rules.Add(Rule);
                    Log.Completed("WINDOWS FIREWALL: Finished Adding " + AppName + " to Firewall! {" + F_LogNote + "}");
                    Completed = true;
                }
                else if (CachedAPIVersion == FirewallAPIVersion.FirewallLegacy)
                {
                    Log.Info("WINDOWS FIREWALL: Supported Firewall [LegacyStandard]");
                    IFirewallRule Rule = FirewallManager.Instance.CreateApplicationRule(
                        FirewallProfiles.Domain | FirewallProfiles.Private | FirewallProfiles.Public,
                        AppName, FirewallAction.Allow, AppPath, C_Protocol);
                    Rule.Direction = C_Direction;

                    FirewallManager.Instance.Rules.Add(Rule);
                    Log.Completed("WINDOWS FIREWALL: Finished Adding " + AppName + " to Firewall! {" + F_LogNote + "}");
                    Completed = true;
                }
                else
                {
                    Log.Completed("WINDOWS FIREWALL: " + AppName + " Rule was not added due to Firewall API Version {" + F_LogNote + "}");
                }
            }
            catch (FirewallWASNotSupportedException Error)
            {
                LogToFileAddons.OpenLog("WINDOWS FIREWALL [F.WAS.N.S.E]", string.Empty, Error, string.Empty, true);
            }
            catch (COMException Error)
            {
                LogToFileAddons.OpenLog("WINDOWS FIREWALL [C.O.M]", string.Empty, Error, string.Empty, true);
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("WINDOWS FIREWALL", string.Empty, Error, string.Empty, true);
            }

            return Completed;
        }
        /// <summary>
        /// Checks if Permissions is Set
        /// <code>"0" Checks File Permission Only</code>
        /// <code>"1" Checks Folder Permission Only</code>
        /// </summary>
        /// <param name="ModeType">
        /// <code>"0" Checks File Permission Only</code>
        /// <code>"1" Checks Folder Permission Only</code>
        /// </param>
        /// <param name="LocationPath">Enter File or Folder Path</param>
        /// <returns><code>True or False</code></returns>
        private bool CheckPermissionAccess(int ModeType, string LocationPath)
        {
            bool Completed = false;

            try
            {
                if (ModeType >= 0 && ModeType <= 1 && !string.IsNullOrWhiteSpace(LocationPath))
                {
                    SecurityIdentifier Everyone_Question_Mark = new SecurityIdentifier(WellKnownSidType.WorldSid, null);

                    switch (ModeType)
                    {
                        /* Checks File Permissions */
                        case 0:
#if NETFRAMEWORK
                            FileSecurity CoreFileSecurity = File.GetAccessControl(LocationPath);
#else
                            FileSecurity CoreFileSecurity = new FileInfo(LocationPath).GetAccessControl();
#endif
                            AuthorizationRuleCollection AccessRuleCollection_File =
                                CoreFileSecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));

                            foreach (FileSystemAccessRule RuleThatIsSet in AccessRuleCollection_File)
                            {
                                if (RuleThatIsSet.IdentityReference.Value == Everyone_Question_Mark.Value &&
                                    RuleThatIsSet.AccessControlType == AccessControlType.Allow &&
                                    (RuleThatIsSet.FileSystemRights & FileSystemRights.Write) == FileSystemRights.Write)
                                {
                                    Completed = true;
                                    Log.Completed("FILE PERMISSION: [" + LocationPath + "] Is permission set? -> Yes");
                                }
                            }
                            break;
                        /* Checks Folder Permissions */
                        case 1:
                            DirectoryInfo FolderInfos = new DirectoryInfo(LocationPath);
                            DirectorySecurity CoreFolderSecurity = FolderInfos.GetAccessControl();
                            AuthorizationRuleCollection AccessRuleCollection_Folder =
                                CoreFolderSecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));

                            foreach (FileSystemAccessRule RuleThatIsSet in AccessRuleCollection_Folder)
                            {
                                if (RuleThatIsSet.IdentityReference.Value == Everyone_Question_Mark.Value &&
                                    RuleThatIsSet.AccessControlType == AccessControlType.Allow &&
                                    (RuleThatIsSet.FileSystemRights & FileSystemRights.Write) == FileSystemRights.Write)
                                {
                                    Completed = true;
                                    Log.Completed("FOLDER PERMISSION: [" + LocationPath + "] Is permission set? -> Yes");
                                }
                            }
                            break;
                        default:
                            Completed = false;
                            break;
                    }
                }
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("PERMISSION Checker", string.Empty, Error, string.Empty, true);
            }

            return Completed;
        }
        /// <summary>
        /// Attempts to Set Read and Write Access Permissions for the User
        /// <code>"0" Sets File Permission Only</code>
        /// <code>"1" Sets Folder Permission Only</code>
        /// </summary>
        /// <param name="ModeType">
        /// <code>"0" Sets File Permission Only</code>
        /// <code>"1" Sets Folder Permission Only</code>
        /// </param>
        /// <param name="LocationPath">Enter File or Folder Path</param>
        /// <returns><code>True or False</code></returns>
        private bool GiveEveryoneReadWriteAccess(int ModeType, string LocationPath)
        {
            bool Completed = false;

            try
            {
                if (ModeType >= 0 && ModeType <= 1 && !string.IsNullOrWhiteSpace(LocationPath))
                {
                    bool IsPermissionAlreadySet = CheckPermissionAccess(ModeType, LocationPath);

                    if (!IsPermissionAlreadySet)
                    {
                        SecurityIdentifier Everyone_Question_Mark = new SecurityIdentifier(WellKnownSidType.WorldSid, null);

                        switch (ModeType)
                        {
                            /* Sets File Permissions */
                            case 0:
                                FileSystemAccessRule accessRule = new FileSystemAccessRule(Everyone_Question_Mark, FileSystemRights.FullControl,
                                    InheritanceFlags.None, PropagationFlags.NoPropagateInherit, AccessControlType.Allow);
#if NETFRAMEWORK
                                FileSecurity CoreFileSecurity = File.GetAccessControl(LocationPath);
#else
                                FileSecurity CoreFileSecurity = new FileInfo(LocationPath).GetAccessControl();
#endif
                                CoreFileSecurity.AddAccessRule(accessRule);
#if NETFRAMEWORK
                                File.SetAccessControl(LocationPath, CoreFileSecurity);
#else
                                new FileInfo(LocationPath).SetAccessControl(CoreFileSecurity);
#endif
                                Completed = true;
                                break;
                            /* Sets Folder Permissions */
                            case 1:
                                DirectoryInfo Info = new DirectoryInfo(LocationPath);
                                DirectorySecurity CoreFolderSecurity = Info.GetAccessControl();
                                CoreFolderSecurity.AddAccessRule(new FileSystemAccessRule(Everyone_Question_Mark, FileSystemRights.FullControl,
                                    InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit,
                                    AccessControlType.Allow));
#if NETFRAMEWORK
                                Directory.SetAccessControl(LocationPath, CoreFolderSecurity);
#else
                                new DirectoryInfo(LocationPath).SetAccessControl(CoreFolderSecurity);
#endif
                                Completed = true;
                                break;
                            default:
                                Completed = false;
                                break;
                        }
                    }
                    else { Completed = true; }
                }
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("PERMISSION Setter", string.Empty, Error, string.Empty, true);
            }

            return Completed;
        }
        /// <summary>Function Splitter For Firewall and Defender Checks</summary>
        /// <param name="ModeType">Range 0-5 Sets the Check Status.
        /// <code>"0" Sets Launcher Path</code>
        /// <code>"1" Sets Updater Path</code>
        /// <code>"2" Sets Current/New Game Path</code>
        /// <code>"3" Sets Old Game Path</code>
        /// <code>"4" Returns the Firewall Status in a form of a Boolean</code>
        /// <code>"5" Returns the Defender Status in a form of a Boolean</code>
        /// <code>"6" Returns the File/Folder Permission in a form of a Boolean</code>
        /// </param>
        /// <param name="ModeAPI">Range 0-2 Sets the Function Status. Each Function Returns a Boolean on if it was completed.
        /// <code>"0" Adds Rule</code>
        /// <code>"1" Removes Rule</code>
        /// <code>"2" Checks if Rule Exists</code>
        /// <code>"3" Adds Exclusion</code>
        /// <code>"4" Removes Exclusion</code>
        /// <code>"5" Checks if Exclusion Exists</code>
        /// <code>"6" Sets File/Folder Permission</code>
        /// </param>
        /// <returns><code>True or False</code></returns>
        private bool DataBase(int ModeType, int ModeAPI)
        {
            string AppName;
            string AppPath;
            string GroupKey;
            string Description;

            switch (ModeType)
            {
                /* Launcher */
                case 0:
                    AppName = "SBRW - Game Launcher";
                    GroupKey = "Game Launcher for Windows";
                    Description = "Soapbox Race World";
                    if (ModeAPI >= 0 && ModeAPI <= 2)
                    {
                        AppPath = Path.Combine(Locations.LauncherFolder, Locations.NameLauncher);
                    }
                    else { AppPath = Locations.LauncherFolder; }
                    break;
                /* Updater */
                case 1:
                    AppName = "SBRW - Game Launcher Updater";
                    AppPath = Path.Combine(Locations.LauncherFolder, Locations.NameUpdater);
                    GroupKey = "Game Launcher for Windows";
                    Description = "Soapbox Race World";
                    break;
                /* Current/New Game Files [2] Old Game Files [3]*/
                case 2:
                case 3:
                    AppName = "SBRW - Game";
                    GroupKey = "Need for Speed: World";
                    Description = GroupKey;
                    if (ModeAPI >= 0 && ModeAPI <= 2)
                    {
                        if (ModeType == 2)
                        {
                            AppPath = Path.Combine(Save_Settings.Live_Data.Game_Path, "nfsw.exe");
                        }
                        else
                        {
                            AppPath = Path.Combine(Save_Settings.Live_Data.Game_Path_Old, "nfsw.exe");
                        }
                    }
                    else
                    {
                        if (ModeType == 2)
                        {
                            AppPath = Save_Settings.Live_Data.Game_Path;
                        }
                        else
                        {
                            AppPath = Save_Settings.Live_Data.Game_Path_Old;
                        }
                    }
                    break;
                case 4:
                    return Firewall();
                case 5:
                    return Defender();
                case 6:
                    return CheckPermissionAccess(1, Locations.LauncherFolder) &&
                            CheckPermissionAccess(1, Save_Settings.Live_Data.Game_Path);
                default:
                    return false;
            }

            if (ModeType >= 0 && ModeType <= 3)
            {
                switch (ModeAPI)
                {
                    /* Firewall Rule Add */
                    case 0:
                        /* If File Path Exits, but not with our Set Name. Then Remove the rule within the set path. */
                        if (RuleExist("Path", AppName, AppPath) && !RuleExist("Name", AppName, AppPath))
                        {
                            /* Inbound & Outbound */
                            RemoveRules("Path", "Non-" + AppName, AppPath, "Path Match");
                        }
                        /* If both path and set name is not set in firewall, go ahead and add it normally */
                        if (!RuleExist("Path", AppName, AppPath) && !RuleExist("Name", AppName, AppPath))
                        {
                            /* Inbound */
                            AddApplicationRule(AppName, AppPath, GroupKey, Description,
                                FirewallDirection.Inbound, FirewallProtocol.Any, "Inbound");
                            /* Outbound */
                            return AddApplicationRule(AppName, AppPath, GroupKey, Description,
                                FirewallDirection.Outbound, FirewallProtocol.Any, "Outbound");
                        }
                        /* If both path and name are set and match, then its already set */
                        else if (RuleExist("Path", AppName, AppPath) && RuleExist("Name", AppName, AppPath))
                        {
                            Log.Completed("WINDOWS FIREWALL: " + AppName + " Rule is already Added");
                            return true;
                        }
                        /* Proabably a code conditional problem. Developer must check the code to verify its issue */
                        else
                        {
                            Log.Completed("WINDOWS FIREWALL: " + AppName + " Rule wasn't added due to a Unknown Issue"); return false;
                        }
                    /* Firewall Rule Removal */
                    case 1:
                        if (RuleExist("Path", AppName, AppPath) && !RuleExist("Name", AppName, AppPath))
                        {
                            /* Inbound & Outbound */
                            RemoveRules("Path", "Non-" + AppName, AppPath, "Path Match");
                        }

                        if (RuleExist("Path", AppName, AppPath) && RuleExist("Name", AppName, AppPath))
                        {
                            return RemoveRules("Path", AppName, AppPath, "Path Match");
                        }
                        else
                        {
                            return false;
                        }
                    /* Firewall Rule Check (Exists) */
                    case 2:
                        return RuleExist("Path", AppName, AppPath) && RuleExist("Name", AppName, AppPath);
                    /* Defender Exclusion Add */
                    case 3:
                        return AddApplicationExclusion(AppName, AppPath);
                    /* Defender Exclusion Removal */
                    case 4:
                        return RemoveExclusion(AppName, AppPath);
                    /* Defender Exclusion Check (Exists) */
                    case 5:
                        return ExclusionExist(AppPath);
                    case 6:
                        return GiveEveryoneReadWriteAccess(1, Locations.LauncherFolder) &&
                            GiveEveryoneReadWriteAccess(1, Save_Settings.Live_Data.Game_Path);
                    default:
                        return false;
                }
            }
            else { return false; }
        }
        /// <summary>
        /// Used to Enable Buttons with only Booleans
        /// </summary>
        /// <param name="ModeType">Range 0-5 Sets the Check Status.
        /// <code>"0" Sets Launcher Path</code>
        /// <code>"1" Sets Updater Path</code>
        /// <code>"2" Sets Current/New Game Path</code>
        /// <code>"3" Sets Old Game Path</code>
        /// <code>"4" Returns the Firewall Status in a form of a Boolean</code>
        /// <code>"5" Returns the Defender Status in a form of a Boolean</code>
        /// <code>"6" Returns the File/Folder Permission in a form of a Boolean</code>
        /// </param>
        /// <param name="ModeAPI">Range 0-2 Sets the Function Status. Each Function Returns a Boolean on if it was completed.
        /// <code>"0" Adds Rule</code>
        /// <code>"1" Removes Rule</code>
        /// <code>"2" Checks if Rule Exists</code>
        /// <code>"3" Adds Exclusion</code>
        /// <code>"4" Removes Exclusion</code>
        /// <code>"5" Checks if Exclusion Exists</code>
        /// <code>"6" Sets File/Folder Permission</code>
        /// </param>
        /// <remarks>
        /// <b>ModeType:</b>
        /// <code>"0" Sets Launcher Path</code>
        /// <code>"1" Sets Updater Path</code>
        /// <code>"2" Sets Current/New Game Path</code>
        /// <code>"3" Sets Old Game Path</code>
        /// <code>"4" Returns the Firewall Status in a form of a Boolean</code>
        /// <code>"5" Returns the Defender Status in a form of a Boolean</code>
        /// <code>"6" Returns the File/Folder Permission in a form of a Boolean</code>
        /// <b>ModeAPI:</b>
        /// <code>"0" Adds Rule</code>
        /// <code>"1" Removes Rule</code>
        /// <code>"2" Checks if Rule Exists</code>
        /// <code>"3" Adds Exclusion</code>
        /// <code>"4" Removes Exclusion</code>
        /// <code>"5" Checks if Exclusion Exists</code>
        /// <code>"6" Sets File/Folder Permission</code> 
        /// </remarks>
        /// <returns><code>True or False</code></returns>
        private bool ButtonEnabler(int ModeType, int ModeAPI)
        {
            try
            {
                return DataBase(ModeType, ModeAPI);
            }
            catch
            {
                return false;
            }
        }
    }
}
#endif