namespace TicTacToeCore.Data
{
    public struct Position : IData
    {
        public int X { get; }


        public int Y { get; }


        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }


        public override string ToString()
        {
            return $"{X}:{Y}";
        }
    }
}