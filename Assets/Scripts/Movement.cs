using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    private Rigidbody2D body;
    private Animator anim;
    public AudioSource PlayerSound;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool WallJump =  false;
    [SerializeField] private bool HasGrounded = false;
    [SerializeField] private bool WallHugged = false;
    [SerializeField] private bool InMenu;

    private bool isDashing;
    private bool isMoving;
    private float dashTimeLeft;
    private float lastDashTime;

    void Start(){
        Time.timeScale = 1f;
        if(InMenu == true){
            speed = 0;
        }
    }

    private void Awake(){
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Jump(){
        body.velocity = new Vector2(body.velocity.x, jumpForce);
    }

    private void Update(){
        body.velocity  =  new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        if (body.velocity.x > 0.01f){
            transform.localScale = Vector3.one;
            if (!isMoving){
                PlayerSound.Play(); 
                isMoving = true;
            }
        }else if (body.velocity.x < -0.01f){
            transform.localScale = new Vector3(-1, 1, 1);
            if (!isMoving){
                PlayerSound.Play(); 
                isMoving = true;
            }
        }else{
            isMoving = false;
            PlayerSound.Stop();
        }    

        anim.SetBool("Run", body.velocity.x != 0);
        anim.SetBool("Grounded", isGrounded);
        anim.SetBool("HasDashed", isDashing);
        anim.SetBool("WallHugged", WallHugged);

        if(Input.GetKey(KeyCode.Space) && isGrounded){
            Jump();
            anim.SetTrigger("Jump");
        }
        if(Input.GetKey(KeyCode.Space) && WallJump && HasGrounded){
            Jump();
            HasGrounded = false;
            WallJump = false;
            anim.SetTrigger("Jump");
        }


        if (isDashing){
            body.velocity = new Vector2(dashSpeed * (body.velocity.x > 0 ? 1 : -1), body.velocity.y);
            dashTimeLeft -= Time.deltaTime;

            if (dashTimeLeft <= 0)
            {
                isDashing = false;
            }
        }
        else{
            if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= (lastDashTime + dashCooldown))
            {
                anim.SetTrigger("Dash");
                isDashing = true;
                dashTimeLeft = dashDuration;
                lastDashTime = Time.time;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.CompareTag("Ground")){
            isGrounded = true;
            HasGrounded = true;
            speed = 5;
            WallHugged = false;
            InMenu = false;
        }
        if(collision.collider.CompareTag("Slope")){
            isGrounded = true;
            HasGrounded = true;
            speed = 5;
            WallHugged = false;
        }
        if(collision.collider.CompareTag("Wall") && HasGrounded == true && isGrounded == false){
            WallJump = true;
            WallHugged = true;
            speed = 7;
            anim.SetTrigger("Climb");
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        if(collision.collider.CompareTag("Ground")){
            isGrounded = false;
            WallHugged = false;
        }
        if(collision.collider.CompareTag("Wall")){
            WallJump = false;
            WallHugged = false;
        }
    }
}
