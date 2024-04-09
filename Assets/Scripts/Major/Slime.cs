using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public int health = 20;
    public float bounceStrength = 100f;
    public GameObject deathEffect;
    [Space]
    public float speed = 10f;
    public Transform groundDetect, heightDetect;
    public Rigidbody2D Rigid2d;

    private GameObject player;
    // SLIME STATES
    private bool PlayerOnSight = false;
    private bool OnCoolDown = false;
    private bool movingRight = true;
    private bool Patrol = true;


    private void Update() {
        if(health <= 0){
            Died(1);
        }

        if(Patrol == true){ 
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, 0.5f);
            RaycastHit2D frontInfo = Physics2D.Raycast(new Vector2(groundDetect.position.x, groundDetect.position.y + 0.5f), Vector2.right, 0.5f);
            RaycastHit2D heightInfo = Physics2D.Raycast(new Vector2(heightDetect.position.x, heightDetect.position.y), Vector2.down, 3.5f);
            
            if(heightInfo.collider == null){
                Died(0);
            }
            else if(groundInfo.collider == false || groundInfo.transform.gameObject.tag == "Wall"){
                if(movingRight == true){
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = false;
                } else{
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    movingRight = true;
                }
            } 
            else if(frontInfo.collider){
                    if(frontInfo.transform.gameObject.tag == "Design" || frontInfo.transform.gameObject.tag == "Floor"){
                        if(movingRight == true){
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        movingRight = false;
                    } else{
                        transform.eulerAngles = new Vector3(0, 180, 0);
                        movingRight = true;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            player = other.gameObject;
            if(other.gameObject.GetComponent<PlayerController>().playerChecker.transform.position.y > this.transform.position.y - 0.5f){
                if(other.gameObject.GetComponent<PlayerController>().isGrounded == true){
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceStrength);
                    other.gameObject.GetComponent<PlayerController>().IsSlowed = true;
                } else{
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceStrength * 3);
                    other.gameObject.GetComponent<PlayerController>().IsSlowed = true;
                }
                health -= other.gameObject.GetComponent<PlayerController>().TopDamage;
            }
        }
    }

    public void Died(int method){
        var _deathEffect = Instantiate(deathEffect);
        _deathEffect.transform.position = this.transform.position;
        if(method == 1){
            GameManager.ReceiveScore(3);
            player.GetComponent<PlayerController>().Manager.Heal(10);
        }
        player.GetComponent<PlayerController>().soundSystem.PlaySoundEffect(5);
        Destroy(_deathEffect, 10);
        Destroy(this.gameObject);
    }
}
