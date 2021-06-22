using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class NetworkCam : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(IsLocalPlayer){
            this.GetComponent<Camera>().enabled = true;
            this.GetComponent<AudioListener>().enabled = true;
        }

        //testing
        //Debug.Log(NetworkManager.Singleton.LocalClientId);
        //NetworkManager.Singleton.ConnectedClients[NetworkManager.Singleton.LocalClientId].PlayerObject.transform.position = new Vector3(10,10,10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
