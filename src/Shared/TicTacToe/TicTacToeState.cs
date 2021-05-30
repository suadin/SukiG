namespace SukiG.Shared.TicTacToe
{
    public class TicTacToeState
    {
        private TicTacToePlayer[,] state;

        public TicTacToeState()
        {
            this.state = new TicTacToePlayer[,] {
                { TicTacToePlayer.Unset, TicTacToePlayer.Unset, TicTacToePlayer.Unset },
                { TicTacToePlayer.Unset, TicTacToePlayer.Unset, TicTacToePlayer.Unset },
                { TicTacToePlayer.Unset, TicTacToePlayer.Unset, TicTacToePlayer.Unset }
            };
        }

        public TicTacToePlayer Get(int fieldId)
        {
            return this.state[fieldId % 3, fieldId / 3];
        }

        public void Set(TicTacToePlayer player, int fieldId)
        {
            this.state[fieldId % 3, fieldId / 3] = player;
        }
    }
}
