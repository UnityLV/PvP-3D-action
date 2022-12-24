using System;
using System.Collections;
using UnityEngine;

public class DashMovement : BasePlayerMovement
{
    private float _maxJerkDistance = 20f;
    private Transform _transform;
    private Vector3 _jerkDirection;
    private Func<IEnumerator, Coroutine> _startCoroutine;
    private readonly float _dashDistance;
    private Vector3 _startDashVelosity;

    public DashMovement(
        Func<IEnumerator, Coroutine> startCoroutine,float dashDistance,
        Rigidbody rigidbody, float movementSpeed) 
        : base(rigidbody, movementSpeed)
    {
        _transform = Rigidbody.gameObject.transform;
        _startCoroutine = startCoroutine;
        _dashDistance = dashDistance;
    }

    public override void Move(Vector3 moveVector)
    {
        moveVector.Normalize();

        var startPosition = Rigidbody.position;
        var endPosition = CalculateEndPosition(startPosition, moveVector.normalized);
        _startDashVelosity = Rigidbody.velocity;

        SetDashVelocity(moveVector);
        _startCoroutine(WaitForDashEnd(endPosition));
    }

    private Vector3 CalculateEndPosition(Vector3 startPosition, Vector3 moveVector)
    {
        return startPosition + moveVector * _dashDistance;
    }

    private void SetDashVelocity(Vector3 moveVector)
    {
        Rigidbody.velocity = moveVector * MovementSpeed;
    }

    private IEnumerator WaitForDashEnd(Vector3 endPosition)
    {
        while (Rigidbody.position != endPosition && !Rigidbody.IsSleeping())
        {
            yield return null;
        }

        Rigidbody.velocity = _startDashVelosity;
    }
}
