using UnityEngine;

public class RandomWalkIdle : IBehavior
{
    private float _moveSpeed = 1;
    private float _rotationSpeed = 1000;

    private Mover _mover;

    private Vector3 _currentDirection;

    private float _timeBeforeChangeDirection = 1;
    private float _currentTime;

    public RandomWalkIdle(Transform characterTransform) 
    {
        _mover = new Mover(characterTransform, _moveSpeed, _rotationSpeed);
    }

    public void Execute()
    {
        if(_timeBeforeChangeDirection <= _currentTime)
        {
            SwitchDirection();
            _currentTime = 0;
        }
        else
        {
            _currentTime += Time.deltaTime;
        }

        _mover.ProcessMoveTo(_currentDirection);
        _mover.ProcessRotateTo(_currentDirection);
    }

    private void SwitchDirection()
    {
        Vector3 normalizedDirection = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2)).normalized; //Иногда останавливается из-за 0го вектора
        _currentDirection = normalizedDirection;
    }
}
