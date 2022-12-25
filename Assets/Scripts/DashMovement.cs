using System;
using System.Collections;
using UnityEngine;

public sealed class DashMovement : BasePlayerMovement
{
    private readonly Transform _transform;
    private readonly Func<IEnumerator, Coroutine> _startCoroutine;
    private readonly PhysicMaterial _dashMaterial;

    private Vector3 _dashStartPosition;

    private float _dashDistanceTraveled;
    private float _dashDistance;

    public DashMovement(Func<IEnumerator, Coroutine> startCoroutine, float dashDistance,
        PhysicMaterial dashMaterial,
        Rigidbody rigidbody, float scaler = 60)
            : base(rigidbody, scaler)
    {
        _transform = rigidbody.transform;
        _startCoroutine = startCoroutine;
        _dashDistance = dashDistance;
        _dashMaterial = dashMaterial;
    }

    public override void Move(Vector3 moveVector)
    {
        if (IsActive == false)
        {
            _startCoroutine(Dash(moveVector));
        }
    }

    private IEnumerator Dash(Vector3 moveVector)
    {
        IsActive = true;
        Rigidbody.velocity = default;
        PhysicMaterial defaultMaterial = null;

        if (Rigidbody.gameObject.TryGetComponent(out CapsuleCollider collider))
        {
            defaultMaterial = collider.material;
            collider.material = _dashMaterial;
        }

        Rigidbody.AddForce(moveVector * Scaler, ForceMode.Impulse);

        _dashDistanceTraveled = 0;
        _dashStartPosition = _transform.position;

        yield return new WaitForSeconds(0.1f);

        Vector3 continuesDashVelosity = Rigidbody.velocity;

        while (ConditionForDash())
        {
            _dashDistanceTraveled += Vector3.Distance(_transform.position, _dashStartPosition);
            _dashStartPosition = _transform.position;

            Rigidbody.velocity = continuesDashVelosity;

            yield return null;
        }

        if (defaultMaterial != null)
        {
            collider.material = defaultMaterial;
        }
        IsActive = false;
    }

    private bool ConditionForDash()
    {
        bool isNotReachDistance = _dashDistanceTraveled < _dashDistance;
        bool isStoped = IsAlmostEqualVector(Rigidbody.velocity, Vector3.zero);
        Debug.Log(isStoped);

        return isNotReachDistance && isStoped == false;
    }

    private bool IsAlmostEqualVector(Vector3 vectorA, Vector3 vectorB, float threshold = 0.001f)
    {
        return Mathf.Abs(vectorA.x - vectorB.x) < threshold &&
                Mathf.Abs(vectorA.y - vectorB.y) < threshold &&
                Mathf.Abs(vectorA.z - vectorB.z) < threshold;
    }


    public override void Reset()
    {
        throw new NotImplementedException();
    }
}
