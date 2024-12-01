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

    void Awake(){
        dialogueAnimator = GetComponent<Animator>();
    }

    void Start(){
        StartDialogue();
    }

    public void StartDialogue(){
        StartCoroutine(DialogueCoroutine());
    }

    IEnumerator DialogueCoroutine(){
        dialogueAnimator.Play(inAnimationName);
        dialogueTMP.text = Messages[Random.Range(0, Messages.Length)];

        yield return new WaitForSeconds(5.0f);

        dialogueAnimator.Play(outAnimationName);
    }
 }
