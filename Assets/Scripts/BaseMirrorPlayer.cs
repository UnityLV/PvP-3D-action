using Mirror;
using System;
using System.Collections;
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
}

