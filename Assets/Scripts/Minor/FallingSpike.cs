using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    public bool PlayerOnTrigger = false;
    public float Damage = 100;
    public float KnockBack = 8;
    public PlayerHealth health;

    public void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            health.TakeDamage(Damage);
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up *+ KnockBack, ForceMode2D.Impulse);
        }
    }
}
