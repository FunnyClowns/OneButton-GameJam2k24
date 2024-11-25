using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    [SerializeField] InputAction input;

    void Start(){
        input.AddBinding("<Keyboard>/space")
        .WithInteraction("slowTap(duration=2)");
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

    void OnSlowTapComplete(){

        Debug.Log("input accept");

    }

    void OnSlowTapCancelled(){

        Debug.Log("Input deny");
    }

}
