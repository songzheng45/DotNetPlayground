using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using MyChatApp.Models;
using System.Linq;

namespace MyChatApp
{
    public interface IClient
    {
        Task showAllUsers(List<User> allUsers);
        Task addNewMessageToPage(User user, string message);
        Task sendMessageToOne(User from, User to, string message);

    }


    public class ChatHub : Hub<IClient>
    {
        private static List<User> _allUsers = new List<User>();

        public Task AddUser(User user)
        {
            if (!_allUsers.Exists(x => x.Nickname == user.Nickname))
            {
                user.ConnectionId = Context.ConnectionId;
                user.Nickname = user.Nickname;

                _allUsers.Add(user);
            }

            return Clients.All.showAllUsers(_allUsers);
        }

        public Task Send(User user, string message)
        {
            return Clients.All.addNewMessageToPage(user, message);
        }

        public Task SendMessageToOne(User from, User to, string message)
        {
            return Clients.All.sendMessageToOne(from, to, message);
        }

        // 连接上服务器
        public override Task OnConnected()
        {
            // 如：在聊天程序中，将 ConnectionId 和用户名关联起来，并将用户标记为在线
            // 该方法执行完之后，会通知客户端连接已建立。比如在 Javascript 客户端中， start().done()回调函数会被执行。

            var user = new User() { UserId = 1111, Nickname = "系统" };
            Clients.All.addNewMessageToPage(user, $"xxx上线了！");

            return base.OnConnected();
        }

        // 客户端重新连接
        public override Task OnReconnected()
        {
            // 如：在聊天程序中，你已经将一个不活动的用户标记为离线，在这种情况下将用户再次标记为在线。

            return base.OnReconnected();
        }

        // 客户端断开连接
        public override Task OnDisconnected(bool stopCalled)
        {
            // 如： 在聊天程序中，将用户标记为“离线”；删除当前 ConnectionId 和用户名的关联

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
