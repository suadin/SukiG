using System;
using System.Collections.Generic;

namespace SukiG.Shared.Chat
{
    public class ChatHubConfig
    {
        public ChatHubConfig(string hubUri)
        {
            this.UserName = string.Empty;
            this.HubUri = hubUri;
            this.BroadcastMessageAction = chatMessage => { };
            this.UserListMessageAction = userList => { };
        }

        public ChatHubConfig(string hubUri, string userName, Action<ChatMessage> broadcastMessageAction, Action<IList<string>> userListMessageAction) : this(hubUri)
        {
            this.UserName = userName;
            this.BroadcastMessageAction = broadcastMessageAction;
            this.UserListMessageAction = userListMessageAction;
        }

        public string HubUri { get; private set; }

        public string UserName { get; private set; }

        public Action<ChatMessage> BroadcastMessageAction { get; private set; }

        public Action<IList<string>> UserListMessageAction { get; private set; }
    }
}
