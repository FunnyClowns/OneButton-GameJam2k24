using UnityEngine;

public class FormInteraction : MonoBehaviour {

    FormData data;

    void Start(){
        data = GetComponent<FormData>();
    }

    public void StampThis(){
        data.SubmitForm(InputHandler.InputType.Accept);
    }

    public void WriteSomething(){

    }

    public void SignForm(){

    }
}