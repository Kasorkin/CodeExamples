using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameAbilitySystem.EffectSystem
{
    [Serializable]
    public class EffectTags : MonoBehaviour
    {
        [SerializeField]
        private EffectTagSO _ownTag;
        [SerializeField]
        private List<EffectTagSO> _cancelEffectsWithTag = new List<EffectTagSO>();
    }
}