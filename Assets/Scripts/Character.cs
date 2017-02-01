using UnityEngine;
using System.Collections.Generic;
using Editor.Layout;
using Common.Layout;
using System;

public class Character : MonoBehaviour {

    public delegate void DefiningPaths(Vector3 postionTile, List<Vector3> paths, bool activePaths);
    public static event DefiningPaths OnDefiningPaths;

    public delegate Player PlayerDuel();
    public static event PlayerDuel OnPlayerDuel;

    public delegate void ClearAllTiles();
    public static event ClearAllTiles OnClearAllTiles;

    public delegate Tile VerifyClick(Vector3 postionCharacter);
    public static event VerifyClick OnVerifyClick;

    public delegate void RemoveCharacter(Character character);
    public static event RemoveCharacter OnRemoveCharacter;

    private List<Vector3> paths;
    private Vector3 postionCharacter;
    bool activePaths;
    bool characterMovement;
    Player player;

    void Awake() {
        characterMovement = true;
        activePaths = false;
        GameController.OnDefiningUserInteraction += SetCharacterMovement;
    }

    void OnDestroy() {
        GameController.OnDefiningUserInteraction -= SetCharacterMovement;
    }

    public List<Vector3> Paths {
        get {
            return paths;
        }
    }

    public bool GetCharacterMovement() {
        return characterMovement;
    }

    public void SetCharacterMovement(bool value) {
        characterMovement = value;       
    }


    public bool ActivePaths {
        get {
            return activePaths;
        }
        set {
            activePaths = value;
        }
    }

    internal void UpdatePlayerMovement() {
        characterMovement = false;
        player.NextMovement();
    }

    void OnMouseDown() {
        Player currentPlayerDuel = null;
        if (OnPlayerDuel != null) {
            currentPlayerDuel = OnPlayerDuel();
        }

        if (currentPlayerDuel == null) return;        

        if(GameController.Instance.SavedCharacter == null) {
            if (!characterMovement) return;
            if (currentPlayerDuel == player) {
                activePaths = true;
                GameController.Instance.SavedCharacter = this;
                if (OnDefiningPaths != null) OnDefiningPaths(postionCharacter, paths, activePaths);
            } else {
                Debug.Log("Is not the current player!");
                return;
            }
        }else {
            if (currentPlayerDuel == player) {
                if (GameController.Instance.SavedCharacter == this) {
                    activePaths = false;
                    GameController.Instance.SavedCharacter = null;
                    if (OnClearAllTiles != null) OnClearAllTiles();
                } else {
                    Debug.Log("Don't have friendly fire!");
                    return;
                }
            } else {
                if (OnVerifyClick != null) {
                    var currentTile = OnVerifyClick(postionCharacter);
                    if (currentTile.ActiveArea) {
                        currentTile.MoveCharacter();
                        if (OnRemoveCharacter != null) OnRemoveCharacter(this);
                    } else {
                        Debug.Log("Is not in fight zone");
                    }
                }
            }
        }       
    }

    internal Player Player {
        get {
            return player;
        }
        set {
            player = value;
        }      
    }

    internal void SetPaths(ArrayLayout layoutPaths) {
        paths = LayoutDefinition.ListPaths(layoutPaths);
    }

    internal Vector3 PostionCharacter {
        get {
            return postionCharacter;
        } set {
            postionCharacter = value;
        }
    }

    internal void UpdateColor() {
        GetComponent<Renderer>().material.color = player.CurrentColor;
    }

}
