using System;
using System.Collections.Generic;
using UnityEngine;

using GameAbilitySystem.CooldownSystem;

namespace GameAbilitySystem
{
    [CreateAssetMenu(fileName = "IceArmorSO", menuName = "GameAbilitySystem/PassiveAbilitiesSO/IceArmor")]
    public class IceArmorSO : PassiveAbilitySO
    {
        [Header("Данные способности")]
        [SerializeField]
        private IceArmorData _data;

        public override IPassiveAbility CreateAbility()
        {
            return new IceArmor(_abilityData, _data);
        }
    }

    [Serializable]
    public struct IceArmorData
    {
        [Range(1, 100)]
        public int PercentHP;
        public CooldownSO cooldown;
        public List<IceArmorLevelData> levelsData;   
    }

    [Serializable]
    public struct IceArmorLevelData
    {
        [Min(0)]
        public float duration;
    };
}