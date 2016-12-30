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
        TilesController.OnDefiningCharacters += DefineLevel;
        LevelModule.OnSaveCharacters += SaveCharacters;
        Character.OnBlockClick += UpdateCharactersPaths;
        Character.OnRemoveCharacter += RemoveCharacter;
    }

    void OnDestroy() {
        TilesController.OnDefiningCharacters -= DefineLevel;
        LevelModule.OnSaveCharacters -= SaveCharacters;
        Character.OnBlockClick -= UpdateCharactersPaths;
        Character.OnRemoveCharacter -= RemoveCharacter;
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
