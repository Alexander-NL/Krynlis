using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    private Rigidbody2D body;
    private bool isGrounded;

    private bool isDashing;
    private float dashTimeLeft;
    private float lastDashTime;

    private void Awake(){
        body = GetComponent<Rigidbody2D>();
    }

    private void Update(){
        body.velocity  =  new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        if(Input.GetKey(KeyCode.Space) && isGrounded){
            body.velocity = new Vector2(body.velocity.x, speed);
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
                isDashing = true;
                dashTimeLeft = dashDuration;
                lastDashTime = Time.time;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.CompareTag("Ground")){
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        if(collision.collider.CompareTag("Ground")){
            isGrounded = false;
        }
    }
}
