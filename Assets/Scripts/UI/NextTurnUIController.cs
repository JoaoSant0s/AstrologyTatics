using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class NextTurnUIController : MonoBehaviour {

    [SerializeField]
    Text turnNumber;

    internal void UpdateTexts(int numberTurn) {
        turnNumber.text = numberTurn.ToString();     
    }
}
