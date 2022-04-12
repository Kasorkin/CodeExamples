using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BaseGameLogic.Managers;

namespace GameAbilitySystem.CooldownSystem
{
    public sealed class ClassicCooldown : Cooldown
    {
        private readonly List<ClassicCooldownData> _cooldownLevelsData;

        public ClassicCooldown(List<ClassicCooldownData> cooldownLevelsData) : base(cooldownLevelsData.Count)
        {
            _cooldownLevelsData = cooldownLevelsData;

            Upgrade();
        }

        public sealed override bool IsReady() => _cooldownCoroutine == null;

        public sealed override void StartCooldown()
        {
            if (_cooldownCoroutine != null)
                return;

            GameManager.Singleton.CoroutineManager.StartCoroutine(Cooldowning(), ref _cooldownCoroutine);
        }

        public sealed override void Upgrade()
        {
            if (!TryLevelUp())
                return;

            _cooldownDuration = _cooldownLevelsData[CurrentLevel - 1].cooldownDuration;
        }

        protected sealed override IEnumerator Cooldowning()
        {
            yield return new WaitForSecondsRealtime(_cooldownDuration);
            GameManager.Singleton.CoroutineManager.StopCoroutine(ref _cooldownCoroutine);
        }
    }
}