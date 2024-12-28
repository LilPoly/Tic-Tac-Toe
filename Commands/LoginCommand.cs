using System.Text;
using TicTacToe.GameAccount;
using TicTacToe.GameAccount.Service;

namespace TicTacToe.Commands;

public class LoginCommand : ICommand
{
    private readonly IAccountService _accountService;
    private readonly Action _showMenu;
    private readonly Action<Account> _setCurrentUser;

    public LoginCommand(IAccountService accountService, Action showMenu, Action<Account> setCurrentUser)
    {
        _accountService = accountService;
        _showMenu = showMenu;
        _setCurrentUser = setCurrentUser;
    }

    public void Execute()
    {
        Console.WriteLine("Enter username to log in:");
        string userName = Console.ReadLine();

        Console.WriteLine("Enter your password:");
        string password = ReadPassword();

        try
        {
            var account = _accountService.GetAccountByUserName(userName);

            PasswordHasher hasher = new PasswordHasher();
            if (!hasher.VerifyPassword(password, account.PasswordHash))
            {
                throw new ArgumentException("Invalid username or password.");
            }

            Console.WriteLine($"Hello {userName}, enjoy your Tic Tac Toe game!");
            _setCurrentUser(account);
            _showMenu.Invoke();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            _showMenu.Invoke();
        }
    }

    private string ReadPassword()
    {
        StringBuilder passwordBuilder = new StringBuilder();
        while (true)
        {
            var key = Console.ReadKey(intercept: true);
            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }

            if (key.Key == ConsoleKey.Backspace && passwordBuilder.Length > 0)
            {
                passwordBuilder.Remove(passwordBuilder.Length - 1, 1);
                Console.Write("\b \b");
            }
            else if (key.Key != ConsoleKey.Backspace)
            {
                passwordBuilder.Append(key.KeyChar);
                Console.Write("*");
            }
        }

        return passwordBuilder.ToString();
    }
}

