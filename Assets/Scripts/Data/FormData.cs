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
        

        [Header("Form Components State")]
        [HideInInspector] public StampAttributes.InputTypes stampState;
    }