using Mirror;
using UnityEngine;




public sealed class MyNetworkManager : NetworkManager
{
    
    public override void OnStartServer()
    {
        base.OnStartServer();

        Debug.Log("Server Starting");
    }

    public override void OnStopServer()
    {
        base.OnStopServer();

        Debug.Log("Server Stoping");
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();

        Debug.Log("Connect to server");
    }

    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();

        Debug.Log("Disconect to server");
    }
}
