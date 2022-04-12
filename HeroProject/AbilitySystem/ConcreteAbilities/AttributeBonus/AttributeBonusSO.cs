using System.Collections.Generic;
using UnityEngine;
using System;

using BaseGameLogic;
using Hero;

namespace GameAbilitySystem
{
    [CreateAssetMenu(fileName = "AttributeBonusSO", menuName = "GameAbilitySystem/PassiveAbilitiesSO/AttributeBonus")]
    public class AttributeBonusSO : PassiveAbilitySO
    {
        [Header("Данные способности")]
        [SerializeField]
        private AttributeBonusData _data;

        public override IPassiveAbility CreateAbility()
        {
            return new AttributeBonus(_abilityData, _data);
        }
    }

    [Serializable]
    public struct AttributeBonusData
    {
        public bool isTotalValue;
        public List<AttributeBonusLevelData> levelsData;
    }

    [Serializable]
    public struct AttributeBonusLevelData
    {
        public UnitAttributeBonusData unitBonusData;
        public HeroAttributeBonusData heroBonusData;
    }
}