using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using scrum_and_xp.Models;
using scrum_and_xp.ViewModels;

namespace SignalRChat
{
    public class ChatHub : Hub
    {
        static List<ChatUser> SignalRUsers = new List<ChatUser>();
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }

        //public void Connect(string userName)
        public override System.Threading.Tasks.Task OnConnected()
        {
            var id = Context.ConnectionId;
            var email = Context.User.Identity.Name;

            if (SignalRUsers.Where(x => x.ConnectionId == id).Count() == 0)
            {
                //SignalRUsers.Add(new ChatUser { ConnectionId = id, UserName = user.FirstName + " " + user.LastName });
                SignalRUsers.Add(new ChatUser { ConnectionId = id, UserName = email });
            }
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = SignalRUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                SignalRUsers.Remove(item);
            }

            return base.OnDisconnected(stopCalled);
        }

        public List<string> GetAllActiveConnections()
        {
            var users = new List<string>();
            foreach (var item in SignalRUsers)
            {
                users.Add(item.UserName);
            }
            return users;
        }
    }
}