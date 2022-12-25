using UnityEngine;

public class MouseMoveInput : BasePlayerInput
{
    private float _sensitivity = 1;

    public MouseMoveInput(float sensitivity = 1)
    {
        _sensitivity = sensitivity;
    }

    public override bool IsInputExist(out Vector3 moveVector)
    {
        var mouseDelta = CalculatDelta();

        if (mouseDelta == default)
        {
            moveVector = default;
            return false;
        }

        moveVector = mouseDelta;
        return true;
    }


    private Vector2 CalculatDelta()
    {
        float mouseX = Input.GetAxis("Mouse X") * _sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _sensitivity;

        return new Vector2(mouseX, mouseY);
    }
}

