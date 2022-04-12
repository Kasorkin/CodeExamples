using UnityEngine;

namespace GameAbilitySystem
{
    public abstract class Ability
    {
        protected readonly int _maxLevel;
        protected readonly AbilityData _abilityData;

        protected Transform _owner;
        private int _currentLevel = 0;

        protected int CurrentLevel => _currentLevel;

        public Ability(AbilityData abilityData, int maxLevel)
        {
            _abilityData = abilityData;
            _maxLevel = maxLevel;
        }

        public Sprite Icon => Icon;

        protected bool TryLevelUp()
        {
            if (_currentLevel == _maxLevel)
                return false;

            _currentLevel++;
            return true;
        }
    }
}