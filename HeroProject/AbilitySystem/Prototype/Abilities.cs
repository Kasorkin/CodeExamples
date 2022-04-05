using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameAbilitySystem
{
    [DisallowMultipleComponent]
    public class Abilities : MonoBehaviour
    {
        public event Action<GameAbility> OnAbilityCreated;

        private readonly List<GameAbility> _abilities = new List<GameAbility>();

        [SerializeField]
        private List<GameAbilitySO> _abilitiesData = new List<GameAbilitySO>();

        private void Start()
        {
            CreateAbilities();
        }

        private void CreateAbilities()
        {
            foreach(var k in _abilitiesData)
            {
                CreateAbility(k);
            }    
        }

        private void CreateAbility(GameAbilitySO abilityData)
        {
            GameAbility creatingAbility = new GameAbility(abilityData.RequirementsForImprovement, transform);
            creatingAbility.SetActiveAbility(abilityData.CreateActiveAbility());

            List<IPassiveAbility> passiveAbilities = abilityData.CreatePassiveAbilities();
            
            foreach(var k in passiveAbilities)
            {
                creatingAbility.AddPassiveAbility(k);
            }

            _abilities.Add(creatingAbility);
            //для установки на дисплей? в общем подумай
            OnAbilityCreated?.Invoke(creatingAbility);
        }  
    }
}
