using System.Collections;
using UnityEngine;

using BaseGameLogic.Managers;
using BaseGameLogic;

namespace GameAbilitySystem.EffectSystem
{
    //TODO : исправить мат ошибки при работе со скоростью
    public class Slow : SimpleEffect, ISimpleEffect
    {
        private readonly SlowData _data;

        private UnitData _targetData;
        private float _slowValue;

        public Slow(EffectData effectData, SlowData data) : base(effectData, data.levelsData.Count)
        {
            _data = data;
        }

        public void Init(Transform owner)
        {
            _owner = owner;
        }

        public void Start(Transform target)
        {
            _targetData = target.GetComponent<UnitData>();
            CalculateValue();
            _targetData.MoveStats.ChangeCurrentSpeed(-_slowValue);

            GameManager.Singleton.CoroutineManager.StartCoroutine(Duration());
        }

        public void Stop()
        {
            _targetData.MoveStats.ChangeCurrentSpeed(+_slowValue);
            GameManager.Singleton.CoroutineManager.StopCoroutine(Duration());
        }

        public void Upgrade()
        {
            if (!TryLevelUp())
                return;
        }

        protected override IEnumerator Duration()
        {
            yield return new WaitForSecondsRealtime(_data.levelsData[CurrentLevel - 1].duration);
            Stop();
        }

        private void CalculateValue()
        {
            _slowValue = _targetData.MoveStats.MaxCurrentSpeed * _data.levelsData[CurrentLevel - 1].percent / 100;
        }
    }
}