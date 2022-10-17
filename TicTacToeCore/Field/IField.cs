using TicTacToeCore;
using TicTacToeCore.Data;


namespace TicTacToe.Core.Field;

public interface IField
{
    int Size { get; }


    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    void MakeTurn(CellState state, Position position);


    bool CheckGameFinished(out FinishType? winner);


    CellState GetAt(Position position);


    CellState[,] ToArray();
}