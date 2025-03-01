namespace SBRW.Launcher.RunTime.InsiderKit
{
    /// <summary>
    /// This is only used for Developers (Bypasses Most Checks)
    /// </summary>
    internal class BuildDevelopment
    {
        /// <summary>
        /// 
        /// </summary>
        private static bool Enabled = true;
        /// <summary>
        /// If User is Opt-In to Use Beta Builds
        /// </summary>
        /// <returns>Conditional Status</returns>
        public static bool Allowed()
        {
            return Enabled;
        }
        /// <summary>
        /// User had Opt-In to Use Beta Builds
        /// </summary>
        /// <param name="Opt_In">Takes in Boolean Values</param>
        /// <returns>New Conditional Status</returns>
        public static bool Allowed(bool Opt_In)
        {
            return Enabled = Opt_In;
        }
    }
}