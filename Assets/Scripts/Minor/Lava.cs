using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public float Damage = 10;
    public PlayerController Player;
    public bool IsInLava = false;

    private void Update() {
        if (IsInLava) {
            Player.Health.TakeDamage(Damage);
        }
    }

    public void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            IsInLava = false;
            Debug.Log("is out of lava");
        }
        Debug.Log("something in lava"); 

    }
    public void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            IsInLava = true;
            Debug.Log("is in lava");

        }
        Debug.Log("something in lava"); 
    }
}
