using System.Text;
using TicTacToe.GameAccount;
using TicTacToe.GameAccount.Repo;
using TicTacToe.GameAccount.Service;

namespace TicTacToe.Commands;

public class RegistrationCommand : ICommand
{
    private readonly IAccountService _accountService;
    private readonly Action _onRegistrationComplete;

    public RegistrationCommand(IAccountService accountService, Action onRegistrationComplete)
    {
        _accountService = accountService;
        _onRegistrationComplete = onRegistrationComplete;
    }

    public void Execute()
    {
        Console.WriteLine("Please enter your username:");
        string userName = Console.ReadLine();

        Console.WriteLine("Select account type (Standart/Vip):");
        string accountTypeInput = Console.ReadLine();

        Console.WriteLine("Please enter your password:");
        string password = ReadPassword();

        Console.WriteLine("Please confirm your password:");
        string confirmPassword = ReadPassword();

        if (password != confirmPassword)
        {
            Console.WriteLine("Passwords do not match. Registration failed.");
            return;
        }

        try
        {
            _accountService.CreateAccount(userName, accountTypeInput, password);
            Console.WriteLine($"Account created successfully! Username: {userName}, Type: {accountTypeInput}");
            Console.WriteLine($"Hello {userName}, you can now log in.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        _onRegistrationComplete();
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




