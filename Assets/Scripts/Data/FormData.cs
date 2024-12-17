using TMPro;
using UnityEngine;

public class FormData : MonoBehaviour {
        public enum FormType{
            Correct,
            Incorrect,
        }

        [HideInInspector] public FormType thisFormType;
        [HideInInspector] public FormFactory.IncorrectFormVariation thisVariation;

        [HideInInspector] public bool formSubmitted;
        [HideInInspector] public bool formStamped;



        [Header("Form Sprites")]
        public Sprite approvedStamp;
        public Sprite deniedStamp;

        [Header("Form Components State")]
        [HideInInspector] public StampAttributes.InputTypes stampState;


        [Header("Form Components")]
        public TextMeshPro signatureText;
        public SpriteRenderer buildingPhotoRenderer;
        public SpriteRenderer stampRenderer;
        public GameObject decisionStamp;
        [HideInInspector] public Animator formAnimator;

        [HideInInspector] public SpriteRenderer decisionStampRenderer;
        [HideInInspector] public Animator decisionStampAnimator;

        void Awake(){   
            if (!SucceedInitializeComponents()){
                Debug.LogError("Cant fully initialize components here");
            }
            
            VariateObjectTransform();
        }

        bool SucceedInitializeComponents(){
            if (!decisionStamp.TryGetComponent<Animator>(out decisionStampAnimator))
                return false;

            if (!decisionStamp.TryGetComponent<SpriteRenderer>(out decisionStampRenderer))
                return false;

            if (!TryGetComponent<Animator>(out formAnimator))
                return false;
            
            return true;
        }

        public void VariateObjectTransform(){
            // stamp
            RandomizeTransformPosition(stampRenderer.transform, 0.02f, 0.02f);
            RandomizeTransformRotation(stampRenderer.transform, 15f);

            // building photo
            RandomizeTransformPosition(buildingPhotoRenderer.transform, 0f, 0.05f);
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

        public void SetSignatureRandomName(){

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

            signatureText.text = choosenName;
        }
    }