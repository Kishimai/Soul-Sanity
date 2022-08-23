using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBotTest : MonoBehaviour
{

    private Transform positionPlayer;
    public float speedBoss;
    
    void Start()
    {
        positionPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if(positionPlayer.gameObject !=null)
        {
        transform.position =  Vector2.MoveTowards(transform.position, positionPlayer.position,speedBoss * Time.deltaTime);
        }
        //put a limit on the search for the player

        
    }
}
