using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BaseGameLogic;
using BaseGameLogic.Managers;
using BaseGameLogic.DamageSystem;

namespace GameAbilitySystem.EffectSystem
{
    public sealed class DamageEffect : SimpleEffect, IPeriodicSimpleEffect
    {
        private readonly DamageEffectData _data;

        private float _currentDuration;
        private float _currentPeriodic;

        private UnitData _ownerUnit;
        private DamageHandler _damageHandler;

        public DamageEffect(DamageEffectData data, EffectData effectData) : base(effectData, data.levelsData.Count)
        {
            _data = data;
        }

        public void Init(Transform owner)
        {
            _owner = owner;
            _ownerUnit = _owner.GetComponent<UnitData>();
        }

        public void Start(Transform target)
        {
            _damageHandler = target.GetComponent<DamageHandler>();

            _currentDuration = _data.levelsData[CurrentLevel - 1].duration;
            _currentPeriodic = _data.levelsData[CurrentLevel - 1].periodic;
            GameManager.Singleton.CoroutineManager.StartCoroutine(Duration());
        }

        public void Step()
        {
            Damage damage = new Damage(_ownerUnit, _data.levelsData[CurrentLevel - 1].damage);
            _damageHandler.Handler(damage);
        }

        public void Stop()
        {
            GameManager.Singleton.CoroutineManager.StopCoroutine(Duration());
        }

        public void Upgrade()
        {
            if (!TryLevelUp())
                return;
        }

        protected sealed override IEnumerator Duration()
        {
            while(_currentDuration >= _currentPeriodic)
            {
                yield return new WaitForSecondsRealtime(_currentPeriodic);
                Step();
                _currentDuration -= _currentPeriodic;
            }

            Stop();
        }
    }
}