using UnityEngine;
using UnityEngine.Events;

// a class that trigger when movedrag objects are placed above it
public class OverheadTriggerZone : MonoBehaviour
{

    MoveOnDrag collidingObject;

    [Space(10)]
    [SerializeField] UnityEvent completedEvent;

    void OnTriggerStay2D (Collider2D col){
        
        if (col.transform.TryGetComponent<MoveOnDrag>(out collidingObject)){
            Debug.Log("Something enter");
            if (!collidingObject.isDragged){
                Debug.Log("Submit form");
                completedEvent.Invoke();
            }
        }
    }
}
