using Newtonsoft.Json;
using SBRW.Launcher.Core.Extension.Logging_;

namespace SBRW.Launcher.RunTime.LauncherCore.Lists.JSON
{
    /// <summary>
    /// JSON Format for Creating a Launcher Logging Cleanup List
    /// </summary>
    /// <remarks><i>Requires <b>Newtonsoft.Json</b> Library</i></remarks>
    public class Json_List_Launcher_Logging_Cleanup
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("Value")]
        public long Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("Details")]
        public string Details { get; set; } = string.Empty;
    }
}
