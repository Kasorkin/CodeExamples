using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameAbilitySystem.EffectSystem
{
    public abstract class SimpleEffectSO : ScriptableObject
    {
        [Header("Общие данные")]
        [SerializeField]
        protected EffectData _effectData;
    }

    [Serializable]
    public struct EffectData
    {
        public Sprite icon;
        public EffectTags tags;
    }
}