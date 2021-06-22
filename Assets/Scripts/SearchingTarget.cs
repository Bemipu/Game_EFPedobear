using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class SearchingTarget : MonoBehaviour
{
    public GameObject PlayerlistGO;
    void Start()
    {
        PlayerlistGO = GameObject.Find("PlaylistGO");
    }

    // Update is called once per frame
    void Update()
    {
        float dist = 999;
        int chasing = 0;
        for(int i=0;i<PlayerlistGO.GetComponent<playerlist>().playermax;i++){
            if(PlayerlistGO.GetComponent<playerlist>().plistGO[i] != null){
                float pbd = Vector3.Distance (this.transform.position, PlayerlistGO.GetComponent<playerlist>().plistGO[i].transform.position);
                if(dist > pbd){
                    dist = pbd;
                    this.GetComponent<AICharacterControl>().target = PlayerlistGO.GetComponent<playerlist>().plistGO[i].transform;
                    chasing = i;
                }
            }
        }
    }
}
