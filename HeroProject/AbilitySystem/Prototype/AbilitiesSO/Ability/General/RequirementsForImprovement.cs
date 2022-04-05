using System;
using UnityEngine;

namespace GameAbilitySystem
{
    [Serializable]
    public class RequirementsForImprovement
    {
        [SerializeField, Min(1)]
        private int _requiredLevel = 1;
        [SerializeField, Min(1)]
        private int _skipLevel = 1;

        public int RequiredLevel => _requiredLevel; 
        public int SkipLevel => _skipLevel;

        public void Upgrade()
        {
            _skipLevel += _skipLevel;
        }
    }
}