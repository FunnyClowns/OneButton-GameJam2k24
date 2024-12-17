using System.Collections;
using UnityEngine;

public class FormBehaviour : MonoBehaviour
{

    FormData data;
    ScoreManager score;
    FormFactory formFactory;
    
    void Start(){
        data = GetComponent<FormData>();
        score = FindObjectOfType<ScoreManager>();
        formFactory = FindObjectOfType<FormFactory>();

        PlayInAnimation();
    }

    public void StampForm(StampAttributes.InputTypes choice){
        if (data.formStamped){
            return;
        }
        data.formStamped = true;

        data.stampState = choice;
        data.decisionStamp.SetActive(true);

        if (data.stampState == StampAttributes.InputTypes.Approve)
            data.decisionStampRenderer.sprite = data.approvedStamp;

        if (data.stampState == StampAttributes.InputTypes.Deny)
            data.decisionStampRenderer.sprite = data.deniedStamp;

        data.decisionStampAnimator.Play("StartStamping");

        // Debug.Log("Stamp choice : " + choice);
        // Debug.Log("Form type : " + data.thisFormType);
    }

    public void SubmitForm(){

        if (data.formSubmitted || !data.formStamped)
            return;

        data.formSubmitted = true;

        EnableAnimator();
        UpdateGlobalScore();
        PlayExitAnimation();
        
        StartCoroutine(SubmitCoroutine());
    }

    IEnumerator SubmitCoroutine(){

        yield return new WaitForSeconds(1.1f);
        ResetForm();
        PlayInAnimation();
    }

    void ResetForm(){
        data.formSubmitted = false;
        data.formStamped = false;

        data.decisionStamp.SetActive(false);

        formFactory.RecycleForm();
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

    void UpdateGlobalScore(){
        score.ProgressScore(data.thisFormType, data.stampState);
    }
}
