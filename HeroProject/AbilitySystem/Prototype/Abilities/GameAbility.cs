using System.Collections.Generic;
using UnityEngine;

namespace GameAbilitySystem
{
    //TODO: ¬ынести сюда общий уровень способности?
    //TODO : —делать здесь возможность получить способность по типу?
    public class GameAbility
    {     
        private readonly Transform _owner;
        private readonly List<IPassiveAbility> _passiveAbilities = new List<IPassiveAbility>();

        private Sprite _dispayIcon;

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
            if (activeAbility == null)
                return;

            _activeAbility = activeAbility;
            _activeAbility.Init(_owner);

            _dispayIcon = _activeAbility.Icon;
        }

        public void AddPassiveAbility(IPassiveAbility passiveAbility)
        {
            if (passiveAbility == null)
                return;

            _passiveAbilities.Add(passiveAbility);
            _passiveAbilities[_passiveAbilities.Count - 1].Init(_owner);

            if (_activeAbility == null && _passiveAbilities.Count == 1)
                _dispayIcon = _passiveAbilities[0].Icon;
        }
        #endregion

        public void Upgrade()
        {
            RequirementsForImprovement.Upgrade();
            _activeAbility?.Upgrade();
            foreach(var k in _passiveAbilities)
            {
                k.Upgrade();
            }
        }
    }
}