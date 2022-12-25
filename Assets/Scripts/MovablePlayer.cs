using Mirror;
using System;
using UnityEngine;

[RequireComponent(typeof(NetworkTransform), typeof(Rigidbody))]
public abstract class MovablePlayer : BaseMirrorPlayer
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
        base.Update();

        bool isNotMovingOnGround = (TryMove() == false) && _isOnGround;

        if (isNotMovingOnGround)
        {
            Slowdown();
        }
    }    

    private void OnCollisionEnter(Collision collision)
    {
        TrySetIsGrouded(collision, true);
    }

    private void OnCollisionExit(Collision collision)
    {
        TrySetIsGrouded(collision, false);
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
        return _movementInput.IsInputExist(out moveVector) && _isOnGround;
    }

    private void Slowdown()
    {
        float slowDownCoeficient = 0.2f;
        Rigidbody.velocity = Vector3.MoveTowards(Rigidbody.velocity, Vector3.zero, slowDownCoeficient);        
        Rigidbody.angularVelocity = Vector3.MoveTowards(Rigidbody.angularVelocity, Vector3.zero, slowDownCoeficient);        
    }

    private void TrySetIsGrouded(Collision collision, bool value)
    {
        if (collision.gameObject.layer == _floorLayerMask)
        {
            _isOnGround = value;
        }
    }

}


