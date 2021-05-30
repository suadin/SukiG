using System.Collections.Generic;

namespace SukiG.Shared.Chat
{
    public interface IChatMessagePrinter
    {
        void PrintChatMessage(IList<ChatMessage> chatMessages);
    }
}