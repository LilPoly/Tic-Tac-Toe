using TicTacToe.GameAccount;

namespace TicTacToe.Game;

public class ClassicGame : Game
{
    public ClassicGame()
    {
        TypeGame = nameof(ClassicGame);
    }
    
    public override void PlayGame(Account player1, Account player2)
    {
        var ticTacToe = new TicTacToe();
        var winner = ticTacToe.StartGame(player1.UserName, player2.UserName);

        if (winner == player1.UserName)
        {
            player1.WinGame();
            player2.LoseGame();
            Results.Add(new GameHistory(player1, player2, WinLose.Win));
        }
        else if (winner == player2.UserName)
        {
            player2.WinGame();
            player1.LoseGame();
            Results.Add(new GameHistory(player1, player2, WinLose.Lose));
        }
        if (winner.Contains("draw"))
        {
            player1.DrawGame();
            player2.DrawGame();
            Results.Add(new GameHistory(player1, player2, WinLose.Draw)); 
        }
    }

}