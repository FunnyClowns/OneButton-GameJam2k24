using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int correctGuess = 0;
    int wrongGuess = 0;

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

        Debug.Log("Correct Guess = " + correctGuess);
        Debug.Log("Wrong Guess = " + wrongGuess);
    }
}
