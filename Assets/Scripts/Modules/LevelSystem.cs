using UnityEngine;
using System.Collections.Generic;

public class LevelSystem : ScriptableObject {
    
    public enum LevelType { 
        level_1,
        level_2,
        level_3
    }

    [SerializeField]
    LevelType currentLevel;
    [SerializeField]
    List<LevelData> levels;


    public LevelData GetCurrentLevel() {
        return levels.Find(x => x.LevelName == currentLevel);
    }

}
