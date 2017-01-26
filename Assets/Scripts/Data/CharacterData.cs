using UnityEngine;
using System.Collections;

[System.Serializable]
public class CharacterData {

    [SerializeField]
    Vector3 position;
    [SerializeField]
    int amount;
    [SerializeField]
    CharacterDictionary.CharacterType type;

    public int Amount {
        get { return amount; }
    }

    public Vector3 Position {
        get { return position; }
    }

    public CharacterDictionary.CharacterType Type {
        get { return type; }
    }


}
