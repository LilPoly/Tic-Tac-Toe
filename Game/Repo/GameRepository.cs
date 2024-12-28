using TicTacToe.Database;
using TicTacToe.GameAccount;

namespace TicTacToe.Game.Repo;

public class GameRepository : IGameRepository
{
    private readonly DbContext _dbContext;

    public GameRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateGame(Game game)
    {
            _dbContext.Games.Add(game);  
    }
    

    public List<Game> GetAllGames()
    {
        return _dbContext.Games.ToList();
    }

}