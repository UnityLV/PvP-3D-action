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
        Debug.Log("Move" + moveVector);
        bool isNotMaxVelosity = Rigidbody.velocity.sqrMagnitude < (Vector3.forward * Scaler).sqrMagnitude;

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
                    Scaler;

        Rigidbody.AddForce(localMoveVector * Scaler);
    }

    private void SetMaxVelosity()
    {
        Vector3 maxVelosity = Rigidbody.velocity.normalized * Scaler;
        Rigidbody.velocity = maxVelosity;
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}



