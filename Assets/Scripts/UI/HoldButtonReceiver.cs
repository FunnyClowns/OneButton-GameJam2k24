using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButtonReceiver : MonoBehaviour,IPointerDownHandler,IPointerUpHandler, ISliderValue
{
    float holdingTime;
    bool isPressed;

    [SerializeField] InputHandler inputHandler;

    void Update() {

        if (isPressed){
            holdingTime += Time.deltaTime;

            if (holdingTime >= 1){
                isPressed = false;
                holdingTime = 0;

                CompleteHold();
            }
            // Debug.Log("Holding time : " + holdingTime);
        }
    }

    public void OnPointerDown(PointerEventData data){
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData data){
        isPressed = false;
        holdingTime = 0;
    }

    void CompleteHold(){
        inputHandler.InputPerformedReceiver();

        Debug.Log("Completed hold");
    }

    public float GetSliderValue(){

        var clampedValue = Mathf.Clamp(holdingTime, 0, 1.0f);

        // Debug.Log("Clamped value = " + clampedValue);

        return clampedValue;
    }
}
