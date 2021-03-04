using System.Diagnostics;
using System.Threading;

namespace GameLauncherSimplified.App.Classes.LauncherCore.Client
{
    class NFSW
    {
        public static bool DetectByMutex()
        {
            Mutex detectRunningNFSW = new Mutex(false, "Global\\{3E34CEFB-7B34-4e62-8034-33256B8BC2F7}");
            try
            {
                if (!detectRunningNFSW.WaitOne(0, false))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                if (detectRunningNFSW != null)
                {
                    detectRunningNFSW.Close();
                }
            }
        }

        public static bool DetectGameProcess()
        {
            Process[] nfswProcess = Process.GetProcessesByName("nfsw");

            if (nfswProcess.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool DetectGameLauncherReborn()
        {
            Process[] nfswProcess = Process.GetProcessesByName("GameLauncher");

            if (nfswProcess.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsNFSWRunning()
        {
            return DetectByMutex() || DetectGameProcess() || DetectGameLauncherReborn();
        }
    }
}
