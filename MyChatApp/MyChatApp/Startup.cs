using System;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyChatApp.Startup))]
namespace MyChatApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.MapSignalR();
            //builder.MapSignalR("/chat", new Microsoft.AspNet.SignalR.HubConfiguration()
            //{
            //    EnableDetailedErrors = true,
            //    EnableJavaScriptProxies = true
            //});
        }
    }
}
