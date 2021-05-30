using System.Collections.Generic;
using System.Threading.Tasks;

namespace SukiG.Shared.Chat
{
    public interface IChatMessageHandler
    {
        Task<IList<ChatMessage>> HandleChatMessage(ChatMessage chatMessage);
    }
}
