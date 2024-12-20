using System;
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

    [Header("Form Variables")]
    FormData formData;
    FormAttributes formAttributes;
    FormStyleData _activeFormStyle;

    
    [Header("Other Components")]
    [SerializeField] GameObject formPrefab;
    [SerializeField] SoundController sound;

    void Start(){
        
        formData = FindObjectOfType<FormData>();
        formAttributes = FindObjectOfType<FormAttributes>();

        _activeFormStyle = formAttributes.activeStyle;
        
        RecycleForm();
    }

    public void RecycleForm(){
        formAttributes.SetSignatureRandomName();
        formAttributes.stampRenderer.sprite = _activeFormStyle.validStampSprite;
        formAttributes.VariateObjectTransform();

        ChooseRandomFormType();
    }

    void ChooseRandomFormType(){
        var formRNG = Random.value;

        if (formRNG > 0.4f){
            SetFormToIncorrect();
        } else {
            SetFormToCorrect();
        }

    }

    void SetFormToCorrect(){
        formData.thisFormType = FormData.FormType.Correct;

        SetCorrectVariation();

        Debug.Log("Correct Form");
    }

    void SetCorrectVariation(){
        formAttributes.buildingPhotoRenderer.sprite = _activeFormStyle.validBuildingSprites[Random.Range(0, _activeFormStyle.validBuildingSprites.Count)];
        formAttributes.stampRenderer.color = _activeFormStyle.validStampColors[Random.Range(0, _activeFormStyle.validStampColors.Count)];
    }

    void SetFormToIncorrect(){ 
        formData.thisFormType = FormData.FormType.Incorrect;

        // chooses variation based on rng
        SetIncorrectVariation();

        return;
    }

    void SetIncorrectVariation(){
        // generate random number of a enum length
        int rng = Random.Range(0, Enum.GetNames(typeof(IncorrectFormVariation)).Length);

        IncorrectFormVariation choosenFormVariation = (IncorrectFormVariation)rng;
        formData.thisVariation = choosenFormVariation;

        switch(choosenFormVariation){

            case IncorrectFormVariation.MissingSignature :
                formAttributes.signatureText.text = " ";
                break;

            case IncorrectFormVariation.WrongBuildingPhoto :
                formAttributes.buildingPhotoRenderer.sprite = _activeFormStyle.invalidBuildingSprites[Random.Range(0, _activeFormStyle.invalidBuildingSprites.Count)];
                break;

            case IncorrectFormVariation.WrongStampColor :
                formAttributes.stampRenderer.color = _activeFormStyle.invalidStampColors[Random.Range(0, _activeFormStyle.invalidStampColors.Count)];
                break;

            case IncorrectFormVariation.WrongStampShape :
                formAttributes.stampRenderer.sprite = _activeFormStyle.invalidStampSprites[Random.Range(0, _activeFormStyle.invalidStampSprites.Count)];
                break;

            default :
                Debug.Log("Choosen Variation is " + choosenFormVariation);
                break;
        }

        // checks if variation is not wrong building, then do create correct photo
        if (choosenFormVariation != IncorrectFormVariation.WrongBuildingPhoto)
            formAttributes.buildingPhotoRenderer.sprite = _activeFormStyle.validBuildingSprites[Random.Range(0, _activeFormStyle.validBuildingSprites.Count)];

        if (choosenFormVariation != IncorrectFormVariation.WrongStampColor){
            formAttributes.stampRenderer.color = _activeFormStyle.validStampColors[Random.Range(0, _activeFormStyle.validStampColors.Count)];
        }

        if (choosenFormVariation != IncorrectFormVariation.WrongStampShape){
            formAttributes.stampRenderer.sprite = _activeFormStyle.validStampSprite;
        }

        Debug.Log("Incorrect Form");

    }

}
