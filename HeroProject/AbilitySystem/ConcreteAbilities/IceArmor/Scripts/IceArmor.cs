using System.Collections;
using UnityEngine;

using BaseGameLogic;
using BaseGameLogic.DamageSystem;
using BaseGameLogic.Managers;
using GameAbilitySystem.CooldownSystem;

namespace GameAbilitySystem
{
    public class IceArmor : Ability, IPassiveAbility
    {
        private readonly IceArmorData _iceArmorData;
        private readonly Cooldown _cooldownData;

        private UnitData _unitData;
        private float _totalAbsrobedValue;
        private float _currentAbsorbedValue;
        private float _currentDuration;

        public IceArmor(AbilityData abilityData, IceArmorData iceArmorData) : base (abilityData, iceArmorData.levelsData.Count)
        {
            _iceArmorData = iceArmorData;
            _cooldownData = _iceArmorData.cooldown.CreateCooldown();
        }

        public void Init(Transform owner)
        {
            _owner = owner;
            _unitData = _owner.GetComponent<UnitData>();

            OnEnable();
        }

        public void OnEnable()
        {
            TotalAbsorbValueUpdate(_unitData.Health.MaxHealth);
            GameManager.Singleton.CoroutineManager.StartCoroutine(IceArmorCooldown());

            _unitData.Health.OnMaxHealthChanged += TotalAbsorbValueUpdate;
            _unitData.DamageHandler.OnDamageAdding += IceArmorDamageHandler;
        }

        public void OnDisable()
        {
            GameManager.Singleton.CoroutineManager.StopCoroutine(IceArmorCooldown());

            _unitData.Health.OnMaxHealthChanged -= TotalAbsorbValueUpdate;
            _unitData.DamageHandler.OnDamageAdding -= IceArmorDamageHandler;
        }

        public void Upgrade()
        {
            if (!TryLevelUp())
                return;

            Debug.Log("Ледяная броня, уровень повышен");
            _cooldownData.Upgrade();
            _currentDuration = _iceArmorData.levelsData[CurrentLevel - 1].duration;
        }

        private IEnumerator IceArmorCooldown()
        {
            while(true)
            {
                yield return new WaitUntil(() => _cooldownData.IsReady());   
                UpdateCurrentAbsorbValue();
                _cooldownData.StartCooldown();
                yield return new WaitForSecondsRealtime(_currentDuration);
            }
        }

        private void TotalAbsorbValueUpdate(in float maxHealth)
        {
            _totalAbsrobedValue = maxHealth * _iceArmorData.PercentHP / 100;
        }

        private void IceArmorDamageHandler(ref Damage damage)
        {
            if (_currentAbsorbedValue == 0)
                return;

            Handler(ref damage.PhysicalDamage);
            Handler(ref damage.MagicalDamage);
        }

        private void Handler(ref float damage)
        {
            if (_currentAbsorbedValue == 0)
                return;

            if (damage <= _currentAbsorbedValue)
            {
                _totalAbsrobedValue -= damage;
                damage = 0;
            }
            else
            {
                damage -= _totalAbsrobedValue;
                _totalAbsrobedValue = 0;
            }
        }

        private void UpdateCurrentAbsorbValue()
        {
            _currentAbsorbedValue = _totalAbsrobedValue;
        }
    }
}