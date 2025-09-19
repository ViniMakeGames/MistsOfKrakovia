using Mirror;
using UnityEngine;

public class CustomAuthenticator : NetworkAuthenticator
{
    public override void OnStartServer()
    {
        NetworkServer.RegisterHandler<LoginMessage>(OnLoginMessage, false);
    }

    public override void OnStartClient()
    {
        NetworkClient.RegisterHandler<LoginSuccessMessage>(OnLoginSuccess, false);
    }

    public override void OnServerAuthenticate(NetworkConnectionToClient conn)
    {
        
    }

    public override void OnClientAuthenticate()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Guest");
        NetworkClient.Send(new LoginMessage { name = playerName });
    }

    private void OnLoginMessage(NetworkConnectionToClient conn, LoginMessage msg)
    {
        conn.authenticationData = msg.name;
        conn.Send(new LoginSuccessMessage());
        ServerAccept(conn);
    }

    private void OnLoginSuccess(LoginSuccessMessage msg)
    {
        ClientAccept();
    }
}

public struct LoginMessage : NetworkMessage
{
    public string name;
}

public struct LoginSuccessMessage : NetworkMessage { }
