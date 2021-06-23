using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class NetworkCam : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.parent.gameObject.transform.parent.GetChild(0).gameObject.transform.position = new Vector3(0, -20, 0);
        //testing
        //Debug.Log(NetworkManager.Singleton.LocalClientId);
        //NetworkManager.Singleton.ConnectedClients[NetworkManager.Singleton.LocalClientId].PlayerObject.transform.position = new Vector3(10,10,10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ClientRpc]
    public void TOCamClientRpc(){
        if(IsLocalPlayer){
            this.GetComponent<Camera>().enabled = true;
            this.GetComponent<AudioListener>().enabled = true;
        }
    }

    [ClientRpc]
    public void TOFCamClientRpc(){
        if(IsLocalPlayer){
            this.GetComponent<Camera>().enabled = false;
            this.GetComponent<AudioListener>().enabled = false;
        }
    }

    [ClientRpc]
    public void MoveToClientRpc(Vector3 newposition){
        if(IsLocalPlayer){
            this.transform.parent.gameObject.transform.parent.GetChild(0).gameObject.transform.position = newposition;
        }
    }



}
