using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    Vector2 startPos;
    SpriteRenderer spriteRenderer;

    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start(){
        startPos =  transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Death")){
            Die();
        }
    }

    void Die(){
        StartCoroutine(UnoReverseCard(0.1f));
    }

    IEnumerator UnoReverseCard(float duration){
        yield return new WaitForSeconds(duration);
        transform.position = startPos;
    }
}
