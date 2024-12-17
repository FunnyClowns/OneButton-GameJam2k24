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

    public void ProgressScore(FormData.FormType form, StampAttributes.InputTypes input){

        // checks form type and player choice
        if (input == StampAttributes.InputTypes.Approve)
            if (form == FormData.FormType.Correct){
                CorrectGuess();
            } else {
                IncorrectGuess();
            }

        else if (input == StampAttributes.InputTypes.Deny)
            if (form == FormData.FormType.Incorrect){
                CorrectGuess();
            } else {
                IncorrectGuess();
            }
        
        remainingFormCount--;

        if (remainingFormCount <= 0){
            GameState.GameWin();
            return;
        }

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
            bossDialogue.StartDialogue();
            
        playerData.ChangeLiveAmount(-1);

    }
}
