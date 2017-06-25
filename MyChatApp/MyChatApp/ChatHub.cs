using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using MyChatApp.Models;

namespace MyChatApp
{
    public class ChatHub : Hub
    {
        private List<User> _allUsers = new List<User>();

        public List<User> ShowAllUsers()
        {
            
        }
    }
}
