using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCube : NetworkBehaviour
{

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if (isLocalPlayer)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal * 0.1f, moveVertical * 0.1f, 0);
            transform.position += movement;
        }
    }
}
