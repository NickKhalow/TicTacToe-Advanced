using TicTacToeCore.Data;


namespace TicTacToeCore.Field
{
    public interface IField : IDataDriven<FieldData>
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
}