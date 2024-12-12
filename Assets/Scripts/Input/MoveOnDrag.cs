using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MoveOnDrag : MonoBehaviour, IMouse {

    [Header("Optional Settings")]
    [Space(10)]
    [SerializeField] bool resetPositionAtMouseUP;
    Vector2 startPosition;

    [Space(10)]
    [Tooltip("Optional call, when mouse is lifted")]
    [SerializeField] UnityEvent inputCancelledCall;

    [HideInInspector] public bool isDragged;
    Vector2 difference;

    void Start(){
        if (resetPositionAtMouseUP)
            startPosition = this.transform.position;
    }

    public void OnMouseClick(InputAction.CallbackContext context){
        // Debug.Log("click");

        isDragged = true;
        difference = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
    }

    public void OnMouseGrab(InputAction.CallbackContext context){

        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - difference;
        // Debug.Log("Mouse vel = " + mouseVelocity * 1);
    }

    public void OnMouseStop(InputAction.CallbackContext context){
        isDragged = false;

        inputCancelledCall.Invoke();
        
        if (resetPositionAtMouseUP)
            this.transform.position = startPosition;
    }
}
