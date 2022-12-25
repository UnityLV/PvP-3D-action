using UnityEngine;

public class MovementRotation : BasePlayerMovement
{
    private Transform _toRotate;    
    private float _rotationLimitEngle = 45;

    private float _verticalRotation;

    public MovementRotation(Transform toRotate, Rigidbody rigidbody, float movementSpeed) : base(rigidbody, movementSpeed)
    {
        _toRotate = toRotate;
    }

    public override void Move(Vector3 moveVector)
    {
        moveVector *= MovementSpeed;

        RotateVertical(moveVector.y);
        RotateHorizontal(moveVector.x);
    }

    protected virtual void RotateVertical(float engle)
    {
        _verticalRotation += engle;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -_rotationLimitEngle, _rotationLimitEngle);

        _toRotate.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
    }

    protected virtual void RotateHorizontal(float engle)
    {
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * engle);
        Rigidbody.MoveRotation(Rigidbody.rotation * deltaRotation);
    }

    public override void Reset()
    {

    }
}

public class DashPlayer : MovablePlayer
{
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private Transform _cameraHolder;
    private float _mouseSensetivity = 4;

    private BasePlayerInput _dashInput;
    private BasePlayerMovement _dashMovement;

    private BasePlayerInput _mouseMoveInput;
    private BasePlayerMovement _viewRotationMovement;

    protected override void Init()
    {
        base.Init();
        _dashInput = new DashPlayerInput(transform);
        _dashMovement = new DashMovement(StartCoroutine, _dashSpeed, Rigidbody, _dashDistance);

        _mouseMoveInput = new MouseMoveInput(1.000000000f);
        _viewRotationMovement = new MovementRotation(_cameraHolder,Rigidbody, 1f);
    }

    protected override void Update()
    {
        if (isLocalPlayer == false)
            return;

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


