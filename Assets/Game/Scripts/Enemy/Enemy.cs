using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private const float MAX_AGGRO_DISTANCE = 3;

    private EnemyState _currentState = EnemyState.Idle;

    private IBehavior _idleBehavior;
    private IBehavior _reactionBehavior;
    private ILostTarget _lostTargetBehavior;

    private DistanceDetector _distanceDetector;
    private Transform _targetTransform;

    private Vector3 _lastSeenTargetPosition;

    private bool _hasEverSeenTarget = false;

    public void Initialization(IBehavior idleBehavior, IBehavior reactionBehavior, ILostTarget lostTargetBehavior, Transform targetTransform)
    {
        _idleBehavior = idleBehavior;
        _reactionBehavior = reactionBehavior;
        _targetTransform = targetTransform;
        _lostTargetBehavior = lostTargetBehavior;

        _distanceDetector = new DistanceDetector(transform, targetTransform);
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
            _lastSeenTargetPosition = _targetTransform.position;
            _currentState = EnemyState.Reaction;
            return;
        }

        _idleBehavior.Execute(Time.deltaTime);
    }

    private void HandleReaction(bool seesTarget)
    {
        if (!seesTarget)
        {
            if (_hasEverSeenTarget)
            {
                _lostTargetBehavior.SetLastSeenPosition(_lastSeenTargetPosition);

                _currentState = EnemyState.LostTarget;
                return;
            }
        }
        else
        {
            _lastSeenTargetPosition = _targetTransform.position;
        }

        _reactionBehavior.Execute(Time.deltaTime);
    }

    private void HandleLostTarget(bool seesPlayer)
    {
        if (seesPlayer)
        {
            _lostTargetBehavior.End();
            _currentState = EnemyState.Reaction;
            return;
        }

        _lostTargetBehavior.Execute(Time.deltaTime);

        if (_lostTargetBehavior.IsFinished)
        {
            _lostTargetBehavior.End();
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