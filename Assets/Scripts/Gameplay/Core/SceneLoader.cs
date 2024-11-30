using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    
    string thisSceneName;
    [SerializeField] string nextSceneName;

    void Start(){
        thisSceneName = SceneManager.GetActiveScene().name;
    }

    public void ReloadScene(){
        SceneManager.LoadScene(thisSceneName);
    }

    public void LoadNextScene(){
        SceneManager.LoadScene(nextSceneName);
    }
}
