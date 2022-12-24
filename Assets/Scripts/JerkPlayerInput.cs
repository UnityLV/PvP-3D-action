using UnityEngine;

public class JerkPlayerInput : BasePlayerInput
{
    private int _triggerMouseButton = 0;    

    public bool IsJerkTrigger()
    {
        return Input.GetMouseButtonDown(_triggerMouseButton);        
    }
}

