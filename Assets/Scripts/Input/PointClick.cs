using UnityEngine;
using UnityEngine.InputSystem;

public class PointClick : MonoBehaviour, IMouse
{
    PlayerInput playerInput;
    
    IMouse currentDrag;
    Mouse mouse;

    InputAction drag;

    void Start(){
        playerInput = GetComponent<PlayerInput>();

        InputActionMap inputMap = playerInput.currentActionMap;

        InputAction click = inputMap.FindAction("Click", true);

        click.started += OnClickStarted;
        click.canceled += OnClickCancelled;

        drag = inputMap.FindAction("Drag", true);

        mouse = Mouse.current;

    }

    public void OnClickStarted(InputAction.CallbackContext context){
        
        currentDrag = this;

        switch (mouse.clickCount.ReadValue()){
            
            // singular click
            case 1:
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mouse.position.ReadValue()), Vector2.zero);

                if (hit.collider != null){
                    if (hit.transform.TryGetComponent<IMouse>(out currentDrag)){
                        currentDrag.OnMouseClick(context);
                        drag.started += currentDrag.OnMouseGrab;
                    }
                }

                break;

        }
    }

    public void OnClickCancelled(InputAction.CallbackContext context){
        drag.started -= currentDrag.OnMouseGrab;
        currentDrag.OnMouseStop(context);
    }

    public void OnMouseClick(InputAction.CallbackContext context){ }

    public void OnMouseGrab(InputAction.CallbackContext context){ }

    public void OnMouseStop(InputAction.CallbackContext context){ }
}
