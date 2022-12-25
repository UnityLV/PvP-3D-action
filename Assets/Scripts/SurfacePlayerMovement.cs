using UnityEngine;

public class SurfacePlayerMovement : BasePlayerMovement
{
    private Transform _transform;
    

    public SurfacePlayerMovement(Rigidbody rigidbody, float movementSpeed) : base(rigidbody, movementSpeed)
    {
        _transform = Rigidbody.gameObject.transform;
        
    }

    public override void Move(Vector3 moveVector)
    {
        Vector3 localMoveVector = 
            ((_transform.right * moveVector.x) + 
            (_transform.forward * moveVector.z)) * 
            MovementSpeed;

        bool isNotMaxVelosity = Rigidbody.velocity.sqrMagnitude < (Vector3.one * MovementSpeed).sqrMagnitude;
        if (isNotMaxVelosity)
        {
            Rigidbody.AddForce(localMoveVector * MovementSpeed);
        }

    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}



