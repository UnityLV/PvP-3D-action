using UnityEngine;

public abstract class BasePlayerMovement
{
    protected Rigidbody Rigidbody { get; private set; }

    protected BasePlayerMovement(Rigidbody rigidbody)
    {
        Rigidbody = rigidbody;
    }

    public abstract void Move(Vector3 moveVector);
}


