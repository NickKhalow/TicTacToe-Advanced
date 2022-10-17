using TicTacToeCore;


namespace TicTacToe.Core.Player;

public class SimpleConsolePlayer : IPlayer
{
    public event Action<Position>? RequestTurnAt;


    public string Name { get; }


    public SimpleConsolePlayer(string name)
    {
        Name = name;
    }


    public void NotifyTimeToTurn()
    {
        Console.WriteLine($"{Name}: my time to turn");
        string input = Console.ReadLine()!;
        RequestTurnAt?.Invoke(
            new Position(
                int.Parse(input[0].ToString()),
                int.Parse(input[1].ToString())
            ));
    }


    public void NotifyGameEnded(bool isWinner)
    {
        Console.WriteLine($"{Name}: {(isWinner ? "I'm winner!" : "I'm not winner :(")}");
    }
}