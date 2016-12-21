using UnityEngine;
using System.Collections.Generic;

public class LevelsModule : ScriptableObject {

    public enum Level {
        level1,
        level2,
        level3
    }

    [SerializeField]
    Transform charactersSet;

    [SerializeField]
    List<LevelModule> levels;

    public void DefineLevel() {

        foreach (var level in levels) {
            level.DefineCharecterLevel(charactersSet);
        }

    }

}
