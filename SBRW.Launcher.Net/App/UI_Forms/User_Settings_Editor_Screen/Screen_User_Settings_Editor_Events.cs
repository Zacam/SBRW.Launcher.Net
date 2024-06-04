using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.Core.Extra.File_.Save_;
using SBRW.Launcher.Core.Extra.XML_;
using SBRW.Launcher.Core.Reference.Json_.Newtonsoft_;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SBRW.Launcher.App.UI_Forms.User_Settings_Editor_Screen
{
    public partial class Screen_User_Settings_Editor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsSave_Click(object sender, EventArgs e)
        {
            if (!FileReadOnly) { Button_Save.Text = "SAVING"; }

            XML_File.XML_Settings_Data.ScreenWidth = ValidWholeNumberRange("Resolution", (comboBoxPerformanceLevel.SelectedValue.ToString() == "5" || ResolutionsListLoaded == false) ?
                                               numericResWidth.Value : Convert.ToDecimal(((Json_List_Resolution)comboResolutions.SelectedItem).Width));
            XML_File.XML_Settings_Data.ScreenHeight = ValidWholeNumberRange("Resolution", (comboBoxPerformanceLevel.SelectedValue.ToString() == "5" || ResolutionsListLoaded == false) ?
                                                numericResHeight.Value : Convert.ToDecimal(((Json_List_Resolution)comboResolutions.SelectedItem).Height));
            
            Save_Settings.Live_Data.Launcher_Language = ((Json_List_Language)ComboBox_Language_List.SelectedItem).Value_Ini;
            XML_File.XML_Settings_Data.Language = ((Json_List_Language)ComboBox_Language_List.SelectedItem).Value_XML;

            XML_File.XML_Settings_Data.Brightness = ValidWholeNumberRange("Brightness", numericBrightness.Value);
            XML_File.XML_Settings_Data.MasterAudio = ValidDecimalNumberRange(numericMVol.Value);
            XML_File.XML_Settings_Data.SFXAudio = ValidDecimalNumberRange(numericSFxVol.Value);
            XML_File.XML_Settings_Data.CarAudio = ValidDecimalNumberRange(numericCarVol.Value);
            XML_File.XML_Settings_Data.SpeechAudio = ValidDecimalNumberRange(numericSpeech.Value);
            XML_File.XML_Settings_Data.MusicAudio = ValidDecimalNumberRange(numericGMusic.Value);
            XML_File.XML_Settings_Data.FreeroamAudio = ValidDecimalNumberRange(numericFEMusic.Value);

            XML_File.XML_Settings_Data.AudioQuality = (radioAQLow.Checked == true) ? "0" : "1";
            XML_File.XML_Settings_Data.VSyncOn = (radioVSyncOff.Checked == true) ? "0" : "1";
            XML_File.XML_Settings_Data.EnableAero = (radioAeroOff.Checked == true) ? "0" : "1";
            XML_File.XML_Settings_Data.ScreenWindowed = (radioWindowedOff.Checked == true) ? "0" : "1";
            XML_File.XML_Settings_Data.Damage = (radioDamageOff.Checked == true) ? "0" : "1";
            XML_File.XML_Settings_Data.SpeedUnits = (radioKmH.Checked == true) ? "0" : "1";

            XML_File.XML_Settings_Data.TransmissionType = comboBoxTransmisson.SelectedValue.ToString(); // Physics
            XML_File.XML_Settings_Data.Transmission = comboBoxTransmisson.SelectedValue.ToString(); // GamePlayOptions
            XML_File.XML_Settings_Data.AudioMode = comboAudioMode.SelectedValue.ToString(); // GamePlayOptions
            XML_File.XML_Settings_Data.AudioM = comboAudioMode.SelectedValue.ToString(); //VideoConfig
            XML_File.XML_Settings_Data.CameraPOV = comboBoxCamera.SelectedValue.ToString(); // Physics
            XML_File.XML_Settings_Data.Camera = comboBoxCamera.SelectedValue.ToString(); // GamePlayOptions

            XML_File.XML_Settings_Data.MotionBlurEnable = (radioMotionBlurOff.Checked == true) ? "0" : "1";
            XML_File.XML_Settings_Data.RoadTextureLODBias = (radioRoadLODBiasOff.Checked == true) ? "0" : "1";
            XML_File.XML_Settings_Data.BaseTextureLODBias = (radioBaseTextureLODOff.Checked == true) ? "0" : "1";
            XML_File.XML_Settings_Data.CarLODLevel = (radioCarDetailLODOff.Checked == true) ? "0" : "1";
            XML_File.XML_Settings_Data.OverBrightEnable = (radioOverBrightOff.Checked == true) ? "0" : "1";
            XML_File.XML_Settings_Data.ParticleSystemEnable = (radioParticleSysOff.Checked == true) ? "0" : "1";
            XML_File.XML_Settings_Data.VisualTreatment = (radioVisualTreatOff.Checked == true) ? "0" : "1";
            XML_File.XML_Settings_Data.WaterSimEnable = (radioWaterSimulationOff.Checked == true) ? "0" : "1";
            XML_File.XML_Settings_Data.MaxSkidMarks = SelectedElement("MaxSkidMarks");
            XML_File.XML_Settings_Data.PostProcessingEnable = (radioPostProcOff.Checked == true) ? "0" : "1";
            XML_File.XML_Settings_Data.RainEnable = (radioButton_Rain_Off.Checked == true) ? "0" : "1";

            XML_File.XML_Settings_Data.PerformanceLevel = comboBoxPerformanceLevel.SelectedValue.ToString();
            XML_File.XML_Settings_Data.BaseTextureFilter = comboBoxBaseTextureFilter.SelectedValue.ToString();
            XML_File.XML_Settings_Data.BaseTextureMaxAni = comboBoxAnisotropicLevel.SelectedValue.ToString();
            XML_File.XML_Settings_Data.CarEnvironmentMapEnable = comboBoxCarEnvironmentDetail.SelectedValue.ToString();
            XML_File.XML_Settings_Data.GlobalDetailLevel = comboBoxWorldGlobalDetail.SelectedValue.ToString();
            XML_File.XML_Settings_Data.RoadReflectionEnable = comboBoxWorldRoadReflection.SelectedValue.ToString();
            XML_File.XML_Settings_Data.RoadTextureFilter = comboBoxWorldRoadTexture.SelectedValue.ToString();
            XML_File.XML_Settings_Data.RoadTextureMaxAni = comboBoxWorldRoadAniso.SelectedValue.ToString();
            XML_File.XML_Settings_Data.FSAALevel = comboBoxShaderFSAA.SelectedValue.ToString();
            XML_File.XML_Settings_Data.ShadowDetail = comboBoxShadowDetail.SelectedValue.ToString();
            XML_File.XML_Settings_Data.ShaderDetail = comboBoxShaderDetail.SelectedValue.ToString();

            if (!FileReadOnly)
            {
                if (XML_File.Save() == 1)
                {
                    Button_Save.Text = "SAVED";
                }
                else
                {
                    Button_Save.Text = "ERROR";
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_Language_List_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (((Json_List_Language)ComboBox_Language_List.SelectedItem).IsSpecial)
                {
                    ComboBox_Language_List.SelectedIndex = LastSelectedLanguage;
                }
                else
                {
                    LastSelectedLanguage = ComboBox_Language_List.SelectedIndex;
                }
            }
            catch { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxPerformanceLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SetValues(comboBoxPerformanceLevel.SelectedIndex);

                if (comboBoxPerformanceLevel.SelectedIndex == 5)
                {
                    comboResolutions.Visible = false;
                    ((Control)TabPage_Advanced).Enabled = true;
                    ((Control)TabPage_Advanced).Visible = true;
                }
                else
                {
                    ((Control)TabPage_Advanced).Enabled = false;
                    ((Control)TabPage_Advanced).Visible = false;

                    if (TabControl_USXE.SelectedTab.Equals(TabPage_Advanced))
                    {
                        TabControl_USXE.SelectedTab = TabPage_General;
                    }

                    if (ResolutionsListLoaded == true)
                    {
                        comboResolutions.Visible = true;
                    }
                    else
                    {
                        comboResolutions.Visible = false;
                    }
                }
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("USXE", String.Empty, Error, String.Empty, true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelLauncherVersion_Click(object sender, EventArgs e)
        {
            DialogResult Alert = MessageBox.Show(null, "Do you want my Super Honk?", "FunkyWacky", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

            if (Alert == DialogResult.No)
            {
                MessageBox.Show(null, "**Sad Turbo Noises**", "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Alert == DialogResult.Yes)
            {
                Process.Start("https://www.youtube.com/watch?v=2aL6D8tj2wk");
            }
            else
            {
                Log.Info("USXE: User Broke the Honk!");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PresetButtonMin_CheckedChanged(object sender, EventArgs e)
        {
            if (PresetLoaded)
            {
                SetValues(0);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PresetButtonLow_CheckedChanged(object sender, EventArgs e)
        {
            if (PresetLoaded)
            {
                SetValues(1);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PresetButtonMed_CheckedChanged(object sender, EventArgs e)
        {
            if (PresetLoaded)
            {
                SetValues(2);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PresetButtonHigh_CheckedChanged(object sender, EventArgs e)
        {
            if (PresetLoaded)
            {
                SetValues(3);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PresetButtonMax_CheckedChanged(object sender, EventArgs e)
        {
            if (PresetLoaded)
            {
                SetValues(4);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PresetButtonCustom_CheckedChanged(object sender, EventArgs e)
        {
            if (PresetLoaded)
            {
                SetValues(5);
            }
        }
    }
}
