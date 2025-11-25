using UnityEngine;

public class RunOutReaction : IBehavior
{
    private float _moveSpeed = 1;
    private float _rotationSpeed = 1000;

    private Mover _mover;

    private Transform _targetTransform;
    private Transform _characterTransform;

    public RunOutReaction(Transform characterTransform, Transform targetTransform) 
    {
        _targetTransform = targetTransform;
        _characterTransform = characterTransform;

        _mover = new Mover(characterTransform, _moveSpeed, _rotationSpeed);
    }

    public void Execute()
    {
        Vector3 normalizedDirection = GetDirectionToTarget().normalized;

        _mover.ProcessMoveTo(new Vector3(-normalizedDirection.x, 0, -normalizedDirection.z));
        _mover.ProcessRotateTo(new Vector3(-normalizedDirection.x, 0, -normalizedDirection.z));
    }

    private Vector3 GetDirectionToTarget() => _targetTransform.position - _characterTransform.position;
}
