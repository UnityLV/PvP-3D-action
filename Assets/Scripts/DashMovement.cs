using System;
using System.Collections;
using UnityEngine;

public class DashMovement : BasePlayerMovement
{
    private Vector3 _startDashVelosity;
    private readonly float _dashDistance;
    private Func<IEnumerator, Coroutine> _startCoroutine;

    public DashMovement(
        Func<IEnumerator, Coroutine> startCoroutine, float dashDistance,
        Rigidbody rigidbody, float movementSpeed)
        : base(rigidbody, movementSpeed)
    {
        _startCoroutine = startCoroutine;
        _dashDistance = dashDistance;
    }

    public override void Move(Vector3 moveVector)
    {
        SetDashVelocity(moveVector);

        _startDashVelosity = Rigidbody.velocity;
        Vector3 endPosition = CalculateEndPosition(moveVector);

        _startCoroutine(WaitForDashEnd(endPosition));
    }

    private Vector3 CalculateEndPosition(Vector3 moveVector)
    {
        Vector3 startPosition = Rigidbody.position;
        Vector3 endPosition = startPosition + moveVector * _dashDistance;
        return endPosition;
    }

    private void SetDashVelocity(Vector3 moveVector)
    {
        Rigidbody.velocity = moveVector * MovementSpeed;
    }

    private IEnumerator WaitForDashEnd(Vector3 endPosition)
    {
        while ((AreVectorsAlmostEqual(Rigidbody.position, endPosition) == false) && Rigidbody.IsSleeping() == false)
        {
            yield return null;
        }

        Rigidbody.velocity = _startDashVelosity;
    }

    private bool AreVectorsAlmostEqual(Vector3 vector1, Vector3 vector2, float threshold = 0.1f)
    {
        return Mathf.Abs(vector1.x - vector2.x) < threshold &&
               Mathf.Abs(vector1.y - vector2.y) < threshold &&
               Mathf.Abs(vector1.z - vector2.z) < threshold;
    }

    public override void Reset()
    {
        throw new NotImplementedException();
    }
}
