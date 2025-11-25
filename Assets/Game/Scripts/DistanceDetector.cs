using UnityEngine;

public class DistanceDetector
{
    private Transform _firstTarget;
    private Transform _secondTarget;

    public DistanceDetector(Transform firstTarget, Transform secondTarget)
    {
        _firstTarget = firstTarget;
        _secondTarget = secondTarget;
    }

    public Vector3 FirstTargetPosition => _firstTarget.position;
    public Vector3 SecondTargetPosition => _secondTarget.position;

    public bool InZone(float maxDistance)
    {
        Vector3 direction = _secondTarget.position - _firstTarget.position;
        return direction.magnitude <= maxDistance;
    }
}
