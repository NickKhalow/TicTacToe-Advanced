using System.Collections.Immutable;
using TicTacToe.Core;
using TicTacToe.Core.Field;
using TicTacToe.Core.Player;
using TicTacToeCore.Data;


namespace TicTacToeCore;

public class SimpleGame : IGame
{
    public event Action<FinishType>? Finished;


    private readonly IPlayer[] playerList;
    private readonly (IPlayer, IPlayer) players;
    private readonly IField field;
    private readonly IDictionary<IPlayer, (FinishType, CellState)> playerMapToCell;


    private IPlayer CurrentTurner { get; set; }


    private IPlayer FirstPlayer => players.Item1;


    private IPlayer SecondPlayer => players.Item2;


    public SimpleGame((IPlayer, IPlayer) players, int fieldSize = 3)
    {
        this.players = players;
        playerList = new[] {FirstPlayer, SecondPlayer};
        playerMapToCell = new Dictionary<IPlayer, (FinishType, CellState)>
        {
            [FirstPlayer] = (FinishType.WinX, CellState.OwnedX),
            [SecondPlayer] = (FinishType.WinY, CellState.OwnedY)
        };
        field = new SimpleField(fieldSize);

        CurrentTurner = FirstPlayer;
    }


    private IPlayer NextPlayer()
    {
        return CurrentTurner == FirstPlayer ? SecondPlayer : FirstPlayer;
    }


    public void Start()
    {
        var moveWrappers = new List<MoveWrapper>();

        foreach (var player in playerList)
        {
            var wrapper = new MoveWrapper(player, OnPlayerMove);
            moveWrappers.Add(wrapper);
        }

        FinishType? finish;

        while (!field.CheckGameFinished(out finish))
        {
            CurrentTurner.NotifyTimeToTurn();
            DrawField();
            CurrentTurner = NextPlayer();
        }

        var ensuredFinish = (FinishType) finish!;

        foreach (var moveWrapper in moveWrappers)
        {
            moveWrapper.Dispose();
        }

        foreach (var player in playerList)
        {
            player.NotifyGameEnded(playerMapToCell[player].Item1 == ensuredFinish);
        }

        Finished?.Invoke(ensuredFinish);
    }


    private void DrawField()
    {
        var array = field.ToArray();
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Console.Write($"{array[i, j]}, ");
            }

            Console.Write('\n');
        }
    }


    private void OnPlayerMove(IPlayer player, Position position)
    {
        if (player == CurrentTurner)
        {
            field.MakeTurn(playerMapToCell[player].Item2, position);
        }
        else
        {
            Console.WriteLine($"{player.Name} is not current turner");
        }
    }


    private class MoveWrapper : IDisposable
    {
        private readonly IPlayer player;
        private readonly Action<IPlayer, Position> onPlayerMove;


        public MoveWrapper(IPlayer player, Action<IPlayer, Position> onPlayerMove)
        {
            this.player = player;
            this.onPlayerMove = onPlayerMove;
            player.RequestTurnAt += ReceiveMove;
        }


        public void Dispose()
        {
            player.RequestTurnAt -= ReceiveMove;
        }


        private void ReceiveMove(Position position)
        {
            onPlayerMove(player, position);
        }
    }
}