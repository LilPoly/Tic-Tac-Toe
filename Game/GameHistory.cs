using TicTacToe.GameAccount;

namespace TicTacToe.Game;

public class GameHistory
{
    public GameAccount.Account Player1 { get; }
    public GameAccount.Account Player2 { get; }
    public WinLose WinLoseRes { get; }
    
    public int Player1Rating { get; }
    public int Player2Rating { get; }

    public GameHistory(GameAccount.Account player1, GameAccount.Account player2, WinLose winLoseRes)
    {
        Player1 = player1;
        Player2 = player2;
        WinLoseRes = WinLoseRes;
        Player1Rating = player1.CurrentRating;
        Player2Rating = player2.CurrentRating;
    }

    public override string ToString()
    {
        string result;

        if (WinLoseRes == WinLose.Win)
        {
            result = $"Winner: {Player1.UserName}, Loser: {Player2.UserName}";
        }
        else if (WinLoseRes == WinLose.Lose)
        {
            result = $"Winner: {Player2.UserName}, Loser: {Player1.UserName}";
        }
        else if (WinLoseRes == WinLose.Draw)
        {
            result = $"Draw: {Player1.UserName} and {Player2.UserName}"; 
        }
        else
        {
            result = "Uknown";
        }

        return $"{result}";
    }

}