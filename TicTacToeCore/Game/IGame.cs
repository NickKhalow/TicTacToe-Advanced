using TicTacToe.Core;
using TicTacToeCore.Data;


namespace TicTacToeCore;

public interface IGame
{
    event Action<FinishType> Finished; 

    void Start();
}