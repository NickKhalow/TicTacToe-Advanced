using TicTacToeCore;
using TicTacToeCore.Data;
using TicTacToeCore.Game;
using TicTacToeCore.Player;


public static class Program
{
    public static void Main(string[] args)
    {
        IGame game = new SimpleGame(new ValueTuple<IPlayer, IPlayer>(
            new SimpleConsolePlayer("Jack", 'X'),
            new SimpleConsolePlayer("Adam", 'O')));

        game.FieldUpdated += GameOnFieldUpdated;
        game.Finished += GameOnFinished;
        game.Start();
    }


    private static void GameOnFieldUpdated(FieldData obj)
    {
        var array = obj.Cells;
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Console.Write($"{array[i, j]}, ");
            }

            Console.Write('\n');
        }
    }


    private static void GameOnFinished(FinishType obj)
    {
        Console.WriteLine($"Game finished as {obj}");
    }
}