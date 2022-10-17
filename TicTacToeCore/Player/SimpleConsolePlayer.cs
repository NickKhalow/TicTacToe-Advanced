using TicTacToeCore;
using TicTacToeCore.Data;


namespace TicTacToe.Core.Player;

public class SimpleConsolePlayer : IPlayer
{
    private readonly char symbol;


    public event Action<Position>? RequestTurnAt;


    public string Name { get; }


    public SimpleConsolePlayer(string name, char symbol)
    {
        this.symbol = symbol;
        Name = name;
    }


    public void MakeTurn()
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


    public PlayerData GetData()
    {
        return new PlayerData(symbol.ToString(), Name);
    }
}