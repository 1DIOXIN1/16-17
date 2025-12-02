using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolIdleNavMesh : IBehavior
{
    private const float MIN_DISTANCE_TO_TARGET = 0.2f;

    private readonly NavMeshAgent _agent;
    private readonly Queue<Vector3> _waypoints;
    private Vector3 _currentPoint;

    public PatrolIdleNavMesh(NavMeshAgent agent, List<Transform> points)
    {
        _agent = agent;
        _waypoints = new Queue<Vector3>();

        foreach (var point in points)
            _waypoints.Enqueue(point.position);

        SwitchPoint();
    }

    public void Execute(float deltaTime)
    {
        if (!_agent.pathPending)
        {
            if (_agent.remainingDistance <= MIN_DISTANCE_TO_TARGET)
            {
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                {
                    SwitchPoint();
                }
            }
        }
    }

    private void SwitchPoint()
    {
        _currentPoint = _waypoints.Dequeue();
        _waypoints.Enqueue(_currentPoint);

        _agent.isStopped = false;
        _agent.SetDestination(_currentPoint);
    }
}
