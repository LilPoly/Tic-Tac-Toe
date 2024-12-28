using TicTacToe.Game;

namespace TicTacToe.GameAccount;

public class VipAccount(string userName) : Account(userName,  AccountType.Vip)
{
    public override void WinGame()
    {
        CurrentRating += 20;
    }

    public override void LoseGame()
    {
        CurrentRating -= 5;
        if (CurrentRating < 1)
        {
            CurrentRating = 1;
        }

    }
}