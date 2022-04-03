using UnityEngine;

namespace BaseGameLogic.Player
{
    public class PlayerData
    {
        public System.Action<int> OnMoneyChanged;
        public System.Action<int> OnGemsChanged;

        public PlayerData(string name, int index, Fraction fraction)
        {
            Name = name;
            Index = index;
            Fraction = fraction;
        }

        public int Index { get; private set; }
        public string Name { get; private set; }
        public Fraction Fraction { get; private set; }

        public int Money { get; private set; }
        public int Gems { get; private set; }

        public void ChangeMoney(in int value)
        {
            Debug.Log(Name + " получил " + value + " gold");
            Money = Mathf.Clamp(Money + value, 0, int.MaxValue);

            OnMoneyChanged?.Invoke(Money);
        }

        public void ChangeGems(in int value)
        {
            Gems = Mathf.Clamp(Gems + value, 0, int.MaxValue);

            OnGemsChanged?.Invoke(Gems);
        }
    }
}