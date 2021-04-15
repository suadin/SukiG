using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SukiG.Server.Hubs
{
    public class ChatRoomHub : Hub
    {
        public const string HubUrl = "/chatroom";
        private static IDictionary<string, string> userList = new Dictionary<string, string>();

        public async Task Broadcast(string username, string message)
        {
            await Clients.All.SendAsync("Broadcast", username, message);
        }

        public async Task UserList()
        {
            await Clients.Caller.SendAsync("UserList", userList.Values);
        }

        public async Task Rename(string userName)
        {
            userList[Context.ConnectionId] = userName;
        }

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