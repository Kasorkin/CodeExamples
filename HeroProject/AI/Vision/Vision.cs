using System;
using System.Collections.Generic;
using UnityEngine;

using BaseGameLogic;
using StrategicManagement;

namespace AI
{
    //TODO : Здесь нужно убрать юнит дату и сделать через юнит контроллер или другой скрипт "только для стратегического управления"
    [DisallowMultipleComponent]
    public sealed class Vision : MonoBehaviour
    {
        public event Action<UnitData> EnemyAddedInQueue;

        private UnitController _unitController;

        private float _visionRange;
        private CircleCollider2D _agressionVisionCollider;

        //[SerializeField]
        //private EnemiesVision _enemiesVision = new EnemiesVision();

        [SerializeField]
        private List<UnitData> _enemiesQuque = new List<UnitData>();

        private Fraction OwnerFraction => _unitController.UnitData.Owner.Fraction;

        public void Init(UnitController controller)
        {
            _unitController = controller;

            _agressionVisionCollider = gameObject.AddComponent<CircleCollider2D>();
            _agressionVisionCollider.isTrigger = true;
            _agressionVisionCollider.radius = Consts.AgressionRange;

            gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }

        public void SetVision(in float range)
        {
            if (range == _visionRange)
                return;

            _visionRange = range;
        }

        public void SetTarget(in UnitData unit)
        {
            if (unit == null)
                return;

            if (_unitController.IsMissingCommand())
                _unitController.AttackCommand(unit);
            else if (_unitController.Movement.IsHoldPosition)
                _unitController.AttackModule.SetTarget(unit);
            else
                //AddEnemy(unit);
                AddEnemyInQueue(unit);

        }

        public void RemoveEnemyOfQuque(UnitData unitData)
        {
            _enemiesQuque.Remove(unitData);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out UnitData unit) && unit.Owner.Fraction != OwnerFraction)
            {
                Debug.LogWarning(_unitController.gameObject.name + " onTriggerEnter");
                if (_unitController.AttackModule.Target == null)
                {
                    //Debug.LogWarning(_unitController.gameObject.name + " setTarget");
                    SetTarget(unit);
                }
                else if (_unitController.AttackModule.Target != unit)
                {
                    //Debug.LogWarning(_unitController.gameObject.name + " addEnemyInList");
                    //AddEnemy(unit);
                    AddEnemyInQueue(unit);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out UnitData unit))
            {
                if (_unitController.AttackModule.Target == unit)
                {
                    //Debug.Log("ты пидор");
                    _unitController.AttackModule.ClearTarget();//ClearTarget();

                    UpdateTarget();
                }
                else
                {
                    RemoveEnemyOfQuque(unit);
                }
            }
        }

        private void UpdateTarget()
        {
            Debug.LogWarning(_unitController.gameObject.name + " Вызван апдейт цели");
            if (_enemiesQuque.Count == 0)
                return;

            int index = SearchIndexOfNearestTarget();

            UnitData target = _enemiesQuque[index];
            _enemiesQuque.RemoveAt(index);


            //_unitController.AttackCommand(target);
            if (_unitController.IsMissingCommand())
                _unitController.AttackCommand(target);
            else //if (_unitController.Movement.IsHoldPosition)
                _unitController.AttackModule.SetTarget(target);

            Debug.Log(_unitController.gameObject.name + " Команда на атаку передана");
        }

        private void AddEnemyInQueue(in UnitData unitData)
        {
            _enemiesQuque.Add(unitData);

            EnemyAddedInQueue?.Invoke(unitData);
        }

        private void ClearEnemies()
        {
            _enemiesQuque.Clear();
        }

        /*private void SearchTargets()
        {
            RaycastHit2D[] physics = Physics2D.CircleCastAll(transform.position, Consts.AgressionRange, Vector2.zero);

            foreach(var k in physics)
            {
                if(k.transform.TryGetComponent(out UnitData unit) && unit.Owner.Fraction != OwnerFraction)
                {
                    if (_enemies.Find(item => item == unit))
                        continue;
                    _enemies.Add(unit);
                }
            }
        }*/

        private int SearchIndexOfNearestTarget()
        {
            int index = 0;
            float minRange = Mathf.Infinity;
            for(int i = 0; i < _enemiesQuque.Count; ++i)
            {
                float newRange = Vector2.Distance(transform.position, _enemiesQuque[i].transform.position);
                if (newRange < minRange)
                {
                    index = i;
                    minRange = newRange;
                }
            }

            return index;
        }

        private void Start()
        {
            SetVision(_unitController.UnitData.VisionRange);

            _unitController.CommandEnded += UpdateTarget;

            _unitController.AttackModule.OnAttackEnded += UpdateTarget;
            _unitController.AttackModule.OnAttackCencelling += SetTarget;
            //_enemiesVision.Init();
            _unitController.UnitData.Health.Death.OnDied += ClearEnemies;
        }

        private void OnDestroy()
        {
            _unitController.CommandEnded -= UpdateTarget;

            _unitController.AttackModule.OnAttackEnded -= UpdateTarget;
            _unitController.AttackModule.OnAttackCencelling -= SetTarget;
            //_enemiesVision.Destroy();
            _unitController.UnitData.Health.Death.OnDied -= ClearEnemies;
        }
    }
}