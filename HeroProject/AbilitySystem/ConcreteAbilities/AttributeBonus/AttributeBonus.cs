using UnityEngine;

using BaseGameLogic;
using Hero;

namespace GameAbilitySystem
{
    public class AttributeBonus : Ability, IPassiveAbility
    {
        private readonly AttributeBonusData _data;

        public AttributeBonus(AbilityData abilityData, AttributeBonusData data) : base (abilityData, data.levelsData.Count)
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

            if (_data.isTotalValue)
                AddBonus();
            else
                AddDifferenceBonus();
        }

        private void AddBonus()
        {
            AttributeBonusLevelData data = _data.levelsData[CurrentLevel - 1];

            UnitAttributeChanger changer = new UnitAttributeChanger();
            changer.Change(_owner.GetComponent<UnitData>(), data.unitBonusData);

            HeroAttributeChanger heroChanger = new HeroAttributeChanger();
            heroChanger.Change(_owner.GetComponent<HeroStats>(), data.heroBonusData);
        }

        private void AddDifferenceBonus()
        {
            AttributeBonusLevelData data = _data.levelsData[CurrentLevel - 1];

            if(CurrentLevel != 1)
            {
                AttributeBonusLevelData previousData = _data.levelsData[CurrentLevel - 2];
                data.heroBonusData.value -= previousData.heroBonusData.value;
                data.unitBonusData.value -= previousData.unitBonusData.value;
            }

            UnitAttributeChanger changer = new UnitAttributeChanger();
            changer.Change(_owner.GetComponent<UnitData>(), data.unitBonusData);

            HeroAttributeChanger heroChanger = new HeroAttributeChanger();
            heroChanger.Change(_owner.GetComponent<HeroStats>(), data.heroBonusData);
        }
    }
}