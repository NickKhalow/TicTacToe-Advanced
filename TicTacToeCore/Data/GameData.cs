using System.Collections.Generic;


namespace TicTacToeCore.Data
{
    public struct GameData : IData
    {
        public GameData(FieldData fieldData, IReadOnlyList<PlayerData> playerDatas, int currentAttackerPlayerId,
            bool isFinished)
        {
            FieldData = fieldData;
            PlayerDatas = playerDatas;
            CurrentAttackerPlayerId = currentAttackerPlayerId;
            IsFinished = isFinished;
        }


        public FieldData FieldData { get; }


        public IReadOnlyList<PlayerData> PlayerDatas { get; }


        public int CurrentAttackerPlayerId { get; }


        public bool IsFinished { get; }
    }
}