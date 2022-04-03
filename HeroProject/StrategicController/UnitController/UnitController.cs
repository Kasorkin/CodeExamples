//#define Debug

using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

using AI;
using BaseGameLogic;

namespace StrategicManagement
{
    [DisallowMultipleComponent, RequireComponent(typeof(Movement), typeof(UnitData), typeof(AttackModule))]
    public sealed class UnitController : MonoBehaviour
    {
        public event Action CommandEnded;

        private readonly List<IStrategicCommand> _commands = new List<IStrategicCommand>();
        private IStrategicCommand _currentCommand;

        private Coroutine _handlerCoroutine;
        private Coroutine _shiftHandlerCoroutine;

        public AttackModule AttackModule { get; private set; }
        public UnitData UnitData { get; private set; }
        public Movement Movement { get; private set; }
        public Vision Vision { get; private set; }

        public bool IsMissingCommand()
        {
#if Debug
            Debug.Log(gameObject.name);
            Debug.Log(_currentCommand == null);
            if(_currentCommand != null)
                Debug.Log(_currentCommand.GetType());
            //Debug.Log(_shiftHandlerCoroutine == null);
#endif
            return _currentCommand == null && _shiftHandlerCoroutine == null;
        }

        public void StopAllCommands()
        {
#if Debug
            Debug.LogWarning(gameObject.name + " Принудительная остановка ВСЕХ команд");
#endif
            StopShiftCommandHandler();
            StopCurrentCommand();
            Movement.UnsetHoldPosition();
        }

        private void StopCurrentCommand()
        {
#if Debug
            Debug.LogWarning(gameObject.name + " Принудительная остановка текущей команды");
#endif
            StopCommandHandler();
            _currentCommand?.Stop();
            _currentCommand = null;
        }

        public void HoldCommand()
        {
            StopAllCommands();
            _currentCommand = new HoldCommand(this);
            _currentCommand.Start();
            StartCommandHandler();
        }

        public void MoveCommand(in Vector2 pos)
        {
            StopCurrentCommand();
#if Debug
            Debug.Log(gameObject.name + " приступил к созданию команды движения");
#endif
            _currentCommand = new MoveCommand(pos, this);
            _currentCommand.Start();
            StartCommandHandler();
        }

        public void MoveCommand(in Transform target, in float distance)
        {
            MoveCommand(Movement.CalculateMovePos(target, distance));
        }

        public void AttackCommand(in UnitData target)
        {
            StopCurrentCommand();
            _currentCommand = new AttackCommand(target, this);
            _currentCommand.Start();
            StartCommandHandler();
        }

        public void AttackOnPosCommand(in Vector2 attackPos)
        {
            StopCurrentCommand();
            _currentCommand = new AttackOnPosCommand(attackPos, this);
            _currentCommand.Start();
            StartCommandHandler();
        }

        private void StartCommandHandler()
        {
            if (_handlerCoroutine != null)
                return;
#if Debug
            Debug.Log(gameObject.name + " Запущен commandHandler");
#endif
            _handlerCoroutine = StartCoroutine(CommandHandler());
        }

        private IEnumerator CommandHandler()
        {
#if Debug
            Debug.Log(gameObject.name + " Жду запуска команды");
#endif
            yield return new WaitUntil(() => _currentCommand != null);
#if Debug
            Debug.Log(gameObject.name + " " + _currentCommand.GetType() + " Жду начала текущей команды");
#endif
            yield return new WaitUntil(() => !_currentCommand.IsEnd());
#if Debug
            Debug.Log(gameObject.name + " " + _currentCommand.GetType() + " Жду завершения текущей команды");
#endif
            yield return new WaitUntil(() =>  _currentCommand.IsEnd());
#if Debug
            Debug.Log(gameObject.name + " " + _currentCommand.GetType() + " Завершена текущая команда");
#endif

            _currentCommand?.Stop();
            _currentCommand = null;

            CommandEnded?.Invoke();

#if Debug
            Debug.LogWarning(gameObject.name + " commandHandler сам закончился");
#endif
            StopCommandHandler();
        }

        private void StopCommandHandler()
        {
            if (_handlerCoroutine == null)
                return;

            StopCoroutine(_handlerCoroutine);
            _handlerCoroutine = null;
        }

#region ShiftCommand
        public void ShiftMoveCommand(in Vector2 pos)
        {
#if Debug
            Debug.Log("Команда шифт получена текущее количество команд = " + _commands.Count);
#endif
            IStrategicCommand command = new MoveCommand(pos, this);
            ShiftHandler(command);
        }

        public void ShiftMoveCommand(in Transform target, in float distance)
        { 
            ShiftMoveCommand(Movement.CalculateMovePos(target, distance));
        }

        public void ShiftAttackCommand(in UnitData target)
        {
            IStrategicCommand command = new AttackCommand(target, this);
            ShiftHandler(command);
        }

        private void ShiftHandler(in IStrategicCommand newCommand)
        {
            if (_currentCommand == null && _shiftHandlerCoroutine == null)
            {
                _currentCommand = newCommand;
                _currentCommand.Start();
            }
            else
            {
#if Debug
                Debug.Log("Добавлено в список ожидания");
#endif
                _commands.Insert(0, newCommand);

                if (_shiftHandlerCoroutine == null)
                    _shiftHandlerCoroutine = StartCoroutine(ShiftCommandHandler());
            }
        }

        private IEnumerator ShiftCommandHandler()
        {
            Debug.Log("Обработчик запущен" + _commands.Count);
            while (_commands.Count > 0)
            {
                yield return new WaitUntil(() => _currentCommand.IsEnd());
                _currentCommand.Stop();
                Debug.Log("Взята новая команда");
                _currentCommand = _commands[_commands.Count - 1];
                _commands.RemoveAt(_commands.Count - 1);
                _currentCommand.Start();
                //TODO : Возможно стоит сделать другой метод .IsStarted();
                yield return new WaitUntil(() => !_currentCommand.IsEnd());
            }

            _shiftHandlerCoroutine = null;
            Debug.Log("Корутина выключена");
        }

        private void StopShiftCommandHandler()
        {
            if (_shiftHandlerCoroutine == null)
                return;
            _commands.Clear();
            StopCoroutine(_shiftHandlerCoroutine);
            _shiftHandlerCoroutine = null;
#if Debug
            Debug.Log("Корутина выключена принудительно");
#endif
        }
#endregion

        private void Start()
        {
            Movement = GetComponent<Movement>();
            UnitData = GetComponent<UnitData>();
            AttackModule = GetComponent<AttackModule>();

            InitVision();

            UnitData.Health.Death.OnDied += StopAllCommands;
        }

        private void InitVision()
        {
            GameObject visionOwner = new GameObject();
            visionOwner.transform.SetParent(gameObject.transform);
            visionOwner.transform.localPosition = Vector3.zero;

            Vision = visionOwner.AddComponent<Vision>();
            Vision.Init(this);
        }

        private void OnDestroy()
        {
            UnitData.Health.Death.OnDied -= StopAllCommands;
        }
    }
}