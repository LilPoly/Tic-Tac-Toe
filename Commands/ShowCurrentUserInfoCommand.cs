using TicTacToe.GameAccount.Repo;

namespace TicTacToe.Commands;

public class ShowCurrentUserInfoCommand : ICommand
{
    private readonly IAccountRepository _accountRepository;
    private readonly int _currentUserId;  

    public ShowCurrentUserInfoCommand(IAccountRepository accountRepository, int currentUserId)
    {
        _accountRepository = accountRepository;
        _currentUserId = currentUserId;
    }

    public void Execute()
    {
        try
        {
           
            var currentUser = _accountRepository.ReadAccountById(_currentUserId);

            if (currentUser != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"User Info for ");
                Console.Write($"{currentUser.UserName}\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write($"ID: ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{currentUser.Id}\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write($"Account Type: ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{currentUser.AccountType}\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write($"Rating: ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{currentUser.CurrentRating}\n");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Current user not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
