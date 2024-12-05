using UnityEngine;
using UnityEngine.UI;

public class SliderFiller : MonoBehaviour
{

    [SerializeField] float valueMultiplier;
    ISliderValue sliderValue;
    bool succeedGetValue;


    [Header("Other Components")]
    [SerializeField] Transform targetObject;
    
    

    [HideInInspector] public bool shouldUpdate = true;

    Slider slider;
    
    
    void Awake(){
        // initialize variables
        slider = GetComponent<Slider>();
 
        if (targetObject.TryGetComponent<ISliderValue>(out sliderValue)){

            succeedGetValue = true;
            Debug.Log("Succeed get value");
        } 
        
    }
    
    void Update(){

        if (succeedGetValue && shouldUpdate){

            // Debug.Log("Slider = " + sliderValue);

            slider.value = sliderValue.GetSliderValue() * valueMultiplier;

            // Debug.Log("Value = " + sliderValue.GetSliderValue());
        }
    }

    public void ResetSlider(){
        slider.value = 0;
    }
    
}
