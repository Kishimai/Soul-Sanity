
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour{
    public int TopDamage; // variable used to damage monsters whenever hit on its head
    public int Damage;
    [Space]
    [SerializeField] float maxSpeed = 3.4f;
    [SerializeField] float slowedSpeed = 1.3f;
    [SerializeField] float maxVelocitySpeed = 9.8f;
    [SerializeField] float jumpHeight = 6.5f;
    [SerializeField] float gravityScale = 1.5f;
    [SerializeField] float climbingSpeed = 2f;
    [SerializeField] float playerInteractRange = 10f;
    [Space]
    public GameObject cameraTarget;
    public Camera mainCamera;
    public float smoothlerp = 1;
    public float CameraMaxZoom, CameraLowestZoom;
    [Space]
    public bool facingRight = true;
    [Space] // Variables
    public GameManager Manager;
    public FogOfWar fogManager;
    public PlayerHealth Health;
    public Rigidbody2D rig2d;
    public BoxCollider2D mainCollider;
    public Animator playerAnim;
    public GameObject DeathObj;
    public Transform playerChecker;
    public SoundSystem soundSystem;
    [Space] // Players output
    Vector3 cameraPos;
    public bool isGrounded = false;
    public bool CanClimb = false;
    public bool IsSlowed = false;
    public bool CanZoom = true;
    [Space]
    public float moveDirection = 0; // -1 or 1
    public float Noise = 0f; // out of 100
    [Space]
    public List<string> UnderCollisions = new List<string>(); // under the player collisions
    [Space] // Death Funcitons
    GameObject CurrentSoul; // Current soul that is in session
    float lastDirection;

    void Start()
    {
        rig2d.freezeRotation = true;
        rig2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rig2d.gravityScale = gravityScale;
        facingRight = transform.localScale.x > 0;

        cameraPos.z = -1;
        mainCamera.orthographicSize = 8f;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O)){
            playerAnim.SetBool("SitSip", true);
        }

        if(rig2d.velocity.magnitude > maxVelocitySpeed){
            rig2d.velocity = Vector2.ClampMagnitude(rig2d.velocity, maxVelocitySpeed);
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))){
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
            lastDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
            playerAnim.SetBool("IsRunning", true);
            playerAnim.SetBool("SitSip", false);
        }
        else{
            if (isGrounded || rig2d.velocity.magnitude < 0.01f){
                moveDirection = 0;
                playerAnim.SetBool("IsRunning", false);
            }
        }
        
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) && moveDirection != 0){
            moveDirection = 0;
        }

        if (moveDirection != 0){
            if (moveDirection > 0 && !facingRight){
                facingRight = true;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight){
                facingRight = false;
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !CanClimb && UnderCollisions != null){
            if(UnderCollisions.Contains("Floor") || UnderCollisions.Contains("PassableFloor")){
                rig2d.velocity = new Vector2(rig2d.velocity.x, jumpHeight);
                soundSystem.PlaySoundEffect(0);
            }
        }

        playerAnim.SetBool("OnAir", !isGrounded);

        // Zoom restriciton
        if (CanZoom == true) {
            if(Input.mouseScrollDelta.y > 0){
                // Going Up
                if(mainCamera.orthographicSize < CameraMaxZoom){
                    mainCamera.orthographicSize += 0.3f;
                }
            } else if(Input.mouseScrollDelta.y < 0){
                // Going Down
                if(mainCamera.orthographicSize > CameraLowestZoom){
                    mainCamera.orthographicSize -= 0.3f;
                }
            }
        }

        // Attack Input
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            Interact(KeyCode.Mouse0);
        }

        if(Input.GetKeyDown(KeyCode.E)){
            Interact(KeyCode.E);
        }
        
    }

    void FixedUpdate()
    {
        if(isGrounded == false || IsSlowed && UnderCollisions.Contains("Floor")){
            isGrounded = true;
            if(IsSlowed == true) IsSlowed = false;
        }

        // Apply movement velocity 
        if(IsSlowed == false){
            rig2d.velocity = new Vector2((moveDirection) * maxSpeed, rig2d.velocity.y);
            //transform.Translate(new Vector2(moveDirection, 0) * maxSpeed * Time.deltaTime);
        } else{
            rig2d.velocity = new Vector2((moveDirection) * slowedSpeed, rig2d.velocity.y);
            //transform.Translate(new Vector2(moveDirection, 0) * maxSpeed * Time.deltaTime);
        }

        if (mainCamera){ // Camera Pos
            cameraPos = mainCamera.transform.position;
        }

        // Camera follow
        if (mainCamera){
            Vector3 smoothedPosition = Vector3.Lerp(cameraPos, new Vector3(cameraTarget.transform.position.x, cameraTarget.transform.position.y, -1), smoothlerp);
            mainCamera.transform.position = smoothedPosition;
        }

        if(CanClimb){
            if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)){
                rig2d.AddForce(Vector2.up * climbingSpeed * Time.deltaTime, ForceMode2D.Impulse);
                Debug.Log("Climbing");
            }
        }
    }

    public void Died(){
        soundSystem.PlaySoundEffect(1);
        if(CurrentSoul != null){
            Destroy(CurrentSoul);
            var Soul = Instantiate(DeathObj, this.transform.position, Quaternion.identity);
            Soul.transform.localScale = new Vector3(1, 1, 1);
            Soul.transform.GetChild(0).GetComponent<Soul>().SoulHave = GameManager.TotalScore;
            CurrentSoul = Soul;
        } else {
            var Soul = Instantiate(DeathObj, this.transform.position, Quaternion.identity);
            Soul.transform.localScale = new Vector3(1, 1, 1);
            Soul.transform.GetChild(0).GetComponent<Soul>().SoulHave = GameManager.TotalScore;
            CurrentSoul = Soul;
        }

        Health.ResetHealth();
        this.transform.position = Manager.LastSavedPos.transform.position;
        GameManager.TotalScore = 0;
    }

    public void Interact(KeyCode key){ // Registers an interact/attack on nearby interactables.
        soundSystem.PlaySoundEffect(2);
        Collider2D[] objectInRange = Physics2D.OverlapCircleAll(transform.position, playerInteractRange);
        foreach(Collider2D collidersDetected in objectInRange){
            if(collidersDetected.transform.position.x > this.transform.position.x && lastDirection == 1 || collidersDetected.transform.position.x < this.transform.position.x && lastDirection == -1){ // On the Right
                if(key == KeyCode.Mouse0){
                    GameObject Enemy = collidersDetected.gameObject;
                    if(Enemy.tag == "Enemy"){
                        if(Enemy.GetComponent<Slime>()){
                            Enemy.GetComponent<Slime>().health -= Damage;
                            if(lastDirection == -1){
                                Enemy.GetComponent<Rigidbody2D>().AddForce(-Vector2.right * 500, ForceMode2D.Impulse);
                                Enemy.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300, ForceMode2D.Impulse);
                            } else if(lastDirection == 1){
                                Enemy.GetComponent<Rigidbody2D>().AddForce(-Vector2.left * 500, ForceMode2D.Impulse);
                                Enemy.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300, ForceMode2D.Impulse);
                            }
                        }
                    }
                } else if(key == KeyCode.E){
                    GameObject reached = collidersDetected.gameObject;
                    if(reached.tag == "Door"){
                        if(reached.GetComponent<Door>()){
                            reached.GetComponent<Door>().InteractedWith();
                        }
                    }
                }
            }
        }
    }
    
    #region CHECKS

    // CHECKINGS
    public void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.transform.position.y < playerChecker.transform.position.y){ // under the player checker
            UnderCollisions.Add(other.gameObject.tag);
        }

        if(other.gameObject.tag == "PassableFloor" && other.gameObject.transform.position.y > playerChecker.transform.position.y){
            other.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    public void OnCollisionExit2D(Collision2D other) {
        UnderCollisions.Remove(other.gameObject.tag);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Vine"){
            CanClimb = true;
            isGrounded = false;
        }

        if(other.gameObject.tag == "Fog"){
            fogManager.ChangeFog(other.gameObject);
            Debug.Log("ChangedFog" + other.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Vine"){
            CanClimb = false;
        }

        if(other.gameObject.tag == "PassableFloor"){
            other.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    #endregion
}