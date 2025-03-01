﻿using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.Support;
using SBRW.Launcher.Core.Extension.Logging_;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SBRW.Launcher.App.UI_Forms.Parent_Screen;

namespace SBRW.Launcher.RunTime.LauncherCore.Logger
{
    public static class LogToFileAddons
    {
        public static string OpenLogMessage { get; set; } = "Would you Like to Open the Launcher Error Log and Send it to a Support Channel?" +
                    "\nThis would be Useful for Fixing Issues and Potential Solutions";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="From"></param>
        /// <param name="MessageDetails"></param>
        /// <param name="Error"></param>
        /// <param name="Icon"></param>
        /// <param name="Suppress"></param>
        /// <param name="Window_Handle"></param>
        /// <param name="Ignore_Log_Alert"></param>
#if (DEBUG_UNIX || RELEASE_UNIX)
        [Obsolete("Unix Builds has issues with this, use alternative methods")]
#endif
        public static void OpenLog(this string From, string MessageDetails, Exception Error, string Icon, bool Suppress = true, bool Ignore_Log_Alert = false)
        {
            if (Error != default)
            {
                /* Call Garbage Collection since it will release the Exception Bubble buildup */
                GarbageCollections.Cleanup();
            }

            bool Core_File = File.Exists(Path.Combine(Locations.LauncherFolder, "SBRW.Launcher.Core.dll"));
            if (Core_File)
            {
                Log_Detail.Full(From, Error, Ignore_Log_Alert);
            }

            if (!Suppress)
            {
                MessageBoxIcon IconBox = Icon switch
                {
                    "Error" => MessageBoxIcon.Error,
                    "Exclamation" => MessageBoxIcon.Exclamation,
                    "Information" => MessageBoxIcon.Information,
                    "Warning" => MessageBoxIcon.Warning,
                    _ => MessageBoxIcon.None,
                };

                string FormattedMessage = string.IsNullOrWhiteSpace(MessageDetails) ? string.Empty : 
                    MessageDetails + "\n" + ((Error != null) ? Error.Message : "Unknown Error [Null Exception]") + "\n\n";

                DialogResult OpenLogFile = (FormattedMessage + OpenLogMessage).Message_Box(MessageBoxButtons.YesNo, IconBox);

                if (OpenLogFile == DialogResult.Yes && Core_File)
                {
                    try
                    {
                        if (Directory.Exists(Log_Location.LogCurrentFolder))
                        {
                            Process.Start(Log_Location.LogCurrentFolder);
                        }

                        if (File.Exists(Log_Location.LogLauncher))
                        {
                            Process.Start(Log_Location.LogLauncher);
                        }
                    }
                    catch { }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Log_Type"></param>
        /// <param name="From"></param>
        /// <param name="Log_Details"></param>
        /// <param name="Log_Clear"></param>
        /// <param name="Log_Supress"></param>
        public static void Parent_Log_Screen(this int Log_Type, string From, string Log_Details, bool Log_Clear = false, bool Log_Supress = false)
        {
            try
            {
                string Log_Full_String = From.ToUpper() + ": " + Log_Details;
                string Log_Type_String = "DEFAULT_TEXT";

                switch (Log_Type)
                {
                    case 1:
                        if (!Log_Supress) 
                        { 
                            Log.Info(Log_Full_String); 
                        }
                        Log_Type_String = "       INFO";
                        break;
                    case 2:
                        if (!Log_Supress)
                        {
                            Log.Checking(Log_Full_String);
                        }
                        Log_Type_String = "   CHECKING";
                        break;
                    case 3:
                        if (!Log_Supress)
                        {
                            Log.Completed(Log_Full_String);
                        }
                        Log_Type_String = "   COMPLETE";
                        break;
                    case 4:
                        if (!Log_Supress)
                        {
                            Log.Warning(Log_Full_String);
                        }
                        Log_Type_String = "       WARN";
                        break;
                    case 5:
                        if (!Log_Supress)
                        {
                            Log.Error(Log_Full_String);
                        }
                        Log_Type_String = "      ERROR";
                        break;
                    case 6:
                        if (!Log_Supress)
                        {
                            Log.UrlCall(Log_Full_String);
                        }
                        Log_Type_String = "        URL";
                        break;
                    case 7:
                        if (!Log_Supress)
                        {
                            Log.System(Log_Full_String);
                        }
                        Log_Type_String = "      SYSTM";
                        break;
                    case 8:
                        if (!Log_Supress)
                        {
                            Log.Build(Log_Full_String);
                        }
                        Log_Type_String = "      BUILD";
                        break;
                    case 9:
                        if (!Log_Supress)
                        {
                            Log.Visuals(Log_Full_String);
                        }
                        Log_Type_String = "      VISUL";
                        break;
                    case 10:
                        if (!Log_Supress)
                        {
                            Log.Api(Log_Full_String);
                        }
                        Log_Type_String = "        API";
                        break;
                    case 11:
                        if (!Log_Supress)
                        {
                            Log.Core(Log_Full_String);
                        }
                        Log_Type_String = "       CORE";
                        break;
                    case 12:
                        if (!Log_Supress)
                        {
                            Log.Function(Log_Full_String);
                        }
                        Log_Type_String = "   FUNCTION";
                        break;
                    default:
                        if (!Log_Supress)
                        {
                            Log.Debug(Log_Full_String);
                        }
                        Log_Type_String = "      DEBUG";
                        break;
                }

                if (!Screen_Parent.Screen_Instance.DisposedForm() && !FunctionStatus.LauncherForceClose)
                {
                    if (Log_Clear)
                    {
                        Screen_Parent.Screen_Instance.TextBox_Live_Log.Clear();
                    }
                    else
                    {
                        Screen_Parent.Screen_Instance.TextBox_Live_Log.AppendText(Environment.NewLine + "[" + Log_Type_String + "] " + Log_Full_String);
                    }
                }
            }
            catch (Exception Error)
            {
                OpenLog("Parent Live Log Setter", string.Empty, Error, string.Empty, true);
            }
        }
    }
}
