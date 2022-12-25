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
        bool isNotMaxVelosity = Rigidbody.velocity.sqrMagnitude < (Vector3.forward * MovementSpeed).sqrMagnitude;

        if (isNotMaxVelosity)
        {
            AddVelosity(moveVector);
        }
        else
        {
            SetMaxVelosity();
        }
    }    

    private void AddVelosity(Vector3 moveVector)
    {
        Vector3 localMoveVector =
                    ((_transform.right * moveVector.x) +
                    (_transform.forward * moveVector.z)) *
                    MovementSpeed;

        Rigidbody.AddForce(localMoveVector * MovementSpeed);
    }

    private void SetMaxVelosity()
    {
        Vector3 maxVelosity = Rigidbody.velocity.normalized * MovementSpeed;
        Rigidbody.velocity = maxVelosity;
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}



