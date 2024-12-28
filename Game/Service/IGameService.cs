namespace TicTacToe.Game.Service;

public interface IGameService
{
    void CreateGame(int player1Id, int player2Id, string gameType);
    List<string> GetGameHistory(int userId);
}