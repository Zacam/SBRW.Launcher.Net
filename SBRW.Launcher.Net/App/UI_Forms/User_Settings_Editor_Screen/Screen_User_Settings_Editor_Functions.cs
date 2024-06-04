using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.Core.Extra.XML_;
using SBRW.Launcher.RunTime.LauncherCore.Lists;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using System;

namespace SBRW.Launcher.App.UI_Forms.User_Settings_Editor_Screen
{
    public partial class Screen_User_Settings_Editor
    {
        /// <summary>
        /// 
        /// </summary>
        private void RememberLastLanguage()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(XML_File.XML_Settings_Data.Language))
                {
                    ComboBox_Language_List.SelectedIndex =
                        LanguageListUpdater.CleanList.FindIndex(i => Equals(i.Value_XML, XML_File.XML_Settings_Data.Language.Trim()));
                }
                else
                {
                    Log.Warning("GAME SETTINGS LANGLIST: Unable to find anything, assuming default");
                    ComboBox_Language_List.SelectedIndex = 1;
                }
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("GAME SETTINGS LANGLIST", string.Empty, Error, string.Empty, true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Preset"></param>
        private void SetValues(int Preset)
        {
            PresetLoaded = false;

            switch (Preset)
            {
                case 0:
                    /* Minimal */
                    PresetButtonMin.Checked = true;

                    comboBoxBaseTextureFilter.SelectedIndex = 0;
                    comboBoxAnisotropicLevel.SelectedIndex = 0;
                    comboBoxCarEnvironmentDetail.SelectedIndex = 0;
                    comboBoxWorldGlobalDetail.SelectedIndex = 0;
                    comboBoxWorldRoadReflection.SelectedIndex = 0;
                    comboBoxWorldRoadTexture.SelectedIndex = 0;
                    comboBoxWorldRoadAniso.SelectedIndex = 0;
                    comboBoxShaderFSAA.SelectedIndex = 0;
                    comboBoxShadowDetail.SelectedIndex = 0;
                    comboBoxShaderDetail.SelectedIndex = 0;

                    SetCorrectElementValues("BaseTextureLODBias", "0");
                    SetCorrectElementValues("CarEnvironmentMapEnable", "0");
                    SetCorrectElementValues("MaxSkidMarks", "0");
                    SetCorrectElementValues("RoadTextureLODBias", "0");
                    SetCorrectElementValues("MotionBlurEnable", "0");
                    SetCorrectElementValues("OverBrightEnable", "0");
                    SetCorrectElementValues("ParticleSystemEnable", "0");
                    SetCorrectElementValues("VisualTreatment", "0");
                    SetCorrectElementValues("WaterSimEnable", "0");
                    SetCorrectElementValues("PostProcessingEnable", "0");
                    SetCorrectElementValues("RainEnable", "0");

                    Log.Info("USXE: Selected Minimum Preset");
                    break;
                case 1:
                    /* Low */
                    PresetButtonLow.Checked = true;

                    comboBoxBaseTextureFilter.SelectedIndex = 0;
                    comboBoxAnisotropicLevel.SelectedIndex = 0;
                    comboBoxCarEnvironmentDetail.SelectedIndex = 1;
                    comboBoxWorldGlobalDetail.SelectedIndex = 1;
                    comboBoxWorldRoadReflection.SelectedIndex = 0;
                    comboBoxWorldRoadTexture.SelectedIndex = 0;
                    comboBoxWorldRoadAniso.SelectedIndex = 0;
                    comboBoxShaderFSAA.SelectedIndex = 0;
                    comboBoxShadowDetail.SelectedIndex = 0;
                    comboBoxShaderDetail.SelectedIndex = 1;

                    SetCorrectElementValues("BaseTextureLODBias", "0");
                    SetCorrectElementValues("CarEnvironmentMapEnable", "1");
                    SetCorrectElementValues("MaxSkidMarks", "0");
                    SetCorrectElementValues("RoadTextureLODBias", "0");
                    SetCorrectElementValues("MotionBlurEnable", "0");
                    SetCorrectElementValues("OverBrightEnable", "0");
                    SetCorrectElementValues("ParticleSystemEnable", "1");
                    SetCorrectElementValues("VisualTreatment", "0");
                    SetCorrectElementValues("WaterSimEnable", "0");
                    SetCorrectElementValues("PostProcessingEnable", "0");
                    SetCorrectElementValues("RainEnable", "0");

                    Log.Info("USXE: Selected Low Preset");
                    break;
                case 2:
                    /* Medium */
                    PresetButtonMed.Checked = true;

                    comboBoxBaseTextureFilter.SelectedIndex = 1;
                    comboBoxAnisotropicLevel.SelectedIndex = 0;
                    comboBoxCarEnvironmentDetail.SelectedIndex = 2;
                    comboBoxWorldGlobalDetail.SelectedIndex = 3;
                    comboBoxWorldRoadReflection.SelectedIndex = 1;
                    comboBoxWorldRoadTexture.SelectedIndex = 1;
                    comboBoxWorldRoadAniso.SelectedIndex = 0;
                    comboBoxShaderFSAA.SelectedIndex = 1;
                    comboBoxShadowDetail.SelectedIndex = 1;
                    comboBoxShaderDetail.SelectedIndex = 1;

                    SetCorrectElementValues("BaseTextureLODBias", "0");
                    SetCorrectElementValues("CarEnvironmentMapEnable", "2");
                    SetCorrectElementValues("MaxSkidMarks", "1");
                    SetCorrectElementValues("RoadTextureLODBias", "0");
                    SetCorrectElementValues("MotionBlurEnable", "0");
                    SetCorrectElementValues("OverBrightEnable", "1");
                    SetCorrectElementValues("ParticleSystemEnable", "1");
                    SetCorrectElementValues("VisualTreatment", "0");
                    SetCorrectElementValues("WaterSimEnable", "0");
                    SetCorrectElementValues("PostProcessingEnable", "0");
                    SetCorrectElementValues("RainEnable", "0");

                    Log.Info("USXE: Selected Medium Preset");
                    break;
                case 3:
                    /* High */
                    PresetButtonHigh.Checked = true;

                    comboBoxBaseTextureFilter.SelectedIndex = 2;
                    comboBoxAnisotropicLevel.SelectedIndex = 3;
                    comboBoxCarEnvironmentDetail.SelectedIndex = 3;
                    comboBoxWorldGlobalDetail.SelectedIndex = 2;
                    comboBoxWorldRoadReflection.SelectedIndex = 1;
                    comboBoxWorldRoadTexture.SelectedIndex = 1;
                    comboBoxWorldRoadAniso.SelectedIndex = 3;
                    comboBoxShaderFSAA.SelectedIndex = 2;
                    comboBoxShadowDetail.SelectedIndex = 2;
                    comboBoxShaderDetail.SelectedIndex = 2;

                    SetCorrectElementValues("BaseTextureLODBias", "0");
                    SetCorrectElementValues("CarEnvironmentMapEnable", "2");
                    SetCorrectElementValues("MaxSkidMarks", "2");
                    SetCorrectElementValues("RoadTextureLODBias", "0");
                    SetCorrectElementValues("MotionBlurEnable", "0");
                    SetCorrectElementValues("OverBrightEnable", "1");
                    SetCorrectElementValues("ParticleSystemEnable", "1");
                    SetCorrectElementValues("VisualTreatment", "1");
                    SetCorrectElementValues("WaterSimEnable", "1");
                    SetCorrectElementValues("PostProcessingEnable", "0");
                    SetCorrectElementValues("RainEnable", "1");

                    Log.Info("USXE: Selected High Preset");
                    break;
                case 4:
                    /* Maximum */
                    PresetButtonMax.Checked = true;

                    comboBoxBaseTextureFilter.SelectedIndex = 2;
                    comboBoxAnisotropicLevel.SelectedIndex = 4;
                    comboBoxCarEnvironmentDetail.SelectedIndex = 4;
                    comboBoxWorldGlobalDetail.SelectedIndex = 4;
                    comboBoxWorldRoadReflection.SelectedIndex = 2;
                    comboBoxWorldRoadTexture.SelectedIndex = 2;
                    comboBoxWorldRoadAniso.SelectedIndex = 4;
                    comboBoxShaderFSAA.SelectedIndex = 2;
                    comboBoxShadowDetail.SelectedIndex = 2;
                    comboBoxShaderDetail.SelectedIndex = 3;

                    SetCorrectElementValues("BaseTextureLODBias", "0");
                    SetCorrectElementValues("CarEnvironmentMapEnable", "3");
                    SetCorrectElementValues("MaxSkidMarks", "2");
                    SetCorrectElementValues("RoadTextureLODBias", "0");
                    SetCorrectElementValues("MotionBlurEnable", "0");
                    SetCorrectElementValues("OverBrightEnable", "1");
                    SetCorrectElementValues("ParticleSystemEnable", "1");
                    SetCorrectElementValues("VisualTreatment", "1");
                    SetCorrectElementValues("WaterSimEnable", "1");
                    SetCorrectElementValues("PostProcessingEnable", "1");
                    SetCorrectElementValues("RainEnable", "1");

                    Log.Info("USXE: Selected Maxium Preset");
                    break;
                case 5:
                    /* Custom */
                    PresetButtonCustom.Checked = true;

                    comboBoxBaseTextureFilter.SelectedIndex = CheckValidRange("BaseTextureFilter", "0-2", XML_File.XML_Settings_Data.BaseTextureFilter);
                    comboBoxAnisotropicLevel.SelectedIndex = CheckValidRange("AnisotropicLevel", "0-4", XML_File.XML_Settings_Data.BaseTextureMaxAni);
                    comboBoxCarEnvironmentDetail.SelectedIndex = CheckValidRange("CarEnvironmentDetail", "0-4", XML_File.XML_Settings_Data.CarEnvironmentMapEnable);
                    comboBoxWorldGlobalDetail.SelectedIndex = CheckValidRange("WorldGlobalDetail", "0-4", XML_File.XML_Settings_Data.GlobalDetailLevel);
                    comboBoxWorldRoadReflection.SelectedIndex = CheckValidRange("WorldRoadReflection", "0-2", XML_File.XML_Settings_Data.RoadReflectionEnable);
                    comboBoxWorldRoadTexture.SelectedIndex = CheckValidRange("WorldRoadTexture", "0-2", XML_File.XML_Settings_Data.RoadTextureFilter);
                    comboBoxWorldRoadAniso.SelectedIndex = CheckValidRange("WorldRoadAniso", "0-4", XML_File.XML_Settings_Data.RoadTextureMaxAni);
                    comboBoxShaderFSAA.SelectedIndex = CheckValidRange("ShaderFSAA", "0-2", XML_File.XML_Settings_Data.FSAALevel);
                    comboBoxShadowDetail.SelectedIndex = CheckValidRange("ShadowDetail", "0-2", XML_File.XML_Settings_Data.ShadowDetail);
                    comboBoxShaderDetail.SelectedIndex = CheckValidRange("ShaderDetail", "0-3", XML_File.XML_Settings_Data.ShaderDetail);

                    SetCorrectElementValues("BaseTextureLODBias", XML_File.XML_Settings_Data.BaseTextureLODBias);
                    SetCorrectElementValues("CarEnvironmentMapEnable", XML_File.XML_Settings_Data.CarEnvironmentMapEnable);
                    SetCorrectElementValues("MaxSkidMarks", XML_File.XML_Settings_Data.MaxSkidMarks);
                    SetCorrectElementValues("RoadTextureLODBias", XML_File.XML_Settings_Data.RoadTextureLODBias);
                    SetCorrectElementValues("MotionBlurEnable", XML_File.XML_Settings_Data.MotionBlurEnable);
                    SetCorrectElementValues("OverBrightEnable", XML_File.XML_Settings_Data.OverBrightEnable);
                    SetCorrectElementValues("ParticleSystemEnable", XML_File.XML_Settings_Data.ParticleSystemEnable);
                    SetCorrectElementValues("VisualTreatment", XML_File.XML_Settings_Data.VisualTreatment);
                    SetCorrectElementValues("WaterSimEnable", XML_File.XML_Settings_Data.WaterSimEnable);
                    SetCorrectElementValues("PostProcessingEnable", XML_File.XML_Settings_Data.PostProcessingEnable);
                    SetCorrectElementValues("RainEnable", XML_File.XML_Settings_Data.RainEnable);

                    Log.Info("USXE: Selected Custom Preset");
                    break;
                default:
                    Log.Warning("USXE: Unknown Selected Preset");
                    break;
            }

            PresetLoaded = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Range"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        private static int CheckValidRange(string Type, string Range, string Value)
        {
            int ConvertedValue;

            try
            {
                ConvertedValue = Convert.ToInt32(Value);
            }
            catch
            {
                ConvertedValue = 0;
            }

            if (Range == "0-1")
            {
                if (ConvertedValue <= 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else if (Range == "0-2")
            {
                if (Type == "AudioMode")
                {
                    if (ConvertedValue <= 1)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    if (ConvertedValue <= 0)
                    {
                        return 0;
                    }
                    else if (ConvertedValue >= 2)
                    {
                        return 2;
                    }
                    else
                    {
                        return ConvertedValue;
                    }
                }
            }
            else if (Range == "0-3")
            {
                switch (Type)
                {
                    case "ShaderFSAA":
                        return ConvertedValue switch
                        {
                            0 => 0,
                            2 => 1,
                            _ => 3,
                        };
                    default:
                        if (ConvertedValue <= 0)
                        {
                            return 0;
                        }
                        else if (ConvertedValue >= 3)
                        {
                            return 3;
                        }
                        else
                        {
                            return ConvertedValue;
                        }
                }
            }
            else if (Range == "0-4")
            {
                switch (Type)
                {
                    case "WorldRoadAniso":
                    case "AnisotropicLevel":
                        return ConvertedValue switch
                        {
                            0 => 0,
                            2 => 1,
                            4 => 2,
                            8 => 3,
                            _ => 4,
                        };
                    case "ShaderDetail":
                        return ConvertedValue switch
                        {
                            0 => 0,
                            1 => 1,
                            2 => 2,
                            _ => 4,
                        };
                    default:
                        if (ConvertedValue <= 0)
                        {
                            return 0;
                        }
                        else if (ConvertedValue >= 4)
                        {
                            return 4;
                        }
                        else
                        {
                            return ConvertedValue;
                        }
                }
            }
            else if (Range == "0-5")
            {
                if (ConvertedValue <= 0)
                {
                    return 0;
                }
                else if (ConvertedValue >= 5)
                {
                    return 5;
                }
                else
                {
                    return ConvertedValue;
                }
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// Converts Decimal Numbers (ex. 0.52) to 52
        /// </summary>
        /// <param name="UIName"></param>
        /// <returns></returns>
        private static decimal ConvertDecimalToWholeNumber(string UIName)
        {
            decimal Value;

            try
            {
                Value = Math.Round(Convert.ToDecimal(UIName), 2) * 100;
            }
            catch
            {
                Value = 52;
            }

            if (Value >= 100)
            {
                return 100;
            }
            else if (Value <= 0)
            {
                return 0;
            }
            else
            {
                return Value;
            }
        }
        /// <summary>
        /// Check User Inputed Value and Keep in Within the Value Range of 0-100 or Round to the Nearest Whole Number
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="UIName"></param>
        /// <returns></returns>
        private static string ValidWholeNumberRange(string Type, decimal UIName)
        {
            decimal Value;

            try
            {
                Value = Math.Round(UIName, MidpointRounding.ToEven);
            }
            catch
            {
                Value = 52;
            }

            if (Type == "Brightness")
            {
                if (Value >= 100)
                {
                    return "100";
                }
                else if (Value <= 0)
                {
                    return "0";
                }
                else
                {
                    return Value.ToString();
                }
            }
            else
            {
                return Value.ToString();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        private string SelectedElement(string Type)
        {
            switch (Type)
            {
                case "MaxSkidMarks":
                    if (radioMaxSkidMarksZero.Checked)
                    {
                        return "0";
                    }
                    else if (radioMaxSkidMarksOne.Checked)
                    {
                        return "1";
                    }
                    else
                    {
                        return "2";
                    }
                default:
                    return "0";
            }
        }
        /// <summary>
        /// Check User Inputed Value and Keep in Within the Value Range of 0-1 with In-between Values
        /// </summary>
        /// <param name="UIName"></param>
        /// <returns></returns>
        private static string ValidDecimalNumberRange(decimal UIName)
        {
            decimal Value;

            try
            {
                Value = Math.Round(UIName, 1);
            }
            catch
            {
                Value = 100;
            }

            if (Value >= 100)
            {
                return "1";
            }
            else if (Value <= 0)
            {
                return "0";
            }
            else
            {
                decimal CleanValue = Value / 100;
                return CleanValue.ToString();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Element"></param>
        /// <param name="ComparisonValue"></param>
        private void SetCorrectElementValues(string Element, string ComparisonValue)
        {
            try
            {
                switch (Element)
                {
                    case "BaseTextureLODBias":
                        if (ComparisonValue == "0")
                        {
                            radioBaseTextureLODOff.Checked = true;
                        }
                        else
                        {
                            radioBaseTextureLODOn.Checked = true;
                        }
                        break;
                    case "CarEnvironmentMapEnable":
                        if (ComparisonValue == "0")
                        {
                            radioCarDetailLODOff.Checked = true;
                        }
                        else
                        {
                            radioCarDetailLODOn.Checked = true;
                        }
                        break;
                    case "MaxSkidMarks":
                        if (ComparisonValue == "0")
                        {
                            radioMaxSkidMarksZero.Checked = true;
                        }
                        else if (ComparisonValue == "1")
                        {
                            radioMaxSkidMarksOne.Checked = true;
                        }
                        else
                        {
                            radioMaxSkidMarksTwo.Checked = true;
                        }
                        break;
                    case "RoadTextureLODBias":
                        if (ComparisonValue == "0")
                        {
                            radioRoadLODBiasOff.Checked = true;
                        }
                        else
                        {
                            radioRoadLODBiasOn.Checked = true;
                        }
                        break;
                    case "MotionBlurEnable":
                        if (ComparisonValue == "0")
                        {
                            radioMotionBlurOff.Checked = true;
                        }
                        else
                        {
                            radioMotionBlurOn.Checked = true;
                        }
                        break;
                    case "OverBrightEnable":
                        if (ComparisonValue == "0")
                        {
                            radioOverBrightOff.Checked = true;
                        }
                        else
                        {
                            radioOverBrightOn.Checked = true;
                        }
                        break;
                    case "ParticleSystemEnable":
                        if (ComparisonValue == "0")
                        {
                            radioParticleSysOff.Checked = true;
                        }
                        else
                        {
                            radioParticleSysOn.Checked = true;
                        }
                        break;
                    case "VisualTreatment":
                        if (ComparisonValue == "0")
                        {
                            radioVisualTreatOff.Checked = true;
                        }
                        else
                        {
                            radioVisualTreatOn.Checked = true;
                        }
                        break;
                    case "WaterSimEnable":
                        if (ComparisonValue == "0")
                        {
                            radioWaterSimulationOff.Checked = true;
                        }
                        else
                        {
                            radioWaterSimulationOn.Checked = true;
                        }
                        break;
                    case "PostProcessingEnable":
                        if (ComparisonValue == "0")
                        {
                            radioPostProcOff.Checked = true;
                        }
                        else
                        {
                            radioPostProcOn.Checked = true;
                        }
                        break;
                    case "RainEnable":
                        if (ComparisonValue == "0")
                        {
                            radioButton_Rain_Off.Checked = true;
                        }
                        else
                        {
                            radioButton_Rain_On.Checked = true;
                        }
                        break;
                    default:
                        Log.Error("USXE: Unknown Function Call [Element: '" + Element + "' ComparisonValue: '" + ComparisonValue + "']");
                        break;

                }
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("USXE", String.Empty, Error, String.Empty, true);
            }
        }
    }
}
