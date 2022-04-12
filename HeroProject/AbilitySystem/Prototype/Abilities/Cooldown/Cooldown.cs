using System.Collections;
using UnityEngine;

namespace GameAbilitySystem.CooldownSystem
{
    public abstract class Cooldown
    {
        private readonly int _maxLevel;

        protected Coroutine _cooldownCoroutine; 
        protected float _cooldownDuration;

        private int _currentLevel = 0;

        public Cooldown(int maxLevel)
        {
            _maxLevel = maxLevel;
        }

        protected int CurrentLevel => _currentLevel;

        public abstract void StartCooldown();
        public abstract bool IsReady();
        public abstract void Upgrade();

        protected abstract IEnumerator Cooldowning();

        protected bool TryLevelUp()
        {
            if (_currentLevel == _maxLevel)
                return false;

            _currentLevel++;
            return true;
        }
    }
}