using TicTacToe.GameAccount;

namespace TicTacToe.Database;

public class DbContext
{
    public List<Account> Accounts { get; set; }
    public List<Game.Game> Games { get; set; }

    public DbContext()
    {
        Accounts = new List<Account>();
        Games = new List<Game.Game>();
    }
}