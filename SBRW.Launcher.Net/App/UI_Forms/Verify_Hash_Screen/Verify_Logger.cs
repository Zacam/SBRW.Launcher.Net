using SBRW.Launcher.Core.Downloader.Log_;
using SBRW.Launcher.Core.Extension.Logging_;
using System;

namespace SBRW.Launcher.App.UI_Forms.Verify_Hash_Screen
{
    /// <summary>
    /// Implementation of ILogger that routes messages to the launcher's Log_Verify static class.
    /// </summary>
    public class Verify_Logger : Downloader_ILogger
    {
        public void Info(string message)
        {
            Log_Verify.Info(message);
        }

        public void Warn(string message)
        {
            // Log_Verify doesn't have a 'Warn', so we'll use 'Error' or 'Info'
            Log_Verify.Error("WARN: " + message);
        }

        public void Error(string message, Exception? ex = null)
        {
            Log_Verify.Error(message);
            if (ex != null)
            {
                Log_Verify.Error("Exception: " + ex.Message);
                Log_Verify.ErrorFR(ex.ToString());
            }
        }

        public void Invalid(string message)
        {
            throw new NotImplementedException();
        }

        public void Deleted(string message)
        {
            throw new NotImplementedException();
        }

        public void Missing(string message)
        {
            throw new NotImplementedException();
        }

        public void Downloaded(string message)
        {
            throw new NotImplementedException();
        }
    }
}