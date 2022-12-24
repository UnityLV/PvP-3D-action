using Mirror;
using UnityEngine;



[RequireComponent(typeof(NetworkTransform), typeof(Rigidbody))]
public abstract class MovablePlayer : BaseMirrorPlayer
{
    [SerializeField] private float _moveSpeed;
    private MovePlayerInput _input;


    protected Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        TryMove();
    }

    private void TryMove()
    {
        if (IsCanMove(out Vector3 moveVector))
        {
            Move(moveVector);
        }
    }

    protected virtual bool IsCanMove(out Vector3 moveVector)
    {
        return _input.IsInputExist(out moveVector);
    }

    protected virtual void Move(Vector3 moveVector)
    {
        Rigidbody.MovePosition(transform.position + moveVector * _moveSpeed * Time.deltaTime);
    }
}


