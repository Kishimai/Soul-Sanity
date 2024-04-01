using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//thank you for being patient with me ajay
//hello i made this -migs (MINE!!!)
public class DoorSystem : MonoBehaviour
{
    //VARIABLES
    public GameObject door; //door
    public PlayerController player; //player
    public Transform nextPos;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DetectedDoor()
    {
        player.gameObject.transform.position = nextPos.transform.position;
    }
}
