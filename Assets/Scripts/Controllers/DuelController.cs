using UnityEngine;
using System.Collections.Generic;

public class DuelController : MonoBehaviour {

    public delegate List<Player> DefinedPlayers();
    public static event DefinedPlayers OnDefinedPlayers;

    public delegate void PopupTurn(int turnNumber, Player currentPlayer, bool isNextTurn);
    public static event PopupTurn OnPopupTurn;

    [SerializeField]
    GameObject charactersSet;

    Player currentPlayer;
    const int INIT_COUNTER = 1;
    const int INIT_NEXT_COUNTER = 0;
    int turnCounter;
    int nextTurnCounter;
    List<Player> players;

    List<Player> GetCurrentPlayers() {
        if (OnDefinedPlayers != null) players = OnDefinedPlayers();        

        if (players == null) Debug.LogError("Don't have players");

        return players;
    }
    void Awake() {
        CampaingUIController.OnDefiningPlayers += DefineLevel;
        Character.OnBlockClick += UpdateCharactersPaths;
        Character.OnRemoveCharacter += RemoveCharacter;
        Player.OnUpdateTurn += NextTurn;
    }

    void OnDestroy() {
        CampaingUIController.OnDefiningPlayers -= DefineLevel;
        Character.OnBlockClick -= UpdateCharactersPaths;
        Character.OnRemoveCharacter += RemoveCharacter;
        Player.OnUpdateTurn -= NextTurn;
    }

    void DefineLevel() {
        foreach(var player in players) {            
            player.DefineCharacters(charactersSet);
        }       
    }

    void Start() {
        GetCurrentPlayers();
        currentPlayer = players[nextTurnCounter];
        turnCounter = INIT_COUNTER;
        nextTurnCounter = INIT_NEXT_COUNTER;

        OnPopupTurn(turnCounter, currentPlayer, false);
    }

    void NextTurn() {
        nextTurnCounter++;
        var auxNextTorn = turnCounter;
        
        if (nextTurnCounter >= players.Count) {
            turnCounter++;
            nextTurnCounter = INIT_NEXT_COUNTER;
            ClearMovements();           
        }
        currentPlayer = players[nextTurnCounter];
        
        if (OnPopupTurn != null) OnPopupTurn(turnCounter, currentPlayer, auxNextTorn != turnCounter);
    }

    void ClearMovements() {
        foreach(var player in players) {
            foreach(var character in player.ListCharacters) {               
                character.SetCharacterMovement(true);
            }
        }
    }

    bool UpdateCharactersPaths(Character character) {            
        if (character.Player != currentPlayer)
            return true;

        return false;
    }

    internal void RemoveCharacter(Character currentCharacter) {
        currentPlayer.RemoveCharacter(currentCharacter);      
    }



}
