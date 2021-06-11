using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFacing : MonoBehaviour
{

    public Camera cameraToLookAt;
    private Transform cam_pos;

    void Start(){
        cam_pos = new GameObject().transform;
    }

    void Update()
    {
        //cam_pos = cameraToLookAt.transform;
        cam_pos.position = new Vector3(cameraToLookAt.transform.position.x,transform.position.y,cameraToLookAt.transform.position.z);
        transform.LookAt(cam_pos);
    }
    
}
