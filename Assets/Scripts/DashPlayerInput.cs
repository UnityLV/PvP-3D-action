using UnityEngine;

public class DashPlayerInput : BasePlayerInput
{
    private int _triggerMouseButton = 0;    

    public bool IsDashTrigger()
    {
        return Input.GetMouseButtonDown(_triggerMouseButton);        
    }
}

