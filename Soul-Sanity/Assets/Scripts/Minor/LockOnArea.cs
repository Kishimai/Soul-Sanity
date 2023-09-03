using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnArea : MonoBehaviour
{
    public Camera _cam;
    public float AreaZoom; // How much zone will be in the area
    public GameObject AreaPin; // where the Camera look at
    [Space]
    public PlayerController Player;

    private float SavedZoom; // the saved zoom of the player
    private bool ZoomingOut = false;


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerController>().cameraTarget = AreaPin;
            SavedZoom = other.gameObject.GetComponent<PlayerController>().mainCamera.orthographicSize;
            other.gameObject.GetComponent<PlayerController>().mainCamera.orthographicSize = AreaZoom;
            Player.CanZoom = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerController>().cameraTarget = other.gameObject;
            other.gameObject.GetComponent<PlayerController>().mainCamera.orthographicSize = SavedZoom;
            Player.CanZoom = true;
        }
    }

    public void Update() {
        
    }
}
