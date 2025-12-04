using UnityEngine;
using UnityEngine.AI;

public class RunToNavMeshAgentReaction : IBehavior
{
    private float _moveSpeed = 1;
    private float _rotationSpeed = 1000;

    private readonly NavMeshAgent _agent;
    private Transform _targetTransform;
    private Transform _characterTransform;
    private Mover _mover;

    public RunToNavMeshAgentReaction(NavMeshAgent agent, Transform characterTransform, Transform targetTransform)
    {
        _agent = agent;
        _targetTransform = targetTransform;
        _characterTransform = characterTransform;

        _mover = new Mover(_characterTransform, _moveSpeed, _rotationSpeed);

        _agent.speed = _moveSpeed;
        _agent.angularSpeed = 999;
    }

    public void Execute(float deltaTime)
    {
        _agent.SetDestination(_targetTransform.position);
        _mover.ProcessRotateTo(GetDirectionToTarget(), deltaTime);
    }

    private Vector3 GetDirectionToTarget() => _targetTransform.position - _characterTransform.position;
}
