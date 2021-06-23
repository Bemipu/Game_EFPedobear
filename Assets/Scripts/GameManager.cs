using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

public class GameManager : NetworkBehaviour
{
    public GameObject pbPrefab;
    public List<Transform> pbSP;
    public List<GameObject> pb;
    private bool begin;
    private float gameRoundTimer,respawnTimer;
    private int livePlayer;
    private int pbSpawned;
    void Start()
    {
        begin = false;
        
        for(int i=0;i<10;i++){
            
            pb[i].GetComponent<AICharacterControl>().enabled = false;
            pb[i].GetComponent<NavMeshAgent>().enabled = false;
            
            //pb[i].GetComponents<CapsuleCollider>()[0].enabled = false;
            //pb[i].GetComponents<CapsuleCollider>()[1].enabled = false;
            //pb[i].GetComponentInChildren<SpriteFacing>().enabled = false;
            //pb[i].GetComponentInChildren<PedoAnim>().enabled = false;
            //pb[i].GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsServer){
            if(begin){
                gameRoundTimer += Time.deltaTime;
                respawnTimer += Time.deltaTime;
            }
            if(respawnTimer > 5f){
                respawnTimer = 0f;
                pbSpawn(pbSpawned);
                pbSpawnClientRpc(pbSpawned);
                pbSpawned++;
            }
        }
    }

    public void gamestart(){
        for(int i=0;i<this.gameObject.GetComponent<playerlist>().playermax;i++){  // turn on player's camera
            this.gameObject.GetComponent<playerlist>().plistGO[i].GetComponent<NetworkCam>().TOCamClientRpc();
            this.gameObject.GetComponent<playerlist>().plistGO[i].GetComponent<NetworkCam>().MoveToClientRpc(new Vector3(0, 10, 0));
        }
        livePlayer = this.gameObject.GetComponent<playerlist>().playermax;
        gameRoundTimer = 0f;
        respawnTimer = 0f;
        pbSpawned = 0;
        begin = true;
    }

    private void pbSpawn(int id){
        pb[id].transform.position = pbSP[0].transform.position;
        
        pb[id].GetComponent<AICharacterControl>().enabled = true;
        pb[id].GetComponent<NavMeshAgent>().enabled = true;
    }

    [ClientRpc]
    private void pbSpawnClientRpc(int id){
        pb[id].transform.position = pbSP[0].transform.position;
        
        pb[id].GetComponent<AICharacterControl>().enabled = true;
        pb[id].GetComponent<NavMeshAgent>().enabled = true;
    }


    public void wipeout(int ID){
        livePlayer--;
        this.gameObject.GetComponent<playerlist>().plistGO[ID-1].GetComponent<NetworkCam>().TOFCamClientRpc();
        this.gameObject.GetComponent<playerlist>().plistGO[ID-1].GetComponent<NetworkCam>().MoveToClientRpc(new Vector3(0, -20, 0));
    }
}
