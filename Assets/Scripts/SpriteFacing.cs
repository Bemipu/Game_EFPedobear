using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class SpriteFacing : NetworkBehaviour
{
    private Transform cam_pos;
    public GameObject target = null;

    void Start(){
        cam_pos = new GameObject().transform;
        target = null;
        //if(IsServer)target = GameObject.Find("MainCamera");
    }

    void Update()
    {
        if(target != null){
            //cam_pos = target.transform; //legacy
            cam_pos.position = new Vector3(target.transform.position.x,transform.position.y,target.transform.position.z);
            transform.LookAt(cam_pos);
        }else{
            if(GameObject.Find("GameManager").GetComponent<playerlist>().lastplayer != null){
                target = GameObject.Find("GameManager").GetComponent<playerlist>().lastplayer;
            }
        }
    }
    
    
}
