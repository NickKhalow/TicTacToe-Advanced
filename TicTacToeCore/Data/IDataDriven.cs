namespace TicTacToeCore.Data
{
    public interface IDataDriven<T> where T : IData
    {
        T GetData();
    }
}