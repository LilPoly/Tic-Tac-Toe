namespace TicTacToe.Game;

public class TicTacToe 
{
    private readonly char[,] _board;
    private const int BoardSize = 3;

    public TicTacToe()
    {
        _board = new char[BoardSize, BoardSize];
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                _board[i, j] = ' ';
            }
        }
    }

    public string StartGame(string player1, string player2)
    {
        string currentPlayer = player1;
        char currentSymbol = 'X';
        int moves = 0;

        while (true)
        {
            PrintBoard();
            Console.WriteLine($"{currentPlayer}'s turn ({currentSymbol}). Enter row and column (0-2):");

            int row, col;
            while (!TryGetInput(out row, out col) || !IsCellEmpty(row, col))
            {
                Console.WriteLine("Invalid move. Try again.");
            }

            _board[row, col] = currentSymbol;
            moves++;

            if (IsWinner(currentSymbol))
            {
                PrintBoard();
                Console.WriteLine($"{currentPlayer} wins!");
                return currentPlayer;
            }

            if (moves == BoardSize * BoardSize)
            {
                PrintBoard();
                Console.WriteLine("It's a draw!");
                return $"{player1} and {player2} draw"; 
            }
            currentPlayer = currentPlayer == player1 ? player2 : player1;
            currentSymbol = currentSymbol == 'X' ? 'O' : 'X';
        }
    }

    private bool TryGetInput(out int row, out int col)
    {
        row = col = -1;
        var input = Console.ReadLine()?.Split();

        if (input != null && input.Length == 2 &&
            int.TryParse(input[0], out row) && int.TryParse(input[1], out col))
        {
            return row >= 0 && row < BoardSize && col >= 0 && col < BoardSize;
        }

        return false;
    }

    private bool IsCellEmpty(int row, int col)
    {
        return _board[row, col] == ' ';
    }

    private bool IsWinner(char symbol)
    {
        for (int i = 0; i < BoardSize; i++)
        {
            if ((_board[i, 0] == symbol && _board[i, 1] == symbol && _board[i, 2] == symbol) ||
                (_board[0, i] == symbol && _board[1, i] == symbol && _board[2, i] == symbol))
            {
                return true;
            }
        }

        return (_board[0, 0] == symbol && _board[1, 1] == symbol && _board[2, 2] == symbol) ||
               (_board[0, 2] == symbol && _board[1, 1] == symbol && _board[2, 0] == symbol);
    }

    private void PrintBoard()
    {
        Console.WriteLine("Board:");
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                Console.Write($" {_board[i, j]} ");
                if (j < BoardSize - 1) Console.Write("|");
            }

            Console.WriteLine();
            if (i < BoardSize - 1) Console.WriteLine("---+---+---");
        }
    }
}