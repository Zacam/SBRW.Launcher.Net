using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using ClassicGameLauncher.App.Classes.LauncherCore.Hashes;
using ClassicGameLauncher.App.Classes.SystemPlatform.Components;

namespace ClassicGameLauncher.App.Classes.LauncherCore.Client.Web
{
    public class WebClientWithTimeout : WebClient
    {
        private static string GameLauncherHash = string.Empty;
        private static long addrange = 0;
        private static int timeout = 3000;

        public static string Value()
        {
            if (string.IsNullOrEmpty(GameLauncherHash))
            {
                GameLauncherHash = SHA.HashFile(AppDomain.CurrentDomain.FriendlyName);
            }

            return GameLauncherHash;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.ServerCertificateValidationCallback = (Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => {
                bool isOk = true;
                if (sslPolicyErrors != SslPolicyErrors.None)
                {
                    for (int i = 0; i < chain.ChainStatus.Length; i++)
                    {
                        if (chain.ChainStatus[i].Status == X509ChainStatusFlags.RevocationStatusUnknown)
                        {
                            continue;
                        }
                        chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                        chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                        chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                        chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                        bool chainIsValid = chain.Build((X509Certificate2)certificate);
                        if (!chainIsValid)
                        {
                            isOk = false;
                            break;
                        }
                    }
                }
                return isOk;
            };

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
            request.UserAgent = UserAgent.UserAgentName;
            request.Headers["X-HWID"] = HardwareID.FingerPrint.Value();
            request.Headers["X-UserAgent"] = UserAgent.UserAgentHeaderName;
            request.Headers["X-GameLauncherHash"] = Value();

            if (addrange != 0)
            {
                request.AddRange(addrange);
            }

            request.Proxy = null;
            request.Timeout = timeout;

            return request;
        }

        internal void AddRange(long filesize)
        {
            addrange = filesize;
        }

        internal void Timeout(int time)
        {
            timeout = time;
        }
    }
}
