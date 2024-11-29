using TMPro;
using UnityEngine;

public class FormData : MonoBehaviour
{
    public enum FormType{
        Correct,
        Incorrect,
    }

    [HideInInspector] public FormType currentFormType;

    [Header("Form Sprites"), SerializeField]
    Sprite approvedStamp;
    [SerializeField] Sprite deniedStamp;

    [Header("Form Components")]
    public TextMeshPro signatureRenderer;
    public SpriteRenderer companyWatermarkRenderer;
    public SpriteRenderer stampRenderer;
    public GameObject decisionStamp;
    Animator formAnimator;

    SpriteRenderer decisionStampRenderer;
    Animator decisionStampAnimator;

    void Start(){   
        decisionStamp.TryGetComponent<Animator>(out decisionStampAnimator);
        decisionStamp.TryGetComponent<SpriteRenderer>(out decisionStampRenderer);
        
        if (TryGetComponent<Animator>(out formAnimator)){
            formAnimator.Play("Form In");
        }

        VariateObjectTransform();
    }

    public void SubmitForm(InputHandler.InputType choice){
        decisionStamp.SetActive(true);

        if (choice == InputHandler.InputType.Accept)
            decisionStampRenderer.sprite = approvedStamp;

        if (choice == InputHandler.InputType.Deny)
            decisionStampRenderer.sprite = deniedStamp;

        decisionStampAnimator.Play("StartStamping");

        Invoke(nameof(PlayExitAnimation), 1f);
    }

    void PlayExitAnimation(){
        formAnimator.Play("Form Out");
    }

    void VariateObjectTransform(){

        RandomizeTransformPosition(stampRenderer.transform, 0.05f, 0.05f);
        RandomizeTransformScale(stampRenderer.transform, 1.1f);
        RandomizeTransformRotation(stampRenderer.transform, 15f);
    }

    void RandomizeTransformPosition(Transform target, float xRange, float yRange){
        target.localPosition = new Vector2(target.localPosition.x + Random.Range(-xRange, xRange), target.localPosition.y + Random.Range(-yRange, yRange));

        return;
    }

    void RandomizeTransformScale(Transform target, float range){

        var scaleMultiplier = Random.Range(1, range);

        target.localScale = (Vector2)target.localScale * scaleMultiplier;

        return;
    }

    void RandomizeTransformRotation(Transform target, float range){
        target.localEulerAngles = new Vector3(0,0, Random.Range(-range, range));

        return;
    }
}
