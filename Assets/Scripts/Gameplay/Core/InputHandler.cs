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
    SlowTapInteraction slowTap;

    

    public enum InputType{
        Accept,
        Deny,
    }

    bool isHolding;
    float holdingTime;

    InputType currentInput;

    void Start(){
        input.AddBinding("<Keyboard>/space")
        .WithInteraction("slowTap(duration=1)");
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
                OnSlowTapComplete();
                isHolding = false;
            }
        };

        input.canceled += context => { 
            if (context.interaction is SlowTapInteraction){
                OnSlowTapCancelled();
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

        formFactory.GenerateForm();

    }

    void OnSlowTapCancelled(){
        
        // inverse input type

        if (currentInput == InputType.Accept){
            Debug.Log("Input deny");
            currentInput = InputType.Deny;
            
        } else {
            Debug.Log("input accept");
            currentInput = InputType.Accept;
        }
    }

    public float GetSliderValue(){
        return holdingTime;
    }
}
