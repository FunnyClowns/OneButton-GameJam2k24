using TMPro;
using UnityEngine;

public class GameState : MonoBehaviour
{
    
    public enum StateType{
        Going,
        Win,
        Lost
    }

    public static StateType currentState;


    [Header("Other Components")]
    [SerializeField] TextMeshProUGUI gameOverTMP;
    static string gameOverMessage;

    void Start(){
        currentState = StateType.Going;
    }

    public static void GameWin(){
        if (!(currentState == StateType.Going))
            return;

        currentState = StateType.Win;

        gameOverMessage = "WIN\nyayyyyyy";
        Debug.Log("WIN");
    }

    public static void GameLose(){

        if (!(currentState == StateType.Going))
            return;

        currentState = StateType.Lost;

        gameOverMessage = "LOSE";
        Debug.Log("LOSE");
    }

    void Update(){
        
        if (currentState != StateType.Going)
            Gameover();
    }

    void Gameover(){
        gameOverTMP.enabled = true;
        gameOverTMP.text = gameOverMessage;
    }

}
