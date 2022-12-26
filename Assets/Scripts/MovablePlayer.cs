using Mirror;
using UnityEngine;

[RequireComponent(typeof(NetworkTransform), typeof(Rigidbody))]
public class MovablePlayer : BaseMirrorPlayer
{
    [SerializeField] private float _moveSpeed;    

    private BasePlayerMovement _surfaceMovement;
    private BasePlayerInput _movementInput;

    private int _floorLayerMask = 6;    
    private bool _isOnGround;

    protected Rigidbody Rigidbody { get; private set; }

    protected override void Init()    
    {
        
        Rigidbody = GetComponent<Rigidbody>();

        _surfaceMovement = new SurfacePlayerMovement(Rigidbody, _moveSpeed);
        _movementInput = new MovePlayerInput();
    }

    protected override void Update()
    {
        if (isLocalPlayer)
        {
            base.Update();

            bool isMoveind = TryMove();
            bool isNotMovingOnGround = (isMoveind == false) && _isOnGround;

            if (isNotMovingOnGround)
            {
                Slowdown();
            }
        }     
    }    

    protected virtual void OnCollisionEnter(Collision collision)
    {              
        TrySetIsGrouded(collision, true);        
    }    

    private void OnCollisionExit(Collision collision)
    {
        TrySetIsGrouded(collision, false);
    }

    private void TrySetIsGrouded(Collision collision, bool value)
    {
        if (collision.gameObject.layer == _floorLayerMask)
        {
            _isOnGround = value;
        }
    }

    private void TryRepulse(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Rigidbody otherRigidbody))
        {
            Repulse(otherRigidbody);
        }
    }

    private void Repulse(Rigidbody otherRigidbody)
    {
        (Vector3 newVelocity1, Vector3 newVelocity2) =
                        CalculateNewVelocities(Rigidbody.velocity, otherRigidbody.velocity, Rigidbody.mass, otherRigidbody.mass);
        Rigidbody.velocity = newVelocity1;
        otherRigidbody.velocity = newVelocity2;
    }

    private (Vector3, Vector3) CalculateNewVelocities(Vector3 velocity1, Vector3 velocity2, float mass1, float mass2)
    {
        Vector3 newVelocity1 = (mass1 * velocity1 + mass2 * velocity2) / (mass1 + mass2);
        Vector3 newVelocity2 = (mass2 * velocity2 + mass1 * velocity1) / (mass1 + mass2);
        return (newVelocity1, newVelocity2);
    }

    protected virtual bool TryMove()
    {
        if (IsCanMove(out Vector3 moveVector))
        {
            _surfaceMovement.Move(moveVector);
            return true;
        }
        return false;
    }    

    protected virtual bool IsCanMove(out Vector3 moveVector)
    {
        Debug.Log(_isOnGround);
        return _movementInput.IsInputExist(out moveVector) && _isOnGround;
    }

    private void Slowdown()
    {
        float slowDownCoeficient = 0.2f;
        Rigidbody.velocity = Vector3.MoveTowards(Rigidbody.velocity, Vector3.zero, slowDownCoeficient);        
        Rigidbody.angularVelocity = Vector3.MoveTowards(Rigidbody.angularVelocity, Vector3.zero, slowDownCoeficient);        
    }  

}


