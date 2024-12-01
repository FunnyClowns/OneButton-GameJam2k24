using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputHandler : MonoBehaviour, ISliderValue
{

    [Header("Player Sprites")]
    [SerializeField] Sprite approveHand;
    [SerializeField] Sprite denyHand;

    [Header("Player Components")]
    [SerializeField] SpriteRenderer handRenderer;
    [SerializeField] Animator handAnimator;

    [Header("Other Components")]
    [SerializeField] InputAction input;
    [SerializeField] FormFactory formFactory;
    [SerializeField] ScoreManager score;
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] SoundController sound;

    [SerializeField] SliderFiller acceptFiller;
    [SerializeField] SliderFiller denyFiller;
    [SerializeField] GameObject acceptSliderBG;
    [SerializeField] GameObject denySliderBG;   
    
    

    
    public enum InputType{
        Accept,
        Deny,
    }

    bool isHolding;
    bool canSubmit;
    float holdingTime;

    InputType currentInput = InputType.Accept;

    void Start(){
        UpdateInputState();

        canSubmit = true;
    }

    void OnEnable(){
        input.Enable();

        input.started += context => {
            if (context.interaction is SlowTapInteraction){
                InputStartReceiver();
            }
        };

        input.performed += context => { 
            if (context.interaction is SlowTapInteraction){
                InputPerformedReceiver();
            }
        };

        input.canceled += context => { 
                if (context.interaction is SlowTapInteraction){
                    InputCancelledReceiver();
                }
            };

    }

    void OnDisable(){
        input.Disable();
    }

    void Update(){
        if (isHolding){
            holdingTime += Time.deltaTime;
        } else {
            holdingTime = 0;
        }
    }

    void InputStartReceiver(){

         // set button to gameplay
        if (GameState.currentState == GameState.StateType.Going){
            isHolding = true;
        }

        if (GameState.currentState == GameState.StateType.Win){
            sceneLoader.LoadNextScene();
        }

        if (GameState.currentState == GameState.StateType.Lost){
            sceneLoader.ReloadScene();
        }
    }

    void InputPerformedReceiver(){

         // set button to gameplay
        if (GameState.currentState == GameState.StateType.Going){
            if (canSubmit){
                    canSubmit = false;

                    OnSlowTapComplete();
                }
                
                isHolding = false;
        }
    }

    void InputCancelledReceiver(){

         // set button to gameplays
        if (GameState.currentState == GameState.StateType.Going){
            InverseInputType();
            isHolding = false;
        }

    }

    void OnSlowTapComplete(){

        // Debug.Log("Complete input");

        score.ProgressScore(formFactory.generatedFormData.thisFormType, currentInput);

        PlayStampSound();

        formFactory.generatedFormData.SubmitForm(currentInput);
        handAnimator.Play("Hand_StartStamp");

        StartCoroutine(TriggerFactoryToGenerate());

    }

    void PlayStampSound(){
        if (currentInput == InputType.Accept)
            sound.PlaySoundOnce(1);
        
        else if (currentInput == InputType.Deny)
            sound.PlaySoundOnce(2);
    }

    IEnumerator TriggerFactoryToGenerate(){

        yield return new WaitForSeconds(2f);

        handAnimator.Play("Hand_Idle");

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

    void InverseInputType(){
        
        // inverse input type

        if (currentInput == InputType.Accept){
            Debug.Log("Input deny");
            currentInput = InputType.Deny;
            handRenderer.sprite = denyHand;

        } else {
            Debug.Log("input accept");
            currentInput = InputType.Accept;
            handRenderer.sprite = approveHand;
        }

        UpdateInputState();
    }

    void UpdateInputState(){

        if (currentInput == InputType.Accept){
            acceptFiller.shouldUpdate = true;
            denyFiller.shouldUpdate = false;

            acceptSliderBG.SetActive(true);
            denySliderBG.SetActive(false);

            denyFiller.ResetSlider();
        } 

        else if (currentInput == InputType.Deny){
            acceptFiller.shouldUpdate = false;
            denyFiller.shouldUpdate = true;

            acceptSliderBG.SetActive(false);
            denySliderBG.SetActive(true);

            acceptFiller.ResetSlider();
        }
    }

    public float GetSliderValue(){
        return holdingTime;
    }
}
