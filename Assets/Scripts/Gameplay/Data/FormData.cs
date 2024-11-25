using UnityEngine;

public class FormData : MonoBehaviour
{
    public enum FormType{
        Correct,
        Incorrect,
    }

    [HideInInspector] public FormType currentFormType;

    public SpriteRenderer signatureRenderer;
    public SpriteRenderer companyWatermarkRenderer;
    public SpriteRenderer formText;
    public SpriteRenderer stampRenderer;
}
