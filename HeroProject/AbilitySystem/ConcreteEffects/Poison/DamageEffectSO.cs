using System;
using UnityEngine;
using System.Collections.Generic;

using BaseGameLogic.DamageSystem;

namespace GameAbilitySystem.EffectSystem
{
    [CreateAssetMenu(fileName = "New DamageEffectSO", menuName = "GameAbilitySystem/EffectSO/DamageEffect")]
    public class DamageEffectSO : PeriodicSimpleEffectSO
    {
        [Header("Данные способности")]
        [SerializeField]
        private DamageEffectData _damageData;

        public override IPeriodicSimpleEffect CreatePeriodicEffect()
        {
            return new DamageEffect(_damageData, _effectData);
        }
    }

    [Serializable]
    public struct DamageEffectData
    {
        public List<DamageEffectLevelData> levelsData;
    }

    [Serializable]
    public struct DamageEffectLevelData
    {
        public DamageData damage;  
        public float periodic;
        public float duration;
    }
}