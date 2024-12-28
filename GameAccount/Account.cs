namespace TicTacToe.GameAccount;

public class Account
{
    public string UserName { get; }
    public int Id { get; set; }
    public int CurrentRating { get; protected set; } = 1;
    public AccountType AccountType { get; }
    public string PasswordHash { get; set; }
    
    
    public Account(string userName, AccountType accountType)
    {
        UserName = userName;
        AccountType = accountType;
    }

    public virtual void WinGame()
    {
        CurrentRating += 10;
    }

    public virtual void LoseGame()
    {
        CurrentRating -= 10;
        if (CurrentRating < 1)
        {
            CurrentRating = 1;
        }

    }

    public void DrawGame()
    {
    }
}