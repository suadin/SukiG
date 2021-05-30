namespace SukiG.Client.Chat
{
    public class FormatedChatMessage
    {
        public FormatedChatMessage(string textMessage, string cssClasses)
        {
            TextMessage = textMessage;
            CssClasses = cssClasses;
        }

        public string TextMessage { get; private set; }

        public string CssClasses { get; private set; }
    }
}
