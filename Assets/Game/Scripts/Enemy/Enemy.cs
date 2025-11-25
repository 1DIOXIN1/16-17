using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private const float MAX_AGGRO_DISTANCE = 3;

    private EnemyState _currentState = EnemyState.Idle;

    private IBehavior _idleBehavior;
    private IBehavior _reactionBehavior;
    private LostTargetReaction _lostTargetBehavior;
    private NavMeshAgent _agent;

    private DistanceDetector _distanceDetector;
    private Transform _targetTransform;

    private bool _hasEverSeenTarget = false;
    private Vector3 _lastSeenPoint;

    public void Initialization(IBehavior idleBehavior, IBehavior reactionBehavior, Transform targetTransform)
    {
        _idleBehavior = idleBehavior;
        _reactionBehavior = reactionBehavior;
        _targetTransform = targetTransform;

        _agent = GetComponent<NavMeshAgent>();

        _distanceDetector = new DistanceDetector(transform, targetTransform);
        _lostTargetBehavior = new LostTargetReaction(transform, _agent);
    }

    private void Update()
    {
        bool seesTarget = _distanceDetector.InZone(MAX_AGGRO_DISTANCE);

        switch (_currentState)
        {
            case EnemyState.Idle:
                HandleIdle(seesTarget);
                break;

            case EnemyState.Reaction:
                HandleReaction(seesTarget);
                break;

            case EnemyState.LostTarget:
                HandleLostTarget(seesTarget);
                break;

            default:
                HandleIdle(seesTarget);
                break;
        }
    }

    // ---------- STATE HANDLERS ----------

    private void HandleIdle(bool seesTarget)
    {
        if (seesTarget)
        {
            _hasEverSeenTarget = true;
            _currentState = EnemyState.Reaction;
            return;
        }

        _idleBehavior.Execute();
    }

    private void HandleReaction(bool seesTarget)
    {
        if (seesTarget == false)
        {
            if (_hasEverSeenTarget)
            {
                _lastSeenPoint = _targetTransform.position;
                _lostTargetBehavior.SetTargetPoint(_lastSeenPoint);
                _currentState = EnemyState.LostTarget;
                return;
            }
        }

        _reactionBehavior.Execute();
    }

    private void HandleLostTarget(bool seesPlayer)
    {
        if (seesPlayer)
        {
            _lostTargetBehavior.Agent.ResetPath();

            _currentState = EnemyState.Reaction;
            return;
        }

        _lostTargetBehavior.Execute();

        if (_lostTargetBehavior.IsPointReached())
        {
            _lostTargetBehavior.Agent.ResetPath();

            _currentState = EnemyState.Idle;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * MAX_AGGRO_DISTANCE);
    }

    private enum EnemyState
    {
        Idle,
        Reaction,
        LostTarget
    }
}
