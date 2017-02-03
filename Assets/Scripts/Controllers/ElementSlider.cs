using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ElementSlider : MonoBehaviour {
    
    [SerializeField]
    Text typeName;
    [SerializeField]
    Slider slider;

    [SerializeField]
    Text currentSlideValue;

    CharacterDictionary.CharacterType type;
    int sliderValue;

    public CharacterDictionary.CharacterType CharacterType {
        get {
            return type;
        }
    }

    public int SliderValue {
        get {
            return sliderValue;
        }

        set {
            sliderValue = value;
        }
    }

    public void Start() {
        slider.value = 0;
        sliderValue = 0;
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }
   
    public void ValueChangeCheck() {
        sliderValue = (int)(slider.value);
        currentSlideValue.text = ((int)(slider.value ) ).ToString();
    }

    public void SetName(CharacterDictionary.CharacterType characterType) {
        type = characterType;
        typeName.text = type.ToString();
    }

    public void SetMaxSliderValue(int maxValue) {
        slider.minValue = 0;
        slider.maxValue = maxValue;
    }

}
