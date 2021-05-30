using SukiG.Shared.Chat;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SukiG.Shared.Chat
{
    public class ChatFunc
    {
        public string Name { get; }
        public string StartCommand { get; }

        private readonly Func<ChatMessage, IList<ChatMessage>> func;

        public ChatFunc(string name, string startCommand, Func<ChatMessage, IList<ChatMessage>> func)
        {
            this.func = func;
            this.Name = name;
            this.StartCommand = startCommand;
        }

        public virtual IList<ChatMessage> OnActive()
        {
            return new[] { new ChatMessage(ChatClient.INFO_USER, $"Join {Name} functions.", string.Empty) };
        }

        public IList<ChatMessage> Execute(ChatMessage chatMessage) => func.Invoke(chatMessage);
    }
}
