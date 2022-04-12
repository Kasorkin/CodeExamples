using UnityEngine;

using BaseGameLogic;

namespace GameAbilitySystem.CostSystem
{
    public sealed class PercentValueCost : Cost
    {
        private readonly PercentValueCostData _data;

        private readonly UnitData _unit;

        public PercentValueCost(PercentValueCostData data, Transform owner) : base(owner, data.levelsData.Count)
        {
            _data = data;

            _unit = owner.GetComponent<UnitData>();
        }

        public sealed override bool CanUse()
        {
            if (_data.attributeType == AttributeCostType.Health)
            {
                float value = CalculateValue(_unit.Health.MaxHealth);
                return _unit.Health.CurrentHealth >= value;
            }
            else
            {
                float value = CalculateValue(_unit.Mana.MaxMana);
                return _unit.Mana.CurrentMana >= value;
            }
        }

        public sealed override bool TryUse()
        {
            if (_data.attributeType == AttributeCostType.Health)
            {
                float value = CalculateValue(_unit.Health.MaxHealth);
                if (_unit.Health.CurrentHealth >= value)
                {
                    _unit.Health.ChangeCurrentHealth(-value);
                    return true;
                }
            }
            else
            {
                float value = CalculateValue(_unit.Mana.MaxMana);
                if (_unit.Mana.CurrentMana >= value)
                {
                    _unit.Mana.ChangeCurrentMana(-value);
                    return true;
                }
            }

            return false;
        }

        public sealed override void Upgrade()
        {
            if(!TryLevelUp())
                return;
        }

        private float CalculateValue(float MaxValue)
        {
            return MaxValue * _data.levelsData[CurrentLevel - 1].percent / 100;
        }
    }
}