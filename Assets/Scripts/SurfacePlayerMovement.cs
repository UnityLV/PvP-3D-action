using UnityEngine;

public class SurfacePlayerMovement : BasePlayerMovement
{
    public SurfacePlayerMovement(Rigidbody rigidbody, float movementSpeed) : base(rigidbody, movementSpeed)
    {

    }

    public override void Move(Vector3 moveVector)
    {
        Vector3 position = Rigidbody.gameObject.transform.position;
        Rigidbody.MovePosition(position + moveVector * MovementSpeed * Time.deltaTime);
    }
    
}



