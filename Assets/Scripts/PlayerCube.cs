using Mirror;
using UnityEngine;

public class PlayerCube : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHolaCountChanged))]
    private int _holaCount;   

    private void Update()
    {
        Move();

        if (isLocalPlayer && Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Send hola to server");
            Hola();
        }
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

    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("Starting server On Player");

    }

    [Command]
    public void Hola()
    {
        Debug.Log(" Resived Hola from Client");
        _holaCount++;
        ReplyHola();
    }

    [TargetRpc]
    private void ReplyHola()
    {
        Debug.Log("Resived Hola from Server");
    }

    private void OnHolaCountChanged(int oldValue,int newValue)
    {
        Debug.Log($"old count is {oldValue} and new is {newValue}");
    }


}
