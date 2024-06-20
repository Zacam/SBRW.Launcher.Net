using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.Core.Extra.File_.Save_;
using SBRW.Launcher.Core.Required.System.Windows_;
using SBRW.Launcher.RunTime.InsiderKit;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SBRW.Launcher.App.UI_Forms.Settings_Screen
{
    partial class Screen_Settings
    {
        ///<summary>Button: Firewall Rules API</summary>
        private async void ButtonFirewallRulesAPI_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!DisableButtonFRAPI)
                {
                    Log.Info("Security Center Screen: ".ToUpper() + "[Check Firewall API] Button was clicked by user");

                    DisableButtonFRAPI = true;

                    if (ButtonEnabler(4, 20))
                    {
                        ButtonsColorSet(ButtonFirewallRulesCheck, 2, true);
                        DisableButtonFRC = false;
                    }
                    else
                    {
                        ButtonsColorSet(ButtonFirewallRulesCheck, 3, false);
                        DisableButtonFRC = true;
                    }

                    ButtonsColorSet(ButtonFirewallRulesAPI, 1, false);
                }
            });
        }
        ///<summary>Button: Firewall Rules Check</summary>
        private async void ButtonFirewallRulesCheck_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!DisableButtonFRC)
                {
                    Log.Info("Security Center Screen: ".ToUpper() + "[Check All Rules] Button was clicked by user");

                    if (Firewall())
                    {
                        ButtonsColorSet(ButtonFirewallRulesCheck, 0, true);

                        /* Both */
                        if (ButtonEnabler(0, 2) && ButtonEnabler(1, 2) && ButtonEnabler(2, 2))
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddAll, 1, true);
                            DisableButtonFRAA = DisableButtonFRRA = false;
                            ButtonsColorSet(ButtonFirewallRulesRemoveAll, 2, true);
                        }
                        else if (!ButtonEnabler(0, 2) && !ButtonEnabler(1, 2) && !ButtonEnabler(2, 2))
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddAll, 2, true);
                            DisableButtonFRAA = false;
                        }
                        else
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddAll, 3, false);
                            DisableButtonFRAA = DisableButtonFRRA = true;
                            ButtonsColorSet(ButtonFirewallRulesRemoveAll, 3, false);
                        }
                        /* Launcher */
                        if (ButtonEnabler(0, 2) && ButtonEnabler(1, 2))
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddLauncher, 1, true);
                            DisableButtonFRAL = DisableButtonFRRL = false;
                            ButtonsColorSet(ButtonFirewallRulesRemoveLauncher, 2, true);
                        }
                        else
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddLauncher, 2, true);
                            DisableButtonFRAL = false;
                            ButtonsColorSet(ButtonFirewallRulesRemoveLauncher, 3, false);
                        }
                        /* Game */
                        if (ButtonEnabler(2, 2) && !string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Game_Path_Old) &&
                            Save_Settings.Live_Data.Game_Path_Old != Save_Settings.Live_Data.Game_Path)
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddGame, 1, true);
                            DisableButtonFRAG = DisableButtonFRRG = false;
                            ButtonsColorSet(ButtonFirewallRulesRemoveGame, 4, true);
                        }
                        else if (ButtonEnabler(2, 2))
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddGame, 1, true);
                            DisableButtonFRAG = DisableButtonFRRG = false;
                            ButtonsColorSet(ButtonFirewallRulesRemoveGame, 2, true);
                        }
                        else
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddGame,
                                (!string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Game_Path_Old) &&
                                (Save_Settings.Live_Data.Game_Path_Old != Save_Settings.Live_Data.Game_Path) ? 4 : 2), true);
                            DisableButtonFRAG = false;
                            ButtonsColorSet(ButtonFirewallRulesRemoveGame, 3, false);
                        }

                        if (Firewall())
                        {
                            ButtonsColorSet(ButtonFirewallRulesCheck, 1, true);
                        }
                        else
                        {
                            ButtonsColorSet(ButtonFirewallRulesCheck, 3, true);
                        }
                    }
                    else
                    {
                        ButtonsColorSet(ButtonFirewallRulesCheck, 3, true);
                    }
                }
            });
        }
        ///<summary>Button: Firewall Rules Add All</summary>
        private async void ButtonFirewallRulesAddAll_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!DisableButtonFRAA)
                {
                    DisableButtonFRAA = true;

                    if (ButtonEnabler(4, 20))
                    {
                        ButtonsColorSet(ButtonFirewallRulesAddAll, 2, true);

                        /* Launcher & Updater */
                        if (ButtonEnabler(0, 0) && ButtonEnabler(1, 0))
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddLauncher, 1, true);
                            ButtonsColorSet(ButtonFirewallRulesRemoveLauncher, 2, true);
                            DisableButtonFRRL = false;
                            Save_Settings.Live_Data.Firewall_Launcher = "Excluded";
                        }
                        else
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddLauncher, 3, false);
                            Save_Settings.Live_Data.Firewall_Launcher = "Error";
                        }
                        /* Game */
                        if (ButtonEnabler(2, 0))
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddGame, 1, true);
                            ButtonsColorSet(ButtonFirewallRulesRemoveGame, 2, true);
                            DisableButtonFRRG = false;
                            Save_Settings.Live_Data.Firewall_Game = "Excluded";
                        }
                        else
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddGame, 3, false);
                            ButtonsColorSet(ButtonFirewallRulesRemoveGame, 3, false);
                            Save_Settings.Live_Data.Firewall_Game = "Error";
                        }

                        Save_Settings.Save();

                        if (Firewall())
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddAll, 1, true);
                            DisableButtonFRRA = !(ButtonFirewallRulesRemoveLauncher.Enabled && ButtonFirewallRulesRemoveGame.Enabled);
                            ButtonsColorSet(ButtonFirewallRulesRemoveAll, 2,
                                ButtonFirewallRulesRemoveLauncher.Enabled && ButtonFirewallRulesRemoveGame.Enabled);
                        }
                        else
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddAll, 3, false);
                            ButtonsColorSet(ButtonFirewallRulesRemoveAll, 3, false);
                        }
                    }
                    else
                    {
                        ButtonsColorSet(ButtonFirewallRulesAddAll, 3, false);
                    }
                }
            });
        }
        ///<summary>Button: Firewall Rules Add Launcher</summary>
        private async void ButtonFirewallRulesAddLauncher_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!DisableButtonFRAL)
                {
                    DisableButtonFRAL = true;

                    if (ButtonEnabler(4, 20))
                    {
                        ButtonsColorSet(ButtonFirewallRulesAddLauncher, 2, true);

                        /* Game */
                        if (ButtonEnabler(0, 0) && ButtonEnabler(1, 0))
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddLauncher, 1, true);
                            ButtonsColorSet(ButtonFirewallRulesRemoveLauncher, 2, true);
                            DisableButtonFRRL = false;
                            Save_Settings.Live_Data.Firewall_Launcher = "Excluded";
                        }
                        else
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddLauncher, 3, false);
                            ButtonsColorSet(ButtonFirewallRulesRemoveLauncher, 3, false);
                            Save_Settings.Live_Data.Firewall_Launcher = "Error";
                        }

                        Save_Settings.Save();
                    }
                    else
                    {
                        ButtonsColorSet(ButtonFirewallRulesAddLauncher, 3, false);
                    }
                }
            });
        }
        ///<summary>Button: Firewall Rules Add Game</summary>
        private async void ButtonFirewallRulesAddGame_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!DisableButtonFRAG)
                {
                    DisableButtonFRAG = true;

                    if (ButtonEnabler(4, 20))
                    {
                        ButtonsColorSet(ButtonFirewallRulesAddGame, 2, true);

                        /* Remove Old Game Path and Cache Location Just in Case for Windows Defender */
                        if (!string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Game_Path_Old))
                        {
                            if (ButtonEnabler(3, 1))
                            {
                                if (string.IsNullOrWhiteSpace(CacheOldGameLocation))
                                {
                                    CacheOldGameLocation = Save_Settings.Live_Data.Game_Path_Old;
                                }
                                Save_Settings.Live_Data.Game_Path_Old = string.Empty;
                            }
                        }

                        /* Game */
                        if (ButtonEnabler(2, 0))
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddGame, 1, true);
                            ButtonsColorSet(ButtonFirewallRulesRemoveGame, 2, true);
                            DisableButtonFRRG = false;
                            Save_Settings.Live_Data.Firewall_Game = "Excluded";
                        }
                        else
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddGame, 3, false);
                            ButtonsColorSet(ButtonFirewallRulesRemoveGame, 3, false);
                            Save_Settings.Live_Data.Firewall_Game = "Error";
                        }

                        Save_Settings.Save();
                    }
                    else
                    {
                        ButtonsColorSet(ButtonFirewallRulesAddGame, 3, false);
                    }
                }
            });
        }
        ///<summary>Button: Firewall Rules Remove All</summary>
        private async void ButtonFirewallRulesRemoveAll_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!DisableButtonFRRA)
                {
                    DisableButtonFRRA = true;

                    if (ButtonEnabler(4, 20))
                    {
                        ButtonsColorSet(ButtonFirewallRulesRemoveAll, 2, true);

                        /* Launcher & Updater */
                        if (ButtonEnabler(0, 1) && ButtonEnabler(1, 1))
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddLauncher, 2, true);
                            DisableButtonFRAL = true;
                            ButtonsColorSet(ButtonFirewallRulesRemoveLauncher, 1, true);
                            Save_Settings.Live_Data.Firewall_Launcher = "Removed";
                        }
                        else
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddLauncher, 3, false);
                            ButtonsColorSet(ButtonFirewallRulesRemoveLauncher, 3, false);
                            Save_Settings.Live_Data.Firewall_Launcher = "Error";
                        }
                        /* Game */
                        if (ButtonEnabler(2, 1))
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddGame, 2, true);
                            DisableButtonFRAG = true;
                            ButtonsColorSet(ButtonFirewallRulesRemoveGame, 1, true);
                            Save_Settings.Live_Data.Firewall_Game = "Removed";
                        }
                        else
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddGame, 3, false);
                            ButtonsColorSet(ButtonFirewallRulesRemoveGame, 3, false);
                            Save_Settings.Live_Data.Firewall_Game = "Error";
                        }

                        Save_Settings.Save();

                        if (Firewall())
                        {
                            ButtonsColorSet(ButtonFirewallRulesRemoveAll, 1, true);
                            DisableButtonFRAA = !(ButtonFirewallRulesAddLauncher.Enabled && ButtonFirewallRulesAddGame.Enabled);
                            ButtonsColorSet(ButtonFirewallRulesAddAll, 2, ButtonFirewallRulesAddLauncher.Enabled && ButtonFirewallRulesAddGame.Enabled);
                        }
                        else
                        {
                            ButtonsColorSet(ButtonFirewallRulesRemoveAll, 3, false);
                            ButtonsColorSet(ButtonFirewallRulesAddAll, 3, false);
                        }
                    }
                    else
                    {
                        ButtonsColorSet(ButtonFirewallRulesRemoveAll, 3, false);
                    }
                }
            });
        }
        ///<summary>Button: Firewall Rules Remove Launcher</summary>
        private async void ButtonFirewallRulesRemoveLauncher_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!DisableButtonFRRL)
                {
                    DisableButtonFRRL = true;

                    if (ButtonEnabler(4, 20))
                    {
                        ButtonsColorSet(ButtonFirewallRulesRemoveLauncher, 2, true);

                        /* Launcher & Updater */
                        if (ButtonEnabler(0, 1) && ButtonEnabler(1, 1))
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddLauncher, 2, true);
                            DisableButtonFRAL = false;
                            ButtonsColorSet(ButtonFirewallRulesRemoveLauncher, 1, true);
                            Save_Settings.Live_Data.Firewall_Launcher = "Removed";
                        }
                        else
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddLauncher, 3, false);
                            ButtonsColorSet(ButtonFirewallRulesRemoveLauncher, 3, false);
                            Save_Settings.Live_Data.Firewall_Launcher = "Error";
                        }

                        Save_Settings.Save();
                    }
                    else
                    {
                        ButtonsColorSet(ButtonFirewallRulesRemoveLauncher, 3, false);
                    }
                }
            });
        }
        ///<summary>Button: Firewall Rules Remove Game</summary>
        private async void ButtonFirewallRulesRemoveGame_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!DisableButtonFRRG)
                {
                    DisableButtonFRRG = true;

                    if (ButtonEnabler(4, 20))
                    {
                        ButtonsColorSet(ButtonFirewallRulesRemoveGame, 0, true);
                        /* Remove Old Game Path and Cache Location Just in Case for Windows Defender */
                        if (!string.IsNullOrWhiteSpace(Save_Settings.Live_Data.Game_Path_Old))
                        {
                            if (ButtonEnabler(3, 1))
                            {
                                if (string.IsNullOrWhiteSpace(CacheOldGameLocation))
                                {
                                    CacheOldGameLocation = Save_Settings.Live_Data.Game_Path_Old;
                                }
                                Save_Settings.Live_Data.Game_Path_Old = string.Empty;
                            }
                        }

                        /* Game */
                        if (ButtonEnabler(2, 1))
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddGame, 2, true);
                            DisableButtonFRAG = false;
                            ButtonsColorSet(ButtonFirewallRulesRemoveGame, 1, true);
                            Save_Settings.Live_Data.Firewall_Game = "Removed";
                        }
                        else
                        {
                            ButtonsColorSet(ButtonFirewallRulesAddGame, 3, false);
                            ButtonsColorSet(ButtonFirewallRulesRemoveGame, 3, false);
                            Save_Settings.Live_Data.Firewall_Game = "Error";
                        }

                        Save_Settings.Save();
                    }
                    else
                    {
                        ButtonsColorSet(ButtonFirewallRulesRemoveGame, 3, false);
                    }
                }
            });
        }
        ///<summary>Button: Defender Exclusion API</summary>
        private async void ButtonDefenderExclusionAPI_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!DisableButtonDRAPI)
                {
                    if (BuildDevelopment.Allowed() || (Product_Version.GetWindowsNumber() >= 10 &&
                        (MessageBox.Show(this, "There has been reports that some users are not able to run Windows Defender Checks." +
                        "\nThis ranges from the Built-In to Third-Party Anti-Virus Software." +
                        "\n\nIf this Window Closes or the Launcher Crashes with an Error Message" +
                        "\n\nDo not run this Check, just simply ignore this section." +
                        "\n\n\nClick Yes to Agree to a potential Launcher Crash" +
                        "\nClick No to avoid a potential Launcher Crash",
                        "Windows Defender API Check - SBRW Launcher", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)))
                    {
                        DisableButtonDRAPI = true;

                        if (ButtonEnabler(5, 10))
                        {
                            ButtonsColorSet(ButtonDefenderExclusionCheck, 2, true);
                            DisableButtonDRC = false;
                        }
                        else { ButtonsColorSet(ButtonDefenderExclusionCheck, 3, false); DisableButtonDRC = true; }

                        ButtonsColorSet(ButtonDefenderExclusionAPI, 1, false);
                    }
                }
            });
        }
        ///<summary>Button: Defender Exclusion Check</summary>
        private async void ButtonDefenderExclusionCheck_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!DisableButtonDRC)
                {
                    if (Defender())
                    {
                        ButtonsColorSet(ButtonDefenderExclusionCheck, 0, true);

                        /* Launcher, Updater, & All */
                        if (ButtonEnabler(0, 5))
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddAll, 1, true);
                            ButtonsColorSet(ButtonDefenderExclusionAddLauncher, 1, true);
                            ButtonsColorSet(ButtonDefenderExclusionRemoveAll, 2, true);
                            ButtonsColorSet(ButtonDefenderExclusionRemoveLauncher, 2, true);
                            DisableButtonDRAA = DisableButtonDRAL = DisableButtonDRRA = DisableButtonDRRL = false;
                        }
                        else
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddAll, 2, true);
                            ButtonsColorSet(ButtonDefenderExclusionAddLauncher, 2, true);
                            DisableButtonDRAL = DisableButtonDRAA = false;
                            ButtonsColorSet(ButtonDefenderExclusionRemoveAll, 3, false);
                            ButtonsColorSet(ButtonDefenderExclusionRemoveLauncher, 3, false);
                        }
                        /* Game */
                        if (ButtonEnabler(2, 5) && !string.IsNullOrWhiteSpace(CacheOldGameLocation) &&
                            CacheOldGameLocation != Save_Settings.Live_Data.Game_Path)
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddGame, 1, true);
                            DisableButtonDRAG = DisableButtonDRRG = false;
                            ButtonsColorSet(ButtonDefenderExclusionRemoveGame, 4, true);
                        }
                        else if (ButtonEnabler(2, 5))
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddGame, 1, true);
                            DisableButtonDRAG = DisableButtonDRRG = false;
                            ButtonsColorSet(ButtonDefenderExclusionRemoveGame, 2, true);
                        }
                        else
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddGame,
                                (!string.IsNullOrWhiteSpace(CacheOldGameLocation) &&
                                (CacheOldGameLocation != Save_Settings.Live_Data.Game_Path) ? 4 : 2), true);
                            DisableButtonDRAG = false;
                            ButtonsColorSet(ButtonDefenderExclusionRemoveGame, 3, false);
                            DisableButtonDRRG = true;
                        }

                        if (Defender())
                        { ButtonsColorSet(ButtonDefenderExclusionCheck, 1, true); }
                        else
                        { ButtonsColorSet(ButtonDefenderExclusionCheck, 3, true); }
                    }
                    else
                    { ButtonsColorSet(ButtonDefenderExclusionCheck, 3, true); }
                }
            });
        }
        ///<summary>Button: Defender Exclusion Add All</summary>
        private async void ButtonDefenderExclusionAddAll_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!DisableButtonDRAA)
                {
                    DisableButtonDRAA = true;

                    if (ButtonEnabler(4, 20))
                    {
                        ButtonsColorSet(ButtonDefenderExclusionAddAll, 2, true);

                        /* Launcher & Updater */
                        if (ButtonEnabler(0, 3))
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddLauncher, 1, true);
                            ButtonsColorSet(ButtonDefenderExclusionRemoveLauncher, 2, true);
                            DisableButtonDRRL = false;
                            Save_Settings.Live_Data.Defender_Launcher = "Excluded";
                        }
                        else
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddLauncher, 3, false);
                            Save_Settings.Live_Data.Defender_Launcher = "Error";
                        }
                        /* Game */
                        if (ButtonEnabler(2, 3))
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddGame, 1, true);
                            ButtonsColorSet(ButtonDefenderExclusionRemoveGame, 2, true);
                            DisableButtonDRRG = false;
                            Save_Settings.Live_Data.Defender_Game = "Excluded";
                        }
                        else
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddGame, 3, false);
                            ButtonsColorSet(ButtonDefenderExclusionRemoveGame, 3, false);
                            Save_Settings.Live_Data.Defender_Game = "Error";
                        }

                        Save_Settings.Save();

                        if (Defender())
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddAll, 1, true);
                            DisableButtonDRRA = !(ButtonDefenderExclusionRemoveLauncher.Enabled && ButtonDefenderExclusionRemoveGame.Enabled);
                            ButtonsColorSet(ButtonDefenderExclusionRemoveAll, 2,
                                ButtonDefenderExclusionRemoveLauncher.Enabled && ButtonDefenderExclusionRemoveGame.Enabled);
                        }
                        else
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddAll, 3, false);
                            ButtonsColorSet(ButtonDefenderExclusionRemoveAll, 3, false);
                        }
                    }
                    else
                    {
                        ButtonsColorSet(ButtonDefenderExclusionAddAll, 3, false);
                    }
                }
            });
        }
        ///<summary>Button: Defender Exclusion Add Launcher</summary>
        private async void ButtonDefenderExclusionAddLauncher_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!DisableButtonDRAL)
                {
                    DisableButtonDRAL = true;

                    if (ButtonEnabler(5, 10))
                    {
                        ButtonsColorSet(ButtonDefenderExclusionAddLauncher, 2, true);

                        /* Launcher & Updater */
                        if (ButtonEnabler(0, 3))
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddLauncher, 1, true);
                            ButtonsColorSet(ButtonDefenderExclusionRemoveLauncher, 2, true);
                            DisableButtonDRRL = false;
                            Save_Settings.Live_Data.Defender_Launcher = "Excluded";
                        }
                        else
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddLauncher, 3, false);
                            ButtonsColorSet(ButtonDefenderExclusionRemoveLauncher, 3, false);
                            Save_Settings.Live_Data.Defender_Launcher = "Error";
                        }

                        Save_Settings.Save();
                    }
                    else
                    {
                        ButtonsColorSet(ButtonDefenderExclusionAddLauncher, 3, false);
                    }
                }
            });
        }
        ///<summary>Button: Defender Exclusion Add Game</summary>
        private async void ButtonDefenderExclusionAddGame_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!DisableButtonDRAG)
                {
                    DisableButtonDRAG = true;

                    if (ButtonEnabler(5, 10))
                    {
                        ButtonsColorSet(ButtonDefenderExclusionAddGame, 2, true);
                        /* Remove Old Game Path */
                        if (!string.IsNullOrWhiteSpace(CacheOldGameLocation))
                        {
                            if (ButtonEnabler(3, 4))
                            {
                                CacheOldGameLocation = string.Empty;
                            }
                        }

                        /* Game */
                        if (ButtonEnabler(2, 3))
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddGame, 1, true);
                            ButtonsColorSet(ButtonDefenderExclusionRemoveGame, 2, true);
                            DisableButtonDRRG = false;
                            Save_Settings.Live_Data.Defender_Game = "Excluded";
                        }
                        else
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddGame, 3, false);
                            ButtonsColorSet(ButtonDefenderExclusionRemoveGame, 3, false);
                            Save_Settings.Live_Data.Defender_Game = "Error";
                        }

                        Save_Settings.Save();
                    }
                    else
                    {
                        ButtonsColorSet(ButtonDefenderExclusionAddGame, 3, false);
                    }
                }
            });
        }
        ///<summary>Button: Defender Exclusion Remove All</summary>
        private async void ButtonDefenderExclusionRemoveAll_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!DisableButtonDRRA)
                {
                    DisableButtonDRRA = true;

                    if (ButtonEnabler(4, 20))
                    {
                        ButtonsColorSet(ButtonDefenderExclusionRemoveAll, 2, true);

                        /* Launcher & Updater */
                        if (ButtonEnabler(0, 4))
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddLauncher, 2, true);
                            DisableButtonDRAL = true;
                            ButtonsColorSet(ButtonDefenderExclusionRemoveLauncher, 1, true);
                            Save_Settings.Live_Data.Defender_Launcher = "Removed";
                        }
                        else
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddLauncher, 3, false);
                            ButtonsColorSet(ButtonDefenderExclusionRemoveLauncher, 3, false);
                            Save_Settings.Live_Data.Defender_Launcher = "Error";
                        }
                        /* Game */
                        if (ButtonEnabler(2, 4))
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddGame, 2, true);
                            DisableButtonDRAG = true;
                            ButtonsColorSet(ButtonDefenderExclusionRemoveGame, 1, true);
                            Save_Settings.Live_Data.Defender_Game = "Removed";
                        }
                        else
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddGame, 3, false);
                            ButtonsColorSet(ButtonDefenderExclusionRemoveGame, 3, false);
                            Save_Settings.Live_Data.Defender_Game = "Error";
                        }

                        Save_Settings.Save();

                        if (Defender())
                        {
                            ButtonsColorSet(ButtonDefenderExclusionRemoveAll, 1, true);
                            DisableButtonDRAA = !(ButtonDefenderExclusionAddLauncher.Enabled && ButtonDefenderExclusionAddGame.Enabled);
                            ButtonsColorSet(ButtonDefenderExclusionAddAll, 2,
                                ButtonDefenderExclusionAddLauncher.Enabled && ButtonDefenderExclusionAddGame.Enabled);
                        }
                        else
                        {
                            ButtonsColorSet(ButtonDefenderExclusionRemoveAll, 3, false);
                            ButtonsColorSet(ButtonDefenderExclusionAddAll, 3, false);
                        }
                    }
                    else
                    {
                        ButtonsColorSet(ButtonDefenderExclusionRemoveAll, 3, false);
                    }
                }
            });
        }
        ///<summary>Button: Defender Exclusion Remove Launcher</summary>
        private async void ButtonDefenderExclusionRemoveLauncher_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!DisableButtonDRRL)
                {
                    DisableButtonDRRL = true;

                    if (ButtonEnabler(4, 20))
                    {
                        ButtonsColorSet(ButtonDefenderExclusionRemoveLauncher, 2, true);

                        /* Launcher & Updater */
                        if (ButtonEnabler(0, 4))
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddLauncher, 2, true);
                            DisableButtonDRAL = false;
                            ButtonsColorSet(ButtonDefenderExclusionRemoveLauncher, 1, true);
                            Save_Settings.Live_Data.Defender_Launcher = "Removed";
                        }
                        else
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddLauncher, 3, false);
                            ButtonsColorSet(ButtonDefenderExclusionRemoveLauncher, 3, false);
                            Save_Settings.Live_Data.Defender_Launcher = "Error";
                        }

                        Save_Settings.Save();
                    }
                    else
                    {
                        ButtonsColorSet(ButtonDefenderExclusionRemoveLauncher, 3, false);
                    }
                }
            });
        }
        ///<summary>Button: Defender Exclusion Remove Game</summary>
        private async void ButtonDefenderExclusionRemoveGame_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!DisableButtonDRRG)
                {
                    DisableButtonDRRG = true;

                    if (ButtonEnabler(4, 20))
                    {
                        ButtonsColorSet(ButtonDefenderExclusionRemoveGame, 0, true);
                        /* Remove Old Game Path */
                        if (!string.IsNullOrWhiteSpace(CacheOldGameLocation))
                        {
                            if (ButtonEnabler(3, 4))
                            {
                                CacheOldGameLocation = string.Empty;
                            }
                        }

                        /* Game */
                        if (ButtonEnabler(2, 4))
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddGame, 2, true);
                            DisableButtonDRAG = false;
                            ButtonsColorSet(ButtonDefenderExclusionRemoveGame, 1, true);
                            Save_Settings.Live_Data.Defender_Game = "Removed";
                        }
                        else
                        {
                            ButtonsColorSet(ButtonDefenderExclusionAddGame, 3, false);
                            ButtonsColorSet(ButtonDefenderExclusionRemoveGame, 3, false);
                            Save_Settings.Live_Data.Defender_Game = "Error";
                        }

                        Save_Settings.Save();
                    }
                    else
                    {
                        ButtonsColorSet(ButtonDefenderExclusionRemoveGame, 3, false);
                    }
                }
            });
        }
        ///<summary>Button: File or Folder Permisson Check</summary>
        private async void ButtonFolderPermissonCheck_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!DisableButtonPRC)
                {
                    if (!ButtonEnabler(6, 6))
                    {
                        ButtonsColorSet(ButtonFolderPermissonSet, 2, true);
                        DisableButtonPRAA = false;
                    }
                    else
                    {
                        ButtonsColorSet(ButtonFolderPermissonSet, 1, false);
                    }

                    ButtonsColorSet(ButtonFolderPermissonCheck, 1, false);
                }
            });
        }
        ///<summary>Button: Firewall Rules Add Launcher</summary>
        private async void ButtonFolderPermissonSet_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (!DisableButtonPRAA)
                {
                    DisableButtonPRAA = true;

                    if (ButtonEnabler(6, 6))
                    {
                        ButtonsColorSet(ButtonFolderPermissonSet, 2, true);
                        DisableButtonPRC = true;

                        /* Game */
                        if (ButtonEnabler(0, 6))
                        {
                            ButtonsColorSet(ButtonFolderPermissonSet, 1, true);
                            Save_Settings.Live_Data.Write_Permissions = "Set";
                        }
                        else
                        {
                            ButtonsColorSet(ButtonFolderPermissonSet, 3, false);
                            Save_Settings.Live_Data.Write_Permissions = "Error";
                        }

                        Save_Settings.Save();
                    }
                    else
                    {
                        ButtonsColorSet(ButtonFolderPermissonSet, 3, false);
                    }
                }
            });
        }
    }
}
