using SBRW.Launcher.RunTime.InsiderKit;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using SBRW.Launcher.Core.Extension.Logging_;
using System;
using System.Windows.Forms;

namespace SBRW.Launcher.RunTime.LauncherCore.Support
{
    static class FormsControls
    {
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
