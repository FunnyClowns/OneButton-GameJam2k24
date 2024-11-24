using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateForm : MonoBehaviour
{
    
    // form behaviour
    enum FormType{
        Correct,
        Incorrect,
        PinkSlips
    }

    enum IncorrectFormVariation{
        MissingSignature,
        MissingCompanyWM,
        Forged,
        MissingStamp,
    }

    FormType currentFormType;
    FormType nextFormType;
    float formRNG;


    // form components
    [SerializeField] SpriteRenderer signatureRenderer;
    [SerializeField] SpriteRenderer companyWatermarkRenderer;
    [SerializeField] SpriteRenderer formText;
    [SerializeField] SpriteRenderer stampRenderer;


    void Awake(){
        DefineNextFormType();

    }

    void DefineNextFormType(){
        formRNG = Random.value;

        if (formRNG > 0.5f){
            GenerateIncorrectForm();
        } else {
            GenerateCorrectForm();
        }

    }

    void GenerateIncorrectForm(){

        nextFormType = FormType.Incorrect;
        

        // generate random number of a enum length
        int rng = Random.Range(0, Enum.GetNames(typeof(IncorrectFormVariation)).Length);

        // chooses variation based on rng
        IncorrectFormVariation choosenFormVariation = (IncorrectFormVariation)rng;

        switch(choosenFormVariation){

            case IncorrectFormVariation.MissingSignature :
                signatureRenderer.color = Color.red;
                break;

            case IncorrectFormVariation.MissingCompanyWM :
                companyWatermarkRenderer.color = Color.red;
                break;
            
            case IncorrectFormVariation.Forged :
                formText.color = Color.red;
                break;

            case IncorrectFormVariation.MissingStamp :
                stampRenderer.color = Color.red;
                break;

            default :
                Debug.Log("Choosen Variation is " + choosenFormVariation);
                break;

        }

        Debug.Log("Incorrect Form");

    }

    void GenerateCorrectForm(){
        nextFormType = FormType.Correct;

        Debug.Log("Correct Form");
    }

}
