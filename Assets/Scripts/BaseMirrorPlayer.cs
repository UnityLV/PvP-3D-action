using Mirror;
using System;
using UnityEngine;

[RequireComponent(typeof(NetworkIdentity))]
public abstract class BaseMirrorPlayer : NetworkBehaviour
{

    private void Start()
    {
            

    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        Init();
        Debug.Log("Init");


    }

    [Command]
    private void CmdInit()
    {
        Init();
    }

    protected abstract void Init();

    protected virtual void Update()
    {
        
    }
}

