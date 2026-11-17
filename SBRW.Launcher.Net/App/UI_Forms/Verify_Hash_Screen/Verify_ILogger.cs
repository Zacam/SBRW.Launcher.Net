using System;

namespace SBRW.Launcher.App.UI_Forms.Verify_Hash_Screen
{
    /// <summary>
    /// An interface for logging, allowing the consumer to hook in their own logging framework.
    /// </summary>
    public interface Verify_ILogger
    {
        void Info(string message);
        void Warn(string message);
        void Error(string message, Exception? ex = null);
    }
}