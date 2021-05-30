namespace SukiG.Shared.TicTacToe
{
    public interface ITicTacToeField
    {
        void DrawGameField(TicTacToeState state);

        void DrawGameResult(TicTacToePlayer winner);
    }
}
