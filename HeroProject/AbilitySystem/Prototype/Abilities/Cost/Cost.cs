using UnityEngine;

namespace GameAbilitySystem.CostSystem
{
    public abstract class Cost
    {
        protected readonly Transform _owner;
        protected readonly int _maxLevel;

        private int _currentLevel;

        protected int CurrentLevel => _currentLevel;

        public Cost(Transform owner, int maxLevel)
        {
            _owner = owner;
            _maxLevel = maxLevel;
        }

        public abstract void Upgrade();
        public abstract bool TryUse();
        public abstract bool CanUse();

        protected bool TryLevelUp()
        {
            if (_currentLevel == _maxLevel)
                return false;

            _currentLevel++;
            return true;
        }
    }
}