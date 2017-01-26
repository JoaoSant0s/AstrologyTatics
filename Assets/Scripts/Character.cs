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

    void Awake() {
        activePaths = false;
    }

    public List<Vector3> Paths {
        get {
            return paths;
        }
    }

    public bool ActivePaths {
        get {
            return activePaths;
        }
        set {
            activePaths = value;
        }
    }

    void OnMouseDown() {     

        if (OnVerifyClick != null) {
            var currentTile = OnVerifyClick(postionCharacter);
                        
            if (currentTile.ActiveArea) {
                currentTile.MoveCharacter();
                if (OnRemoveCharacter != null) OnRemoveCharacter(this);                
                return;
            }            
        }

        if (OnBlockClick != null) {
            if (OnBlockClick(this)) return;
        }

        activePaths = !activePaths;
        if (OnDefiningPaths != null) OnDefiningPaths(postionCharacter, paths, activePaths);

        GameController.Instance.SavedCharacter = this;
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

}
