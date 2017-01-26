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
    }

    void OnDestroy() {
        CampaingUIController.OnDefiningPlayers -= DefineLevel;
        Character.OnBlockClick -= UpdateCharactersPaths;
        Character.OnRemoveCharacter += RemoveCharacter;
    }

    void DefineLevel() {
        foreach(var player in players) {
            Debug.Log(player.Name);
            player.DefineCharacters(charactersSet);
        }       
    }

    void Start() {
        GetCurrentPlayers();
        currentPlayer = players[nextTurnCounter];
    }

    void NextTurn() {
        nextTurnCounter++;
        var auxNextTorn = turnCounter;
        if (nextTurnCounter >= players.Count) {
            turnCounter++;
            nextTurnCounter = INIT_NEXT_COUNTER;
        }
        currentPlayer = players[nextTurnCounter];

        if (OnPopupTurn != null) OnPopupTurn(turnCounter, currentPlayer, auxNextTorn != turnCounter);                
    }

    bool UpdateCharactersPaths(Character player) {     
        var containPlayer = currentPlayer.ListCharacters.Contains(player);

        Debug.Log(currentPlayer.Name + " " + containPlayer);
        if (!containPlayer)
            return true;

        return false;
    }

    internal void RemoveCharacter(Character currentCharacter) {
        currentPlayer.RemoveCharacter(currentCharacter);      
    }



}
