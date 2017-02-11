using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CampaingUIController : MonoBehaviour {

    public delegate void DefiningPlayers(List<Vector2> elements);
    public static event DefiningPlayers OnDefiningPlayers;

    public delegate void ActiveMenu(bool active);
    public static event ActiveMenu OnActiveMenu;

    [SerializeField]
    Button btnStart;
    [SerializeField]
    Button btnReturn;

    [SerializeField]
    Transform contentScrollbar;

    [SerializeField]
    ElementSlider slidePrefab;

    List<Vector2> sliderElement;

    void Start() {
        sliderElement = new List<Vector2>();
        InitSliderScrollbar();
        btnStart.onClick.AddListener(actionStart);
        btnReturn.onClick.AddListener(actionReturn);
    }

    void InitSliderScrollbar() {
        Player player = DuelController.Instance.PlayerHuman;        
        CharacterDictionary.CharacterType [] listType = (CharacterDictionary.CharacterType []) Enum.GetValues(typeof(CharacterDictionary.CharacterType));

        foreach(var type in listType) {            
            var listCharacters = player.ListCharactersData.FindAll(x => x.Type == type);
            
            if(listCharacters.Count > 0) {
                var elementSlider = GameObject.Instantiate(slidePrefab) as ElementSlider;
                elementSlider.SetMaxSliderValue(listCharacters.Count);
                elementSlider.SetName(type);
                elementSlider.transform.SetParent(contentScrollbar);
                
            }
        }
    }

    void UpdateSliderValues() {
        for (int i = 0; i < contentScrollbar.childCount; i++) {
            var element = contentScrollbar.GetChild(i).GetComponent< ElementSlider>();
            sliderElement.Add(new Vector2((int)element.CharacterType, element.SliderValue));
        }              
    }

    void actionStart() {
        UpdateSliderValues();
        gameObject.SetActive(false);
        Debug.Log("Slider Elements");
        foreach (var item in sliderElement) {
            Debug.Log(item);
        }
        if (OnDefiningPlayers != null) OnDefiningPlayers(sliderElement);
    }

    void actionReturn() {
        gameObject.SetActive(false);
        if (OnActiveMenu != null) OnActiveMenu(true);
    }

}
