using System;
using System.Configuration;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin;
using MyChatApp.Models;
using Owin;

[assembly: OwinStartup(typeof(MyChatApp.Startup))]
namespace MyChatApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            string[] redisConnectionString = ConfigurationManager.AppSettings["redis"].Split(':');
            string server = redisConnectionString?[0];
            int port = 6379;

            GlobalHost.DependencyResolver.UseRedis(server, port, null, "ChatApp");

            builder.MapSignalR();
        }
    }
}
