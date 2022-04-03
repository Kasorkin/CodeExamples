using AI;
using BaseGameLogic;

namespace StrategicManagement
{
    public class AttackCommand : IStrategicCommand
    {
        private readonly UnitData _target;
        private readonly AttackModule _attackController;

        public AttackCommand(in UnitData target, in UnitController controller)
        {
            _target = target;
            _attackController = controller.AttackModule;
        }

        public void Start()
        {
            _attackController.SetTarget(_target);
        }

        public void Stop()
        {
            if(_attackController.Target)
                _attackController.CancelingAttack();
            else
                _attackController.StopAttack();
        }

        public bool IsEnd()
        {
            return _attackController.Target == null;
        }
    }
}