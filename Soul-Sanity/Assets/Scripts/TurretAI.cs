using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretAI : MonoBehaviour
{
    public float Range = 10f;
    public Transform Target;
    public Vector2 Direction;
    [Space]
    public float FireRate;
    public GameObject Bullet;
    public GameObject Parent;
    public Transform Point;
    public float Force;
    [Space]
    public GameObject AlarmLight;
    public GameObject FireLight;
    [Space]
    public GameObject Laser;
    public LineRenderer renderer_;
    [Space]
    [Header("States")]
    [SerializeField] bool detected_player = false;

    float Angle;
    float TimeToFire = 2;
    float WarningTime = 2;

    public void Update(){
        Vector2 targetPos = Target.position;
        Direction = targetPos - (Vector2)transform.position;

        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, Range);

        if(rayInfo){
            if(rayInfo.collider.gameObject.tag == "Player"){
                if(detected_player == false){
                    detected_player = true;
                    AlarmLight.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
        } else {
            if(detected_player == true){
                detected_player = false;
                AlarmLight.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        if(detected_player){
            if(WarningTime <= 0){
                if(Time.time > TimeToFire){
                    TimeToFire = Time.time + 2.3f / FireRate;
                    Shoot();
                }
            } else {
                WarningTime -= Time.deltaTime;
            }
        } else if(!detected_player && WarningTime <= 0){
            WarningTime = 2;
        }

        // LASER
        RaycastHit2D hit = Physics2D.Raycast(Point.transform.position, (Vector2)Direction);
        if(detected_player){
            renderer_.gameObject.SetActive(true);
            renderer_.SetPosition(0, Laser.transform.position);
            renderer_.SetPosition(1, hit.point);
        } else {
            renderer_.gameObject.SetActive(false);
        }
    }

    public void Shoot(){
        GameObject bullet = Instantiate(Bullet, Point.position, Quaternion.identity);
        bullet.transform.parent = Parent.transform;
        bullet.GetComponent<Rigidbody2D>().AddForce(Direction * Force);

    }

    public void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}
