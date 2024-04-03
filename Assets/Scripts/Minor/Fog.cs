using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public FogManager manager;

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            manager.ChangeFogLocation(this.gameObject);
        }
    }
}
