using TicTacToe.GameAccount;

namespace TicTacToe.Game.Repo;

public interface IGameRepository
{
    void CreateGame(Game game);
    List<Game> GetAllGames();
}