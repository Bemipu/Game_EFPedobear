using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class SpriteFacing : NetworkBehaviour
{
    public GameObject source;
    private Transform cam_pos;
    public GameObject target = null;

    void Start(){
        cam_pos = new GameObject().transform;
        target = null;
    }

    void Update()
    {
        if(target != null){
            //cam_pos = target.transform; //legacy
            cam_pos.position = new Vector3(target.transform.position.x,transform.position.y,target.transform.position.z);
            transform.LookAt(cam_pos);
        }else{
            if(source.GetComponent<getMainplayer>().mainplayer != null){
                target = source.GetComponent<getMainplayer>().mainplayer;
            }
        }
    }
    
    
}
