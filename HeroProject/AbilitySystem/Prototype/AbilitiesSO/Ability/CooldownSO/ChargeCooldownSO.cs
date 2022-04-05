using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameAbilitySystem.CooldownSystem
{
    [CreateAssetMenu(fileName = "New ChargeCooldownSO", menuName = "GameAbilitySystem/CooldownSO/ChargeCooldown")]
    public class ChargeCooldownSO : CooldownSO
    {
        [SerializeField]
        private List<ChargeCooldownData> _chargeCooldownLevelsData = new List<ChargeCooldownData>();

        public override Cooldown CreateCooldown()
        {
            return new ChargeCooldown(_chargeCooldownLevelsData);
        }
    }

    [Serializable]
    public struct ChargeCooldownData
    {
        [SerializeField, Min(1)]
        private int _maxCharges;
        [SerializeField, Min(0)]
        private float _cooldownChargeDuration;
    }
}