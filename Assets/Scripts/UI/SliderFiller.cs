using UnityEngine;
using UnityEngine.UI;

public class SliderFiller : MonoBehaviour
{
    [SerializeField] Transform targetObject;
    [SerializeField] float valueMultiplier;
    ISliderValue sliderValue;
    bool succeedGetValue;

    Slider slider;
    
    
    void Awake(){
        // initialize variables
        slider = GetComponent<Slider>();
        if (targetObject.TryGetComponent<ISliderValue>(out sliderValue)){
            succeedGetValue = true;
        }
    }
    
    void Update(){

        if (succeedGetValue){
            slider.value = sliderValue.GetSliderValue() * valueMultiplier;

            // Debug.Log("Value = " + sliderValue.GetSliderValue());
        }
    }
    
}
