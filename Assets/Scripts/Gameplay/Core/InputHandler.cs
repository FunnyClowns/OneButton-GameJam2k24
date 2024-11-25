using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    [SerializeField] InputAction input;
    [SerializeField] FormFactory formFactory;
    [SerializeField] ScoreManager score;

    public enum InputType{
        Accept,
        Deny,
    }

    void Start(){
        input.AddBinding("<Keyboard>/space")
        .WithInteraction("slowTap(duration=0.5)");
    }

    void OnEnable(){
        input.Enable();

        input.performed += context => { 
            if (context.interaction is UnityEngine.InputSystem.Interactions.SlowTapInteraction){
                OnSlowTapComplete();
            }
        };

        input.canceled += context => { 
            if (context.interaction is UnityEngine.InputSystem.Interactions.SlowTapInteraction){
                OnSlowTapCancelled();
            }
        };
    }

    void OnDisable(){
        input.Disable();
    }

    void OnSlowTapComplete(){

        Debug.Log("input accept");

        score.ProgressScore(formFactory.formData.currentFormType, InputType.Accept);

        formFactory.GenerateForm();

    }

    void OnSlowTapCancelled(){

        Debug.Log("Input deny");
        score.ProgressScore(formFactory.formData.currentFormType, InputType.Deny);

        formFactory.GenerateForm();
    }

}
