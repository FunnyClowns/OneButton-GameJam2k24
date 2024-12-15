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
    FormData thisSceneForm;
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
        
        thisSceneForm = FindObjectOfType<FormData>();
        
        ChooseRandomFormType();
    }

    public void ChooseRandomFormType(){
        var formRNG = Random.value;

        if (formRNG > 0.4f){
            SetFormToIncorrect();
        } else {
            SetFormToCorrect();
        }

        thisSceneForm.VariateObjectTransform();

    }

    void SetFormToCorrect(){
        thisSceneForm.thisFormType = FormData.FormType.Correct;

        SetCorrectVariation();

        Debug.Log("Correct Form");
    }

    void SetCorrectVariation(){
        thisSceneForm.buildingPhotoRenderer.sprite = correctBuildingPhoto[Random.Range(0, correctBuildingPhoto.Count)];
        thisSceneForm.stampRenderer.color = correctStampColor[Random.Range(0, correctStampColor.Count)];
    }

    void SetFormToIncorrect(){ 
        thisSceneForm.thisFormType = FormData.FormType.Incorrect;

        // chooses variation based on rng
        SetIncorrectVariation();

        return;
    }

    void SetIncorrectVariation(){
        // generate random number of a enum length
        int rng = Random.Range(0, Enum.GetNames(typeof(IncorrectFormVariation)).Length);

        IncorrectFormVariation choosenFormVariation = (IncorrectFormVariation)rng;
        thisSceneForm.thisVariation = choosenFormVariation;

        switch(choosenFormVariation){

            case IncorrectFormVariation.MissingSignature :
                thisSceneForm.signatureText.text = " ";
                break;

            case IncorrectFormVariation.WrongBuildingPhoto :
                thisSceneForm.buildingPhotoRenderer.sprite = incorrectBuildingPhoto[Random.Range(0, incorrectBuildingPhoto.Count)];
                break;

            case IncorrectFormVariation.WrongStampColor :
                thisSceneForm.stampRenderer.color = incorrectStampColor[Random.Range(0, incorrectStampColor.Count)];
                break;

            case IncorrectFormVariation.WrongStampShape :
                thisSceneForm.stampRenderer.sprite = wrongStamp;
                break;

            default :
                Debug.Log("Choosen Variation is " + choosenFormVariation);
                break;
        }

        // checks if variation is not wrong building, then do create correct photo
        if (choosenFormVariation != IncorrectFormVariation.WrongBuildingPhoto)
            thisSceneForm.buildingPhotoRenderer.sprite = correctBuildingPhoto[Random.Range(0, correctBuildingPhoto.Count)];

        if (choosenFormVariation != IncorrectFormVariation.WrongStampColor){
            thisSceneForm.stampRenderer.color = correctStampColor[Random.Range(0, correctStampColor.Count)];
        }

        if (choosenFormVariation != IncorrectFormVariation.WrongStampShape){
            thisSceneForm.stampRenderer.sprite = correctStamp;
        }

        Debug.Log("Incorrect Form");

    }

}
