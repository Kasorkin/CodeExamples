using UnityEngine;
using System;

using Framework;

namespace GameAbilitySystem
{
    [Serializable]
    public class InfluenceOfAbility
    {
        [SerializeField, EnumFlags]
        private FractionInfluence _fractionsOfInfluence;
        [SerializeField, EnumFlags]
        private UnitTypeInfluence _unitTypeInInfluence;
    }
}