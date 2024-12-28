using TicTacToe.GameAccount;

namespace TicTacToe.Game;

public class TrainingGame : Game
{
    public TrainingGame()
    {
        TypeGame = nameof(TrainingGame);
    }
    
    public override void PlayGame(Account player1, Account player2)
    {
        var ticTacToe = new TicTacToe();
        var winner = ticTacToe.StartGame(player1.UserName, player2.UserName);

        if (winner == player1.UserName)
        {
            Results.Add(new GameHistory(player1, player2, WinLose.Win));
        }
        else if (winner == player2.UserName)
        {
            Results.Add(new GameHistory(player2, player1, WinLose.Lose));
        }
        else
        {
            Results.Add(new GameHistory(player1, player2, WinLose.Draw));
        }
    }
}