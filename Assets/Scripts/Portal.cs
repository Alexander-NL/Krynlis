using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public Timer whygameprog;
    public string sceneName;
    private bool trigger;
    [SerializeField] private bool TIMERTIMERTIMERTIMERTIMER;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(TIMERTIMERTIMERTIMERTIMER == true){
                whygameprog.StopTimer();
            }
            trigger = true;
        }
        
    }
    
    void Update(){
        if(trigger == true){
            SceneManager.LoadScene(sceneName);
        }
    }
}
