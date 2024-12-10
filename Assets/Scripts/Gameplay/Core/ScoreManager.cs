using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int correctGuessCount = 0;
    public static int wrongGuessCount = 0;
    public static int remainingFormCount = 0;

    [SerializeField] int formAmount;

    [Header("Other Components")]
    [SerializeField] PlayerData playerData;
    [SerializeField] TextMeshProUGUI remainFormText;
    [SerializeField] DialogueController bossDialogue;

    void Start(){

        // reset static variables
        correctGuessCount = 0;
        wrongGuessCount = 0;
        remainingFormCount = formAmount;

        UpdateText();

    }

    public void ProgressScore(FormData.FormType form, InputHandler.InputType input){

        if (input == InputHandler.InputType.Accept)
            if (form == FormData.FormType.Correct){
                CorrectGuess();
            } else {
                IncorrectGuess();
            }

        else if (input == InputHandler.InputType.Deny)
            if (form == FormData.FormType.Incorrect){
                CorrectGuess();
            } else {
                IncorrectGuess();
            }
        
        remainingFormCount--;
        UpdateText();
    }

    void UpdateText(){
        remainFormText.text = "Forms Left = " + remainingFormCount;
    }

    void CorrectGuess(){
        correctGuessCount++;

    }   

    void IncorrectGuess(){
        wrongGuessCount++;

        if (wrongGuessCount < 3)
            StartCoroutine(ShowBossDialogue());
            
        playerData.ChangeLiveAmount(-1);

    }

    IEnumerator ShowBossDialogue(){
        bossDialogue.StartDialogue();

        yield return new WaitForSeconds(5f);

        bossDialogue.StopDialogue();

        yield return new WaitForSeconds(1f);
    }
}
