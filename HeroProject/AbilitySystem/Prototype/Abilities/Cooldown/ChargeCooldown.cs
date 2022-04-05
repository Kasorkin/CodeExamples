using System;
using System.Collections.Generic;

namespace GameAbilitySystem.CooldownSystem
{
    [Serializable]
    public class ChargeCooldown : Cooldown
    {
        private readonly List<ChargeCooldownData> _chargeCooldownLevelsData = new List<ChargeCooldownData>();

        private int _currentCharges;

        public ChargeCooldown(List<ChargeCooldownData> chargeCooldownLevelsData)
        {
            _chargeCooldownLevelsData = chargeCooldownLevelsData;
        }

        public override void StartCooldown()
        {
            throw new System.NotImplementedException();
        }
    }
}