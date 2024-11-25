using UnityEngine;
using UnityEngine.UI;

public class SliderFiller : MonoBehaviour
{
    [SerializeField] Transform targetObject;
    [SerializeField] float valueMultiplier;
    ISliderValue sliderValue;
    bool succeedGetValue;

    [HideInInspector] public bool shouldUpdate = true;

    Slider slider;
    
    
    void Awake(){
        // initialize variables
        slider = GetComponent<Slider>();
        if (targetObject.TryGetComponent<ISliderValue>(out sliderValue)){
            succeedGetValue = true;
        }
    }
    
    void Update(){

        if (succeedGetValue && shouldUpdate){
            slider.value = sliderValue.GetSliderValue() * valueMultiplier;

            // Debug.Log("Value = " + sliderValue.GetSliderValue());
        }
    }

    public void ResetSlider(){
        slider.value = 0;
    }
    
}
