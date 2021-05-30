namespace SukiG.Shared.Chat
{
    public class ChatMessage
    {
        public ChatMessage(string username, string textMessage, string lastKeyInput)
        {
            UserName = username;
            TextMessage = textMessage;
            LastKeyInput = lastKeyInput;
        }

        public string UserName { get; }
        public string TextMessage { get; }
        public string LastKeyInput { get; }
        public bool IsRemoteChatMessage { get; set; } = false;
    }
}