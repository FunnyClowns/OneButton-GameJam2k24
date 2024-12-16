using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MoveOnDrag : MonoBehaviour, IMouse {

    Vector2 extents;

    Vector2 topRight;
    Vector2 bottomLeft;
    const float CAMERA_LIMIT_OFFSET = 1.5f;

    [Header("Optional Settings")]
    [Space(10)]
    [SerializeField] bool resetPositionAtMouseUP;
    [SerializeField] bool isMoveRestrictedToScreen;
    Vector2 startPosition;

    [Space(10)]
    [Tooltip("Optional call, when mouse is lifted")]
    [SerializeField] UnityEvent inputCancelledCall;

    [HideInInspector] public bool isDragged;
    Vector2 difference;

    void Start(){
        if (resetPositionAtMouseUP)
            startPosition = this.transform.position;

        if (isMoveRestrictedToScreen){
            topRight = Camera.main.ViewportToWorldPoint(Vector3.one);
            bottomLeft = Camera.main.ViewportToWorldPoint(Vector3.zero);

            topRight.y *= CAMERA_LIMIT_OFFSET;
            bottomLeft.y *= CAMERA_LIMIT_OFFSET;

            extents = GetComponent<SpriteRenderer>().sprite.bounds.extents;

            // Debug.Log("Extents : " + extents);
        }
    }

    public void OnMouseClick(InputAction.CallbackContext context){
        // Debug.Log("click");

        isDragged = true;
        difference = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
    }

    public void OnMouseGrab(InputAction.CallbackContext context){

        Vector2 pos = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - difference;

        if (isMoveRestrictedToScreen){
            pos.x = Mathf.Clamp(pos.x, bottomLeft.x + extents.x, topRight.x - extents.x);
            pos.y = Mathf.Clamp(pos.y, bottomLeft.y + extents.y, topRight.y - extents.y);
        }

        transform.position = pos;
        // Debug.Log("Pos = " + pos);
    }

    public void OnMouseStop(InputAction.CallbackContext context){
        isDragged = false;

        inputCancelledCall.Invoke();
        
        if (resetPositionAtMouseUP)
            this.transform.position = startPosition;
    }
}
