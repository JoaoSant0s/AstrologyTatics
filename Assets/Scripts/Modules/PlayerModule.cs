using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common.Layout;

public class PlayerModule : ScriptableObject {
    [SerializeField]
    List<Player> levels;    

    internal List<Player> Levels {
        get { 
            return levels;
        }
    }
}

[System.Serializable]
public class Player {

    public enum TypePlayer {
        machine,
        human
    }

    [SerializeField]
    string name;
    [SerializeField]
    TypePlayer type;
    [SerializeField]
    List<CharacterData> listCharactersData;

    List<Character> listCharacters;

    public List<CharacterData> ListCharactersData {
        get {
            return listCharactersData;
        }
    }

    public List<Character> ListCharacters {
        get {
            return listCharacters;
        }
    }

    public string Name{
        get {
            return name;
        }
    }

    internal void DefineCharacters(GameObject charactersSet) {
        listCharacters = new List<Character>();

        foreach (var character in listCharactersData) {
            var characterData = DataCharactersData.Instance.CharacterPrefab(character.Type);

            Vector3 position = LayoutDefinition.ConvertPostion(character.Position);
            var auxCharacter = GameObject.Instantiate(characterData.Module, position, Quaternion.identity) as Character;

            auxCharacter.name += " " + name;

            auxCharacter.SetPaths(characterData.Layout);
            auxCharacter.PostionCharacter = position;

            auxCharacter.transform.SetParent(charactersSet.transform);
            listCharacters.Add(auxCharacter);
        }               
    }  

    internal void RemoveCharacter(Character currentCharacter) {
        listCharacters.Remove(currentCharacter);
        GameObject.DestroyObject(currentCharacter.gameObject);
    }

    public override string ToString() {
        return name;
    }

}

