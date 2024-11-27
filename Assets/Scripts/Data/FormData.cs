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
    public SpriteRenderer signatureRenderer;
    public SpriteRenderer companyWatermarkRenderer;
    public SpriteRenderer formText;
    public SpriteRenderer stampRenderer;
    public GameObject decisionStamp;

    SpriteRenderer decisionStampRenderer;
    Animator decisionStampAnimator;

    void Start(){   
        decisionStamp.TryGetComponent<Animator>(out decisionStampAnimator);
        decisionStamp.TryGetComponent<SpriteRenderer>(out decisionStampRenderer);

    }

    public void SubmitForm(InputHandler.InputType choice){
        decisionStamp.SetActive(true);

        if (choice == InputHandler.InputType.Accept)
            decisionStampRenderer.sprite = approvedStamp;

        if (choice == InputHandler.InputType.Deny)
            decisionStampRenderer.sprite = deniedStamp;

        decisionStampAnimator.Play("StartStamping");
    }
}
