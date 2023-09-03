using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TitleTrigger : MonoBehaviour
{
    public TMP_Text Text;
    public Animator Animation;


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            Animation.SetBool("Start_Animation", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            Animation.SetBool("Start_Animation", false);
        }
    }

    
}
