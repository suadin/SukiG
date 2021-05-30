using System;

namespace SukiG.Shared.TicTacToe
{
    public class TicTacToeGame
    {
        public static string SessionId;
        public TicTacToePlayer Winner { get; private set; }
        public TicTacToePlayer CurrentPlayer { get; private set; }
        public TicTacToePlayer OtherPlayer { get { return CurrentPlayer != TicTacToePlayer.PlayerX ? TicTacToePlayer.PlayerX : TicTacToePlayer.PlayerO; } }
        public TicTacToeState State { get; private set; }

        public TicTacToeGame()
        {
            SessionId = Guid.NewGuid().ToString();
            Winner = TicTacToePlayer.Unset;
            this.ResetGame();
        }

        private void ResetGame()
        {
            CurrentPlayer = TicTacToePlayer.PlayerX;
            State = new TicTacToeState();
        }

        /// <summary>
        /// Sets based on <paramref name="fieldId"/> the field for current player, validates win/draw condition, switch player if game is now over.
        /// </summary>
        /// <param name="fieldId">ID of field, valid values are 0-9 | 0 = top/left | 1 = top/middle | ... | 7 = bottom/middle | 8 = bottom/right | 9 = give up -> reset game |</param>
        public void Set(int fieldId)
        {
            // ignore invalid inputs
            if (fieldId < 0 || fieldId > 9)
            {
                return;
            }

            if (fieldId == 0)
            {
                this.GiveUp();
                return;
            }

            fieldId = fieldId - 1;
            var fieldState = State.Get(fieldId);
            if (fieldState == TicTacToePlayer.Unset)
            {
                State.Set(CurrentPlayer, fieldId);
                if (HasWon())
                {
                    Winner = CurrentPlayer;
                    CurrentPlayer = TicTacToePlayer.Unset;
                    return;
                }
                if (IsDraw())
                {
                    Winner = TicTacToePlayer.Unset;
                    CurrentPlayer = TicTacToePlayer.Unset;
                    return;
                }
                CurrentPlayer = OtherPlayer;
            }
        }

        private void GiveUp()
        {
            Winner = OtherPlayer;
            this.ResetGame();
        }

        private bool HasWon()
        {
            for(var i = 0; i < 3; i++)
            {
                if (Math.Abs((int)State.Get(i) + (int)State.Get(i + 3) + (int)State.Get(i + 6)) == 3)
                    return true; // vertical win
                if (Math.Abs((int)State.Get(i * 3) + (int)State.Get(i * 3 + 1) + (int)State.Get(i * 3 + 2)) == 3)
                    return true; // horizontal win
            }

            // cross win
            if (Math.Abs((int)State.Get(0) + (int)State.Get(4) + (int)State.Get(8)) == 3)
                return true;
            if (Math.Abs((int)State.Get(2) + (int)State.Get(4) + (int)State.Get(6)) == 3)
                return true;

            return false;
        }

        private bool IsDraw()
        {
            for (var i = 0; i < 9; i++)
                if (State.Get(i) == TicTacToePlayer.Unset)
                    return false;

            return true;
        }
    }
}
