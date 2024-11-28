using UnityEngine;

public class GameState : MonoBehaviour
{
    
    public enum StateType{
        Going,
        Win,
        Lost
    }

    public static StateType currentState;


    void Start(){
        currentState = StateType.Going;
    }

    public static void GameWin(){
        if (!(currentState == StateType.Going))
            return;

        currentState = StateType.Win;

        Debug.Log("WIN");
    }

    public static void GameLose(){

        if (!(currentState == StateType.Going))
            return;

        currentState = StateType.Lost;

        Debug.Log("LOSE");
    }

}
