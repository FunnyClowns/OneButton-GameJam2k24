using UnityEngine;

public class PlayerData : MonoBehaviour
{
    
    public int liveAmount = 3;
    [HideInInspector] public int currentLive;

    void Awake(){

        // initialize variables
        currentLive = liveAmount;
    }

    public void ChangeLiveAmount(int amount){
        currentLive += amount;

        if (currentLive <= 0){
            Death();
            return;
        }
    }

    void Death(){
        Debug.Log("Death");

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }


}
