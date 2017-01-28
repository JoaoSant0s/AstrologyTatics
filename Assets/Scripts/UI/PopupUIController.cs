﻿using UnityEngine;
using System.Collections;
using Common.Layout;

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
        GameController.Instance.UserInteraction(false);
        if (isNextTurn) {            
            StartCoroutine(NextTurnCourotine(turnNumber, currentPlayer.Name));           
        } else {
            StartCoroutine(TurnCourotine(turnNumber, currentPlayer.Name));
        }
    }

    IEnumerator NextTurnCourotine(int turnNumber, string playerName) {
        nextTurnUIController.UpdateTexts(turnNumber);
        yield return new WaitForSeconds(0.5f);
        nextTurnUIController.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        nextTurnUIController.gameObject.SetActive(false);
        StartCoroutine(TurnCourotine(turnNumber, playerName));
    }

    IEnumerator TurnCourotine(int turnNumber, string playerName) {
        turnUIController.UpdateTexts(turnNumber, playerName);
        yield return new WaitForSeconds(0.5f);
        turnUIController.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        turnUIController.gameObject.SetActive(false);

        GameController.Instance.UserInteraction(true);
    }

}
