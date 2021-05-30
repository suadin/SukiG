using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SukiG.Shared.Chat
{
    public class ChatClient
    {
        public const string CLEAR_USER = "CLEAR";
        public const string INFO_USER = "INFO";
        public const string ERROR_USER = "ERROR";
        public const string EXIT_USER = "EXIT";
        public const string BROADCAST_MESSAGE = "Broadcast";

        private readonly IChatClientHub hub;
        private readonly IChatMessageHandler handler;
        private readonly IChatMessagePrinter printer;

        public string ChatUserName { get; set; }

        public ChatClient(IChatClientHub hub, IChatMessageHandler handler, IChatMessagePrinter printer)
        {
            this.hub = hub;
            this.handler = handler;
            this.printer = printer;
        }

        public async Task Login(string hubUri)
        {
            try
            {
                this.ChatUserName = $"Guest-{new Random().Next(100000, 999999)}";
                var config = new ChatHubConfig(hubUri, this.ChatUserName, BroadcastMessage, UserListMessage);
                await this.hub.Login(config);
                var infoMessage = new ChatMessage(INFO_USER, $"User '{this.ChatUserName}' joins the chat", "Enter");
                await this.hub.Send<ChatMessage>(BROADCAST_MESSAGE, infoMessage);
            }
            catch (Exception ex)
            {
                var errorMessage = new ChatMessage(ERROR_USER, $"Hub connection doesn't work: {ex.Message}", "Enter");
                this.printer.PrintChatMessage(new[] { errorMessage });
            }
        }

        private void BroadcastMessage(ChatMessage chatMessage)
        {
            chatMessage.IsRemoteChatMessage = true;
            this.HandleChatMessage(chatMessage).Wait();
        }

        private void UserListMessage(IList<string> userList)
        {
            var infoMessage = new ChatMessage(INFO_USER, string.Join(" | ", userList), "Enter");
            this.printer.PrintChatMessage(new[] { infoMessage });
        }

        public async Task HandleChatMessage(ChatMessage chatMessage)
        {
            var chatMessages = await this.handler.HandleChatMessage(chatMessage);
            if (chatMessages.Count > 0)
                this.printer.PrintChatMessage(chatMessages);
        }

        public async Task Logout()
        {
            try
            {
                var infoMessage = new ChatMessage(INFO_USER, $"User '{this.ChatUserName}' lefts the chat", "Enter");
                await this.hub.Send<ChatMessage>(BROADCAST_MESSAGE, infoMessage);
                await this.hub.Logout();
            }
            catch (Exception ex)
            {
                var errorMessage = new ChatMessage(ERROR_USER, $"Hub connection doesn't work: {ex.Message}", "Enter");
                this.printer.PrintChatMessage(new[] { errorMessage });
            }
        }
    }
}
