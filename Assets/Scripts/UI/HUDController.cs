using UnityEngine;
using UnityEngine.UI;

class HUDController : MonoBehaviour {

    const string info = "Turno: {0}; \nPlayer: {1}";
    [SerializeField]
    [Multiline]
    Text turnInfo;

    void Start() {
        DuelController.OnPopupTurn += UpdateTextInfo;
    }

    void OnDestroy() {
        DuelController.OnPopupTurn -= UpdateTextInfo;
    }

    void UpdateTextInfo(int turnNumber, Player playerName, bool nextTurn) {
        turnInfo.text = string.Format(info, turnNumber, playerName.Name);
    }

}

