using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BaseGameLogic;
using BaseGameLogic.Player;

namespace StrategicManagement
{
    //TODO : Может стоит вынести нижние команды в отдельный CommandController?
    [DisallowMultipleComponent]
    public sealed class StrategicController : MonoBehaviour
    {
        private readonly Vector2 _clickSize = new Vector2(0.1f, 0.1f);
        private readonly Color _drawScreenColor = new Color(0.8f, 0.8f, 0.95f, 0.25f);
        private readonly Color _drawScreenBorderColor = new Color(0.8f, 0.8f, 0.95f);

        private readonly StrategicSelector _selector = new StrategicSelector();
        private readonly StrategicDrawer _drawer = new StrategicDrawer();

        private Coroutine _attackOnPosCoroutine;
        private Vector2 _endPos;
        private Vector2 _size;
        private Vector2 _rightClickPos;
        private Vector2 _startInputPos;
        private bool _isSelecting = false;
        private bool _isShiftButton = false;
        
        private List<UnitController> _selectedObjects = new List<UnitController>();

        //TODO: Костыль фракции, сюда игрок должен подгружаться
        public PlayerData Owner { get; set; } = new PlayerData("Player 1", 55, Fraction.Light);

        private void Start()
        {
            _drawer.Init();
        }

        private void OnGUI()                        
        {
            if (_isSelecting)
            {
                var rect = _drawer.GetScreenRect(_startInputPos, Input.mousePosition);
                _drawer.DrawScreenRect(rect, _drawScreenColor);
                _drawer.DrawScreenRectBorder(rect, 2, _drawScreenBorderColor);
            }
        }

        private void Update()
        {
            Selecting();
            ShiftHolding();
            HoldButton();
            RightClicking();
            GoAndAttackEnemiesHandler();
            StopButton();
        }

        private void StopButton()
        {
            if (_selectedObjects.Count == 0)
                return;

            if(InputHandler.IsStopButton())
            {
                //Debug.LogWarning("Отправлена команда на остановку");
                StopCommand();
            }
        }

        private void GoAndAttackEnemiesHandler()
        {
            if (_selectedObjects.Count == 0)
                return;

            if (_attackOnPosCoroutine != null && Input.anyKeyDown && InputHandler.IsLeftMouseButtonDown() == false)
            {
                //Debug.LogWarning("Отмена команды на атаку позиции");
                StopAttackOnPosCoroutine();
            }

            if (InputHandler.IsAttackButtonDown())
                _attackOnPosCoroutine = StartCoroutine(SelectingAttackPos());
        }

        private IEnumerator SelectingAttackPos()
        {
            //добавить анимацию выбора точки атаки
            //Debug.Log("Ожидаю атаки");
            yield return new WaitUntil(() => InputHandler.IsLeftMouseButtonDown());
            //Debug.Log("Атака");
            Vector2 attackPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            AttackOnPos(attackPos);
            StopAttackOnPosCoroutine();
        }

        private void StopAttackOnPosCoroutine()
        {
            //Debug.LogWarning("Короутина ожидания указания атаки на позицию остановлена");
            StopCoroutine(_attackOnPosCoroutine);
            _attackOnPosCoroutine = null;
        }

        private void Selecting()
        {
            if (_attackOnPosCoroutine != null)
                return;

            if (InputHandler.IsLeftMouseButtonDown())
            {
                _isSelecting = true;
                _startInputPos = Input.mousePosition;
            }
            if (InputHandler.IsLeftMouseButtonUp() && _isSelecting)
            {
                _endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _isSelecting = false;
                Select();
            }
        }

        private void HoldButton()
        {
            if (_selectedObjects.Count == 0)
                return;

            if(InputHandler.IsHoldButtonDown())
            {
                HoldCommand();
            }
        }

        private void ShiftHolding()
        {
            if (InputHandler.IsShiftButtonDown())
            {
                _isShiftButton = true;
            }

            if (InputHandler.IsShiftButtonUp())
            {
                _isShiftButton = false;
            }
        }

        private void RightClicking()
        {
            if (InputHandler.IsRightMouseButtonDown())
            {
                _rightClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RightClickHandler();
            }
        }

        private void Select()
        {
            Vector2 startPos = Camera.main.ScreenToWorldPoint(_startInputPos);
            _size.x = Mathf.Abs(startPos.x - _endPos.x);
            _size.y = Mathf.Abs(startPos.y - _endPos.y);

            SelectedClear();
            _selectedObjects = _selector.FindUnitsInBoxForOwner((startPos + _endPos) / 2, _size, Owner);
        }

        private void SelectedClear()
        {
            //Debug.LogError("Очистка");
            _selectedObjects.Clear();
        }

        private void ClearCommands()
        {
            foreach (var k in _selectedObjects)
            {
                k.StopAllCommands();
            }
        }

        private void RightClickHandler()
        {
            if(_isShiftButton == false)
                ClearCommands();

            List<GameObject> objectsInClickZone = _selector.FindObjectsInBox(_rightClickPos, _clickSize);

            if(objectsInClickZone.Count == 0)
            {
                MoveCommand();
            }
            else
            {
                CommandSelector(objectsInClickZone);
            }
        }

        private void CommandSelector(in List<GameObject> objectsInClickZone)
        {
            List<UnitController> units = _selector.FindUnits(objectsInClickZone);
            if (units.Count == 0)
            {
                InteractionCommand(objectsInClickZone);
            }
            else
            {
                UnitCommand(units[0]);
            }
        }

        private void HoldCommand()
        {
            foreach(var k in _selectedObjects)
            {
                k.HoldCommand();
            }
        }

        private void UnitCommand(in UnitController target)
        {
            if(target.UnitData.Owner.Fraction != Owner.Fraction)
            {
                AttackCommand(target.UnitData);
            }
            else
            {
                MoveCommand();
            }
        }

        private void AttackCommand(in UnitData target)
        {
            if (_isShiftButton)
            {
                foreach (var k in _selectedObjects)
                {
                    k.ShiftAttackCommand(target);
                }
            }
            else
            {
                foreach (var k in _selectedObjects)
                {
                    Debug.Log(gameObject.name + " отдал команду" + k.name + " на атаку объекта " + target.name);
                    k.AttackCommand(target);
                }
            }
        }

        private void MoveCommand()
        {
            if (_isShiftButton)
            {
                foreach (var k in _selectedObjects)
                {
                    k.ShiftMoveCommand(_rightClickPos);
                }
            }
            else
            {
                foreach (var k in _selectedObjects)
                {
                    Debug.Log(gameObject.name + " отдал команду на движение");
                    k.MoveCommand(_rightClickPos);
                }
            }
        }

        private void StopCommand()
        {
            foreach (var k in _selectedObjects)
            {
                k.StopAllCommands();
            }
        }

        private void AttackOnPos(in Vector2 pos)
        {
            foreach (var k in _selectedObjects)
            {
                k.AttackOnPosCommand(pos);
            }
        }

        private void InteractionCommand(in List<GameObject> objectsInClickZone)
        {
            GameObject item = _selector.FindItemObject(objectsInClickZone);
            if (item != null)
            {
                PickUpCommand();
            }
            else
            {
                MoveCommand();
            }
        }

        private void PickUpCommand()
        {
            if(_isShiftButton)
            {

            }
            else
            {

            }
        }
    }
}