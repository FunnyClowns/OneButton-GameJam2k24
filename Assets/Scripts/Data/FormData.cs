using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FormData : MonoBehaviour
{
    public enum FormType{
        Correct,
        Incorrect,
    }

    [HideInInspector] public FormType thisFormType;
    [HideInInspector] public FormFactory.IncorrectFormVariation thisVariation;



    [Header("Form Sprites"), SerializeField]
    Sprite approvedStamp;
    [SerializeField] Sprite deniedStamp;

    [Header("Form Components")]
    public TextMeshPro signatureText;
    public SpriteRenderer buildingPhotoRenderer;
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

        if (thisFormType == FormType.Correct || thisVariation != FormFactory.IncorrectFormVariation.MissingSignature){
            signatureText.text = GetRandomSignatureName();
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
        // stamp
        RandomizeTransformPosition(stampRenderer.transform, 0.05f, 0.05f);
        RandomizeTransformScale(stampRenderer.transform, 1.1f);
        RandomizeTransformRotation(stampRenderer.transform, 15f);

        // building photo
        RandomizeTransformPosition(buildingPhotoRenderer.transform, 0f, 0.8f);
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

    string GetRandomSignatureName(){

        string[] clientNames = {"Aaron", "Adam", "Adrian", "Albert", "Alexander", "Andrew", "Arthur", "Austin", 
        "Benjamin", "Bernard", "Blake", "Bradley", "Brandon", "Brian", "Bruce", "Caleb", 
        "Carl", "Charles", "Charlie", "Chris", "Christopher", "Colin", "Connor", "Craig", 
        "Daniel", "Darren", "David", "Dennis", "Dominic", "Douglas", "Dylan", "Edward", 
        "Elliot", "Elijah", "Elliott", "Emmanuel", "Eric", "Ethan", "Eugene", "Felix", 
        "Francis", "Frederick", "Gabriel", "George", "Grant", "Gregory", "Harry", "Harvey", 
        "Hector", "Henry", "Hugh", "Isaac", "Jack", "Jacob", "James", "Jason", "Jeffrey", 
        "Jeremy", "Jerry", "Jesse", "John", "Jonathan", "Jordan", "Joseph", "Joshua", 
        "Julian", "Kenneth", "Kevin", "Liam", "Louis", "Lucas", "Luke", "Mark", "Martin", 
        "Matthew", "Michael", "Nathan", "Nicholas", "Oliver", "Oscar", "Patrick", "Paul", 
        "Peter", "Philip", "Ray", "Richard", "Robert", "Samuel", "Simon", "Stephen", "Steve", 
        "Thomas", "Timothy", "Toby", "Tony", "Victor", "Vincent", "William", "Zachary", "Zane",
        "Kizons", "Kitty", "Pendrinho", "Riley", "Katie"};

        var choosenName = clientNames[Random.Range(0, clientNames.Length - 1)];

        return choosenName;
    }
}
