using Microsoft.AspNetCore.SignalR;
using SukiG.Shared.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SukiG.Server.Chat
{
    public class ChatServerHub : Hub
    {
        public const string HubUrl = "/chat";
        private static IDictionary<string, string> userList = new Dictionary<string, string>();

        public async Task Broadcast(ChatMessage chatMessage)
        {
            await Clients.All.SendAsync(Shared.Chat.ChatClient.BROADCAST_MESSAGE, chatMessage);
        }

        public async Task UserList()
        {
            await Clients.Caller.SendAsync("UserList", userList.Values);
        }

        //public async Task Rename(string userName)
        //{
        //    userList[Context.ConnectionId] = userName;
        //}

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} connected");
            userList.Add(Context.ConnectionId, Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            userList.Remove(Context.ConnectionId);
            await base.OnDisconnectedAsync(e);
        }
    }
}