using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{   

    [Header("Other Components")]
    [SerializeField] DialogueController bossDialogue;
    [SerializeField] FormData formData;

    void Start(){
        StartCoroutine(TutorialSequence());
    }

    IEnumerator TutorialSequence(){

        bossDialogue.PlayInAnimation();

        yield return new WaitForSeconds(4f);

        bossDialogue.ShowDialogueManual("Welcome to your desk! Cozy I know. And with real mahogany veneer too!");
        bossDialogue.StartYapping();

        yield return new WaitForSeconds(6f);

        bossDialogue.ShowDialogueManual(" ");
        bossDialogue.StopYapping();

        yield return new WaitForSeconds(2f);

        bossDialogue.ShowDialogueManual("You see on your left two stamps. One green. One red.");
        bossDialogue.StartYapping();

        yield return new WaitForSeconds(5f);

        bossDialogue.ShowDialogueManual(" ");
        bossDialogue.StopYapping();

        yield return new WaitForSeconds(2f);

        bossDialogue.ShowDialogueManual("TAP the SPACEBAR to pick which one you need, and HOLD it to select it!");
        bossDialogue.StartYapping();

        yield return new WaitForSeconds(8f);

        bossDialogue.ShowDialogueManual(" ");
        bossDialogue.StopYapping();

        yield return new WaitForSeconds(2f);

        bossDialogue.ShowDialogueManual("The green one Approves a work order, and the red one Denies an order. Go figure…");
        bossDialogue.StartYapping();

        yield return new WaitForSeconds(8f);

        bossDialogue.ShowDialogueManual(" ");
        bossDialogue.StopYapping();

        yield return new WaitForSeconds(2f);

        bossDialogue.ShowDialogueManual("Your job is to ensure these work orders are filled out correctly.");
        bossDialogue.StartYapping();

        yield return new WaitForSeconds(6f);

        bossDialogue.ShowDialogueManual(" ");
        bossDialogue.StopYapping();

        yield return new WaitForSeconds(1f);

        bossDialogue.ShowDialogueManual("Here's one coming down the pipe now!");
        bossDialogue.StartYapping();

        yield return new WaitForSeconds(4.5f);

        formData.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        bossDialogue.ShowDialogueManual(" ");
        bossDialogue.StopYapping();

         yield return new WaitForSeconds(1f);

        bossDialogue.ShowDialogueManual("This is a work order. As you can see, it's filled with all the info we need for a job.");
        bossDialogue.StartYapping();

        yield return new WaitForSeconds(5f);

        bossDialogue.ShowDialogueManual(" ");
        bossDialogue.StopYapping();

        yield return new WaitForSeconds(1f);

        bossDialogue.ShowDialogueManual("But what you need to be concerned with are these 3 things.The photo, the stamp, and the signature!");
        bossDialogue.StartYapping();

        yield return new WaitForSeconds(5f);

        bossDialogue.ShowDialogueManual(" ");
        bossDialogue.StopYapping();

        yield return new WaitForSeconds(1f);
    }
}
