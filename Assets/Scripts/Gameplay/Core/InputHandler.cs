using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputHandler : MonoBehaviour
{

    [SerializeField] InputAction input;
    [SerializeField] FormFactory formFactory;
    [SerializeField] ScoreManager score;

    public enum InputType{
        Accept,
        Deny,
    }

    InputType currentInput;

    void Start(){
        input.AddBinding("<Keyboard>/space")
        .WithInteraction("slowTap(duration=0.5)");
    }

    void OnEnable(){
        input.Enable();

        input.performed += context => { 
            if (context.interaction is SlowTapInteraction){
                OnSlowTapComplete();
            }
        };

        input.canceled += context => { 
            if (context.interaction is SlowTapInteraction){
                OnSlowTapCancelled();
            }
        };
    }

    void OnDisable(){
        input.Disable();
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

}
