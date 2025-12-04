using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private readonly int _isRunninKey = Animator.StringToHash("IsRunning");

    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;

    private void Update()
    {
        if(_player.CurrentVelocity.magnitude >= 0.05f)
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
