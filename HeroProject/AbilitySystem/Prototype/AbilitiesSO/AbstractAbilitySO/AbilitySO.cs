using UnityEngine;
using System;

namespace GameAbilitySystem
{
    public abstract class AbilitySO : ScriptableObject
    {
        [Header("Общие данные")]
        [SerializeField]
        protected AbilityData _abilityData;

        protected int _maxAbilityLevel = 1;
    }

    [Serializable]
    public struct AbilityData
    {
        public Sprite icon;
        public InfluenceOfAbility influence;
        public AbilityTags abilityTags;
    }
}