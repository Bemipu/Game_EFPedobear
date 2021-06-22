using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;

public class GrapplingGun : NetworkBehaviour
{

    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    public Rigidbody player_rb;
    private float maxDistance = 100f;
    private SpringJoint joint;
    private float nowTime;
    private float time_s = 0f;
    private bool isGrappling;
    public float duration = 5f;

    /*
    NetworkVariable<LineRenderer> nlr = new NetworkVariable<LineRenderer>(new NetworkVariableSettings{WritePermission = NetworkVariablePermission.Everyone});
    //grapplePoint, gunTip.position, currentGrapplePosition
    NetworkVariableVector3 gP = new NetworkVariableVector3(new NetworkVariableSettings{WritePermission = NetworkVariablePermission.OwnerOnly});
    NetworkVariableVector3 gT = new NetworkVariableVector3(new NetworkVariableSettings{WritePermission = NetworkVariablePermission.OwnerOnly});
    NetworkVariableVector3 cGP = new NetworkVariableVector3(new NetworkVariableSettings{WritePermission = NetworkVariablePermission.OwnerOnly,});
    */

    private void Start()
    {
        isGrappling = false;
    }

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        TimerForGrapple();

        if (isGrappling && IsLocalPlayer){
            player_rb.AddForce(this.transform.forward * Time.deltaTime * 1000);
            player_rb.AddForce(camera.forward * Time.deltaTime * 0.5f);
        }
        
        if(IsLocalPlayer){
            if (time_s >= duration) {
                StopGrapple();
            }
            if (Input.GetMouseButtonDown(0))
            {
                StartGrapple();
                
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopGrapple();
                
            }
        }
    }

    //Called after Update
    void LateUpdate()
    {
        if(IsLocalPlayer)DrawRope();
    }

    /*
    [ServerRpc]
    void SGServerRpc(){
        cGP.Value = Vector3.Lerp(cGP.Value, gP.Value, Time.deltaTime * 8f);

        nlr.Value.SetPosition(0, gT.Value);
        nlr.Value.SetPosition(1, cGP.Value);
    }

    void STGServerRpc(){
        nlr.Value.positionCount = 0;
    }

    */

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            isGrappling = true;

            grapplePoint = hit.point;//
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values to fit your game.
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;//

            /*
            gP.Value = grapplePoint; gT.Value = gunTip.position; cGP.Value = currentGrapplePosition;
            SGServerRpc();
            */

        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
        isGrappling = false;
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

    public void TimerForGrapple()
    {
        if (!isGrappling)
        {
            time_s = 0f;
        }
        else
        {
            time_s += Time.deltaTime;
        }

    }

}