using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameAbilitySystem.EffectSystem
{
    [CreateAssetMenu(fileName = "New SlowEffectSO", menuName = "GameAbilitySystem/EffectSO/SlowEffect")]
    public sealed class SlowSO : ConstantSimpleEffectSO
    {
        [SerializeField]
        private SlowData _data;

        public sealed override ISimpleEffect CreateEffect()
        {
            return new Slow(_effectData, _data);
        }
    }

    [Serializable]
    public struct SlowData
    {
        public List<SlowLevelData> levelsData;
    }

    [Serializable]
    public struct SlowLevelData
    {
        [Range(0, 100)]
        public float percent;
        public float duration;
    }
}