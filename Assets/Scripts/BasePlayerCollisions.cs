using UnityEngine;

public abstract class BasePlayerCollisions
{
    protected BaseMirrorPlayer ThisPlayer { get; private set; }

    protected BasePlayerCollisions(BaseMirrorPlayer thisPlayer)
    {
        ThisPlayer = thisPlayer;
    }

    public abstract void CollisonWith(Collision Collision);
}


