using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LevelModule {

    [SerializeField]
    LevelsModule.Level level;

    [SerializeField]
    List<CharacterData> charactersLevel;

    public void DefineCharecterLevel(Transform charactersSet) {

        foreach (var character in charactersLevel) {
            var prefab = DataCharactersData.Instance.CharacterPrefab(character.Type);
            var auxCharacter = GameObject.Instantiate(prefab, character.Position, Quaternion.identity) as Character;

            auxCharacter.transform.SetParent(charactersSet);
        }

    }

}
