namespace TicTacToe.GameAccount.Repo;

public interface IAccountRepository
{
    void CreateAccount(Account account);
    Account GetAccountByUserName(string userName);
    Account ReadAccountById(int id);
    List<Account> GetAccounts();
}