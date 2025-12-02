using UnityEngine;

public class Player : MonoBehaviour
{
    private const string HORIZONTAL_AXIS_NAME = "Horizontal";
    private const string VERTICAL_AXIS_NAME = "Vertical";

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private float _deadZone = 0.05f;
    
    private Vector3 _input;
    private Mover _mover;

    private void Awake()
    {
        _mover = new Mover(transform, _moveSpeed, _rotationSpeed);
    }

    private void Update()
    {
        _input = new Vector3(Input.GetAxis(HORIZONTAL_AXIS_NAME), 0, Input.GetAxis(VERTICAL_AXIS_NAME));

        if(_input.magnitude <= _deadZone)
            return;

        _mover.ProcessMoveTo(GetNormalizedDirection(), Time.deltaTime);
        _mover.ProcessRotateTo(GetNormalizedDirection(), Time.deltaTime);
    }

    public Vector3 GetNormalizedDirection() => _input.normalized;
}
