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

    public void StampForm(InputHandler.InputType choice){
        if (data.formStamped){
            return;
        }
        data.formStamped = true;

        data.stampState = choice;
        data.decisionStamp.SetActive(true);

        if (data.stampState == InputHandler.InputType.Accept)
            data.decisionStampRenderer.sprite = data.approvedStamp;

        if (data.stampState == InputHandler.InputType.Deny)
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

        ChooseNewVariation();
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

    void ChooseNewVariation(){
        formFactory.ChooseRandomFormType();
    }
}