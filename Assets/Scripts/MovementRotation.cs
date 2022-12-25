using UnityEngine;

public class MovementRotation : BasePlayerMovement
{
    private Transform _toRotate;
    private readonly Vector2 _directionalSensetivity;
    private float _verticalRotationLimitEngle = 25;
    private float _verticalRotation;

    public MovementRotation(Transform toRotate, Vector2 directionalSensetivity, Rigidbody rigidbody, float movementSpeed) : base(rigidbody, movementSpeed)
    {
        _toRotate = toRotate;
        _directionalSensetivity = directionalSensetivity;
    }

    public override void Move(Vector3 moveVector)
    {
        moveVector *= _directionalSensetivity;
        moveVector *= Scaler;

        RotateVertical(moveVector.y);
        RotateHorizontal(moveVector.x);
    }

    protected virtual void RotateVertical(float engle)
    {
        _verticalRotation -= engle;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -_verticalRotationLimitEngle, _verticalRotationLimitEngle);

        _toRotate.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
    }

    protected virtual void RotateHorizontal(float engle)
    {
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * engle);
        Rigidbody.MoveRotation(Rigidbody.rotation * deltaRotation);
    }

    public override void Reset()
    {
        _verticalRotation = 0;
    }
}


