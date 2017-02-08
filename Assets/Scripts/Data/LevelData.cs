using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LevelData {
    [SerializeField]
    LevelSystem.LevelType levelName;

    [SerializeField]
    List<Player> players;


    public LevelSystem.LevelType LevelName {
        get {
            return levelName;
        }
    }

    public List<Player> Players {
        get {
            return players;
        }
    }

}
