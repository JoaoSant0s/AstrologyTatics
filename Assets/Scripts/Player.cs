using UnityEngine;
using System.Collections.Generic;
using Common.Layout;
using System;

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
    List<Vector2> elementsSelected;
    int turnNumberController;    

    public TypePlayer Type {
        get {
            return type;
        }
    }
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

    public void SetLimitElements(List<Vector2> elements) {
        elementsSelected = elements;
    }

    public void DefineCharacters(GameObject charactersSet) {
        listCharacters = new List<Character>();
        turnNumberController = 1;        

        foreach (var auxCharacter in listCharactersData) {
            
            if (elementsSelected.Count > 0) {
                var auxCharac = elementsSelected.Find(element => element.x == (int)auxCharacter.Type);
                elementsSelected.Remove(auxCharac);    
                var continueCharacter = auxCharac.y <= 0;
                auxCharac -= (new Vector2(0, 1));
                elementsSelected.Add(auxCharac);
                if (continueCharacter) continue;
            }
            
            var characterData = DataCharactersData.Instance.CharacterPrefab(auxCharacter.Type);

            Vector3 position = LayoutDefinition.ConvertPostion(auxCharacter.Position);
            var character = GameObject.Instantiate(characterData.Module, position, Quaternion.identity) as Character;

            character.name += " " + name;

            character.SetPaths(characterData.Layout);
            character.Type = auxCharacter.Type;
            character.Player = this;
            character.PostionCharacter = position;
            character.UpdateColor();

            character.transform.SetParent(charactersSet.transform);
            listCharacters.Add(character);
        }
    }

   

    public void NextMovement() {
        if (turnNumberController >= listCharacters.Count) {
            turnNumberController = 1;
            if (OnUpdateTurn != null) OnUpdateTurn();
        } else {
            turnNumberController++;
        }        
    }

    public bool IsCharacterListEmpty() {
        return listCharacters.Count == 0;
    }

    public bool RemoveCharacter(Character currentCharacter) {
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
