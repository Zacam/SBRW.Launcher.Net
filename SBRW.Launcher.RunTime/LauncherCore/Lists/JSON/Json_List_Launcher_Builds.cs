using Newtonsoft.Json;

namespace SBRW.Launcher.RunTime.LauncherCore.Lists.JSON
{
    /// <summary>
    /// JSON Format for Creating a Launcher Build List
    /// </summary>
    /// <remarks><i>Requires <b>Newtonsoft.Json</b> Library</i></remarks>
    public class Json_List_Launcher_Builds
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
