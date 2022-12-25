using UnityEngine;

public abstract class BasePlayerMovement
{
    protected Rigidbody Rigidbody { get; private set; }
    protected float Scaler { get; private set; }

    public virtual bool IsActive { get; protected set; }

    protected BasePlayerMovement(Rigidbody rigidbody, float scaler)
    {
        Rigidbody = rigidbody;
        Scaler = scaler;
    }

    public abstract void Move(Vector3 moveVector);

    public abstract void Reset();
}


