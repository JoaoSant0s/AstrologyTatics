using UnityEngine;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour {

    [SerializeField]
    PlayerModule module;

    void Awake() {
        DuelController.OnDefinedPlayers += GetPlayers;
    }


    void OnDestroy() {
        DuelController.OnDefinedPlayers -= GetPlayers;
    }

    List<Player> GetPlayers() {
        return module.Levels;
    }    

}
