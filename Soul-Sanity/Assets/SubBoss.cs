using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBoss : MonoBehaviour
{
    public Animator anim_manager;

    private void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            anim_manager.SetBool("Spawn_Boss", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            anim_manager.SetBool("Boss_Retreat", true);
        }
    }
}
