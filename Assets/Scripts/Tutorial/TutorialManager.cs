using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{   

    [HideInInspector] public bool waitingForInput = false;

    [Header("Other Components")]
    [SerializeField] DialogueController bossDialogue;
    public FormData formData;
    

    void Start(){
        StartCoroutine(TutorialSequence());
    }

    IEnumerator TutorialSequence(){

        bossDialogue.PlayInAnimation();

        yield return new WaitForSeconds(3f);

        bossDialogue.ShowDialogueManual("Welcome to your desk! Cozy I know. And with real mahogany veneer too!");
        bossDialogue.StartYapping();

        yield return new WaitForSeconds(6f);

        bossDialogue.ShowDialogueManual(" ");
        bossDialogue.StopYapping();

        yield return new WaitForSeconds(1f);

        bossDialogue.ShowDialogueManual("You see on your left two stamps. One green. One red.");
        bossDialogue.StartYapping();

        yield return new WaitForSeconds(5f);

        bossDialogue.ShowDialogueManual(" ");
        bossDialogue.StopYapping();

        yield return new WaitForSeconds(1f);

        bossDialogue.ShowDialogueManual("TAP the SPACEBAR to pick which one you need, and HOLD it to select it!");
        bossDialogue.StartYapping();

        yield return new WaitForSeconds(8f);

        bossDialogue.ShowDialogueManual(" ");
        bossDialogue.StopYapping();

        yield return new WaitForSeconds(1f);

        bossDialogue.ShowDialogueManual("The green one Approves a work order, and the red one Denies an order. Go figure…");
        bossDialogue.StartYapping();

        yield return new WaitForSeconds(8f);

        bossDialogue.ShowDialogueManual(" ");
        bossDialogue.StopYapping();

        yield return new WaitForSeconds(1f);

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

        yield return new WaitForSeconds(1f);

        bossDialogue.ShowDialogueManual(" ");
        bossDialogue.StopYapping();

         yield return new WaitForSeconds(1f);

        bossDialogue.ShowDialogueManual("This is a work order. As you can see, it's filled with all the info we need for a job.");
        bossDialogue.StartYapping();

        yield return new WaitForSeconds(5f);

        bossDialogue.ShowDialogueManual(" ");
        bossDialogue.StopYapping();

        yield return new WaitForSeconds(1f);

        bossDialogue.ShowDialogueManual("But what you need to be concerned with are these 3 things. The photo, the stamp, and the signature!");
        bossDialogue.StartYapping();

        yield return new WaitForSeconds(5f);

        bossDialogue.ShowDialogueManual(" ");
        bossDialogue.StopYapping();

        yield return new WaitForSeconds(1f);

        bossDialogue.ShowDialogueManual("If the photo has a picture of anything other than a dome, if ain’t for us. This is obviously a dome, so we’re all good here!");
        bossDialogue.StartYapping();

        yield return new WaitForSeconds(8f);

        bossDialogue.ShowDialogueManual(" ");
        bossDialogue.StopYapping();

        yield return new WaitForSeconds(1f);

        bossDialogue.ShowDialogueManual("We got the company stamp. If it’s missing or in any colour other than (COLOUR), it’s incorrect.");
        bossDialogue.StartYapping();

        yield return new WaitForSeconds(5f);

        bossDialogue.ShowDialogueManual(" ");
        bossDialogue.StopYapping();

        yield return new WaitForSeconds(1f);

        bossDialogue.ShowDialogueManual("Then you got the signature. You wouldn’t believe how many times the drones forget to sign their paperwork.");
        bossDialogue.StartYapping();

        yield return new WaitForSeconds(5.5f);

        bossDialogue.ShowDialogueManual(" ");
        bossDialogue.StopYapping();

        yield return new WaitForSeconds(1f);

        bossDialogue.ShowDialogueManual("This looks to be all in order so let’s send it on its way");
        bossDialogue.StartYapping();

        yield return new WaitForSeconds(3.5f);

        bossDialogue.StopYapping();

        waitingForInput = true;
        while(!formData.formSubmitted){
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        bossDialogue.ShowDialogueManual("Easy right? You got a few minutes before the day starts. Go grab a coffee and a donut");
        bossDialogue.StartYapping();

        yield return new WaitForSeconds(4.5f);

        bossDialogue.ShowDialogueManual(" ");
        bossDialogue.StopYapping();

        yield return new WaitForSeconds(1f);

        bossDialogue.ShowDialogueManual("ITS TIME!!");
        bossDialogue.StartYapping();

        yield return new WaitForSeconds(4f);

        bossDialogue.ShowDialogueManual(" ");
        bossDialogue.StopYapping();

        yield return new WaitForSeconds(1f);
    }
}
