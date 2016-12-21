using UnityEngine;
using System.Collections;

public class DataCharactersData : MonoBehaviour {

    [SerializeField]
    DataCharactersModule characterModule;

    static DataCharactersData instance;

    public static DataCharactersData Instance {
        get { return instance; }
    }

    public Character CharacterPrefab(CharacterDictionary.CharacterType type) {
        return characterModule.CharactersTypeList.Find(x => x.Type == type).Module;
    }

    void Awake() {
        instance  = this;
    }

}
