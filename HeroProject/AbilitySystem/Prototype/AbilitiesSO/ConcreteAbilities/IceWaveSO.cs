using System.Collections.Generic;
using UnityEngine;

using GameAbilitySystem.CooldownSystem;

using BaseGameLogic.DamageSystem;

namespace GameAbilitySystem
{
    [CreateAssetMenu(fileName = "IceWallSO", menuName = "GameAbilitySystem/AbilitiesSO/IceWall")]
    public sealed class IceWaveSO : ActiveAbilitySO
    {
        [Header("Данные способности")]
        [SerializeField]
        private List<IceWaveData> _iceWaveLevelsData = new List<IceWaveData>();
        [SerializeField]
        private CooldownSO cooldownSO;

        public override IActiveAbility CreateAbility()
        {
            return new IceWave(_icon, _iceWaveLevelsData);
        }
    }
    
    [System.Serializable]
    public struct IceWaveData
    {
        public float width;
        public float length;
        public DamageData damage;
        public Cost cost; //?
    };
}