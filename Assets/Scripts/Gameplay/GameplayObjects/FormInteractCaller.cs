using UnityEngine;

public class FormInteractCaller : MonoBehaviour
{

    enum InteractType {
        Stamp,
        Write,
        Signature,
    }

    [SerializeField] InteractType thisType;

    [SerializeField] LayerMask interactableMask;

    public void GetInterectableObject(){

        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, 1f, interactableMask);

        if (hit.collider != null)
            if (hit.transform.TryGetComponent<FormInteraction>(out FormInteraction form))
            {
                Debug.Log("Hit : " + hit.transform.gameObject.name);

                switch (thisType){
                    case InteractType.Stamp :
                        form.StampThis();
                        break;

                    case InteractType.Write :
                        form.WriteSomething();
                        break;

                    case InteractType.Signature :
                        form.SignForm();
                        break;

                    default :
                        break;
                }
            }

    }
}
