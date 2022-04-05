using UnityEngine;

namespace GameAbilitySystem
{
    public abstract class Ability
    {
        protected readonly int _maxLevel;
        protected readonly Sprite _icon;

        protected int _currentLevel = 0;

        public Ability(Sprite icon, int maxLevel)
        {
            _icon = icon;
            _maxLevel = maxLevel;
        }
    }
}