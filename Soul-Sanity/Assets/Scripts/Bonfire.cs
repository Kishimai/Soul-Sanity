using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public bool playerClose = false;
    public GameManager manager;
    [Space]
    public string Id;

    GameObject playerObject;

    void UpdateBonfire() {
        if(manager.lastSavedId == Id){
            transform.parent.GetChild(2).transform.gameObject.SetActive(true);
            if(manager.LastSavedPos == null){
                manager.LastSavedPos = transform.parent.GetChild(1).transform;
            }
        } else if(manager.lastSavedId != Id){
            transform.parent.GetChild(2).transform.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerClose == true && Input.GetKeyDown(KeyCode.E)){
            transform.parent.GetChild(2).transform.gameObject.SetActive(true);
            manager.LastSavedPos = transform.parent.GetChild(1).transform;
            manager.lastSavedId = Id;
            playerObject.GetComponent<PlayerController>().Health.ResetHealth();
        }

        UpdateBonfire();
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.gameObject.tag == "Player"){
            playerClose = true;
            playerObject = other.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D other) {
        if(other.transform.gameObject.tag == "Player" && playerClose){
            playerClose = false;
            playerObject = null;
        }
    }
}
