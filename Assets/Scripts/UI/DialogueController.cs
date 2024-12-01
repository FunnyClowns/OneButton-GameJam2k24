using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueTMP;
    Animator dialogueAnimator;
    [SerializeField] string inAnimationName;
    [SerializeField] string outAnimationName;
    [SerializeField] string[] Messages;

    bool isPlaying = false;


    [Header("Other Components")]
    [SerializeField] SoundController sound;

    void Awake(){
        dialogueAnimator = GetComponent<Animator>();
    }

    public void StartDialogue(){
        if (!isPlaying)
            StartCoroutine(DialogueCoroutine());
    }

    IEnumerator DialogueCoroutine(){
        isPlaying = true;

        dialogueAnimator.Play(inAnimationName);
        dialogueTMP.text = Messages[Random.Range(0, Messages.Length)];
        sound.PlaySoundOnceOverload(3, 0.2f);

        yield return new WaitForSeconds(5.0f);

        dialogueAnimator.Play(outAnimationName);
        sound.StopSound(3);

        isPlaying = false;
    }
 }
