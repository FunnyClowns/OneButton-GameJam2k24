using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int correctGuessCount = 0;
    public static int wrongGuessCount = 0;

    [Header("Other Components")]
    [SerializeField] PlayerData playerData;
    [SerializeField] TextMeshProUGUI correctScoreText;
    [SerializeField] TextMeshProUGUI wrongScoreText;

    void Start(){

        // reset static variables
        correctGuessCount = 0;
        wrongGuessCount = 0;
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

        correctScoreText.text = "Correct Guess = " + correctGuessCount;
        wrongScoreText.text = "Wrong Guess = " + wrongGuessCount;
    }

    void CorrectGuess(){
        correctGuessCount++;

    }   

    void IncorrectGuess(){
        correctGuessCount++;

        playerData.ChangeLiveAmount(-1);

    }
}
