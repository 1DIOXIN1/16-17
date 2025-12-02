using UnityEngine;
using UnityEngine.AI;

public class RunToNavMeshAgentLostTarget : ILostTarget
{
    private readonly NavMeshAgent _agent;
    private Vector3 _lastSeenPosition;
    private bool _destinationSet = false;

    public RunToNavMeshAgentLostTarget(NavMeshAgent agent)
    {
        _agent = agent;
    }

    public bool IsFinished { get; private set; } = false;

    public void SetLastSeenPosition(Vector3 position)
    {
        _lastSeenPosition = position;
        _destinationSet = false;
    }

    public void Execute(float deltaTime)
    {
        if (!_destinationSet)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_lastSeenPosition);
            _destinationSet = true;
            IsFinished = false;
        }

        if (!_agent.pathPending)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance &&
                (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f))
            {
                IsFinished = true;
            }
        }
    }

    public void End()
    {
        _destinationSet = false;
        _agent.isStopped = true;
        _agent.ResetPath();
        IsFinished = false;
    }
}

