using SBRW.Launcher.RunTime.Auth;
using SBRW.Launcher.RunTime.InsiderKit;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using SBRW.Launcher.Core.Cache;
using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.Core.Extension.Web_;
using SBRW.Nancy.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Cache;
using Newtonsoft.Json.Linq;

namespace SBRW.Launcher.RunTime.LauncherCore.Client.Auth
{
    class Authentication
    {
        private static int ServerErrorCode { get; set; }
        private static string LoginResponse { get; set; } = string.Empty;

        private static string ServerErrorResponse { get; set; } = string.Empty;
        private static HttpWebResponse? ServerResponse { get; set; }

        /// <summary>
        /// Form Url or Post Request to the Server for Login and Registration
        /// </summary>
        /// <remarks>Non Secure: Uses regualar URL Request. Secure: Uses Post Request</remarks>
        /// <returns>Receives UserId and Auth Key for Login. Sends Email and Password to Server</returns>
        /// <param name="ConnectionProtocol">Connection Protocol: Check AuthProtocol</param>
        /// <param name="Method">Form Type: "Login" or "Register"</param>
        public static void Client(string Method, bool Modern_Auth, String Email, String Password, String Token)
        {
            try
            {
                if (!Modern_Auth)
                {
                    Uri URLCall =
                        new Uri((Method == "Login") ? Tokens.IPAddress + "/User/authenticateUser?email=" + Email + "&password=" + Password :
                        Tokens.IPAddress + "/User/createUser?email=" + Email + "&password=" + Password +
                        (!String.IsNullOrWhiteSpace(Token) ? "&inviteTicket=" + Token : ""));
#pragma warning disable SYSLIB0014 // Type or member is obsolete
                    ServicePointManager.FindServicePoint(URLCall).ConnectionLeaseTimeout = (int)TimeSpan.FromMinutes(1).TotalMilliseconds;
                    var Client = new WebClient
                    {
                        Encoding = Encoding.UTF8,
                        Headers = Custom_Header.Headers_WHC(),
                        CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
                    };
#pragma warning restore SYSLIB0014 // Type or member is obsolete

                    if (!Launcher_Value.Launcher_Alternative_Webcalls()) 
                    { 
                        Client = new WebClientWithTimeout { Encoding = Encoding.UTF8, CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore) }; 
                    }
                    else
                    {
                        Client.Headers.Add(HttpRequestHeader.UserAgent, Custom_Header.Primary);
                    }

                    try
                    {
                        LoginResponse = Client.DownloadString(URLCall);
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        Client?.Dispose();
                    }
                }
                else
                {
                    string ServerUrl = Tokens.IPAddress + "/User/modernAuth";
                    if (Method == "Register")
                    {
                        ServerUrl = Tokens.IPAddress + "/User/modernRegister";
                    }

                    Uri SendRequest = new Uri(ServerUrl);
#pragma warning disable SYSLIB0014 // Type or member is obsolete
                    ServicePointManager.FindServicePoint(SendRequest).ConnectionLeaseTimeout = (int)TimeSpan.FromSeconds(30).TotalMilliseconds;

                    HttpWebRequest? httpWebRequest = WebRequest.Create(SendRequest) as HttpWebRequest;
#pragma warning restore SYSLIB0014 // Type or member is obsolete
                    if (httpWebRequest != null)
                    {
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";
                        httpWebRequest.Timeout = (int)TimeSpan.FromSeconds(30).TotalMilliseconds;
                        httpWebRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
                        httpWebRequest.Headers.Add(Custom_Header.Headers_WHC());

                        using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string JSON;

                            if (Method == "Login")
                            {
                                JSON = new JavaScriptSerializer().Serialize(new { Email, Password, upgrade = true });
                            }
                            else
                            {
                                JSON = new JavaScriptSerializer().Serialize(new { Email, Password, Ticket = Token });
                            }

                            streamWriter.Write(JSON);
                        }

                        ServerResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        using var sr = new StreamReader(ServerResponse.GetResponseStream(), Encoding.UTF8, false);
                        ServerErrorCode = (int)ServerResponse.StatusCode;
                        LoginResponse = sr.ReadToEnd();
                    }
                }
            }
            catch (WebException Error)
            {
                LogToFileAddons.OpenLog("CLIENT [LOGIN/REGISTER]", string.Empty, Error, string.Empty, true);

                ServerResponse = Error.Response as HttpWebResponse;

                if (ServerResponse == null)
                {
                    ServerErrorCode = 500;
                    LoginResponse = Modern_Auth ? "{\"error\":\"Failed to get reply from server. Please retry.\"}" :
                    "<LoginStatusVO><UserId>0</UserId><Description>Failed to get reply from server. Please retry.</Description></LoginStatusVO>";
                }
                else
                {
                    using var sr = new StreamReader(ServerResponse.GetResponseStream(), Encoding.UTF8, false);
                    ServerErrorCode = (int)ServerResponse.StatusCode;
                    ServerErrorResponse = Modern_Auth ? "{\"error\":\"" + ServerResponse.StatusDescription + "\"}" : string.Empty;
                    LoginResponse = sr.ReadToEnd();

                    if (BuildBeta.Allowed() || BuildDevelopment.Allowed())
                    {
                        Log.Info("LOGIN ERROR RESPONSE:" + LoginResponse);
                        Log.Info("LOGIN ERROR RESPONSE SERVER:" + ServerErrorResponse);
                    }
                }
            }

