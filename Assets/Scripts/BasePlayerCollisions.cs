using UnityEngine;
using UnityEngine.Events;
public abstract class BasePlayerCollisions
{
    protected BaseMirrorPlayer ThisPlayer { get; private set; }

    public event UnityAction CollisionСonfirm;

    protected BasePlayerCollisions(BaseMirrorPlayer thisPlayer)
    {
        ThisPlayer = thisPlayer;
    }

    public abstract void CollisionWith(Collision Collision);

    protected void InvokeColisionConfirm()
    {
        CollisionСonfirm?.Invoke();
    }
}


