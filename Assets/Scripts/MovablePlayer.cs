using Mirror;
using UnityEngine;

[RequireComponent(typeof(NetworkTransform), typeof(Rigidbody))]
public abstract class MovablePlayer : BaseMirrorPlayer
{
    [SerializeField] private float _moveSpeed;
    private MovePlayerInput _input = new();
    private SurfacePlayerMovement _surfaceMovement;

    protected Rigidbody Rigidbody { get; private set; }

    protected override void Init()
    {
        Rigidbody = GetComponent<Rigidbody>();

        _surfaceMovement = new SurfacePlayerMovement(Rigidbody, _moveSpeed);        
    }

    protected virtual void Update()
    {
        
        TryMove();
    }

    protected virtual void TryMove()
    {
        if (IsCanMove(out Vector3 moveVector))
        {
            _surfaceMovement.Move(moveVector);
        }
    }

    protected virtual bool IsCanMove(out Vector3 moveVector)
    {
        return _input.IsInputExist(out moveVector);
    }

    
}


