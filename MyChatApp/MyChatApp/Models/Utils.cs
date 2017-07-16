using System;
using System.Net;
using System.Net.Sockets;

namespace MyChatApp.Models
{
    public class Utils
    {
        public static string GetLocalIP()
        {
            string name = Dns.GetHostName();
            IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
            foreach (IPAddress ipa in ipadrlist)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    return ipa.ToString();
            }
            return "127.0.0.1";
        }
    }
}
