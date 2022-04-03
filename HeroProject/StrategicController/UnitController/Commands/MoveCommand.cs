using AI;
using UnityEngine;

namespace StrategicManagement
{
    public class MoveCommand : IStrategicCommand
    {
        private readonly Movement _movement;
        private Vector2 _pos;

        public MoveCommand(in Vector2 pos, in UnitController controller)
        {
            _pos = pos;
            _movement = controller.Movement;
        }

        public void Start()
        {
            _movement.Move(_pos);
        }

        public void Stop()
        {
            _movement.StopMoving();
        }

        public bool IsEnd()
        {
            return _movement.IsEndPath;
        }
    }
}