using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    [DisallowMultipleComponent]
    public sealed class Movement : MonoBehaviour
    {
        private NavMeshAgent _agent;

        public bool IsHoldPosition { get; private set; } = false;
        
        public bool IsMoving => _agent.hasPath;
        public bool IsEndPath => _agent.hasPath == false;

        public void SetHoldPosition()
        {
            IsHoldPosition = true;
        }

        public void UnsetHoldPosition()
        {
            IsHoldPosition = false;
        }

        public void StopMoving()
        {
            _agent.ResetPath();
        }

        public void SetStoppingDistance(in float distance)
        {
            _agent.stoppingDistance = distance;
        }

        public void Move(in Vector2 pos)
        {
            _agent.SetDestination(pos);
        }

        public void Move(in Transform target, in float distance)
        {
            Move(CalculateMovePos(target, distance));
        }

        public Vector2 CalculateMovePos(in Transform target, in float distance)
        {
            return transform.position + (target.position - transform.position) * (1 - distance / Vector2.Distance(target.position, transform.position));
        }

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }
    }
}