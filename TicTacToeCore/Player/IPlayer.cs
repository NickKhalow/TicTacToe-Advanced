using TicTacToeCore;
using TicTacToeCore.Data;


namespace TicTacToe.Core.Player;

public interface IPlayer : IDataDriven<PlayerData>
{
    string Name { get; }


    event Action<Position> RequestTurnAt;


    void MakeTurn();


    void NotifyGameEnded(bool isWinner);
}