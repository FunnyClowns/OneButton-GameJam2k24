using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FormFactory : MonoBehaviour
{

    public enum IncorrectFormVariation{
        MissingSignature,
        WrongBuildingPhoto,
        WrongStampColor,
        WrongStampShape,
    }

    [Header("Form Components")]
    [SerializeField] Sprite correctStamp;
    [SerializeField] Sprite wrongStamp;
    [SerializeField] List<Sprite> correctBuildingPhoto;
    [SerializeField] List<Sprite> incorrectBuildingPhoto;
    [SerializeField] List<Color> correctStampColor;
    [SerializeField] List<Color> incorrectStampColor;


    [Header("Form Generation Data")]
    [SerializeField] Vector2 formStartPosition;
    GameObject generatedForm;

    
    [Header("Other Components")]
    [SerializeField] GameObject formPrefab;
    [SerializeField] SoundController sound;
   
    [HideInInspector] public FormData generatedFormData;

    void Start(){
        
        CreateForm();

        if (!generatedForm.TryGetComponent<FormData>(out generatedFormData)){
            Debug.LogError("Cant find form data at generated form");
            return;
        }
        
        ChoosesFormVariety();
    }

    void CreateForm(){
        generatedForm = Instantiate(formPrefab, formStartPosition, Quaternion.identity);
    }

    public void ChoosesFormVariety(){
        var formRNG = Random.value;
        
        if (!generatedForm.TryGetComponent<FormData>(out generatedFormData)){
            Debug.LogError("Cant find form data at generated form");
            return;
        }

        generatedFormData.PlayInAnimation();

        if (formRNG > 0.4f){
            GenerateIncorrectForm();
        } else {
            GenerateCorrectForm();
        }

    }
    public void DeleteActiveForm(){
        Destroy(generatedForm);

        Debug.Log ("Delete form");
    }

    void GenerateCorrectForm(){
        generatedFormData.thisFormType = FormData.FormType.Correct;

        ChooseCorrectFormVariation();

        Debug.Log("Correct Form");
    }

    void ChooseCorrectFormVariation(){
        generatedFormData.buildingPhotoRenderer.sprite = correctBuildingPhoto[Random.Range(0, correctBuildingPhoto.Count)];
        generatedFormData.stampRenderer.color = correctStampColor[Random.Range(0, correctStampColor.Count)];
    }

    void GenerateIncorrectForm(){ 
        generatedFormData.thisFormType = FormData.FormType.Incorrect;

        // chooses variation based on rng
        ChooseIncorrectFormVariation();

        return;
    }

    void ChooseIncorrectFormVariation(){
        // generate random number of a enum length
        int rng = Random.Range(0, Enum.GetNames(typeof(IncorrectFormVariation)).Length);

        IncorrectFormVariation choosenFormVariation = (IncorrectFormVariation)rng;
        generatedFormData.thisVariation = choosenFormVariation;

        switch(choosenFormVariation){

            case IncorrectFormVariation.MissingSignature :
                generatedFormData.signatureText.text = " ";
                break;

            case IncorrectFormVariation.WrongBuildingPhoto :
                generatedFormData.buildingPhotoRenderer.sprite = incorrectBuildingPhoto[Random.Range(0, incorrectBuildingPhoto.Count)];
                break;

            case IncorrectFormVariation.WrongStampColor :
                generatedFormData.stampRenderer.color = incorrectStampColor[Random.Range(0, incorrectStampColor.Count)];
                break;

            case IncorrectFormVariation.WrongStampShape :
                generatedFormData.stampRenderer.sprite = wrongStamp;
                break;

            default :
                Debug.Log("Choosen Variation is " + choosenFormVariation);
                break;
        }

        // checks if variation is not wrong building, then do create correct photo
        if (choosenFormVariation != IncorrectFormVariation.WrongBuildingPhoto)
            generatedFormData.buildingPhotoRenderer.sprite = correctBuildingPhoto[Random.Range(0, correctBuildingPhoto.Count)];

        if (choosenFormVariation != IncorrectFormVariation.WrongStampColor){
            generatedFormData.stampRenderer.color = correctStampColor[Random.Range(0, correctStampColor.Count)];
        }

        if (choosenFormVariation != IncorrectFormVariation.WrongStampShape){
            generatedFormData.stampRenderer.sprite = correctStamp;
        }

        Debug.Log("Incorrect Form");

    }

}
