using TicTacToe.GameAccount.Repo;

namespace TicTacToe.GameAccount.Service;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public void CreateAccount(string userName, string accountTypeInput, string password)
    { 
        var existingAccount = _accountRepository.GetAccountByUserName(userName);
        if (existingAccount != null)
        {
            throw new ArgumentException($"Account with username '{userName}' already exists.");
        }

        PasswordHasher hasher = new PasswordHasher();
        string hashedPassword = hasher.HashPassword(password);
        
        Account account = CreateAccountInstance(userName, accountTypeInput, hashedPassword);
        
        _accountRepository.CreateAccount(account);
    }

    private Account CreateAccountInstance(string userName, string accountTypeInput, string passwordHash)
    {
        if (accountTypeInput.Equals("Vip", StringComparison.OrdinalIgnoreCase))
        {
            return new VipAccount(userName)
            {
                PasswordHash = passwordHash
            };
        }
        else if (accountTypeInput.Equals("Standart", StringComparison.OrdinalIgnoreCase))
        {
            return new Account(userName, AccountType.Standart)
            {
                PasswordHash = passwordHash
            };
        }
        else
        {
            throw new ArgumentException("Invalid account type entered.");
        }
    }
        public Account GetAccountByUserName(string username)
        {
            var account = _accountRepository.GetAccountByUserName(username);
            if (account == null)
            {
                throw new ArgumentException($"Account with username '{username}' not found.");
            }
            return account;
        }
    }
    