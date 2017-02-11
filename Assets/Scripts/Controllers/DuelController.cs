using UnityEngine;
using System.Collections.Generic;
using Common.Layout;

public class DuelController : MonoBehaviour {

    public delegate LevelData DefineOthersPlayers();
    public static event DefineOthersPlayers OnDefineOthersPlayers;

    public delegate Player DefinePlayer();
    public static event DefinePlayer OnDefinePlayer;

    public delegate void PopupTurn(int turnNumber, Player currentPlayer, bool isNextTurn);
    public static event PopupTurn OnPopupTurn;

    public delegate void VictoryEvent(bool victory);
    public static event VictoryEvent OnVictoryEvent;
    public static event VictoryEvent OnVictoryPopup;

    [SerializeField]
    GameObject charactersSet;

    Player currentPlayer;
    const int INIT_COUNTER = 1;
    const int INIT_NEXT_COUNTER = 0;
    int turnCounter;
    int nextTurnCounter;

    LevelData level;
    Player playerHuman;
    List<Player> playerList;

    public LevelData Level {
        get {
            return level;
        }
    }
    public Player PlayerHuman {
        get {
            return playerHuman;
        }
    }

    public static DuelController instance;

    public static DuelController Instance {
        get { return instance; }
    }

    void GetCurrentPlayers() {
        if (OnDefineOthersPlayers != null) level = OnDefineOthersPlayers();
        if (OnDefinePlayer != null) playerHuman = OnDefinePlayer();        

        if (level == null || playerHuman == null) Debug.LogError("Don't have players");        
    }

    void Awake() {        
        playerList = new List<Player>();
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
        DestroyCharacterGameObjects();
        foreach (var player in playerList) {
            if(player.Type == Player.TypePlayer.human) {                
                playerHuman.SetLimitElements(elements);
            }            
            player.DefineCharacters(charactersSet);
        }
    }

    void DestroyCharacterGameObjects() {
        for(var cont = 0; cont < charactersSet.transform.childCount; cont++) {
            DestroyObject(charactersSet.transform.GetChild(cont).gameObject);
        }
    }

    void AddPlayersList() {       
        playerList.Add(playerHuman);

        foreach (var player in level.Players) {
            playerList.Add(player);            
        }
    }

    void Start() {
        GetCurrentPlayers();
        AddPlayersList();

        currentPlayer = playerList[nextTurnCounter];
        turnCounter = INIT_COUNTER;
        nextTurnCounter = INIT_NEXT_COUNTER;

        OnPopupTurn(turnCounter, currentPlayer, false);
    }

    void NextTurn() {
        nextTurnCounter++;
        var auxNextTorn = turnCounter;
        
        if (nextTurnCounter >= playerList.Count) {
            turnCounter++;
            nextTurnCounter = INIT_NEXT_COUNTER;
            ClearMovements();           
        }
        currentPlayer = playerList[nextTurnCounter];
        
        if (OnPopupTurn != null) OnPopupTurn(turnCounter, currentPlayer, auxNextTorn != turnCounter);
    }

    void ClearMovements() {
        foreach(var player in playerList) {
            foreach(var character in player.ListCharacters) {               
                character.SetCharacterMovement(true);
            }
        }
    }

    public void RemoveCharacter(Character currentCharacter) {
        foreach (var auxPlayer in playerList) {
            if (auxPlayer.RemoveCharacter(currentCharacter)) {
                RemovePlayer(auxPlayer);
                break;
            }
        }        
        GameObject.DestroyObject(currentCharacter.gameObject);
    }

    void RemovePlayer(Player auxPlayer) {
        if (auxPlayer.IsCharacterListEmpty()) {
            playerList.Remove(auxPlayer);
            nextTurnCounter--;
            VerifyVictory();
        }
    }

    private void VerifyVictory() {
        if (OnVictoryEvent != null || OnVictoryPopup != null) {
            var human = playerList.Find(x => x.Type == Player.TypePlayer.human);
            var machine = playerList.Find(x => x.Type == Player.TypePlayer.machine);

            if (human == null) {
                OnVictoryPopup(false);
                GameController.Instance.UserInteraction(false);
                OnVictoryEvent(false);
            }else if (machine == null) {
                OnVictoryPopup(true);
                GameController.Instance.UserInteraction(false);
                OnVictoryEvent(true);
            }


        }
            
    }
}
