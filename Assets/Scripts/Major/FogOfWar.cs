using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public List<GameObject> Fogs = new List<GameObject>();

    public void ChangeFog(GameObject ActivateFog){
        foreach(GameObject fog in Fogs){
            if(fog != ActivateFog){
                fog.SetActive(true);
            } else {
                fog.SetActive(false);
            }
        }
    }
}
