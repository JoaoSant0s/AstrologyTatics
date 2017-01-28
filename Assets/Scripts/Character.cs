using UnityEngine;
using System.Collections.Generic;
using Editor.Layout;
using Common.Layout;
using System;

public class Character : MonoBehaviour {

    public delegate void DefiningPaths(Vector3 postionTile, List<Vector3> paths, bool activePaths);
    public static event DefiningPaths OnDefiningPaths;    

    public delegate bool BlockClick(Character character);
    public static event BlockClick OnBlockClick;

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
        if (!characterMovement) return;

        if (OnVerifyClick != null) {
            var currentTile = OnVerifyClick(postionCharacter);
                        
            if (currentTile.ActiveArea) {                
                currentTile.MoveCharacter();
                if (OnRemoveCharacter != null) OnRemoveCharacter(this);                
                return;
            }            
        }

        if (OnBlockClick != null) {
            if (OnBlockClick(this)) {
                Debug.Log("Is not the current player");
                return;
            }
        }

        activePaths = !activePaths;
        if (OnDefiningPaths != null) OnDefiningPaths(postionCharacter, paths, activePaths);

        GameController.Instance.SavedCharacter = this;
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
