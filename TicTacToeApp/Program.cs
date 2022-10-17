using TicTacToe.Core.Field;
using TicTacToe.Core.Player;
using TicTacToeCore;
using TicTacToeCore.Data;


public static class Program
{
    public static void Main(string[] args)
    {
        IGame game = new SimpleGame(new ValueTuple<IPlayer, IPlayer>(
            new SimpleConsolePlayer("Jack"),
            new SimpleConsolePlayer("Adam")));
        
        game.Finished += GameOnFinished;
        game.Start();
    }


    private static void GameOnFinished(FinishType obj)
    {
        Console.WriteLine($"Game finished as {obj}");
    }
}