using TicTacToe.Core;


namespace TicTacToeCore.Data;

public struct FieldData : IData
{
    public CellState[,] Cells { get; } = { };


    public FieldData(CellState[,] cells)
    {
        Cells = cells;
    }
}