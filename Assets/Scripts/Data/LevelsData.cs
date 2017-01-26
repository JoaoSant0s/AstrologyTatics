using UnityEngine;
using System.Collections.Generic;

public class LevelsData : MonoBehaviour {

    public enum TypeClick {
        selectCharacter,
        destroyCharacter,
        none
    }
    [SerializeField]
    LevelsModule levelsModule;

    [SerializeField]
    GameObject charactersSet;

    List<Character> listCharacters;

    void Awake() {
        //CampaingUIController.OnDefiningPlayers += DefineLevel;
        //Character.OnBlockClick += UpdateCharactersPaths;
        //Character.OnRemoveCharacter += RemoveCharacter;
        //LevelModule.OnSaveCharacters += SaveCharacters;
    }

    void OnDestroy() {
        //CampaingUIController.OnDefiningPlayers -= DefineLevel;
        //Character.OnBlockClick -= UpdateCharactersPaths;
        //Character.OnRemoveCharacter -= RemoveCharacter;
        //LevelModule.OnSaveCharacters -= SaveCharacters;
    }

    bool UpdateCharactersPaths(Character player) {
        var listActive = listCharacters.FindAll(x => x != player && x.ActivePaths);
        if (listActive.Count > 0) return true;
        return false;
    }

    void DefineLevel() {
        levelsModule.DefineLevel(charactersSet);
    }

    void SaveCharacters(List<Character> newList) {
        listCharacters = newList;
    }

    void RemoveCharacter(Character currentCharacter) {
        listCharacters.Remove(currentCharacter);
        DestroyObject(currentCharacter.gameObject);
    }

    

}
