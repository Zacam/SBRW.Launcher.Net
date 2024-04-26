using Newtonsoft.Json;

namespace SBRW.Concepts
{
    /// <summary>
    /// JSON Format for Reading an Account List
    /// </summary>
    /// <remarks><i>Requires <b>Newtonsoft.Json</b> Library</i></remarks>
    /// /* accounts.json */
    public class Json_List_Account
    {
        /// <summary>
        /// Game Server Discord App ID Assigned from Server List
        /// </summary>
        /// <remarks>Used for Discord Rich Presence</remarks>
        [JsonProperty("target")]
        public string Target { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("nickname")]
        public string Nickname { get; set; }
        /// <summary>
        /// Game Server Name Assigned from Server List
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }
        /// <summary>
        /// Game Server Address Assigned from Server List
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }
        /// <summary>
        /// Game Server Category Assigned from Server List
        /// </summary>
        [JsonProperty("created")]
        public string Created { get; set; }
        /// <summary>
        /// Used to Tag accounts
        /// </summary>
        /// <remarks>Account ID</remarks>
        [JsonProperty("aid")]
        public int AID { get; set; }
    }
}
