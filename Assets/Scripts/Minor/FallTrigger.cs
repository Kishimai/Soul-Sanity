using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    public FallingSpike fallingSpike;
    public void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            fallingSpike.gameObject.GetComponent<Rigidbody2D>().simulated = true;
        }
    }
}
