using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public PlayerController player;
    public bool isGroound = true; // states that it is horizontal if true and vertical if false

    public void InteractedWith(){
        // first Check if what type of door it is (platform door or normal door)
        if(isGroound == true){
            // Check if player is on left or right or if platform, if lower then the door or higher
            if(player.gameObject.transform.position.x > this.transform.position.x){
                // If player is on left then transfer on right, vise-versa same with the platform door
                player.transform.position = new Vector2(this.transform.position.x - 5, this.transform.position.y - 2);
            } else if(player.gameObject.transform.position.x < this.transform.position.x){
                // If player is on left then transfer on right, vise-versa same with the platform door
                player.transform.position = new Vector2(this.transform.position.x + 5, this.transform.position.y - 2);
            }
        } else {
            
        }
    }
}