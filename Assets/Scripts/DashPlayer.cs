using UnityEngine;

public class DashPlayer : MovablePlayer
{
    [SerializeField] private float _dashDistance;
    [SerializeField] private Vector3 _dashDirection;
    [SerializeField] private float _dashSpeed;
    private DashPlayerInput _playerInput;
    private DashMovement _dashMovement;

    private void Start()
    {
        _playerInput = new DashPlayerInput();
        _dashMovement = new DashMovement(StartCoroutine, _dashSpeed, Rigidbody, _dashDistance);
    }

    protected override void Update()
    {
        base.Update();

        if (_playerInput.IsDashTrigger())
        {
            _dashMovement.Move(_dashDirection);
        }
    }

    private Vector3 CalculateDashVector() => transform.InverseTransformPoint(_dashDirection);
}


