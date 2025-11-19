using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float MAX_AGGRO_DISTANCE = 3;

    private IIdleBehavior _idleBehavior;
    private IReactionBehavior _reactionBehavior;
    private DistanceDetector _distanceDetector;

    public void Initialization(IIdleBehavior idleBehavior, IReactionBehavior reactionBehavior, Transform playerTransform)
    {
        _idleBehavior = idleBehavior;
        _reactionBehavior = reactionBehavior;

        _distanceDetector = new DistanceDetector(transform, playerTransform);
    }

    private void Update()
    {
        if (_distanceDetector.InZone(MAX_AGGRO_DISTANCE))
            ReactionState();
        else
            RestState();
    }

    private void RestState() => _idleBehavior.Execute();

    private void ReactionState() => _reactionBehavior.Execute();
}
