using UnityEngine;

namespace GameAbilitySystem.CooldownSystem
{
    public abstract class CooldownSO : ScriptableObject
    {
        //[SerializeField]
        //protected bool _isOneValue;

        public abstract Cooldown CreateCooldown();
    }
}