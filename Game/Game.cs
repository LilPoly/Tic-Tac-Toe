using TicTacToe.GameAccount;

namespace TicTacToe.Game;

public abstract class Game
{
    public string TypeGame { get; set; }
    public List<GameHistory> Results { get; }

    protected Game()
    {
        Results = new List<GameHistory>();

    }
    
    public abstract void PlayGame(Account player1, Account player2);
    
}