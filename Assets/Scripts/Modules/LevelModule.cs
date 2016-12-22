using UnityEngine;
using System.Collections.Generic;
using Common.Layout;

[System.Serializable]
public class LevelModule {

    [SerializeField]
    LevelsModule.Level level;

    [SerializeField]
    List<CharacterData> charactersLevel;

    public void DefineCharecterLevel(GameObject charactersSet) {

        foreach (var character in charactersLevel) {
            var characterData = DataCharactersData.Instance.CharacterPrefab(character.Type);

            Vector3 position = LayoutDefinition.ConvertPostion(character.Position);
            var auxCharacter = GameObject.Instantiate(characterData.Module, position, Quaternion.identity) as Character;

            auxCharacter.SetPaths(characterData.Layout);
            auxCharacter.PostionCharacter = position;

            auxCharacter.transform.SetParent(charactersSet.transform);
        }

    }

}
