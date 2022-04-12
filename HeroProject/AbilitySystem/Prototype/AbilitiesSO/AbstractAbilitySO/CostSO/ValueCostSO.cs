using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameAbilitySystem.CostSystem
{
    [CreateAssetMenu(fileName = "New ValueCostSO", menuName = "GameAbilitySystem/CostSO/ValueCost")]
    public class ValueCostSO : CostSO
    {
        [Header("Данные способности")]
        [SerializeField]
        private ValueCostData _data;

        public override Cost CreateCost(Transform owner)
        {
            return new ValueCost(_data, owner);
        }
    }

    [Serializable]
    public struct ValueCostData
    {
        public AttributeCostType attributeType;
        public List<ValueCostLevelData> levelsData;
    };

    [Serializable]
    public struct ValueCostLevelData
    {
        public float value;
    };
}