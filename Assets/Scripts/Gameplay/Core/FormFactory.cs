using System;
using System.Collections.Generic;
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

    [Header("Form Sprites")]
    [SerializeField] Sprite correctStamp;
    [SerializeField] List<Sprite> wrongStamp;

    [Header("Form Generation Data")]
    [SerializeField] Vector2 formStartPosition;

    
    [Header("Other Components")]
    [SerializeField] GameObject formPrefab;
    GameObject generatedForm;
    [HideInInspector] public FormData formData;

    void Awake(){
        GenerateForm();
    }

    public void GenerateForm(){
        var formRNG = Random.value;

        if (generatedForm != null){
            Destroy(generatedForm);
        }

        generatedForm = Instantiate(formPrefab, formStartPosition, Quaternion.identity);
        
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
                formData.stampRenderer.sprite = wrongStamp[Random.Range(0, wrongStamp.Count)];
                break;

            default :
                Debug.Log("Choosen Variation is " + choosenFormVariation);
                break;

        }

        Debug.Log("Incorrect Form");

    }

    void GenerateCorrectForm(){
        formData.currentFormType = FormData.FormType.Correct;

        Debug.Log("Correct Form");
    }

}
