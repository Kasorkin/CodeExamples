using System.Collections.Generic;
using UnityEngine;

namespace GameAbilitySystem
{
    [CreateAssetMenu(fileName = "New GameAbilitySO", menuName = "GameAbilitySystem/GameAbility")]
    public class GameAbilitySO : ScriptableObject
    {
        [SerializeField]
        private ActiveAbilitySO _activeAbility;
        [SerializeField]
        private List<PassiveAbilitySO> _passiveAbilities = new List<PassiveAbilitySO>();
        [SerializeField]
        private RequirementsForImprovement _requirementsForImprovement;

        public RequirementsForImprovement RequirementsForImprovement => _requirementsForImprovement;

        public IActiveAbility CreateActiveAbility()
        {
            return _activeAbility.CreateAbility();
        }

        public List<IPassiveAbility> CreatePassiveAbilities()
        {
            List<IPassiveAbility> abilities = new List<IPassiveAbility>();

            foreach(var k in _passiveAbilities)
            {
                IPassiveAbility passiveAbility = k.CreateAbility();
                abilities.Add(passiveAbility);
            }

            return abilities;
        }
    }
}
