using UnityEngine;
using UnityEngine.AI;

public class LostTargetReaction : IBehavior
{
    private readonly Transform _characterTransform;
    private readonly NavMeshAgent _agent;

    private Vector3 _targetPoint;
    private bool _hasPoint = false;

    public LostTargetReaction(Transform characterTransform, NavMeshAgent agent)
    {
        _characterTransform = characterTransform;
        _agent = agent;
    }

    public NavMeshAgent Agent => _agent;

    public void SetTargetPoint(Vector3 point)
    {
        _targetPoint = point;
        _hasPoint = true;

        if (_agent != null)
            _agent.SetDestination(_targetPoint);
    }

    public void Execute()
    {
        if (_hasPoint == false)
            return;

        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
            _hasPoint = false;
    }

    //
    public bool IsPointReached() => !_hasPoint;
}
