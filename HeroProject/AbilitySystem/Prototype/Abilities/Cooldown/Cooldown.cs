using UnityEngine;
using System;

namespace GameAbilitySystem.CooldownSystem
{
    [Serializable]
    public abstract class Cooldown
    {
        [SerializeField]
        private CooldownType _cooldownType;

        public abstract void StartCooldown();
    }
}