using System.Collections.Generic;
using UnityEngine;
using System;

using BaseGameLogic.DamageSystem;

namespace GameAbilitySystem
{
    [CreateAssetMenu(fileName = "ArmorAuraSO", menuName = "GameAbilitySystem/PassiveAbilitiesSO/ArmorAura")]
    public class ArmorAuraSO : PassiveAbilitySO
    {
        [Header("Данные способности")]
        [SerializeField]
        private ArmorAuraData _data;

        public override IPassiveAbility CreateAbility()
        {
            return new ArmorAura(_abilityData, _data);
        }
    }

    [Serializable]
    public struct ArmorAuraData
    {
        public List<ArmorAuraLevelData> levelsData;
    };

    [Serializable]
    public struct ArmorAuraLevelData
    {
        public ArmorData _data;
    };
}