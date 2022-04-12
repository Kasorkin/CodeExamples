using UnityEngine;

using BaseGameLogic;

namespace GameAbilitySystem.CostSystem
{
    //TODO : Может стоит вынести абстракции под абстрактный класс Upgrade? (но тогда вроде интерфейс подходит, но с другой стороны тогда смысл теряется)
    public sealed class ValueCost : Cost
    {
        private readonly ValueCostData _valueCostData;

        private readonly UnitData _unit;

        public ValueCost(ValueCostData data, Transform owner) : base(owner, data.levelsData.Count)
        {
            _valueCostData = data;

            _unit = owner.GetComponent<UnitData>();
        }

        public sealed override void Upgrade()
        {
            if (!TryLevelUp())
                return;
        }

        public sealed override bool CanUse()
        {
            if (_valueCostData.attributeType == AttributeCostType.Health)
                return _unit.Health.CurrentHealth >= _valueCostData.levelsData[CurrentLevel - 1].value;
            else
                return _unit.Mana.CurrentMana >= _valueCostData.levelsData[CurrentLevel - 1].value;

        }

        public sealed override bool TryUse()
        {
            if (_valueCostData.attributeType == AttributeCostType.Health)
            {
                if (_unit.Health.CurrentHealth >= _valueCostData.levelsData[CurrentLevel - 1].value)
                {
                    _unit.Health.ChangeCurrentHealth(-_valueCostData.levelsData[CurrentLevel - 1].value);
                    return true;
                }
            }
            else
            {
                if (_unit.Mana.CurrentMana >= _valueCostData.levelsData[CurrentLevel - 1].value)
                {
                    _unit.Mana.ChangeCurrentMana(-_valueCostData.levelsData[CurrentLevel - 1].value);
                    return true;
                }
            }

            return false;
        }
    }
}