using SBRW.Launcher.RunTime.InsiderKit;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using SBRW.Launcher.Core.Extension.Logging_;
using System;
using System.Windows.Forms;
using SBRW.Launcher.App.UI_Forms.Parent_Screen;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SBRW.Launcher.RunTime.LauncherCore.Support
{
    /// <summary>
    /// 
    /// </summary>
    public static class FormsControls
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static DialogResult Message_Box(this string Text)
        {
            return Message_Box(Text, MessageBoxButtons.OK);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Buttons"></param>
        /// <returns></returns>
        public static DialogResult Message_Box(this string Text, MessageBoxButtons Buttons)
        {
            return Message_Box(Text, Buttons, MessageBoxIcon.None);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Buttons"></param>
        /// <param name="Icon"></param>
        /// <returns></returns>
        public static DialogResult Message_Box(this string Text, MessageBoxButtons Buttons, MessageBoxIcon Icon)
        {
            if (Screen_Parent.Screen_Instance.DisposedForm())
            {
                return Message_Box(null, Text, Buttons, Icon);
            }
            else
            {
                return Message_Box(Screen_Parent.Screen_Instance, Text, Buttons, Icon);
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Window_Handle"></param>
        /// <param name="Text"></param>
        /// <param name="Buttons"></param>
        /// <param name="Icon"></param>
        /// <returns></returns>
        public static DialogResult Message_Box(this IWin32Window Window_Handle, string Text, MessageBoxButtons Buttons, MessageBoxIcon Icon)
        {
            string Caption = "Soapbox Race World: Launcher ";
            switch (Icon)
            {
                case MessageBoxIcon.Error:
                    Caption += "- Stop";
                    break;
                case MessageBoxIcon.Question:
                    Caption += "- Question";
                    break;
                case MessageBoxIcon.Exclamation:
                    Caption += "- Warning";
                    break;
                case MessageBoxIcon.Information:
                    Caption += "- Information";
                    break;
            }

            return MessageBox.Show(Window_Handle, Text, Caption, Buttons, Icon);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Window_Handle"></param>
        /// <param name="Text"></param>
        /// <param name="Buttons"></param>
        /// <param name="Icon"></param>
        /// <returns></returns>
        public static DialogResult Message_Box_Details(this string Text_Message, string Text_Details, MessageBoxButtons Button_Text, MessageBoxIcon Title_Bar)
        {
            string Title_Bar_Caption = "Soapbox Race World: Launcher ";
            switch (Title_Bar)
            {
                case MessageBoxIcon.Error:
                    Title_Bar_Caption += "- Stop";
                    break;
                case MessageBoxIcon.Question:
                    Title_Bar_Caption += "- Question";
                    break;
                case MessageBoxIcon.Exclamation:
                    Title_Bar_Caption += "- Warning";
                    break;
                case MessageBoxIcon.Information:
                    Title_Bar_Caption += "- Information";
                    break;
            }

            /* Set the Custom Text for Buttons */
            string Text_Okay = "OK";
            string Text_Cancel = "Cancel";
            bool Text_Changed = false;
            switch (Button_Text)
            {
                case MessageBoxButtons.YesNoCancel:
                    Text_Okay = "Yes";
                    Text_Cancel = "No";
                    Text_Changed = true;
                    break;
                case MessageBoxButtons.RetryCancel:
                    Text_Okay = "Retry";
                    Text_Cancel = "Cancel";
                    Text_Changed = true;
                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    Text_Okay = "Retry";
                    Text_Cancel = "Abort";
                    Text_Changed = true;
                    break;
            }

            string Dialog_Type_Name = "System.Windows.Forms.PropertyGridInternal.GridErrorDlg";
            Type Dialog_Type = typeof(Form).Assembly.GetType(Dialog_Type_Name);
            /* Create dialog instance */
            Form Dialog = (Form)Activator.CreateInstance(Dialog_Type, new PropertyGrid());
            /* Populate relevant properties on the dialog instance */
            Dialog.Text = Title_Bar_Caption;
            Dialog_Type.GetProperty("Details").SetValue(Dialog, Text_Details, null);
            Dialog_Type.GetProperty("Message").SetValue(Dialog, Text_Message, null);
            /* Set the new Text for Buttons (Live Patch) */
            Control[] Button_Ok = Dialog.Controls.Find("okBtn", true);
            Control[] Button_Cancel = Dialog.Controls.Find("cancelBtn", true);
            if (Button_Text == MessageBoxButtons.OK)
            {
                Button_Ok[0].Visible = false;
                Button_Cancel[0].Text = Button_Ok[0].Text;
            }
            else if (Text_Changed)
            {
                Button_Ok[0].Text = Text_Okay;
                Button_Cancel[0].Text = Text_Cancel;
            }
            
            /* Display dialog */
            return Dialog.ShowDialog(Screen_Parent.Screen_Instance);
        }
        /// <summary>
        /// Checks if the Forms Screen has been Disposed
        /// </summary>
        /// <param name="Screen_Instance">Form Handle</param>
        /// <returns>True if Form is Disposed. Otherwise, False.</returns>
        /// <remarks><i>Mainly used when updating a handle from a seperate thread</i></remarks>
        public static bool DisposedForm(this Form? Screen_Instance)
        {
            if (Screen_Instance != default)
            {
                if (!(Screen_Instance.Disposing || Screen_Instance.IsDisposed))
                {
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// Executes the specified delegate on the thread that owns the control's underlying window handle.
        /// </summary>
        /// <returns>The delegate being invoked has no return value.</returns>
        /// <param name="Control_Form">Name of the Control</param>
        /// <param name="Action_Refresh">Parameters to be set for this Control</param>
        /// <param name="Window_Name">Name of the Parent Form</param>
        public static void SafeInvokeAction(this Control Control_Form, Action Action_Refresh, Form Window_Name, bool Force_Refresh = true)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Window_Name?.Name) || Window_Name == default)
                {
                    if (BuildDevelopment.Allowed() || BuildBeta.Allowed())
                    {
                        Log.Function("SafeInvokeAction: ".ToUpper() + "Control: " + Control_Form + " Action: " + Action_Refresh + " Form: " + Window_Name + " <- Is Null");
                    }
                    return;
                }

                Form Cached_Form = Application.OpenForms[Window_Name.Name];
                bool Cached_Form_IsValid = Cached_Form is not null && !Cached_Form.IsDisposed && !Cached_Form.Disposing;

                if (!Cached_Form_IsValid)
                {
                    return;
                }

                bool Control_IsValid = !Control_Form.IsDisposed || (Control_Form.IsHandleCreated && Control_Form.FindForm().IsHandleCreated);

                if (!Control_IsValid)
                {
                    if (Cached_Form_IsValid)
                    {
                        Window_Name.Controls.Add(Control_Form);
                        SafeInvokeAction(Control_Form, Action_Refresh);
                        Log.Function("SafeInvokeAction: ".ToUpper() + "Control: " + Control_Form.Name + " was added to the Form: " + Window_Name.Name);
                    }
                    return;
                }

                if (Control_Form.Disposing)
                {
                    Log.Function("SafeInvokeAction".ToUpper() + "Control: " + Control_Form.Name + " is being Disposed");
                    return;
                }

                if (Control_Form.InvokeRequired)
                {
                    Control_Form.Invoke(Action_Refresh);

                    if (Force_Refresh)
                    {
                        Control_Form.SafeInvokeAction(() =>
                        {
                            Control_Form.Refresh();
                        }, Window_Name, false);
                    }
                }
                else
                {
                    Action_Refresh();
                }
            }
            catch (Exception ex)
            {
                LogToFileAddons.OpenLog("Safe Invoker Action [Base]", string.Empty, ex, string.Empty, true);
            }
        }
        /// <summary>
        /// Executes the specified delegate on the thread that owns the control's underlying window handle.
        /// </summary>
        /// <returns>The delegate being invoked has no return value.</returns>
        /// <param name="Control_Form">Name of the Control</param>
        /// <param name="Action_Refresh">Parameters to be set for this Control</param>
        public static void SafeInvokeAction(this Control Control_Form, Action Action_Refresh, bool Force_Refresh = true)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Control_Form?.Name) || Control_Form == default)
                {
                    if (BuildDevelopment.Allowed() || BuildBeta.Allowed())
                    {
                        Log.Function($"SAFEINVOKEACTION: Control is null | Control Name: {Control_Form?.Name}");
                    }

                    return;
                }

                if (Control_Form.IsDisposed || Control_Form.Disposing)
                {
                    Log.Function($"SAFEINVOKEACTION: Control '{Control_Form.Name}' is being disposed");
                    return;
                }

                if (Control_Form.InvokeRequired)
                {
                    try
                    {
                        Control_Form.Invoke(Action_Refresh);
                        if (Force_Refresh) Control_Form.BeginInvoke((Action)(() => Control_Form.Refresh()));
                    }
                    catch (Exception ex)
                    {
                        LogToFileAddons.OpenLog("SafeInvokeAction", "Invoke failed", ex, string.Empty, true);
                    }
                    return;
                }

                try
                {
                    Action_Refresh();
                }
                catch (Exception ex)
                {
                    LogToFileAddons.OpenLog("SafeInvokeAction", "Action execution failed", ex, string.Empty, true);
                }

                if (Force_Refresh) Control_Form.Refresh();
            }
            catch (Exception ex)
            {
                LogToFileAddons.OpenLog("Safe Invoker Action [Delta]", string.Empty, ex, string.Empty, true);
            }
        }
        /// <summary>
        /// Executes the specified delegate on the thread that owns the control's underlying window handle.
        /// </summary>
        /// <remarks>Inoke Method: Action</remarks>
        /// <returns>The return value from the delegate being invoked, or null if the delegate has no return value.</returns>
        /// <typeparam name="T">Controls Value</typeparam>
        /// <param name="this">Name of the Control</param>
        /// <param name="Action_Refresh">Parameters to be set for this Control</param>
        public static void SafeInvokeAction<T>(this T @this, Action<T> Action_Refresh, bool Force_Refresh = true) where T : Control
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(@this.Name))
                {
                    if (!@this.IsDisposed || (@this.IsHandleCreated && @this.FindForm().IsHandleCreated))
                    {
                        if (@this.InvokeRequired)
                        {
                            @this.Invoke(Action_Refresh);

                            if (Force_Refresh)
                            {
                                @this.SafeInvokeAction(() =>
                                {
                                    @this.Refresh();
                                }, false);
                            }
                        }
                        else
                        {
                            Action_Refresh(@this);
                        }
                    }
                    else if (!@this.FindForm().IsDisposed)
                    {
                        @this.FindForm().Controls.Add(@this);
                        SafeInvokeAction(@this, Action_Refresh);
                        Log.Function("SafeInvokeAction: ".ToUpper() + "Control: " + @this.Name + " was added to the Form: " + @this.FindForm().Name);
                    }
                    else if (BuildDevelopment.Allowed() || BuildBeta.Allowed())
                    {
                        Log.Function("SafeInvokeAction: ".ToUpper() + "Control: " + @this + " <- Handle hasn't been Created or has been Disposed | Action: " + Action_Refresh);
                    }
                }
                else if (BuildDevelopment.Allowed() || BuildBeta.Allowed())
                {
                    Log.Function("SafeInvokeAction: ".ToUpper() + "Control: " + @this + " <- Is Null | Action: " + Action_Refresh);
                }
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("Safe Invoker Action [Control Only]", string.Empty, Error, string.Empty, true);
            }
        }
        /// <summary>
        /// Executes the specified delegate asynchronously on the thread that the control's underlying handle was created on.
        /// </summary>
        /// <remarks>Inoke Method: Action</remarks>
        /// <param name="this">Name of the Control</param>
        /// <param name="Action_Refresh">Parameters to be set for this Control</param>
        /// <returns>An System.IAsyncResult that represents the result of the System.Windows.Forms.Control.BeginInvoke(System.Delegate) operation.</returns>
        public static IAsyncResult SafeBeginInvokeActionAsync<T>(this T @this, Action<T> Action_Refresh, bool Force_Refresh = true) where T : Control
        {
#if NETFRAMEWORK
            return @this.BeginInvoke((Action)delegate { @this.SafeInvokeAction(Action_Refresh, Force_Refresh); });
#else
            return @this.BeginInvoke(delegate { @this.SafeInvokeAction(Action_Refresh, Force_Refresh); });
#endif
        }
        /// <summary>
        /// Retrieves the return value of the asynchronous operation represented by the System.IAsyncResult passed.
        /// </summary>
        /// <typeparam name="T">Controls Value</typeparam>
        /// <param name="this">Name of the Control</param>
        /// <param name="Invoke_Result">The System.IAsyncResult that represents a specific invoke asynchronous operation, 
        /// returned when calling System.Windows.Forms.Control.BeginInvoke(System.Delegate).</param>
        /// <returns>The System.Object generated by the asynchronous operation.</returns>
        public static void SafeEndInvokeAsyncCatch<T>(this T @this, IAsyncResult Invoke_Result) where T : Control
        {
            try
            {
                @this.EndInvoke(Invoke_Result);
            }
            catch (Exception Error)
            {
                LogToFileAddons.OpenLog("Safe Invoker [End Invoke (Singleton)]", string.Empty, Error, string.Empty, true);
            }
        }
    }
}
