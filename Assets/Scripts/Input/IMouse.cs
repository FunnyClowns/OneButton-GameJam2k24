using UnityEngine.InputSystem;

public interface IMouse {
    void OnMouseClick(InputAction.CallbackContext context);
    void OnMouseGrab(InputAction.CallbackContext context);
    void OnMouseStop(InputAction.CallbackContext context);
}