using Mirror;
using UnityEngine;

[RequireComponent(typeof(NetworkIdentity))]
public abstract class BaseMirrorPlayer : NetworkBehaviour
{    

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Debug.Log("Init"); 
        Init();
    }

    protected abstract void Init();

    protected virtual void Update()
    {
        
    }
}

