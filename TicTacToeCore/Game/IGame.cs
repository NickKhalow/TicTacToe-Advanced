using TicTacToe.Core;
using TicTacToeCore.Data;


namespace TicTacToeCore;

public interface IGame : IDataDriven<GameData>
{
    event Action<FinishType> Finished;


    event Action<FieldData> FieldUpdated;


    event Action<PlayerData> AttackerUpdated;

    void Start();
}