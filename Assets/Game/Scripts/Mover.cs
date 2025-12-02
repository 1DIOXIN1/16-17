using UnityEngine;

public class Mover
{
    private Transform _charachterTransform;

    public Mover(Transform charachterTransform, float moveSpeed, float rotationSpeed)
    {
        _charachterTransform = charachterTransform;
        MoveSpeed = moveSpeed;
        RotationSpeed = rotationSpeed;
    }

    public float MoveSpeed{get; private set;}
    public float RotationSpeed{get; private set;}

    public virtual void ProcessMoveTo(Vector3 direction, float deltaTime)
    {
        Vector3 movement = direction * MoveSpeed * Time.deltaTime;

        _charachterTransform.position += movement;
    }

    public virtual void ProcessRotateTo(Vector3 direction, float deltaTime)
    {
        if(direction == Vector3.zero)//Возможно костыль
            return;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        float step = RotationSpeed * deltaTime;

        _charachterTransform.rotation = Quaternion.RotateTowards(_charachterTransform.rotation, lookRotation, step);
    }
}
