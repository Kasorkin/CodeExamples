using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAbilitySystem
{
    public class ArmorAura : Ability, IPassiveAbility
    {
        private readonly ArmorAuraData _data;

        public ArmorAura(AbilityData abilityData, ArmorAuraData data) : base(abilityData, data.levelsData.Count)
        {
            _data = data;
        }

        public void Init(Transform owner)
        {
            _owner = owner;
        }

        public void OnDisable()
        {
            throw new System.NotImplementedException();
        }

        public void OnEnable()
        {
            throw new System.NotImplementedException();
        }

        public void Upgrade()
        {
            if (!TryLevelUp())
                return;
        }
    }
}