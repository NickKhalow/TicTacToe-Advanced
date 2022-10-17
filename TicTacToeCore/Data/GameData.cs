using System.Collections.Generic;


namespace TicTacToeCore.Data
{
    public struct GameData : IData
    {
        public GameData(FieldData fieldData, IReadOnlyList<PlayerData> playerDatas, int currentAttackerPlayerId)
        {
            FieldData = fieldData;
            PlayerDatas = playerDatas;
            CurrentAttackerPlayerId = currentAttackerPlayerId;
        }


        public FieldData FieldData { get; }


        public IReadOnlyList<PlayerData> PlayerDatas { get; }


        public int CurrentAttackerPlayerId { get; }
    }
}