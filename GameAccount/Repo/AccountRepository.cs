using System.Data.Common;
using TicTacToe.Database;

namespace TicTacToe.GameAccount.Repo;

public class AccountRepository : IAccountRepository
{
    private readonly DbContext _dbContext;
    private static int _nextId = 1;  

    public AccountRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateAccount(Account account)
    {
        if (account.Id == 0)
        {
            account.Id = _nextId++;
        }
        
        _dbContext.Accounts.Add(account);
    }
    
    public Account ReadAccountById(int id)
    {
        var account = _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
        return account;
    }
    
    public Account GetAccountByUserName(string username)
    {
        return _dbContext.Accounts.FirstOrDefault(a => a.UserName == username);
    }
    
    public List<Account> GetAccounts()
    {
        return _dbContext.Accounts;
    }
    
}