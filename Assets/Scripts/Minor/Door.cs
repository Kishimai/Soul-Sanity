using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public PlayerController player;
    public bool isGroound = false; // states that the "Door" is a lock for the ground

    public void InteractedWith(){
        // first Check if what type of door it is (platform door or normal door)
        // Check if player is on left or right or if platform, if lower then the door or higher
        // If player is on left then transfer on right, vise-versa same with the platform door
    }
}
