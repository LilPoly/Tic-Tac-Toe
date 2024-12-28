using TicTacToe.Game.Repo;
using TicTacToe.GameAccount;
using TicTacToe.GameAccount.Repo;

namespace TicTacToe.Game.Service;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IAccountRepository _accountRepository;

    public GameService(IGameRepository gameRepository, IAccountRepository accountRepository)
    {
        _gameRepository = gameRepository;
        _accountRepository = accountRepository;
    }

    public void CreateGame(int player1Id, int player2Id, string gameType)
    {
        Game game = null;
  
        if (gameType == "Classic")
        {
            game = new ClassicGame();
        }
        else if (gameType == "Training")
        {
            game = new TrainingGame();
        }

        if (game != null)
        {
            var player1 = _accountRepository.ReadAccountById(player1Id);
            var player2 = _accountRepository.ReadAccountById(player2Id);
            
            game.PlayGame(player1, player2);
            
            _gameRepository.CreateGame(game);
            
        }
    }

    public List<string> GetGameHistory(int userId)
    {
        var currentUser = _accountRepository.ReadAccountById(userId);

        if (currentUser == null)
        {
            throw new Exception("Current user not found.");
        }

        var allGames = _gameRepository.GetAllGames();
        var gameResults = new List<string>();

        foreach (var game in allGames)
        {
            var userGameResults = game.Results
                .Where(result => result.Player1.Id == currentUser.Id || result.Player2.Id == currentUser.Id)
                .ToList();

            if (!userGameResults.Any()) continue;

            foreach (var result in userGameResults)
            {
                string outcome;
                string opponentName;

                if (result.WinLoseRes == WinLose.Draw)
                {
                    outcome = "Draw";
                    opponentName = result.Player1.UserName == currentUser.UserName 
                        ? result.Player2.UserName 
                        : result.Player1.UserName;
                }
                else
                {
                    if (result.Player1.Id == currentUser.Id)
                    {
                        outcome = result.WinLoseRes switch
                        {
                            WinLose.Win => "Win",
                            WinLose.Lose => "Lose",
                            _ => "Unknown"
                        };
                        opponentName = result.Player2.UserName;
                    }
                    else
                    {
                        outcome = result.WinLoseRes switch
                        {
                            WinLose.Win => "Lose",
                            WinLose.Lose => "Win",
                            _ => "Unknown"
                        };
                        opponentName = result.Player1.UserName;
                    }
                }
                gameResults.Add($"Game Type: {game.TypeGame}, Opponent: {opponentName}, Result: {outcome}");
            }
        }

        return gameResults;
    }
    }
