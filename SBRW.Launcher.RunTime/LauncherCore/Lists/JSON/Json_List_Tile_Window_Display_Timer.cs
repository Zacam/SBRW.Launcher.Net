using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SBRW.Launcher.RunTime.LauncherCore.Lists.JSON
{
    /// <summary>
    /// JSON Format for Creating a Title Window Display Timer List
    /// </summary>
    /// <remarks><i>Requires <b>Newtonsoft.Json</b> Library</i></remarks>
    public class Json_List_Tile_Window_Display_Timer
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
