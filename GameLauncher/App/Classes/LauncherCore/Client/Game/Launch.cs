using GameLauncher.App.Classes.LauncherCore.Client.Auth;
using GameLauncher.App.Classes.LauncherCore.Client.Web;
using GameLauncher.App.Classes.LauncherCore.FileReadWrite;
using GameLauncher.App.Classes.LauncherCore.Global;
using GameLauncher.App.Classes.LauncherCore.RPC;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace GameLauncher.App.Classes.LauncherCore.Client.Game
{
    public class Launch
    {
        /* START SpeedBug Timer */
        public static bool GameKilledBySpeedBugCheck = false;
        public static int NFSWPID;
        public static int secondsToShutDown = 0;
        public static bool CheatsWasUsed = false;
        /* END SpeedBug Timer */

        public static void Game()
        {
            try
            {
                if (!string.IsNullOrEmpty(Form1.result["webPanelUrl"]))
                {
                    DiscordLauncherPresense.ServerPanelLink = Form1.result["webPanelUrl"];
                }
            }
            catch { }

            DiscordLauncherPresense.Status("In-Game", "nfsw");

            var args = "EU " + Tokens.IPAddress + " " + Tokens.LoginToken + " " + Tokens.UserId;
            var psi = new ProcessStartInfo
            {
                WorkingDirectory = FileSettingsSave.GameInstallation,
                FileName = FileSettingsSave.GameInstallation + "\\nfsw.exe",
                Arguments = args
            };

            var nfswProcess = Process.Start(psi);
            nfswProcess.PriorityClass = ProcessPriorityClass.AboveNormal;

            var processorAffinity = 0;
            for (var i = 0; i < Math.Min(Math.Max(1, Environment.ProcessorCount), 8); i++)
            {
                processorAffinity |= 1 << i;
            }

            nfswProcess.ProcessorAffinity = (IntPtr)processorAffinity;

            AntiCheat.process_id = nfswProcess.Id;
            AntiCheat.Checks();

            //TIMER HERE
            secondsToShutDown = (Form1.result["secondsToShutDown"].AsInt == 0) ? Form1.result["secondsToShutDown"].AsInt : 2 * 60 * 60;
            System.Timers.Timer shutdowntimer = new System.Timers.Timer();
            shutdowntimer.Elapsed += (x2, y2) =>
            {
                Process[] allOfThem = Process.GetProcessesByName("nfsw");

                if (secondsToShutDown <= 0)
                {
                    GameKilledBySpeedBugCheck = true;

                    foreach (var oneProcess in allOfThem)
                    {
                        Process.GetProcessById(oneProcess.Id).Kill();
                    }
                }

                //change title

                foreach (var oneProcess in allOfThem)
                {
                    long p = oneProcess.MainWindowHandle.ToInt64();
                    TimeSpan t = TimeSpan.FromSeconds(secondsToShutDown);
                    string secondsToShutDownNamed = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);

                    User32.SetWindowText((IntPtr)p, "NEED FOR SPEED™ WORLD | Server: " + Form1.SelectedServerName + " | " + UserAgent.WindowTextForGame + " | Force Restart In: " + secondsToShutDownNamed);
                }

                --secondsToShutDown;
            };

            shutdowntimer.Interval = 1000;
            shutdowntimer.Enabled = true;

            if (nfswProcess != null)
            {
                nfswProcess.EnableRaisingEvents = true;
                NFSWPID = nfswProcess.Id;

                nfswProcess.Exited += (sender2, e2) =>
                {
                    NFSWPID = 0;
                    var exitCode = nfswProcess.ExitCode;

                    if (GameKilledBySpeedBugCheck == true && CheatsWasUsed == false) exitCode = 2137;
                    if (CheatsWasUsed == true) exitCode = 2017;

                    if (exitCode == 0)
                    {
                        Form1.LauncherState("Launcher Shutdown");
                    }
                    else
                    {
                        try
                        {
                            AntiCheat.thread.Abort();
                        }
                        catch { }

                        String errorMsg = "Game Crash with exitcode: " + exitCode.ToString() + " (0x" + exitCode.ToString("X") + ")";
                        if (exitCode == -1073741819) errorMsg = "Game Crash: Access Violation (0x" + exitCode.ToString("X") + ")";
                        if (exitCode == -1073740940) errorMsg = "Game Crash: Heap Corruption (0x" + exitCode.ToString("X") + ")";
                        if (exitCode == -1073740791) errorMsg = "Game Crash: Stack buffer overflow (0x" + exitCode.ToString("X") + ")";
                        if (exitCode == -805306369) errorMsg = "Game Crash: Application Hang (0x" + exitCode.ToString("X") + ")";
                        if (exitCode == -1073741515) errorMsg = "Game Crash: Missing dependency files (0x" + exitCode.ToString("X") + ")";
                        if (exitCode == -1073740972) errorMsg = "Game Crash: Debugger crash (0x" + exitCode.ToString("X") + ")";
                        if (exitCode == -1073741676) errorMsg = "Game Crash: Division by Zero (0x" + exitCode.ToString("X") + ")";
                        if (exitCode == 1) errorMsg = "The process nfsw.exe was killed via Task Manager";
                        if (exitCode == 2017) errorMsg = "Server replied with Code: " + Tokens.UserId + " (0x" + exitCode.ToString("X") + ")";
                        if (exitCode == 2137) errorMsg = "Launcher killed your game to prevent SpeedBugging.";
                        if (exitCode == -3) errorMsg = "The Server was unable to resolve the request";
                        if (exitCode == -4) errorMsg = "Another instance is already executed";
                        if (exitCode == -5) errorMsg = "DirectX Device was not found. Please install GPU Drivers before playing";
                        if (exitCode == -6) errorMsg = "Server was unable to resolve your request";
                        //ModLoader
                        if (exitCode == 2) errorMsg = "ModNet: Game was launched with invalid command line parameters.";
                        if (exitCode == 3) errorMsg = "ModNet: .links file should not exist upon startup!";
                        if (exitCode == 4) errorMsg = "ModNet: An Unhandled Error Appeared";

                        if (NFSWPID != 0)
                        {
                            try
                            {
                                Process.GetProcessById(NFSWPID).Kill();
                            }
                            catch { /* ignored */ }
                        }

                        DialogResult restartApp = MessageBox.Show(null, errorMsg + "\nWould you like to restart the launcher?", "GameLauncher", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (restartApp == DialogResult.Yes)
                        {
                            Form1.LauncherState("Launcher Restart");
                        }
                        else
                        {
                            Form1.LauncherState("Launcher Shutdown");
                        }
                    }
                };
            }
        }
    }
}
