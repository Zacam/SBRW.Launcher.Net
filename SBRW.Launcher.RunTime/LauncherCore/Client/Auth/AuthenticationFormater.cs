using System;
using System.Xml;
using SBRW.Launcher.Core.Extension.Logging_;
using SBRW.Launcher.RunTime.LauncherCore.Logger;
using SBRW.Launcher.RunTime.InsiderKit;
using SBRW.Launcher.Core.Extension.Validation_.Json_.Newtonsoft_;
using Newtonsoft.Json;
using SBRW.Launcher.RunTime.LauncherCore.Global;
using SBRW.Launcher.RunTime.LauncherCore.Client.Auth.JSON;
using SBRW.Launcher.Core.Extension.Hash_;

namespace SBRW.Launcher.RunTime.LauncherCore.Client.Auth
{
    /// <summary>
    /// 
    /// </summary>
    public static class Authentication_Formater
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LocationData"></param>
        /// <param name="Type"></param>
        /// <param name="FullNodePath"></param>
        /// <param name="AttributeName"></param>
        /// <returns></returns>
        public static string NodeReader(XmlDocument LocationData, string Type, string FullNodePath, string AttributeName)
        {
            try
            {
                if (BuildDevelopment.Allowed() || BuildBeta.Allowed())
                {
                    Log.Info("XMLSERVERCORE: Attmempting to Read XML [NodePath: '" + FullNodePath + "' Attribute: '" + AttributeName + "']");
                }
                if (Type == "InnerText")
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    if (string.IsNullOrWhiteSpace(LocationData.SelectSingleNode(FullNodePath) != null ?
                        LocationData.SelectSingleNode(FullNodePath).InnerText : string.Empty))
                    {
                        if (BuildDevelopment.Allowed() || BuildBeta.Allowed())
                        {
                            Log.Info("XMLSERVERCORE: EMPTY VALUE - LAUNCHER");
                        }
                        return "EMPTY VALUE - LAUNCHER";
                    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    return LocationData.SelectSingleNode(FullNodePath).InnerText;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                }
                else if (Type == "NodeOnly")
                {
                    if ((LocationData.SelectSingleNode(FullNodePath) ?? null) == null)
                    {
                        if (BuildDevelopment.Allowed() || BuildBeta.Allowed())
                        {
                            Log.Info("XMLSERVERCORE: INVALID NODE - LAUNCHER");
                        }
                        return "INVALID NODE - LAUNCHER";
                    }

                    return "VAILD NODE - LAUNCHER";
                }
                else
                {
                    return "UNKNOWN TYPE - LAUNCHER";
                }
            }
            catch (Exception Error)
            {
                if (BuildDevelopment.Allowed() || BuildBeta.Allowed())
                {
                    Log.Error("XMLSERVERCORE: Unable to Read XML [NodePath: '" + FullNodePath + "' Attribute: '" + AttributeName + "']" + Error.Message);
                    Log.ErrorIC("XMLSERVERCORE: " + Error.HResult);
                    Log.ErrorFR("XMLSERVERCORE: " + Error.ToString());
                }
                if (Type == "InnerText")
                {
                    if (BuildDevelopment.Allowed() || BuildBeta.Allowed())
                    {
                        Log.Error("XMLSERVERCORE: ERROR VALUE - LAUNCHER");
                    }
                    return "ERROR VALUE - LAUNCHER";
                }
                else if (Type == "NodeOnly")
                {
                    if (BuildDevelopment.Allowed() || BuildBeta.Allowed())
                    {
                        Log.Error("XMLSERVERCORE: ERROR NODE - LAUNCHER");
                    }
                    return "ERROR NODE - LAUNCHER";
                }
                else
                {
                    return "ERROR UNKNOWN TYPE - LAUNCHER";
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="XML_DATA"></param>
        /// <returns></returns>
        public static bool XML_Valid(this string XML_DATA)
        {
            if (string.IsNullOrWhiteSpace(XML_DATA))
            {
                return false;
            }
            else
            {
                try
                {
                    if (BuildDevelopment.Allowed())
                    {
                        Log.Checking("Authentication Formater: Checking XML -> " + XML_DATA);
                    }

                    ///Try to parse the XML file to check if it's valid XML
                    XmlDocument xmlDoc = new XmlDocument();
                    ///Attempt to load the XML
                    xmlDoc.LoadXml(XML_DATA);

                    if (BuildDevelopment.Allowed())
                    {
                        Log.Completed("AuthenticationFormater: Valid XML -> " + xmlDoc.OuterXml);
                    }
                }
                catch (XmlException Error)
                {
                    Console.WriteLine("Not a valid XML.");
                    LogToFileAddons.OpenLog("XML Format", String.Empty, Error, String.Empty, true);
                    return false;
                }
                catch (Exception Error)
                {
                    Console.WriteLine($"An error occurred: {Error.Message}");
                    LogToFileAddons.OpenLog("XML Exception", String.Empty, Error, String.Empty, true);
                    return false;
                }

                // If all checks pass, the XML file is valid
                return true;
            }
        }
        /// <summary>
        /// 1
        /// </summary>
        /// <param name="SBRW_XML_DATA"></param>
        /// <returns></returns>
        public static C_Information_Login_Register Server_Reader(this string SBRW_XML_DATA)
        {
            if (Is_Json.Valid_Json(SBRW_XML_DATA))
            {
                C_Information_Login_Register Converted_Response = new C_Information_Login_Register();
#pragma warning disable CS8600 // Null Safe Check Done Above
                Authentication_JSON sbrwJSON = JsonConvert.DeserializeObject<Authentication_JSON>(SBRW_XML_DATA);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                if (sbrwJSON != default)
                {
                    if (!string.IsNullOrWhiteSpace(sbrwJSON.Ban))
                    {
                        C_Information_Ban Converted_Ban_Response = new C_Information_Ban();
                        Converted_Ban_Response.Active = true;

                        if (string.IsNullOrWhiteSpace(sbrwJSON.Description))
                        {
                            Converted_Ban_Response.Reason = sbrwJSON.Description;
                        }

                        Converted_Response.Ban = Converted_Ban_Response;
                    }
                    else if (sbrwJSON.UserId.Equals(0))
                    {
                        if (sbrwJSON.Description == "LOGIN ERROR")
                        {
                            Converted_Response.Invalid_Login = true;
                        }
                        else
                        {
                            Converted_Response.Description = sbrwJSON.Description;
                            Converted_Response.Error = sbrwJSON.Error;
                        }
                    }
                    else
                    {
                        Converted_Response.UserId = sbrwJSON.UserId.ToString();
                        Converted_Response.Token = sbrwJSON.Token;

                        if (!string.IsNullOrWhiteSpace(sbrwJSON.Warning))
                        {
                            Converted_Response.Alert = sbrwJSON.Warning;
                        }
                    }
                }
                else
                {
                    Converted_Response.Invalid_Format = true;
                }

                return Converted_Response;
            }
            else if (XML_Valid(SBRW_XML_DATA))
            {
                C_Information_Login_Register Converted_Response = new C_Information_Login_Register();
                XmlDocument sbrwXml = new XmlDocument();
                sbrwXml.LoadXml(SBRW_XML_DATA);

                if (NodeReader(sbrwXml, "NodeOnly", "LoginStatusVO", "NodeOnly") == "VAILD NODE - LAUNCHER")
                {
                    if (NodeReader(sbrwXml, "InnerText", "LoginStatusVO/Ban", "InnerText") != "EMPTY VALUE - LAUNCHER")
                    {
                        C_Information_Ban Converted_Ban_Response = new C_Information_Ban();
                        Converted_Ban_Response.Active = true;

                        if (NodeReader(sbrwXml, "NodeOnly", "LoginStatusVO/Description", "NodeOnly") == "VAILD NODE - LAUNCHER")
                        {
                            Converted_Response.Description = NodeReader(sbrwXml, "InnerText", "LoginStatusVO/Description", "InnerText");
                        }
                        if (NodeReader(sbrwXml, "NodeOnly", "LoginStatusVO/Ban/Reason", "NodeOnly") != "INVALID NODE - LAUNCHER")
                        {
                            Converted_Ban_Response.Reason = NodeReader(sbrwXml, "InnerText", "LoginStatusVO/Ban/Reason", "InnerText");
                        }
                        if (NodeReader(sbrwXml, "NodeOnly", "LoginStatusVO/Ban/Expires", "NodeOnly") != "INVALID NODE - LAUNCHER")
                        {
                            Converted_Ban_Response.Expires = NodeReader(sbrwXml, "InnerText", "LoginStatusVO/Ban/Expires", "InnerText");
                        }

                        Converted_Response.Ban = Converted_Ban_Response;
                    }
                    else if (NodeReader(sbrwXml, "InnerText", "LoginStatusVO/UserId", "InnerText") == "0")
                    {
                        if (NodeReader(sbrwXml, "NodeOnly", "LoginStatusVO/Description", "NodeOnly") != "ERROR NODE - LAUNCHER" &&
                            NodeReader(sbrwXml, "InnerText", "LoginStatusVO/Description", "InnerText") != "EMPTY VALUE - LAUNCHER")
                        {
                            if (NodeReader(sbrwXml, "InnerText", "LoginStatusVO/Description", "InnerText") == "LOGIN ERROR")
                            {
                                Converted_Response.Invalid_Login = true;
                            }
                            else
                            {
                                Converted_Response.Description = NodeReader(sbrwXml, "InnerText", "LoginStatusVO/Description", "InnerText");
                            }
                        }
                        else
                        {
                            Converted_Response.Error = NodeReader(sbrwXml, "InnerText", "html/body", "InnerText");
                        }
                    }
                    else
                    {
                        Converted_Response.UserId = NodeReader(sbrwXml, "InnerText", "LoginStatusVO/UserId", "InnerText");
                        Converted_Response.Token = NodeReader(sbrwXml, "InnerText", "LoginStatusVO/LoginToken", "InnerText");

                        if (NodeReader(sbrwXml, "NodeOnly", "LoginStatusVO/Warning", string.Empty) == "VAILD NODE - LAUNCHER")
                        {
                            Converted_Response.Alert = NodeReader(sbrwXml, "InnerText", "LoginStatusVO/Warning", "InnerText");
                        }
                    }
                }
                else if (NodeReader(sbrwXml, "NodeOnly", "EngineExceptionTrans", "NodeOnly") == "VAILD NODE - LAUNCHER")
                {
                    if (NodeReader(sbrwXml, "InnerText", "EngineExceptionTrans/StackTrace", "InnerText") != "EMPTY VALUE - LAUNCHER")
                    {
                        Converted_Response.Error = NodeReader(sbrwXml, "InnerText", "EngineExceptionTrans/StackTrace", "InnerText");
                    }
                    else if (NodeReader(sbrwXml, "InnerText", "EngineExceptionTrans/StackTrace/InnerException/StackTrace", "InnerText") != "EMPTY VALUE - LAUNCHER")
                    {
                        Converted_Response.Error = NodeReader(sbrwXml, "InnerText", "EngineExceptionTrans/StackTrace/InnerException/StackTrace", "InnerText");
                    }
                    else if (NodeReader(sbrwXml, "InnerText", "EngineExceptionTrans/ErrorCode", "InnerText") != "EMPTY VALUE - LAUNCHER")
                    {
                        Converted_Response.Error = $"Server Returned an Error: {NodeReader(sbrwXml, "InnerText", "EngineExceptionTrans/ErrorCode", "InnerText")}";
                    }
                    else
                    {
                        Converted_Response.Error = $"Server Encountered an Error";
                    }
                }
                else
                {
                    Converted_Response.Invalid_Format = true;
                }

                return Converted_Response;
            }
            else
            {
                return new C_Information_Login_Register()
                {
                    Invalid_Response = true
                };
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="HType"></param>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static AuthenticationGeneration Login_Formater(this AuthHash HType, string Email, string Password)
        {
            switch (HType)
            {
                case AuthHash.H10:
                    return new AuthenticationGeneration()
                    {
                        Version = "1.0",
                        Email = Email,
                        Password = Password
                    };
                case AuthHash.H11:
                    return new AuthenticationGeneration()
                    {
                        Version = "1.1",
                        Email = Email,
                        Password = Password.Hash_String(0).ToLower()
                    };
                case AuthHash.H12:
                    return new AuthenticationGeneration()
                    {
                        Version = "1.2",
                        Email = Email,
                        Password = Password.Hash_String(1).ToLower()
                    };
                case AuthHash.H13:
                    return new AuthenticationGeneration()
                    {
                        Version = "1.3",
                        Email = Email,
                        Password = Password.Hash_String(2).ToLower()
                    };
                case AuthHash.H20:
                    return new AuthenticationGeneration()
                    {
                        Version = "2.0",
                        Email = Email.Hash_String(0).ToLower(),
                        Password = Password.Hash_String(0).ToLower()
                    };
                case AuthHash.H21:
                    return new AuthenticationGeneration()
                    {
                        Version = "2.1",
                        Email = Email.Hash_String(1).ToLower(),
                        Password = Password.Hash_String(1).ToLower()
                    };
                case AuthHash.H22:
                    return new AuthenticationGeneration()
                    {
                        Version = "2.2",
                        Email = Email.Hash_String(2).ToLower(),
                        Password = Password.Hash_String(2).ToLower()
                    };
                default:
                    Log.Error("HASH TYPE: Unknown Hash Standard was Provided");
                    return new AuthenticationGeneration()
                    {
                        Version = "Unknown",
                        Email = Email,
                        Password = Password
                    };
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class C_Information_Login_Register
    {
        /// <summary>
        /// Alert Message when successfully logging into Server
        /// </summary>
        /// <remarks><i>Is usually empty, unless user was recently unbanned</i></remarks>
        public string Alert { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public C_Information_Ban Ban { get; set; } = new C_Information_Ban();
        /// <summary>
        /// Information Response from Description Header Node
        /// </summary>
        /// <remarks><i>Usually is used for Warning the User of their Ban Status or Password/Email Error</i></remarks>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string Error { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public bool Invalid_Login { get; set; }
        /// <summary>
        /// Bad Data was received from Server
        /// </summary>
        public bool Invalid_Response { get; set; }
        /// <summary>
        /// XML/JSON Header Does Not Match Expected Response
        /// </summary>
        public bool Invalid_Format { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Offline { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value>Gets a Auth Token. Used to Login into the Server</value>
        public string Token { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; } = "0";
    }
    /// <summary>
    /// 
    /// </summary>
    public class C_Information_Ban
    {
        /// <summary>
        /// 
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Reason { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string Expires { get; set; } = string.Empty;
    }
}