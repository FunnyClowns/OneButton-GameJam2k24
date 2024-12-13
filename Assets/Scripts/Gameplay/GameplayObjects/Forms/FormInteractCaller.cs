using UnityEngine;

public class FormInteractCaller : MonoBehaviour
{

    enum InteractType {
        StampApprove,
        StampDeny,
        Write,
        Signature,
    }

    [SerializeField] InteractType thisType;

    [SerializeField] LayerMask interactableMask;

    public void GetInterectableObject(){

        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, 1f, interactableMask);

        if (hit.collider != null)
            if (hit.transform.TryGetComponent<FormBehaviour>(out FormBehaviour form))
            {
                Debug.Log("Hit : " + hit.transform.gameObject.name);

                switch (thisType){
                    case InteractType.StampApprove :
                        form.StampForm(InputHandler.InputType.Accept);
                        break;

                    case InteractType.StampDeny :
                        form.StampForm(InputHandler.InputType.Deny);
                        break;

                    case InteractType.Write :
                        // form.WriteSomething();
                        break;

                    case InteractType.Signature :
                        // form.SignForm();
                        break;

                    default :
                        break;
                }
            }

    }
}
