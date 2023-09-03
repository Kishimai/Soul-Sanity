using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    public int SoulHave;
    public bool PlayerNear = false;

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            PlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            PlayerNear = false;
        }
    }

    public void Update(){
        if(PlayerNear == true && Input.GetKeyDown(KeyCode.E)){
            GameManager.TotalScore += SoulHave;
            Destroy(this.transform.parent.gameObject); // Ends
        }
    }
}
