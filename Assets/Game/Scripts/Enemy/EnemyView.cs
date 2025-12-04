using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private readonly int _isRunninKey = Animator.StringToHash("IsRunning");

    [SerializeField] private Animator _animator;
    [SerializeField] private Enemy _enemy;

    private void Update()
    {
        if(_enemy.CurrentVelocity.magnitude >= 0.05f)
            StartRunning();
        else
            StopRunning();
    }

    private void StartRunning()
    {
        _animator.SetBool(_isRunninKey, true);
    }

    private void StopRunning()
    {
        _animator.SetBool(_isRunninKey, false);
    }
}
