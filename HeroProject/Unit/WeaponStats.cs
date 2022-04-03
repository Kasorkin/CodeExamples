using UnityEngine;
using System;

namespace BaseGameLogic
{
    //TODO : нужно добавить значение радиуса коллайдера к сумме общей ренджи
    [Serializable]
    public class WeaponStats
    {
        [Header("Характеристики")]
        [SerializeField]
        private DamageType _damageType;
        [SerializeField]
        private float _damage;
        [SerializeField]
        private float _attackCooldown;
        [SerializeField]
        private float _attackRange;

        [Header("Бонусные характеристики")]
        [SerializeField]
        private float _bonusDamage;
        [SerializeField]
        private float _bonusAttackCooldown;
        [SerializeField]
        private float _bonusAttackRange;

        [Header("Особые настройки")]
        [SerializeField, Min(1)]
        private int _attacksCount = 1;
        [SerializeField]
        private bool _isSplash;
        [SerializeField]
        private float _splashRadius;

        public event InAction<float> OnAttackRangeChanged;

        public float GetSummaryDamage => _damage + _bonusDamage;
        public float GetSummaryCooldown => _attackCooldown - _bonusAttackCooldown;
        public float GetSummaryRange => _attackRange + _bonusAttackRange;

        public int AttacksCount { get => _attacksCount; set => _attacksCount = value; }
        public bool IsSplash { get => _isSplash; set => _isSplash = value; }
        public float SplashRadius { get => _splashRadius; set => _splashRadius = value; }

        public DamageType GetDamageType => _damageType;
        public float GetDamage => _damage;
        public float GetAttackCooldown => _attackCooldown;
        public float GetAttackRange => _attackRange;

        public float GetBonusDamage => _bonusDamage;
        public float GetBonusAttackCooldown => _bonusAttackCooldown;
        public float GetBonusAttackRange => _bonusAttackRange;

        public void ChangeType(DamageType newType)
        {
            _damageType = newType;
        }

        public void ChangeDamage(float value)
        {
            _damage += value;
        }

        public void ChangeCooldown(float value)
        {
            _attackCooldown = Mathf.Clamp(_attackCooldown + value, Consts.MinAttackCooldown, Mathf.Infinity);
        }

        public void ChangeRange(float value)
        {
            _attackRange = Mathf.Clamp(_attackRange + value, Consts.MinAttackRange, Mathf.Infinity);

            OnAttackRangeChanged?.Invoke(GetSummaryRange);
        }

        public void ChangeBonusDamage(float value)
        {
            _bonusDamage += value;
        }

        public void ChangeBonusCooldown(float value)
        {
            _bonusAttackCooldown = Mathf.Clamp(_bonusAttackCooldown + value, Consts.MinAttackCooldown, Mathf.Infinity);
        }

        public void ChangeBonusRange(float value)
        {
            _bonusAttackRange = Mathf.Clamp(_bonusAttackRange + value, Consts.MinAttackRange, Mathf.Infinity);

            OnAttackRangeChanged?.Invoke(GetSummaryRange);
        }
    }
}