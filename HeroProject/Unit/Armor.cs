using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseGameLogic
{
    [System.Serializable]
    public class Armor
    {
        public event Action<Armor> OnPhysicArmorChanged;
        public event Action<Armor> OnMagicArmorChanged;

        [Header("Характеристики")]
        [SerializeField]
        private float _physicalArmor;
        [SerializeField]
        private float _magicalArmor;

        [Header("Бонусные характеристики")]
        [SerializeField]
        private float _bonusPhysicalArmor;
        [SerializeField]
        private float _bonusMagicalArmor;

        public float GetSummaryPhysicalArmor => _physicalArmor + _bonusPhysicalArmor;
        public float GetSummaryMagicalArmor => _magicalArmor + _bonusMagicalArmor;

        public float GetPhysicalArmor => _physicalArmor;
        public float GetMagicalArmor => _magicalArmor;
        public float GetBonusPhysicalArmor => _bonusPhysicalArmor;
        public float GetBonusMagicalArmor => _bonusMagicalArmor;

        public void ArmorHandler(ref Damage damageData)
        {
            PhysicArmorHandler(ref damageData.PhysicalDamage);
            MagicArmorHandler(ref damageData.MagicalDamage);
        }

        private void PhysicArmorHandler(ref float damage)
        {
            damage /= GetSummaryPhysicalArmor * Consts.ArmorReductionFactor + 1;
        }

        private void MagicArmorHandler(ref float damage)
        {
            damage /= GetSummaryMagicalArmor * Consts.ArmorReductionFactor + 1;
        }

        public void ChangePhysicalArmor(float value)
        {
            _physicalArmor += value;

            OnPhysicArmorChanged?.Invoke(this);
        }

        public void ChangeMagicalArmor(float value)
        {
            _magicalArmor += value;

            OnMagicArmorChanged?.Invoke(this);
        }

        public void ChangeBonusPhysicalArmor(float value)
        {
            _bonusPhysicalArmor += value;

            OnPhysicArmorChanged?.Invoke(this);
        }

        public void ChangeBonusMagicalArmor(float value)
        {
            _bonusMagicalArmor += value;

            OnMagicArmorChanged?.Invoke(this);
        }
    }
}