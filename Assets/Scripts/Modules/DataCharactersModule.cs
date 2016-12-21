using UnityEngine;
using System.Collections.Generic;

public class DataCharactersModule : ScriptableObject {

    [SerializeField]
    List<CharacterDictionary> charactersType;

    public List<CharacterDictionary> CharactersTypeList {
        get {
            return charactersType;
        }
    }

}


[System.Serializable]
public struct CharacterDictionary {

    public enum CharacterType {
        gemmini
    }

    [SerializeField]
    CharacterType type;

    [SerializeField]
    Character module;


    public CharacterType Type {
        get {
            return type;
        }
    }

    public Character Module {
        get {
            return module;
        }
    }

}

