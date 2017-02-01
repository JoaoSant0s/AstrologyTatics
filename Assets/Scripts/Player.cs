using UnityEngine;
using System.Collections.Generic;
using Common.Layout;

[System.Serializable]
public class Player {

    public delegate void UpdateTurn();
    public static event UpdateTurn OnUpdateTurn;

    public enum TypePlayer {
        machine,
        human
    }

    [SerializeField]
    string name;

    [SerializeField]
    TypePlayer type;

    [SerializeField]
    ColorKey.ColorKeyList currentColor;

    [SerializeField]
    List<CharacterData> listCharactersData;

    List<Character> listCharacters;
    int turnNumberController;    

    public Color CurrentColor {
        get {
            return ColorKey.GetColor(currentColor);
        }     
    }

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

    public string Name {
        get {
            return name;
        }
    }

    internal void DefineCharacters(GameObject charactersSet) {
        listCharacters = new List<Character>();
        turnNumberController = 1;

        foreach (var auxCharacter in listCharactersData) {
            var characterData = DataCharactersData.Instance.CharacterPrefab(auxCharacter.Type);

            Vector3 position = LayoutDefinition.ConvertPostion(auxCharacter.Position);
            var character = GameObject.Instantiate(characterData.Module, position, Quaternion.identity) as Character;

            character.name += " " + name;

            character.SetPaths(characterData.Layout);
            character.Player = this;
            character.PostionCharacter = position;
            character.UpdateColor();

            character.transform.SetParent(charactersSet.transform);
            listCharacters.Add(character);
        }
    }

    internal void NextMovement() {
        Debug.Log(listCharacters.Count);      
        if (turnNumberController >= listCharacters.Count) {
            turnNumberController = 1;
            if (OnUpdateTurn != null) OnUpdateTurn();
        } else {
            turnNumberController++;
        }        
    }

    internal bool RemoveCharacter(Character currentCharacter) {
        if(listCharacters.Contains(currentCharacter)) {
            listCharacters.Remove(currentCharacter);
            return true;
        }
        return false;
    }

    public override string ToString() {
        return name;
    }

}
