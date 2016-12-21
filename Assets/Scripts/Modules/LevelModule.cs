using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LevelModule {

    [SerializeField]
    LevelsModule.Level level;

    [SerializeField]
    List<CharacterData> charactersLevel;

    public void DefineCharecterLevel(GameObject charactersSet) {

        foreach (var character in charactersLevel) {
            var characterData = DataCharactersData.Instance.CharacterPrefab(character.Type);

            var auxCharacter = GameObject.Instantiate(characterData.Module, character.Position, Quaternion.identity) as Character;

            auxCharacter.SetPaths(characterData.Layout);

            auxCharacter.transform.SetParent(charactersSet.transform);
        }

    }

}
