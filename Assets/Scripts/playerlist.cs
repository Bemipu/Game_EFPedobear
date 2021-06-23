using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
public class playerlist : NetworkBehaviour
{
    public int playermax;
    public List<GameObject> plistGO = new List<GameObject>();
    public GameObject lastplayer;
    void Start()
    {
        playermax=0;
        lastplayer = null;
    }

    void Update()
    {
        
    }

    public int join(GameObject camera){
        playermax++;
        Debug.Log("player " + playermax+" joined.");
        plistGO.Add(camera);
        
        
        lastplayer = plistGO[playermax-1];
        

        return playermax;
    }
}