            try
            {
                if (!string.IsNullOrWhiteSpace(ServerErrorResponse))
                {
                    /// Convert JSON strings to JObject
                    JObject Object_One = JObject.Parse(ServerErrorResponse);
                    JObject Object_Two = JObject.Parse(LoginResponse);
                    /// Merge Object_Two into Object_One
                    Object_One.Merge(Object_Two, new JsonMergeSettings
                    {
                        MergeArrayHandling = MergeArrayHandling.Union,
                        MergeNullValueHandling = MergeNullValueHandling.Ignore
                    });
                    /// Now Update LoginResponse with New JSON String
                    LoginResponse = Object_One.ToString();

                    if (BuildBeta.Allowed() || BuildDevelopment.Allowed())
                    {
                        Log.Info("MERGED JSONS:" + LoginResponse);
                    }
                }

                if (string.IsNullOrWhiteSpace(LoginResponse))
                {
                    Tokens.Error = "Did not Receive a Response from the Server";
                }
                else
                {
                    C_Information_Login_Register TEMP_DATA = LoginResponse.Server_Reader();

                    if (!TEMP_DATA.Invalid_Format)
                    {
                        if (TEMP_DATA.Ban != default)
                        {
                            if (!string.IsNullOrWhiteSpace(TEMP_DATA.Description))
                            {
                                Tokens.Error = TEMP_DATA.Description;
                            }
                            else
                            {
                                string msgBoxInfo = string.Format("You got banned on {0}.", Tokens.ServerName) + "\n";

                                if (!string.IsNullOrWhiteSpace(TEMP_DATA.Ban.Reason))
                                {
                                    msgBoxInfo += "Reason: " + TEMP_DATA.Ban.Reason + "\n";
                                }
                                else
                                {
                                    msgBoxInfo += "Reason: Unknown \n";
                                }

                                if (!string.IsNullOrWhiteSpace(TEMP_DATA.Ban.Expires))
                                {
                                    msgBoxInfo += "Ban Expires: " + TEMP_DATA.Ban.Expires;
                                }
                                else
                                {
                                    msgBoxInfo += "Banned Forever";
                                }

                                Tokens.Error = msgBoxInfo;
                            }
                        }
                        else if (TEMP_DATA.UserId == "0")
                        {
                            if (!string.IsNullOrWhiteSpace(TEMP_DATA.Description))
                            {
                                if (TEMP_DATA.Description == "LOGIN ERROR")
                                {
                                    Tokens.Error = "Invalid E-mail or Password";
                                }
                                else
                                {
                                    Tokens.Error = TEMP_DATA.Description;
                                }
                            }
                            else
                            {
                                Tokens.Error = "ERROR " + ServerErrorCode + ": " + TEMP_DATA.Error;
                            }
                        }
                        else if (!string.IsNullOrWhiteSpace(TEMP_DATA.Error))
                        {
                            Tokens.Error = TEMP_DATA.Error;
                        }

                        if (string.IsNullOrWhiteSpace(TEMP_DATA.Error) || TEMP_DATA.Error == "SERVER FULL")
                        {
                            if (Method == "Login" && string.IsNullOrWhiteSpace(TEMP_DATA.Error))
                            {
                                Tokens.UserId = TEMP_DATA.UserId;
                                Tokens.LoginToken = TEMP_DATA.Token;

                                if (string.IsNullOrWhiteSpace(TEMP_DATA.Alert))
                                {
                                    Tokens.Warning = TEMP_DATA.Alert;
                                }
                            }
                            else if (Method == "Register")
                            {
                                string MessageSuccess;
                                string MessageServerWelcome = string.Empty;

                                if (!string.IsNullOrWhiteSpace(Launcher_Value.Launcher_Select_Server_JSON.Server_Message))
                                {
                                    if (Launcher_Value.Launcher_Select_Server_JSON.Server_Message.ToLower().Contains("welcome"))
                                    {
                                        MessageServerWelcome = Launcher_Value.Launcher_Select_Server_JSON.Server_Message + "\n";
                                    }
                                    else
                                    {
                                        MessageServerWelcome = "Welcome: " + Launcher_Value.Launcher_Select_Server_JSON.Server_Message + "\n";
                                    }
                                }

                                if (TEMP_DATA.Error == "SERVER FULL")
                                {
                                    MessageSuccess = string.Format(MessageServerWelcome + "Successfully registered on {0}. However, server is actually full, " +
                                        "therefore you cannot play it right now.", Tokens.ServerName);
                                }
                                else
                                {
                                    MessageSuccess = string.Format(MessageServerWelcome + "Successfully registered on {0}. You can log in now.", Tokens.ServerName);
                                }

                                Tokens.Success = MessageSuccess;
                            }
                            else
                            {
                                Tokens.Error = TEMP_DATA.Error;
                            }
                        }
                        else
                        {
                            Tokens.Error = TEMP_DATA.Error;
                        }
                    }
                    else
                    {
                        Log.Error("Authentication: " + "Unable to Read " + (Modern_Auth ? "JSON" : "XML") + " File");
                        Tokens.Error = "Unable to Read " + (Modern_Auth ? "JSON": "XML") +  " File";
                    }
                }
            }
            finally
            {
                ServerErrorCode = default;
                LoginResponse = string.Empty;
                ServerErrorResponse = string.Empty;
                ServerResponse = default;
            }
        }
        /// <summary>
        /// Hash Method (Used how to Authenticate Logins)
        /// </summary>
        /// <returns>A hash type standard that is used on the server</returns>
        public static AuthHash HashType(string HType)
        {
            if (!string.IsNullOrWhiteSpace(HType))
            {
                switch (HType)
                {
                    case "1.0":
                    case "true":
                        return AuthHash.H10;
                    case "1.1":
                        return AuthHash.H11;
                    case "1.2":
                    case "false":
                        return AuthHash.H12;
                    case "1.3":
                        return AuthHash.H13;
                    case "2.0":
                        return AuthHash.H20;
                    case "2.1":
                        return AuthHash.H21;
                    case "2.2":
                        return AuthHash.H22;
                    default:
                        return AuthHash.Unknown;
                }
            }
            else
            {
                return Launcher_Value.Launcher_Select_Server_JSON.Server_Authentication_Post ? AuthHash.H10 : AuthHash.H12;
            }
        }
    }
}