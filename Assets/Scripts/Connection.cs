using MLAPI;
using UnityEngine;
using MLAPI.Transports.UNET;

namespace Connection
{
    public class Connection : MonoBehaviour
    {
        string ip = "127.0.0.1";
        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                ip = GUILayout.TextField(ip,15);
                NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = ip;
                
                StartButtons();
            }

            GUILayout.EndArea();
        }

        static void StartButtons()
        {
            if (GUILayout.Button("Client"))NetworkManager.Singleton.StartClient();
            if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
            if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
        }

    }
}