using TicTacToe.Commands;
using TicTacToe.Database;
using TicTacToe.Game.Repo;
using TicTacToe.Game.Service;
using TicTacToe.GameAccount;
using TicTacToe.GameAccount.Repo;
using TicTacToe.GameAccount.Service;

public class Menu
{
    private readonly IAccountRepository _accountRepository;
    private readonly GameService _gameService;
    private readonly DbContext _dbContext;
    private readonly IGameRepository _gameRepository;
    private readonly IAccountService _accountService;

    public Menu(IAccountRepository accountRepository, GameService gameService, DbContext dbContext, IGameRepository gameRepository, IAccountService accountService)
    {
        _accountRepository = accountRepository;
        _gameService = gameService;
        _dbContext = dbContext;
        _gameRepository = gameRepository;
        _accountService = accountService;
    }

    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome to the game Tic Tac Toe!");
            Console.ResetColor();
            Console.WriteLine("--------------------------------");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Please choose an option: ");
            Console.ResetColor();
            
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var registrationCommand = new RegistrationCommand(_accountService, ShowMenu);
                    registrationCommand.Execute();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(); 
                    break;
                case "2":
                    var loginCommand = new LoginCommand(
                        _accountService,
                        ShowMenu, 
                        user => ShowPostLoginMenu(user.Id, user)  
                    );
                    loginCommand.Execute();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.ResetColor();
                    break;
            }
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }
    }

    private void ShowPostLoginMenu(int currentUserId, Account currentUser)
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome back to the game!");
            Console.ResetColor();
            Console.WriteLine("1. View account info");
            Console.WriteLine("2. Play a game");
            Console.WriteLine("3. View game history");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("4. Log out");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Please choose an option: ");
            Console.ResetColor();

            string choice = Console.ReadLine().Trim(); 

            switch (choice)
            {
                case "1":
                    var showCurrentUserInfoCommand = new ShowCurrentUserInfoCommand(_accountRepository, currentUserId);
                    showCurrentUserInfoCommand.Execute();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(); 
                    break;

                case "2":
                    Console.WriteLine("Option 2 selected.");
                    var playGame = new PlayGameCommand(_accountRepository, _gameService, _dbContext, currentUser);
                    playGame.Execute();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(); 
                    break;
                case "3":
                    var showGameHistoryCommand = new ShowGameHistoryCommand(_gameService, currentUserId);
                    showGameHistoryCommand.Execute();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(); 
                    break;
                case "4":
                    Console.WriteLine("You have logged out.");
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.ResetColor();
                    break;
            }

        }
    }
}