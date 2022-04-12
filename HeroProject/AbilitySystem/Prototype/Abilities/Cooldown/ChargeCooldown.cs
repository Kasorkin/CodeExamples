using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BaseGameLogic.Managers;

namespace GameAbilitySystem.CooldownSystem
{
    public sealed class ChargeCooldown : Cooldown
    {
        private readonly List<ChargeCooldownData> _chargeCooldownLevelsData;

        private int _maxCharges;
        private int _currentCharges = 0;

        public ChargeCooldown(List<ChargeCooldownData> chargeCooldownLevelsData) : base(chargeCooldownLevelsData.Count)
        {
            _chargeCooldownLevelsData = chargeCooldownLevelsData;

            Upgrade();
        }

        public sealed override bool IsReady() => _currentCharges > 0;

        public sealed override void StartCooldown()
        {
            _currentCharges--;

            StartCooldownCoroutine();
        }

        private void StartCooldownCoroutine()
        {
            if (_cooldownCoroutine != null || _currentCharges == _maxCharges)
                return;

            GameManager.Singleton.CoroutineManager.StartCoroutine(Cooldowning(), ref _cooldownCoroutine);
        }

        protected sealed override IEnumerator Cooldowning()
        {
            while(_maxCharges != _currentCharges)
            {
                yield return new WaitForSecondsRealtime(_cooldownDuration);
                _currentCharges++;
            }
            GameManager.Singleton.CoroutineManager.StopCoroutine(ref _cooldownCoroutine);
        }

        public sealed override void Upgrade()
        {
            if (!TryLevelUp())
                return;

            ChargeCooldownData data = _chargeCooldownLevelsData[CurrentLevel - 1];

            _cooldownDuration = data.cooldownChargeDuration;
            _maxCharges = data.maxCharges;

            StartCooldownCoroutine();
        }
    }
}