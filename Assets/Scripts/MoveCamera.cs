using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
public class MoveCamera : NetworkBehaviour
{

    public Transform player;

    void Update()
    {
        transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + 0.3f, player.transform.position.z);
    }
}