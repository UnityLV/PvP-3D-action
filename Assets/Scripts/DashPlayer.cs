using UnityEngine;

public class DashPlayer : MovablePlayer
{
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private Transform _cameraHolder;
    [SerializeField] private Vector2 _mouseSensetivity;

    private BasePlayerInput _dashInput;
    private BasePlayerMovement _dashMovement;

    private BasePlayerInput _mouseMoveInput;
    private BasePlayerMovement _viewRotationMovement;

    protected override void Init()
    {
        base.Init();
        _dashInput = new DashPlayerInput(transform);
        _dashMovement = new DashMovement(StartCoroutine, _dashSpeed, Rigidbody, _dashDistance);

        _mouseMoveInput = new MouseMoveInput();
        _viewRotationMovement = new MovementRotation(_cameraHolder, _mouseSensetivity,Rigidbody, 1);
    }

    protected override void Update()
    {
        
        base.Update();

        if (_dashInput.IsInputExist(out Vector3 dashDirection))
        {
            _dashMovement.Move(dashDirection);
        }

        if (_mouseMoveInput.IsInputExist(out Vector3 mouseMovement))
        {
            _viewRotationMovement.Move(mouseMovement);
        }



    }


}


