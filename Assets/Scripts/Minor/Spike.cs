using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float DamageMulitplier = 1;

    public void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            if(other.gameObject.transform.position.y > transform.position.y){ // means hes upwards going downwards
                other.gameObject.GetComponent<PlayerController>().Health.TakeDamage(20 * DamageMulitplier);
            } else if(other.gameObject.transform.position.x > transform.position.x){ // right
                other.gameObject.GetComponent<PlayerController>().Health.TakeDamage(10 * DamageMulitplier);
            } else if(other.gameObject.transform.position.x < transform.position.x){ // left
                other.gameObject.GetComponent<PlayerController>().Health.TakeDamage(10 * DamageMulitplier);
            }
        }
    }
}
