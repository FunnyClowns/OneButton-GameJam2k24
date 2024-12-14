using System.Collections;
using UnityEngine;

public class FormBehaviour : MonoBehaviour
{

    FormData data;
    
    void Start(){
        data = GetComponent<FormData>();

        PlayInAnimation();
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

        if (data.formSubmitted || !data.formStamped)
            return;

        data.formSubmitted = true;

        EnableAnimator();
        ResetForm();
        
        StartCoroutine(SubmitCoroutine());
    }

    IEnumerator SubmitCoroutine(){

        yield return new WaitUntil(() => data.formAnimator.enabled);
        PlayInAnimation();
    }

    void ResetForm(){
        data.formSubmitted = false;
        data.formStamped = false;

        data.formAnimator.Play("Idle");

        data.decisionStamp.SetActive(false);
    }

    public void PlayInAnimation(){
        data.formAnimator.Play("Form In");
        
        Invoke(nameof(DisableAnimator), 1.2f);
    }

    public void PlayExitAnimation(){
        data.formAnimator.Play("Form Out");
    }

    void EnableAnimator(){
        data.formAnimator.enabled = true;
    }

    // disable animator so form can be dragged
    void DisableAnimator(){
        data.formAnimator.enabled = false;
    }
}
