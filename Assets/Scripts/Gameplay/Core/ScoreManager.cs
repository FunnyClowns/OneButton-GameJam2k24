using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int correctGuess = 0;
    int wrongGuess = 0;

    [SerializeField] TextMeshProUGUI correctScoreText;
    [SerializeField] TextMeshProUGUI wrongScoreText;

    public void ProgressScore(FormData.FormType form, InputHandler.InputType input){

        if (input == InputHandler.InputType.Accept)
            if (form == FormData.FormType.Correct){
                correctGuess++;
            } else {
                wrongGuess++;
            }

        else if (input == InputHandler.InputType.Deny)
            if (form == FormData.FormType.Incorrect){
                correctGuess++;
            } else {
                wrongGuess++;
            }

        correctScoreText.text = "Correct Guess = " + correctGuess;
        wrongScoreText.text = "Wrong Guess = " + wrongGuess;
    }
}
