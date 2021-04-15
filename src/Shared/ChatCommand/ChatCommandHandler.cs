
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SukiG.Shared.ChatCommand
{
    public class ChatCommandHandler
    {
        private IEnumerable<ChatCommandFunc> funcs;

        public ChatCommandHandler(IEnumerable<ChatCommandFunc> funcs)
        {
            this.funcs = funcs;
        }

        public async Task<ChatCommandResult> Handle(string message)
        {
            message = message.Trim().ToLower().Replace("  ", " "); // normalize message
            if (message[0] != '/')
                return ChatCommandResult.Ignored; // no command, skip handling

            var chatCommandParams = message.Substring(1).Split(" ");
            foreach(var func in funcs)
                if(func.Execute(chatCommandParams) == ChatCommandResult.Executed)
                    return ChatCommandResult.Executed;

            return ChatCommandResult.Failed; // not expected, handle error on client
        }
    }
}