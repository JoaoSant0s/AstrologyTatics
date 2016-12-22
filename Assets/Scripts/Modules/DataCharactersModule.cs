using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Editor.Layout;

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
        gemmini,
        aquarius
    }

    [SerializeField]
    CharacterType type;

    [SerializeField]
    Character module;

    [Header("Layout Moviment")]
    [SerializeField]
    ArrayLayout layout;

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

    public ArrayLayout Layout {
        get {
            return layout;
        }
    }

}

