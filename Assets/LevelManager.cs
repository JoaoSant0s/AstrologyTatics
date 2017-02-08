using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    LevelSystem module;

    void Awake() {
        DuelController.OnDefineOthersPlayers += GetPlayers;
    }


    void OnDestroy() {
        DuelController.OnDefineOthersPlayers -= GetPlayers;
    }

    LevelData GetPlayers() {
        return module.GetCurrentLevel();
    }

}
