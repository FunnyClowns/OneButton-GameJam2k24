using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MoveOnDrag : MonoBehaviour, IMouse {

    [Space(10)]
    [Tooltip("Optional call, when mouse is lifted")]
    [SerializeField] UnityEvent inputCancelledCall;

    Vector2 difference;

    public void OnMouseClick(InputAction.CallbackContext context){
        // Debug.Log("click");

        difference = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
    }

    public void OnMouseGrab(InputAction.CallbackContext context){
        Vector2 mouseVelocity = context.ReadValue<Vector2>();

        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - difference;
        // Debug.Log("Mouse vel = " + mouseVelocity * 1);
    }

    public void OnMouseStop(InputAction.CallbackContext context){
        inputCancelledCall.Invoke();
    }
}
