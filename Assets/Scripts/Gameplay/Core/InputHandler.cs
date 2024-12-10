using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputHandler : MonoBehaviour
{

    [SerializeField] bool isTutorial = false;

    [Header("Player Sprites")]
    [SerializeField] Sprite approveHand;
    [SerializeField] Sprite denyHand;


    [Header("Player Components")]
    [SerializeField] SpriteRenderer handRenderer;
    [SerializeField] Animator handAnimator;


    [Header("Other Components")]
    [SerializeField] FormFactory formFactory;
    [SerializeField] ScoreManager score;
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] SoundController sound;
    [SerializeField] TutorialManager tutorialManager; 
    
    

    
    public enum InputType{
        Accept,
        Deny,
    }

    bool canSubmit;

    InputType currentInput = InputType.Accept;

    const float STAMP_COOLDOWN = 1f;
    float stampCooldownCounter;

    void Start(){
        canSubmit = true;
    }

    void Update(){
        stampCooldownCounter -= Time.deltaTime;

        // Debug.Log(stampCooldownCounter);
    }

    public void InputPerformedReceiver(){
            
         // set button to gameplay
        if (GameState.currentState == GameState.StateType.Going && stampCooldownCounter <= 0){
            if (canSubmit || isTutorial){
                    canSubmit = false;

                    OnSlowTapComplete();
                    stampCooldownCounter = STAMP_COOLDOWN;
                    Debug.Log("STAMP");
                }
                
        }
    }

    public void UpdateInputType(bool isApprove){

        if (isApprove){
            currentInput = InputType.Accept;
            handRenderer.sprite = approveHand;
        }
        else {
            currentInput = InputType.Deny;
            handRenderer.sprite = denyHand;
        }
    }

    void OnSlowTapComplete(){

        // Debug.Log("Complete input");

        PlayStampSound();

        StartCoroutine(PlayHandAnimation());

        if (!isTutorial){
            score.ProgressScore(formFactory.generatedFormData.thisFormType, currentInput);
            formFactory.generatedFormData.SubmitForm(currentInput);
            StartCoroutine(TriggerFactoryToGenerate());
        }

        if (isTutorial && currentInput == InputType.Accept && tutorialManager.waitingForInput){
            tutorialManager.formData.SubmitForm(InputType.Accept);
        }

    }

    IEnumerator PlayHandAnimation(){
        handAnimator.Play("Hand_StartStamp");

        yield return new WaitForSeconds(1.0f);

        handAnimator.Play("Hand_Idle");
    }

    void PlayStampSound(){
        if (currentInput == InputType.Accept)
            sound.PlaySoundOnce(1);
        
        else if (currentInput == InputType.Deny)
            sound.PlaySoundOnce(2);
    }

    IEnumerator TriggerFactoryToGenerate(){

        yield return new WaitForSeconds(2f);

        // if remaining form is less than 0, do stop there
        if (ScoreManager.remainingFormCount <= 0){
            formFactory.DeleteActiveForm();
            GameState.GameWin();
            yield break;
        }

        // game is already over
        if (GameState.currentState != GameState.StateType.Going){
            formFactory.DeleteActiveForm();
            yield break;
        }

        formFactory.GenerateForm();

        yield return new WaitForSeconds(1.5f);

        canSubmit = true;
    }
}
