using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FormFactory : MonoBehaviour
{

    enum IncorrectFormVariation{
        MissingSignature,
        MissingCompanyWM,
        Forged,
        MissingStamp,
    }

    [SerializeField] GameObject formPrefab;
    GameObject generatedForm;
    FormData formData;


    void Awake(){
        GenerateForm();

    }

    void GenerateForm(){
        var formRNG = Random.value;

        generatedForm = Instantiate(formPrefab, Vector3.zero, Quaternion.identity);
        
        if (!generatedForm.TryGetComponent<FormData>(out formData)){
            Debug.Log("Cant find formdata in prefab");
            return;
        }

        if (formRNG > 0.5f){
            GenerateIncorrectForm();
        } else {
            GenerateCorrectForm();
        }

    }

    void GenerateIncorrectForm(){ 
        formData.currentFormType = FormData.FormType.Incorrect;

        // generate random number of a enum length
        int rng = Random.Range(0, Enum.GetNames(typeof(IncorrectFormVariation)).Length);

        // chooses variation based on rng
        IncorrectFormVariation choosenFormVariation = (IncorrectFormVariation)rng;

        switch(choosenFormVariation){

            case IncorrectFormVariation.MissingSignature :
                formData.signatureRenderer.color = Color.red;
                break;

            case IncorrectFormVariation.MissingCompanyWM :
                formData.companyWatermarkRenderer.color = Color.red;
                break;
            
            case IncorrectFormVariation.Forged :
                formData.formText.color = Color.red;
                break;

            case IncorrectFormVariation.MissingStamp :
                formData.stampRenderer.color = Color.red;
                break;

            default :
                Debug.Log("Choosen Variation is " + choosenFormVariation);
                break;

        }

        Debug.Log("Incorrect Form");

    }

    void GenerateCorrectForm(){
        formData.currentFormType = FormData.FormType.Incorrect;
        
        Debug.Log("Correct Form");
    }

}
