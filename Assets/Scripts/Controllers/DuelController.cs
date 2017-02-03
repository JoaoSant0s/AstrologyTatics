using UnityEngine;
using System.Collections.Generic;
using Common.Layout;

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

    public List<Player> Players {
        get {
            return players;
        }
    }
    public static DuelController instance;

    public static DuelController Instance {
        get { return instance; }
    }

    void GetCurrentPlayers() {
        if (OnDefinedPlayers != null) players = OnDefinedPlayers();        

        if (players == null) Debug.LogError("Don't have players");        
    }

    void Awake() {
        instance = this;
        CampaingUIController.OnDefiningPlayers += DefineLevel;
        Character.OnRemoveCharacter += RemoveCharacter;
        Character.OnPlayerDuel += GetCurrentPlayer;
        Player.OnUpdateTurn += NextTurn;

    }

    void OnDestroy() {
        CampaingUIController.OnDefiningPlayers -= DefineLevel;
        Character.OnRemoveCharacter -= RemoveCharacter;
        Character.OnPlayerDuel -= GetCurrentPlayer;
        Player.OnUpdateTurn -= NextTurn;
    }

    Player GetCurrentPlayer() {
        return currentPlayer;
    }

    void DefineLevel(List<Vector2> elements) {
        Player humanPlayer = players.Find(x => x.Type == Player.TypePlayer.human);
        humanPlayer.SetLimitElements(elements);        
        
        foreach (var player in players) {            
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

    internal void RemoveCharacter(Character currentCharacter) {
        foreach (var item in players) {
            if (item.RemoveCharacter(currentCharacter)) {
                break;
            }
        }        
        GameObject.DestroyObject(currentCharacter.gameObject);
    }
}
