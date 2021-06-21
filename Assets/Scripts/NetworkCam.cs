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
        GameObject.Find("GetTarget").GetComponent<getMainplayer>().getTarget(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
