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
        Task showAllUsers(IEnumerable<string> allUsers);
        Task addNewMessageToPage(string nickName, string message);
    }


    public class ChatHub : Hub<IClient>
    {
        // 昵称和连接的映射
        //private static ConnectionMapping _connections = new ConnectionMapping();

        // 保存所有用户
        private static UserMapping _users = new UserMapping();

        public string CallerNickName => Context.QueryString["nickName"];

        public Task Send(string message)
        {
            return Clients.All.addNewMessageToPage(CallerNickName, message);
        }

        // 连接上服务器
        public override Task OnConnected()
        {
            // 如：在聊天程序中，将 ConnectionId 和用户名关联起来，并将用户标记为在线
            // 该方法执行完之后，会通知客户端连接已建立。比如在 Javascript 客户端中， start().done()回调函数会被执行。

            _users.AddUser(CallerNickName);

            IEnumerable<string> allUser = _users.GetAllUsers();
            Clients.All.showAllUsers(allUser);  // 上线后刷新所有用户的用户列表

            Clients.All.addNewMessageToPage("系统", $"{CallerNickName}上线了！"); // 系统广播该用户上线

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

            _users.RemoveUser(CallerNickName);

            IEnumerable<string> allUsers = _users.GetAllUsers();
            Clients.All.showAllUsers(allUsers);

            return base.OnDisconnected(stopCalled);
        }


    }
}
