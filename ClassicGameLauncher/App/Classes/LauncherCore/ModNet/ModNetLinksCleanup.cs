using System;
using System.IO;
using System.Windows.Forms;

namespace ClassicGameLauncher.App.Classes.LauncherCore.ModNet
{
    class ModNetLinksCleanup
    {
        public static void CleanLinks(string linksPath)
        {
            try
            {
                if (File.Exists(linksPath))
                {
                    string dir = AppDomain.CurrentDomain.BaseDirectory;
                    foreach (var readLine in File.ReadLines(linksPath))
                    {
                        var parts = readLine.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

                        if (parts.Length != 2)
                        {
                            continue;
                        }

                        string loc = parts[0];
                        int type = int.Parse(parts[1]);
                        string realLoc = Path.Combine(dir, loc);
                        if (type == 0)
                        {
                            string origPath = realLoc + ".orig";

                            if (!File.Exists(realLoc))
                            {
                                if (!File.Exists(origPath))
                                {
                                    continue;
                                }
                                else if (File.Exists(origPath))
                                {
                                    DialogResult skipFolder = MessageBox.Show(null, "Found .orig file that should not be present:\n" +
                                        origPath, "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    Environment.Exit(0);
                                }
                                else
                                {
                                    DialogResult skipFolder = MessageBox.Show(null, ".links file includes nonexistent file:\n" +
                                        realLoc + "\n\nChoose \"Yes\" to Skip File \nChoose \"No\" to Close Launcher", "GameLauncher", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                                    if (skipFolder == DialogResult.Yes)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        Environment.Exit(0);
                                    }
                                }
                            }

                            if (!File.Exists(origPath))
                            {
                                File.Delete(realLoc);
                                continue;
                            }

                            try
                            {
                                File.Delete(realLoc);
                                File.Move(origPath, realLoc);
                            }
                            catch (Exception)
                            {

                            }
                        }
                        else
                        {
                            if (!Directory.Exists(realLoc))
                            {
                                DialogResult skipFolder = MessageBox.Show(null, ".links file includes nonexistent file:\n" +
                                    realLoc + "\n\nChoose \"Yes\" to Skip File \nChoose \"No\" to Close Launcher", "GameLauncher", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                                if (skipFolder == DialogResult.Yes)
                                {
                                    continue;
                                }
                                else
                                {
                                    Environment.Exit(0);
                                }
                            }
                            Directory.Delete(realLoc, true);
                        }
                    }

                    File.Delete(linksPath);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
