using System;
using TicTacToeCore.Data;


namespace TicTacToeCore.Game
{
    public interface IGame : IDataDriven<GameData>
    {
        event Action<FinishType> Finished;


        event Action<FieldData> FieldUpdated;


        event Action<PlayerData> AttackerUpdated;

        void Start();
    }
}