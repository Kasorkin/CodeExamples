using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAbilitySystem.EffectSystem
{
    public abstract class SimpleEffect
    {
        protected readonly EffectData _effectData;

        protected Transform _owner;

        private readonly int _maxLevel;
        private int _currentLevel;

        protected int CurrentLevel => _currentLevel;

        public SimpleEffect(EffectData effectData, int maxLevel)
        {
            _effectData = effectData;
            _maxLevel = maxLevel;
        }

        protected abstract IEnumerator Duration();

        public Sprite Icon => _effectData.icon;

        protected bool TryLevelUp()
        {
            if (_currentLevel == _maxLevel)
                return false;

            _currentLevel++;
            return true;
        }
    }
}