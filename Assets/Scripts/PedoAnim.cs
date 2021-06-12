using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedoAnim : MonoBehaviour
{
    float timer = 0.0f;
    void Update()
    {
        timer += Time.deltaTime*2;

        if((int)timer%2==0){
            this.GetComponent<SpriteRenderer>().flipX=true;
        }else{
            this.GetComponent<SpriteRenderer>().flipX=false;
        }
    }
}
