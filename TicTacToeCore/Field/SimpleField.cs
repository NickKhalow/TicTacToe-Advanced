using TicTacToe.Core.Player;
using TicTacToeCore;
using TicTacToeCore.Data;


namespace TicTacToe.Core.Field;

public class SimpleField : IField
{
    public int Size { get; }


    private readonly CellState[,] cells; //first is row, second is column


    public SimpleField(int size)
    {
        Size = size;
        cells = new CellState[size, size];
    }


    public SimpleField(CellState[,] data)
    {
        Size = data.GetLength(0);
        if (data.GetLength(0) != data.GetLength(1))
        {
            throw new Exception("Data dimensions is not correct");
        }

        cells = new CellState[Size, Size];

        Array.Copy(data, cells, Size * Size);
    }


    public void MakeTurn(CellState state, Position position)
    {
        if (state == CellState.Free)
        {
            throw new ArgumentException(nameof(state));
        }

        if (position.X >= Size || position.Y >= Size || position.X < 0 || position.Y < 0)
        {
            throw new ArgumentOutOfRangeException($"Position {position} is out of range");
        }

        var targetCell = cells[position.X, position.Y];

        if (targetCell != CellState.Free)
        {
            throw new InvalidOperationException();
        }

        cells[position.X, position.Y] = state;
    }


    public bool CheckGameFinished(out FinishType? winner)
    {
        var map = new Dictionary<CellState, FinishType>()
        {
            [CellState.OwnedX] = FinishType.WinX,
            [CellState.OwnedY] = FinishType.WinY
        };

        foreach (var player in new[] {CellState.OwnedX, CellState.OwnedY})
        {
            for (int i = 0; i < Size; i++)
            {
                if (IsColumnFilledBy(i, player)
                    || IsRowFilledBy(i, player)
                    || IsDiagonalFilledBy(player)
                    || IsReversedDiagonalFilledBy(player)
                   )
                {
                    winner = map[player];
                    return true;
                }
            }
        }

        if (IsAllCellsAreOwned())
        {
            winner = FinishType.Draw;
            return true;
        }

        winner = null;
        return false;
    }


    private bool IsAllCellsAreOwned()
    {
        foreach (var cell in cells)
        {
            if (cell == CellState.Free)
            {
                return false;
            }
        }

        return true;
    }


    public CellState GetAt(Position position)
    {
        return cells[position.X, position.Y];
    }


    private bool IsColumnFilledBy(int columnIndex, in CellState state)
    {
        return IsFilledBy(i => cells[i, columnIndex], state);
    }


    private bool IsRowFilledBy(int rowIndex, in CellState state)
    {
        return IsFilledBy(i => cells[rowIndex, i], state);
    }


    private bool IsDiagonalFilledBy(in CellState state)
    {
        return IsFilledBy(i => cells[i, i], state);
    }


    private bool IsReversedDiagonalFilledBy(in CellState state)
    {
        return IsFilledBy(i => cells[Size - 1 - i, Size - 1 - i], state);
    }


    private bool IsFilledBy(in Func<int, CellState> cellSelector, in CellState state)
    {
        for (var i = 0; i < Size; i++)
        {
            if (cellSelector(i) != state)
            {
                return false;
            }
        }

        return true;
    }


    public CellState[,] ToArray()
    {
        var output = new CellState[Size, Size];
        Array.Copy(cells, output, Size * Size);
        return output;
    }
}