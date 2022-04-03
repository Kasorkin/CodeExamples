using AI;

namespace StrategicManagement
{
    public class HoldCommand : IStrategicCommand
    {
        private readonly Movement _movement;

        public HoldCommand(UnitController controller)
        {
            _movement = controller.Movement;
        }

        public bool IsEnd()
        {
            return false;
        }

        public void Start()
        {
            _movement.SetHoldPosition();
        }

        public void Stop()
        {
            _movement.UnsetHoldPosition();
        }
    }
}