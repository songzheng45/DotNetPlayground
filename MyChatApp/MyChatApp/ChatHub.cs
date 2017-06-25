using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using MyChatApp.Models;
using System.Linq;

namespace MyChatApp
{
    public class ChatHub : Hub
    {
        private static List<User> _allUsers = new List<User>();

        public void AddUser(User user)
        {
            if (!_allUsers.Exists(x => x.Nickname == user.Nickname))
            {
                user.ConnectionId = Context.ConnectionId;
                user.Nickname = user.Nickname;

                _allUsers.Add(user);
            }

            Clients.All.showAllUsers(_allUsers);
        }

        public void Send(User user, string message)
        {
            Clients.All.addNewMessageToPage(user, message);
        }

        public void SendMessageToOne(User from, User to, string message)
        {
            Clients.All.sendMessageToOne(from, to, message);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var removedUser = _allUsers.SingleOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (removedUser != null)
            {
                _allUsers.Remove(removedUser);
            }

            Clients.All.showAllUsers(_allUsers);

            return base.OnDisconnected(stopCalled);
        }
    }
}
