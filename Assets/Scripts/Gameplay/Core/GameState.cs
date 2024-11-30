using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    
    public enum StateType{
        Going,
        Win,
        Lost
    }

    public static StateType currentState;

    [Header("Game Sprites")]
    [SerializeField] Sprite WinSpriteScene;
    [SerializeField] Sprite LostSpriteScene;


    [Header("Other Components")]
    [SerializeField] Image gameOverRenderer;
    static string gameOverMessage;

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

    void Update(){
        if (currentState != StateType.Going)
            Gameover();
    }

    void Gameover(){
        Debug.Log("Game over");

        ActivateGameOverScene();
    }

    void ActivateGameOverScene(){
        
        if (currentState == StateType.Win)
            gameOverRenderer.sprite = WinSpriteScene;

        if (currentState == StateType.Lost)
            gameOverRenderer.sprite = LostSpriteScene;

        gameOverRenderer.gameObject.SetActive(true);
    }

}
