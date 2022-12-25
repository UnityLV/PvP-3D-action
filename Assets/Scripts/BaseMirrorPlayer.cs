using Mirror;
using System;
using System.Collections;
using UnityEngine;


public class RotationMovement : BasePlayerMovement
{
    public float _sensitivity;

    private readonly float minVerticalRotation = -45.0f;
    private readonly float maxVerticalRotation = 45.0f;


    public RotationMovement(float sensitivity,Rigidbody rigidbody, float movementSpeed) : base(rigidbody, movementSpeed)
    {
        _sensitivity = sensitivity;
    }


    public override void Move(Vector3 moveVector)
    {
        float rotationX = moveVector.x * Time.deltaTime;
        float rotationY = moveVector.y * Time.deltaTime;     
        




    }
    

    public override void Reset()
    {
        throw new NotImplementedException();
    }
}


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

