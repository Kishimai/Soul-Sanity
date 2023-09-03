using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject Follow;
    public GameObject player;
    [Space]
    public float MaxHealth = 50f;
    public float Health = 50;

    public void Update(){
        this.transform.position = Follow.transform.position; // Setting the Position to the Top of the head

        transform.GetChild(0).GetComponent<Slider>().value = Health;
        if(transform.GetChild(0).GetComponent<Slider>().maxValue != MaxHealth){ // Set Max value of the Slider to the Max Health
            transform.GetChild(0).GetComponent<Slider>().maxValue = MaxHealth;
        }

        if(Health <= 0){
            Follow.transform.parent.GetComponent<PlayerController>().Died();
        }
    }

    public void TakeDamage(float damage){
        Health -= damage; // take the Damage

    }

    public void ResetHealth(){
        Health = MaxHealth;
    }

    public static void Heal(int healAmount){
        
    }
}
