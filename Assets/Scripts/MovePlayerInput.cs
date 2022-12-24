using UnityEngine;

public class MovePlayerInput : BasePlayerInput
{
    private float _moveSpeed;    

    public MovePlayerInput(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
    }

    public bool IsInputExist(out Vector3 moveVector)
    {
        Vector3 rawMoveVector = CalculateRawMoveVector();

        if (rawMoveVector == Vector3.zero)
        {
            moveVector = default;
            return false;
        }
        moveVector = CalculateMoveBySpeed(rawMoveVector);
        return true;
    }   

    private Vector3 CalculateMoveBySpeed(Vector3 rawMoveVector)
    {
        return rawMoveVector.normalized * _moveSpeed;
    }

    private Vector3 CalculateRawMoveVector()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        return new Vector3(horizontal, 0, vertical);
    }

}

