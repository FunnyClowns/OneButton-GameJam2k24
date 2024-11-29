using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FormFactory : MonoBehaviour
{

    public enum IncorrectFormVariation{
        MissingSignature,
        MissingCompanyWM,
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
    [HideInInspector] public FormData generatedFormData;

    void Awake(){
        GenerateForm();
    }

    public void GenerateForm(){
        var formRNG = Random.value;

        if (generatedForm != null){
            DeleteActiveForm();
        }

        generatedForm = Instantiate(formPrefab, formStartPosition, Quaternion.identity);
        
        if (!generatedForm.TryGetComponent<FormData>(out generatedFormData)){
            Debug.Log("Cant find generatedFormData in prefab");
            return;
        }

        if (formRNG > 0.5f){
            GenerateIncorrectForm();
        } else {
            GenerateCorrectForm();
        }

    }

    public void DeleteActiveForm(){
        Destroy(generatedForm);
    }

    void GenerateIncorrectForm(){ 
        generatedFormData.thisFormType = FormData.FormType.Incorrect;

        // generate random number of a enum length
        int rng = Random.Range(0, Enum.GetNames(typeof(IncorrectFormVariation)).Length);

        // chooses variation based on rng
        IncorrectFormVariation choosenFormVariation = (IncorrectFormVariation)rng;
        generatedFormData.thisVariation = choosenFormVariation;

        switch(choosenFormVariation){

            case IncorrectFormVariation.MissingSignature :
                generatedFormData.signatureText.text = " ";
                break;

            case IncorrectFormVariation.MissingCompanyWM :
                generatedFormData.companyWatermarkRenderer.color = Color.red;
                break;

            case IncorrectFormVariation.MissingStamp :
                generatedFormData.stampRenderer.sprite = wrongStamp[Random.Range(0, wrongStamp.Count)];
                break;

            default :
                Debug.Log("Choosen Variation is " + choosenFormVariation);
                break;

        }

        Debug.Log("Incorrect Form");

    }

    void GenerateCorrectForm(){
        generatedFormData.thisFormType = FormData.FormType.Correct;

        Debug.Log("Correct Form");
    }

}
