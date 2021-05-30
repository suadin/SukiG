namespace SukiG.Shared.TicTacToe
{
    public enum TicTacToePlayer
    {
        // sum of row/column/cross: 3 -> X wins, -3 -> O wins
        Unset = 0,
        PlayerX = 1,
        PlayerO = -1
    }
}
