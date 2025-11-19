using System.Collections.Generic;
using UnityEngine;

public class PatrolIdle : IIdleBehavior
{
    private const float MIN_DISTANCE_TO_TARGET = 0.05f;

    private float _moveSpeed = 1;
    private float _rotationSpeed = 1000;

    private Queue<Vector3> _targetsPositions;
    private Vector3 _currentTarget;

    private Mover _mover;
    private Transform _characterTransform;

    public PatrolIdle(Transform characterTransform, List<Transform> targets) 
    {
        _characterTransform = characterTransform;

        _mover = new Mover(characterTransform, _moveSpeed, _rotationSpeed);
        _targetsPositions = new Queue<Vector3>();

        foreach (var target in targets)
            _targetsPositions.Enqueue(target.position);

        SwitchTarget();
    }
    
    public void Execute()
    {
        Vector3 direction = GetDirectionToTarget();

        if (direction.magnitude < MIN_DISTANCE_TO_TARGET)
            SwitchTarget();
        
        Vector3 normalizedDirection = direction.normalized;

        _mover.ProcessMoveTo(normalizedDirection);
        _mover.ProcessRotateTo(normalizedDirection);
    }

    private Vector3 GetDirectionToTarget() => _currentTarget - _characterTransform.position;
    
    private void SwitchTarget()
    {
        _currentTarget = _targetsPositions.Dequeue();
        _targetsPositions.Enqueue(_currentTarget);
    }
}
