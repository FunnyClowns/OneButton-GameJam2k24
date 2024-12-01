using System.Collections;
using DigitalRuby.SoundManagerNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    
    [Header("Setting")]
    [SerializeField] string nextSceneName;
    string thisSceneName;

    [Header("Other Sources")]
    [SerializeField] Sprite nextDaySprite;
    [SerializeField] Image dayScreen;
    
    void Start(){
        thisSceneName = SceneManager.GetActiveScene().name;
    }

    public void ReloadScene(){
        SceneManager.LoadScene(thisSceneName);
    }

    public void LoadNextScene(){
        StartCoroutine(LoadNextSceneCoroutine());
    }

    IEnumerator LoadNextSceneCoroutine(){
        
        SoundManager.StopAll();

        dayScreen.sprite = nextDaySprite;
        dayScreen.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(nextSceneName);
    }
}
