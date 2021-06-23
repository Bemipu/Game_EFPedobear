using MLAPI;
using UnityEngine;
using MLAPI.Transports.UNET;
using MLAPI.NetworkVariable;

namespace Connection
{
    public class Connection : NetworkBehaviour
    {
        NetworkVariableBool showInfo = new NetworkVariableBool(new NetworkVariableSettings{WritePermission = NetworkVariablePermission.ServerOnly,ReadPermission = NetworkVariablePermission.Everyone}, true);
        string ip = "127.0.0.1";
        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                
                ip = GUILayout.TextField(ip,15);
                NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = ip;
                
                StartButtons();
            }else
            {
                StatusLabels();
                if(showInfo.Value)Info();
                //SubmitNewPosition();
                if(NetworkManager.Singleton.IsServer && showInfo.Value)GameStart();
            }

            GUILayout.EndArea();
        }

        static void StartButtons()
        {
            if (GUILayout.Button("Client"))NetworkManager.Singleton.StartClient();
            if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
            if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
        }

        static void StatusLabels()
        {
            var mode = NetworkManager.Singleton.IsHost ?
                "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";
            GUILayout.Label("Mode: " + mode);
        }
        static void Info(){
            GUILayout.Label("等待遊戲主持人開始遊戲...");
        }

        static void GameStart(){
            if (GUILayout.Button("Start")){
                GameObject.Find("ConnectionUI").GetComponent<Connection>().showInfo.Value = false;
                Debug.Log("start");
                GameObject.Find("GameManager").GetComponent<GameManager>().gamestart();
                //可以在這裡呼叫 game start blabla
            }
        }

        /*
        static void SubmitNewPosition()
        {
            if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change"))
            {
                if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId,
                    out var networkedClient))
                {
                    var player = networkedClient.PlayerObject.GetComponent<HelloWorldPlayer>();
                    if (player)
                    {
                        player.Move();
                    }
                }
            }
        }
        */
    }
}