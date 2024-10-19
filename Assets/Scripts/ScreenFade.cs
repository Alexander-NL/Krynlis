using UnityEngine;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    public Animator objectAnimator;
    public GameObject[] objectsToDeactivate;
    public GameObject[] objectToActive;
    public float delayBeforeDeactivation = 2f;
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other){
        if (!hasTriggered && other.CompareTag("Player")){
            objectAnimator.Play("FadeIn");
            hasTriggered = true;
            
            StartCoroutine(DeactivateObjectsWithDelay());
        }
    }

    private IEnumerator DeactivateObjectsWithDelay(){
        yield return new WaitForSeconds(delayBeforeDeactivation);

        foreach (GameObject obj in objectsToDeactivate){
            obj.SetActive(false);
        }
        foreach (GameObject obj in objectToActive){
            obj.SetActive(true);
        }
    }
}
