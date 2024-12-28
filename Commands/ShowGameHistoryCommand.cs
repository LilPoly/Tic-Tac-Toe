using TicTacToe.Game.Repo;
using TicTacToe.Game.Service;
using TicTacToe.GameAccount;
using TicTacToe.GameAccount.Repo;

namespace TicTacToe.Commands;

public class ShowGameHistoryCommand : ICommand
{
    private readonly GameService _gameService;
    private readonly int _currentUserId;

    public ShowGameHistoryCommand(GameService gameService, int currentUserId)
    {
        _gameService = gameService;
        _currentUserId = currentUserId;
    }

    public void Execute()
    {
        try
        {
            var gameResults = _gameService.GetGameHistory(_currentUserId);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Game history for user {_currentUserId}:");
            Console.ResetColor();

            if (!gameResults.Any())
            {
                Console.WriteLine("No games found.");
            }
            else
            {
                foreach (var result in gameResults)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine(result);
                    Console.ResetColor();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}


