using System.Collections;
using TMPro;
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
    bool isGameOver = false;

    [Header("Game Sprites")]
    [SerializeField] Sprite WinSpriteScreen;
    [SerializeField] Sprite LostSpriteScreen;
    


    [Header("Other Components")]
    [SerializeField] Image gameOverRenderer;
    [SerializeField] TextMeshProUGUI continueText;
    [SerializeField] SoundController sound;

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
        if (currentState != StateType.Going && !isGameOver){
            isGameOver = true;
            Gameover();
        }
    }

    void Gameover(){
        // Debug.Log("Game over");

        StartCoroutine(ActivateGameOverScreen());
    }

    IEnumerator ActivateGameOverScreen(){
        
        if (currentState == StateType.Win){
            gameOverRenderer.sprite = WinSpriteScreen;
            sound.PlayMusicOverload(1, 0.5f);
        }

        if (currentState == StateType.Lost){
            gameOverRenderer.sprite = LostSpriteScreen;
            sound.PlayMusicOverload(2, 0.5f);
        }
        gameOverRenderer.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);

        continueText.gameObject.SetActive(true);
    }

}
