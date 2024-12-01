using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    
    enum DialogueType{
        LocalControlled,
        PublicControlled,    
    }

    [Header("Adjustment")]
    
    [SerializeField] DialogueType thisDialogueType;
    [SerializeField] string inAnimationName;
    [SerializeField] string outAnimationName;
    [SerializeField] string[] Messages;

    bool isPlaying = false;


    [Header("Other Components")]
    [SerializeField] TextMeshProUGUI dialogueTMP;
    Animator dialogueAnimator;
    [SerializeField] SoundController sound;

    void Awake(){
        dialogueAnimator = GetComponent<Animator>();
    }

    public void StartDialogue(){
        if (!isPlaying)
            StartCoroutine(RandomDialogueCoroutine());
    }

    IEnumerator RandomDialogueCoroutine(){
        isPlaying = true;

        PlayInAnimation();
        StartYapping();
        
        dialogueTMP.text = Messages[Random.Range(0, Messages.Length)];

        yield return new WaitForSeconds(5.0f);

        PlayOutAnimation();
        StopYapping();
        

        isPlaying = false;
    }

    public void ShowDialogueManual(string message){
        dialogueTMP.text = message;
    }

    public void PlayInAnimation(){
        dialogueAnimator.Play(inAnimationName);
    }

    public void PlayOutAnimation(){
        dialogueAnimator.Play(outAnimationName);
    }

    public void StartYapping(){
        sound.PlaySoundOnceOverload(3, 0.2f);
    }

    public void StopYapping(){
        sound.StopSound(3);
    }
 }
