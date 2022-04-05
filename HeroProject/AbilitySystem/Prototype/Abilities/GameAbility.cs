using System.Collections.Generic;
using UnityEngine;

namespace GameAbilitySystem
{
    public class GameAbility
    {
        private readonly Transform _owner;
        private readonly List<IPassiveAbility> _passiveAbilities = new List<IPassiveAbility>();

        private IActiveAbility _activeAbility = null;

        public GameAbility(RequirementsForImprovement requirements, Transform owner)
        {
            RequirementsForImprovement = requirements;
            _owner = owner;
        }

        public RequirementsForImprovement RequirementsForImprovement { get; }

        #region Bulder
        public void SetActiveAbility(IActiveAbility activeAbility)
        {
            _activeAbility = activeAbility;
            _activeAbility.Init(_owner);
        }

        public void AddPassiveAbility(IPassiveAbility passiveAbility)
        {
            _passiveAbilities.Add(passiveAbility);
            _passiveAbilities[_passiveAbilities.Count - 1].Init(_owner);
        }
        #endregion

        public void Upgrade()
        {
            RequirementsForImprovement.Upgrade();
            _activeAbility.Upgrade();
            foreach(var k in _passiveAbilities)
            {
                k.Upgrade();
            }
        }
    }
}