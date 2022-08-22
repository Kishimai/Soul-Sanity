using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public int Damage = 20;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerController>().Health.TakeDamage(Damage);
            Destroy(this.gameObject);
        } else{
            Destroy(this.gameObject);
        }
    }
}
