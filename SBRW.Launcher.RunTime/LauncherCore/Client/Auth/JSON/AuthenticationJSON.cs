using Newtonsoft.Json;
/// <summary>
/// Form JSON for Requests
/// </summary>
/// <returns>Used for ensuring certain Login or Register function checks</returns>
public class Authentication_JSON
{
    /// <summary>
    /// UserID upon a failed Login
    /// </summary>
    /// <value>Gets a UserID. Used to tell the Server which Account to use</value>
    [JsonProperty("userId")]
    public int UserId { get; set; } = 0;
    /// <summary>
    /// Auth Token upon sucessful Login
    /// </summary>
    /// <value>Gets a Auth Token. Used to Login into the Server</value>
    [JsonProperty("token")]
    public string Token { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("Description")]
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("Ban")]
    public string Ban { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    /// <value>Gets a Error Code. Used to inform the user about an issue and can not proceed to login into the server</value>
    [JsonProperty("error")]
    public string Error { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    /// <value>Gets a Warning Code. Used to inform the user about an issue, but can still login in</value>
    [JsonProperty("Warning")]
    public string Warning { get; set; } = string.Empty;
    
}