using System.Collections.Generic;

namespace BaseGameLogic.Player
{
    public class PlayerManager
    {
        public PlayerManager()
        {
            CreateAIPlayers();
        }

        public Dictionary<int, PlayerData> Players { get; } = new Dictionary<int, PlayerData>();

        public void CreatePlayer(string playerName, Fraction fraction)
        {
            PlayerData playerData = new PlayerData(playerName, Players.Count, fraction);
            Players.Add(Players.Count, playerData);
        }

        public void Distributor(in UnitData unit)
        {
            foreach(var k in Players)
            {
                if(k.Value.Fraction == unit.StartFraction)
                {
                    unit.Owner = k.Value;
                }
            }
        }

        private void CreateAIPlayers()
        {
            CreatePlayer("Light", Fraction.Light);
            CreatePlayer("Dark", Fraction.Dark);
            CreatePlayer("NeutralEnemy", Fraction.Neutral);
        }
    }
}