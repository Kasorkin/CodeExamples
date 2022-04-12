using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameAbilitySystem.CostSystem
{
    [CreateAssetMenu(fileName = "New PercentValueCostSO", menuName = "GameAbilitySystem/CostSO/PercentValueCost")]
    public class PercentValueCostSO : CostSO
    {
        [Header("Данные способности")]
        [SerializeField]
        private PercentValueCostData _data;

        public override Cost CreateCost(Transform owner)
        {
            return new PercentValueCost(_data, owner);
        }
    }

    [Serializable]
    public struct PercentValueCostData
    {
        public AttributeCostType attributeType;
        public List<PercentValueCostLevelsData> levelsData;
    }

    [Serializable]
    public struct PercentValueCostLevelsData
    {
        [Range(0, 100)]
        public int percent;
    }
}