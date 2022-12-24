using UnityEngine;

public class MovePlayerInput : BasePlayerInput
{

    public bool IsInputExist(out Vector3 moveVector)
    {
        Vector3 rawMoveVector = CalculateRawMoveVector();

        if (rawMoveVector == Vector3.zero)
        {
            moveVector = default;
            return false;
        }

        moveVector = rawMoveVector.normalized;
        return true;
    }   


    private Vector3 CalculateRawMoveVector()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        return new Vector3(horizontal, 0, vertical);
    }

}

