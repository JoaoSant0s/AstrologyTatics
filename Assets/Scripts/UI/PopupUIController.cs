using UnityEngine;
using System.Collections;

public class PopupUIController : MonoBehaviour {
    [SerializeField]
    NextTurnUIController nextTurnUIController;

    [SerializeField]
    TurnUIController turnUIController;

    void Awake() {
        DuelController.OnPopupTurn += PopDefinition;
    }


    void OnDestroy() {
        DuelController.OnPopupTurn -= PopDefinition;
    }

    void PopDefinition(int turnNumber, Player currentPlayer, bool isNextTurn) {
        if (isNextTurn) {
            StartCoroutine(NextTurnCourotine(turnNumber, currentPlayer.Name));           
        } else {
            StartCoroutine(TurnCourotine(turnNumber, currentPlayer.Name));
        }
    }


    IEnumerator NextTurnCourotine(int turnNumber, string playerName) {
        nextTurnUIController.UpdateTexts(turnNumber);
        yield return new WaitForSeconds(1);
        nextTurnUIController.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        nextTurnUIController.gameObject.SetActive(false);

        TurnCourotine(turnNumber, playerName);
    }

    IEnumerator TurnCourotine(int turnNumber, string playerName) {
        turnUIController.UpdateTexts(turnNumber, playerName);
        yield return new WaitForSeconds(1);
        turnUIController.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        turnUIController.gameObject.SetActive(false);
    }

}
