using TicTacToeCore;


namespace TicTacToe.Core.Player;

public interface IPlayer
{
    string Name { get; }


    event Action<Position> RequestTurnAt;


    void NotifyTimeToTurn();


    void NotifyGameEnded(bool isWinner);
}