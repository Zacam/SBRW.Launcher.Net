using SBRW.Launcher.App.UI_Forms.Parent_Screen;
using SBRW.Launcher.Core.Extra.XML_;
using SBRW.Launcher.Core.Reference.Json_.Newtonsoft_;
using SBRW.Launcher.Core.Theme;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.Lists;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using SBRW.Launcher.RunTime.LauncherCore.Support;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SBRW.Launcher.App.UI_Forms.User_Settings_Editor_Screen
{
    public partial class Screen_User_Settings_Editor
    {
        /// <summary>
        /// Sets the Category for the Language Drop Down Menu with its set of Colors
        /// </summary>
        /// <remarks>Dropdown Menu Visual</remarks>
        private void ComboBox_Language_List_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                string langListText = string.Empty;

                if (sender is ComboBox cb)
                {
                    if (e.Index != -1 && cb.Items != null)
                    {
                        if (cb.Items[e.Index] is Json_List_Language si)
                        {
                            langListText = si.Name;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(langListText) && sender != null)
                {
                    Font font = ((ComboBox)sender).Font;
                    Brush backgroundColor;
                    Brush textColor;

                    if (langListText.StartsWith("<GROUP>"))
                    {
                        font = new Font(font, FontStyle.Bold);
                        e.Graphics.FillRectangle(new SolidBrush(Color_Winform_Other.DropMenu_Category_Background_ForeColor), e.Bounds);
                        e.Graphics.DrawString(langListText.Replace("<GROUP>", string.Empty), font,
                            new SolidBrush(Color_Winform_Other.DropMenu_Category_Text_ForeColor), e.Bounds);
                    }
                    else
                    {
                        font = new Font(font, FontStyle.Bold);
                        if ((e.State & DrawItemState.Selected) == DrawItemState.Selected && e.State != DrawItemState.ComboBoxEdit)
                        {
                            backgroundColor = SystemBrushes.Highlight;
                            textColor = SystemBrushes.HighlightText;
                        }
                        else
                        {
                            backgroundColor = new SolidBrush(Color_Winform_Other.DropMenu_Background_ForeColor);
                            textColor = new SolidBrush(Color_Winform_Other.DropMenu_Text_ForeColor);
                        }

                        e.Graphics.FillRectangle(backgroundColor, e.Bounds);
                        e.Graphics.DrawString("    " + langListText, font, textColor, e.Bounds);
                    }
                }
            }
            catch { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Greenbutton_hover_MouseEnter(object sender, EventArgs e)
        {
            Button_Save.Image = Image_Button.Green_Hover;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Greenbutton_MouseLeave(object sender, EventArgs e)
        {
            Button_Save.Image = Image_Button.Green;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Greenbutton_hover_MouseUp(object sender, EventArgs e)
        {
            Button_Save.Image = Image_Button.Green_Hover;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Greenbutton_click_MouseDown(object sender, EventArgs e)
        {
            Button_Save.Image = Image_Button.Green_Click;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Graybutton_click_MouseDown(object sender, EventArgs e)
        {
            Button_Cancel.Image = Image_Button.Grey_Click;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Graybutton_hover_MouseEnter(object sender, EventArgs e)
        {
            Button_Cancel.Image = Image_Button.Grey_Hover;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Graybutton_MouseLeave(object sender, EventArgs e)
        {
            Button_Cancel.Image = Image_Button.Grey;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Graybutton_hover_MouseUp(object sender, EventArgs e)
        {
            Button_Cancel.Image = Image_Button.Grey_Hover;
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetVisuals()
        {
            /*******************************/
            /* Set Initial Position         /
            /*******************************/

            FunctionStatus.CenterParent(this);

            /*******************************/
            /* Set Window Name              /
            /*******************************/

            Text = "SBRW UserSettings XML Editor";

            /*******************************/
            /* Set Background Image         /
            /*******************************/

            BackgroundImage = Image_Background.User_XML_Settings;
            TransparencyKey = Color_Screen.BG_User_XML_Editor;

            /********************************/
            /* Set Hardcoded Text and Values /
            /********************************/

            labelLauncherVersion.Text = "Version: " + Application.ProductVersion;
            //labelOverRideAspect.Text = XML_File.XML_Settings_Data.PostProcessingEnable;

            /*******************************/
            /* Set Font                     /
            /*******************************/
#if !(RELEASE_UNIX || DEBUG_UNIX)
            float MainFontSize = 9f * 96f / CreateGraphics().DpiY;
            float SecondaryFontSize = 8f * 96f / CreateGraphics().DpiY;
#else
            float MainFontSize = 9f;
            float SecondaryFontSize = 8f;
#endif
            Font = new Font(FormsFont.Primary(), SecondaryFontSize, FontStyle.Bold);

            labelVideoOptions.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Button_Save.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            Button_Cancel.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            /* Titles */
            labelVideoOptions.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold | FontStyle.Underline);
            labelAudioOptions.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold | FontStyle.Underline);
            labelVolumeControl.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold | FontStyle.Underline);
            labelGameplayOptions.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold | FontStyle.Underline);
            labelShaderDetails.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold | FontStyle.Underline);
            labelWorldDetails.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold | FontStyle.Underline);
            labelCarDetail.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold | FontStyle.Underline);
            labelBaseTextures.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold | FontStyle.Underline);
            LabelGraphicPreset.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold | FontStyle.Underline);
            /* Sub-Titles */
            Label_Rain.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelPerfLevel.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelResolution.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelBrightness.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelWindowed.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelEnableAero.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelVSync.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelPixelAspect.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            //labelOverRideAspect.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelAudioMode.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelAudioQuality.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelMasterVol.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelSFXVol.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelCarVol.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelSpeechVol.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelGameMusicVol.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelFEMusicVol.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelGPOCamera.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelGPOTrans.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelGPODamage.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelGPOUnits.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelFSAA.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelMotionBlur.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelOverbright.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelPostProc.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelPartSys.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelShadowDetail.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelShaderDetail.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelVisTreat.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelWaterSimulation.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelGlobalDetail.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelSkidMarks.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelRoadReflection.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelRoadTexture.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelRoadLODBias.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelRoadAniso.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelCarEnvMap.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelCDLODBias.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelFilterLvl.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelBTLODBias.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelAntialiasing.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            labelLauncherVersion.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            /* Radio Buttons */
            radioWindowedOn.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioWindowedOff.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioAeroOn.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioAeroOff.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioVSyncOn.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioVSyncOff.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioAQLow.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioAQHigh.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioDamageOn.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioDamageOff.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioKmH.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioMpH.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioMotionBlurOn.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioMotionBlurOff.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioOverBrightOn.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioOverBrightOff.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioPostProcOn.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioPostProcOff.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioParticleSysOn.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioParticleSysOff.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioVisualTreatOn.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioVisualTreatOff.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioWaterSimulationOn.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioWaterSimulationOff.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioMaxSkidMarksZero.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioMaxSkidMarksOne.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioMaxSkidMarksTwo.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioRoadLODBiasOn.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioRoadLODBiasOff.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioCarDetailLODOn.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioCarDetailLODOff.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioBaseTextureLODOn.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioBaseTextureLODOff.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioButton_Rain_On.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            radioButton_Rain_Off.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            /* Preset Radio Buttons */
            PresetButtonMin.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            PresetButtonLow.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            PresetButtonMed.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            PresetButtonHigh.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            PresetButtonMax.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            PresetButtonCustom.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            /* Input Boxes */
            numericResWidth.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            numericResHeight.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            numericBrightness.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            numericMVol.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            numericSFxVol.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            numericCarVol.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            numericSpeech.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            numericGMusic.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            numericFEMusic.Font = new Font(FormsFont.Primary_Bold(), MainFontSize, FontStyle.Bold);
            /* DropDown Menus */
            comboBoxPerformanceLevel.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            comboAudioMode.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            comboBoxCamera.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            comboBoxTransmisson.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            comboBoxShaderFSAA.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            comboBoxShadowDetail.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            comboBoxShaderDetail.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            comboBoxWorldGlobalDetail.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            comboBoxWorldRoadReflection.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            comboBoxWorldRoadTexture.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            comboBoxWorldRoadAniso.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            comboBoxCarEnvironmentDetail.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            comboBoxBaseTextureFilter.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            comboBoxAnisotropicLevel.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);
            ComboBox_Language_List.Font = new Font(FormsFont.Primary_Bold(), SecondaryFontSize, FontStyle.Bold);

            /********************************/
            /* Set Theme Colors & Images     /
            /********************************/
            /* Buttons */
            Button_Save.Text = FileReadOnly ? "READ-ONLY" : "SAVE";
            Button_Save.ForeColor = FileReadOnly ? Color_Winform.Error_Text_Fore_Color : Color_Text.L_Seven;
            Button_Save.Image = Image_Button.Green;
            Button_Cancel.Image = Image_Button.Grey;
            Button_Cancel.ForeColor = Color_Text.L_Five;
            /* Titles */
            labelVideoOptions.ForeColor = Color_Winform.Secondary_Text_Fore_Color;
            labelAudioOptions.ForeColor = Color_Winform.Secondary_Text_Fore_Color;
            labelVolumeControl.ForeColor = Color_Winform.Secondary_Text_Fore_Color;
            labelGameplayOptions.ForeColor = Color_Winform.Secondary_Text_Fore_Color;
            labelShaderDetails.ForeColor = Color_Winform.Secondary_Text_Fore_Color;
            labelWorldDetails.ForeColor = Color_Winform.Secondary_Text_Fore_Color;
            labelCarDetail.ForeColor = Color_Winform.Secondary_Text_Fore_Color;
            labelBaseTextures.ForeColor = Color_Winform.Secondary_Text_Fore_Color;
            LabelGraphicPreset.ForeColor = Color_Winform.Secondary_Text_Fore_Color;
            /* Sub-Titles */
            Label_Rain.ForeColor = Color_Winform.Text_Fore_Color;
            labelPerfLevel.ForeColor = Color_Winform_Other.Link;
            labelResolution.ForeColor = Color_Winform.Text_Fore_Color;
            labelBrightness.ForeColor = Color_Winform.Text_Fore_Color;
            labelWindowed.ForeColor = Color_Winform.Text_Fore_Color;
            labelEnableAero.ForeColor = Color_Winform.Text_Fore_Color;
            labelVSync.ForeColor = Color_Winform.Text_Fore_Color;
            labelPixelAspect.ForeColor = Color_Winform.Text_Fore_Color;
            labelAudioMode.ForeColor = Color_Winform.Text_Fore_Color;
            labelAudioQuality.ForeColor = Color_Winform.Text_Fore_Color;
            labelMasterVol.ForeColor = Color_Winform.Text_Fore_Color;
            labelSFXVol.ForeColor = Color_Winform.Text_Fore_Color;
            labelCarVol.ForeColor = Color_Winform.Text_Fore_Color;
            labelSpeechVol.ForeColor = Color_Winform.Text_Fore_Color;
            labelGameMusicVol.ForeColor = Color_Winform.Text_Fore_Color;
            labelFEMusicVol.ForeColor = Color_Winform.Text_Fore_Color;
            labelGPOCamera.ForeColor = Color_Winform.Text_Fore_Color;
            labelGPOTrans.ForeColor = Color_Winform.Text_Fore_Color;
            labelGPODamage.ForeColor = Color_Winform.Text_Fore_Color;
            labelGPOUnits.ForeColor = Color_Winform.Text_Fore_Color;
            labelFSAA.ForeColor = Color_Winform.Text_Fore_Color;
            labelMotionBlur.ForeColor = Color_Winform.Text_Fore_Color;
            labelOverbright.ForeColor = Color_Winform.Text_Fore_Color;
            labelPostProc.ForeColor = Color_Winform.Text_Fore_Color;
            labelPartSys.ForeColor = Color_Winform.Text_Fore_Color;
            labelShadowDetail.ForeColor = Color_Winform.Text_Fore_Color;
            labelShaderDetail.ForeColor = Color_Winform.Text_Fore_Color;
            labelVisTreat.ForeColor = Color_Winform.Text_Fore_Color;
            labelWaterSimulation.ForeColor = Color_Winform.Text_Fore_Color;
            labelGlobalDetail.ForeColor = Color_Winform.Text_Fore_Color;
            labelSkidMarks.ForeColor = Color_Winform.Text_Fore_Color;
            labelRoadReflection.ForeColor = Color_Winform.Text_Fore_Color;
            labelRoadTexture.ForeColor = Color_Winform.Text_Fore_Color;
            labelRoadLODBias.ForeColor = Color_Winform.Text_Fore_Color;
            labelRoadAniso.ForeColor = Color_Winform.Text_Fore_Color;
            labelCarEnvMap.ForeColor = Color_Winform.Text_Fore_Color;
            labelCDLODBias.ForeColor = Color_Winform.Text_Fore_Color;
            labelFilterLvl.ForeColor = Color_Winform.Text_Fore_Color;
            labelBTLODBias.ForeColor = Color_Winform.Text_Fore_Color;
            labelAntialiasing.ForeColor = Color_Winform.Text_Fore_Color;
            labelLauncherVersion.ForeColor = Color_Winform.Text_Fore_Color;
            /* Radio Buttons */
            radioWindowedOn.ForeColor = Color_Winform.Text_Fore_Color;
            radioWindowedOff.ForeColor = Color_Winform.Text_Fore_Color;
            radioAeroOn.ForeColor = Color_Winform.Text_Fore_Color;
            radioAeroOff.ForeColor = Color_Winform.Text_Fore_Color;
            radioVSyncOn.ForeColor = Color_Winform.Text_Fore_Color;
            radioVSyncOff.ForeColor = Color_Winform.Text_Fore_Color;
            radioAQLow.ForeColor = Color_Winform.Text_Fore_Color;
            radioAQHigh.ForeColor = Color_Winform.Text_Fore_Color;
            radioDamageOn.ForeColor = Color_Winform.Text_Fore_Color;
            radioDamageOff.ForeColor = Color_Winform.Text_Fore_Color;
            radioKmH.ForeColor = Color_Winform.Text_Fore_Color;
            radioMpH.ForeColor = Color_Winform.Text_Fore_Color;
            radioMotionBlurOn.ForeColor = Color_Winform.Text_Fore_Color;
            radioMotionBlurOff.ForeColor = Color_Winform.Text_Fore_Color;
            radioOverBrightOn.ForeColor = Color_Winform.Text_Fore_Color;
            radioOverBrightOff.ForeColor = Color_Winform.Text_Fore_Color;
            radioPostProcOn.ForeColor = Color_Winform.Text_Fore_Color;
            radioPostProcOff.ForeColor = Color_Winform.Text_Fore_Color;
            radioParticleSysOn.ForeColor = Color_Winform.Text_Fore_Color;
            radioParticleSysOff.ForeColor = Color_Winform.Text_Fore_Color;
            radioVisualTreatOn.ForeColor = Color_Winform.Text_Fore_Color;
            radioVisualTreatOff.ForeColor = Color_Winform.Text_Fore_Color;
            radioWaterSimulationOn.ForeColor = Color_Winform.Text_Fore_Color;
            radioWaterSimulationOff.ForeColor = Color_Winform.Text_Fore_Color;
            radioMaxSkidMarksZero.ForeColor = Color_Winform.Text_Fore_Color;
            radioMaxSkidMarksOne.ForeColor = Color_Winform.Text_Fore_Color;
            radioMaxSkidMarksTwo.ForeColor = Color_Winform.Text_Fore_Color;
            radioRoadLODBiasOn.ForeColor = Color_Winform.Text_Fore_Color;
            radioRoadLODBiasOff.ForeColor = Color_Winform.Text_Fore_Color;
            radioCarDetailLODOn.ForeColor = Color_Winform.Text_Fore_Color;
            radioCarDetailLODOff.ForeColor = Color_Winform.Text_Fore_Color;
            radioBaseTextureLODOn.ForeColor = Color_Winform.Text_Fore_Color;
            radioBaseTextureLODOff.ForeColor = Color_Winform.Text_Fore_Color;
            radioButton_Rain_On.ForeColor = Color_Winform.Text_Fore_Color;
            radioButton_Rain_Off.ForeColor = Color_Winform.Text_Fore_Color;
            /* Preset Radio Buttons */
            PresetButtonMin.ForeColor = Color_Winform.Text_Fore_Color;
            PresetButtonLow.ForeColor = Color_Winform.Text_Fore_Color;
            PresetButtonMed.ForeColor = Color_Winform.Text_Fore_Color;
            PresetButtonHigh.ForeColor = Color_Winform.Text_Fore_Color;
            PresetButtonMax.ForeColor = Color_Winform.Text_Fore_Color;
            PresetButtonCustom.ForeColor = Color_Winform.Text_Fore_Color;
            /* Input Boxes */
            numericResWidth.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            numericResWidth.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            numericResHeight.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            numericResHeight.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            numericBrightness.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            numericBrightness.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            numericMVol.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            numericMVol.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            numericSFxVol.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            numericSFxVol.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            numericCarVol.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            numericCarVol.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            numericSpeech.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            numericSpeech.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            numericGMusic.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            numericGMusic.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            numericFEMusic.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            numericFEMusic.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            /* DropDown Menus */
            comboBoxPerformanceLevel.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            comboBoxPerformanceLevel.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            comboResolutions.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            comboResolutions.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            comboAudioMode.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            comboAudioMode.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            comboBoxCamera.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            comboBoxCamera.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            comboBoxTransmisson.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            comboBoxTransmisson.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            comboBoxShaderFSAA.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            comboBoxShaderFSAA.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            comboBoxShadowDetail.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            comboBoxShadowDetail.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            comboBoxShaderDetail.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            comboBoxShaderDetail.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            comboBoxWorldGlobalDetail.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            comboBoxWorldGlobalDetail.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            comboBoxWorldRoadReflection.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            comboBoxWorldRoadReflection.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            comboBoxWorldRoadTexture.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            comboBoxWorldRoadTexture.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            comboBoxWorldRoadAniso.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            comboBoxWorldRoadAniso.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            comboBoxCarEnvironmentDetail.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            comboBoxCarEnvironmentDetail.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            comboBoxBaseTextureFilter.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            comboBoxBaseTextureFilter.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;
            comboBoxAnisotropicLevel.ForeColor = Color_Winform_Other.DropMenu_Text_ForeColor;
            comboBoxAnisotropicLevel.BackColor = Color_Winform_Other.DropMenu_Background_ForeColor;

            /* Tabs Global Background Color */
            TabControl_USXE.BackColor = Color.FromArgb(22, 29, 38);
            /* Tabs (Menu) Text Color */
            TabControl_USXE.ForeColor = Color.FromArgb(192, 192, 192);
            /* Tabs Current Selected & Hover Menu Tab */
            TabControl_USXE.SelectedTabColor = Color.FromArgb(128, 44, 58, 76);
            /* Tabs Other Menu Tab */
            TabControl_USXE.TabColor = Color.FromArgb(44, 58, 76);

            /*******************************/
            /* Comboboxes                   /
            /*******************************/

            /* Transmisson ComboBox */
            var TransmissonList = new[] {
                new { SaveTheManuals = "Automatic", Value = "0" },
                new { SaveTheManuals = "Manual", Value = "1" }
            };
            comboBoxTransmisson.DisplayMember = "SaveTheManuals";
            comboBoxTransmisson.ValueMember = "Value";
            comboBoxTransmisson.DataSource = TransmissonList;

            /* AudioMode ComboBox */
            var AudioModeList = new[] {
                new { Sound = "Stereo", Value = "1" },
                new { Sound = "Surround", Value = "2" }
            };
            comboAudioMode.DisplayMember = "Sound";
            comboAudioMode.ValueMember = "Value";
            comboAudioMode.DataSource = AudioModeList;

            /* CameraPOV ComboBox */
            var CameraPOVList = new[] {
                new { CameraPOV = "Bumper", Value = "0" },
                new { CameraPOV = "Hood", Value = "1" },
                new { CameraPOV = "Near", Value = "2" },
                new { CameraPOV = "Far", Value = "3" }
            };
            comboBoxCamera.DisplayMember = "CameraPOV";
            comboBoxCamera.ValueMember = "Value";
            comboBoxCamera.DataSource = CameraPOVList;

            /* PerformanceLevel ComboBox */
            var PerformanceLevelList = new[] {
                new { PerformanceLevel = "Minimum", Value = "0" },
                new { PerformanceLevel = "Low", Value = "1" },
                new { PerformanceLevel = "Medium", Value = "2" },
                new { PerformanceLevel = "High", Value = "3" },
                new { PerformanceLevel = "Maximum", Value = "4" },
                new { PerformanceLevel = "Custom", Value = "5" }
            };
            comboBoxPerformanceLevel.DisplayMember = "PerformanceLevel";
            comboBoxPerformanceLevel.ValueMember = "Value";
            comboBoxPerformanceLevel.DataSource = PerformanceLevelList;

            /* BaseTextureFilter ComboBox */
            var BaseTextureFilterList = new[] {
                new { BaseTextureFilter = "Bilinear", Value = "0" },
                new { BaseTextureFilter = "Trilinear", Value = "1" },
                new { BaseTextureFilter = "Anisotropic", Value = "2" }
            };
            comboBoxBaseTextureFilter.DisplayMember = "BaseTextureFilter";
            comboBoxBaseTextureFilter.ValueMember = "Value";
            comboBoxBaseTextureFilter.DataSource = BaseTextureFilterList;

            /* AnisotropicLevel ComboBox */
            var AnisotropicLevelList = new[] {
                new { AnisotropicLevel = "None", Value = "0" },
                new { AnisotropicLevel = "2x", Value = "2" },
                new { AnisotropicLevel = "4x", Value = "4" },
                new { AnisotropicLevel = "8x", Value = "8" },
                new { AnisotropicLevel = "16x", Value = "16" }
            };
            comboBoxAnisotropicLevel.DisplayMember = "AnisotropicLevel";
            comboBoxAnisotropicLevel.ValueMember = "Value";
            comboBoxAnisotropicLevel.DataSource = AnisotropicLevelList;

            /* CarEnvironmentDetail ComboBox */
            var CarEnvironmentDetailList = new[] {
                new { CarEnvironmentDetail = "Minimum", Value = "0" },
                new { CarEnvironmentDetail = "Low", Value = "1" },
                new { CarEnvironmentDetail = "Medium", Value = "2" },
                new { CarEnvironmentDetail = "High", Value = "3" },
                new { CarEnvironmentDetail = "Maximum", Value = "4" }
            };
            comboBoxCarEnvironmentDetail.DisplayMember = "CarEnvironmentDetail";
            comboBoxCarEnvironmentDetail.ValueMember = "Value";
            comboBoxCarEnvironmentDetail.DataSource = CarEnvironmentDetailList;

            /* WorldGlobalDetail ComboBox */
            var WorldGlobalDetailList = new[] {
                new { WorldGlobalDetail = "Minimum", Value = "0" },
                new { WorldGlobalDetail = "Low", Value = "1" },
                new { WorldGlobalDetail = "Medium", Value = "2" },
                new { WorldGlobalDetail = "High", Value = "3" },
                new { WorldGlobalDetail = "Maximum", Value = "4" }
            };
            comboBoxWorldGlobalDetail.DisplayMember = "WorldGlobalDetail";
            comboBoxWorldGlobalDetail.ValueMember = "Value";
            comboBoxWorldGlobalDetail.DataSource = WorldGlobalDetailList;

            /* WorldRoadReflection ComboBox */
            var WorldRoadReflectionList = new[] {
                new { WorldRoadReflection = "Minimum", Value = "0" },
                new { WorldRoadReflection = "Medium", Value = "1" },
                new { WorldRoadReflection = "Maximum", Value = "2" }
            };
            comboBoxWorldRoadReflection.DisplayMember = "WorldRoadReflection";
            comboBoxWorldRoadReflection.ValueMember = "Value";
            comboBoxWorldRoadReflection.DataSource = WorldRoadReflectionList;

            /* WorldRoadTexture ComboBox */
            var WorldRoadTextureList = new[] {
                new { WorldRoadTexture = "Minimum", Value = "0" },
                new { WorldRoadTexture = "Medium", Value = "1" },
                new { WorldRoadTexture = "Maximum", Value = "2" }
            };
            comboBoxWorldRoadTexture.DisplayMember = "WorldRoadTexture";
            comboBoxWorldRoadTexture.ValueMember = "Value";
            comboBoxWorldRoadTexture.DataSource = WorldRoadTextureList;

            /* WorldRoadAniso ComboBox */
            var WorldRoadAnisoList = new[] {
                new { WorldRoadAniso = "None", Value = "0" },
                new { WorldRoadAniso = "2x", Value = "2" },
                new { WorldRoadAniso = "4x", Value = "4" },
                new { WorldRoadAniso = "8x", Value = "8" },
                new { WorldRoadAniso = "16x", Value = "16" }
            };
            comboBoxWorldRoadAniso.DisplayMember = "WorldRoadAniso";
            comboBoxWorldRoadAniso.ValueMember = "Value";
            comboBoxWorldRoadAniso.DataSource = WorldRoadAnisoList;

            /* ShaderFSAA ComboBox */
            var ShaderFSAAList = new[] {
                new { ShaderFSAA = "Off", Value = "0" },
                new { ShaderFSAA = "2x", Value = "2" },
                new { ShaderFSAA = "4x", Value = "4" }
            };
            comboBoxShaderFSAA.DisplayMember = "ShaderFSAA";
            comboBoxShaderFSAA.ValueMember = "Value";
            comboBoxShaderFSAA.DataSource = ShaderFSAAList;

            /* ShadowDetail ComboBox */
            var ShadowDetailList = new[] {
                new { ShadowDetail = "Low", Value = "0" },
                new { ShadowDetail = "Medium", Value = "1" },
                new { ShadowDetail = "High", Value = "2" }
            };
            comboBoxShadowDetail.DisplayMember = "ShadowDetail";
            comboBoxShadowDetail.ValueMember = "Value";
            comboBoxShadowDetail.DataSource = ShadowDetailList;

            /* ShaderDetail ComboBox */
            var ShaderDetailList = new[] {
                new { ShaderDetail = "Minimum", Value = "0" },
                new { ShaderDetail = "Low", Value = "1" },
                new { ShaderDetail = "Medium", Value = "2" },
                new { ShaderDetail = "High", Value = "4" }
            };
            comboBoxShaderDetail.DisplayMember = "ShaderDetail";
            comboBoxShaderDetail.ValueMember = "Value";
            comboBoxShaderDetail.DataSource = ShaderDetailList;

            ComboBox_Language_List.DisplayMember = "Name";
            ComboBox_Language_List.DataSource = LanguageListUpdater.CleanList;

            try
            {
                ResolutionsListLoaded = true;
                comboResolutions.DisplayMember = "Resolution";
                comboResolutions.DataSource = ResolutionsListUpdater.List;
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("Resolution", String.Empty, Error, String.Empty, true);
            }

            /********************************/
            /* Events                        /
            /********************************/

            Button_Save.MouseEnter += new EventHandler(Greenbutton_hover_MouseEnter);
            Button_Save.MouseLeave += new EventHandler(Greenbutton_MouseLeave);
            Button_Save.MouseUp += new MouseEventHandler(Greenbutton_hover_MouseUp);
            Button_Save.MouseDown += new MouseEventHandler(Greenbutton_click_MouseDown);

            Button_Cancel.MouseEnter += new EventHandler(Graybutton_hover_MouseEnter);
            Button_Cancel.MouseLeave += new EventHandler(Graybutton_MouseLeave);
            Button_Cancel.MouseUp += new MouseEventHandler(Graybutton_hover_MouseUp);
            Button_Cancel.MouseDown += new MouseEventHandler(Graybutton_click_MouseDown);

            comboBoxPerformanceLevel.SelectedIndexChanged += new EventHandler(ComboBoxPerformanceLevel_SelectedIndexChanged);

            PresetButtonMin.CheckedChanged += new EventHandler(PresetButtonMin_CheckedChanged);
            PresetButtonLow.CheckedChanged += new EventHandler(PresetButtonLow_CheckedChanged);
            PresetButtonMed.CheckedChanged += new EventHandler(PresetButtonMed_CheckedChanged);
            PresetButtonHigh.CheckedChanged += new EventHandler(PresetButtonHigh_CheckedChanged);
            PresetButtonMax.CheckedChanged += new EventHandler(PresetButtonMax_CheckedChanged);
            PresetButtonCustom.CheckedChanged += new EventHandler(PresetButtonCustom_CheckedChanged);

            TabControl_USXE.SelectedIndexChanged += (x, y) =>
            {
                if (TabControl_USXE.SelectedTab.Equals(TabPage_Advanced))
                {
                    LabelGraphicPreset.Visible = PresetPanel.Visible = true;
                }
                else
                {
                    LabelGraphicPreset.Visible = PresetPanel.Visible = false;
                }
            };

            ComboBox_Language_List.DrawItem += new DrawItemEventHandler(ComboBox_Language_List_DrawItem);
            ComboBox_Language_List.SelectedIndexChanged += new EventHandler(ComboBox_Language_List_SelectedIndexChanged);
            //ComboBox_Language_List.MouseWheel += new MouseEventHandler(DropDownMenu_MouseWheel);

            /*********************************************************************/
            /* Set Drop Down, Radio, Input Boxes, and Set Window Size and Postion /
            /*********************************************************************/

            if (ResolutionsListLoaded == true)
            {
                comboResolutions.Visible = true;
            }
            else
            {
                comboResolutions.Visible = false;
            }

            numericResWidth.Value = Convert.ToInt32(XML_File.XML_Settings_Data.ScreenWidth);
            numericResHeight.Value = Convert.ToInt32(XML_File.XML_Settings_Data.ScreenHeight);
            numericBrightness.Value = Convert.ToDecimal(XML_File.XML_Settings_Data.Brightness);
            numericMVol.Value = ConvertDecimalToWholeNumber(XML_File.XML_Settings_Data.MasterAudio);
            numericSFxVol.Value = ConvertDecimalToWholeNumber(XML_File.XML_Settings_Data.SFXAudio);
            numericCarVol.Value = ConvertDecimalToWholeNumber(XML_File.XML_Settings_Data.CarAudio);
            numericSpeech.Value = ConvertDecimalToWholeNumber(XML_File.XML_Settings_Data.SpeechAudio);
            numericGMusic.Value = ConvertDecimalToWholeNumber(XML_File.XML_Settings_Data.MusicAudio);
            numericFEMusic.Value = ConvertDecimalToWholeNumber(XML_File.XML_Settings_Data.FreeroamAudio);

            comboBoxTransmisson.SelectedIndex = CheckValidRange("Transmission", "0-1", XML_File.XML_Settings_Data.Transmission);
            comboAudioMode.SelectedIndex = CheckValidRange("AudioMode", "0-2", XML_File.XML_Settings_Data.AudioMode);
            comboBoxCamera.SelectedIndex = CheckValidRange("Camera", "0-3", XML_File.XML_Settings_Data.CameraPOV);
            comboBoxPerformanceLevel.SelectedIndex = CheckValidRange("PerformanceLevel", "0-5", XML_File.XML_Settings_Data.PerformanceLevel);

            if (XML_File.XML_Settings_Data.ScreenWindowed == "0")
            {
                radioWindowedOff.Checked = true;
            }
            else
            {
                radioWindowedOn.Checked = true;
            }

            if (XML_File.XML_Settings_Data.EnableAero == "0")
            {
                radioAeroOff.Checked = true;
            }
            else
            {
                radioAeroOn.Checked = true;
            }

            if (XML_File.XML_Settings_Data.VSyncOn == "0")
            {
                radioVSyncOff.Checked = true;
            }
            else
            {
                radioVSyncOn.Checked = true;
            }

            if (XML_File.XML_Settings_Data.AudioQuality == "0")
            {
                radioAQLow.Checked = true;
            }
            else
            {
                radioAQHigh.Checked = true;
            }

            if (XML_File.XML_Settings_Data.Damage == "0")
            {
                radioDamageOff.Checked = true;
            }
            else
            {
                radioDamageOn.Checked = true;
            }

            if (XML_File.XML_Settings_Data.SpeedUnits == "0")
            {
                radioKmH.Checked = true;
            }
            else
            {
                radioMpH.Checked = true;
            }

            if (XML_File.XML_Settings_Data.RainEnable == "0")
            {
                radioButton_Rain_Off.Checked = true;
            }
            else
            {
                radioButton_Rain_On.Checked = true;
            }

            string SavedResolution = XML_File.XML_Settings_Data.ScreenWidth + "x" + XML_File.XML_Settings_Data.ScreenHeight;
            if (!string.IsNullOrWhiteSpace(SavedResolution))
            {
                try
                {
                    if (ResolutionsListUpdater.List.FindIndex(i => string.Equals(i.Resolution, SavedResolution)) != 0)
                    {
                        int Index = ResolutionsListUpdater.List.FindIndex(i => string.Equals(i.Resolution, SavedResolution));

                        if (Index >= 0)
                        {
                            comboResolutions.SelectedIndex = Index;
                        }
                    }
                    else
                    {
                        comboResolutions.SelectedIndex = 1;
                    }
                }
                catch (Exception Error)
                {
                    LogToFileAddons.OpenLog("USXE Resolution", String.Empty, Error, String.Empty, true);
                }
            }

            /*******************************/
            /* Set ToolTip Texts            /
            /*******************************/

            Hover.SetToolTip(PresetButtonMin, "Preset: Minimum Graphics");
            Hover.SetToolTip(PresetButtonLow, "Preset: Low Graphics");
            Hover.SetToolTip(PresetButtonMed, "Preset: Medium Graphics");
            Hover.SetToolTip(PresetButtonHigh, "Preset: High Graphics");
            Hover.SetToolTip(PresetButtonMax, "Preset: Max Graphics");
            Hover.SetToolTip(PresetButtonCustom, "Preset: Saved Graphics");
            Hover.SetToolTip(labelGPOTrans, "Only Applies to \"Drag\" Event control");
            Hover.SetToolTip(labelPixelAspect, "Control Aspect Ratio:\n" +
                "0: Default POV Ratio\n" +
                "0 -> 100 : Condense POV to Center\n" +
                "0 -> -100 : Stretch POV from Center");

            Shown += (x, y) =>
            {
                Shown_Setup();
            };
        }
    }
}
