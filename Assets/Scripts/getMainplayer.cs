using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class getMainplayer : NetworkBehaviour
{
    public GameObject mainplayer = null;

    public void getTarget(GameObject starget){
        mainplayer = starget;
    }
}
