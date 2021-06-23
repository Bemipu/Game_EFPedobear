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
        public NetworkVariableInt WinnerID = new NetworkVariableInt(new NetworkVariableSettings{WritePermission = NetworkVariablePermission.ServerOnly, ReadPermission = NetworkVariablePermission.Everyone}, 0);
        //string Username="User";
        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                
                //Username = GUILayout.TextField(Username,15);
                //ConnectionUI 名字放 gamemanager
                //player prefab 生成自己去抓
                ip = GUILayout.TextField(ip,15);
                NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = ip;
                
                StartButtons();
            }else{
                StatusLabels();
                if(WinnerID.Value != 0){
                    showInfo.Value = true;
                    winner(WinnerID.Value);
                }
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
            GUILayout.Label("等待遊戲主持人開始遊戲...(目前玩家人數:"+ GameObject.Find("GameManager").GetComponent<playerlist>().playermax +")");
        }

        static void winner(int id){
            GUILayout.Label("玩家 " + id + " 存活了下來!");
        }

        static void GameStart(){
            if (GUILayout.Button("Start")){
                GameObject.Find("ConnectionUI").GetComponent<Connection>().showInfo.Value = false;
                Debug.Log("start");
                GameObject.Find("GameManager").GetComponent<GameManager>().gamestart();
                GameObject.Find("ConnectionUI").GetComponent<Connection>().WinnerID.Value = 0;
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