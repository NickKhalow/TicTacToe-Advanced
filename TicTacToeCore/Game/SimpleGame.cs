using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToeCore.Data;
using TicTacToeCore.Field;
using TicTacToeCore.Player;


namespace TicTacToeCore.Game
{
    public class SimpleGame : IGame
    {
        public event Action<FinishType>? Finished;


        public event Action<FieldData>? FieldUpdated;


        public event Action<PlayerData>? AttackerUpdated;


        private readonly IList<IPlayer> playerList;
        private readonly (IPlayer, IPlayer) players;
        private readonly IField field;
        private readonly IDictionary<IPlayer, (FinishType, CellState)> playerMapToCell;


        private bool IsFinished { get; set; }


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
                CurrentTurner.MakeTurn();
                FieldUpdated?.Invoke(field.GetData());
                CurrentTurner = NextPlayer();
                AttackerUpdated?.Invoke(CurrentTurner.GetData());
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

            IsFinished = true;
            Finished?.Invoke(ensuredFinish);
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


        public GameData GetData()
        {
            return new GameData(
                field.GetData(),
                playerList.Select(p => p.GetData()).ToList(),
                playerList.IndexOf(CurrentTurner),
                IsFinished);
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
}