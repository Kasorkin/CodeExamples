using System.Collections;
using UnityEngine;
using System.Collections.Generic;

using BaseGameLogic;
using BaseGameLogic.Managers;
using BaseGameLogic.DamageSystem;
using Hero;
using StrategicManagement.InputSystem;
using GameAbilitySystem.DummySystem;
using GameAbilitySystem.CooldownSystem;
using GameAbilitySystem.CostSystem;
using GameAbilitySystem.EffectSystem;

namespace GameAbilitySystem
{
    public class IceWave : Ability, IActiveAbility
    {
        private readonly IceWaveData _iceWaveData;
        private readonly Cooldown _cooldownData;
        private readonly Cost _costData;
        private readonly List<GameEffect> _effects = new List<GameEffect>();

        private HeroStats _hero;
        private UnitData _unit;

        private Coroutine _usingCoroutine;
        private Vector2 _targetPos;

        public IceWave(AbilityData abilityData, IceWaveData iceWaveData) : base(abilityData, iceWaveData.levelsData.Count)
        {
            _iceWaveData = iceWaveData;
            _cooldownData = iceWaveData.cooldownSO.CreateCooldown();
            _costData = iceWaveData.costSO.CreateCost(_owner);

            foreach (var k in iceWaveData.effects)
            {
                _effects.Add(new GameEffect(k));
            }
        }

        public void Init(Transform owner)
        {
            _owner = owner;

            _hero = _owner.GetComponent<HeroStats>();
            _unit = _hero.UnitData;
        }

        public void Upgrade()
        {
            if (!TryLevelUp())
                return;

            Debug.Log("Ледяная волна, уровень повышен");
            _cooldownData.Upgrade();
            _costData.Upgrade();

            foreach (var k in _effects)
                k.Upgrade();
        }

        public void Use()
        {
            if (_costData.CanUse() == false || _cooldownData.IsReady() == false)
                return;

            if (_usingCoroutine != null)
                return;

            GameManager.Singleton.CoroutineManager.StartCoroutine(UsingWaiter(), ref _usingCoroutine);
        }

        private IEnumerator UsingWaiter()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => Input.anyKeyDown);

            if (InputHandler.IsLeftMouseButtonDown())
                Using();

            StopUsing();
        }

        private void Using()
        {
            if (_costData.TryUse() == false)
                return;

            _targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Dummy waveDummy = new GameObject().AddComponent<Dummy>();
            waveDummy.CreateBoxCollider(_iceWaveData.width, _iceWaveData.length);

            Damage waveDamage = new Damage(_unit, 0f, CalculateMagicDamage());
            waveDummy.Damage = waveDamage;
            waveDummy.Move(_targetPos, _iceWaveData.waveSpeed, _iceWaveData.distance, true);
        }

        private void StopUsing()
        {
            GameManager.Singleton.CoroutineManager.StopCoroutine(ref _usingCoroutine);
        }

        private float CalculateMagicDamage()
        {
            return _hero.GetSummaryStrength * _hero.GetSummaryIntelligence + _unit.WeaponStats.GetSummaryDamage * 2;
        }
    }
}