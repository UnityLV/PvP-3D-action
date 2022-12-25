using UnityEngine;

public abstract class BasePlayerMovement
{
    protected Rigidbody Rigidbody { get; private set; }
    protected float MovementSpeed { get; private set; }

    protected BasePlayerMovement(Rigidbody rigidbody, float movementSpeed)
    {
        Rigidbody = rigidbody;
        MovementSpeed = movementSpeed;
    }

    public abstract void Move(Vector3 moveVector);

    public abstract void Reset();
}


