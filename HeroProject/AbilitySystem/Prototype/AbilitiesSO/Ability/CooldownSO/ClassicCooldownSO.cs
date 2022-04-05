using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameAbilitySystem.CooldownSystem
{
    [CreateAssetMenu(fileName = "New ClassicCooldownSO", menuName = "GameAbilitySystem/CooldownSO/ClassicCooldown")]
    public class ClassicCooldownSO : CooldownSO
    {
        [SerializeField]
        private List<ClassicCooldownData> _cooldownLevelsData = new List<ClassicCooldownData>();
        
        public override Cooldown CreateCooldown()
        {
            return new ClassicCooldown(_cooldownLevelsData);
        }
    }

    [Serializable]
    public struct ClassicCooldownData
    {
        [SerializeField, Min(0)]
        private float _cooldownDuration;
    }
}