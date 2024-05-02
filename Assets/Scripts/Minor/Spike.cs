using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float DamageMulitplier = 1;
    private int knockback = 5;

    public void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            if(other.gameObject.transform.position.y > transform.position.y){ // means hes upwards going downwards
                other.gameObject.GetComponent<PlayerController>().Health.TakeDamage(20 * DamageMulitplier);
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * knockback, ForceMode2D.Impulse);
            } else if(other.gameObject.transform.position.x > transform.position.x){ // right
                other.gameObject.GetComponent<PlayerController>().Health.TakeDamage(10 * DamageMulitplier);
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * knockback, ForceMode2D.Impulse);
            } else if(other.gameObject.transform.position.x < transform.position.x){ // left
                other.gameObject.GetComponent<PlayerController>().Health.TakeDamage(10 * DamageMulitplier);
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * knockback, ForceMode2D.Impulse);
            }
        }
    }
}
