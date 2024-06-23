using Newtonsoft.Json;

namespace SBRW.Launcher.RunTime.LauncherCore.Lists.JSON
{
    /// <summary>
    /// JSON Format for Creating a Proxy Logging List
    /// </summary>
    /// <remarks><i>Requires <b>Newtonsoft.Json</b> Library</i></remarks>
    public class Json_List_Proxy_Logging
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("Mode")]
        public SBRW.Launcher.Core.Proxy.Log_.CommunicationLogRecord Mode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("Details")]
        public string Details { get; set; } = string.Empty;
    }
}