using System.Collections;
using UnityEngine;

public class FormBehaviour : MonoBehaviour
{

    FormData data;
    FormAttributes attributes;
    ScoreManager score;
    FormFactory formFactory;

    [SerializeField] Transform trStampApprove;
    [SerializeField] Transform trStampDeny;
    
    void Start(){
        data = GetComponent<FormData>();
        attributes = GetComponent<FormAttributes>();
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
        attributes.decisionStamp.SetActive(true);

        if (data.stampState == StampAttributes.InputTypes.Approve){
            attributes.decisionStampRenderer.sprite = attributes.approveStampSprite;
            attributes.decisionStamp.transform.position = trStampApprove.position;
        }

        if (data.stampState == StampAttributes.InputTypes.Deny){
            attributes.decisionStampRenderer.sprite = attributes.denyStampSprite;
            attributes.decisionStamp.transform.position = trStampDeny.position;
        }

        attributes.decisionStampAnimator.Play("StartStamping");

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

        attributes.decisionStamp.SetActive(false);

        formFactory.RecycleForm();
    }

    public void PlayInAnimation(){
        attributes.formAnimator.Play("Form In");
        
        Invoke(nameof(DisableAnimator), 1.2f);
    }

    public void PlayExitAnimation(){
        attributes.formAnimator.Play("Form Out");
    }

    void EnableAnimator(){
        attributes.formAnimator.enabled = true;
    }

    // disable animator so form can be dragged
    void DisableAnimator(){
        attributes.formAnimator.enabled = false;
    }

    void UpdateGlobalScore(){
        score.ProgressScore(data.thisFormType, data.stampState);
    }
}
