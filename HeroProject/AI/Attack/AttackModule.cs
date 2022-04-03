using System;
using System.Collections;
using UnityEngine;

using BaseGameLogic;

namespace AI
{
    //TODO : Разделить periodicAttack корутину
    [DisallowMultipleComponent]
    public sealed class AttackModule : MonoBehaviour
    {
        private const float _timeForPreparation = 0.05f;

        public event InAction<UnitData> OnAttackCencelling;
        public event Action OnAttackEnded;

        private Coroutine _cooldownCoroutine;
        private Coroutine _attackCoroutine;
        private Coroutine _followCoroutine;
        private UnitData _owner;
        private Movement _movement;

        public UnitData Target { get; private set; }

        private float AttackRange => _owner.WeaponStats.GetSummaryRange;

        public void SetTarget(in UnitData target)
        {
            if (target == null)
                return;

            Debug.Log(gameObject.name + " цель установлена");
            Target = target;

            if (_attackCoroutine == null)
            {
                _attackCoroutine = StartCoroutine(PeriodicAttack());
            }
        }

        //
        public void CancelingAttack()
        {
            OnAttackCencelling?.Invoke(Target);
            StopAttack();
        }

        public void StopAttack()
        {
            ClearTarget();

            StopAttackCoroutine();
            StopFollowCoroutine();
        }

        public void ClearTarget()
        {
            Target = null;
        }

        private bool CanAttack()
        {
            return Vector2.Distance(Target.transform.position, transform.position) <= AttackRange;
        }

        private IEnumerator FollowingTarget()
        {
            _movement.Move(Target.transform, AttackRange - 0.05f);
            yield return new WaitUntil(() => !_movement.IsEndPath);
            //Debug.Log("Дождался начала");
            yield return new WaitUntil(() => _movement.IsEndPath);
            //Debug.LogWarning("Добрался до цели");

            _followCoroutine = null;
        }

        private IEnumerator PeriodicAttack()
        {
            yield return new WaitForSeconds(_timeForPreparation);

            while(Target)
            {
                if (CanAttack() == false)
                {
                    //Debug.LogWarning("Двигаюсь к цели");
                    if (_movement.IsHoldPosition == false)
                    {
                        yield return _followCoroutine = StartCoroutine(FollowingTarget());
                        //Debug.LogWarning("Закончил дигаться к цели");
                    }
                    else
                    {
                        yield return null;
                    }
                    continue;
                }

                if (_cooldownCoroutine != null)
                {
                    //Debug.LogWarning("Перезарядка продолжается");
                    yield return null;
                    continue;
                }

                //Debug.Log("Атака модуля");
                _owner.Weapon.Attack(Target);
                for (int i = 1; i < _owner.WeaponStats.AttacksCount; ++i)
                {
                    yield return new WaitForSeconds(_timeForPreparation);
                    Debug.Log("Доп. Атака модуля");
                    _owner.Weapon.Attack(Target);
                }

                _cooldownCoroutine = StartCoroutine(CooldownOfAttack());
            }

            Debug.LogWarning(gameObject.name + " завершил атаку");
            _attackCoroutine = null;
            OnAttackEnded?.Invoke();
        }

        private IEnumerator CooldownOfAttack()
        {
            yield return new WaitForSeconds(_owner.WeaponStats.GetSummaryCooldown);
            _cooldownCoroutine = null;
        }

        private void StopCooldownCoroutine()
        {
            StopCoroutine(CooldownOfAttack());
            _cooldownCoroutine = null;
        }

        private void StopAttackCoroutine()
        {
            if (_attackCoroutine == null)
                return;

            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }

        private void StopFollowCoroutine()
        {
            if (_followCoroutine == null)
                return;

            StopCoroutine(_followCoroutine);
            _followCoroutine = null;
        }

        private void StopCoroutines()
        {
            Debug.LogWarning(gameObject.name + " Остановка модуля атаки");
            StopAttackCoroutine();
            StopFollowCoroutine();
            StopCooldownCoroutine();
        }

        private void Start()
        {
            _owner = GetComponent<UnitData>();
            _movement = GetComponent<Movement>();

            _owner.Health.Death.OnDied += StopCoroutines;
        }

        private void OnDestroy()
        {
            _owner.Health.Death.OnDied -= StopCoroutines;
        }
    }
}