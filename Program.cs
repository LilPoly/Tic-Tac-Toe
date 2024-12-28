using TicTacToe.Database;
using TicTacToe.Game.Repo;
using TicTacToe.Game.Service;
using TicTacToe.GameAccount.Repo;
using TicTacToe.GameAccount.Service;

class Program
{
    static void Main(string[] args)
    {
        var dbContext = new DbContext();  

        IAccountRepository accountRepository = new AccountRepository(dbContext);
        IGameRepository gameRepository = new GameRepository(dbContext);  
        IAccountService accountService = new AccountService(accountRepository);
        GameService gameService = new GameService(gameRepository, accountRepository);

        var menu = new Menu(accountRepository, gameService, dbContext, gameRepository, accountService);
        
        menu.ShowMenu();
    }
    
}