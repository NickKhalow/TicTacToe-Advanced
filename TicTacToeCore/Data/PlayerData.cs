namespace TicTacToeCore.Data;

public struct PlayerData : IData
{
    public PlayerData(string symbol, string name)
    {
        Symbol = symbol;
        Name = name;
    }


    public string Symbol { get; }


    public string Name { get; }
}