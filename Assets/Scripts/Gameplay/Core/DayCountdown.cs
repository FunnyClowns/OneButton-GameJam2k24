using System.Collections;
using UnityEngine;

public class DayCountdown : MonoBehaviour
{
    
    [Header("Timer")]
    [SerializeField] float gameTime;
    float gameTimerCount;
    bool gameRunning;

    void Start(){

        gameRunning = true;
        StartCoroutine(TimerCountdown());
    }

    IEnumerator TimerCountdown(){

        while (gameRunning){
            yield return new WaitForSeconds(1f);

            gameTimerCount++;

            Debug.Log("Day Timer : " + gameTimerCount);

            if (gameTimerCount >= gameTime){
                gameRunning = false;
            }
        }

        TimerEnded();

    }

    void TimerEnded(){
        Debug.Log("DAY ENDED");
        StopCoroutine(TimerCountdown());

        if (ScoreManager.correctGuessCount >= 5){
            Debug.Log("WIN");
        } else {
            Debug.Log("LOST");
        }
    }
    
}
