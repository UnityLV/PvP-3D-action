using Mirror;
using UnityEngine;

[RequireComponent(typeof(NetworkTransform), typeof(Rigidbody))]
public abstract class MovablePlayer : BaseMirrorPlayer
{
    [SerializeField] private float _moveSpeed;
    private MovePlayerInput _input;
    private SurfacePlayerMovement _surfaceMovement;

    protected Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();

        _input = new();
        _surfaceMovement = new(Rigidbody, _moveSpeed);
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


