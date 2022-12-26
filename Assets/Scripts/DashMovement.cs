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
    private WaitForSeconds _axelerationDelay = new WaitForSeconds(0.1f);

    private PhysicMaterial _defaultMaterial;
    private Collider _collider;

    public DashMovement(Func<IEnumerator, Coroutine> startCoroutine, float dashDistance,
        PhysicMaterial dashMaterial,
        Rigidbody rigidbody, float scaler)
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
        SetStartValues();
        SetStartCondition(moveVector);

        yield return _axelerationDelay;

        yield return ContinuesDash();

        SetFinalValues();
    }

    private void SetStartValues()
    {
        IsActive = true;
        Rigidbody.velocity = default;
        _dashStartPosition = _transform.position;
        _dashDistanceTraveled = 0;

        if (Rigidbody.gameObject.TryGetComponent(out _collider))
        {
            _defaultMaterial = _collider.material;
            _collider.material = _dashMaterial;
        }

    }

    private void SetStartCondition(Vector3 moveVector)
    {
        var worldSpaceVector = _transform.TransformDirection(moveVector);

        Rigidbody.AddForce(worldSpaceVector * Scaler, ForceMode.Impulse);
    }

    private IEnumerator ContinuesDash()
    {
        Vector3 continuesDashVelosity = Rigidbody.velocity;

        while (ConditionForContinueDash())
        {
            _dashDistanceTraveled += Vector3.Distance(_transform.position, _dashStartPosition);
            _dashStartPosition = _transform.position;
            
            Rigidbody.velocity = continuesDashVelosity;

            yield return null;
        }
    }

    private void SetFinalValues()
    {
        if (_defaultMaterial != null)
        {
            _collider.material = _defaultMaterial;
        }
        IsActive = false;
    }


    private bool ConditionForContinueDash()
    {
        bool isNotReachDistance = _dashDistanceTraveled < _dashDistance;
        bool isStoped = IsAlmostEqualVector(Rigidbody.velocity, Vector3.zero);
        
        return isNotReachDistance && isStoped == false;
    }

    private bool IsAlmostEqualVector(Vector3 vectorA, Vector3 vectorB, float threshold = 0.8f)
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
