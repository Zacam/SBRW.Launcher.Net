using Nancy.Hosting.Self;
using System;

namespace GameLauncherSimplified.App.Classes.LauncherCore.Proxy
{
    public class ProxyServer
    {
        private ProxyServer() { }
        private static readonly Lazy<ProxyServer> Lazy = new Lazy<ProxyServer>(() => new ProxyServer());
        public static ProxyServer Instance => Lazy.Value;

        public static int Port = new Random().Next(2009, 2015);
        private NancyHost Server;

        public void Start()
        {
            if (Server != null)
            {
                throw new Exception("Server already running!");
            }
            else
            {
                var hostConfigs = new HostConfiguration()
                {
                    UrlReservations = new UrlReservations()
                    {
                        CreateAutomatically = true,
                    },
                    AllowChunkedEncoding = false,
                    RewriteLocalhost = false
                };

                Server = new NancyHost(new Uri("http://127.0.0.1:" + Port), new ProxyBootstrap(), hostConfigs);
                Server.Start();
            }
        }

        public void Stop()
        {
            Server.Stop();
        }
    }
}
