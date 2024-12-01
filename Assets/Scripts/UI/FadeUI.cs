using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{

    Image targetImage;
    [SerializeField] float waitTime;

    void Start(){
        targetImage = GetComponent<Image>();
    }

    public IEnumerator FadeOut() {

        for (float i = 1; i >= 0; i -= Time.deltaTime) {
            // set color with i as alpha
            targetImage.color = new Color(1, 1, 1, i);
            Debug.Log(i);

            if (i <= 0.01f)
                targetImage.color = new Color(1, 1, 1, 0);

            yield return null;
        }    
    }

    public IEnumerator FadeIn(){
        // loop over 1 second
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            targetImage.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    public IEnumerator FadeInOut(float waitSeconds){
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            targetImage.color = new Color(1, 1, 1, i);
            yield return null;
        }

        yield return new WaitForSeconds(waitSeconds);

        for (float i = 1; i >= 0; i -= Time.deltaTime) {
            // set color with i as alpha
            targetImage.color = new Color(1, 1, 1, i);
            Debug.Log(i);

            if (i <= 0.01f)
                targetImage.color = new Color(1, 1, 1, 0);

            yield return null;
        }
    }
}
