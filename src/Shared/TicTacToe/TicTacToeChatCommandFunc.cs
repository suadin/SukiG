using SukiG.Shared.Chat;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SukiG.Shared.TicTacToe
{
    public class TicTacToeChatCommandFunc : ChatFunc
    {
        private static TicTacToeGame Game;

        private static IList<ChatMessage> lastMessages;

        public TicTacToeChatCommandFunc() : base("TicTacToe", "tictactoe", TicTacToeFunc)
        {
        }

        public override IList<ChatMessage> OnActive()
        {
            var welcome = new List<ChatMessage>();
            welcome.Add(new ChatMessage("CLEAR", string.Empty, string.Empty));
            welcome.Add(new ChatMessage("INFO", "Welcome to 1vs1 hot-seat TicTacToe!", string.Empty));
            welcome.Add(new ChatMessage("INFO", "--------------------------------------------------", string.Empty));
            welcome.Add(new ChatMessage("INFO", "Description:", string.Empty));
            welcome.Add(new ChatMessage("INFO", "TicTacToe hot-seat is a 1vs1 match on one PC with one keyboard. First player starts with turn by click number between 1-9. After his click player two starts with turn. That repeats until game is over. Begin/Surrender/Restart a TicTacToe game with 0. Exit TicTacToe mode with ESC.", string.Empty));
            welcome.Add(new ChatMessage("INFO", "--------------------------------------------------", string.Empty));
            welcome.Add(new ChatMessage("INFO", "Control:", string.Empty));
            welcome.Add(new ChatMessage("INFO", "0: start game | ESC: exit", string.Empty));
            welcome.Add(new ChatMessage("INFO", "--------------------------------------------------", string.Empty));
            lastMessages = welcome;
            return welcome;
        }

        private static IList<ChatMessage> TicTacToeFunc(ChatMessage chatMessage)
        {
            if (chatMessage.LastKeyInput == "Escape")
                return new[] { new ChatMessage("EXIT", string.Empty, string.Empty) };

            var updatedLastKeyInput = chatMessage.LastKeyInput.Replace("Digit", string.Empty).Replace("Numpad", string.Empty);
            var isNumberInput = int.TryParse(updatedLastKeyInput, out var fieldId);
            if (Game != null && isNumberInput)
            {
                Game.Set(fieldId);
                var gameField = GameField();
                lastMessages = gameField;
                if (Game.Winner != TicTacToePlayer.Unset)
                    Game = null;
                return gameField;
            }

            if(Game == null && isNumberInput && fieldId == 0)
            {
                Game = new TicTacToeGame();
                var gameField = GameField();
                lastMessages = gameField;
                return gameField;
            }

            return lastMessages;
        }

        public static IList<ChatMessage> GameField()
        {
            var gameField = new List<ChatMessage>();
            gameField.Add(new ChatMessage("CLEAR", string.Empty, string.Empty));
            if(Game.Winner == TicTacToePlayer.Unset)
                gameField.Add(new ChatMessage("INFO", $"Player {GetSign(Game.CurrentPlayer)} turn!", string.Empty));
            else
                gameField.Add(new ChatMessage("INFO", $"Player {Game.Winner} wins!", string.Empty));
            gameField.Add(new ChatMessage("INFO", "--------------------------------------------------", string.Empty));
            gameField.Add(new ChatMessage("INFO", "Control:", string.Empty));
            if (Game.Winner == TicTacToePlayer.Unset)
                gameField.Add(new ChatMessage("INFO", "0: surrender | 1-9: set player sign to field | ESC: exit", string.Empty));
            else
                gameField.Add(new ChatMessage("INFO", "0: restart | ESC: exit", string.Empty));
            gameField.Add(new ChatMessage("INFO", "--------------------------------------------------", string.Empty));
            if (Game.Winner == TicTacToePlayer.Unset)
            {
                gameField.Add(new ChatMessage("INFO", $"|{GetSign(7-1)}|{GetSign(8-1)}|{GetSign(9-1)}|", string.Empty));
                gameField.Add(new ChatMessage("INFO", $"|{GetSign(4-1)}|{GetSign(5-1)}|{GetSign(6-1)}|", string.Empty));
                gameField.Add(new ChatMessage("INFO", $"|{GetSign(1-1)}|{GetSign(2-1)}|{GetSign(3-1)}|", string.Empty));
                gameField.Add(new ChatMessage("INFO", "--------------------------------------------------", string.Empty));
            }
            return gameField;
        }

        private static string GetSign(int fieldId)
        {
            var player = Game.State.Get(fieldId);
            return GetSign(player) ?? (fieldId + 1).ToString();
        }

        private static string GetSign(TicTacToePlayer player)
        {
            switch (player)
            {
                case TicTacToePlayer.PlayerX: return "X";
                case TicTacToePlayer.PlayerO: return "O";
                default: return null;
            }
        }
    }
}