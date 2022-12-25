﻿using UnityEngine;


public class DashPlayerInput : BasePlayerInput
{
    private int _triggerMouseButton = 0;
    private Transform _transform;
    private Vector3 _defaultDashDirection = new Vector3(0.2f,0f,1f);

    public DashPlayerInput(Transform transform)
    {
        _transform = transform;
    }

    public override bool IsInputExist(out Vector3 moveVector)
    {
        if (IsDashTrigger() == false)
        {
            moveVector = default;
            return false;
        }

        moveVector = CalculateDashVector();
        return true;
    }
    private bool IsDashTrigger() => Input.GetMouseButtonDown(_triggerMouseButton);

    private Vector3 CalculateDashVector()
    {
        return _defaultDashDirection;
    }
}

