using GameLauncher;
using GameLauncher.App.Classes.LauncherCore.Global;
using GameLauncher.App.Classes.LauncherCore.ModNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace GameLauncher.App.Classes.LauncherCore
{
    class AntiCheat
    {
        public static int process_id = 0;
        public static Thread thread = new Thread(() => { });
        public static List<int> addresses = new List<int> {
                    418534,  // GMZ_MULTIHACK
                    3788216, // FAST_POWERUPS
                    4552702, // SPEEDHACK
                    4476396, // SMOOTH_WALLS
                    4506534, // TANK
                    4587060, // WALLHACK
                    4486168, // DRIFTMOD/MULTIHACK
                    4820249, // PURSUITBOT (NO COPS VARIATION)
                    8972152 // PROFILEMASKER!
                };

        public static void Checks()
        {
            Process process = Process.GetProcessById(process_id);
            IntPtr processHandle = Kernel32.OpenProcess(0x0010, false, process.Id);
            int baseAddress = process.MainModule.BaseAddress.ToInt32();

            thread = new Thread(() =>
            {
                while (true)
                {
                    bool CanKillGame = false;

                    foreach (var oneAddress in addresses)
                    {
                        int bytesRead = 0;
                        byte[] buffer = new byte[4];
                        Kernel32.ReadProcessMemory((int)processHandle, baseAddress + oneAddress, buffer, buffer.Length, ref bytesRead);

                        String checkInt = "0x" + BitConverter.ToString(buffer).Replace("-", String.Empty);

                        if (
                        oneAddress == 418534 && checkInt != "0x3B010F84" || oneAddress == 3788216 && checkInt != "0x807DFB00" || oneAddress == 4552702 && checkInt != "0x76390F2E" ||
                        oneAddress == 4476396 && checkInt != "0x84C00F84" || oneAddress == 4506534 && checkInt != "0x74170F57" || oneAddress == 4587060 && checkInt != "0x74228B16" ||
                        oneAddress == 4820249 && checkInt != "0x0F845403")
                        {
                            CanKillGame = true;
                        }
                        else if (oneAddress == 4486168 && checkInt != "0xF30F1086")
                        {
                            if (checkInt.Substring(0, 4) == "0xE8" || checkInt.Substring(0, 4) == "0xE9")
                            {
                                CanKillGame = true;
                            }
                        }
                    }

                    Thread.Sleep(100);

                    if (CanKillGame)
                    {
                        Form1.secondsToShutDown = 0;
                        Form1.CheatsWasUsed = true;
                    }
                }
            })
            { IsBackground = true };
            thread.Start();
        }
    }
}
