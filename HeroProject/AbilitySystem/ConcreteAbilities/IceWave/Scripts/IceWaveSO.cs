using System;
using System.Collections.Generic;
using UnityEngine;

using GameAbilitySystem.CooldownSystem;
using GameAbilitySystem.CostSystem;
using GameAbilitySystem.EffectSystem;

namespace GameAbilitySystem
{
    [CreateAssetMenu(fileName = "IceWaveSO", menuName = "GameAbilitySystem/ActiveAbilitiesSO/IceWave")]
    public sealed class IceWaveSO : ActiveAbilitySO
    {
        [Header("Данные способности")]
        [SerializeField]
        private IceWaveData _iceWaveData;

        public override IActiveAbility CreateAbility()
        {
            return new IceWave(_abilityData, _iceWaveData);
        }
    }
    
    [Serializable]
    public struct IceWaveData
    {
        public float width;
        public float length;
        public float distance;
        public float waveSpeed;
        public CooldownSO cooldownSO;
        public CostSO costSO;
        public List<GameEffectSO> effects;
        public List<IceWaveLevelData> levelsData;
    };

    [Serializable]
    public struct IceWaveLevelData
    {
        //[Tooltip("формула: strength * intelligence + summaryDamage * 2")]
        //public DamageData damage;
    }
}