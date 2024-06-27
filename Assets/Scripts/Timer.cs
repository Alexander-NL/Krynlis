using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;
    float elapsedTime1;

    public GameObject pause1;
    public bool Paused;
    public CanvasGroup canvasGroup;
    public float duration = 2.0f;

    public Movement mov;
    
    private float fastestTime;
    
    void Start(){
        pause1.SetActive(false);
        StartCoroutine(TimerForPause());
        mov.enabled = false;

        fastestTime = PlayerPrefs.GetFloat("FastestTime", float.MaxValue);
    }

    void Update(){
        elapsedTime1 += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime1 / 60);
        int seconds = Mathf.FloorToInt(elapsedTime1 % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        timerText.text = elapsedTime1.ToString("F1") + "s";

        if(Input.GetKeyDown(KeyCode.Escape)){
            if(Paused){
                Resume();
            }
            else{
                PauseGame();
            }
        }
    }

    public void PauseGame(){
        pause1.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }

    public void Resume(){
        pause1.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    } 

    public void GoToMainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator TimerForPause()
    {
        float startAlpha = 1.0f;
        float endAlpha = 0.0f;

        canvasGroup.alpha = startAlpha;

        while (elapsedTime < duration){
            elapsedTime1 = 0.0f;
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
        mov.enabled = true;
    }

    public void StopTimer()
    {
        if (elapsedTime1 < fastestTime)
        {
            fastestTime = elapsedTime1;
            PlayerPrefs.SetFloat("FastestTime", fastestTime);
            PlayerPrefs.Save();
        }
    }
}
