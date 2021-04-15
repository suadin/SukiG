using System;

namespace SukiG.Shared.ChatCommand
{
    public class ChatCommandFunc
    {
        public string CommandPattern { get; }

        private readonly Func<string[], ChatCommandResult> func;

        public ChatCommandFunc(Func<string[], ChatCommandResult> func, string commandPattern)
        {
            this.func = func;
            this.CommandPattern = commandPattern;
        }

        public ChatCommandResult Execute(string[] parameters) => func.Invoke(parameters);
    }
}
