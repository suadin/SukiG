using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SukiG.Shared.Chat
{
    public class ChatMessageHandler : IChatMessageHandler
    {
        private Stack<ChatFunc> activeChatFunc;
        private readonly IChatClientHub hub;
        private readonly IList<ChatFunc> funcs;

        public ChatMessageHandler(IChatClientHub hub, IList<ChatFunc> funcs)
        {
            this.activeChatFunc = new Stack<ChatFunc>();
            this.hub = hub;
            this.funcs = funcs;
        }

        public async Task<IList<ChatMessage>> HandleChatMessage(ChatMessage chatMessage)
        {
            var chatCommandParameter = GetChatCommandParameter(chatMessage);
            if (!activeChatFunc.TryPeek(out _) && chatCommandParameter == null)
            {
                if (chatMessage.LastKeyInput == "Enter")
                {
                    if (!chatMessage.IsRemoteChatMessage)
                    {
                        await this.hub.Send(ChatClient.BROADCAST_MESSAGE, chatMessage);
                        return new ChatMessage[] { };
                    }
                    else
                    {
                        return new ChatMessage[] { chatMessage };
                    }
                }
                else
                {
                    return new ChatMessage[] { };
                }
            }

            if (activeChatFunc.TryPeek(out _))
            {
                var func = activeChatFunc.Peek();
                var messages = func.Execute(chatMessage);
                if (messages.Any(m => m?.UserName == ChatClient.EXIT_USER))
                {
                    activeChatFunc.Pop();
                    var clearMessage = new ChatMessage(ChatClient.CLEAR_USER, string.Empty, string.Empty);
                    var infoMessage = new ChatMessage(ChatClient.INFO_USER, $"Leaved {func.Name}...", string.Empty);
                    return new[] { clearMessage, infoMessage };
                }
                return messages;
            }
            
            if (chatCommandParameter?.Length == 1 && "help" == chatCommandParameter[0])
            {
                var helpMessages = new List<ChatMessage>();
                helpMessages.Add(new ChatMessage(ChatClient.INFO_USER, "/help", string.Empty));
                foreach (var func in funcs)
                    helpMessages.Add(new ChatMessage(ChatClient.INFO_USER, $"/{func.StartCommand}", string.Empty));

                return helpMessages;
            }

            foreach (var func in funcs)
            {
                if (chatCommandParameter?.Length == 1 && func.StartCommand == chatCommandParameter[0])
                {
                    activeChatFunc.Push(func);
                    return func.OnActive();
                }
            }

            var errorMessage = new ChatMessage(ChatClient.ERROR_USER, $"Cannot handle chat message! UserName={chatMessage.UserName}, TextMessage={chatMessage.TextMessage}, LastKeyInput={chatMessage.LastKeyInput}", "Enter");
            return new[] { errorMessage };
        }

        public static string[] GetChatCommandParameter(ChatMessage chatMessage)
        {
            var normalizedTextMessage = chatMessage?.TextMessage?.Trim().ToLower().Replace("  ", " ");
            bool isChatCommand = normalizedTextMessage?.Length > 1 && normalizedTextMessage?[0] == '/';
            return isChatCommand ? normalizedTextMessage.Substring(1).Split(" ") : null;
        }
    }
}
