﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using GameLauncher.App.Classes.LauncherCore.Client.Web;
using GameLauncher.App.Classes.LauncherCore.Global;
using GameLauncher.App.Classes.LauncherCore.Logger;
using GameLauncher.App.Classes.LauncherCore.RPC;
using GameLauncher.App.Classes.SystemPlatform.Components;
using GameLauncher.App.Classes.SystemPlatform.Linux;
using Microsoft.Win32;

// based on https://github.com/bitbeans/RedistributableChecker/blob/master/RedistributableChecker/RedistributablePackage.cs
namespace GameLauncher.App.Classes.SystemPlatform.Windows
{
    /// <summary>
    /// Microsoft Visual C++ Redistributable Package Versions
    /// </summary>
    public enum RedistributablePackageVersion
    {
        VC2015to2019x86,
        VC2015to2019x64,
    };

    /// <summary>
    ///	Class to detect installed Microsoft Redistributable Packages.
    /// </summary>
    /// <see cref="//https://stackoverflow.com/questions/12206314/detect-if-visual-c-redistributable-for-visual-studio-2012-is-installed"/>
    public static class RedistributablePackage
    {
        private static RegistryKey sk = null;
        private static string InstalledVersion;
        /// <summary>
        /// Check if a Microsoft Redistributable Package is installed.
        /// </summary>
        /// <param name="redistributableVersion">The package version to detect.</param>
        /// <returns><c>true</c> if the package is installed, otherwise <c>false</c></returns>
        public static bool IsInstalled(RedistributablePackageVersion redistributableVersion)
        {
            {
                switch (redistributableVersion)
                {
                    case RedistributablePackageVersion.VC2015to2019x86:
                    case RedistributablePackageVersion.VC2015to2019x64:
                        try
                        {
                            string subKey = Path.Combine("SOFTWARE", "Microsoft", "VisualStudio", "14.0", "VC", "Runtimes",
                                (redistributableVersion == RedistributablePackageVersion.VC2015to2019x86) ? "x86" : "x64");

                            sk = Registry.LocalMachine.OpenSubKey(subKey, false);

                            if (sk != null)
                            {
                                InstalledVersion = sk.GetValue("Version").ToString();

                                if (!string.IsNullOrWhiteSpace(InstalledVersion))
                                {
                                    if (InstalledVersion.StartsWith("v"))
                                    {
                                        char[] charsToTrim = { 'v' };
                                        InstalledVersion = InstalledVersion.Trim(charsToTrim);
                                    }

                                    if (InstalledVersion.CompareTo("14.20") >= 0)
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        catch (Exception Error)
                        {
                            LogToFileAddons.OpenLog("Redistributable Package", null, Error, null, true);
                            return false;
                        }
                        finally
                        {
                            if (InstalledVersion != null)
                            {
                                InstalledVersion = null;
                            }
                            if (sk != null)
                            {
                                sk.Close();
                                sk.Dispose();
                            }
                        }
                    default:
                        return false;
                }

            }
        }
    }

    class Redistributable
    {
        public static bool ErrorFree = true;
        public static void Check()
        {
            if (!DetectLinux.LinuxDetected())
            {
                Log.Checking("REDISTRIBUTABLE: Is Installed or Not");
                DiscordLauncherPresense.Status("Start Up", "Checking Redistributable Package Visual Code 2015 to 2019");

                if (!RedistributablePackage.IsInstalled(RedistributablePackageVersion.VC2015to2019x86))
                {
                    var result = MessageBox.Show(
                        "You do not have the 32-bit 2015-2019 VC++ Redistributable Package installed.\n \nThis will install in the Background\n \nThis may restart your computer. \n \nClick OK to install it.",
                        "Compatibility",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning);

                    if (result != DialogResult.OK)
                    {
                        ErrorFree = false;
                        MessageBox.Show("The game will not be started.", "Compatibility", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }

                    try
                    {
                        FunctionStatus.TLS();
                        Uri URLCall = new Uri("https://aka.ms/vs/16/release/VC_redist.x86.exe");
                        ServicePointManager.FindServicePoint(URLCall).ConnectionLeaseTimeout = (int)TimeSpan.FromMinutes(1).TotalMilliseconds;
                        var Client = new WebClient
                        {
                            Encoding = Encoding.UTF8
                        };

                        if (!WebCalls.Alternative()) { Client = new WebClientWithTimeout { Encoding = Encoding.UTF8 }; }
                        else
                        {
                            Client.Headers.Add("user-agent", "SBRW Launcher " +
                            Application.ProductVersion + " (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)");
                        }

                        try
                        {
                            Client.DownloadFile(URLCall, "VC_redist.x86.exe");
                        }
                        catch (Exception Error)
                        {
                            LogToFileAddons.OpenLog("REDISTRIBUTABLE", null, Error, null, true);
                        }
                        finally
                        {
                            if (Client != null)
                            {
                                Client.Dispose();
                            }
                        }
                    }
                    catch (Exception Error)
                    {
                        LogToFileAddons.OpenLog("REDISTRIBUTABLE", null, Error, null, true);
                    }

                    if (File.Exists("VC_redist.x86.exe"))
                    {
                        try
                        {
                            var proc = Process.Start(new ProcessStartInfo
                            {
                                Verb = "runas",
                                Arguments = "/quiet",
                                FileName = "VC_redist.x86.exe"
                            });

                            if (proc == null)
                            {
                                ErrorFree = false;
                                MessageBox.Show("Failed to run package installer. The game will not be started.", "Compatibility", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
                        }
                        catch
                        {
                            ErrorFree = false;
                            MessageBox.Show("Failed to start package installer. The game will not be started.", "Compatibility", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        ErrorFree = false;
                        MessageBox.Show("Failed to download package installer. The game will not be started.", "Compatibility", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                    }
                }
                else
                {
                    Log.Info("REDISTRIBUTABLE: 32-bit 2015-2019 VC++ Redistributable Package is Installed");
                }

                if (Environment.Is64BitOperatingSystem)
                {
                    if (!RedistributablePackage.IsInstalled(RedistributablePackageVersion.VC2015to2019x64))
                    {
                        var result = MessageBox.Show(
                            "You do not have the 64-bit 2015-2019 VC++ Redistributable Package installed.\n \nThis will install in the Background\n \nThis may restart your computer. \n \nClick OK to install it.",
                            "Compatibility",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Warning);

                        if (result != DialogResult.OK)
                        {
                            ErrorFree = false;
                            MessageBox.Show("The game will not be started.", "Compatibility", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }

                        try
                        {
                            FunctionStatus.TLS();
                            Uri URLCall = new Uri("https://aka.ms/vs/16/release/VC_redist.x64.exe");
                            ServicePointManager.FindServicePoint(URLCall).ConnectionLeaseTimeout = (int)TimeSpan.FromMinutes(1).TotalMilliseconds;
                            var Client = new WebClient
                            {
                                Encoding = Encoding.UTF8
                            };

                            if (!WebCalls.Alternative()) { Client = new WebClientWithTimeout { Encoding = Encoding.UTF8 }; }
                            else
                            {
                                Client.Headers.Add("user-agent", "SBRW Launcher " +
                                Application.ProductVersion + " (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)");
                            }

                            try
                            {
                                Client.DownloadFile(URLCall, "VC_redist.x64.exe");
                            }
                            catch (Exception Error)
                            {
                                LogToFileAddons.OpenLog("REDISTRIBUTABLE", null, Error, null, true);
                            }
                            finally
                            {
                                if (Client != null)
                                {
                                    Client.Dispose();
                                }
                            }
                        }
                        catch (Exception Error)
                        {
                            LogToFileAddons.OpenLog("REDISTRIBUTABLE x64", null, Error, null, true);
                        }

                        if (File.Exists("VC_redist.x64.exe"))
                        {
                            try
                            {
                                var proc = Process.Start(new ProcessStartInfo
                                {
                                    Verb = "runas",
                                    Arguments = "/quiet",
                                    FileName = "VC_redist.x64.exe"
                                });

                                if (proc == null)
                                {
                                    ErrorFree = false;
                                    MessageBox.Show("Failed to run package installer. The game will not be started.", "Compatibility", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                                }
                            }
                            catch
                            {
                                ErrorFree = false;
                                MessageBox.Show("Failed to start package installer. The game will not be started.", "Compatibility", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            ErrorFree = false;
                            MessageBox.Show("Failed to download package installer. The game will not be started.", "Compatibility", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        Log.Info("REDISTRIBUTABLE: 64-bit 2015-2019 VC++ Redistributable Package is Installed");
                    }
                }

                Log.Completed("REDISTRIBUTABLE: Done");
            }

            Log.Info("ID: Moved to Function");
            /* (Start Process) */
            HardwareID.FingerPrint.Get();
        }
    }
}
