using UnityEngine;

public class FormBehaviour : MonoBehaviour
{

    FormData data;
    
    void Start(){
        data = GetComponent<FormData>();
    }

    public void StampForm(InputHandler.InputType choice){
        if (data.formStamped){
            return;
        }
        data.formStamped = true;

        data.decisionStamp.SetActive(true);

        if (choice == InputHandler.InputType.Accept)
            data.decisionStampRenderer.sprite = data.approvedStamp;

        if (choice == InputHandler.InputType.Deny)
            data.decisionStampRenderer.sprite = data.deniedStamp;

        data.decisionStampAnimator.Play("StartStamping");
    }

    public void SubmitForm(){

        if (data.formSubmitted)
            return;

        data.formSubmitted = true;

        this.gameObject.SetActive(false);
    }
}
