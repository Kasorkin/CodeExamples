using System;
using System.Collections.Generic;

namespace GameAbilitySystem.CooldownSystem
{
    [Serializable]
    public class ClassicCooldown : Cooldown
    {
        private readonly List<ClassicCooldownData> _cooldownLevelsData;

        public ClassicCooldown(List<ClassicCooldownData> cooldownLevelsData)
        {
            _cooldownLevelsData = cooldownLevelsData;
        }

        public override void StartCooldown()
        {
            throw new System.NotImplementedException();
        }
    }
}