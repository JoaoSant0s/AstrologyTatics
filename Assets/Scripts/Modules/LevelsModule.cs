using UnityEngine;
using System.Collections.Generic;

public class LevelsModule : ScriptableObject {

    public enum Level {
        level1,
        level2,
        level3
    }

    [SerializeField]
    List<LevelModule> levels;

    public void DefineLevel(GameObject charactersSet) {
        foreach (var level in levels) {
            level.DefineCharecterLevel(charactersSet);
        }
    }

}
