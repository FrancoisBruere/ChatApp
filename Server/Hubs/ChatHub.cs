using ChatApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Server.Hubs
{
    //[Authorize]
    public class ChatHub : Hub
    {
        public Microsoft.AspNetCore.SignalR.Client.HubConnectionState State { get; }
        public TimeSpan ServerTimeout { get; set; }

        public TimeSpan KeepAliveInterval { get; set; }

        public TimeSpan HandshakeTimeout { get; set; }

        public string ConnectionId { get; }

        public async Task SendMessages(Message message) //Message shared class folder and sending message
        {
            
            var users = new string[] { message.FromUserId, message.ToUserId};
            
            //sends message to all clients
            await Clients.All.SendAsync("ReceiveMessage", message); // client will listen on "ReceiveMessage"

            // to send to specific user
              

               //await Clients.Users(users).SendAsync("ReceiveMessage", message);
                //Clients.Client(Context.UserIdentifier);
                //Clients.Clients(Context.User.Identity.Name);
                //Clients.Client(Context.Items.Keys.ToString());
                //Clients.Client(Context.User.Identity.AuthenticationType);
                          

        }


    }
}
