using TicTacToe.Database;
using TicTacToe.Game.Repo;
using TicTacToe.Game.Service;
using TicTacToe.GameAccount;
using TicTacToe.GameAccount.Repo;

namespace TicTacToe.Commands;

public class PlayGameCommand : ICommand
{
    private readonly IAccountRepository _accountRepository;
    private readonly IGameService _gameService;
    private readonly DbContext _dbContext;
    private readonly Account _currentUser;

    public PlayGameCommand(IAccountRepository accountRepository, IGameService gameService, DbContext dbContext, Account currentUser)
    {
        _accountRepository = accountRepository;
        _gameService = gameService;
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public void Execute()
    {
        Console.WriteLine($"Welcome, {_currentUser.UserName}!");

        Console.WriteLine("Available opponents:");
        var opponents = _accountRepository.GetAccounts()
            .Where(p => p.Id != _currentUser.Id).ToList();

        foreach (var opponent in opponents)
        {
            Console.WriteLine($"ID: {opponent.Id}, Name: {opponent.UserName}");
        }

        int opponentId;
        while (true)
        {
            Console.WriteLine("Select an opponent by entering their ID:");
            string input = Console.ReadLine();

            if (int.TryParse(input, out opponentId))
            {
                var opponentPlayer = opponents.FirstOrDefault(p => p.Id == opponentId);

                if (opponentPlayer != null)
                {
                    Console.WriteLine($"Opponent selected: {opponentPlayer.UserName}");
                    break; 
                }
            }

            Console.WriteLine("Invalid opponent selection. Please try again.");
        }

        string gameType;
        while (true)
        {
            Console.WriteLine("Select game type: Classic or Training?");
            gameType = Console.ReadLine();

            if (gameType.Equals("Classic", StringComparison.OrdinalIgnoreCase) ||
                gameType.Equals("Training", StringComparison.OrdinalIgnoreCase))
            {
                break; 
            }

            Console.WriteLine("Invalid game type. Please enter 'Classic' or 'Training'.");
        }

        _gameService.CreateGame(_currentUser.Id, opponentId, gameType);
    }

}
