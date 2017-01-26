using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TurnUIController : MonoBehaviour {

    const string TEXT_TURN_NUMBER = "Turno: {0}";
    const string TEXT_PLAYER_NAME = "Player: {0}";
    
    [SerializeField]
    Text turnNumber;

    [SerializeField]
    Text playerName;

    internal void UpdateTexts(int numberTurn, string nanmePlayer) {
        turnNumber.text = String.Format(TEXT_TURN_NUMBER, numberTurn);
        playerName.text = String.Format(TEXT_PLAYER_NAME, nanmePlayer);
    }
}
