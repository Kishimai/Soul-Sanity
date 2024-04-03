using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogManager : MonoBehaviour
{
    public List<GameObject> FogOfWar = new List<GameObject>();
    
    public void ChangeFogLocation(GameObject _fog){
        foreach(GameObject Object in FogOfWar){
            if(Object != _fog){
                Object.SetActive(false);
            } else {
                Object.SetActive(true);
            }
        }
    }
}
