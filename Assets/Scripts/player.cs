using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public int myID;
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        myID = GameObject.Find("GameManager").GetComponent<playerlist>().join(camera);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
