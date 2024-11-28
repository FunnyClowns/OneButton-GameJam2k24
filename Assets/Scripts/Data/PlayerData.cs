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

        GameState.GameLose();
    }


}
