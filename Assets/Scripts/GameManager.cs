using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

public class GameManager : NetworkBehaviour
{
    public float pbSpawnRate = 10f;
    public GameObject pbPrefab;
    public List<Transform> pbSP;
    public List<GameObject> pb;
    private bool begin;
    private float gameRoundTimer,respawnTimer,speedUpTimer;
    private int livePlayer;
    private int pbSpawned;
    private List<bool> alive;
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
                speedUpTimer += Time.deltaTime;
                if(livePlayer <=1){                 // end game
                    for(int i=0;i<10;i++){
                        pb[i].GetComponent<AICharacterControl>().enabled = false;
                        pb[i].GetComponent<NavMeshAgent>().enabled = false;
                        pb[i].transform.position = new Vector3(-4+i,-210,0);
                        pb[i].GetComponent<ThirdPersonCharacter>().m_MoveSpeedMultiplier=1f;
                    }
                    begin = false;
                    int aliveID = 0;
                    for(int i=0;i<this.gameObject.GetComponent<playerlist>().playermax;i++){
                        if(alive[i] == true)aliveID=i+1;
                    }
                    GameObject.Find("ConnectionUI").gameObject.GetComponent<Connection.Connection>().WinnerID.Value=aliveID;
                    wipeout(aliveID);
                    
                }
            }
            if(respawnTimer > pbSpawnRate && pbSpawned < 10){
                respawnTimer = 0f;
                pbSpawn(pbSpawned);
                pbSpawnClientRpc(pbSpawned);
                pbSpawned++;
            }
            if(speedUpTimer > pbSpawnRate){
                speedUpTimer = 0f;
                for(int i=0;i<10;i++){
                    pb[i].GetComponent<ThirdPersonCharacter>().m_MoveSpeedMultiplier+=0.1f;
                }
            }
        }
    }

    public void gamestart(){
        alive = new List<bool>();
        for(int i=0;i<this.gameObject.GetComponent<playerlist>().playermax;i++){  // turn on player's camera
            this.gameObject.GetComponent<playerlist>().plistGO[i].GetComponent<NetworkCam>().TOCamClientRpc();
            this.gameObject.GetComponent<playerlist>().plistGO[i].GetComponent<NetworkCam>().MoveToClientRpc(new Vector3(0, 10, 0));
            alive.Add(true);
        }
        livePlayer = this.gameObject.GetComponent<playerlist>().playermax;
        gameRoundTimer = 0f;
        respawnTimer = 0f;
        speedUpTimer = 0f;
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
        alive[ID-1] = false;
        livePlayer--;
        this.gameObject.GetComponent<playerlist>().plistGO[ID-1].GetComponent<NetworkCam>().TOFCamClientRpc();
        this.gameObject.GetComponent<playerlist>().plistGO[ID-1].GetComponent<NetworkCam>().MoveToClientRpc(new Vector3(0, -220, 0));
    }
}