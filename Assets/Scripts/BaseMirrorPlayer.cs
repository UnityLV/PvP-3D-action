using Mirror;
using UnityEngine;   

[RequireComponent(typeof(NetworkIdentity))]
public abstract class BaseMirrorPlayer : NetworkBehaviour
{
    public override void OnStartClient()
    {
        Init();
        base.OnStartClient();
    }

    protected abstract void Init();

    protected virtual void Update()
    {
        if (isLocalPlayer == false)
            return;
    }
}

