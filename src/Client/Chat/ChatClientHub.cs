using Microsoft.AspNetCore.SignalR.Client;
using SukiG.Shared.Chat;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SukiG.Client.Chat
{
    public class ChatClientHub : IChatClientHub
    {
        public HubConnection HubConnection { get; private set; }

        public async Task Login(ChatHubConfig config)
        {
            HubConnection = new HubConnectionBuilder().WithUrl(config.HubUri).Build();
            HubConnection.On<ChatMessage>("Broadcast", config.BroadcastMessageAction);
            HubConnection.On<IList<string>>("UserList", config.UserListMessageAction);
            await HubConnection.StartAsync();
        }

        public async Task Logout()
        {
            if (HubConnection != null && HubConnection.State == HubConnectionState.Connected)
            {
                await HubConnection.StopAsync();
                await HubConnection.DisposeAsync();
                HubConnection = null;
            }
        }

        public async Task Send<T>(string method, T input)
        {
            await HubConnection.SendAsync(method, input);
        }
    }
}
