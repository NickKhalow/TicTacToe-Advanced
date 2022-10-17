using System;
using TicTacToeCore.Data;


namespace TicTacToeCore.Player
{
    public interface IPlayer : IDataDriven<PlayerData>
    {
        string Name { get; }


        event Action<Position> RequestTurnAt;


        void MakeTurn();


        void NotifyGameEnded(bool isWinner);
    }
}