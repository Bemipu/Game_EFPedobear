using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class SearchingTarget : MonoBehaviour
{
    public GameObject GameManager;
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        float dist = 999;
        int chasing = 0;
        for(int i=0;i<GameManager.GetComponent<playerlist>().playermax;i++){
            if(GameManager.GetComponent<playerlist>().plistGO[i] != null){
                float pbd = Vector3.Distance (this.transform.position, GameManager.GetComponent<playerlist>().plistGO[i].transform.position);
                if(dist > pbd){
                    dist = pbd;
                    this.GetComponent<AICharacterControl>().target = GameManager.GetComponent<playerlist>().plistGO[i].transform;
                    chasing = i;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            int pID = other.gameObject.transform.parent.GetChild(1).GetChild(0).gameObject.GetComponent<player>().myID;
            GameObject.Find("GameManager").GetComponent<GameManager>().wipeout(pID);
            Debug.Log("Player " + pID + " is wiped out.");
        }
        
    }
}
