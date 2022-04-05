using UnityEngine;

namespace GameAbilitySystem.CooldownSystem
{
    public abstract class CooldownSO : ScriptableObject
    {
        public abstract Cooldown CreateCooldown();
    }
}