using UnityEngine;
using System.Collections;

using AI;
using BaseGameLogic;
using BaseGameLogic.Managers;

namespace StrategicManagement
{
    public sealed class AttackOnPosCommand : IStrategicCommand
    {
        private readonly Vector2 _attackPos;

        private readonly Movement _movement;
        private readonly AttackModule _attackModule;
        private readonly Vision _vision;

        private Coroutine _commandCoroutine;

        public AttackOnPosCommand(Vector2 pos, in UnitController unitController)
        {
            _attackPos = pos;
            _movement = unitController.Movement;
            _attackModule = unitController.AttackModule;
            _vision = unitController.Vision;
        }

        bool IStrategicCommand.IsEnd()
        {
            return _commandCoroutine == null;
        }

        void IStrategicCommand.Start()
        {
            GameManager.Singleton.CoroutineManager.StartCoroutine(CommandCoroutine(), ref _commandCoroutine);
            _vision.EnemyAddedInQueue += AddTarget;
        }

        void IStrategicCommand.Stop()
        {
            GameManager.Singleton.CoroutineManager.StopCoroutine(ref _commandCoroutine);
            _movement.StopMoving();
            _attackModule.CancelingAttack();
        }

        private IEnumerator CommandCoroutine()
        {
            while(true)
            {
                if(_attackModule.Target == null)
                {
                    if(_movement.IsMoving)
                    {
                        yield return null;
                    }
                    else
                    {
                        if (Vector2.Distance(_movement.transform.position, _attackPos) < 2f)
                        {
                            //Debug.LogWarning("Атака на позицию - корутина - сама завершилась");
                            _commandCoroutine = null;
                            yield break;
                        }

                            _movement.Move(_attackPos);
                    }
                }
                yield return null;
            }
        }

        private void AddTarget(UnitData target)
        {
            if(_attackModule.Target == null)
            {
                _attackModule.SetTarget(target);
                _vision.RemoveEnemyOfQuque(target);
            }
        }
    }
}