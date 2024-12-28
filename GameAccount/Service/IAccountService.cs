namespace TicTacToe.GameAccount.Service;

public interface IAccountService
{
    void CreateAccount(string userName, string accountTypeInput, string password);
    Account GetAccountByUserName(string username);
}