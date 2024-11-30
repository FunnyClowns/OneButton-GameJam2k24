using System.Collections;
using UnityEngine;

public class DayCountdown : MonoBehaviour, ISliderValue
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

            // Debug.Log("Day Timer : " + gameTimerCount);

            if (gameTimerCount >= gameTime){
                gameRunning = false;
            }
        }

        TimerEnded();

    }

    void TimerEnded(){
        Debug.Log("DAY ENDED");
        StopCoroutine(TimerCountdown());

        if (ScoreManager.remainingFormCount <= 0){
            GameState.GameWin();
        } else {
            GameState.GameLose();
        }
    }

    // interfaces
    public float GetSliderValue(){

        return gameTimerCount;
    }
    
}
