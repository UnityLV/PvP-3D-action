using Mirror;
using System;
using UnityEngine;

[RequireComponent(typeof(NetworkTransform), typeof(Rigidbody))]
public abstract class MovablePlayer : BaseMirrorPlayer
{
    [SerializeField] private float _moveSpeed;    
    private SurfacePlayerMovement _surfaceMovement;
    private MovePlayerInput _input = new();
    private int _floorLayer = 6;

    private bool _isOnGround;

    protected Rigidbody Rigidbody { get; private set; }

    protected override void Init()
    {
        Rigidbody = GetComponent<Rigidbody>();

        _surfaceMovement = new SurfacePlayerMovement(Rigidbody, _moveSpeed);
    }

    protected virtual void Update()
    {
        if (TryMove() == false && _isOnGround)
        {
            Slowdown();
        }
        
    }    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == _floorLayer)
        {
            _isOnGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == _floorLayer)
        {
            _isOnGround = false;
        }
       
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

    private void Slowdown()
    {
        Rigidbody.velocity = Vector3.Lerp(Rigidbody.velocity, Vector3.zero, Time.deltaTime);
    }

    protected virtual bool IsCanMove(out Vector3 moveVector)
    {        
        return _input.IsInputExist(out moveVector) && _isOnGround;
    }


}


