using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour {

    [SerializeField]
    PlayerModule module;

    void Awake() {
        DuelController.OnDefinePlayer += GetPlayer;
    }


    void OnDestroy() {
        DuelController.OnDefinePlayer -= GetPlayer;
    }

    Player GetPlayer() {
        return module.Player;
    }
}
