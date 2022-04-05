using UnityEngine;

namespace GameAbilitySystem
{
    public abstract class AbilitySO : ScriptableObject
    {
        [SerializeField]
        protected Sprite _icon;
        [SerializeField, Min(1)]
        protected int _maxAbilityLevel = 1;
        [SerializeField]
        protected InfluenceOfAbility _influence;
        [SerializeField]
        protected AbilityTags _abilityTags;
    }
}