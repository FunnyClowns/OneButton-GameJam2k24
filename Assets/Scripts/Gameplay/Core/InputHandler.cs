using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputHandler : MonoBehaviour, ISliderValue
{

    // components
    [SerializeField] InputAction input;
    [SerializeField] FormFactory formFactory;
    [SerializeField] ScoreManager score;

    [SerializeField] SliderFiller acceptFiller;
    [SerializeField] SliderFiller denyFiller;
    [SerializeField] GameObject acceptSliderBG;
    [SerializeField] GameObject denySliderBG;    


    SlowTapInteraction slowTap;

    

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
                slowTap = context.interaction as SlowTapInteraction;
                isHolding = true;
            }
        };

        input.performed += context => { 
            if (context.interaction is SlowTapInteraction){
                
                if (canSubmit){
                    canSubmit = false;

                    OnSlowTapComplete();
                }
                
                isHolding = false;
            }
        };

        input.canceled += context => { 
            if (context.interaction is SlowTapInteraction){
                InverseInputType();
                isHolding = false;
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

    void OnSlowTapComplete(){

        Debug.Log("Complete input");

        score.ProgressScore(formFactory.formData.currentFormType, currentInput);

        formFactory.formData.SubmitForm(currentInput);

        Invoke(nameof(TriggerFactoryToGenerate), 1.5f);

    }

    void TriggerFactoryToGenerate(){

        // if remaining form is less than 0, do stop there
        if (ScoreManager.remainingFormCount <= 0){
            formFactory.DeleteActiveForm();
            GameState.GameWin();
            return;
        }

        // game is already over
        if (GameState.currentState != GameState.StateType.Going){
            formFactory.DeleteActiveForm();
            return;
        }
        canSubmit = true;

        formFactory.GenerateForm();
    }

    void InverseInputType(){
        
        // inverse input type

        if (currentInput == InputType.Accept){
            Debug.Log("Input deny");
            currentInput = InputType.Deny;

            
            
        } else {
            Debug.Log("input accept");
            currentInput = InputType.Accept;  
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
